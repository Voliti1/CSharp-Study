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
    public partial class LogInGUI : Form
    {
        private Control loginPanel;

        internal AccountInfo LoggedInAccount { get; private set; }

        public LogInGUI()
        {
            InitializeComponent();
            InitializeLoginScreen();
        }

        private void InitializeLoginScreen()
        {
            loginPanel = pnl_Login;
            btn_SignIn.Click += btn_SignIn_Click;
            AcceptButton = btn_SignIn;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sign In";
        }

        private void btn_SignIn_Click(object sender, EventArgs e)
        {
            AccountInfo account;
            string message;
            if (!AccountService.TryLogin(txtBox_ID.Text, txtBox_PW.Text, out account, out message))
            {
                MessageBox.Show(message, "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoggedInAccount = account;
            ShowLoginStatePanel(account);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btn_SignUp_Click(object sender, EventArgs e)
        {
            ShowSignUpPanel();
        }

        private void btn_IDSearch_Click(object sender, EventArgs e)
        {
            ShowIDSearchPanel();
        }

        private void btn_PWSearch_Click(object sender, EventArgs e)
        {
            ShowPWSearchPanel();
        }

        private void signUp_AccountCreated(object sender, EventArgs e)
        {
            ShowLoginPanel();
        }

        private void ShowLoginPanel()
        {
            pnl_LoginChange.Controls.Clear();
            pnl_LoginChange.Controls.Add(loginPanel);
            loginPanel.Dock = DockStyle.None;
            Text = "Sign In";
        }

        private void ShowSignUpPanel()
        {
            SignUp signUp = new SignUp();
            signUp.Dock = DockStyle.Fill;
            signUp.AccountCreated += signUp_AccountCreated;
            signUp.SignInRequested += delegate { ShowLoginPanel(); };
            signUp.IDSearchRequested += delegate { ShowIDSearchPanel(); };
            signUp.PWSearchRequested += delegate { ShowPWSearchPanel(); };

            pnl_LoginChange.Controls.Clear();
            pnl_LoginChange.Controls.Add(signUp);
            Text = "Sign Up";
        }

        private void ShowIDSearchPanel()
        {
            IDSearch idSearch = new IDSearch();
            idSearch.Dock = DockStyle.Fill;
            idSearch.SignInRequested += delegate { ShowLoginPanel(); };
            idSearch.SignUpRequested += delegate { ShowSignUpPanel(); };
            idSearch.PWSearchRequested += delegate { ShowPWSearchPanel(); };

            pnl_LoginChange.Controls.Clear();
            pnl_LoginChange.Controls.Add(idSearch);
            Text = "ID Search";
        }

        private void ShowPWSearchPanel()
        {
            PWSearch pwSearch = new PWSearch();
            pwSearch.Dock = DockStyle.Fill;
            pwSearch.SignInRequested += delegate { ShowLoginPanel(); };
            pwSearch.SignUpRequested += delegate { ShowSignUpPanel(); };
            pwSearch.IDSearchRequested += delegate { ShowIDSearchPanel(); };

            pnl_LoginChange.Controls.Clear();
            pnl_LoginChange.Controls.Add(pwSearch);
            Text = "PW Search";
        }

        private void ShowLoginStatePanel(AccountInfo account)
        {
            LoginState loginState = new LoginState(account);
            loginState.Dock = DockStyle.Fill;

            pnl_LoginChange.Controls.Clear();
            pnl_LoginChange.Controls.Add(loginState);
        }
    }
}
