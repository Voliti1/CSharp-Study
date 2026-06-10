using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IEG3268_DLL;

namespace SCT_Form
{
    public partial class Form1 : Form
    {
        IEG3268 EtherCAT_M = new IEG3268();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(EtherCat_M.CIFX_50RE_Connect() == true)
            {
                label2.Text = "OK";
                EtherCat_M.ReadData_Send_Start(300); //Timer Interval Set
                EtherCat_M.ReadData_Timer_Start(); //Timer Start
            }
            else
            {
                label2.Text = "NG";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Digital_Output(0, false);
        }
    }
}
