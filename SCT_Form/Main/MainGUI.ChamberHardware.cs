using System;
using System.Collections.Generic;

namespace SCT_Form
{
    // PM 챔버 도어/램프, 로봇 실린더/진공 흡착, 관련 센서 읽기 등 EtherCAT 디지털 I/O를
    // 직접 다루는 하드웨어 액션 메서드 모음. AutoSequenceBuilder와 MaintGUI 수동 버튼이
    // 이 메서드들을 통해서만 챔버 하드웨어를 건드린다.
    public partial class MainGUI
    {
        internal void SetChamberDoorStatus(string pmName, bool isOpen)
        {
            string normalizedPmName = NormalizePmName(pmName);

            if (normalizedPmName == "PM A")
            {
                isChamADoorOpen = isOpen;
            }
            else if (normalizedPmName == "PM B")
            {
                isChamBDoorOpen = isOpen;
            }
            else if (normalizedPmName == "PM C")
            {
                isChamCDoorOpen = isOpen;
            }

            if (currentGUI != null && !currentGUI.IsDisposed)
            {
                currentGUI.SetDoorStatus(normalizedPmName, isOpen);
            }
        }

        private const long RobotFacingToleranceCounts = 500;

        internal void OpenChamberDoor(string module)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            EtherCAT_M.Digital_Output(profile.DoorOpenOutput, true);
            EtherCAT_M.Digital_Output(profile.DoorCloseOutput, false);
            SetChamberDoorStatus(EquipmentLayout.NormalizeModule(module), true);
        }

        internal void CloseChamberDoor(string module)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            EtherCAT_M.Digital_Output(profile.DoorOpenOutput, false);
            EtherCAT_M.Digital_Output(profile.DoorCloseOutput, true);
            SetChamberDoorStatus(EquipmentLayout.NormalizeModule(module), false);
        }

        private const int ChamberLampBlinkCount = 5;
        private const int ChamberLampBlinkIntervalMs = 1000;
        private readonly Dictionary<string, System.Windows.Forms.Timer> chamberLampBlinkTimers = new Dictionary<string, System.Windows.Forms.Timer>();

        // 진행 중인 깜빡임을 취소하고 즉시 On/Off를 확정한다. 수동 제어(MaintGUI)와
        // 자동 시퀀스의 "공정 시작" 액션이 이 메서드를 쓴다.
        internal void SetChamberLamp(string module, bool on)
        {
            string normalized = EquipmentLayout.NormalizeModule(module);
            StopChamberLampBlink(normalized);
            SetChamberLampOutput(normalized, on);
        }

        // 공정 종료 시 램프를 바로 끄는 대신 1초 간격으로 On/Off를 5회 반복(총 9회 토글,
        // 약 9초)한 뒤 Off로 마무리한다. 백그라운드 System.Windows.Forms.Timer로 동작하므로
        // 호출 직후 자동 시퀀스의 다음 동작(로봇 언로딩 등)은 블로킹 없이 바로 진행된다.
        internal void BlinkChamberLamp(string module)
        {
            string normalized = EquipmentLayout.NormalizeModule(module);
            StopChamberLampBlink(normalized);

            int toggleCount = 0;
            int totalToggles = (ChamberLampBlinkCount * 2) - 1;
            SetChamberLampOutput(normalized, true);

            System.Windows.Forms.Timer blinkTimer = new System.Windows.Forms.Timer();
            blinkTimer.Interval = ChamberLampBlinkIntervalMs;
            blinkTimer.Tick += (sender, e) =>
            {
                toggleCount++;
                SetChamberLampOutput(normalized, toggleCount % 2 == 0);
                if (toggleCount >= totalToggles)
                {
                    StopChamberLampBlink(normalized);
                }
            };

            chamberLampBlinkTimers[normalized] = blinkTimer;
            blinkTimer.Start();
        }

        private void StopChamberLampBlink(string module)
        {
            System.Windows.Forms.Timer blinkTimer;
            if (chamberLampBlinkTimers.TryGetValue(module, out blinkTimer))
            {
                blinkTimer.Stop();
                blinkTimer.Dispose();
                chamberLampBlinkTimers.Remove(module);
            }
        }

        private void SetChamberLampOutput(string module, bool on)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            EtherCAT_M.Digital_Output(profile.LampOutput, on);
            EtherCAT_M.Digital_Output(1, !on);
            EtherCAT_M.Digital_Output(2, on);
        }

        internal void MoveCylinderFront()
        {
            EtherCAT_M.Digital_Output(13, false);
            EtherCAT_M.Digital_Output(12, true);
        }

        internal void MoveCylinderBack()
        {
            EtherCAT_M.Digital_Output(12, false);
            EtherCAT_M.Digital_Output(13, true);
        }

        internal void SetWaferSuction(bool on)
        {
            isWaferSuctionOn = on;
            EtherCAT_M.Digital_Output(14, on);
            if (currentGUI != null && !currentGUI.IsDisposed)
            {
                currentGUI.SetRobotWaferState(on);
            }
        }

        internal void SetWaferExhaust(bool on)
        {
            EtherCAT_M.Digital_Output(15, on);
        }

        internal void SetAllChamberLamps(bool on)
        {
            SetChamberLamp("PM A", on);
            SetChamberLamp("PM B", on);
            SetChamberLamp("PM C", on);
        }

        internal bool IsCylinderForward()
        {
            return EtherCAT_M.Digital_Input(13);
        }

        internal bool IsCylinderBack()
        {
            return EtherCAT_M.Digital_Input(12) && !EtherCAT_M.Digital_Input(13);
        }

        internal string GetCylinderSensorSnapshot()
        {
            return "BackInput12=" + EtherCAT_M.Digital_Input(12) + ", FrontInput13=" + EtherCAT_M.Digital_Input(13);
        }

        internal bool IsChamberDoorOpen(string module)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            return EtherCAT_M.Digital_Input(profile.DoorDownSensor);
        }

        internal bool IsChamberDoorClosed(string module)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            return EtherCAT_M.Digital_Input(profile.DoorUpSensor);
        }

        internal bool IsRobotFacingModule(string module)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            long current;
            if (!long.TryParse(EtherCAT_M.Axis2_is_PosData(), out current)) return false;
            return Math.Abs(current - profile.LR) <= RobotFacingToleranceCounts;
        }
    }
}
