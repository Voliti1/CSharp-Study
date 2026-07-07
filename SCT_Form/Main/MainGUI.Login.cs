using System;
using System.Drawing;
using System.Windows.Forms;

namespace SCT_Form
{
    // 로그인/로그아웃 UI 전환과, 장비 조작/관리자 설정 화면 진입 전에 로그인 상태를
    // 확인하는 게이트(EnsureEquipmentOperationAllowed 등)를 모아둔 부분 클래스.
    public partial class MainGUI
    {
        private void InitializeLoginEntryPoints()
        {
            tBox_ID.ReadOnly = true;
            tBox_PW.ReadOnly = true;
            tBox_ID.BackColor = Color.White;
            tBox_PW.BackColor = Color.White;
            tBox_PW.UseSystemPasswordChar = true;
            tBox_ID.Cursor = Cursors.Hand;
            tBox_PW.Cursor = Cursors.Hand;
            tBox_ID.Click += LoginTextBox_Click;
            tBox_PW.Click += LoginTextBox_Click;
            UpdateLoginDisplay();
        }

        private void LoginTextBox_Click(object sender, EventArgs e)
        {
            ShowLoginDialog();
        }

        private void ShowLoginDialog()
        {
            using (LogInGUI loginGUI = new LogInGUI())
            {
                if (loginGUI.ShowDialog(this) != DialogResult.OK) return;

                currentAccount = loginGUI.LoggedInAccount;
                ShowLoginStatePanel();
                WriteSystemLog("User", "INFO", "로그인: " + currentAccount.UserId);
            }
        }

        private void UpdateLoginDisplay()
        {
            if (currentAccount == null)
            {
                tBox_ID.Text = string.Empty;
                tBox_PW.Text = string.Empty;
                return;
            }

            tBox_ID.Text = currentAccount.UserId;
            tBox_PW.Text = "********";
        }

        private void ShowLoginInputPanel()
        {
            pnl_LoginChange.Controls.Clear();
            pnl_LogIn.Controls.Clear();
            pnl_LogIn.Controls.Add(lbl_ID, 0, 0);
            pnl_LogIn.Controls.Add(lbl_PW, 1, 0);
            pnl_LogIn.Controls.Add(tBox_ID, 0, 1);
            pnl_LogIn.Controls.Add(tBox_PW, 1, 1);
            pnl_LoginChange.Controls.Add(pnl_LogIn);
            UpdateLoginDisplay();
        }

        private void ShowLoginStatePanel()
        {
            pnl_LoginChange.Controls.Clear();
            loginStateGUI = new LoginState(currentAccount);
            loginStateGUI.Dock = DockStyle.Fill;
            loginStateGUI.LogoutRequested += LoginStateGUI_LogoutRequested;
            pnl_LoginChange.Controls.Add(loginStateGUI);
        }

        private void LoginStateGUI_LogoutRequested(object sender, EventArgs e)
        {
            string logoutUserId = currentAccount == null ? string.Empty : currentAccount.UserId;
            currentAccount = null;
            loginStateGUI = null;
            ShowLoginInputPanel();
            WriteSystemLog("User", "INFO", "로그아웃: " + logoutUserId);
        }

        internal bool IsLoggedIn
        {
            get { return currentAccount != null; }
        }

        internal bool IsAdminLoggedIn
        {
            get { return AccountService.IsAdmin(currentAccount); }
        }

        // 장비를 실제로 움직이는 모든 진입점(수동 제어 버튼, 자동 시퀀스 Start 등)이
        // 맨 처음에 호출해야 하는 공용 게이트: 로그인 여부 → 축 위치 확인 오류(Fault) 여부 →
        // Door Open Interlock 순서로 확인한다.
        internal bool EnsureEquipmentOperationAllowed()
        {
            if (!IsLoggedIn)
            {
                MessageBox.Show("장비 동작을 하려면 로그인해주세요", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (isAxisPositionFault)
            {
                MessageBox.Show("UD 축 위치 확인 실패로 장비 동작이 차단되었습니다. Alarm Reset 후 다시 시도하세요.", "Axis Position Fault", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return EnsureDoorInterlockAllowed();
        }

        private bool EnsureDoorInterlockAllowed()
        {
            if (settings == null || !settings.DoorOpenInterlock) return true;

            bool isAnyDoorOpen;
            try
            {
                isAnyDoorOpen = IsChamberDoorOpen("PM A") || IsChamberDoorOpen("PM B") || IsChamberDoorOpen("PM C");
            }
            catch (Exception ex)
            {
                WriteSystemLog("Alarm", "ERROR", "Door Open Interlock: door sensor read failed - " + ex.Message);
                MessageBox.Show("Chamber Door 센서 상태를 확인할 수 없어 장비 동작이 차단되었습니다.", "Safety Interlock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!isAnyDoorOpen) return true;

            MessageBox.Show("Door Open Interlock 상태입니다. Chamber Door를 닫은 후 동작해주세요.", "Safety Interlock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            WriteSystemLog("Alarm", "WARN", "Door Open Interlock: 장비 동작 차단");
            return false;
        }

        internal bool EnsureAdminSettingAllowed()
        {
            if (IsAdminLoggedIn) return true;

            MessageBox.Show("설정을 하려면 관리자 계정으로 로그인해주세요", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
    }
}
