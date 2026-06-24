using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20260624_WinForm
{
    public partial class Form1 : Form
    {
        class CustomForm2 : Form
        {
            public CustomForm2()
            {
                Text = "모달리스창";
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            DialogResult result;
            do
            {
                result = MessageBox.Show("내용", "제목", MessageBoxButtons.RetryCancel);
            } while (result == DialogResult.Retry);
        }

        private void btnCustomForm_Click(object sender, EventArgs e)
        {
            CustomForm1 form1 = new CustomForm1();
            form1.ShowDialog();
        }

        private void btnCustomForm2_Click(object sender, EventArgs e)
        {
            CustomForm2 form2 = new CustomForm2();
            form2.Show();
        }
    }
}
