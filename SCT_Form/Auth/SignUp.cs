using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class SignUp : UserControl
    {
        public event EventHandler AccountCreated;
        public event EventHandler SignInRequested;
        public event EventHandler IDSearchRequested;
        public event EventHandler PWSearchRequested;

        public SignUp()
        {
            InitializeComponent();
            InitializeSignUpScreen();
        }

        private void InitializeSignUpScreen()
        {
            cbox_UserLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cbox_UserLevel.Items.Clear();
            cbox_UserLevel.Items.Add(AccountService.AdminLevel);
            cbox_UserLevel.Items.Add(AccountService.GeneralLevel);
            cbox_UserLevel.SelectedIndex = 1;

            btn_SignUp.Click += btn_SignUp_Click;
            btn_SignIn.Click += btn_SignIn_Click;
            btn_IDSearch.Click += btn_IDSearch_Click;
            btn_PWSearch.Click += btn_PWSearch_Click;

            txtBox_ID.TabIndex = 0;
            txtBox_PW.TabIndex = 1;
            txtBox_PWCheck.TabIndex = 2;
            txtBox_name.TabIndex = 3;
            cbox_UserLevel.TabIndex = 4;
            btn_SignUp.TabIndex = 5;
            btn_SignIn.TabIndex = 6;
            btn_IDSearch.TabIndex = 7;
            btn_PWSearch.TabIndex = 8;
        }

        private void btn_SignUp_Click(object sender, EventArgs e)
        {
            string message;
            if (!AccountService.TryCreateAccount(
                txtBox_ID.Text,
                txtBox_PW.Text,
                txtBox_PWCheck.Text,
                Convert.ToString(cbox_UserLevel.SelectedItem),
                txtBox_name.Text,
                out message))
            {
                MessageBox.Show(message, "Sign Up", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(message, "Sign Up", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AccountCreated?.Invoke(this, EventArgs.Empty);
        }

        private void btn_SignIn_Click(object sender, EventArgs e)
        {
            SignInRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btn_IDSearch_Click(object sender, EventArgs e)
        {
            IDSearchRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btn_PWSearch_Click(object sender, EventArgs e)
        {
            PWSearchRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
