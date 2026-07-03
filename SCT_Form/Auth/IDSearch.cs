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
    public partial class IDSearch : UserControl
    {
        public event EventHandler SignInRequested;
        public event EventHandler SignUpRequested;
        public event EventHandler PWSearchRequested;

        public IDSearch()
        {
            InitializeComponent();
            InitializeIDSearchScreen();
        }

        private void InitializeIDSearchScreen()
        {
            cbox_UserLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cbox_UserLevel.Items.Clear();
            cbox_UserLevel.Items.Add(AccountService.AdminLevel);
            cbox_UserLevel.Items.Add(AccountService.GeneralLevel);
            cbox_UserLevel.SelectedIndex = 1;

            btn_SignIn.Click += btn_SignIn_Click;
            btn_SignUp.Click += btn_SignUp_Click;
            btn_PWSearch.Click += btn_PWSearch_Click;

            txtBox_Username.TabIndex = 0;
            cbox_UserLevel.TabIndex = 1;
            btn_Search.TabIndex = 2;
            btn_SignIn.TabIndex = 3;
            btn_SignUp.TabIndex = 4;
            btn_PWSearch.TabIndex = 5;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            List<string> userIds;
            string message;
            if (!AccountService.TryFindIds(
                txtBox_Username.Text,
                Convert.ToString(cbox_UserLevel.SelectedItem),
                out userIds,
                out message))
            {
                MessageBox.Show(message, "ID Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("ID: " + string.Join(", ", userIds), "ID Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_SignIn_Click(object sender, EventArgs e)
        {
            SignInRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btn_SignUp_Click(object sender, EventArgs e)
        {
            SignUpRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btn_PWSearch_Click(object sender, EventArgs e)
        {
            PWSearchRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
