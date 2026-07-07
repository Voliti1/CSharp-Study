using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SCT_Form
{
    // 로봇 축(UD=Axis1 상하, LR=Axis2 좌우) 서보 on/off, 원점복귀, 위치 이동 명령과
    // 그 위치 도달을 확인하는 안전장치(위치 검증 타임아웃, 상한 소프트 리밋)를 담당한다.
    // 실제 이동 자체는 여기서 시작만 시키고, 도달 여부 확인은 timer1(200ms, core 파일의
    // timer1_Tick)와 AutoSequenceBuilder의 WaitSensor 스텝이 이 파일의 Is...AtPosition류
    // 메서드를 폴링해서 판단한다.
    public partial class MainGUI
    {
        internal void servoMotorON()
        {
            ApplySettingRobotAxisConfig();
            EtherCAT_M.Axis1_ON();
            EtherCAT_M.Axis2_ON();
        }

        internal void servoMotorOFF()
        {
            EtherCAT_M.Axis1_OFF();
            EtherCAT_M.Axis2_OFF();
        }
        internal void setBasicPoint()
        {
            HomeAxis1UD(); //상하 원점복귀
            HomeAxis2LR(); //좌우 원점복귀
        }

        internal void RecoverCylinderThenHomeAtStartup()
        {
            if (!IsCylinderBack())
            {
                WriteSystemLog("WARN", "Startup recovery: cylinder is not back. Cylinder back output applied before homing.");
                MoveCylinderBack();

                if (!WaitUntilCylinderBack(StartupCylinderBackTimeoutMs))
                {
                    WriteSystemLog("ERROR", "Startup recovery canceled: cylinder back sensor was not confirmed. Homing skipped.");
                    MessageBox.Show(
                        "실린더 후진 센서가 확인되지 않아 원점복귀를 실행하지 않았습니다.\r\n실린더 상태를 확인한 뒤 다시 시도해주세요.",
                        "Startup Safety",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            WriteSystemLog("INFO", "Startup recovery: cylinder back confirmed. Homing started.");
            CloseAllChamberDoors();
            setBasicPoint();
        }

        // Abort 시 안전 복구: 흡착/램프를 끄고 실린더를 후진시킨 뒤(확인되면) 원점복귀까지 실행한다.
        // 실린더 후진이 확인되지 않으면 원점복귀는 걸지 않고 사용자에게 알린다(무리한 자동 이동 방지).
        internal bool SafeAbortAndHome()
        {
            try
            {
                WriteSystemLog("WARN", "Abort safety recovery started.");
                SetWaferExhaust(false);
                SetAllChamberLamps(false);
                MoveCylinderBack();

                if (!WaitUntilCylinderBack(StartupCylinderBackTimeoutMs))
                {
                    WriteSystemLog("ERROR", "Abort safety recovery canceled: cylinder back sensor was not confirmed. " + GetCylinderSensorSnapshot());
                    MessageBox.Show(
                        "실린더 후진 센서가 확인되지 않아 Abort 후 원점복귀를 실행하지 않았습니다.\r\n실린더 상태를 확인해주세요.",
                        "Abort Safety",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                setBasicPoint();
                WriteSystemLog("WARN", "Abort safety recovery completed: cylinder back confirmed and homing started.");
                return true;
            }
            catch (Exception ex)
            {
                WriteSystemLog("ERROR", "Abort safety recovery failed: " + ex.Message);
                MessageBox.Show(
                    "Abort 안전 복구 중 오류가 발생했습니다.\r\n" + ex.Message,
                    "Abort Safety",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        private void CloseAllChamberDoors()
        {
            if (!IsCylinderBack())
            {
                WriteSystemLog("WARN", "Chamber door close skipped: cylinder back sensor is not confirmed.");
                return;
            }

            EtherCAT_M.Digital_Output(5, false);
            EtherCAT_M.Digital_Output(4, true);
            EtherCAT_M.Digital_Output(8, false);
            EtherCAT_M.Digital_Output(7, true);
            EtherCAT_M.Digital_Output(11, false);
            EtherCAT_M.Digital_Output(10, true);
            WriteSystemLog("INFO", "Chamber door close output applied after cylinder back confirmation.");
        }

        internal void HomeAxis1UD()
        {
            ClearAxisTargetPosition(Axis1TargetPositionWriteOffset);
            EtherCAT_M.Axis1_UD_Homming();
        }

        internal void HomeAxis2LR()
        {
            ClearAxisTargetPosition(Axis2TargetPositionWriteOffset);
            EtherCAT_M.Axis2_LR_Homming();
        }

        internal void MoveAxis1UDTo(long targetPosition)
        {
            string currentPos = EtherCAT_M?.Axis1_is_PosData() ?? "N/A";
            bool wasMoving = IsAxis1Moving();
            WriteSystemLog("DEBUG", $"UD move requested. Target={targetPosition}, CurrentReadback={currentPos}, WasMoving={wasMoving}, WriteData[0-6] before={DumpWriteDataBytes(0, 7)}");

            if (isAxisPositionFault)
            {
                WriteSystemLog("WARN", $"UD move blocked. Axis position fault is active (Alarm Reset 필요). Target={targetPosition}");
                return;
            }

            // PP_D(target reached) 비트는 서보 정착 꼬리 동안 흔들려서(chatter), 직전 이동이
            // 사실상 끝났는데도 WasMoving=True로 판정되는 경우가 로그로 확인됐다.
            // "추적 중인 직전 이동이 있고(pending) 아직 목표 ±허용범위에 못 들어간" 경우에만
            // 진짜 이동 중으로 보고 차단한다. pending이 이미 도달 확인으로 지워진 뒤라면
            // PP_D가 흔들려도 새 명령을 받는다.
            bool previousMoveInFlight = pendingAxis1TargetPosition.HasValue
                && !IsAxis1AtPosition(pendingAxis1TargetPosition.Value, Axis1PositionToleranceCounts);

            if (wasMoving && previousMoveInFlight)
            {
                WriteSystemLog("WARN", $"UD move ignored. Previous move is still running. Target={targetPosition}");
                return;
            }

            EtherCAT_M.Axis1_UD_POS_Update(targetPosition);
            EtherCAT_M.Axis1_UD_Move_Send();
            ScheduleAxisControlwordSettle(0, 100);

            pendingAxis1TargetPosition = targetPosition;
            pendingAxis1VerifyDeadline = DateTime.Now.AddMilliseconds(Axis1PositionVerifyTimeoutMs);

            WriteSystemLog("DEBUG", $"UD move sent. Target={targetPosition}, WriteData[0-6] after={DumpWriteDataBytes(0, 7)}");
        }

        internal void MoveAxis2LRTo(long targetPosition)
        {
            string currentPos = EtherCAT_M?.Axis2_is_PosData() ?? "N/A";
            bool wasMoving = IsAxis2Moving();
            WriteSystemLog("DEBUG", $"LR move requested. Target={targetPosition}, CurrentReadback={currentPos}, WasMoving={wasMoving}, WriteData[23-29] before={DumpWriteDataBytes(23, 7)}");

            SettleAxisControlword(23);

            ClearAxisTargetPosition(Axis2TargetPositionWriteOffset);
            EtherCAT_M.Axis2_LR_POS_Update(targetPosition);
            EtherCAT_M.Axis2_LR_Move_Send();

            WriteSystemLog("DEBUG", $"LR move sent. Target={targetPosition}, WriteData[23-29] after={DumpWriteDataBytes(23, 7)}");
        }

        internal bool IsAxis1TargetReached()
        {
            return EtherCAT_M != null && EtherCAT_M.Axis1_Status("PP_D");
        }

        internal bool IsAxis1AtPosition(long expectedPosition, long toleranceCounts)
        {
            long currentPosition;
            if (!TryReadAxis1Position(out currentPosition)) return false;

            return Math.Abs(currentPosition - expectedPosition) <= toleranceCounts;
        }

        internal bool IsAxis2TargetReached()
        {
            return EtherCAT_M != null && EtherCAT_M.Axis2_Status("PP_D");
        }

        // 컨트롤워드를 15(Switch On|Enable Voltage|Quick Stop|Enable Operation, New-Setpoint/Change-Immediately=0)로
        // 내려 전송한다. IEG3268_Dll이 컨트롤워드만 단독으로 전송하는 public API를 제공하지 않으므로,
        // Digital_Output(기존 값 그대로 재기록)을 이용해 WriteData 전체를 flush시킨다.
        private void SettleAxisControlword(int controlWordOffset)
        {
            byte[] writeData = EtherCAT_M?.WriteData;
            if (writeData == null || writeData.Length <= controlWordOffset) return;

            writeData[controlWordOffset] = 15;
            EtherCAT_M.Digital_Output(0, EtherCAT_M.Digital_Out_Value[0]);
        }

        private void ResetAxisMoveCommand(int controlWordOffset, int targetPositionOffset)
        {
            SettleAxisControlword(controlWordOffset);
            Thread.Sleep(50);
            ClearAxisTargetPosition(targetPositionOffset);
        }

        private void LatchAndSettleAxisMoveCommand(int controlWordOffset)
        {
            ScheduleAxisControlwordSettle(controlWordOffset, 100);
        }

        private void ScheduleAxisControlwordSettle(int controlWordOffset, int delayMs)
        {
            System.Windows.Forms.Timer settleTimer = new System.Windows.Forms.Timer();
            settleTimer.Interval = Math.Max(1, delayMs);
            settleTimer.Tick += (sender, e) =>
            {
                settleTimer.Stop();
                settleTimer.Dispose();
                SettleAxisControlword(controlWordOffset);
            };
            settleTimer.Start();
        }

        private bool WaitUntilAxis1TargetReached(int timeoutMs)
        {
            DateTime deadline = DateTime.Now.AddMilliseconds(timeoutMs);
            while (DateTime.Now < deadline)
            {
                if (IsAxis1TargetReached()) return true;
                Thread.Sleep(100);
            }

            return IsAxis1TargetReached();
        }

        private bool WaitUntilCylinderBack(int timeoutMs)
        {
            DateTime deadline = DateTime.Now.AddMilliseconds(timeoutMs);
            while (DateTime.Now < deadline)
            {
                if (IsCylinderBack()) return true;
                Thread.Sleep(100);
                Application.DoEvents();
            }

            return IsCylinderBack();
        }

        private bool WaitUntilAxis1PositionBelow(long positionLimit, int timeoutMs)
        {
            DateTime deadline = DateTime.Now.AddMilliseconds(timeoutMs);
            long currentPosition;

            while (DateTime.Now < deadline)
            {
                if (TryReadAxis1Position(out currentPosition) && currentPosition < positionLimit)
                {
                    WriteSystemLog("INFO", $"UD shutdown home confirmed. Current={currentPosition}, Limit={positionLimit}");
                    return true;
                }

                Thread.Sleep(100);
            }

            if (TryReadAxis1Position(out currentPosition))
            {
                WriteSystemLog("WARN", $"UD shutdown home wait timeout. Current={currentPosition}, Limit={positionLimit}");
            }
            else
            {
                WriteSystemLog("WARN", "UD shutdown home wait timeout. Current position read failed.");
            }

            return false;
        }

        private bool TryReadAxis1Position(out long currentPosition)
        {
            currentPosition = 0;
            if (EtherCAT_M == null) return false;

            string positionText = EtherCAT_M.Axis1_is_PosData();
            return long.TryParse(positionText, NumberStyles.Integer, CultureInfo.InvariantCulture, out currentPosition);
        }

        private bool TryReadAxis2Position(out long currentPosition)
        {
            currentPosition = 0;
            if (EtherCAT_M == null) return false;

            string positionText = EtherCAT_M.Axis2_is_PosData();
            return long.TryParse(positionText, NumberStyles.Integer, CultureInfo.InvariantCulture, out currentPosition);
        }

        internal bool IsAxis2AtPosition(long expectedPosition, long toleranceCounts)
        {
            long currentPosition;
            if (!TryReadAxis2Position(out currentPosition)) return false;

            return Math.Abs(currentPosition - expectedPosition) <= toleranceCounts;
        }

        // timer1(200ms)에서 호출되는 비동기 위치 확인. UI를 멈추지 않고 매 tick마다
        // 목표값 도달 여부만 확인하다가, 허용범위 안에 들어오면 통과, 타임아웃까지
        // 못 들어오면 Axis Position Fault를 걸어 이후 UD 이동을 전부 차단한다.
        private void CheckAxis1PositionVerification()
        {
            if (!pendingAxis1TargetPosition.HasValue) return;

            long currentPosition;
            if (!TryReadAxis1Position(out currentPosition)) return;

            long target = pendingAxis1TargetPosition.Value;
            if (Math.Abs(currentPosition - target) <= Axis1PositionToleranceCounts)
            {
                pendingAxis1TargetPosition = null;
                return;
            }

            if (DateTime.Now < pendingAxis1VerifyDeadline) return;

            pendingAxis1TargetPosition = null;
            isAxisPositionFault = true;
            WriteSystemLog("Alarm", "ERROR", $"UD 위치 확인 실패: 목표={target}, 현재={currentPosition} (허용범위 ±{Axis1PositionToleranceCounts} 밖). 장비 동작이 차단되었습니다. Alarm Reset 필요.");

            if (settings != null)
            {
                ApplyTowerLampStatus(settings.AlarmLampStatus);
            }
        }

        // 특정 이동 명령의 목표값과 무관하게, 매 tick마다 UD 좌표가 상한 소프트 리밋을
        // 넘었는지 확인한다. 넘었다면 원인(과도한 튐, 리밋 스위치 접근 등)과 상관없이
        // 즉시 차단해서 기계적 끝단과의 추가 충돌을 막는다.
        private void CheckAxis1SoftLimit()
        {
            if (isAxisPositionFault) return;

            long currentPosition;
            if (!TryReadAxis1Position(out currentPosition)) return;
            if (currentPosition <= Axis1UpperSoftLimit) return;

            pendingAxis1TargetPosition = null;
            isAxisPositionFault = true;
            WriteSystemLog("Alarm", "ERROR", $"UD 축 상한 소프트 리밋 초과: 현재={currentPosition}, 리밋={Axis1UpperSoftLimit}. 장비 동작이 즉시 차단되었습니다. Alarm Reset 필요.");

            if (settings != null)
            {
                ApplyTowerLampStatus(settings.AlarmLampStatus);
            }
        }

        // 진단용: 이전 이동이 완료(Target Reached)됐는지 여부. 현재는 이동을 막지 않고 로그에만 남긴다.
        private bool IsAxis1Moving()
        {
            return EtherCAT_M != null && EtherCAT_M.Axis1_Status("PP_M") && !EtherCAT_M.Axis1_Status("PP_D");
        }

        private bool IsAxis2Moving()
        {
            return EtherCAT_M != null && EtherCAT_M.Axis2_Status("PP_M") && !EtherCAT_M.Axis2_Status("PP_D");
        }

        private string DumpWriteDataBytes(int start, int count)
        {
            byte[] writeData = EtherCAT_M?.WriteData;
            if (writeData == null || writeData.Length < start + count) return "N/A";

            return string.Join(" ", writeData.Skip(start).Take(count).Select(b => b.ToString("X2")));
        }

        private void ClearAxisTargetPosition(int startIndex)
        {
            byte[] writeData = EtherCAT_M?.WriteData;
            if (writeData == null || writeData.Length < startIndex + AxisTargetPositionByteCount) return;

            Array.Clear(writeData, startIndex, AxisTargetPositionByteCount);
        }

        internal void SetRobotAxisConfig(long acceleration, long deceleration, long maxVelocity, long velocity)
        {
            if (!isConnect || EtherCAT_M == null) return;

            EtherCAT_M.Axis1_UD_Config_Update(acceleration, deceleration, maxVelocity, velocity);
            EtherCAT_M.Axis2_LR_Config_Update(acceleration, deceleration, maxVelocity, velocity);
        }

        private void ApplySettingRobotAxisConfig()
        {
            if (settingGUI != null)
            {
                settingGUI.ApplyRobotAxisConfig();
                return;
            }

            SetRobotAxisConfig(DefaultRobotAcceleration, DefaultRobotDeceleration, DefaultRobotMaxVelocity, DefaultRobotVelocity);
        }
    }
}
