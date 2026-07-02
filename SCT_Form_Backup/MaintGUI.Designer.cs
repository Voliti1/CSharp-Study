namespace SCT_Form
{
    partial class MaintGUI
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
            this.pnl_ProcessManualControl = new System.Windows.Forms.TableLayoutPanel();
            this.grpbox_Cham_A_Manual = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_Cham_A_Lamp = new System.Windows.Forms.Panel();
            this.lbl_Cham_A_Lamp = new System.Windows.Forms.Label();
            this.pnl_Cham_A_Door = new System.Windows.Forms.Panel();
            this.lbl_Cham_A_Door = new System.Windows.Forms.Label();
            this.btn_Cham_A_Door_CLOSE = new System.Windows.Forms.Button();
            this.btn_Cham_A_Lamp_OFF = new System.Windows.Forms.Button();
            this.btn_Cham_A_Lamp_ON = new System.Windows.Forms.Button();
            this.btn_Cham_A_Door_OPEN = new System.Windows.Forms.Button();
            this.grpbox_Tower = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.AllLightOn = new System.Windows.Forms.Button();
            this.AllLightOff = new System.Windows.Forms.Button();
            this.RedLightOn = new System.Windows.Forms.Button();
            this.RedLightOff = new System.Windows.Forms.Button();
            this.YellowLightOn = new System.Windows.Forms.Button();
            this.YellowLightOff = new System.Windows.Forms.Button();
            this.GreenLightOn = new System.Windows.Forms.Button();
            this.GreenLightOff = new System.Windows.Forms.Button();
            this.grpbox_Cham_B_Manual = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_Cham_B_Lamp = new System.Windows.Forms.Panel();
            this.lbl_Cham_B_Lamp = new System.Windows.Forms.Label();
            this.btn_Cham_B_Door_CLOSE = new System.Windows.Forms.Button();
            this.btn_Cham_B_Lamp_OFF = new System.Windows.Forms.Button();
            this.pnl_Cham_B_Door = new System.Windows.Forms.Panel();
            this.lbl_Cham_B_Door = new System.Windows.Forms.Label();
            this.btn_Cham_B_Lamp_ON = new System.Windows.Forms.Button();
            this.btn_Cham_B_Door_OPEN = new System.Windows.Forms.Button();
            this.grpbox_Cham_C_Manual = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_Cham_C_Lamp = new System.Windows.Forms.Panel();
            this.lbl_Cham_C_Lamp = new System.Windows.Forms.Label();
            this.btn_Cham_C_Door_CLOSE = new System.Windows.Forms.Button();
            this.btn_Cham_C_Lamp_OFF = new System.Windows.Forms.Button();
            this.pnl_Cham_C_Door = new System.Windows.Forms.Panel();
            this.lbl_Cham_C_Door = new System.Windows.Forms.Label();
            this.btn_Cham_C_Lamp_ON = new System.Windows.Forms.Button();
            this.btn_Cham_C_Door_OPEN = new System.Windows.Forms.Button();
            this.grpbox_RobotManualControl = new System.Windows.Forms.GroupBox();
            this.pnl_RobotManualControl = new System.Windows.Forms.TableLayoutPanel();
            this.grpbox_Vacuum = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ExOFF = new System.Windows.Forms.Button();
            this.btn_ExON = new System.Windows.Forms.Button();
            this.btn_InOFF = new System.Windows.Forms.Button();
            this.btn_InOn = new System.Windows.Forms.Button();
            this.grpbox_AxisPositionControl = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_LRMove = new System.Windows.Forms.Button();
            this.btn_UDMove = new System.Windows.Forms.Button();
            this.lbl_TargetPosition = new System.Windows.Forms.Label();
            this.pnl_TargetPosition = new System.Windows.Forms.NumericUpDown();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpbox_currentPos = new System.Windows.Forms.GroupBox();
            this.pnl_AxisStatus = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_LRcurrentPos = new System.Windows.Forms.Label();
            this.lbl_UD = new System.Windows.Forms.Label();
            this.lbl_LR = new System.Windows.Forms.Label();
            this.lbl_UDcurrentPos = new System.Windows.Forms.Label();
            this.grpbox_RobotSylinder = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_moveBack = new System.Windows.Forms.Button();
            this.btn_moveFront = new System.Windows.Forms.Button();
            this.test = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_ProcessManualControl.SuspendLayout();
            this.grpbox_Cham_A_Manual.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnl_Cham_A_Lamp.SuspendLayout();
            this.pnl_Cham_A_Door.SuspendLayout();
            this.grpbox_Tower.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.grpbox_Cham_B_Manual.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnl_Cham_B_Lamp.SuspendLayout();
            this.pnl_Cham_B_Door.SuspendLayout();
            this.grpbox_Cham_C_Manual.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.pnl_Cham_C_Lamp.SuspendLayout();
            this.pnl_Cham_C_Door.SuspendLayout();
            this.grpbox_RobotManualControl.SuspendLayout();
            this.pnl_RobotManualControl.SuspendLayout();
            this.grpbox_Vacuum.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.grpbox_AxisPositionControl.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_TargetPosition)).BeginInit();
            this.grpbox_AxisJogControl.SuspendLayout();
            this.pnl_AxisJogControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_MovementDistance)).BeginInit();
            this.grpbox_BasicPoint.SuspendLayout();
            this.pnl_DriverControl.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpbox_currentPos.SuspendLayout();
            this.pnl_AxisStatus.SuspendLayout();
            this.grpbox_RobotSylinder.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.test.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ProcessManualControl
            // 
            this.pnl_ProcessManualControl.ColumnCount = 4;
            this.pnl_ProcessManualControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_ProcessManualControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_ProcessManualControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_ProcessManualControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_ProcessManualControl.Controls.Add(this.grpbox_Cham_A_Manual, 0, 0);
            this.pnl_ProcessManualControl.Controls.Add(this.grpbox_Tower, 3, 0);
            this.pnl_ProcessManualControl.Controls.Add(this.grpbox_Cham_B_Manual, 1, 0);
            this.pnl_ProcessManualControl.Controls.Add(this.grpbox_Cham_C_Manual, 2, 0);
            this.pnl_ProcessManualControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_ProcessManualControl.Location = new System.Drawing.Point(3, 17);
            this.pnl_ProcessManualControl.Name = "pnl_ProcessManualControl";
            this.pnl_ProcessManualControl.RowCount = 1;
            this.pnl_ProcessManualControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_ProcessManualControl.Size = new System.Drawing.Size(760, 178);
            this.pnl_ProcessManualControl.TabIndex = 49;
            // 
            // grpbox_Cham_A_Manual
            // 
            this.grpbox_Cham_A_Manual.Controls.Add(this.tableLayoutPanel1);
            this.grpbox_Cham_A_Manual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_Cham_A_Manual.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grpbox_Cham_A_Manual.Location = new System.Drawing.Point(3, 3);
            this.grpbox_Cham_A_Manual.Name = "grpbox_Cham_A_Manual";
            this.grpbox_Cham_A_Manual.Size = new System.Drawing.Size(184, 172);
            this.grpbox_Cham_A_Manual.TabIndex = 27;
            this.grpbox_Cham_A_Manual.TabStop = false;
            this.grpbox_Cham_A_Manual.Text = "Chamber A Manual Control";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.pnl_Cham_A_Lamp, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnl_Cham_A_Door, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Cham_A_Door_CLOSE, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_Cham_A_Lamp_OFF, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Cham_A_Lamp_ON, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_Cham_A_Door_OPEN, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(178, 152);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // pnl_Cham_A_Lamp
            // 
            this.pnl_Cham_A_Lamp.Controls.Add(this.lbl_Cham_A_Lamp);
            this.pnl_Cham_A_Lamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Cham_A_Lamp.Location = new System.Drawing.Point(3, 79);
            this.pnl_Cham_A_Lamp.Name = "pnl_Cham_A_Lamp";
            this.pnl_Cham_A_Lamp.Padding = new System.Windows.Forms.Padding(2);
            this.pnl_Cham_A_Lamp.Size = new System.Drawing.Size(53, 70);
            this.pnl_Cham_A_Lamp.TabIndex = 35;
            // 
            // lbl_Cham_A_Lamp
            // 
            this.lbl_Cham_A_Lamp.BackColor = System.Drawing.Color.White;
            this.lbl_Cham_A_Lamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Cham_A_Lamp.Location = new System.Drawing.Point(2, 2);
            this.lbl_Cham_A_Lamp.Name = "lbl_Cham_A_Lamp";
            this.lbl_Cham_A_Lamp.Size = new System.Drawing.Size(49, 66);
            this.lbl_Cham_A_Lamp.TabIndex = 25;
            this.lbl_Cham_A_Lamp.Text = "LAMP";
            this.lbl_Cham_A_Lamp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_Cham_A_Door
            // 
            this.pnl_Cham_A_Door.Controls.Add(this.lbl_Cham_A_Door);
            this.pnl_Cham_A_Door.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Cham_A_Door.Location = new System.Drawing.Point(3, 3);
            this.pnl_Cham_A_Door.Name = "pnl_Cham_A_Door";
            this.pnl_Cham_A_Door.Padding = new System.Windows.Forms.Padding(2);
            this.pnl_Cham_A_Door.Size = new System.Drawing.Size(53, 70);
            this.pnl_Cham_A_Door.TabIndex = 34;
            // 
            // lbl_Cham_A_Door
            // 
            this.lbl_Cham_A_Door.BackColor = System.Drawing.Color.White;
            this.lbl_Cham_A_Door.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Cham_A_Door.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Cham_A_Door.Location = new System.Drawing.Point(2, 2);
            this.lbl_Cham_A_Door.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Cham_A_Door.Name = "lbl_Cham_A_Door";
            this.lbl_Cham_A_Door.Size = new System.Drawing.Size(49, 66);
            this.lbl_Cham_A_Door.TabIndex = 26;
            this.lbl_Cham_A_Door.Text = "DOOR";
            this.lbl_Cham_A_Door.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Cham_A_Door_CLOSE
            // 
            this.btn_Cham_A_Door_CLOSE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_A_Door_CLOSE.Location = new System.Drawing.Point(121, 3);
            this.btn_Cham_A_Door_CLOSE.Name = "btn_Cham_A_Door_CLOSE";
            this.btn_Cham_A_Door_CLOSE.Size = new System.Drawing.Size(54, 70);
            this.btn_Cham_A_Door_CLOSE.TabIndex = 12;
            this.btn_Cham_A_Door_CLOSE.Text = "CLOSE";
            this.btn_Cham_A_Door_CLOSE.UseVisualStyleBackColor = true;
            this.btn_Cham_A_Door_CLOSE.Click += new System.EventHandler(this.btn_Cham_A_Door_CLOSE_Click);
            // 
            // btn_Cham_A_Lamp_OFF
            // 
            this.btn_Cham_A_Lamp_OFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_A_Lamp_OFF.Location = new System.Drawing.Point(121, 79);
            this.btn_Cham_A_Lamp_OFF.Name = "btn_Cham_A_Lamp_OFF";
            this.btn_Cham_A_Lamp_OFF.Size = new System.Drawing.Size(54, 70);
            this.btn_Cham_A_Lamp_OFF.TabIndex = 24;
            this.btn_Cham_A_Lamp_OFF.Text = "OFF";
            this.btn_Cham_A_Lamp_OFF.UseVisualStyleBackColor = true;
            this.btn_Cham_A_Lamp_OFF.Click += new System.EventHandler(this.btn_Cham_A_Lamp_OFF_Click);
            // 
            // btn_Cham_A_Lamp_ON
            // 
            this.btn_Cham_A_Lamp_ON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_A_Lamp_ON.Location = new System.Drawing.Point(62, 79);
            this.btn_Cham_A_Lamp_ON.Name = "btn_Cham_A_Lamp_ON";
            this.btn_Cham_A_Lamp_ON.Size = new System.Drawing.Size(53, 70);
            this.btn_Cham_A_Lamp_ON.TabIndex = 15;
            this.btn_Cham_A_Lamp_ON.Text = "ON";
            this.btn_Cham_A_Lamp_ON.UseVisualStyleBackColor = true;
            this.btn_Cham_A_Lamp_ON.Click += new System.EventHandler(this.btn_Cham_A_Lamp_ON_Click);
            // 
            // btn_Cham_A_Door_OPEN
            // 
            this.btn_Cham_A_Door_OPEN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_A_Door_OPEN.Location = new System.Drawing.Point(62, 3);
            this.btn_Cham_A_Door_OPEN.Name = "btn_Cham_A_Door_OPEN";
            this.btn_Cham_A_Door_OPEN.Size = new System.Drawing.Size(53, 70);
            this.btn_Cham_A_Door_OPEN.TabIndex = 21;
            this.btn_Cham_A_Door_OPEN.Text = "OPEN";
            this.btn_Cham_A_Door_OPEN.UseVisualStyleBackColor = true;
            this.btn_Cham_A_Door_OPEN.Click += new System.EventHandler(this.btn_Cham_A_Door_OPEN_Click);
            // 
            // grpbox_Tower
            // 
            this.grpbox_Tower.Controls.Add(this.tableLayoutPanel6);
            this.grpbox_Tower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_Tower.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grpbox_Tower.Location = new System.Drawing.Point(573, 3);
            this.grpbox_Tower.Name = "grpbox_Tower";
            this.grpbox_Tower.Size = new System.Drawing.Size(184, 172);
            this.grpbox_Tower.TabIndex = 51;
            this.grpbox_Tower.TabStop = false;
            this.grpbox_Tower.Text = "Lamp Tower";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.AllLightOn, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.AllLightOff, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.RedLightOn, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.RedLightOff, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.YellowLightOn, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.YellowLightOff, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.GreenLightOn, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.GreenLightOff, 1, 3);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(178, 152);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // AllLightOn
            // 
            this.AllLightOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllLightOn.Location = new System.Drawing.Point(3, 3);
            this.AllLightOn.Name = "AllLightOn";
            this.AllLightOn.Size = new System.Drawing.Size(83, 32);
            this.AllLightOn.TabIndex = 10;
            this.AllLightOn.Text = "ALL ON";
            this.AllLightOn.UseVisualStyleBackColor = true;
            // 
            // AllLightOff
            // 
            this.AllLightOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllLightOff.Location = new System.Drawing.Point(92, 3);
            this.AllLightOff.Name = "AllLightOff";
            this.AllLightOff.Size = new System.Drawing.Size(83, 32);
            this.AllLightOff.TabIndex = 11;
            this.AllLightOff.Text = "ALL OFF";
            this.AllLightOff.UseVisualStyleBackColor = true;
            // 
            // RedLightOn
            // 
            this.RedLightOn.BackColor = System.Drawing.Color.Red;
            this.RedLightOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RedLightOn.Location = new System.Drawing.Point(3, 41);
            this.RedLightOn.Name = "RedLightOn";
            this.RedLightOn.Size = new System.Drawing.Size(83, 32);
            this.RedLightOn.TabIndex = 4;
            this.RedLightOn.Text = "ON";
            this.RedLightOn.UseVisualStyleBackColor = false;
            // 
            // RedLightOff
            // 
            this.RedLightOff.BackColor = System.Drawing.Color.Red;
            this.RedLightOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RedLightOff.Location = new System.Drawing.Point(92, 41);
            this.RedLightOff.Name = "RedLightOff";
            this.RedLightOff.Size = new System.Drawing.Size(83, 32);
            this.RedLightOff.TabIndex = 6;
            this.RedLightOff.Text = "OFF";
            this.RedLightOff.UseVisualStyleBackColor = false;
            // 
            // YellowLightOn
            // 
            this.YellowLightOn.BackColor = System.Drawing.Color.Yellow;
            this.YellowLightOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YellowLightOn.Location = new System.Drawing.Point(3, 79);
            this.YellowLightOn.Name = "YellowLightOn";
            this.YellowLightOn.Size = new System.Drawing.Size(83, 32);
            this.YellowLightOn.TabIndex = 5;
            this.YellowLightOn.Text = "ON";
            this.YellowLightOn.UseVisualStyleBackColor = false;
            // 
            // YellowLightOff
            // 
            this.YellowLightOff.BackColor = System.Drawing.Color.Yellow;
            this.YellowLightOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YellowLightOff.Location = new System.Drawing.Point(92, 79);
            this.YellowLightOff.Name = "YellowLightOff";
            this.YellowLightOff.Size = new System.Drawing.Size(83, 32);
            this.YellowLightOff.TabIndex = 7;
            this.YellowLightOff.Text = "OFF";
            this.YellowLightOff.UseVisualStyleBackColor = false;
            // 
            // GreenLightOn
            // 
            this.GreenLightOn.BackColor = System.Drawing.Color.Lime;
            this.GreenLightOn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GreenLightOn.Location = new System.Drawing.Point(3, 117);
            this.GreenLightOn.Name = "GreenLightOn";
            this.GreenLightOn.Size = new System.Drawing.Size(83, 32);
            this.GreenLightOn.TabIndex = 8;
            this.GreenLightOn.Text = "ON";
            this.GreenLightOn.UseVisualStyleBackColor = false;
            // 
            // GreenLightOff
            // 
            this.GreenLightOff.BackColor = System.Drawing.Color.Lime;
            this.GreenLightOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GreenLightOff.Location = new System.Drawing.Point(92, 117);
            this.GreenLightOff.Name = "GreenLightOff";
            this.GreenLightOff.Size = new System.Drawing.Size(83, 32);
            this.GreenLightOff.TabIndex = 9;
            this.GreenLightOff.Text = "OFF";
            this.GreenLightOff.UseVisualStyleBackColor = false;
            // 
            // grpbox_Cham_B_Manual
            // 
            this.grpbox_Cham_B_Manual.Controls.Add(this.tableLayoutPanel2);
            this.grpbox_Cham_B_Manual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_Cham_B_Manual.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grpbox_Cham_B_Manual.Location = new System.Drawing.Point(193, 3);
            this.grpbox_Cham_B_Manual.Name = "grpbox_Cham_B_Manual";
            this.grpbox_Cham_B_Manual.Size = new System.Drawing.Size(184, 172);
            this.grpbox_Cham_B_Manual.TabIndex = 29;
            this.grpbox_Cham_B_Manual.TabStop = false;
            this.grpbox_Cham_B_Manual.Text = "Chamber B Manual Control";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.pnl_Cham_B_Lamp, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_Cham_B_Door_CLOSE, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Cham_B_Lamp_OFF, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.pnl_Cham_B_Door, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Cham_B_Lamp_ON, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_Cham_B_Door_OPEN, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(178, 152);
            this.tableLayoutPanel2.TabIndex = 28;
            // 
            // pnl_Cham_B_Lamp
            // 
            this.pnl_Cham_B_Lamp.Controls.Add(this.lbl_Cham_B_Lamp);
            this.pnl_Cham_B_Lamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Cham_B_Lamp.Location = new System.Drawing.Point(3, 79);
            this.pnl_Cham_B_Lamp.Name = "pnl_Cham_B_Lamp";
            this.pnl_Cham_B_Lamp.Padding = new System.Windows.Forms.Padding(2);
            this.pnl_Cham_B_Lamp.Size = new System.Drawing.Size(53, 70);
            this.pnl_Cham_B_Lamp.TabIndex = 36;
            // 
            // lbl_Cham_B_Lamp
            // 
            this.lbl_Cham_B_Lamp.BackColor = System.Drawing.Color.White;
            this.lbl_Cham_B_Lamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Cham_B_Lamp.Location = new System.Drawing.Point(2, 2);
            this.lbl_Cham_B_Lamp.Name = "lbl_Cham_B_Lamp";
            this.lbl_Cham_B_Lamp.Size = new System.Drawing.Size(49, 66);
            this.lbl_Cham_B_Lamp.TabIndex = 25;
            this.lbl_Cham_B_Lamp.Text = "LAMP";
            this.lbl_Cham_B_Lamp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Cham_B_Door_CLOSE
            // 
            this.btn_Cham_B_Door_CLOSE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_B_Door_CLOSE.Location = new System.Drawing.Point(121, 3);
            this.btn_Cham_B_Door_CLOSE.Name = "btn_Cham_B_Door_CLOSE";
            this.btn_Cham_B_Door_CLOSE.Size = new System.Drawing.Size(54, 70);
            this.btn_Cham_B_Door_CLOSE.TabIndex = 12;
            this.btn_Cham_B_Door_CLOSE.Text = "CLOSE";
            this.btn_Cham_B_Door_CLOSE.UseVisualStyleBackColor = true;
            this.btn_Cham_B_Door_CLOSE.Click += new System.EventHandler(this.btn_Cham_B_Door_CLOSE_Click);
            // 
            // btn_Cham_B_Lamp_OFF
            // 
            this.btn_Cham_B_Lamp_OFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_B_Lamp_OFF.Location = new System.Drawing.Point(121, 79);
            this.btn_Cham_B_Lamp_OFF.Name = "btn_Cham_B_Lamp_OFF";
            this.btn_Cham_B_Lamp_OFF.Size = new System.Drawing.Size(54, 70);
            this.btn_Cham_B_Lamp_OFF.TabIndex = 24;
            this.btn_Cham_B_Lamp_OFF.Text = "OFF";
            this.btn_Cham_B_Lamp_OFF.UseVisualStyleBackColor = true;
            this.btn_Cham_B_Lamp_OFF.Click += new System.EventHandler(this.btn_Cham_B_Lamp_OFF_Click);
            // 
            // pnl_Cham_B_Door
            // 
            this.pnl_Cham_B_Door.Controls.Add(this.lbl_Cham_B_Door);
            this.pnl_Cham_B_Door.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Cham_B_Door.Location = new System.Drawing.Point(3, 3);
            this.pnl_Cham_B_Door.Name = "pnl_Cham_B_Door";
            this.pnl_Cham_B_Door.Padding = new System.Windows.Forms.Padding(2);
            this.pnl_Cham_B_Door.Size = new System.Drawing.Size(53, 70);
            this.pnl_Cham_B_Door.TabIndex = 35;
            // 
            // lbl_Cham_B_Door
            // 
            this.lbl_Cham_B_Door.BackColor = System.Drawing.Color.White;
            this.lbl_Cham_B_Door.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Cham_B_Door.Location = new System.Drawing.Point(2, 2);
            this.lbl_Cham_B_Door.Name = "lbl_Cham_B_Door";
            this.lbl_Cham_B_Door.Size = new System.Drawing.Size(49, 66);
            this.lbl_Cham_B_Door.TabIndex = 26;
            this.lbl_Cham_B_Door.Text = "DOOR";
            this.lbl_Cham_B_Door.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Cham_B_Lamp_ON
            // 
            this.btn_Cham_B_Lamp_ON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_B_Lamp_ON.Location = new System.Drawing.Point(62, 79);
            this.btn_Cham_B_Lamp_ON.Name = "btn_Cham_B_Lamp_ON";
            this.btn_Cham_B_Lamp_ON.Size = new System.Drawing.Size(53, 70);
            this.btn_Cham_B_Lamp_ON.TabIndex = 15;
            this.btn_Cham_B_Lamp_ON.Text = "ON";
            this.btn_Cham_B_Lamp_ON.UseVisualStyleBackColor = true;
            this.btn_Cham_B_Lamp_ON.Click += new System.EventHandler(this.btn_Cham_B_Lamp_ON_Click);
            // 
            // btn_Cham_B_Door_OPEN
            // 
            this.btn_Cham_B_Door_OPEN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_B_Door_OPEN.Location = new System.Drawing.Point(62, 3);
            this.btn_Cham_B_Door_OPEN.Name = "btn_Cham_B_Door_OPEN";
            this.btn_Cham_B_Door_OPEN.Size = new System.Drawing.Size(53, 70);
            this.btn_Cham_B_Door_OPEN.TabIndex = 21;
            this.btn_Cham_B_Door_OPEN.Text = "OPEN";
            this.btn_Cham_B_Door_OPEN.UseVisualStyleBackColor = true;
            this.btn_Cham_B_Door_OPEN.Click += new System.EventHandler(this.btn_Cham_B_Door_OPEN_Click);
            // 
            // grpbox_Cham_C_Manual
            // 
            this.grpbox_Cham_C_Manual.Controls.Add(this.tableLayoutPanel3);
            this.grpbox_Cham_C_Manual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_Cham_C_Manual.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grpbox_Cham_C_Manual.Location = new System.Drawing.Point(383, 3);
            this.grpbox_Cham_C_Manual.Name = "grpbox_Cham_C_Manual";
            this.grpbox_Cham_C_Manual.Size = new System.Drawing.Size(184, 172);
            this.grpbox_Cham_C_Manual.TabIndex = 30;
            this.grpbox_Cham_C_Manual.TabStop = false;
            this.grpbox_Cham_C_Manual.Text = "Chamber C Manual Control";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.pnl_Cham_C_Lamp, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btn_Cham_C_Door_CLOSE, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Cham_C_Lamp_OFF, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.pnl_Cham_C_Door, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Cham_C_Lamp_ON, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.btn_Cham_C_Door_OPEN, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(178, 152);
            this.tableLayoutPanel3.TabIndex = 28;
            // 
            // pnl_Cham_C_Lamp
            // 
            this.pnl_Cham_C_Lamp.Controls.Add(this.lbl_Cham_C_Lamp);
            this.pnl_Cham_C_Lamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Cham_C_Lamp.Location = new System.Drawing.Point(3, 79);
            this.pnl_Cham_C_Lamp.Name = "pnl_Cham_C_Lamp";
            this.pnl_Cham_C_Lamp.Padding = new System.Windows.Forms.Padding(2);
            this.pnl_Cham_C_Lamp.Size = new System.Drawing.Size(53, 70);
            this.pnl_Cham_C_Lamp.TabIndex = 38;
            // 
            // lbl_Cham_C_Lamp
            // 
            this.lbl_Cham_C_Lamp.BackColor = System.Drawing.Color.White;
            this.lbl_Cham_C_Lamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Cham_C_Lamp.Location = new System.Drawing.Point(2, 2);
            this.lbl_Cham_C_Lamp.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Cham_C_Lamp.Name = "lbl_Cham_C_Lamp";
            this.lbl_Cham_C_Lamp.Size = new System.Drawing.Size(49, 66);
            this.lbl_Cham_C_Lamp.TabIndex = 25;
            this.lbl_Cham_C_Lamp.Text = "LAMP";
            this.lbl_Cham_C_Lamp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Cham_C_Door_CLOSE
            // 
            this.btn_Cham_C_Door_CLOSE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_C_Door_CLOSE.Location = new System.Drawing.Point(121, 3);
            this.btn_Cham_C_Door_CLOSE.Name = "btn_Cham_C_Door_CLOSE";
            this.btn_Cham_C_Door_CLOSE.Size = new System.Drawing.Size(54, 70);
            this.btn_Cham_C_Door_CLOSE.TabIndex = 12;
            this.btn_Cham_C_Door_CLOSE.Text = "CLOSE";
            this.btn_Cham_C_Door_CLOSE.UseVisualStyleBackColor = true;
            this.btn_Cham_C_Door_CLOSE.Click += new System.EventHandler(this.btn_Cham_C_Door_CLOSE_Click);
            // 
            // btn_Cham_C_Lamp_OFF
            // 
            this.btn_Cham_C_Lamp_OFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_C_Lamp_OFF.Location = new System.Drawing.Point(121, 79);
            this.btn_Cham_C_Lamp_OFF.Name = "btn_Cham_C_Lamp_OFF";
            this.btn_Cham_C_Lamp_OFF.Size = new System.Drawing.Size(54, 70);
            this.btn_Cham_C_Lamp_OFF.TabIndex = 24;
            this.btn_Cham_C_Lamp_OFF.Text = "OFF";
            this.btn_Cham_C_Lamp_OFF.UseVisualStyleBackColor = true;
            this.btn_Cham_C_Lamp_OFF.Click += new System.EventHandler(this.btn_Cham_C_Lamp_OFF_Click);
            // 
            // pnl_Cham_C_Door
            // 
            this.pnl_Cham_C_Door.Controls.Add(this.lbl_Cham_C_Door);
            this.pnl_Cham_C_Door.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Cham_C_Door.Location = new System.Drawing.Point(3, 3);
            this.pnl_Cham_C_Door.Name = "pnl_Cham_C_Door";
            this.pnl_Cham_C_Door.Padding = new System.Windows.Forms.Padding(2);
            this.pnl_Cham_C_Door.Size = new System.Drawing.Size(53, 70);
            this.pnl_Cham_C_Door.TabIndex = 37;
            // 
            // lbl_Cham_C_Door
            // 
            this.lbl_Cham_C_Door.BackColor = System.Drawing.Color.White;
            this.lbl_Cham_C_Door.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Cham_C_Door.Location = new System.Drawing.Point(2, 2);
            this.lbl_Cham_C_Door.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Cham_C_Door.Name = "lbl_Cham_C_Door";
            this.lbl_Cham_C_Door.Size = new System.Drawing.Size(49, 66);
            this.lbl_Cham_C_Door.TabIndex = 26;
            this.lbl_Cham_C_Door.Text = "DOOR";
            this.lbl_Cham_C_Door.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Cham_C_Lamp_ON
            // 
            this.btn_Cham_C_Lamp_ON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_C_Lamp_ON.Location = new System.Drawing.Point(62, 79);
            this.btn_Cham_C_Lamp_ON.Name = "btn_Cham_C_Lamp_ON";
            this.btn_Cham_C_Lamp_ON.Size = new System.Drawing.Size(53, 70);
            this.btn_Cham_C_Lamp_ON.TabIndex = 15;
            this.btn_Cham_C_Lamp_ON.Text = "ON";
            this.btn_Cham_C_Lamp_ON.UseVisualStyleBackColor = true;
            this.btn_Cham_C_Lamp_ON.Click += new System.EventHandler(this.btn_Cham_C_Lamp_ON_Click);
            // 
            // btn_Cham_C_Door_OPEN
            // 
            this.btn_Cham_C_Door_OPEN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cham_C_Door_OPEN.Location = new System.Drawing.Point(62, 3);
            this.btn_Cham_C_Door_OPEN.Name = "btn_Cham_C_Door_OPEN";
            this.btn_Cham_C_Door_OPEN.Size = new System.Drawing.Size(53, 70);
            this.btn_Cham_C_Door_OPEN.TabIndex = 21;
            this.btn_Cham_C_Door_OPEN.Text = "OPEN";
            this.btn_Cham_C_Door_OPEN.UseVisualStyleBackColor = true;
            this.btn_Cham_C_Door_OPEN.Click += new System.EventHandler(this.btn_Cham_C_Door_OPEN_Click);
            // 
            // grpbox_RobotManualControl
            // 
            this.grpbox_RobotManualControl.Controls.Add(this.pnl_RobotManualControl);
            this.grpbox_RobotManualControl.Location = new System.Drawing.Point(15, 294);
            this.grpbox_RobotManualControl.Name = "grpbox_RobotManualControl";
            this.grpbox_RobotManualControl.Size = new System.Drawing.Size(766, 234);
            this.grpbox_RobotManualControl.TabIndex = 50;
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
            this.pnl_RobotManualControl.Size = new System.Drawing.Size(760, 214);
            this.pnl_RobotManualControl.TabIndex = 0;
            // 
            // grpbox_Vacuum
            // 
            this.grpbox_Vacuum.Controls.Add(this.tableLayoutPanel4);
            this.grpbox_Vacuum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_Vacuum.Location = new System.Drawing.Point(573, 3);
            this.grpbox_Vacuum.Name = "grpbox_Vacuum";
            this.grpbox_Vacuum.Size = new System.Drawing.Size(184, 208);
            this.grpbox_Vacuum.TabIndex = 1;
            this.grpbox_Vacuum.TabStop = false;
            this.grpbox_Vacuum.Text = "ROBOT Vacuum Control";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btn_ExOFF, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.btn_ExON, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.btn_InOFF, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_InOn, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(178, 188);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // btn_ExOFF
            // 
            this.btn_ExOFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ExOFF.Location = new System.Drawing.Point(92, 97);
            this.btn_ExOFF.Name = "btn_ExOFF";
            this.btn_ExOFF.Size = new System.Drawing.Size(83, 88);
            this.btn_ExOFF.TabIndex = 3;
            this.btn_ExOFF.Text = "Exhaust OFF";
            this.btn_ExOFF.UseVisualStyleBackColor = true;
            this.btn_ExOFF.Click += new System.EventHandler(this.btn_ExOFF_Click);
            // 
            // btn_ExON
            // 
            this.btn_ExON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ExON.Location = new System.Drawing.Point(3, 97);
            this.btn_ExON.Name = "btn_ExON";
            this.btn_ExON.Size = new System.Drawing.Size(83, 88);
            this.btn_ExON.TabIndex = 2;
            this.btn_ExON.Text = "Exhaust  ON";
            this.btn_ExON.UseVisualStyleBackColor = true;
            this.btn_ExON.Click += new System.EventHandler(this.btn_ExON_Click);
            // 
            // btn_InOFF
            // 
            this.btn_InOFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_InOFF.Location = new System.Drawing.Point(92, 3);
            this.btn_InOFF.Name = "btn_InOFF";
            this.btn_InOFF.Size = new System.Drawing.Size(83, 88);
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
            this.btn_InOn.Size = new System.Drawing.Size(83, 88);
            this.btn_InOn.TabIndex = 0;
            this.btn_InOn.Text = "Inhalation ON";
            this.btn_InOn.UseVisualStyleBackColor = true;
            this.btn_InOn.Click += new System.EventHandler(this.btn_InOn_Click);
            // 
            // grpbox_AxisPositionControl
            // 
            this.grpbox_AxisPositionControl.Controls.Add(this.tableLayoutPanel5);
            this.grpbox_AxisPositionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_AxisPositionControl.Location = new System.Drawing.Point(383, 3);
            this.grpbox_AxisPositionControl.Name = "grpbox_AxisPositionControl";
            this.grpbox_AxisPositionControl.Size = new System.Drawing.Size(184, 208);
            this.grpbox_AxisPositionControl.TabIndex = 1;
            this.grpbox_AxisPositionControl.TabStop = false;
            this.grpbox_AxisPositionControl.Text = "ROBOT Axis Control";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btn_LRMove, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.btn_UDMove, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbl_TargetPosition, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.pnl_TargetPosition, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(178, 188);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // btn_LRMove
            // 
            this.btn_LRMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_LRMove.Location = new System.Drawing.Point(92, 97);
            this.btn_LRMove.Name = "btn_LRMove";
            this.btn_LRMove.Size = new System.Drawing.Size(83, 88);
            this.btn_LRMove.TabIndex = 3;
            this.btn_LRMove.Text = "LR Move";
            this.btn_LRMove.UseVisualStyleBackColor = true;
            this.btn_LRMove.Click += new System.EventHandler(this.btn_LRMove_Click);
            // 
            // btn_UDMove
            // 
            this.btn_UDMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_UDMove.Location = new System.Drawing.Point(3, 97);
            this.btn_UDMove.Name = "btn_UDMove";
            this.btn_UDMove.Size = new System.Drawing.Size(83, 88);
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
            this.lbl_TargetPosition.Size = new System.Drawing.Size(83, 94);
            this.lbl_TargetPosition.TabIndex = 6;
            this.lbl_TargetPosition.Text = "Target Position";
            this.lbl_TargetPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_TargetPosition
            // 
            this.pnl_TargetPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_TargetPosition.Location = new System.Drawing.Point(89, 35);
            this.pnl_TargetPosition.Margin = new System.Windows.Forms.Padding(0, 35, 0, 35);
            this.pnl_TargetPosition.Maximum = new decimal(new int[] {
            3500000,
            0,
            0,
            0});
            this.pnl_TargetPosition.Minimum = new decimal(new int[] {
            500000,
            0,
            0,
            -2147483648});
            this.pnl_TargetPosition.Name = "pnl_TargetPosition";
            this.pnl_TargetPosition.Size = new System.Drawing.Size(89, 21);
            this.pnl_TargetPosition.TabIndex = 6;
            // 
            // grpbox_AxisJogControl
            // 
            this.grpbox_AxisJogControl.Controls.Add(this.pnl_AxisJogControl);
            this.grpbox_AxisJogControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_AxisJogControl.Location = new System.Drawing.Point(193, 3);
            this.grpbox_AxisJogControl.Name = "grpbox_AxisJogControl";
            this.grpbox_AxisJogControl.Size = new System.Drawing.Size(184, 208);
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
            this.pnl_AxisJogControl.Size = new System.Drawing.Size(178, 188);
            this.pnl_AxisJogControl.TabIndex = 0;
            // 
            // btn_MoveLeft
            // 
            this.btn_MoveLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MoveLeft.Location = new System.Drawing.Point(3, 122);
            this.btn_MoveLeft.Name = "btn_MoveLeft";
            this.btn_MoveLeft.Size = new System.Drawing.Size(83, 63);
            this.btn_MoveLeft.TabIndex = 2;
            this.btn_MoveLeft.Text = "Jog Move ←";
            this.btn_MoveLeft.UseVisualStyleBackColor = true;
            this.btn_MoveLeft.Click += new System.EventHandler(this.btn_MoveLeft_Click);
            // 
            // btn_MoveDown
            // 
            this.btn_MoveDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MoveDown.Location = new System.Drawing.Point(92, 54);
            this.btn_MoveDown.Name = "btn_MoveDown";
            this.btn_MoveDown.Size = new System.Drawing.Size(83, 62);
            this.btn_MoveDown.TabIndex = 1;
            this.btn_MoveDown.Text = "Jog Move ↓";
            this.btn_MoveDown.UseVisualStyleBackColor = true;
            this.btn_MoveDown.Click += new System.EventHandler(this.btn_MoveDown_Click);
            // 
            // btn_MoveUp
            // 
            this.btn_MoveUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MoveUp.Location = new System.Drawing.Point(3, 54);
            this.btn_MoveUp.Name = "btn_MoveUp";
            this.btn_MoveUp.Size = new System.Drawing.Size(83, 62);
            this.btn_MoveUp.TabIndex = 0;
            this.btn_MoveUp.Text = "Jog Move ↑";
            this.btn_MoveUp.UseVisualStyleBackColor = true;
            this.btn_MoveUp.Click += new System.EventHandler(this.btn_MoveUp_Click);
            // 
            // btn_MoveRight
            // 
            this.btn_MoveRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MoveRight.Location = new System.Drawing.Point(92, 122);
            this.btn_MoveRight.Name = "btn_MoveRight";
            this.btn_MoveRight.Size = new System.Drawing.Size(83, 63);
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
            this.lbl_MoveDistance.Size = new System.Drawing.Size(83, 51);
            this.lbl_MoveDistance.TabIndex = 4;
            this.lbl_MoveDistance.Text = "Movement Distance";
            this.lbl_MoveDistance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nUpDown_MovementDistance
            // 
            this.nUpDown_MovementDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nUpDown_MovementDistance.Location = new System.Drawing.Point(89, 13);
            this.nUpDown_MovementDistance.Margin = new System.Windows.Forms.Padding(0, 13, 0, 13);
            this.nUpDown_MovementDistance.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nUpDown_MovementDistance.Name = "nUpDown_MovementDistance";
            this.nUpDown_MovementDistance.Size = new System.Drawing.Size(89, 21);
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
            this.grpbox_BasicPoint.Size = new System.Drawing.Size(184, 208);
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
            this.pnl_DriverControl.Size = new System.Drawing.Size(178, 188);
            this.pnl_DriverControl.TabIndex = 0;
            // 
            // btn_LRBasic
            // 
            this.btn_LRBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_LRBasic.Location = new System.Drawing.Point(92, 97);
            this.btn_LRBasic.Name = "btn_LRBasic";
            this.btn_LRBasic.Size = new System.Drawing.Size(83, 88);
            this.btn_LRBasic.TabIndex = 3;
            this.btn_LRBasic.Text = "LR Basic Point";
            this.btn_LRBasic.UseVisualStyleBackColor = true;
            this.btn_LRBasic.Click += new System.EventHandler(this.btn_LRBasic_Click);
            // 
            // btn_UDBasic
            // 
            this.btn_UDBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_UDBasic.Location = new System.Drawing.Point(3, 97);
            this.btn_UDBasic.Name = "btn_UDBasic";
            this.btn_UDBasic.Size = new System.Drawing.Size(83, 88);
            this.btn_UDBasic.TabIndex = 2;
            this.btn_UDBasic.Text = "UD Basic Point";
            this.btn_UDBasic.UseVisualStyleBackColor = true;
            this.btn_UDBasic.Click += new System.EventHandler(this.btn_UDBasic_Click);
            // 
            // btn_ServoOFF
            // 
            this.btn_ServoOFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ServoOFF.Location = new System.Drawing.Point(92, 3);
            this.btn_ServoOFF.Name = "btn_ServoOFF";
            this.btn_ServoOFF.Size = new System.Drawing.Size(83, 88);
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
            this.btn_ServoON.Size = new System.Drawing.Size(83, 88);
            this.btn_ServoON.TabIndex = 0;
            this.btn_ServoON.Text = "Servo ON";
            this.btn_ServoON.UseVisualStyleBackColor = true;
            this.btn_ServoON.Click += new System.EventHandler(this.btn_ServoON_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnl_ProcessManualControl);
            this.groupBox1.Location = new System.Drawing.Point(18, 534);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 198);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chamber and Lamp Manual Control";
            // 
            // grpbox_currentPos
            // 
            this.grpbox_currentPos.Controls.Add(this.pnl_AxisStatus);
            this.grpbox_currentPos.Location = new System.Drawing.Point(465, 214);
            this.grpbox_currentPos.Name = "grpbox_currentPos";
            this.grpbox_currentPos.Size = new System.Drawing.Size(313, 74);
            this.grpbox_currentPos.TabIndex = 53;
            this.grpbox_currentPos.TabStop = false;
            this.grpbox_currentPos.Text = "Axis Status";
            // 
            // pnl_AxisStatus
            // 
            this.pnl_AxisStatus.ColumnCount = 2;
            this.pnl_AxisStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_AxisStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_AxisStatus.Controls.Add(this.lbl_LRcurrentPos, 1, 1);
            this.pnl_AxisStatus.Controls.Add(this.lbl_UD, 0, 0);
            this.pnl_AxisStatus.Controls.Add(this.lbl_LR, 0, 1);
            this.pnl_AxisStatus.Controls.Add(this.lbl_UDcurrentPos, 1, 0);
            this.pnl_AxisStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_AxisStatus.Location = new System.Drawing.Point(3, 17);
            this.pnl_AxisStatus.Name = "pnl_AxisStatus";
            this.pnl_AxisStatus.RowCount = 2;
            this.pnl_AxisStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_AxisStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_AxisStatus.Size = new System.Drawing.Size(307, 54);
            this.pnl_AxisStatus.TabIndex = 0;
            // 
            // lbl_LRcurrentPos
            // 
            this.lbl_LRcurrentPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_LRcurrentPos.Location = new System.Drawing.Point(156, 27);
            this.lbl_LRcurrentPos.Name = "lbl_LRcurrentPos";
            this.lbl_LRcurrentPos.Size = new System.Drawing.Size(148, 27);
            this.lbl_LRcurrentPos.TabIndex = 3;
            this.lbl_LRcurrentPos.Text = "0";
            this.lbl_LRcurrentPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_UD
            // 
            this.lbl_UD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_UD.Location = new System.Drawing.Point(3, 0);
            this.lbl_UD.Name = "lbl_UD";
            this.lbl_UD.Size = new System.Drawing.Size(147, 27);
            this.lbl_UD.TabIndex = 0;
            this.lbl_UD.Text = "UD current Pos : ";
            this.lbl_UD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_LR
            // 
            this.lbl_LR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_LR.Location = new System.Drawing.Point(3, 27);
            this.lbl_LR.Name = "lbl_LR";
            this.lbl_LR.Size = new System.Drawing.Size(147, 27);
            this.lbl_LR.TabIndex = 1;
            this.lbl_LR.Text = "LR current Pos : ";
            this.lbl_LR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_UDcurrentPos
            // 
            this.lbl_UDcurrentPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_UDcurrentPos.Location = new System.Drawing.Point(156, 0);
            this.lbl_UDcurrentPos.Name = "lbl_UDcurrentPos";
            this.lbl_UDcurrentPos.Size = new System.Drawing.Size(148, 27);
            this.lbl_UDcurrentPos.TabIndex = 2;
            this.lbl_UDcurrentPos.Text = "0";
            this.lbl_UDcurrentPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpbox_RobotSylinder
            // 
            this.grpbox_RobotSylinder.Controls.Add(this.tableLayoutPanel7);
            this.grpbox_RobotSylinder.Location = new System.Drawing.Point(259, 214);
            this.grpbox_RobotSylinder.Name = "grpbox_RobotSylinder";
            this.grpbox_RobotSylinder.Size = new System.Drawing.Size(200, 74);
            this.grpbox_RobotSylinder.TabIndex = 54;
            this.grpbox_RobotSylinder.TabStop = false;
            this.grpbox_RobotSylinder.Text = "Robot Sylinder";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.btn_moveBack, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.btn_moveFront, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(194, 54);
            this.tableLayoutPanel7.TabIndex = 55;
            // 
            // btn_moveBack
            // 
            this.btn_moveBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_moveBack.Location = new System.Drawing.Point(100, 3);
            this.btn_moveBack.Name = "btn_moveBack";
            this.btn_moveBack.Size = new System.Drawing.Size(91, 48);
            this.btn_moveBack.TabIndex = 1;
            this.btn_moveBack.Text = "Move Back";
            this.btn_moveBack.UseVisualStyleBackColor = true;
            this.btn_moveBack.Click += new System.EventHandler(this.btn_moveBack_Click);
            // 
            // btn_moveFront
            // 
            this.btn_moveFront.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_moveFront.Location = new System.Drawing.Point(3, 3);
            this.btn_moveFront.Name = "btn_moveFront";
            this.btn_moveFront.Size = new System.Drawing.Size(91, 48);
            this.btn_moveFront.TabIndex = 0;
            this.btn_moveFront.Text = "Move Front";
            this.btn_moveFront.UseVisualStyleBackColor = true;
            this.btn_moveFront.Click += new System.EventHandler(this.btn_moveFront_Click);
            // 
            // test
            // 
            this.test.Controls.Add(this.tableLayoutPanel8);
            this.test.Location = new System.Drawing.Point(259, 94);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(200, 100);
            this.test.TabIndex = 55;
            this.test.TabStop = false;
            this.test.Text = "test";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(194, 80);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(100, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 80);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 80);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MaintGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.test);
            this.Controls.Add(this.grpbox_RobotSylinder);
            this.Controls.Add(this.grpbox_currentPos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpbox_RobotManualControl);
            this.Name = "MaintGUI";
            this.Size = new System.Drawing.Size(1000, 750);
            this.pnl_ProcessManualControl.ResumeLayout(false);
            this.grpbox_Cham_A_Manual.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnl_Cham_A_Lamp.ResumeLayout(false);
            this.pnl_Cham_A_Door.ResumeLayout(false);
            this.grpbox_Tower.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.grpbox_Cham_B_Manual.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.pnl_Cham_B_Lamp.ResumeLayout(false);
            this.pnl_Cham_B_Door.ResumeLayout(false);
            this.grpbox_Cham_C_Manual.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.pnl_Cham_C_Lamp.ResumeLayout(false);
            this.pnl_Cham_C_Door.ResumeLayout(false);
            this.grpbox_RobotManualControl.ResumeLayout(false);
            this.pnl_RobotManualControl.ResumeLayout(false);
            this.grpbox_Vacuum.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.grpbox_AxisPositionControl.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_TargetPosition)).EndInit();
            this.grpbox_AxisJogControl.ResumeLayout(false);
            this.pnl_AxisJogControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_MovementDistance)).EndInit();
            this.grpbox_BasicPoint.ResumeLayout(false);
            this.pnl_DriverControl.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.grpbox_currentPos.ResumeLayout(false);
            this.pnl_AxisStatus.ResumeLayout(false);
            this.grpbox_RobotSylinder.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.test.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnl_ProcessManualControl;
        private System.Windows.Forms.GroupBox grpbox_Cham_A_Manual;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl_Cham_A_Lamp;
        private System.Windows.Forms.Label lbl_Cham_A_Lamp;
        private System.Windows.Forms.Panel pnl_Cham_A_Door;
        private System.Windows.Forms.Label lbl_Cham_A_Door;
        private System.Windows.Forms.Button btn_Cham_A_Door_CLOSE;
        private System.Windows.Forms.Button btn_Cham_A_Lamp_OFF;
        private System.Windows.Forms.Button btn_Cham_A_Lamp_ON;
        private System.Windows.Forms.Button btn_Cham_A_Door_OPEN;
        private System.Windows.Forms.GroupBox grpbox_Cham_B_Manual;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnl_Cham_B_Lamp;
        private System.Windows.Forms.Label lbl_Cham_B_Lamp;
        private System.Windows.Forms.Button btn_Cham_B_Door_CLOSE;
        private System.Windows.Forms.Button btn_Cham_B_Lamp_OFF;
        private System.Windows.Forms.Panel pnl_Cham_B_Door;
        private System.Windows.Forms.Label lbl_Cham_B_Door;
        private System.Windows.Forms.Button btn_Cham_B_Lamp_ON;
        private System.Windows.Forms.Button btn_Cham_B_Door_OPEN;
        private System.Windows.Forms.GroupBox grpbox_Cham_C_Manual;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel pnl_Cham_C_Lamp;
        private System.Windows.Forms.Label lbl_Cham_C_Lamp;
        private System.Windows.Forms.Button btn_Cham_C_Door_CLOSE;
        private System.Windows.Forms.Button btn_Cham_C_Lamp_OFF;
        private System.Windows.Forms.Panel pnl_Cham_C_Door;
        private System.Windows.Forms.Label lbl_Cham_C_Door;
        private System.Windows.Forms.Button btn_Cham_C_Lamp_ON;
        private System.Windows.Forms.Button btn_Cham_C_Door_OPEN;
        internal System.Windows.Forms.GroupBox grpbox_RobotManualControl;
        internal System.Windows.Forms.TableLayoutPanel pnl_RobotManualControl;
        internal System.Windows.Forms.GroupBox grpbox_Vacuum;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        internal System.Windows.Forms.Button btn_ExOFF;
        internal System.Windows.Forms.Button btn_ExON;
        internal System.Windows.Forms.Button btn_InOFF;
        internal System.Windows.Forms.Button btn_InOn;
        internal System.Windows.Forms.GroupBox grpbox_AxisPositionControl;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        internal System.Windows.Forms.Button btn_LRMove;
        internal System.Windows.Forms.Button btn_UDMove;
        internal System.Windows.Forms.Label lbl_TargetPosition;
        internal System.Windows.Forms.NumericUpDown pnl_TargetPosition;
        internal System.Windows.Forms.GroupBox grpbox_AxisJogControl;
        internal System.Windows.Forms.TableLayoutPanel pnl_AxisJogControl;
        internal System.Windows.Forms.Button btn_MoveLeft;
        internal System.Windows.Forms.Button btn_MoveDown;
        internal System.Windows.Forms.Button btn_MoveUp;
        internal System.Windows.Forms.Button btn_MoveRight;
        internal System.Windows.Forms.Label lbl_MoveDistance;
        internal System.Windows.Forms.NumericUpDown nUpDown_MovementDistance;
        internal System.Windows.Forms.GroupBox grpbox_BasicPoint;
        internal System.Windows.Forms.TableLayoutPanel pnl_DriverControl;
        internal System.Windows.Forms.Button btn_LRBasic;
        internal System.Windows.Forms.Button btn_UDBasic;
        internal System.Windows.Forms.Button btn_ServoOFF;
        internal System.Windows.Forms.Button btn_ServoON;
        internal System.Windows.Forms.GroupBox grpbox_Tower;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        internal System.Windows.Forms.Button AllLightOn;
        internal System.Windows.Forms.Button AllLightOff;
        internal System.Windows.Forms.Button RedLightOn;
        internal System.Windows.Forms.Button RedLightOff;
        internal System.Windows.Forms.Button YellowLightOn;
        internal System.Windows.Forms.Button YellowLightOff;
        internal System.Windows.Forms.Button GreenLightOn;
        internal System.Windows.Forms.Button GreenLightOff;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpbox_currentPos;
        private System.Windows.Forms.TableLayoutPanel pnl_AxisStatus;
        private System.Windows.Forms.Label lbl_LRcurrentPos;
        private System.Windows.Forms.Label lbl_UD;
        private System.Windows.Forms.Label lbl_LR;
        private System.Windows.Forms.Label lbl_UDcurrentPos;
        private System.Windows.Forms.GroupBox grpbox_RobotSylinder;
        private System.Windows.Forms.Button btn_moveFront;
        private System.Windows.Forms.Button btn_moveBack;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.GroupBox test;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
