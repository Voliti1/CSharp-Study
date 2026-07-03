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
    public partial class LoginState : UserControl
    {
        public event EventHandler LogoutRequested;

        public LoginState()
        {
            InitializeComponent();
        }

        internal LoginState(AccountInfo account)
            : this()
        {
            SetAccount(account);
        }

        internal void SetAccount(AccountInfo account)
        {
            if (account == null)
            {
                lbl_currentID.Text = string.Empty;
                lbl_currentLevel.Text = string.Empty;
                return;
            }

            lbl_currentID.Text = account.UserId;
            lbl_currentLevel.Text = AccountService.NormalizeUserLevel(account.UserLevel);
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            LogoutRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
