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
                panel_Connection.BackColor = Color.DodgerBlue;

                isChamALampOn = false; isChamBLampOn = false; isChamCLampOn = false;
                isChamADoorOpen = false; isChamBDoorOpen = false; isChamCDoorOpen = false;

                EtherCAT_M.Digital_Output(5, false);
                EtherCAT_M.Digital_Output(4, true);

                EtherCAT_M.Digital_Output(8, false);
                EtherCAT_M.Digital_Output(7, true);

                EtherCAT_M.Digital_Output(11, false);
                EtherCAT_M.Digital_Output(10, true);

                Color idleColor = Color.LightCyan;

                pnl_ChamA.BackColor = idleColor;
                pnl_ChamB.BackColor = idleColor;
                pnl_ChamC.BackColor = idleColor;

                Color color2 = Color.LightGray;

                panel_Cham_A_Door.BackColor = color2;
                panel_Cham_A_Lamp.BackColor = color2;
                panel_Cham_B_Door.BackColor = color2;
                panel_Cham_B_Lamp.BackColor = color2;
                panel_Cham_C_Door.BackColor = color2;
                panel_Cham_C_Lamp.BackColor = color2;
            }
            else
            {
                label2.Text = "NG";
                panel_Connection.BackColor = Color.Yellow;
            }
        }

        private void DisConnect_Click(object sender, EventArgs e)
        {
            EtherCAT_M.CIFX_50RE_Disconnect();
            label2.Text = "Disconnect";
            panel_Connection.BackColor = Color.Red;

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
        }

        private void GreenLightOff_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(2, false);
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
            panel_Cham_A_Lamp.BackColor = Color.Lime;

            isChamALampOn = true;
        }

        private void btn_Cham_A_Lamp_OFF_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(3, false);
            panel_Cham_A_Lamp.BackColor = Color.LightGray;

            isChamALampOn = false;
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
        }

        private void btn_Cham_B_Lamp_OFF_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(6, false);
            panel_Cham_B_Lamp.BackColor = Color.LightGray;

            isChamBLampOn = false;
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
        }

        private void btn_Cham_C_Lamp_OFF_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(9, false);
            panel_Cham_C_Lamp.BackColor = Color.LightGray;

            isChamCLampOn = false;
        }
    }
}
