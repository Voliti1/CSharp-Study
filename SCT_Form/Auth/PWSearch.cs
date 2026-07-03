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
    public partial class PWSearch : UserControl
    {
        public event EventHandler SignInRequested;
        public event EventHandler SignUpRequested;
        public event EventHandler IDSearchRequested;

        public PWSearch()
        {
            InitializeComponent();
            InitializePWSearchScreen();
        }

        private void InitializePWSearchScreen()
        {
            cbox_UserLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cbox_UserLevel.Items.Clear();
            cbox_UserLevel.Items.Add(AccountService.AdminLevel);
            cbox_UserLevel.Items.Add(AccountService.GeneralLevel);
            cbox_UserLevel.SelectedIndex = 1;
            txtBox_PWCheck.UseSystemPasswordChar = false;

            btn_SignIn.Click += btn_SignIn_Click;
            btn_SignUp.Click += btn_SignUp_Click;
            btn_IDSearch.Click += btn_IDSearch_Click;

            txtBox_ID.TabIndex = 0;
            cbox_UserLevel.TabIndex = 1;
            txtBox_PWCheck.TabIndex = 2;
            btn_Search.TabIndex = 3;
            btn_SignIn.TabIndex = 4;
            btn_SignUp.TabIndex = 5;
            btn_IDSearch.TabIndex = 6;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string password;
            string message;
            if (!AccountService.TryFindPassword(
                txtBox_PWCheck.Text,
                txtBox_ID.Text,
                Convert.ToString(cbox_UserLevel.SelectedItem),
                out password,
                out message))
            {
                MessageBox.Show(message, "PW Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("PW: " + password, "PW Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_SignIn_Click(object sender, EventArgs e)
        {
            SignInRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btn_SignUp_Click(object sender, EventArgs e)
        {
            SignUpRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btn_IDSearch_Click(object sender, EventArgs e)
        {
            IDSearchRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
