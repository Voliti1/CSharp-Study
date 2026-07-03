namespace SCT_Form
{
    partial class IDSearch
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
            this.pnl_IDSearch = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Search = new System.Windows.Forms.Button();
            this.cbox_UserLevel = new System.Windows.Forms.ComboBox();
            this.lbl_UserLevel = new System.Windows.Forms.Label();
            this.btn_PWSearch = new System.Windows.Forms.Button();
            this.btn_SignUp = new System.Windows.Forms.Button();
            this.btn_SignIn = new System.Windows.Forms.Button();
            this.lbl_Username = new System.Windows.Forms.Label();
            this.txtBox_Username = new System.Windows.Forms.TextBox();
            this.pnl_IDSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_IDSearch
            // 
            this.pnl_IDSearch.ColumnCount = 3;
            this.pnl_IDSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_IDSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_IDSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_IDSearch.Controls.Add(this.btn_Search, 2, 0);
            this.pnl_IDSearch.Controls.Add(this.cbox_UserLevel, 1, 1);
            this.pnl_IDSearch.Controls.Add(this.lbl_UserLevel, 0, 1);
            this.pnl_IDSearch.Controls.Add(this.btn_PWSearch, 2, 2);
            this.pnl_IDSearch.Controls.Add(this.btn_SignUp, 1, 2);
            this.pnl_IDSearch.Controls.Add(this.btn_SignIn, 0, 2);
            this.pnl_IDSearch.Controls.Add(this.lbl_Username, 0, 0);
            this.pnl_IDSearch.Controls.Add(this.txtBox_Username, 1, 0);
            this.pnl_IDSearch.Location = new System.Drawing.Point(19, 17);
            this.pnl_IDSearch.Name = "pnl_IDSearch";
            this.pnl_IDSearch.RowCount = 3;
            this.pnl_IDSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_IDSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_IDSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_IDSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnl_IDSearch.Size = new System.Drawing.Size(561, 116);
            this.pnl_IDSearch.TabIndex = 2;
            // 
            // btn_Search
            // 
            this.btn_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Search.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btn_Search.Location = new System.Drawing.Point(375, 1);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Search.Name = "btn_Search";
            this.pnl_IDSearch.SetRowSpan(this.btn_Search, 2);
            this.btn_Search.Size = new System.Drawing.Size(185, 74);
            this.btn_Search.TabIndex = 18;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // cbox_UserLevel
            // 
            this.cbox_UserLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbox_UserLevel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.cbox_UserLevel.FormattingEnabled = true;
            this.cbox_UserLevel.Location = new System.Drawing.Point(190, 41);
            this.cbox_UserLevel.Name = "cbox_UserLevel";
            this.cbox_UserLevel.Size = new System.Drawing.Size(181, 23);
            this.cbox_UserLevel.TabIndex = 17;
            // 
            // lbl_UserLevel
            // 
            this.lbl_UserLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_UserLevel.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_UserLevel.Location = new System.Drawing.Point(3, 38);
            this.lbl_UserLevel.Name = "lbl_UserLevel";
            this.lbl_UserLevel.Size = new System.Drawing.Size(181, 38);
            this.lbl_UserLevel.TabIndex = 16;
            this.lbl_UserLevel.Text = "User Level";
            this.lbl_UserLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_PWSearch
            // 
            this.btn_PWSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PWSearch.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.btn_PWSearch.Location = new System.Drawing.Point(375, 77);
            this.btn_PWSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btn_PWSearch.Name = "btn_PWSearch";
            this.btn_PWSearch.Size = new System.Drawing.Size(185, 38);
            this.btn_PWSearch.TabIndex = 15;
            this.btn_PWSearch.Text = "PW Search";
            this.btn_PWSearch.UseVisualStyleBackColor = true;
            // 
            // btn_SignUp
            // 
            this.btn_SignUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SignUp.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.btn_SignUp.Location = new System.Drawing.Point(188, 77);
            this.btn_SignUp.Margin = new System.Windows.Forms.Padding(1);
            this.btn_SignUp.Name = "btn_SignUp";
            this.btn_SignUp.Size = new System.Drawing.Size(185, 38);
            this.btn_SignUp.TabIndex = 14;
            this.btn_SignUp.Text = "Sign Up";
            this.btn_SignUp.UseVisualStyleBackColor = true;
            // 
            // btn_SignIn
            // 
            this.btn_SignIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SignIn.Font = new System.Drawing.Font("맑은 고딕", 14.25F);
            this.btn_SignIn.Location = new System.Drawing.Point(1, 77);
            this.btn_SignIn.Margin = new System.Windows.Forms.Padding(1);
            this.btn_SignIn.Name = "btn_SignIn";
            this.btn_SignIn.Size = new System.Drawing.Size(185, 38);
            this.btn_SignIn.TabIndex = 13;
            this.btn_SignIn.Text = "Sign In";
            this.btn_SignIn.UseVisualStyleBackColor = true;
            // 
            // lbl_Username
            // 
            this.lbl_Username.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Username.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_Username.Location = new System.Drawing.Point(3, 0);
            this.lbl_Username.Name = "lbl_Username";
            this.lbl_Username.Size = new System.Drawing.Size(181, 38);
            this.lbl_Username.TabIndex = 0;
            this.lbl_Username.Text = "User Name";
            this.lbl_Username.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBox_Username
            // 
            this.txtBox_Username.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_Username.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.txtBox_Username.Location = new System.Drawing.Point(190, 3);
            this.txtBox_Username.Name = "txtBox_Username";
            this.txtBox_Username.Size = new System.Drawing.Size(181, 34);
            this.txtBox_Username.TabIndex = 2;
            this.txtBox_Username.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // IDSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_IDSearch);
            this.Name = "IDSearch";
            this.Size = new System.Drawing.Size(600, 226);
            this.pnl_IDSearch.ResumeLayout(false);
            this.pnl_IDSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnl_IDSearch;
        private System.Windows.Forms.Button btn_PWSearch;
        private System.Windows.Forms.Button btn_SignIn;
        private System.Windows.Forms.Button btn_SignUp;
        private System.Windows.Forms.Label lbl_Username;
        private System.Windows.Forms.TextBox txtBox_Username;
        private System.Windows.Forms.ComboBox cbox_UserLevel;
        private System.Windows.Forms.Label lbl_UserLevel;
        private System.Windows.Forms.Button btn_Search;
    }
}
