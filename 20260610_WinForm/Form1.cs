using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20260610_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
            FormClosed += Form1_FormClosed;

            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "+";
            label1.Text += "+"; 
        }

        private void Button1_Click(object sender, EventArgs e)
        {            
            Button self = (Button)sender;
            self.Text = "클릭";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.IO.File.AppendAllText("log.txt",
                $"[{DateTime.Now}] Form1이 닫혔습니다.{Environment.NewLine}");

            timer1.Stop();
            MessageBox.Show("종료되었습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int elapsedTime = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            textBox2.Text = elapsedTime + "초 경과";
            label2.Text = elapsedTime + "초 경과";
        }
    }
}
