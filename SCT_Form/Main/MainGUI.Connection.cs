using System;
using System.Drawing;
using System.Windows.Forms;

namespace SCT_Form
{
    // EtherCAT 마스터 연결/재연결, 타워램프(적/황/녹) 수동 제어 및 모드별 자동 점등,
    // 상단 모드 버튼(Operate/Maint/Recipe/Log/Setting) 스타일 갱신을 담당한다.
    public partial class MainGUI
    {
        private void SystemConnect()
        {
            WriteSystemLog("INFO", "EtherCAT 마스터 연결 시도 중...");
            try
            {
                bool isConnected = false;
                int retryCount = settings == null ? 3 : settings.ReconnectRetryCount;
                for (int attempt = 0; attempt <= retryCount; attempt++)
                {
                    if (EtherCAT_M.CIFX_50RE_Connect() == true)
                    {
                        isConnected = true;
                        break;
                    }

                    if (attempt < retryCount)
                    {
                        WriteSystemLog("WARN", "EtherCAT 마스터 연결 재시도: " + (attempt + 1) + "/" + retryCount);
                    }
                }

                if (isConnected)
                {
                    WriteSystemLog("INFO", "EtherCAT 마스터 연결 성공 (Connect OK)");
                    isConnect = true;

                    EtherCAT_M.ReadData_Send_Start(settings.EtherCatReadCycleMs);
                    EtherCAT_M.ReadData_Timer_Start();
                    // 타이머 시작 같은 개발 분석용 세부 로직은 라벨을 가리지 않고 파일에만 남김
                    log.Debug("EtherCAT 데이터 리드 타이머 시작 (주기: " + settings.EtherCatReadCycleMs + "ms)");

                    lbl_CurrentConnect.ForeColor = Color.Lime;

                    // 플래그 초기화
                    isChamALampOn = false; isChamBLampOn = false; isChamCLampOn = false;
                    isChamADoorOpen = false; isChamBDoorOpen = false; isChamCDoorOpen = false;
                    if (currentGUI != null && !currentGUI.IsDisposed)
                    {
                        currentGUI.RefreshDoorStatusLabels();
                    }

                    currentUbarState = "Operate";

                    // 상단버튼 활성화
                    btn_Operate.Enabled = true;
                    btn_Maint.Enabled = true;

                    // 모드에 맞게 버튼 색상 스타일 출력
                    UpdateModeButtonStyles();

                    // 황색등 점등
                    ApplyTowerLampStatus(settings.IdleLampStatus);
                    WriteSystemLog("INFO", "타워램프 상태 변경: " + settings.IdleLampStatus + " (장비 대기)");
                }
                else
                {
                    WriteSystemLog("WARN", "EtherCAT 마스터 연결 실패 (하드웨어 감지 안 됨)");
                }
            }
            catch (Exception ex)
            {
                log.Error("EtherCAT 연결 처리 중 예외 발생: ", ex);
                WriteSystemLog("ERROR", $"연결 예외 오류: {ex.Message}");
            }
        }
        private void Reconnect_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            if (isConnect) return;
            SystemConnect();
            servoMotorON();
        }

        // --- 타워 램프 제어 영역 ---
        private void RedLightOn_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(0, true);
            WriteSystemLog("INFO", "수동 제어: 타워램프 적색등(Red) ON");
        }

        private void RedLightOff_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(0, false);
            WriteSystemLog("INFO", "수동 제어: 타워램프 적색등(Red) OFF");
        }

        private void YellowLightOn_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(1, true);
            WriteSystemLog("INFO", "수동 제어: 타워램프 황색등(Yellow) ON");
        }

        private void YellowLightOff_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(1, false);
            WriteSystemLog("INFO", "수동 제어: 타워램프 황색등(Yellow) OFF");
        }

        private void GreenLightOn_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(2, true);
            isGreenLightOn = true;
            WriteSystemLog("INFO", "수동 제어: 타워램프 녹색등(Green) ON");
        }

        private void GreenLightOff_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(2, false);
            isGreenLightOn = false;
            WriteSystemLog("INFO", "수동 제어: 타워램프 녹색등(Green) OFF");
        }

        private void AllLightOn_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(0, true);
            EtherCAT_M.Digital_Output(1, true);
            EtherCAT_M.Digital_Output(2, true);
            WriteSystemLog("INFO", "수동 제어: 타워램프 전체 등 ON");
        }

        private void AllLightOff_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(0, false);
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(2, false);
            WriteSystemLog("INFO", "수동 제어: 타워램프 전체 등 OFF");
        }

        private void ApplyTowerLampForMode()
        {
            if (settings == null) return;

            if (currentUbarState == "Maint" || currentUbarState == "Recipe" || currentUbarState == "Setting")
            {
                ApplyTowerLampStatus(settings.MaintenanceLampStatus);
                return;
            }

            ApplyTowerLampStatus(settings.IdleLampStatus);
        }

        private void ApplyTowerLampStatus(string status)
        {
            if (!isConnect || EtherCAT_M == null) return;

            string lampStatus = string.IsNullOrWhiteSpace(status) ? "Off" : status.Trim();
            bool redOn = string.Equals(lampStatus, "Red", StringComparison.OrdinalIgnoreCase);
            bool yellowOn = string.Equals(lampStatus, "Yellow", StringComparison.OrdinalIgnoreCase);
            bool greenOn = string.Equals(lampStatus, "Green", StringComparison.OrdinalIgnoreCase);

            EtherCAT_M.Digital_Output(0, redOn);
            EtherCAT_M.Digital_Output(1, yellowOn);
            EtherCAT_M.Digital_Output(2, greenOn);

            isGreenLightOn = greenOn;
        }
        private void ForceStopAllChambers()
        {
            WriteSystemLog("INFO", "모드 변경에 따른 전 공정 인터록(Force Stop) 가동");
            EtherCAT_M.Digital_Output(0, false);
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(2, false);
            EtherCAT_M.Digital_Output(3, false);
            EtherCAT_M.Digital_Output(6, false);
            EtherCAT_M.Digital_Output(9, false);

            isChamALampOn = false;
            isChamBLampOn = false;
            isChamCLampOn = false;

            log.Debug("인터록 완료: 모든 공정 인터록 및 GUI 상태 리셋 완료");
        }

        private void btnMode_Paint(object sender, PaintEventArgs e)
        {
            // 화면 렌더링 영역은 부하 조절 및 가독성을 위해 로그 생략
            Button btn = (Button)sender;
            bool isActive = (currentUbarState == "Operate" && btn == btn_Operate) ||
                            (currentUbarState == "Maint" && btn == btn_Maint) ||
                            (currentUbarState == "Recipe" && btn == btn_Recipe) ||
                            (currentUbarState == "Log" && btn == btn_Log) ||
                            (currentUbarState == "Setting" && btn == btn_Setting);

            Color highlight = isActive ? Color.Gray : Color.White;
            Color shadow = isActive ? Color.White : Color.Gray;
            int borderThickness = isActive ? 2 : 1;

            ControlPaint.DrawBorder(e.Graphics, btn.ClientRectangle,
                highlight, borderThickness, ButtonBorderStyle.Solid,
                highlight, borderThickness, ButtonBorderStyle.Solid,
                shadow, borderThickness, ButtonBorderStyle.Solid,
                shadow, borderThickness, ButtonBorderStyle.Solid);
        }

        private void UpdateModeButtonStyles()
        {
            ApplyModeButtonStyle(btn_Operate, "Operate");
            ApplyModeButtonStyle(btn_Maint, "Maint");
            ApplyModeButtonStyle(btn_Recipe, "Recipe");
            ApplyModeButtonStyle(btn_Log, "Log");
            ApplyModeButtonStyle(btn_Setting, "Setting");
        }

        private void ApplyModeButtonStyle(Button button, string modeName)
        {
            bool isActive = currentUbarState == modeName;

            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = isActive ? 2 : 1;
            button.FlatAppearance.BorderColor = isActive ? Color.White : Color.Black;
            button.BackColor = isActive ? Color.SkyBlue : Color.FromArgb(60, 60, 60);
            button.ForeColor = isActive ? Color.White : Color.DimGray;
            button.Invalidate();
        }
    }
}
