namespace SCT_Form
{
    partial class LoginState
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_LoginState = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ID = new System.Windows.Forms.Label();
            this.lbl_currentID = new System.Windows.Forms.Label();
            this.btn_Logout = new System.Windows.Forms.Button();
            this.lbl_Level = new System.Windows.Forms.Label();
            this.lbl_currentLevel = new System.Windows.Forms.Label();
            this.pnl_LoginState.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_LoginState
            // 
            this.pnl_LoginState.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.pnl_LoginState.ColumnCount = 3;
            this.pnl_LoginState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_LoginState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_LoginState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_LoginState.Controls.Add(this.lbl_currentLevel, 1, 1);
            this.pnl_LoginState.Controls.Add(this.lbl_Level, 1, 0);
            this.pnl_LoginState.Controls.Add(this.btn_Logout, 2, 0);
            this.pnl_LoginState.Controls.Add(this.lbl_currentID, 0, 1);
            this.pnl_LoginState.Controls.Add(this.lbl_ID, 0, 0);
            this.pnl_LoginState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_LoginState.Location = new System.Drawing.Point(0, 0);
            this.pnl_LoginState.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_LoginState.Name = "pnl_LoginState";
            this.pnl_LoginState.RowCount = 2;
            this.pnl_LoginState.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_LoginState.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_LoginState.Size = new System.Drawing.Size(250, 50);
            this.pnl_LoginState.TabIndex = 0;
            // 
            // lbl_ID
            // 
            this.lbl_ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ID.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_ID.Location = new System.Drawing.Point(1, 1);
            this.lbl_ID.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_ID.Name = "lbl_ID";
            this.lbl_ID.Size = new System.Drawing.Size(82, 23);
            this.lbl_ID.TabIndex = 0;
            this.lbl_ID.Text = "Login ID";
            this.lbl_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_currentID
            // 
            this.lbl_currentID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_currentID.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_currentID.Location = new System.Drawing.Point(1, 25);
            this.lbl_currentID.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_currentID.Name = "lbl_currentID";
            this.lbl_currentID.Size = new System.Drawing.Size(82, 24);
            this.lbl_currentID.TabIndex = 2;
            this.lbl_currentID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Logout
            // 
            this.btn_Logout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Logout.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Logout.Location = new System.Drawing.Point(167, 1);
            this.btn_Logout.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Logout.Name = "btn_Logout";
            this.pnl_LoginState.SetRowSpan(this.btn_Logout, 2);
            this.btn_Logout.Size = new System.Drawing.Size(82, 48);
            this.btn_Logout.TabIndex = 4;
            this.btn_Logout.Text = "Log Out";
            this.btn_Logout.UseVisualStyleBackColor = true;
            this.btn_Logout.Click += new System.EventHandler(this.btn_Logout_Click);
            // 
            // lbl_Level
            // 
            this.lbl_Level.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Level.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_Level.Location = new System.Drawing.Point(84, 1);
            this.lbl_Level.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Level.Name = "lbl_Level";
            this.lbl_Level.Size = new System.Drawing.Size(82, 23);
            this.lbl_Level.TabIndex = 5;
            this.lbl_Level.Text = "Level";
            this.lbl_Level.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_currentLevel
            // 
            this.lbl_currentLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_currentLevel.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_currentLevel.Location = new System.Drawing.Point(84, 25);
            this.lbl_currentLevel.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_currentLevel.Name = "lbl_currentLevel";
            this.lbl_currentLevel.Size = new System.Drawing.Size(82, 24);
            this.lbl_currentLevel.TabIndex = 6;
            this.lbl_currentLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_LoginState);
            this.Name = "LoginState";
            this.Size = new System.Drawing.Size(250, 50);
            this.pnl_LoginState.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnl_LoginState;
        private System.Windows.Forms.Label lbl_ID;
        private System.Windows.Forms.Label lbl_currentID;
        private System.Windows.Forms.Label lbl_Level;
        private System.Windows.Forms.Button btn_Logout;
        private System.Windows.Forms.Label lbl_currentLevel;
    }
}
