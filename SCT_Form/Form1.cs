using IEG3268_Dll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class Form1 : Form
    {
        IEG3268 EtherCAT_M = new IEG3268();

        private bool isGreenLightOn = false;
        private bool isChamALampOn = false;
        private bool isChamBLampOn = false;
        private bool isChamCLampOn = false;
        private bool isChamADoorOpen = false;
        private bool isChamBDoorOpen = false;
        private bool isChamCDoorOpen = false;
        private string currentState = "AUTO";

        public Form1()
        {
            InitializeComponent();
            grpbox_Cham_A_Manual.Enabled = false;
            grpbox_Cham_B_Manual.Enabled = false;
            grpbox_Cham_C_Manual.Enabled = false;
            grpbox_Tower.Enabled = false;
            btn_Auto.Enabled = false;
            btn_Manual.Enabled = false;

            btn_Auto.FlatStyle = FlatStyle.Flat;
            btn_Auto.FlatAppearance.BorderSize = 0;
            btn_Manual.FlatStyle = FlatStyle.Flat;
            btn_Manual.FlatAppearance.BorderSize = 0;
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            if (EtherCAT_M.CIFX_50RE_Connect() == true)
            {
                label2.Text = "Connect OK";
                EtherCAT_M.ReadData_Send_Start(300);
                EtherCAT_M.ReadData_Timer_Start();

                // 연결 시 Connect OK 라벨 테두리에 색깔 변경
                panel_Connection.BackColor = Color.DodgerBlue;
                
                // 플래그 초기화
                isChamALampOn = false; isChamBLampOn = false; isChamCLampOn = false;
                isChamADoorOpen = false; isChamBDoorOpen = false; isChamCDoorOpen = false;

                // 상단 버튼 활성화
                btn_Auto.Enabled = true;
                btn_Manual.Enabled = true;

                // 모든 챔버 문 닫힌 상태로 만들기
                EtherCAT_M.Digital_Output(5, false);
                EtherCAT_M.Digital_Output(4, true);

                EtherCAT_M.Digital_Output(8, false);
                EtherCAT_M.Digital_Output(7, true);

                EtherCAT_M.Digital_Output(11, false);
                EtherCAT_M.Digital_Output(10, true);

                // 챔버 GUI 부분 초기 상태 표시
                Color idleColor = Color.LightCyan;

                pnl_ChamA.BackColor = idleColor;
                pnl_ChamB.BackColor = idleColor;
                pnl_ChamC.BackColor = idleColor;

                // 챔버 Manual 부분 초기상태 표시
                Color color2 = Color.LightGray;

                pnl_Cham_A_Door.BackColor = color2;
                pnl_Cham_A_Lamp.BackColor = color2;
                pnl_Cham_B_Door.BackColor = color2;
                pnl_Cham_B_Lamp.BackColor = color2;
                pnl_Cham_C_Door.BackColor = color2;
                pnl_Cham_C_Lamp.BackColor = color2;

                //황색등 점등
                EtherCAT_M.Digital_Output(1, true);
            }
            else
            {
                label2.Text = "NG";
                panel_Connection.BackColor = Color.Yellow;
            }
        }

        private void DisConnect_Click(object sender, EventArgs e)
        {
            // 황색등 점멸
            EtherCAT_M.Digital_Output(1, false);

            EtherCAT_M.CIFX_50RE_Disconnect();
            label2.Text = "Disconnect";
            panel_Connection.BackColor = Color.Red;

            // 플래그 초기화
            isChamALampOn = false; isChamBLampOn = false; isChamCLampOn = false;
            isChamADoorOpen = false; isChamBDoorOpen = false; isChamCDoorOpen = false;
            isGreenLightOn = false;

            //상단 버튼 비활성화
            btn_Auto.Enabled = false;
            btn_Manual.Enabled = false;

            // 연결이 끊겼을 때도 버튼 색상을 비활성 상태로 강제 변경
            btn_Auto.BackColor = Color.FromArgb(60, 60, 60);
            btn_Manual.BackColor = Color.FromArgb(60, 60, 60);
            btn_Auto.Invalidate();
            btn_Manual.Invalidate();

            // 연결 끊으면 상태를 모르므로 전체 색깔 변경
            Color grayOffline = SystemColors.ControlDark;
            pnl_ChamA.BackColor = grayOffline;
            pnl_ChamB.BackColor = grayOffline;
            pnl_ChamC.BackColor = grayOffline;

            pnl_Cham_A_Door.BackColor = grayOffline;
            pnl_Cham_A_Lamp.BackColor = grayOffline;
            pnl_Cham_B_Door.BackColor = grayOffline;
            pnl_Cham_B_Lamp.BackColor = grayOffline;
            pnl_Cham_C_Door.BackColor = grayOffline;
            pnl_Cham_C_Lamp.BackColor = grayOffline;
        }

        // 적색등 조작
        private void RedLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, true);
        }

        private void RedLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, false);
        }

        // 황색등 조작
        private void YellowLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(1, true);
        }

        private void YellowLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(1, false);
        }

        // 초록등 조작
        private void GreenLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(2, true);
            isGreenLightOn = true;
        }

        private void GreenLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(2, false);
            isGreenLightOn = false;
        }

        // 전체 등 조작
        private void AllLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, true);
            EtherCAT_M.Digital_Output(1, true);
            EtherCAT_M.Digital_Output(2, true);
        }

        private void AllLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, false);
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(2, false);
        }

        // 챔버 A 문 조작
        private void btn_Cham_A_Door_OPEN_Click(object sender, EventArgs e)
        {
            if (isChamALampOn)
            {
                MessageBox.Show("Chamber A가 가동 중(Lamp ON)이므로 도어를 열 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(5, true);
            EtherCAT_M.Digital_Output(4, false);

            pnl_Cham_A_Door.BackColor = Color.Red;
            pnl_ChamA.BackColor = Color.Orange;

            isChamADoorOpen = true;
        }

        private void btn_Cham_A_Door_CLOSE_Click(object sender, EventArgs e)
        {
            if (!isChamADoorOpen) return;

            EtherCAT_M.Digital_Output(5, false);
            EtherCAT_M.Digital_Output(4, true);

            pnl_Cham_A_Door.BackColor = Color.LightGray;
            pnl_ChamA.BackColor = Color.LightCyan;


            isChamADoorOpen = false;
        }

        // 챔버 A 램프 조작
        private void btn_Cham_A_Lamp_ON_Click(object sender, EventArgs e)
        {
            if (isChamADoorOpen)
            {
                MessageBox.Show("Chamber A의 도어가 열려 있어 램프를 켤 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(3, true);
            pnl_Cham_A_Lamp.BackColor = Color.LimeGreen;
            pnl_ChamA.BackColor = Color.LimeGreen;

            isChamALampOn = true;

            if (isGreenLightOn == false) 
            {
                EtherCAT_M.Digital_Output(1, false);
                EtherCAT_M.Digital_Output(2, true);
                isGreenLightOn = true;
            }
                
        }

        private void btn_Cham_A_Lamp_OFF_Click(object sender, EventArgs e)
        {
            if (!isChamALampOn) return;

            EtherCAT_M.Digital_Output(3, false);
            pnl_Cham_A_Lamp.BackColor = Color.LightGray;

            isChamALampOn = false;

            if (isChamADoorOpen)
            {
                pnl_ChamA.BackColor = Color.Orange;
            }
            else
            {
                pnl_ChamA.BackColor = Color.LightCyan;
            }

            if(isChamBLampOn == false && isChamCLampOn == false)
            {
                EtherCAT_M.Digital_Output(2, false);
                EtherCAT_M.Digital_Output(1, true);
                isGreenLightOn = false;
            }
        }

        // 챔버 B 문 조작
        private void btn_Cham_B_Door_OPEN_Click(object sender, EventArgs e)
        {
            if (isChamBLampOn)
            {
                MessageBox.Show("Chamber B가 가동 중(Lamp ON)이므로 도어를 열 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(8, true);
            EtherCAT_M.Digital_Output(7, false);

            pnl_Cham_B_Door.BackColor = Color.Red;
            pnl_ChamB.BackColor = Color.Orange;

            isChamBDoorOpen = true;
        }

        private void btn_Cham_B_Door_CLOSE_Click(object sender, EventArgs e)
        {
            if (!isChamBDoorOpen) return;

            EtherCAT_M.Digital_Output(8, false);
            EtherCAT_M.Digital_Output(7, true);

            pnl_Cham_B_Door.BackColor = Color.LightGray;
            pnl_ChamB.BackColor = Color.LightCyan;

            isChamBDoorOpen = false;
        }

        // 챔버 B 램프 조작
        private void btn_Cham_B_Lamp_ON_Click(object sender, EventArgs e)
        {
            if (isChamBDoorOpen)
            {
                MessageBox.Show("Chamber B의 도어가 열려 있어 램프를 켤 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(6, true);
            pnl_Cham_B_Lamp.BackColor = Color.LimeGreen;
            pnl_ChamB.BackColor = Color.LimeGreen;

            isChamBLampOn = true;

            if (isGreenLightOn == false)
            {
                EtherCAT_M.Digital_Output(1, false);
                EtherCAT_M.Digital_Output(2, true);
                isGreenLightOn = true;
            }
        }

        private void btn_Cham_B_Lamp_OFF_Click(object sender, EventArgs e)
        {
            if (!isChamBLampOn) return;

            EtherCAT_M.Digital_Output(6, false);
            pnl_Cham_B_Lamp.BackColor = Color.LightGray;

            isChamBLampOn = false;

            if (isChamBDoorOpen)
            {
                pnl_ChamB.BackColor = Color.Orange;
            }
            else
            {
                pnl_ChamB.BackColor = Color.LightCyan;
            }

            if (isChamALampOn == false && isChamCLampOn == false)
            {
                EtherCAT_M.Digital_Output(1, true);
                EtherCAT_M.Digital_Output(2, false);
                isGreenLightOn = false;
            }
        }

        // 챔버 C 문 조작
        private void btn_Cham_C_Door_OPEN_Click(object sender, EventArgs e)
        {
            if (isChamCLampOn)
            {
                MessageBox.Show("Chamber C가 가동 중(Lamp ON)이므로 도어를 열 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(11, true);
            EtherCAT_M.Digital_Output(10, false);
            pnl_Cham_C_Door.BackColor = Color.Red;
            pnl_ChamC.BackColor = Color.Orange;

            isChamCDoorOpen = true;
        }

        private void btn_Cham_C_Door_CLOSE_Click(object sender, EventArgs e)
        {
            if (!isChamCDoorOpen) return;

            EtherCAT_M.Digital_Output(11, false);
            EtherCAT_M.Digital_Output(10, true);
            pnl_Cham_C_Door.BackColor = Color.LightGray;
            pnl_ChamC.BackColor = Color.LightCyan;

            isChamCDoorOpen = false;
        }


        // 챔버 C 램프 조작
        private void btn_Cham_C_Lamp_ON_Click(object sender, EventArgs e)
        {
            if (isChamCDoorOpen)
            {
                MessageBox.Show("Chamber C의 도어가 열려 있어 램프를 켤 수 없습니다.", "인터록 경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EtherCAT_M.Digital_Output(9, true);
            pnl_Cham_C_Lamp.BackColor = Color.LimeGreen;
            pnl_ChamC.BackColor = Color.LimeGreen;

            isChamCLampOn = true;

            if (isGreenLightOn == false)
            {
                EtherCAT_M.Digital_Output(1, false);
                EtherCAT_M.Digital_Output(2, true);
                isGreenLightOn = true;
            }
        }

        private void btn_Cham_C_Lamp_OFF_Click(object sender, EventArgs e)
        {
            if (!isChamCLampOn) return;

            EtherCAT_M.Digital_Output(9, false);
            pnl_Cham_C_Lamp.BackColor = Color.LightGray;

            isChamCLampOn = false;

            // C 챔버 문 상태에 따라 GUI 챔버 배경색 변경
            if (isChamCDoorOpen)
            {
                pnl_ChamC.BackColor = Color.Orange;
            }
            else
            {
                pnl_ChamC.BackColor = Color.LightCyan;
            }

            // A, B 챔버 등이 꺼져있으면 삼색등을 황색으로 변경
            if (isChamALampOn == false && isChamBLampOn == false)
            {
                EtherCAT_M.Digital_Output(1, true);
                EtherCAT_M.Digital_Output(2, false);
                isGreenLightOn = false;
            }
        }

        // 챔버 문이 열리거나 램프가 켜진 상태로 GUI를 꺼버릴 경우 전체 소등 및 문 닫기
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 삼색등 끄기
            EtherCAT_M.Digital_Output(0, false);
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(2, false);

            // 챔버 등 끄기
            EtherCAT_M.Digital_Output(3, false);
            EtherCAT_M.Digital_Output(6, false);
            EtherCAT_M.Digital_Output(9, false);

            // 챔버 문  닫기
            EtherCAT_M.Digital_Output(5, false);
            EtherCAT_M.Digital_Output(4, true);
            EtherCAT_M.Digital_Output(8, false);
            EtherCAT_M.Digital_Output(7, true);
            EtherCAT_M.Digital_Output(11, false);
            EtherCAT_M.Digital_Output(10, true);

            // 연결 해제
            EtherCAT_M.CIFX_50RE_Disconnect();
        }

        // 상단 버튼 모드에 따라 스타일 변경
        private void btnMode_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;

            // 눌린 상태인지 확인 (태그를 사용해 상태 관리)
            bool isActive = (currentState == "AUTO" && btn == btn_Auto) ||
                    (currentState == "MANUAL" && btn == btn_Manual);

                // 밝은 선과 어두운 선 색상 설정
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
            // MANUAL 모드일 때
            bool isManual = (currentState == "MANUAL");

            // AUTO 버튼 스타일
            btn_Auto.BackColor = !isManual ? Color.DarkBlue : Color.FromArgb(60, 60, 60);
            btn_Auto.ForeColor = !isManual ? Color.White : Color.DimGray;
            btn_Auto.Invalidate(); // Paint를 강제로 다시 부름

            // MANUAL 버튼 스타일
            btn_Manual.BackColor = isManual ? Color.DarkBlue : Color.FromArgb(60, 60, 60);
            btn_Manual.ForeColor = isManual ? Color.White : Color.DimGray;
            btn_Manual.Invalidate();
        }

        // 상단 버튼 조작
        private void btn_auto_Click(object sender, EventArgs e)
        {
            currentState = "AUTO";
            UpdateModeButtonStyles();

            ForceStopAllChambers();

            grpbox_Cham_A_Manual.Enabled = false;
            grpbox_Cham_B_Manual.Enabled = false;
            grpbox_Cham_C_Manual.Enabled = false;
            grpbox_Tower.Enabled = false;

            MessageBox.Show("AUTO 모드로 전환됨: 모든 수동 동작이 중단되었습니다.");
            EtherCAT_M.Digital_Output(1, true);
        }

        private void btn_manual_Click(object sender, EventArgs e)
        {
            currentState = "MANUAL";
            UpdateModeButtonStyles();

            ForceStopAllChambers();

            grpbox_Cham_A_Manual.Enabled = true;
            grpbox_Cham_B_Manual.Enabled = true;
            grpbox_Cham_C_Manual.Enabled = true;
            grpbox_Tower.Enabled = true;

            MessageBox.Show("MANUAL 모드로 전환됨: 모든 자동 동작이 중단되었습니다.");
            EtherCAT_M.Digital_Output(1, true);
        }

        private void ForceStopAllChambers()
        {
            //모든 램프 끄기
            EtherCAT_M.Digital_Output(0, false); // 적색
            EtherCAT_M.Digital_Output(1, false); // 황색
            EtherCAT_M.Digital_Output(2, false); // 초록색
            EtherCAT_M.Digital_Output(3, false); // Chamber A
            EtherCAT_M.Digital_Output(6, false); // Chamber B
            EtherCAT_M.Digital_Output(9, false); // Chamber C

            // 2. GUI 상태 플래그 초기화
            isChamALampOn = false;
            isChamBLampOn = false;
            isChamCLampOn = false;

            // 3. GUI 패널 색상 초기화 (LightGray)
            Color idleColor = Color.LightGray;
            pnl_Cham_A_Lamp.BackColor = idleColor;
            pnl_Cham_B_Lamp.BackColor = idleColor;
            pnl_Cham_C_Lamp.BackColor = idleColor;

            // 4. 추가로 챔버 A/B/C 배경색도 LightCyan으로 복귀
            pnl_ChamA.BackColor = Color.LightCyan;
            pnl_ChamB.BackColor = Color.LightCyan;
            pnl_ChamC.BackColor = Color.LightCyan;
        }
    }
}
