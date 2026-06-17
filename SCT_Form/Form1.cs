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

        public Form1()
        {
            InitializeComponent();
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

                panel_Cham_A_Door.BackColor = color2;
                panel_Cham_A_Lamp.BackColor = color2;
                panel_Cham_B_Door.BackColor = color2;
                panel_Cham_B_Lamp.BackColor = color2;
                panel_Cham_C_Door.BackColor = color2;
                panel_Cham_C_Lamp.BackColor = color2;

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

            // 연결 끊으면 상태를 모르므로 전체 색깔 변경
            Color grayOffline = SystemColors.ControlDark;
            pnl_ChamA.BackColor = grayOffline;
            pnl_ChamB.BackColor = grayOffline;
            pnl_ChamC.BackColor = grayOffline;

            panel_Cham_A_Door.BackColor = grayOffline;
            panel_Cham_A_Lamp.BackColor = grayOffline;
            panel_Cham_B_Door.BackColor = grayOffline;
            panel_Cham_B_Lamp.BackColor = grayOffline;
            panel_Cham_C_Door.BackColor = grayOffline;
            panel_Cham_C_Lamp.BackColor = grayOffline;
        }

        private void RedLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, true);
        }

        private void RedLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, false);
        }

        private void YellowLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(1, true);
        }

        private void YellowLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(1, false);
        }

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

        private void btn_Cham_A_Door_OPEN_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(5, true);
            EtherCAT_M.Digital_Output(4, false);

            panel_Cham_A_Door.BackColor = Color.Red;

            isChamADoorOpen = true;
        }

        private void btn_Cham_A_Door_CLOSE_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(5, false);
            EtherCAT_M.Digital_Output(4, true);

            panel_Cham_A_Door.BackColor = Color.LightGray;

            isChamADoorOpen = false;
        }

        private void btn_Cham_A_Lamp_ON_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(3, true);
            EtherCAT_M.Digital_Output(2, true);
            panel_Cham_A_Lamp.BackColor = Color.Lime;

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
            EtherCAT_M.Digital_Output(3, false);
            panel_Cham_A_Lamp.BackColor = Color.LightGray;

            isChamALampOn = false;

            if(isChamBLampOn == false && isChamCLampOn == false)
            {
                EtherCAT_M.Digital_Output(2, false);
                EtherCAT_M.Digital_Output(1, true);
                isGreenLightOn = false;
            }
        }

        private void btn_Cham_B_Door_OPEN_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(8, true);
            EtherCAT_M.Digital_Output(7, false);

            panel_Cham_B_Door.BackColor = Color.Red;

            isChamBDoorOpen = true;
        }

        private void btn_Cham_B_Door_CLOSE_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(8, false);
            EtherCAT_M.Digital_Output(7, true);

            panel_Cham_B_Door.BackColor = Color.LightGray;

            isChamBDoorOpen = false;
        }

        private void btn_Cham_B_Lamp_ON_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(6, true);
            panel_Cham_B_Lamp.BackColor = Color.Lime;

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
            EtherCAT_M.Digital_Output(6, false);
            panel_Cham_B_Lamp.BackColor = Color.LightGray;

            isChamBLampOn = false;

            if (isChamALampOn == false && isChamCLampOn == false)
            {
                EtherCAT_M.Digital_Output(1, true);
                EtherCAT_M.Digital_Output(2, false);
                isGreenLightOn = false;
            }
        }

        private void btn_Cham_C_Door_OPEN_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(11, true);
            EtherCAT_M.Digital_Output(10, false);
            panel_Cham_C_Door.BackColor = Color.Red;

            isChamCDoorOpen = true;
        }

        private void btn_Cham_C_Door_CLOSE_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(11, false);
            EtherCAT_M.Digital_Output(10, true);
            panel_Cham_C_Door.BackColor = Color.LightGray;

            isChamCDoorOpen = false;
        }

        private void btn_Cham_C_Lamp_ON_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(9, true);
            panel_Cham_C_Lamp.BackColor = Color.Lime;

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
            EtherCAT_M.Digital_Output(9, false);
            panel_Cham_C_Lamp.BackColor = Color.LightGray;

            isChamCLampOn = false;

            if (isChamALampOn == false && isChamBLampOn == false)
            {
                EtherCAT_M.Digital_Output(1, true);
                EtherCAT_M.Digital_Output(2, false);
                isGreenLightOn = false;
            }
        }
    }
}
