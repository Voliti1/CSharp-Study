namespace SCT_Form
{
    partial class PWSearch
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
            this.pnl_PWSearch = new System.Windows.Forms.TableLayoutPanel();
            this.cbox_UserLevel = new System.Windows.Forms.ComboBox();
            this.lbl_UserLevel = new System.Windows.Forms.Label();
            this.btn_IDSearch = new System.Windows.Forms.Button();
            this.btn_SignUp = new System.Windows.Forms.Button();
            this.btn_SignIn = new System.Windows.Forms.Button();
            this.txtBox_PWCheck = new System.Windows.Forms.TextBox();
            this.lbl_ID = new System.Windows.Forms.Label();
            this.lbl_Username = new System.Windows.Forms.Label();
            this.txtBox_ID = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.pnl_PWSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_PWSearch
            // 
            this.pnl_PWSearch.ColumnCount = 3;
            this.pnl_PWSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_PWSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_PWSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_PWSearch.Controls.Add(this.btn_Search, 2, 0);
            this.pnl_PWSearch.Controls.Add(this.cbox_UserLevel, 1, 1);
            this.pnl_PWSearch.Controls.Add(this.lbl_UserLevel, 0, 1);
            this.pnl_PWSearch.Controls.Add(this.btn_IDSearch, 2, 3);
            this.pnl_PWSearch.Controls.Add(this.btn_SignUp, 1, 3);
            this.pnl_PWSearch.Controls.Add(this.btn_SignIn, 0, 3);
            this.pnl_PWSearch.Controls.Add(this.txtBox_PWCheck, 1, 2);
            this.pnl_PWSearch.Controls.Add(this.lbl_ID, 0, 2);
            this.pnl_PWSearch.Controls.Add(this.lbl_Username, 0, 0);
            this.pnl_PWSearch.Controls.Add(this.txtBox_ID, 1, 0);
            this.pnl_PWSearch.Location = new System.Drawing.Point(20, 11);
            this.pnl_PWSearch.Name = "pnl_PWSearch";
            this.pnl_PWSearch.RowCount = 4;
            this.pnl_PWSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_PWSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_PWSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_PWSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_PWSearch.Size = new System.Drawing.Size(561, 158);
            this.pnl_PWSearch.TabIndex = 2;
            // 
            // cbox_UserLevel
            // 
            this.cbox_UserLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbox_UserLevel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.cbox_UserLevel.FormattingEnabled = true;
            this.cbox_UserLevel.Location = new System.Drawing.Point(190, 42);
            this.cbox_UserLevel.Name = "cbox_UserLevel";
            this.cbox_UserLevel.Size = new System.Drawing.Size(181, 23);
            this.cbox_UserLevel.TabIndex = 17;
            // 
            // lbl_UserLevel
            // 
            this.lbl_UserLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_UserLevel.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_UserLevel.Location = new System.Drawing.Point(3, 39);
            this.lbl_UserLevel.Name = "lbl_UserLevel";
            this.lbl_UserLevel.Size = new System.Drawing.Size(181, 39);
            this.lbl_UserLevel.TabIndex = 16;
            this.lbl_UserLevel.Text = "User Level";
            this.lbl_UserLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_IDSearch
            // 
            this.btn_IDSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_IDSearch.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.btn_IDSearch.Location = new System.Drawing.Point(375, 118);
            this.btn_IDSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btn_IDSearch.Name = "btn_IDSearch";
            this.btn_IDSearch.Size = new System.Drawing.Size(185, 39);
            this.btn_IDSearch.TabIndex = 15;
            this.btn_IDSearch.Text = "ID Search";
            this.btn_IDSearch.UseVisualStyleBackColor = true;
            // 
            // btn_SignUp
            // 
            this.btn_SignUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SignUp.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.btn_SignUp.Location = new System.Drawing.Point(188, 118);
            this.btn_SignUp.Margin = new System.Windows.Forms.Padding(1);
            this.btn_SignUp.Name = "btn_SignUp";
            this.btn_SignUp.Size = new System.Drawing.Size(185, 39);
            this.btn_SignUp.TabIndex = 14;
            this.btn_SignUp.Text = "Sign Up";
            this.btn_SignUp.UseVisualStyleBackColor = true;
            // 
            // btn_SignIn
            // 
            this.btn_SignIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SignIn.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.btn_SignIn.Location = new System.Drawing.Point(1, 118);
            this.btn_SignIn.Margin = new System.Windows.Forms.Padding(1);
            this.btn_SignIn.Name = "btn_SignIn";
            this.btn_SignIn.Size = new System.Drawing.Size(185, 39);
            this.btn_SignIn.TabIndex = 13;
            this.btn_SignIn.Text = "Sign In";
            this.btn_SignIn.UseVisualStyleBackColor = true;
            // 
            // txtBox_PWCheck
            // 
            this.txtBox_PWCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_PWCheck.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.txtBox_PWCheck.Location = new System.Drawing.Point(190, 81);
            this.txtBox_PWCheck.Name = "txtBox_PWCheck";
            this.txtBox_PWCheck.Size = new System.Drawing.Size(181, 34);
            this.txtBox_PWCheck.TabIndex = 9;
            this.txtBox_PWCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBox_PWCheck.UseSystemPasswordChar = true;
            // 
            // lbl_ID
            // 
            this.lbl_ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ID.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_ID.Location = new System.Drawing.Point(3, 78);
            this.lbl_ID.Name = "lbl_ID";
            this.lbl_ID.Size = new System.Drawing.Size(181, 39);
            this.lbl_ID.TabIndex = 8;
            this.lbl_ID.Text = "ID";
            this.lbl_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Username
            // 
            this.lbl_Username.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Username.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_Username.Location = new System.Drawing.Point(3, 0);
            this.lbl_Username.Name = "lbl_Username";
            this.lbl_Username.Size = new System.Drawing.Size(181, 39);
            this.lbl_Username.TabIndex = 0;
            this.lbl_Username.Text = "User name";
            this.lbl_Username.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // btn_Search
            // 
            this.btn_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Search.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btn_Search.Location = new System.Drawing.Point(375, 1);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Search.Name = "btn_Search";
            this.pnl_PWSearch.SetRowSpan(this.btn_Search, 3);
            this.btn_Search.Size = new System.Drawing.Size(185, 115);
            this.btn_Search.TabIndex = 19;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // PWSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_PWSearch);
            this.Name = "PWSearch";
            this.Size = new System.Drawing.Size(600, 226);
            this.pnl_PWSearch.ResumeLayout(false);
            this.pnl_PWSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnl_PWSearch;
        private System.Windows.Forms.Button btn_IDSearch;
        private System.Windows.Forms.Button btn_SignUp;
        private System.Windows.Forms.Button btn_SignIn;
        private System.Windows.Forms.TextBox txtBox_PWCheck;
        private System.Windows.Forms.Label lbl_ID;
        private System.Windows.Forms.Label lbl_Username;
        private System.Windows.Forms.TextBox txtBox_ID;
        private System.Windows.Forms.ComboBox cbox_UserLevel;
        private System.Windows.Forms.Label lbl_UserLevel;
        private System.Windows.Forms.Button btn_Search;
    }
}
