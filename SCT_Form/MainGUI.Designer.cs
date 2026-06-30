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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Connect = new System.Windows.Forms.Button();
            this.DisConnect = new System.Windows.Forms.Button();
            this.YellowLightOn = new System.Windows.Forms.Button();
            this.RedLightOn = new System.Windows.Forms.Button();
            this.YellowLightOff = new System.Windows.Forms.Button();
            this.RedLightOff = new System.Windows.Forms.Button();
            this.GreenLightOn = new System.Windows.Forms.Button();
            this.GreenLightOff = new System.Windows.Forms.Button();
            this.AllLightOff = new System.Windows.Forms.Button();
            this.AllLightOn = new System.Windows.Forms.Button();
            this.grpbox_Tower = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Connection = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pnl_ChamA = new System.Windows.Forms.Panel();
            this.lbl_ChamA = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_ChamB = new System.Windows.Forms.Panel();
            this.lbl_ChamB = new System.Windows.Forms.Label();
            this.pnl_ChamC = new System.Windows.Forms.Panel();
            this.lbl_ChamC = new System.Windows.Forms.Label();
            this.pnl_Robot = new System.Windows.Forms.Panel();
            this.lbl_Robot = new System.Windows.Forms.Label();
            this.btn_Auto = new System.Windows.Forms.Button();
            this.btn_Manual = new System.Windows.Forms.Button();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btnErrorTest = new System.Windows.Forms.Button();
            this.btnWarnTest = new System.Windows.Forms.Button();
            this.btn_Setting = new System.Windows.Forms.Button();
            this.SystemLog = new System.Windows.Forms.GroupBox();
            this.LogView = new System.Windows.Forms.ListView();
            this.pnl_BottomContainer = new System.Windows.Forms.Panel();
            this.grpbox_RobotManualControl = new System.Windows.Forms.GroupBox();
            this.pnl_RobotManualControl = new System.Windows.Forms.TableLayoutPanel();
            this.grpbox_AxisJogControl = new System.Windows.Forms.GroupBox();
            this.pnl_AxisJogControl = new System.Windows.Forms.TableLayoutPanel();
            this.btn_MoveLeft = new System.Windows.Forms.Button();
            this.btn_MoveDown = new System.Windows.Forms.Button();
            this.btn_MoveUp = new System.Windows.Forms.Button();
            this.btn_MoveRight = new System.Windows.Forms.Button();
            this.lbl_MoveDistance = new System.Windows.Forms.Label();
            this.nUpDown_MovementDistance = new System.Windows.Forms.NumericUpDown();
            this.grpbox_BasicPoint = new System.Windows.Forms.GroupBox();
            this.pnl_DriverControl = new System.Windows.Forms.TableLayoutPanel();
            this.btn_LRBasic = new System.Windows.Forms.Button();
            this.btn_UDBasic = new System.Windows.Forms.Button();
            this.btn_ServoOFF = new System.Windows.Forms.Button();
            this.btn_ServoON = new System.Windows.Forms.Button();
            this.grpbox_AxisPositionControl = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_LRMove = new System.Windows.Forms.Button();
            this.btn_UDMove = new System.Windows.Forms.Button();
            this.lbl_TargetPosition = new System.Windows.Forms.Label();
            this.pnl_TargetPosition = new System.Windows.Forms.NumericUpDown();
            this.grpbox_Vacuum = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ExOFF = new System.Windows.Forms.Button();
            this.btn_ExON = new System.Windows.Forms.Button();
            this.btn_InOFF = new System.Windows.Forms.Button();
            this.btn_InOn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_UDcurrentPos = new System.Windows.Forms.Label();
            this.lbl_LRcurrentPos = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grpbox_Tower.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel_Connection.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.pnl_ChamA.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.pnl_ChamB.SuspendLayout();
            this.pnl_ChamC.SuspendLayout();
            this.pnl_Robot.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SystemLog.SuspendLayout();
            this.grpbox_RobotManualControl.SuspendLayout();
            this.pnl_RobotManualControl.SuspendLayout();
            this.grpbox_AxisJogControl.SuspendLayout();
            this.pnl_AxisJogControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_MovementDistance)).BeginInit();
            this.grpbox_BasicPoint.SuspendLayout();
            this.pnl_DriverControl.SuspendLayout();
            this.grpbox_AxisPositionControl.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_TargetPosition)).BeginInit();
            this.grpbox_Vacuum.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 61);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 52);
            this.label2.TabIndex = 1;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Connect
            // 
            this.Connect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Connect.Location = new System.Drawing.Point(95, 3);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(87, 55);
            this.Connect.TabIndex = 2;
            this.Connect.Text = "ON";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // DisConnect
            // 
            this.DisConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisConnect.Location = new System.Drawing.Point(95, 64);
            this.DisConnect.Name = "DisConnect";
            this.DisConnect.Size = new System.Drawing.Size(87, 56);
            this.DisConnect.TabIndex = 3;
            this.DisConnect.Text = "OFF";
            this.DisConnect.UseVisualStyleBackColor = true;
            this.DisConnect.Click += new System.EventHandler(this.DisConnect_Click);
            // 
            // YellowLightOn
            // 
            this.YellowLightOn.BackColor = System.Drawing.Color.Yellow;
            this.YellowLightOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YellowLightOn.Location = new System.Drawing.Point(3, 49);
            this.YellowLightOn.Name = "YellowLightOn";
            this.YellowLightOn.Size = new System.Drawing.Size(86, 17);
            this.YellowLightOn.TabIndex = 5;
            this.YellowLightOn.Text = "ON";
            this.YellowLightOn.UseVisualStyleBackColor = false;
            this.YellowLightOn.Click += new System.EventHandler(this.YellowLightOn_Click);
            // 
            // RedLightOn
            // 
            this.RedLightOn.BackColor = System.Drawing.Color.Red;
            this.RedLightOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RedLightOn.Location = new System.Drawing.Point(3, 26);
            this.RedLightOn.Name = "RedLightOn";
            this.RedLightOn.Size = new System.Drawing.Size(86, 17);
            this.RedLightOn.TabIndex = 4;
            this.RedLightOn.Text = "ON";
            this.RedLightOn.UseVisualStyleBackColor = false;
            this.RedLightOn.Click += new System.EventHandler(this.RedLightOn_Click);
            // 
            // YellowLightOff
            // 
            this.YellowLightOff.BackColor = System.Drawing.Color.Yellow;
            this.YellowLightOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YellowLightOff.Location = new System.Drawing.Point(95, 49);
            this.YellowLightOff.Name = "YellowLightOff";
            this.YellowLightOff.Size = new System.Drawing.Size(87, 17);
            this.YellowLightOff.TabIndex = 7;
            this.YellowLightOff.Text = "OFF";
            this.YellowLightOff.UseVisualStyleBackColor = false;
            this.YellowLightOff.Click += new System.EventHandler(this.YellowLightOff_Click);
            // 
            // RedLightOff
            // 
            this.RedLightOff.BackColor = System.Drawing.Color.Red;
            this.RedLightOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RedLightOff.Location = new System.Drawing.Point(95, 26);
            this.RedLightOff.Name = "RedLightOff";
            this.RedLightOff.Size = new System.Drawing.Size(87, 17);
            this.RedLightOff.TabIndex = 6;
            this.RedLightOff.Text = "OFF";
            this.RedLightOff.UseVisualStyleBackColor = false;
            this.RedLightOff.Click += new System.EventHandler(this.RedLightOff_Click);
            // 
            // GreenLightOn
            // 
            this.GreenLightOn.BackColor = System.Drawing.Color.Lime;
            this.GreenLightOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GreenLightOn.Location = new System.Drawing.Point(3, 72);
            this.GreenLightOn.Name = "GreenLightOn";
            this.GreenLightOn.Size = new System.Drawing.Size(86, 20);
            this.GreenLightOn.TabIndex = 8;
            this.GreenLightOn.Text = "ON";
            this.GreenLightOn.UseVisualStyleBackColor = false;
            this.GreenLightOn.Click += new System.EventHandler(this.GreenLightOn_Click);
            // 
            // GreenLightOff
            // 
            this.GreenLightOff.BackColor = System.Drawing.Color.Lime;
            this.GreenLightOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GreenLightOff.Location = new System.Drawing.Point(95, 72);
            this.GreenLightOff.Name = "GreenLightOff";
            this.GreenLightOff.Size = new System.Drawing.Size(87, 20);
            this.GreenLightOff.TabIndex = 9;
            this.GreenLightOff.Text = "OFF";
            this.GreenLightOff.UseVisualStyleBackColor = false;
            this.GreenLightOff.Click += new System.EventHandler(this.GreenLightOff_Click);
            // 
            // AllLightOff
            // 
            this.AllLightOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllLightOff.Location = new System.Drawing.Point(95, 3);
            this.AllLightOff.Name = "AllLightOff";
            this.AllLightOff.Size = new System.Drawing.Size(87, 17);
            this.AllLightOff.TabIndex = 11;
            this.AllLightOff.Text = "ALL OFF";
            this.AllLightOff.UseVisualStyleBackColor = true;
            this.AllLightOff.Click += new System.EventHandler(this.AllLightOff_Click);
            // 
            // AllLightOn
            // 
            this.AllLightOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllLightOn.Location = new System.Drawing.Point(3, 3);
            this.AllLightOn.Name = "AllLightOn";
            this.AllLightOn.Size = new System.Drawing.Size(86, 17);
            this.AllLightOn.TabIndex = 10;
            this.AllLightOn.Text = "ALL ON";
            this.AllLightOn.UseVisualStyleBackColor = true;
            this.AllLightOn.Click += new System.EventHandler(this.AllLightOn_Click);
            // 
            // grpbox_Tower
            // 
            this.grpbox_Tower.Controls.Add(this.tableLayoutPanel4);
            this.grpbox_Tower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_Tower.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grpbox_Tower.Location = new System.Drawing.Point(3, 781);
            this.grpbox_Tower.Name = "grpbox_Tower";
            this.grpbox_Tower.Size = new System.Drawing.Size(191, 115);
            this.grpbox_Tower.TabIndex = 31;
            this.grpbox_Tower.TabStop = false;
            this.grpbox_Tower.Text = "Lamp Tower";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.AllLightOn, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.AllLightOff, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.RedLightOn, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.RedLightOff, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.YellowLightOn, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.YellowLightOff, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.GreenLightOn, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.GreenLightOff, 1, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(185, 95);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.panel_Connection, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.Connect, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.DisConnect, 1, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(185, 123);
            this.tableLayoutPanel5.TabIndex = 32;
            // 
            // panel_Connection
            // 
            this.panel_Connection.Controls.Add(this.label2);
            this.panel_Connection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Connection.Location = new System.Drawing.Point(3, 64);
            this.panel_Connection.Name = "panel_Connection";
            this.panel_Connection.Padding = new System.Windows.Forms.Padding(2);
            this.panel_Connection.Size = new System.Drawing.Size(86, 56);
            this.panel_Connection.TabIndex = 35;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel5);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(3, 53);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(191, 143);
            this.groupBox5.TabIndex = 33;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Connection";
            // 
            // pnl_ChamA
            // 
            this.pnl_ChamA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_ChamA.Controls.Add(this.lbl_ChamA);
            this.pnl_ChamA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_ChamA.Location = new System.Drawing.Point(200, 253);
            this.pnl_ChamA.Name = "pnl_ChamA";
            this.pnl_ChamA.Size = new System.Drawing.Size(191, 194);
            this.pnl_ChamA.TabIndex = 34;
            // 
            // lbl_ChamA
            // 
            this.lbl_ChamA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ChamA.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_ChamA.Location = new System.Drawing.Point(0, 0);
            this.lbl_ChamA.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_ChamA.Name = "lbl_ChamA";
            this.lbl_ChamA.Size = new System.Drawing.Size(189, 192);
            this.lbl_ChamA.TabIndex = 0;
            this.lbl_ChamA.Text = "CHAMBER A";
            this.lbl_ChamA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 5;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.pnl_ChamB, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.pnl_ChamC, 3, 2);
            this.tableLayoutPanel6.Controls.Add(this.pnl_Robot, 2, 2);
            this.tableLayoutPanel6.Controls.Add(this.pnl_ChamA, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.btn_Auto, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.btn_Manual, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.groupBox5, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.btn_Setting, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.SystemLog, 0, 6);
            this.tableLayoutPanel6.Controls.Add(this.pnl_BottomContainer, 1, 5);
            this.tableLayoutPanel6.Controls.Add(this.grpbox_Tower, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.grpbox_RobotManualControl, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.button1, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this.label3, 4, 1);
            this.tableLayoutPanel6.Controls.Add(this.lbl_UDcurrentPos, 4, 2);
            this.tableLayoutPanel6.Controls.Add(this.lbl_LRcurrentPos, 4, 3);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 7;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 203F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(988, 1032);
            this.tableLayoutPanel6.TabIndex = 35;
            // 
            // pnl_ChamB
            // 
            this.pnl_ChamB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_ChamB.Controls.Add(this.lbl_ChamB);
            this.pnl_ChamB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_ChamB.Location = new System.Drawing.Point(397, 53);
            this.pnl_ChamB.Name = "pnl_ChamB";
            this.pnl_ChamB.Size = new System.Drawing.Size(191, 194);
            this.pnl_ChamB.TabIndex = 36;
            // 
            // lbl_ChamB
            // 
            this.lbl_ChamB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ChamB.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_ChamB.Location = new System.Drawing.Point(0, 0);
            this.lbl_ChamB.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_ChamB.Name = "lbl_ChamB";
            this.lbl_ChamB.Size = new System.Drawing.Size(189, 192);
            this.lbl_ChamB.TabIndex = 0;
            this.lbl_ChamB.Text = "CHAMBER B";
            this.lbl_ChamB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_ChamC
            // 
            this.pnl_ChamC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_ChamC.Controls.Add(this.lbl_ChamC);
            this.pnl_ChamC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_ChamC.Location = new System.Drawing.Point(594, 253);
            this.pnl_ChamC.Name = "pnl_ChamC";
            this.pnl_ChamC.Size = new System.Drawing.Size(191, 194);
            this.pnl_ChamC.TabIndex = 36;
            // 
            // lbl_ChamC
            // 
            this.lbl_ChamC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ChamC.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_ChamC.Location = new System.Drawing.Point(0, 0);
            this.lbl_ChamC.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_ChamC.Name = "lbl_ChamC";
            this.lbl_ChamC.Size = new System.Drawing.Size(189, 192);
            this.lbl_ChamC.TabIndex = 0;
            this.lbl_ChamC.Text = "CHAMBER C";
            this.lbl_ChamC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_Robot
            // 
            this.pnl_Robot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Robot.Controls.Add(this.lbl_Robot);
            this.pnl_Robot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Robot.Location = new System.Drawing.Point(397, 253);
            this.pnl_Robot.Name = "pnl_Robot";
            this.pnl_Robot.Size = new System.Drawing.Size(191, 194);
            this.pnl_Robot.TabIndex = 36;
            // 
            // lbl_Robot
            // 
            this.lbl_Robot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Robot.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Robot.Location = new System.Drawing.Point(0, 0);
            this.lbl_Robot.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Robot.Name = "lbl_Robot";
            this.lbl_Robot.Size = new System.Drawing.Size(189, 192);
            this.lbl_Robot.TabIndex = 0;
            this.lbl_Robot.Text = "TRANSFER ROBOT";
            this.lbl_Robot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Auto
            // 
            this.btn_Auto.BackColor = System.Drawing.Color.DarkBlue;
            this.btn_Auto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Auto.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Auto.FlatAppearance.BorderSize = 2;
            this.btn_Auto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Auto.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btn_Auto.ForeColor = System.Drawing.Color.White;
            this.btn_Auto.Location = new System.Drawing.Point(200, 3);
            this.btn_Auto.Name = "btn_Auto";
            this.btn_Auto.Size = new System.Drawing.Size(191, 44);
            this.btn_Auto.TabIndex = 37;
            this.btn_Auto.Text = "AUTO";
            this.btn_Auto.UseVisualStyleBackColor = false;
            this.btn_Auto.Click += new System.EventHandler(this.btn_auto_Click);
            this.btn_Auto.Paint += new System.Windows.Forms.PaintEventHandler(this.btnMode_Paint);
            // 
            // btn_Manual
            // 
            this.btn_Manual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Manual.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Manual.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btn_Manual.ForeColor = System.Drawing.Color.Silver;
            this.btn_Manual.Location = new System.Drawing.Point(397, 3);
            this.btn_Manual.Name = "btn_Manual";
            this.btn_Manual.Size = new System.Drawing.Size(191, 44);
            this.btn_Manual.TabIndex = 38;
            this.btn_Manual.Text = "MANUAL";
            this.btn_Manual.UseVisualStyleBackColor = true;
            this.btn_Manual.Click += new System.EventHandler(this.btn_manual_Click);
            this.btn_Manual.Paint += new System.Windows.Forms.PaintEventHandler(this.btnMode_Paint);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.94737F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.05263F));
            this.tableLayoutPanel7.Controls.Add(this.btnErrorTest, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnWarnTest, 1, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(594, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(190, 34);
            this.tableLayoutPanel7.TabIndex = 42;
            // 
            // btnErrorTest
            // 
            this.btnErrorTest.Location = new System.Drawing.Point(3, 3);
            this.btnErrorTest.Name = "btnErrorTest";
            this.btnErrorTest.Size = new System.Drawing.Size(75, 23);
            this.btnErrorTest.TabIndex = 41;
            this.btnErrorTest.Text = "Error Test";
            this.btnErrorTest.UseVisualStyleBackColor = true;
            this.btnErrorTest.Click += new System.EventHandler(this.btnErrorTest_Click);
            // 
            // btnWarnTest
            // 
            this.btnWarnTest.Location = new System.Drawing.Point(96, 3);
            this.btnWarnTest.Name = "btnWarnTest";
            this.btnWarnTest.Size = new System.Drawing.Size(75, 23);
            this.btnWarnTest.TabIndex = 40;
            this.btnWarnTest.Text = "Warn Test";
            this.btnWarnTest.UseVisualStyleBackColor = true;
            this.btnWarnTest.Click += new System.EventHandler(this.btnWarnTest_Click);
            // 
            // btn_Setting
            // 
            this.btn_Setting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Setting.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Setting.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold);
            this.btn_Setting.ForeColor = System.Drawing.Color.Black;
            this.btn_Setting.Location = new System.Drawing.Point(3, 3);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Size = new System.Drawing.Size(191, 44);
            this.btn_Setting.TabIndex = 44;
            this.btn_Setting.Text = "SETTING";
            this.btn_Setting.UseVisualStyleBackColor = true;
            this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
            // 
            // SystemLog
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.SystemLog, 4);
            this.SystemLog.Controls.Add(this.LogView);
            this.SystemLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SystemLog.Location = new System.Drawing.Point(3, 902);
            this.SystemLog.Name = "SystemLog";
            this.SystemLog.Size = new System.Drawing.Size(782, 127);
            this.SystemLog.TabIndex = 39;
            this.SystemLog.TabStop = false;
            this.SystemLog.Text = "SystemLog";
            // 
            // LogView
            // 
            this.LogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogView.FullRowSelect = true;
            this.LogView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LogView.HideSelection = false;
            this.LogView.Location = new System.Drawing.Point(3, 17);
            this.LogView.Name = "LogView";
            this.LogView.Size = new System.Drawing.Size(776, 107);
            this.LogView.TabIndex = 0;
            this.LogView.UseCompatibleStateImageBehavior = false;
            this.LogView.View = System.Windows.Forms.View.Details;
            // 
            // pnl_BottomContainer
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.pnl_BottomContainer, 3);
            this.pnl_BottomContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_BottomContainer.Location = new System.Drawing.Point(200, 781);
            this.pnl_BottomContainer.Name = "pnl_BottomContainer";
            this.pnl_BottomContainer.Size = new System.Drawing.Size(585, 115);
            this.pnl_BottomContainer.TabIndex = 45;
            // 
            // grpbox_RobotManualControl
            // 
            this.tableLayoutPanel6.SetColumnSpan(this.grpbox_RobotManualControl, 4);
            this.grpbox_RobotManualControl.Controls.Add(this.pnl_RobotManualControl);
            this.grpbox_RobotManualControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_RobotManualControl.Location = new System.Drawing.Point(3, 578);
            this.grpbox_RobotManualControl.Name = "grpbox_RobotManualControl";
            this.grpbox_RobotManualControl.Size = new System.Drawing.Size(782, 197);
            this.grpbox_RobotManualControl.TabIndex = 46;
            this.grpbox_RobotManualControl.TabStop = false;
            this.grpbox_RobotManualControl.Text = "Robot Manual Control";
            // 
            // pnl_RobotManualControl
            // 
            this.pnl_RobotManualControl.ColumnCount = 4;
            this.pnl_RobotManualControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_RobotManualControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_RobotManualControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_RobotManualControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_RobotManualControl.Controls.Add(this.grpbox_Vacuum, 3, 0);
            this.pnl_RobotManualControl.Controls.Add(this.grpbox_AxisPositionControl, 2, 0);
            this.pnl_RobotManualControl.Controls.Add(this.grpbox_AxisJogControl, 1, 0);
            this.pnl_RobotManualControl.Controls.Add(this.grpbox_BasicPoint, 0, 0);
            this.pnl_RobotManualControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_RobotManualControl.Location = new System.Drawing.Point(3, 17);
            this.pnl_RobotManualControl.Name = "pnl_RobotManualControl";
            this.pnl_RobotManualControl.RowCount = 1;
            this.pnl_RobotManualControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_RobotManualControl.Size = new System.Drawing.Size(776, 177);
            this.pnl_RobotManualControl.TabIndex = 0;
            // 
            // grpbox_AxisJogControl
            // 
            this.grpbox_AxisJogControl.Controls.Add(this.pnl_AxisJogControl);
            this.grpbox_AxisJogControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_AxisJogControl.Location = new System.Drawing.Point(197, 3);
            this.grpbox_AxisJogControl.Name = "grpbox_AxisJogControl";
            this.grpbox_AxisJogControl.Size = new System.Drawing.Size(188, 171);
            this.grpbox_AxisJogControl.TabIndex = 1;
            this.grpbox_AxisJogControl.TabStop = false;
            this.grpbox_AxisJogControl.Text = "ROBOT Axis Jog Control";
            // 
            // pnl_AxisJogControl
            // 
            this.pnl_AxisJogControl.ColumnCount = 2;
            this.pnl_AxisJogControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_AxisJogControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_AxisJogControl.Controls.Add(this.btn_MoveLeft, 0, 2);
            this.pnl_AxisJogControl.Controls.Add(this.btn_MoveDown, 1, 1);
            this.pnl_AxisJogControl.Controls.Add(this.btn_MoveUp, 0, 1);
            this.pnl_AxisJogControl.Controls.Add(this.btn_MoveRight, 1, 2);
            this.pnl_AxisJogControl.Controls.Add(this.lbl_MoveDistance, 0, 0);
            this.pnl_AxisJogControl.Controls.Add(this.nUpDown_MovementDistance, 1, 0);
            this.pnl_AxisJogControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_AxisJogControl.Location = new System.Drawing.Point(3, 17);
            this.pnl_AxisJogControl.Name = "pnl_AxisJogControl";
            this.pnl_AxisJogControl.RowCount = 3;
            this.pnl_AxisJogControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.pnl_AxisJogControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.pnl_AxisJogControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.pnl_AxisJogControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnl_AxisJogControl.Size = new System.Drawing.Size(182, 151);
            this.pnl_AxisJogControl.TabIndex = 0;
            // 
            // btn_MoveLeft
            // 
            this.btn_MoveLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MoveLeft.Location = new System.Drawing.Point(3, 98);
            this.btn_MoveLeft.Name = "btn_MoveLeft";
            this.btn_MoveLeft.Size = new System.Drawing.Size(85, 50);
            this.btn_MoveLeft.TabIndex = 2;
            this.btn_MoveLeft.Text = "Jog Move ←";
            this.btn_MoveLeft.UseVisualStyleBackColor = true;
            this.btn_MoveLeft.Click += new System.EventHandler(this.btn_MoveLeft_Click);
            // 
            // btn_MoveDown
            // 
            this.btn_MoveDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MoveDown.Location = new System.Drawing.Point(94, 44);
            this.btn_MoveDown.Name = "btn_MoveDown";
            this.btn_MoveDown.Size = new System.Drawing.Size(85, 48);
            this.btn_MoveDown.TabIndex = 1;
            this.btn_MoveDown.Text = "Jog Move ↓";
            this.btn_MoveDown.UseVisualStyleBackColor = true;
            this.btn_MoveDown.Click += new System.EventHandler(this.btn_MoveDown_Click);
            // 
            // btn_MoveUp
            // 
            this.btn_MoveUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MoveUp.Location = new System.Drawing.Point(3, 44);
            this.btn_MoveUp.Name = "btn_MoveUp";
            this.btn_MoveUp.Size = new System.Drawing.Size(85, 48);
            this.btn_MoveUp.TabIndex = 0;
            this.btn_MoveUp.Text = "Jog Move ↑";
            this.btn_MoveUp.UseVisualStyleBackColor = true;
            this.btn_MoveUp.Click += new System.EventHandler(this.btn_MoveUp_Click);
            // 
            // btn_MoveRight
            // 
            this.btn_MoveRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MoveRight.Location = new System.Drawing.Point(94, 98);
            this.btn_MoveRight.Name = "btn_MoveRight";
            this.btn_MoveRight.Size = new System.Drawing.Size(85, 50);
            this.btn_MoveRight.TabIndex = 3;
            this.btn_MoveRight.Text = "Jog Move →";
            this.btn_MoveRight.UseVisualStyleBackColor = true;
            this.btn_MoveRight.Click += new System.EventHandler(this.btn_MoveRight_Click);
            // 
            // lbl_MoveDistance
            // 
            this.lbl_MoveDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_MoveDistance.Location = new System.Drawing.Point(3, 0);
            this.lbl_MoveDistance.Name = "lbl_MoveDistance";
            this.lbl_MoveDistance.Size = new System.Drawing.Size(85, 41);
            this.lbl_MoveDistance.TabIndex = 4;
            this.lbl_MoveDistance.Text = "Movement Distance";
            this.lbl_MoveDistance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nUpDown_MovementDistance
            // 
            this.nUpDown_MovementDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nUpDown_MovementDistance.Location = new System.Drawing.Point(91, 13);
            this.nUpDown_MovementDistance.Margin = new System.Windows.Forms.Padding(0, 13, 0, 13);
            this.nUpDown_MovementDistance.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nUpDown_MovementDistance.Name = "nUpDown_MovementDistance";
            this.nUpDown_MovementDistance.Size = new System.Drawing.Size(91, 21);
            this.nUpDown_MovementDistance.TabIndex = 5;
            this.nUpDown_MovementDistance.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // grpbox_BasicPoint
            // 
            this.grpbox_BasicPoint.Controls.Add(this.pnl_DriverControl);
            this.grpbox_BasicPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_BasicPoint.Location = new System.Drawing.Point(3, 3);
            this.grpbox_BasicPoint.Name = "grpbox_BasicPoint";
            this.grpbox_BasicPoint.Size = new System.Drawing.Size(188, 171);
            this.grpbox_BasicPoint.TabIndex = 0;
            this.grpbox_BasicPoint.TabStop = false;
            this.grpbox_BasicPoint.Text = "ROBOT Driver Control";
            // 
            // pnl_DriverControl
            // 
            this.pnl_DriverControl.ColumnCount = 2;
            this.pnl_DriverControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_DriverControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_DriverControl.Controls.Add(this.btn_LRBasic, 1, 1);
            this.pnl_DriverControl.Controls.Add(this.btn_UDBasic, 0, 1);
            this.pnl_DriverControl.Controls.Add(this.btn_ServoOFF, 1, 0);
            this.pnl_DriverControl.Controls.Add(this.btn_ServoON, 0, 0);
            this.pnl_DriverControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_DriverControl.Location = new System.Drawing.Point(3, 17);
            this.pnl_DriverControl.Name = "pnl_DriverControl";
            this.pnl_DriverControl.RowCount = 2;
            this.pnl_DriverControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_DriverControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_DriverControl.Size = new System.Drawing.Size(182, 151);
            this.pnl_DriverControl.TabIndex = 0;
            // 
            // btn_LRBasic
            // 
            this.btn_LRBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_LRBasic.Location = new System.Drawing.Point(94, 78);
            this.btn_LRBasic.Name = "btn_LRBasic";
            this.btn_LRBasic.Size = new System.Drawing.Size(85, 70);
            this.btn_LRBasic.TabIndex = 3;
            this.btn_LRBasic.Text = "LR Basic Point";
            this.btn_LRBasic.UseVisualStyleBackColor = true;
            this.btn_LRBasic.Click += new System.EventHandler(this.btn_LRBasic_Click);
            // 
            // btn_UDBasic
            // 
            this.btn_UDBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_UDBasic.Location = new System.Drawing.Point(3, 78);
            this.btn_UDBasic.Name = "btn_UDBasic";
            this.btn_UDBasic.Size = new System.Drawing.Size(85, 70);
            this.btn_UDBasic.TabIndex = 2;
            this.btn_UDBasic.Text = "UD Basic Point";
            this.btn_UDBasic.UseVisualStyleBackColor = true;
            this.btn_UDBasic.Click += new System.EventHandler(this.btn_UDBasic_Click);
            // 
            // btn_ServoOFF
            // 
            this.btn_ServoOFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ServoOFF.Location = new System.Drawing.Point(94, 3);
            this.btn_ServoOFF.Name = "btn_ServoOFF";
            this.btn_ServoOFF.Size = new System.Drawing.Size(85, 69);
            this.btn_ServoOFF.TabIndex = 1;
            this.btn_ServoOFF.Text = "Servo OFF";
            this.btn_ServoOFF.UseVisualStyleBackColor = true;
            this.btn_ServoOFF.Click += new System.EventHandler(this.btn_ServoOFF_Click);
            // 
            // btn_ServoON
            // 
            this.btn_ServoON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ServoON.Location = new System.Drawing.Point(3, 3);
            this.btn_ServoON.Name = "btn_ServoON";
            this.btn_ServoON.Size = new System.Drawing.Size(85, 69);
            this.btn_ServoON.TabIndex = 0;
            this.btn_ServoON.Text = "Servo ON";
            this.btn_ServoON.UseVisualStyleBackColor = true;
            this.btn_ServoON.Click += new System.EventHandler(this.btn_ServoON_Click);
            // 
            // grpbox_AxisPositionControl
            // 
            this.grpbox_AxisPositionControl.Controls.Add(this.tableLayoutPanel1);
            this.grpbox_AxisPositionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_AxisPositionControl.Location = new System.Drawing.Point(391, 3);
            this.grpbox_AxisPositionControl.Name = "grpbox_AxisPositionControl";
            this.grpbox_AxisPositionControl.Size = new System.Drawing.Size(188, 171);
            this.grpbox_AxisPositionControl.TabIndex = 1;
            this.grpbox_AxisPositionControl.TabStop = false;
            this.grpbox_AxisPositionControl.Text = "ROBOT Axis Position Control";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btn_LRMove, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_UDMove, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_TargetPosition, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl_TargetPosition, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(182, 151);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btn_LRMove
            // 
            this.btn_LRMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_LRMove.Location = new System.Drawing.Point(94, 78);
            this.btn_LRMove.Name = "btn_LRMove";
            this.btn_LRMove.Size = new System.Drawing.Size(85, 70);
            this.btn_LRMove.TabIndex = 3;
            this.btn_LRMove.Text = "LR Move";
            this.btn_LRMove.UseVisualStyleBackColor = true;
            this.btn_LRMove.Click += new System.EventHandler(this.btn_LRMove_Click);
            // 
            // btn_UDMove
            // 
            this.btn_UDMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_UDMove.Location = new System.Drawing.Point(3, 78);
            this.btn_UDMove.Name = "btn_UDMove";
            this.btn_UDMove.Size = new System.Drawing.Size(85, 70);
            this.btn_UDMove.TabIndex = 2;
            this.btn_UDMove.Text = "UD Move";
            this.btn_UDMove.UseVisualStyleBackColor = true;
            this.btn_UDMove.Click += new System.EventHandler(this.btn_UDMove_Click);
            // 
            // lbl_TargetPosition
            // 
            this.lbl_TargetPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TargetPosition.Location = new System.Drawing.Point(3, 0);
            this.lbl_TargetPosition.Name = "lbl_TargetPosition";
            this.lbl_TargetPosition.Size = new System.Drawing.Size(85, 75);
            this.lbl_TargetPosition.TabIndex = 6;
            this.lbl_TargetPosition.Text = "Target Position";
            this.lbl_TargetPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_TargetPosition
            // 
            this.pnl_TargetPosition.Location = new System.Drawing.Point(91, 30);
            this.pnl_TargetPosition.Margin = new System.Windows.Forms.Padding(0, 30, 0, 30);
            this.pnl_TargetPosition.Name = "pnl_TargetPosition";
            this.pnl_TargetPosition.Size = new System.Drawing.Size(91, 21);
            this.pnl_TargetPosition.TabIndex = 6;
            // 
            // grpbox_Vacuum
            // 
            this.grpbox_Vacuum.Controls.Add(this.tableLayoutPanel2);
            this.grpbox_Vacuum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_Vacuum.Location = new System.Drawing.Point(585, 3);
            this.grpbox_Vacuum.Name = "grpbox_Vacuum";
            this.grpbox_Vacuum.Size = new System.Drawing.Size(188, 171);
            this.grpbox_Vacuum.TabIndex = 1;
            this.grpbox_Vacuum.TabStop = false;
            this.grpbox_Vacuum.Text = "ROBOT Vacuum Control";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btn_ExOFF, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_ExON, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_InOFF, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_InOn, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(182, 151);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btn_ExOFF
            // 
            this.btn_ExOFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ExOFF.Location = new System.Drawing.Point(94, 78);
            this.btn_ExOFF.Name = "btn_ExOFF";
            this.btn_ExOFF.Size = new System.Drawing.Size(85, 70);
            this.btn_ExOFF.TabIndex = 3;
            this.btn_ExOFF.Text = "Exhaust OFF";
            this.btn_ExOFF.UseVisualStyleBackColor = true;
            this.btn_ExOFF.Click += new System.EventHandler(this.btn_ExOFF_Click);
            // 
            // btn_ExON
            // 
            this.btn_ExON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ExON.Location = new System.Drawing.Point(3, 78);
            this.btn_ExON.Name = "btn_ExON";
            this.btn_ExON.Size = new System.Drawing.Size(85, 70);
            this.btn_ExON.TabIndex = 2;
            this.btn_ExON.Text = "Exhaust  ON";
            this.btn_ExON.UseVisualStyleBackColor = true;
            this.btn_ExON.Click += new System.EventHandler(this.btn_ExON_Click);
            // 
            // btn_InOFF
            // 
            this.btn_InOFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_InOFF.Location = new System.Drawing.Point(94, 3);
            this.btn_InOFF.Name = "btn_InOFF";
            this.btn_InOFF.Size = new System.Drawing.Size(85, 69);
            this.btn_InOFF.TabIndex = 1;
            this.btn_InOFF.Text = "Inhalation OFF";
            this.btn_InOFF.UseVisualStyleBackColor = true;
            this.btn_InOFF.Click += new System.EventHandler(this.btn_InOFF_Click);
            // 
            // btn_InOn
            // 
            this.btn_InOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_InOn.Location = new System.Drawing.Point(3, 3);
            this.btn_InOn.Name = "btn_InOn";
            this.btn_InOn.Size = new System.Drawing.Size(85, 69);
            this.btn_InOn.TabIndex = 0;
            this.btn_InOn.Text = "Inhalation ON";
            this.btn_InOn.UseVisualStyleBackColor = true;
            this.btn_InOn.Click += new System.EventHandler(this.btn_InOn_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(791, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 47;
            this.label3.Text = "1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(791, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 48;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lbl_UDcurrentPos
            // 
            this.lbl_UDcurrentPos.Location = new System.Drawing.Point(791, 250);
            this.lbl_UDcurrentPos.Name = "lbl_UDcurrentPos";
            this.lbl_UDcurrentPos.Size = new System.Drawing.Size(100, 23);
            this.lbl_UDcurrentPos.TabIndex = 49;
            // 
            // lbl_LRcurrentPos
            // 
            this.lbl_LRcurrentPos.Location = new System.Drawing.Point(791, 450);
            this.lbl_LRcurrentPos.Name = "lbl_LRcurrentPos";
            this.lbl_LRcurrentPos.Size = new System.Drawing.Size(100, 23);
            this.lbl_LRcurrentPos.TabIndex = 50;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 1032);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Name = "MainGUI";
            this.Text = "Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.grpbox_Tower.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel_Connection.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.pnl_ChamA.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.pnl_ChamB.ResumeLayout(false);
            this.pnl_ChamC.ResumeLayout(false);
            this.pnl_Robot.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.SystemLog.ResumeLayout(false);
            this.grpbox_RobotManualControl.ResumeLayout(false);
            this.pnl_RobotManualControl.ResumeLayout(false);
            this.grpbox_AxisJogControl.ResumeLayout(false);
            this.pnl_AxisJogControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_MovementDistance)).EndInit();
            this.grpbox_BasicPoint.ResumeLayout(false);
            this.pnl_DriverControl.ResumeLayout(false);
            this.grpbox_AxisPositionControl.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_TargetPosition)).EndInit();
            this.grpbox_Vacuum.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button Connect;
        internal System.Windows.Forms.Button DisConnect;
        internal System.Windows.Forms.Button YellowLightOn;
        internal System.Windows.Forms.Button RedLightOn;
        internal System.Windows.Forms.Button YellowLightOff;
        internal System.Windows.Forms.Button RedLightOff;
        internal System.Windows.Forms.Button GreenLightOn;
        internal System.Windows.Forms.Button GreenLightOff;
        internal System.Windows.Forms.Button AllLightOff;
        internal System.Windows.Forms.Button AllLightOn;
        internal System.Windows.Forms.GroupBox grpbox_Tower;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        internal System.Windows.Forms.GroupBox groupBox5;
        internal System.Windows.Forms.Panel panel_Connection;
        internal System.Windows.Forms.Panel pnl_ChamA;
        internal System.Windows.Forms.Label lbl_ChamA;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        internal System.Windows.Forms.Panel pnl_ChamB;
        internal System.Windows.Forms.Label lbl_ChamB;
        internal System.Windows.Forms.Panel pnl_Robot;
        internal System.Windows.Forms.Label lbl_Robot;
        internal System.Windows.Forms.Panel pnl_ChamC;
        internal System.Windows.Forms.Label lbl_ChamC;
        internal System.Windows.Forms.Button btn_Auto;
        internal System.Windows.Forms.Button btn_Manual;
        internal System.Windows.Forms.GroupBox SystemLog;
        internal System.Windows.Forms.ListView LogView;
        internal System.Windows.Forms.Button btnErrorTest;
        internal System.Windows.Forms.Button btnWarnTest;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        internal System.Windows.Forms.Button btn_Setting;
        internal System.Windows.Forms.Panel pnl_BottomContainer;
        internal System.Windows.Forms.GroupBox grpbox_RobotManualControl;
        internal System.Windows.Forms.TableLayoutPanel pnl_RobotManualControl;
        internal System.Windows.Forms.GroupBox grpbox_BasicPoint;
        internal System.Windows.Forms.TableLayoutPanel pnl_DriverControl;
        internal System.Windows.Forms.Button btn_LRBasic;
        internal System.Windows.Forms.Button btn_UDBasic;
        internal System.Windows.Forms.Button btn_ServoOFF;
        internal System.Windows.Forms.Button btn_ServoON;
        internal System.Windows.Forms.GroupBox grpbox_AxisJogControl;
        internal System.Windows.Forms.TableLayoutPanel pnl_AxisJogControl;
        internal System.Windows.Forms.Button btn_MoveRight;
        internal System.Windows.Forms.Button btn_MoveLeft;
        internal System.Windows.Forms.Button btn_MoveDown;
        internal System.Windows.Forms.Button btn_MoveUp;
        internal System.Windows.Forms.Label lbl_MoveDistance;
        internal System.Windows.Forms.NumericUpDown nUpDown_MovementDistance;
        internal System.Windows.Forms.GroupBox grpbox_AxisPositionControl;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.Button btn_LRMove;
        internal System.Windows.Forms.Button btn_UDMove;
        internal System.Windows.Forms.Label lbl_TargetPosition;
        internal System.Windows.Forms.NumericUpDown pnl_TargetPosition;
        internal System.Windows.Forms.GroupBox grpbox_Vacuum;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        internal System.Windows.Forms.Button btn_ExOFF;
        internal System.Windows.Forms.Button btn_ExON;
        internal System.Windows.Forms.Button btn_InOFF;
        internal System.Windows.Forms.Button btn_InOn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_UDcurrentPos;
        private System.Windows.Forms.Label lbl_LRcurrentPos;
        private System.Windows.Forms.Timer timer1;
    }
}

