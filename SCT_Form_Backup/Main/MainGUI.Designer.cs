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
            this.pnl_Datetime = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.lbl_Date = new System.Windows.Forms.Label();
            this.pnl_PMStatus = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_PMCStatus = new System.Windows.Forms.Label();
            this.lbl_PMBStatus = new System.Windows.Forms.Label();
            this.lbl_PMAStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_PMStatus_PMA = new System.Windows.Forms.Label();
            this.pnl_Connection = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_TopBarText = new System.Windows.Forms.Label();
            this.btn_AlarmReset = new System.Windows.Forms.Button();
            this.pnl_LoginChange = new System.Windows.Forms.Panel();
            this.pnl_LogIn = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_PW = new System.Windows.Forms.Label();
            this.tBox_PW = new System.Windows.Forms.TextBox();
            this.tBox_ID = new System.Windows.Forms.TextBox();
            this.lbl_ID = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Log = new System.Windows.Forms.Button();
            this.btn_Recipe = new System.Windows.Forms.Button();
            this.pnl_Top.SuspendLayout();
            this.pnl_Datetime.SuspendLayout();
            this.pnl_PMStatus.SuspendLayout();
            this.pnl_Connection.SuspendLayout();
            this.pnl_LoginChange.SuspendLayout();
            this.pnl_LogIn.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Connection
            // 
            this.lbl_Connection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Connection.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
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
            this.btn_Reconnect.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
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
            this.btn_Operate.Size = new System.Drawing.Size(98, 48);
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
            this.btn_Maint.Location = new System.Drawing.Point(101, 1);
            this.btn_Maint.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Maint.Name = "btn_Maint";
            this.btn_Maint.Size = new System.Drawing.Size(98, 48);
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
            this.btn_Setting.ForeColor = System.Drawing.Color.Silver;
            this.btn_Setting.Location = new System.Drawing.Point(401, 1);
            this.btn_Setting.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Size = new System.Drawing.Size(98, 48);
            this.btn_Setting.TabIndex = 44;
            this.btn_Setting.Text = "SETTING";
            this.btn_Setting.UseVisualStyleBackColor = true;
            this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
            // 
            // Mainpnl
            // 
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
            this.pnl_Top.ColumnCount = 6;
            this.pnl_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnl_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.pnl_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.pnl_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnl_Top.Controls.Add(this.pnl_Datetime, 4, 0);
            this.pnl_Top.Controls.Add(this.pnl_PMStatus, 2, 0);
            this.pnl_Top.Controls.Add(this.pnl_Connection, 3, 0);
            this.pnl_Top.Controls.Add(this.lbl_TopBarText, 0, 0);
            this.pnl_Top.Controls.Add(this.btn_AlarmReset, 5, 0);
            this.pnl_Top.Controls.Add(this.pnl_LoginChange, 1, 0);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.RowCount = 1;
            this.pnl_Top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_Top.Size = new System.Drawing.Size(1000, 50);
            this.pnl_Top.TabIndex = 36;
            // 
            // pnl_Datetime
            // 
            this.pnl_Datetime.ColumnCount = 1;
            this.pnl_Datetime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_Datetime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_Datetime.Controls.Add(this.lbl_Time, 0, 1);
            this.pnl_Datetime.Controls.Add(this.lbl_Date, 0, 0);
            this.pnl_Datetime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Datetime.Location = new System.Drawing.Point(753, 3);
            this.pnl_Datetime.Name = "pnl_Datetime";
            this.pnl_Datetime.RowCount = 2;
            this.pnl_Datetime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_Datetime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_Datetime.Size = new System.Drawing.Size(144, 44);
            this.pnl_Datetime.TabIndex = 0;
            // 
            // lbl_Time
            // 
            this.lbl_Time.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Time.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lbl_Time.Location = new System.Drawing.Point(3, 22);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(138, 22);
            this.lbl_Time.TabIndex = 1;
            this.lbl_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Date
            // 
            this.lbl_Date.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Date.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lbl_Date.Location = new System.Drawing.Point(3, 0);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(138, 22);
            this.lbl_Date.TabIndex = 0;
            this.lbl_Date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_PMStatus
            // 
            this.pnl_PMStatus.ColumnCount = 3;
            this.pnl_PMStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_PMStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_PMStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_PMStatus.Controls.Add(this.lbl_PMCStatus, 2, 1);
            this.pnl_PMStatus.Controls.Add(this.lbl_PMBStatus, 1, 1);
            this.pnl_PMStatus.Controls.Add(this.lbl_PMAStatus, 0, 1);
            this.pnl_PMStatus.Controls.Add(this.label2, 2, 0);
            this.pnl_PMStatus.Controls.Add(this.label1, 1, 0);
            this.pnl_PMStatus.Controls.Add(this.lbl_PMStatus_PMA, 0, 0);
            this.pnl_PMStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_PMStatus.Location = new System.Drawing.Point(353, 3);
            this.pnl_PMStatus.Name = "pnl_PMStatus";
            this.pnl_PMStatus.RowCount = 2;
            this.pnl_PMStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_PMStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_PMStatus.Size = new System.Drawing.Size(144, 44);
            this.pnl_PMStatus.TabIndex = 0;
            // 
            // lbl_PMCStatus
            // 
            this.lbl_PMCStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_PMCStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PMCStatus.Font = new System.Drawing.Font("굴림", 15F);
            this.lbl_PMCStatus.ForeColor = System.Drawing.Color.Gray;
            this.lbl_PMCStatus.Location = new System.Drawing.Point(94, 22);
            this.lbl_PMCStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_PMCStatus.Name = "lbl_PMCStatus";
            this.lbl_PMCStatus.Size = new System.Drawing.Size(50, 22);
            this.lbl_PMCStatus.TabIndex = 5;
            this.lbl_PMCStatus.Text = "●";
            this.lbl_PMCStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PMBStatus
            // 
            this.lbl_PMBStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_PMBStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PMBStatus.Font = new System.Drawing.Font("굴림", 15F);
            this.lbl_PMBStatus.ForeColor = System.Drawing.Color.Gray;
            this.lbl_PMBStatus.Location = new System.Drawing.Point(47, 22);
            this.lbl_PMBStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_PMBStatus.Name = "lbl_PMBStatus";
            this.lbl_PMBStatus.Size = new System.Drawing.Size(47, 22);
            this.lbl_PMBStatus.TabIndex = 4;
            this.lbl_PMBStatus.Text = "●";
            this.lbl_PMBStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PMAStatus
            // 
            this.lbl_PMAStatus.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_PMAStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PMAStatus.Font = new System.Drawing.Font("굴림", 15F);
            this.lbl_PMAStatus.ForeColor = System.Drawing.Color.Gray;
            this.lbl_PMAStatus.Location = new System.Drawing.Point(0, 22);
            this.lbl_PMAStatus.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_PMAStatus.Name = "lbl_PMAStatus";
            this.lbl_PMAStatus.Size = new System.Drawing.Size(47, 22);
            this.lbl_PMAStatus.TabIndex = 3;
            this.lbl_PMAStatus.Text = "●";
            this.lbl_PMAStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(97, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "PM C";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(50, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "PM B";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PMStatus_PMA
            // 
            this.lbl_PMStatus_PMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PMStatus_PMA.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_PMStatus_PMA.Location = new System.Drawing.Point(3, 0);
            this.lbl_PMStatus_PMA.Name = "lbl_PMStatus_PMA";
            this.lbl_PMStatus_PMA.Size = new System.Drawing.Size(41, 22);
            this.lbl_PMStatus_PMA.TabIndex = 0;
            this.lbl_PMStatus_PMA.Text = "PM A";
            this.lbl_PMStatus_PMA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_Connection
            // 
            this.pnl_Connection.ColumnCount = 3;
            this.pnl_Connection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.71428F));
            this.pnl_Connection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.42857F));
            this.pnl_Connection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.85714F));
            this.pnl_Connection.Controls.Add(this.lbl_CurrentConnect, 1, 0);
            this.pnl_Connection.Controls.Add(this.btn_Reconnect, 2, 0);
            this.pnl_Connection.Controls.Add(this.lbl_Connection, 0, 0);
            this.pnl_Connection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Connection.Location = new System.Drawing.Point(503, 3);
            this.pnl_Connection.Name = "pnl_Connection";
            this.pnl_Connection.RowCount = 1;
            this.pnl_Connection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_Connection.Size = new System.Drawing.Size(244, 44);
            this.pnl_Connection.TabIndex = 37;
            // 
            // lbl_TopBarText
            // 
            this.lbl_TopBarText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TopBarText.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_TopBarText.Location = new System.Drawing.Point(3, 0);
            this.lbl_TopBarText.Name = "lbl_TopBarText";
            this.lbl_TopBarText.Size = new System.Drawing.Size(94, 50);
            this.lbl_TopBarText.TabIndex = 56;
            this.lbl_TopBarText.Text = "실습 장비\r\n제어 GUI";
            this.lbl_TopBarText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_AlarmReset
            // 
            this.btn_AlarmReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_AlarmReset.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Bold);
            this.btn_AlarmReset.ForeColor = System.Drawing.Color.Black;
            this.btn_AlarmReset.Location = new System.Drawing.Point(903, 3);
            this.btn_AlarmReset.Name = "btn_AlarmReset";
            this.btn_AlarmReset.Size = new System.Drawing.Size(94, 44);
            this.btn_AlarmReset.TabIndex = 57;
            this.btn_AlarmReset.Text = "Alarm\r\nReset";
            this.btn_AlarmReset.UseVisualStyleBackColor = false;
            this.btn_AlarmReset.Click += new System.EventHandler(this.btn_AlarmReset_Click);
            // 
            // pnl_LoginChange
            // 
            this.pnl_LoginChange.Controls.Add(this.pnl_LogIn);
            this.pnl_LoginChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_LoginChange.Location = new System.Drawing.Point(100, 0);
            this.pnl_LoginChange.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_LoginChange.Name = "pnl_LoginChange";
            this.pnl_LoginChange.Size = new System.Drawing.Size(250, 50);
            this.pnl_LoginChange.TabIndex = 58;
            // 
            // pnl_LogIn
            // 
            this.pnl_LogIn.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.pnl_LogIn.ColumnCount = 2;
            this.pnl_LogIn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_LogIn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_LogIn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnl_LogIn.Controls.Add(this.lbl_PW, 1, 0);
            this.pnl_LogIn.Controls.Add(this.tBox_PW, 1, 1);
            this.pnl_LogIn.Controls.Add(this.tBox_ID, 0, 1);
            this.pnl_LogIn.Controls.Add(this.lbl_ID, 0, 0);
            this.pnl_LogIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_LogIn.Location = new System.Drawing.Point(0, 0);
            this.pnl_LogIn.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_LogIn.Name = "pnl_LogIn";
            this.pnl_LogIn.RowCount = 2;
            this.pnl_LogIn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.93877F));
            this.pnl_LogIn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.06123F));
            this.pnl_LogIn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnl_LogIn.Size = new System.Drawing.Size(250, 50);
            this.pnl_LogIn.TabIndex = 37;
            // 
            // lbl_PW
            // 
            this.lbl_PW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PW.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_PW.Location = new System.Drawing.Point(128, 1);
            this.lbl_PW.Name = "lbl_PW";
            this.lbl_PW.Size = new System.Drawing.Size(118, 22);
            this.lbl_PW.TabIndex = 40;
            this.lbl_PW.Text = "PW";
            this.lbl_PW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tBox_PW
            // 
            this.tBox_PW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tBox_PW.ForeColor = System.Drawing.Color.White;
            this.tBox_PW.Location = new System.Drawing.Point(128, 27);
            this.tBox_PW.Name = "tBox_PW";
            this.tBox_PW.Size = new System.Drawing.Size(118, 21);
            this.tBox_PW.TabIndex = 38;
            // 
            // tBox_ID
            // 
            this.tBox_ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tBox_ID.ForeColor = System.Drawing.Color.White;
            this.tBox_ID.Location = new System.Drawing.Point(4, 27);
            this.tBox_ID.Name = "tBox_ID";
            this.tBox_ID.Size = new System.Drawing.Size(117, 21);
            this.tBox_ID.TabIndex = 37;
            // 
            // lbl_ID
            // 
            this.lbl_ID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ID.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_ID.Location = new System.Drawing.Point(4, 1);
            this.lbl_ID.Name = "lbl_ID";
            this.lbl_ID.Size = new System.Drawing.Size(117, 22);
            this.lbl_ID.TabIndex = 39;
            this.lbl_ID.Text = "ID";
            this.lbl_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tableLayoutPanel5.ColumnCount = 6;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btn_Log, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.btn_Recipe, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.btn_Operate, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btn_Maint, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btn_Setting, 4, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 800);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1000, 50);
            this.tableLayoutPanel5.TabIndex = 38;
            // 
            // btn_Log
            // 
            this.btn_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Log.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Log.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
            this.btn_Log.ForeColor = System.Drawing.Color.Silver;
            this.btn_Log.Location = new System.Drawing.Point(301, 1);
            this.btn_Log.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Log.Name = "btn_Log";
            this.btn_Log.Size = new System.Drawing.Size(98, 48);
            this.btn_Log.TabIndex = 46;
            this.btn_Log.Text = "LOG";
            this.btn_Log.UseVisualStyleBackColor = true;
            this.btn_Log.Click += new System.EventHandler(this.btn_Log_Click);
            // 
            // btn_Recipe
            // 
            this.btn_Recipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Recipe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Recipe.Font = new System.Drawing.Font("굴림", 14F, System.Drawing.FontStyle.Bold);
            this.btn_Recipe.ForeColor = System.Drawing.Color.Silver;
            this.btn_Recipe.Location = new System.Drawing.Point(201, 1);
            this.btn_Recipe.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Recipe.Name = "btn_Recipe";
            this.btn_Recipe.Size = new System.Drawing.Size(98, 48);
            this.btn_Recipe.TabIndex = 45;
            this.btn_Recipe.Text = "RECIPE";
            this.btn_Recipe.UseVisualStyleBackColor = true;
            this.btn_Recipe.Click += new System.EventHandler(this.btn_Recipe_Click);
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
            this.pnl_Datetime.ResumeLayout(false);
            this.pnl_PMStatus.ResumeLayout(false);
            this.pnl_Connection.ResumeLayout(false);
            this.pnl_LoginChange.ResumeLayout(false);
            this.pnl_LogIn.ResumeLayout(false);
            this.pnl_LogIn.PerformLayout();
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
        internal System.Windows.Forms.TableLayoutPanel pnl_Connection;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        internal System.Windows.Forms.Button btn_Log;
        internal System.Windows.Forms.Button btn_Recipe;
        internal System.Windows.Forms.TableLayoutPanel pnl_LogIn;
        internal System.Windows.Forms.Label lbl_PW;
        internal System.Windows.Forms.TextBox tBox_PW;
        internal System.Windows.Forms.TextBox tBox_ID;
        internal System.Windows.Forms.Label lbl_ID;
        private System.Windows.Forms.Label lbl_TopBarText;
        private System.Windows.Forms.TableLayoutPanel pnl_PMStatus;
        private System.Windows.Forms.Label lbl_PMStatus_PMA;
        internal System.Windows.Forms.Label lbl_PMCStatus;
        internal System.Windows.Forms.Label lbl_PMBStatus;
        internal System.Windows.Forms.Label lbl_PMAStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel pnl_Datetime;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label lbl_Date;
        private System.Windows.Forms.Button btn_AlarmReset;
        private System.Windows.Forms.Panel pnl_LoginChange;
    }
}

