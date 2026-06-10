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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EtherCAT_M.CIFX_50RE_Connect() == true)
            {
                label2.Text = "Connect OK";
                EtherCAT_M.ReadData_Send_Start(300); //Timer Interval Set
                EtherCAT_M.ReadData_Timer_Start(); //Timer Start
            }
            else
            {
                label2.Text = "NG";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EtherCAT_M.CIFX_50RE_Disconnect();
            label2.Text = "Disconnect";
        }

        private void RedLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, true);
        }

        private void RedLightOFF_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, false);
        }

        private void YellowLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(1, true);
        }

        private void YellowLightOFF_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(1, false);
        }

        private void GreenLightOn_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(2, true);
        }

        private void GreenLightOFF_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(2, false);
        }
    }
}
