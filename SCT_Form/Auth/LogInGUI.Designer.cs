namespace SCT_Form
{
    partial class LogInGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_Login = new System.Windows.Forms.TableLayoutPanel();
            this.btn_PWSearch = new System.Windows.Forms.Button();
            this.btn_IDSearch = new System.Windows.Forms.Button();
            this.btn_SignUp = new System.Windows.Forms.Button();
            this.lbl_ID = new System.Windows.Forms.Label();
            this.lbl_PW = new System.Windows.Forms.Label();
            this.txtBox_ID = new System.Windows.Forms.TextBox();
            this.txtBox_PW = new System.Windows.Forms.TextBox();
            this.btn_SignIn = new System.Windows.Forms.Button();
            this.pnl_LoginChange = new System.Windows.Forms.Panel();
            this.pnl_Login.SuspendLayout();
            this.pnl_LoginChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Login
            // 
            this.pnl_Login.ColumnCount = 3;
            this.pnl_Login.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_Login.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_Login.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_Login.Controls.Add(this.btn_PWSearch, 2, 2);
            this.pnl_Login.Controls.Add(this.btn_IDSearch, 1, 2);
            this.pnl_Login.Controls.Add(this.btn_SignUp, 0, 2);
            this.pnl_Login.Controls.Add(this.lbl_ID, 0, 0);
            this.pnl_Login.Controls.Add(this.lbl_PW, 0, 1);
            this.pnl_Login.Controls.Add(this.txtBox_ID, 1, 0);
            this.pnl_Login.Controls.Add(this.txtBox_PW, 1, 1);
            this.pnl_Login.Controls.Add(this.btn_SignIn, 2, 0);
            this.pnl_Login.Location = new System.Drawing.Point(12, 12);
            this.pnl_Login.Name = "pnl_Login";
            this.pnl_Login.RowCount = 3;
            this.pnl_Login.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_Login.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_Login.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_Login.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnl_Login.Size = new System.Drawing.Size(577, 131);
            this.pnl_Login.TabIndex = 0;
            // 
            // btn_PWSearch
            // 
            this.btn_PWSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PWSearch.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_PWSearch.Location = new System.Drawing.Point(387, 89);
            this.btn_PWSearch.Name = "btn_PWSearch";
            this.btn_PWSearch.Size = new System.Drawing.Size(187, 39);
            this.btn_PWSearch.TabIndex = 7;
            this.btn_PWSearch.Text = "PW Search";
            this.btn_PWSearch.UseVisualStyleBackColor = true;
            this.btn_PWSearch.Click += new System.EventHandler(this.btn_PWSearch_Click);
            // 
            // btn_IDSearch
            // 
            this.btn_IDSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_IDSearch.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_IDSearch.Location = new System.Drawing.Point(195, 89);
            this.btn_IDSearch.Name = "btn_IDSearch";
            this.btn_IDSearch.Size = new System.Drawing.Size(186, 39);
            this.btn_IDSearch.TabIndex = 6;
            this.btn_IDSearch.Text = "ID Search";
            this.btn_IDSearch.UseVisualStyleBackColor = true;
            this.btn_IDSearch.Click += new System.EventHandler(this.btn_IDSearch_Click);
            // 
            // btn_SignUp
            // 
            this.btn_SignUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SignUp.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_SignUp.Location = new System.Drawing.Point(3, 89);
            this.btn_SignUp.Name = "btn_SignUp";
            this.btn_SignUp.Size = new System.Drawing.Size(186, 39);
            this.btn_SignUp.TabIndex = 5;
            this.btn_SignUp.Text = "Sign Up";
            this.btn_SignUp.UseVisualStyleBackColor = true;
            this.btn_SignUp.Click += new System.EventHandler(this.btn_SignUp_Click);
            // 
            // lbl_ID
            // 
            this.lbl_ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ID.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lbl_ID.Location = new System.Drawing.Point(3, 0);
            this.lbl_ID.Name = "lbl_ID";
            this.lbl_ID.Size = new System.Drawing.Size(186, 43);
            this.lbl_ID.TabIndex = 0;
            this.lbl_ID.Text = "ID";
            this.lbl_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PW
            // 
            this.lbl_PW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PW.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.lbl_PW.Location = new System.Drawing.Point(3, 43);
            this.lbl_PW.Name = "lbl_PW";
            this.lbl_PW.Size = new System.Drawing.Size(186, 43);
            this.lbl_PW.TabIndex = 1;
            this.lbl_PW.Text = "Password";
            this.lbl_PW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBox_ID
            // 
            this.txtBox_ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_ID.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.txtBox_ID.Location = new System.Drawing.Point(195, 3);
            this.txtBox_ID.Name = "txtBox_ID";
            this.txtBox_ID.Size = new System.Drawing.Size(186, 34);
            this.txtBox_ID.TabIndex = 2;
            this.txtBox_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBox_PW
            // 
            this.txtBox_PW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox_PW.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.txtBox_PW.Location = new System.Drawing.Point(195, 46);
            this.txtBox_PW.Name = "txtBox_PW";
            this.txtBox_PW.Size = new System.Drawing.Size(186, 34);
            this.txtBox_PW.TabIndex = 3;
            this.txtBox_PW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBox_PW.UseSystemPasswordChar = true;
            // 
            // btn_SignIn
            // 
            this.btn_SignIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SignIn.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_SignIn.Location = new System.Drawing.Point(387, 3);
            this.btn_SignIn.Name = "btn_SignIn";
            this.pnl_Login.SetRowSpan(this.btn_SignIn, 2);
            this.btn_SignIn.Size = new System.Drawing.Size(187, 80);
            this.btn_SignIn.TabIndex = 4;
            this.btn_SignIn.Text = "Sign In";
            this.btn_SignIn.UseVisualStyleBackColor = true;
            // 
            // pnl_LoginChange
            // 
            this.pnl_LoginChange.Controls.Add(this.pnl_Login);
            this.pnl_LoginChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_LoginChange.Location = new System.Drawing.Point(0, 0);
            this.pnl_LoginChange.Name = "pnl_LoginChange";
            this.pnl_LoginChange.Size = new System.Drawing.Size(600, 226);
            this.pnl_LoginChange.TabIndex = 1;
            // 
            // LogInGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 226);
            this.Controls.Add(this.pnl_LoginChange);
            this.Name = "LogInGUI";
            this.Text = "Sign In";
            this.pnl_Login.ResumeLayout(false);
            this.pnl_Login.PerformLayout();
            this.pnl_LoginChange.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnl_Login;
        private System.Windows.Forms.Label lbl_ID;
        private System.Windows.Forms.Label lbl_PW;
        private System.Windows.Forms.TextBox txtBox_ID;
        private System.Windows.Forms.TextBox txtBox_PW;
        private System.Windows.Forms.Button btn_SignIn;
        private System.Windows.Forms.Button btn_PWSearch;
        private System.Windows.Forms.Button btn_IDSearch;
        private System.Windows.Forms.Button btn_SignUp;
        private System.Windows.Forms.Panel pnl_LoginChange;
    }
}