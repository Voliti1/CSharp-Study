namespace SCT_Form
{
    partial class SignUp
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
            this.pnl_SignUp = new System.Windows.Forms.TableLayoutPanel();
            this.txtBox_name = new System.Windows.Forms.TextBox();
            this.lbl_name = new System.Windows.Forms.Label();
            this.btn_PWSearch = new System.Windows.Forms.Button();
            this.btn_IDSearch = new System.Windows.Forms.Button();
            this.btn_SignIn = new System.Windows.Forms.Button();
            this.lbl_UserLevel = new System.Windows.Forms.Label();
            this.btn_SignUp = new System.Windows.Forms.Button();
            this.txtBox_PWCheck = new System.Windows.Forms.TextBox();
            this.lbl_PWCheck = new System.Windows.Forms.Label();
            this.lbl_ID = new System.Windows.Forms.Label();
            this.lbl_PW = new System.Windows.Forms.Label();
            this.txtBox_ID = new System.Windows.Forms.TextBox();
            this.txtBox_PW = new System.Windows.Forms.TextBox();
            this.cbox_UserLevel = new System.Windows.Forms.ComboBox();
            this.pnl_SignUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_SignUp
            // 
            this.pnl_SignUp.ColumnCount = 3;
            this.pnl_SignUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_SignUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_SignUp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_SignUp.Controls.Add(this.txtBox_name, 1, 3);
            this.pnl_SignUp.Controls.Add(this.lbl_name, 0, 3);
            this.pnl_SignUp.Controls.Add(this.btn_PWSearch, 2, 4);
            this.pnl_SignUp.Controls.Add(this.btn_IDSearch, 1, 4);
            this.pnl_SignUp.Controls.Add(this.btn_SignIn, 0, 4);
            this.pnl_SignUp.Controls.Add(this.lbl_UserLevel, 2, 0);
            this.pnl_SignUp.Controls.Add(this.btn_SignUp, 2, 2);
            this.pnl_SignUp.Controls.Add(this.txtBox_PWCheck, 1, 2);
            this.pnl_SignUp.Controls.Add(this.lbl_PWCheck, 0, 2);
            this.pnl_SignUp.Controls.Add(this.lbl_ID, 0, 0);
            this.pnl_SignUp.Controls.Add(this.lbl_PW, 0, 1);
            this.pnl_SignUp.Controls.Add(this.txtBox_ID, 1, 0);
            this.pnl_SignUp.Controls.Add(this.txtBox_PW, 1, 1);
            this.pnl_SignUp.Controls.Add(this.cbox_UserLevel, 2, 1);
            this.pnl_SignUp.Location = new System.Drawing.Point(21, 11);
            this.pnl_SignUp.Name = "pnl_SignUp";
            this.pnl_SignUp.RowCount = 5;
            this.pnl_SignUp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_SignUp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_SignUp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_SignUp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_SignUp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_SignUp.Size = new System.Drawing.Size(561, 202);
            this.pnl_SignUp.TabIndex = 1;
            // 
            // txtBox_name
            // 
            this.txtBox_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_name.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.txtBox_name.Location = new System.Drawing.Point(190, 123);
            this.txtBox_name.Name = "txtBox_name";
            this.txtBox_name.Size = new System.Drawing.Size(181, 34);
            this.txtBox_name.TabIndex = 20;
            this.txtBox_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_name
            // 
            this.lbl_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_name.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_name.Location = new System.Drawing.Point(3, 120);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(181, 40);
            this.lbl_name.TabIndex = 19;
            this.lbl_name.Text = "User Name";
            this.lbl_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_PWSearch
            // 
            this.btn_PWSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PWSearch.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.btn_PWSearch.Location = new System.Drawing.Point(375, 161);
            this.btn_PWSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btn_PWSearch.Name = "btn_PWSearch";
            this.btn_PWSearch.Size = new System.Drawing.Size(185, 40);
            this.btn_PWSearch.TabIndex = 18;
            this.btn_PWSearch.Text = "PW Search";
            this.btn_PWSearch.UseVisualStyleBackColor = true;
            this.btn_PWSearch.Click += new System.EventHandler(this.btn_PWSearch_Click);
            // 
            // btn_IDSearch
            // 
            this.btn_IDSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_IDSearch.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.btn_IDSearch.Location = new System.Drawing.Point(188, 161);
            this.btn_IDSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btn_IDSearch.Name = "btn_IDSearch";
            this.btn_IDSearch.Size = new System.Drawing.Size(185, 40);
            this.btn_IDSearch.TabIndex = 17;
            this.btn_IDSearch.Text = "ID Search";
            this.btn_IDSearch.UseVisualStyleBackColor = true;
            this.btn_IDSearch.Click += new System.EventHandler(this.btn_IDSearch_Click);
            // 
            // btn_SignIn
            // 
            this.btn_SignIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SignIn.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.btn_SignIn.Location = new System.Drawing.Point(1, 161);
            this.btn_SignIn.Margin = new System.Windows.Forms.Padding(1);
            this.btn_SignIn.Name = "btn_SignIn";
            this.btn_SignIn.Size = new System.Drawing.Size(185, 40);
            this.btn_SignIn.TabIndex = 16;
            this.btn_SignIn.Text = "Sign In";
            this.btn_SignIn.UseVisualStyleBackColor = true;
            this.btn_SignIn.Click += new System.EventHandler(this.btn_SignIn_Click);
            // 
            // lbl_UserLevel
            // 
            this.lbl_UserLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_UserLevel.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_UserLevel.Location = new System.Drawing.Point(377, 0);
            this.lbl_UserLevel.Name = "lbl_UserLevel";
            this.lbl_UserLevel.Size = new System.Drawing.Size(181, 40);
            this.lbl_UserLevel.TabIndex = 11;
            this.lbl_UserLevel.Text = "User Level";
            this.lbl_UserLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_SignUp
            // 
            this.btn_SignUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SignUp.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btn_SignUp.Location = new System.Drawing.Point(375, 81);
            this.btn_SignUp.Margin = new System.Windows.Forms.Padding(1);
            this.btn_SignUp.Name = "btn_SignUp";
            this.pnl_SignUp.SetRowSpan(this.btn_SignUp, 2);
            this.btn_SignUp.Size = new System.Drawing.Size(185, 78);
            this.btn_SignUp.TabIndex = 10;
            this.btn_SignUp.Text = "Sign Up";
            this.btn_SignUp.UseVisualStyleBackColor = true;
            // 
            // txtBox_PWCheck
            // 
            this.txtBox_PWCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_PWCheck.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.txtBox_PWCheck.Location = new System.Drawing.Point(190, 83);
            this.txtBox_PWCheck.Name = "txtBox_PWCheck";
            this.txtBox_PWCheck.Size = new System.Drawing.Size(181, 34);
            this.txtBox_PWCheck.TabIndex = 9;
            this.txtBox_PWCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBox_PWCheck.UseSystemPasswordChar = true;
            // 
            // lbl_PWCheck
            // 
            this.lbl_PWCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PWCheck.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_PWCheck.Location = new System.Drawing.Point(3, 80);
            this.lbl_PWCheck.Name = "lbl_PWCheck";
            this.lbl_PWCheck.Size = new System.Drawing.Size(181, 40);
            this.lbl_PWCheck.TabIndex = 8;
            this.lbl_PWCheck.Text = "PW Check";
            this.lbl_PWCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ID
            // 
            this.lbl_ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ID.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_ID.Location = new System.Drawing.Point(3, 0);
            this.lbl_ID.Name = "lbl_ID";
            this.lbl_ID.Size = new System.Drawing.Size(181, 40);
            this.lbl_ID.TabIndex = 0;
            this.lbl_ID.Text = "ID";
            this.lbl_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PW
            // 
            this.lbl_PW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PW.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_PW.Location = new System.Drawing.Point(3, 40);
            this.lbl_PW.Name = "lbl_PW";
            this.lbl_PW.Size = new System.Drawing.Size(181, 40);
            this.lbl_PW.TabIndex = 1;
            this.lbl_PW.Text = "Password";
            this.lbl_PW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBox_ID
            // 
            this.txtBox_ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_ID.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.txtBox_ID.Location = new System.Drawing.Point(190, 3);
            this.txtBox_ID.Name = "txtBox_ID";
            this.txtBox_ID.Size = new System.Drawing.Size(181, 34);
            this.txtBox_ID.TabIndex = 2;
            this.txtBox_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBox_PW
            // 
            this.txtBox_PW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_PW.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.txtBox_PW.Location = new System.Drawing.Point(190, 43);
            this.txtBox_PW.Name = "txtBox_PW";
            this.txtBox_PW.Size = new System.Drawing.Size(181, 34);
            this.txtBox_PW.TabIndex = 3;
            this.txtBox_PW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBox_PW.UseSystemPasswordChar = true;
            // 
            // cbox_UserLevel
            // 
            this.cbox_UserLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbox_UserLevel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.cbox_UserLevel.FormattingEnabled = true;
            this.cbox_UserLevel.Location = new System.Drawing.Point(377, 43);
            this.cbox_UserLevel.Name = "cbox_UserLevel";
            this.cbox_UserLevel.Size = new System.Drawing.Size(181, 23);
            this.cbox_UserLevel.TabIndex = 12;
            // 
            // SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_SignUp);
            this.Name = "SignUp";
            this.Size = new System.Drawing.Size(600, 226);
            this.pnl_SignUp.ResumeLayout(false);
            this.pnl_SignUp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnl_SignUp;
        private System.Windows.Forms.TextBox txtBox_PWCheck;
        private System.Windows.Forms.Label lbl_PWCheck;
        private System.Windows.Forms.Label lbl_ID;
        private System.Windows.Forms.Label lbl_PW;
        private System.Windows.Forms.TextBox txtBox_ID;
        private System.Windows.Forms.TextBox txtBox_PW;
        private System.Windows.Forms.Label lbl_UserLevel;
        private System.Windows.Forms.Button btn_SignUp;
        private System.Windows.Forms.ComboBox cbox_UserLevel;
        private System.Windows.Forms.Button btn_PWSearch;
        private System.Windows.Forms.Button btn_IDSearch;
        private System.Windows.Forms.Button btn_SignIn;
        private System.Windows.Forms.TextBox txtBox_name;
        private System.Windows.Forms.Label lbl_name;
    }
}
