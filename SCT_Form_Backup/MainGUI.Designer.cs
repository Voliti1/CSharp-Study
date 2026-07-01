namespace SCT_Form
{
    partial class MainGUI
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label lbl1;
            this.lbl_Connection = new System.Windows.Forms.Label();
            this.lbl_CurrentConnect = new System.Windows.Forms.Label();
            this.btn_Reconnect = new System.Windows.Forms.Button();
            this.DisConnect = new System.Windows.Forms.Button();
            this.btn_Operate = new System.Windows.Forms.Button();
            this.btn_Maint = new System.Windows.Forms.Button();
            this.btn_Setting = new System.Windows.Forms.Button();
            this.Mainpnl = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnl_Top = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_PW = new System.Windows.Forms.Label();
            this.tBox_PW = new System.Windows.Forms.TextBox();
            this.tBox_ID = new System.Windows.Forms.TextBox();
            this.lbl_ID = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            lbl1 = new System.Windows.Forms.Label();
            this.pnl_Top.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Connection
            // 
            this.lbl_Connection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Connection.Location = new System.Drawing.Point(3, 0);
            this.lbl_Connection.Name = "lbl_Connection";
            this.lbl_Connection.Size = new System.Drawing.Size(81, 44);
            this.lbl_Connection.TabIndex = 0;
            this.lbl_Connection.Text = "Connection";
            this.lbl_Connection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CurrentConnect
            // 
            this.lbl_CurrentConnect.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_CurrentConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CurrentConnect.Font = new System.Drawing.Font("굴림", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_CurrentConnect.ForeColor = System.Drawing.Color.Red;
            this.lbl_CurrentConnect.Location = new System.Drawing.Point(87, 0);
            this.lbl_CurrentConnect.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_CurrentConnect.Name = "lbl_CurrentConnect";
            this.lbl_CurrentConnect.Size = new System.Drawing.Size(52, 44);
            this.lbl_CurrentConnect.TabIndex = 1;
            this.lbl_CurrentConnect.Text = "●";
            this.lbl_CurrentConnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Reconnect
            // 
            this.btn_Reconnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Reconnect.Location = new System.Drawing.Point(142, 3);
            this.btn_Reconnect.Name = "btn_Reconnect";
            this.btn_Reconnect.Size = new System.Drawing.Size(99, 38);
            this.btn_Reconnect.TabIndex = 2;
            this.btn_Reconnect.Text = "RECONNECT";
            this.btn_Reconnect.UseVisualStyleBackColor = true;
            this.btn_Reconnect.Click += new System.EventHandler(this.Reconnect_Click);
            // 
            // DisConnect
            // 
            this.DisConnect.Location = new System.Drawing.Point(0, 0);
            this.DisConnect.Name = "DisConnect";
            this.DisConnect.Size = new System.Drawing.Size(75, 23);
            this.DisConnect.TabIndex = 37;
            // 
            // btn_Operate
            // 
            this.btn_Operate.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_Operate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Operate.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Operate.FlatAppearance.BorderSize = 2;
            this.btn_Operate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Operate.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btn_Operate.ForeColor = System.Drawing.Color.White;
            this.btn_Operate.Location = new System.Drawing.Point(1, 1);
            this.btn_Operate.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Operate.Name = "btn_Operate";
            this.btn_Operate.Size = new System.Drawing.Size(107, 48);
            this.btn_Operate.TabIndex = 37;
            this.btn_Operate.Text = "Operate";
            this.btn_Operate.UseVisualStyleBackColor = false;
            this.btn_Operate.Click += new System.EventHandler(this.btn_Operate_Click);
            this.btn_Operate.Paint += new System.Windows.Forms.PaintEventHandler(this.btnMode_Paint);
            // 
            // btn_Maint
            // 
            this.btn_Maint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Maint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Maint.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btn_Maint.ForeColor = System.Drawing.Color.Silver;
            this.btn_Maint.Location = new System.Drawing.Point(110, 1);
            this.btn_Maint.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Maint.Name = "btn_Maint";
            this.btn_Maint.Size = new System.Drawing.Size(107, 48);
            this.btn_Maint.TabIndex = 38;
            this.btn_Maint.Text = "MAINT";
            this.btn_Maint.UseVisualStyleBackColor = true;
            this.btn_Maint.Click += new System.EventHandler(this.btn_maint_Click);
            this.btn_Maint.Paint += new System.Windows.Forms.PaintEventHandler(this.btnMode_Paint);
            // 
            // btn_Setting
            // 
            this.btn_Setting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Setting.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Setting.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
            this.btn_Setting.ForeColor = System.Drawing.Color.Black;
            this.btn_Setting.Location = new System.Drawing.Point(221, 3);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Size = new System.Drawing.Size(103, 44);
            this.btn_Setting.TabIndex = 44;
            this.btn_Setting.Text = "SETTING";
            this.btn_Setting.UseVisualStyleBackColor = true;
            this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
            // 
            // Mainpnl
            // 
            this.Mainpnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Mainpnl.Location = new System.Drawing.Point(0, 50);
            this.Mainpnl.Name = "Mainpnl";
            this.Mainpnl.Size = new System.Drawing.Size(1000, 750);
            this.Mainpnl.TabIndex = 45;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnl_Top
            // 
            this.pnl_Top.ColumnCount = 4;
            this.pnl_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_Top.Controls.Add(this.tableLayoutPanel8, 2, 0);
            this.pnl_Top.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.pnl_Top.Controls.Add(lbl1, 3, 0);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.RowCount = 1;
            this.pnl_Top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_Top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.pnl_Top.Size = new System.Drawing.Size(1000, 50);
            this.pnl_Top.TabIndex = 36;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.71428F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.42857F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.tableLayoutPanel8.Controls.Add(this.lbl_CurrentConnect, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.btn_Reconnect, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.lbl_Connection, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(503, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(244, 44);
            this.tableLayoutPanel8.TabIndex = 37;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.lbl_PW, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tBox_PW, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.tBox_ID, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbl_ID, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(253, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(244, 44);
            this.tableLayoutPanel3.TabIndex = 37;
            // 
            // lbl_PW
            // 
            this.lbl_PW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PW.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_PW.Location = new System.Drawing.Point(125, 0);
            this.lbl_PW.Name = "lbl_PW";
            this.lbl_PW.Size = new System.Drawing.Size(116, 24);
            this.lbl_PW.TabIndex = 40;
            this.lbl_PW.Text = "PW";
            this.lbl_PW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tBox_PW
            // 
            this.tBox_PW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tBox_PW.Location = new System.Drawing.Point(125, 27);
            this.tBox_PW.Name = "tBox_PW";
            this.tBox_PW.Size = new System.Drawing.Size(116, 21);
            this.tBox_PW.TabIndex = 38;
            // 
            // tBox_ID
            // 
            this.tBox_ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tBox_ID.Location = new System.Drawing.Point(3, 27);
            this.tBox_ID.Name = "tBox_ID";
            this.tBox_ID.Size = new System.Drawing.Size(116, 21);
            this.tBox_ID.TabIndex = 37;
            // 
            // lbl_ID
            // 
            this.lbl_ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ID.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_ID.Location = new System.Drawing.Point(3, 0);
            this.lbl_ID.Name = "lbl_ID";
            this.lbl_ID.Size = new System.Drawing.Size(116, 24);
            this.lbl_ID.TabIndex = 39;
            this.lbl_ID.Text = "ID";
            this.lbl_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.90909F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.90909F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.90909F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.27273F));
            this.tableLayoutPanel5.Controls.Add(this.btn_Operate, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btn_Maint, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btn_Setting, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 800);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1000, 50);
            this.tableLayoutPanel5.TabIndex = 38;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Dock = System.Windows.Forms.DockStyle.Fill;
            lbl1.Location = new System.Drawing.Point(753, 0);
            lbl1.Name = "lbl1";
            lbl1.Size = new System.Drawing.Size(244, 50);
            lbl1.TabIndex = 55;
            lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 850);
            this.Controls.Add(this.Mainpnl);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.pnl_Top);
            this.Controls.Add(this.DisConnect);
            this.Name = "MainGUI";
            this.Text = "Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.pnl_Top.ResumeLayout(false);
            this.pnl_Top.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lbl_Connection;
        internal System.Windows.Forms.Label lbl_CurrentConnect;
        internal System.Windows.Forms.Button btn_Reconnect;
        internal System.Windows.Forms.Button DisConnect;
        internal System.Windows.Forms.Button btn_Operate;
        internal System.Windows.Forms.Button btn_Maint;
        internal System.Windows.Forms.Button btn_Setting;
        internal System.Windows.Forms.Panel Mainpnl;
        internal System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.TableLayoutPanel pnl_Top;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        internal System.Windows.Forms.Label lbl_PW;
        internal System.Windows.Forms.TextBox tBox_PW;
        internal System.Windows.Forms.TextBox tBox_ID;
        internal System.Windows.Forms.Label lbl_ID;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    }
}

