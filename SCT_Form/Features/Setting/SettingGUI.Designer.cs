namespace SCT_Form
{
    partial class SettingGUI
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
            this.grpbox_RobotParameter = new System.Windows.Forms.GroupBox();
            this.pnl_RobotParameter = new System.Windows.Forms.TableLayoutPanel();
            this.nUpDown_Velo = new System.Windows.Forms.NumericUpDown();
            this.nUpDown_MaxVelo = new System.Windows.Forms.NumericUpDown();
            this.nUpDown_Decel = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Accel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nUpDown_Accel = new System.Windows.Forms.NumericUpDown();
            this.btn_ParameterSet = new System.Windows.Forms.Button();
            this.grpBox_EtherCAT_Setting = new System.Windows.Forms.GroupBox();
            this.pnl_EtherCAT = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ConnectionTimeOut = new System.Windows.Forms.Label();
            this.lbl_ReconnectRetryCount = new System.Windows.Forms.Label();
            this.nUpDown_TimeOut = new System.Windows.Forms.NumericUpDown();
            this.nUpDown_RetryCount = new System.Windows.Forms.NumericUpDown();
            this.lbl_EtherCATreadCycle = new System.Windows.Forms.Label();
            this.nUpDown_ReadCycle = new System.Windows.Forms.NumericUpDown();
            this.grpBox_LogSetting = new System.Windows.Forms.GroupBox();
            this.pnl_LogSetting = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ShowDebugLog = new System.Windows.Forms.Label();
            this.lbl_MaxDisplayLogCount = new System.Windows.Forms.Label();
            this.nUpDown_MaxDisplayLogCount = new System.Windows.Forms.NumericUpDown();
            this.LogRetentionPeriod = new System.Windows.Forms.Label();
            this.nUpDown_LogRetentionPeriod = new System.Windows.Forms.NumericUpDown();
            this.cbox_ShowDebugLog = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_DefaultRecipeSavePath = new System.Windows.Forms.Label();
            this.btn_OpenRecipeFolder = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cbox_ModeChangeForceStop = new System.Windows.Forms.ComboBox();
            this.cbox_AutoStopAlarmLevel = new System.Windows.Forms.ComboBox();
            this.cbox_AlarmAutoStop = new System.Windows.Forms.ComboBox();
            this.cbox_DoorOpenInterlock = new System.Windows.Forms.ComboBox();
            this.lbl_AlarmAutoStop = new System.Windows.Forms.Label();
            this.lbl_DoorOpenInterlock = new System.Windows.Forms.Label();
            this.lbl_AutoStopAlarmLevel = new System.Windows.Forms.Label();
            this.lbl_ModeChangeForceStop = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cbox_MaintenanceLampStatus = new System.Windows.Forms.ComboBox();
            this.cbox_AlarmLampStatus = new System.Windows.Forms.ComboBox();
            this.cbox_RunLampStatus = new System.Windows.Forms.ComboBox();
            this.cbox_IdleLampStatus = new System.Windows.Forms.ComboBox();
            this.lbl_RunLampStatus = new System.Windows.Forms.Label();
            this.lbl_IdleLampStatus = new System.Windows.Forms.Label();
            this.lbl_AlarmLampStatus = new System.Windows.Forms.Label();
            this.lbl_MaintenanceLampStatus = new System.Windows.Forms.Label();
            this.pnl_ApplyCancel = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.grpbox_RobotParameter.SuspendLayout();
            this.pnl_RobotParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Velo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_MaxVelo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Decel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Accel)).BeginInit();
            this.grpBox_EtherCAT_Setting.SuspendLayout();
            this.pnl_EtherCAT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_TimeOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_RetryCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_ReadCycle)).BeginInit();
            this.grpBox_LogSetting.SuspendLayout();
            this.pnl_LogSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_MaxDisplayLogCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_LogRetentionPeriod)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.pnl_ApplyCancel.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbox_RobotParameter
            // 
            this.grpbox_RobotParameter.Controls.Add(this.pnl_RobotParameter);
            this.grpbox_RobotParameter.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grpbox_RobotParameter.Location = new System.Drawing.Point(128, 31);
            this.grpbox_RobotParameter.Name = "grpbox_RobotParameter";
            this.grpbox_RobotParameter.Size = new System.Drawing.Size(325, 196);
            this.grpbox_RobotParameter.TabIndex = 1;
            this.grpbox_RobotParameter.TabStop = false;
            this.grpbox_RobotParameter.Text = "1. Transfer Robot Parameter";
            // 
            // pnl_RobotParameter
            // 
            this.pnl_RobotParameter.ColumnCount = 2;
            this.pnl_RobotParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_RobotParameter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_RobotParameter.Controls.Add(this.nUpDown_Velo, 1, 3);
            this.pnl_RobotParameter.Controls.Add(this.nUpDown_MaxVelo, 1, 2);
            this.pnl_RobotParameter.Controls.Add(this.nUpDown_Decel, 1, 1);
            this.pnl_RobotParameter.Controls.Add(this.label2, 0, 1);
            this.pnl_RobotParameter.Controls.Add(this.lbl_Accel, 0, 0);
            this.pnl_RobotParameter.Controls.Add(this.label3, 0, 2);
            this.pnl_RobotParameter.Controls.Add(this.label1, 0, 3);
            this.pnl_RobotParameter.Controls.Add(this.nUpDown_Accel, 1, 0);
            this.pnl_RobotParameter.Controls.Add(this.btn_ParameterSet, 0, 4);
            this.pnl_RobotParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_RobotParameter.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.pnl_RobotParameter.Location = new System.Drawing.Point(3, 19);
            this.pnl_RobotParameter.Name = "pnl_RobotParameter";
            this.pnl_RobotParameter.RowCount = 5;
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnl_RobotParameter.Size = new System.Drawing.Size(319, 174);
            this.pnl_RobotParameter.TabIndex = 0;
            // 
            // nUpDown_Velo
            // 
            this.nUpDown_Velo.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_Velo.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.nUpDown_Velo.Location = new System.Drawing.Point(167, 110);
            this.nUpDown_Velo.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_Velo.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nUpDown_Velo.Name = "nUpDown_Velo";
            this.nUpDown_Velo.Size = new System.Drawing.Size(144, 23);
            this.nUpDown_Velo.TabIndex = 7;
            this.nUpDown_Velo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nUpDown_Velo.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // nUpDown_MaxVelo
            // 
            this.nUpDown_MaxVelo.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_MaxVelo.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.nUpDown_MaxVelo.Location = new System.Drawing.Point(167, 76);
            this.nUpDown_MaxVelo.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_MaxVelo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nUpDown_MaxVelo.Name = "nUpDown_MaxVelo";
            this.nUpDown_MaxVelo.Size = new System.Drawing.Size(144, 23);
            this.nUpDown_MaxVelo.TabIndex = 6;
            this.nUpDown_MaxVelo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nUpDown_MaxVelo.Value = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            // 
            // nUpDown_Decel
            // 
            this.nUpDown_Decel.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_Decel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.nUpDown_Decel.Location = new System.Drawing.Point(167, 42);
            this.nUpDown_Decel.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_Decel.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nUpDown_Decel.Name = "nUpDown_Decel";
            this.nUpDown_Decel.Size = new System.Drawing.Size(144, 23);
            this.nUpDown_Decel.TabIndex = 5;
            this.nUpDown_Decel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nUpDown_Decel.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 34);
            this.label2.TabIndex = 2;
            this.label2.Text = "Deceleration (감속도)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Accel
            // 
            this.lbl_Accel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Accel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_Accel.Location = new System.Drawing.Point(3, 0);
            this.lbl_Accel.Name = "lbl_Accel";
            this.lbl_Accel.Size = new System.Drawing.Size(153, 34);
            this.lbl_Accel.TabIndex = 0;
            this.lbl_Accel.Text = "Acceleration (가속도)";
            this.lbl_Accel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label3.Location = new System.Drawing.Point(3, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 34);
            this.label3.TabIndex = 3;
            this.label3.Text = "Max Velocity (최대 속도)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label1.Location = new System.Drawing.Point(3, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "Veloctiy (속도)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nUpDown_Accel
            // 
            this.nUpDown_Accel.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_Accel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.nUpDown_Accel.Location = new System.Drawing.Point(167, 8);
            this.nUpDown_Accel.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_Accel.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nUpDown_Accel.Name = "nUpDown_Accel";
            this.nUpDown_Accel.Size = new System.Drawing.Size(144, 23);
            this.nUpDown_Accel.TabIndex = 4;
            this.nUpDown_Accel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nUpDown_Accel.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // btn_ParameterSet
            // 
            this.pnl_RobotParameter.SetColumnSpan(this.btn_ParameterSet, 2);
            this.btn_ParameterSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ParameterSet.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btn_ParameterSet.Location = new System.Drawing.Point(3, 139);
            this.btn_ParameterSet.Name = "btn_ParameterSet";
            this.btn_ParameterSet.Size = new System.Drawing.Size(313, 32);
            this.btn_ParameterSet.TabIndex = 8;
            this.btn_ParameterSet.Text = "Transfer Robot Parameter Set";
            this.btn_ParameterSet.UseVisualStyleBackColor = true;
            // 
            // grpBox_EtherCAT_Setting
            // 
            this.grpBox_EtherCAT_Setting.Controls.Add(this.pnl_EtherCAT);
            this.grpBox_EtherCAT_Setting.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grpBox_EtherCAT_Setting.Location = new System.Drawing.Point(128, 233);
            this.grpBox_EtherCAT_Setting.Name = "grpBox_EtherCAT_Setting";
            this.grpBox_EtherCAT_Setting.Size = new System.Drawing.Size(325, 149);
            this.grpBox_EtherCAT_Setting.TabIndex = 2;
            this.grpBox_EtherCAT_Setting.TabStop = false;
            this.grpBox_EtherCAT_Setting.Text = "2. Connection, EtherCAT";
            // 
            // pnl_EtherCAT
            // 
            this.pnl_EtherCAT.ColumnCount = 2;
            this.pnl_EtherCAT.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_EtherCAT.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_EtherCAT.Controls.Add(this.lbl_ConnectionTimeOut, 0, 2);
            this.pnl_EtherCAT.Controls.Add(this.lbl_ReconnectRetryCount, 0, 1);
            this.pnl_EtherCAT.Controls.Add(this.nUpDown_TimeOut, 1, 2);
            this.pnl_EtherCAT.Controls.Add(this.nUpDown_RetryCount, 1, 1);
            this.pnl_EtherCAT.Controls.Add(this.lbl_EtherCATreadCycle, 0, 0);
            this.pnl_EtherCAT.Controls.Add(this.nUpDown_ReadCycle, 1, 0);
            this.pnl_EtherCAT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_EtherCAT.Location = new System.Drawing.Point(3, 19);
            this.pnl_EtherCAT.Name = "pnl_EtherCAT";
            this.pnl_EtherCAT.RowCount = 3;
            this.pnl_EtherCAT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_EtherCAT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_EtherCAT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_EtherCAT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_EtherCAT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_EtherCAT.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnl_EtherCAT.Size = new System.Drawing.Size(319, 127);
            this.pnl_EtherCAT.TabIndex = 0;
            // 
            // lbl_ConnectionTimeOut
            // 
            this.lbl_ConnectionTimeOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ConnectionTimeOut.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_ConnectionTimeOut.Location = new System.Drawing.Point(3, 84);
            this.lbl_ConnectionTimeOut.Name = "lbl_ConnectionTimeOut";
            this.lbl_ConnectionTimeOut.Size = new System.Drawing.Size(153, 43);
            this.lbl_ConnectionTimeOut.TabIndex = 10;
            this.lbl_ConnectionTimeOut.Text = "Connection TimeOut\r\n(Max : 30000ms/sec)";
            this.lbl_ConnectionTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ReconnectRetryCount
            // 
            this.lbl_ReconnectRetryCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ReconnectRetryCount.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_ReconnectRetryCount.Location = new System.Drawing.Point(3, 42);
            this.lbl_ReconnectRetryCount.Name = "lbl_ReconnectRetryCount";
            this.lbl_ReconnectRetryCount.Size = new System.Drawing.Size(153, 42);
            this.lbl_ReconnectRetryCount.TabIndex = 9;
            this.lbl_ReconnectRetryCount.Text = "Reconnect Retry Count";
            this.lbl_ReconnectRetryCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nUpDown_TimeOut
            // 
            this.nUpDown_TimeOut.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_TimeOut.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.nUpDown_TimeOut.Location = new System.Drawing.Point(167, 92);
            this.nUpDown_TimeOut.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_TimeOut.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nUpDown_TimeOut.Name = "nUpDown_TimeOut";
            this.nUpDown_TimeOut.Size = new System.Drawing.Size(144, 23);
            this.nUpDown_TimeOut.TabIndex = 6;
            this.nUpDown_TimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nUpDown_TimeOut.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // nUpDown_RetryCount
            // 
            this.nUpDown_RetryCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_RetryCount.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.nUpDown_RetryCount.Location = new System.Drawing.Point(167, 50);
            this.nUpDown_RetryCount.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_RetryCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUpDown_RetryCount.Name = "nUpDown_RetryCount";
            this.nUpDown_RetryCount.Size = new System.Drawing.Size(144, 23);
            this.nUpDown_RetryCount.TabIndex = 5;
            this.nUpDown_RetryCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nUpDown_RetryCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lbl_EtherCATreadCycle
            // 
            this.lbl_EtherCATreadCycle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_EtherCATreadCycle.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_EtherCATreadCycle.Location = new System.Drawing.Point(3, 0);
            this.lbl_EtherCATreadCycle.Name = "lbl_EtherCATreadCycle";
            this.lbl_EtherCATreadCycle.Size = new System.Drawing.Size(153, 42);
            this.lbl_EtherCATreadCycle.TabIndex = 0;
            this.lbl_EtherCATreadCycle.Text = "EtherCAT Read Cycle(ms)\r\n(Max : 1000, Min : 100)";
            this.lbl_EtherCATreadCycle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nUpDown_ReadCycle
            // 
            this.nUpDown_ReadCycle.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_ReadCycle.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.nUpDown_ReadCycle.Location = new System.Drawing.Point(167, 8);
            this.nUpDown_ReadCycle.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_ReadCycle.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nUpDown_ReadCycle.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nUpDown_ReadCycle.Name = "nUpDown_ReadCycle";
            this.nUpDown_ReadCycle.Size = new System.Drawing.Size(144, 23);
            this.nUpDown_ReadCycle.TabIndex = 4;
            this.nUpDown_ReadCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nUpDown_ReadCycle.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // grpBox_LogSetting
            // 
            this.grpBox_LogSetting.Controls.Add(this.pnl_LogSetting);
            this.grpBox_LogSetting.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.grpBox_LogSetting.Location = new System.Drawing.Point(128, 388);
            this.grpBox_LogSetting.Name = "grpBox_LogSetting";
            this.grpBox_LogSetting.Size = new System.Drawing.Size(325, 149);
            this.grpBox_LogSetting.TabIndex = 3;
            this.grpBox_LogSetting.TabStop = false;
            this.grpBox_LogSetting.Text = "3. Log Setting";
            // 
            // pnl_LogSetting
            // 
            this.pnl_LogSetting.ColumnCount = 2;
            this.pnl_LogSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_LogSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_LogSetting.Controls.Add(this.lbl_ShowDebugLog, 0, 2);
            this.pnl_LogSetting.Controls.Add(this.lbl_MaxDisplayLogCount, 0, 1);
            this.pnl_LogSetting.Controls.Add(this.nUpDown_MaxDisplayLogCount, 1, 1);
            this.pnl_LogSetting.Controls.Add(this.LogRetentionPeriod, 0, 0);
            this.pnl_LogSetting.Controls.Add(this.nUpDown_LogRetentionPeriod, 1, 0);
            this.pnl_LogSetting.Controls.Add(this.cbox_ShowDebugLog, 1, 2);
            this.pnl_LogSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_LogSetting.Location = new System.Drawing.Point(3, 19);
            this.pnl_LogSetting.Name = "pnl_LogSetting";
            this.pnl_LogSetting.RowCount = 3;
            this.pnl_LogSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_LogSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_LogSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_LogSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_LogSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_LogSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnl_LogSetting.Size = new System.Drawing.Size(319, 127);
            this.pnl_LogSetting.TabIndex = 0;
            // 
            // lbl_ShowDebugLog
            // 
            this.lbl_ShowDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ShowDebugLog.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_ShowDebugLog.Location = new System.Drawing.Point(3, 84);
            this.lbl_ShowDebugLog.Name = "lbl_ShowDebugLog";
            this.lbl_ShowDebugLog.Size = new System.Drawing.Size(153, 43);
            this.lbl_ShowDebugLog.TabIndex = 10;
            this.lbl_ShowDebugLog.Text = "Show Debug Log";
            this.lbl_ShowDebugLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_MaxDisplayLogCount
            // 
            this.lbl_MaxDisplayLogCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_MaxDisplayLogCount.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_MaxDisplayLogCount.Location = new System.Drawing.Point(3, 42);
            this.lbl_MaxDisplayLogCount.Name = "lbl_MaxDisplayLogCount";
            this.lbl_MaxDisplayLogCount.Size = new System.Drawing.Size(153, 42);
            this.lbl_MaxDisplayLogCount.TabIndex = 9;
            this.lbl_MaxDisplayLogCount.Text = "Max Display Log Count";
            this.lbl_MaxDisplayLogCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nUpDown_MaxDisplayLogCount
            // 
            this.nUpDown_MaxDisplayLogCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_MaxDisplayLogCount.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.nUpDown_MaxDisplayLogCount.Location = new System.Drawing.Point(167, 50);
            this.nUpDown_MaxDisplayLogCount.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_MaxDisplayLogCount.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.nUpDown_MaxDisplayLogCount.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nUpDown_MaxDisplayLogCount.Name = "nUpDown_MaxDisplayLogCount";
            this.nUpDown_MaxDisplayLogCount.Size = new System.Drawing.Size(144, 23);
            this.nUpDown_MaxDisplayLogCount.TabIndex = 5;
            this.nUpDown_MaxDisplayLogCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nUpDown_MaxDisplayLogCount.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // LogRetentionPeriod
            // 
            this.LogRetentionPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogRetentionPeriod.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.LogRetentionPeriod.Location = new System.Drawing.Point(3, 0);
            this.LogRetentionPeriod.Name = "LogRetentionPeriod";
            this.LogRetentionPeriod.Size = new System.Drawing.Size(153, 42);
            this.LogRetentionPeriod.TabIndex = 0;
            this.LogRetentionPeriod.Text = "Log Retention Period(day)";
            this.LogRetentionPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nUpDown_LogRetentionPeriod
            // 
            this.nUpDown_LogRetentionPeriod.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_LogRetentionPeriod.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.nUpDown_LogRetentionPeriod.Location = new System.Drawing.Point(167, 8);
            this.nUpDown_LogRetentionPeriod.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_LogRetentionPeriod.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nUpDown_LogRetentionPeriod.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.nUpDown_LogRetentionPeriod.Name = "nUpDown_LogRetentionPeriod";
            this.nUpDown_LogRetentionPeriod.Size = new System.Drawing.Size(144, 23);
            this.nUpDown_LogRetentionPeriod.TabIndex = 4;
            this.nUpDown_LogRetentionPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nUpDown_LogRetentionPeriod.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // cbox_ShowDebugLog
            // 
            this.cbox_ShowDebugLog.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbox_ShowDebugLog.FormattingEnabled = true;
            this.cbox_ShowDebugLog.Location = new System.Drawing.Point(168, 94);
            this.cbox_ShowDebugLog.Name = "cbox_ShowDebugLog";
            this.cbox_ShowDebugLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbox_ShowDebugLog.Size = new System.Drawing.Size(142, 23);
            this.cbox_ShowDebugLog.TabIndex = 4;
            this.cbox_ShowDebugLog.Text = "Yes";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.groupBox1.Location = new System.Drawing.Point(557, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 112);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "4. Recipe Management";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_DefaultRecipeSavePath, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_OpenRecipeFolder, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(319, 90);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_DefaultRecipeSavePath
            // 
            this.lbl_DefaultRecipeSavePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_DefaultRecipeSavePath.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_DefaultRecipeSavePath.Location = new System.Drawing.Point(3, 0);
            this.lbl_DefaultRecipeSavePath.Name = "lbl_DefaultRecipeSavePath";
            this.lbl_DefaultRecipeSavePath.Size = new System.Drawing.Size(153, 45);
            this.lbl_DefaultRecipeSavePath.TabIndex = 0;
            this.lbl_DefaultRecipeSavePath.Text = "Default Recipe Save Path";
            this.lbl_DefaultRecipeSavePath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_OpenRecipeFolder
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btn_OpenRecipeFolder, 2);
            this.btn_OpenRecipeFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_OpenRecipeFolder.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btn_OpenRecipeFolder.Location = new System.Drawing.Point(3, 48);
            this.btn_OpenRecipeFolder.Name = "btn_OpenRecipeFolder";
            this.btn_OpenRecipeFolder.Size = new System.Drawing.Size(313, 39);
            this.btn_OpenRecipeFolder.TabIndex = 11;
            this.btn_OpenRecipeFolder.Text = "Open Recipe Folder";
            this.btn_OpenRecipeFolder.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.groupBox2.Location = new System.Drawing.Point(557, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 196);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "5. Safety InterLock";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.cbox_ModeChangeForceStop, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.cbox_AutoStopAlarmLevel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbox_AlarmAutoStop, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbox_DoorOpenInterlock, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_AlarmAutoStop, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbl_DoorOpenInterlock, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_AutoStopAlarmLevel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbl_ModeChangeForceStop, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(319, 174);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cbox_ModeChangeForceStop
            // 
            this.cbox_ModeChangeForceStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbox_ModeChangeForceStop.FormattingEnabled = true;
            this.cbox_ModeChangeForceStop.Location = new System.Drawing.Point(168, 141);
            this.cbox_ModeChangeForceStop.Name = "cbox_ModeChangeForceStop";
            this.cbox_ModeChangeForceStop.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbox_ModeChangeForceStop.Size = new System.Drawing.Size(142, 23);
            this.cbox_ModeChangeForceStop.TabIndex = 12;
            this.cbox_ModeChangeForceStop.Text = "True";
            // 
            // cbox_AutoStopAlarmLevel
            // 
            this.cbox_AutoStopAlarmLevel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbox_AutoStopAlarmLevel.FormattingEnabled = true;
            this.cbox_AutoStopAlarmLevel.Location = new System.Drawing.Point(168, 97);
            this.cbox_AutoStopAlarmLevel.Name = "cbox_AutoStopAlarmLevel";
            this.cbox_AutoStopAlarmLevel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbox_AutoStopAlarmLevel.Size = new System.Drawing.Size(142, 23);
            this.cbox_AutoStopAlarmLevel.TabIndex = 11;
            this.cbox_AutoStopAlarmLevel.Text = "ERROR";
            // 
            // cbox_AlarmAutoStop
            // 
            this.cbox_AlarmAutoStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbox_AlarmAutoStop.FormattingEnabled = true;
            this.cbox_AlarmAutoStop.Location = new System.Drawing.Point(168, 54);
            this.cbox_AlarmAutoStop.Name = "cbox_AlarmAutoStop";
            this.cbox_AlarmAutoStop.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbox_AlarmAutoStop.Size = new System.Drawing.Size(142, 23);
            this.cbox_AlarmAutoStop.TabIndex = 10;
            this.cbox_AlarmAutoStop.Text = "True";
            // 
            // cbox_DoorOpenInterlock
            // 
            this.cbox_DoorOpenInterlock.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbox_DoorOpenInterlock.FormattingEnabled = true;
            this.cbox_DoorOpenInterlock.Location = new System.Drawing.Point(168, 11);
            this.cbox_DoorOpenInterlock.Name = "cbox_DoorOpenInterlock";
            this.cbox_DoorOpenInterlock.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbox_DoorOpenInterlock.Size = new System.Drawing.Size(142, 23);
            this.cbox_DoorOpenInterlock.TabIndex = 9;
            this.cbox_DoorOpenInterlock.Text = "True";
            // 
            // lbl_AlarmAutoStop
            // 
            this.lbl_AlarmAutoStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_AlarmAutoStop.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_AlarmAutoStop.Location = new System.Drawing.Point(3, 43);
            this.lbl_AlarmAutoStop.Name = "lbl_AlarmAutoStop";
            this.lbl_AlarmAutoStop.Size = new System.Drawing.Size(153, 43);
            this.lbl_AlarmAutoStop.TabIndex = 2;
            this.lbl_AlarmAutoStop.Text = "Alarm Auto Stop";
            this.lbl_AlarmAutoStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DoorOpenInterlock
            // 
            this.lbl_DoorOpenInterlock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_DoorOpenInterlock.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_DoorOpenInterlock.Location = new System.Drawing.Point(3, 0);
            this.lbl_DoorOpenInterlock.Name = "lbl_DoorOpenInterlock";
            this.lbl_DoorOpenInterlock.Size = new System.Drawing.Size(153, 43);
            this.lbl_DoorOpenInterlock.TabIndex = 0;
            this.lbl_DoorOpenInterlock.Text = "Door Open Interlock";
            this.lbl_DoorOpenInterlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AutoStopAlarmLevel
            // 
            this.lbl_AutoStopAlarmLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_AutoStopAlarmLevel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_AutoStopAlarmLevel.Location = new System.Drawing.Point(3, 86);
            this.lbl_AutoStopAlarmLevel.Name = "lbl_AutoStopAlarmLevel";
            this.lbl_AutoStopAlarmLevel.Size = new System.Drawing.Size(153, 43);
            this.lbl_AutoStopAlarmLevel.TabIndex = 3;
            this.lbl_AutoStopAlarmLevel.Text = "Auto Stop Alarm Level";
            this.lbl_AutoStopAlarmLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ModeChangeForceStop
            // 
            this.lbl_ModeChangeForceStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ModeChangeForceStop.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_ModeChangeForceStop.Location = new System.Drawing.Point(3, 129);
            this.lbl_ModeChangeForceStop.Name = "lbl_ModeChangeForceStop";
            this.lbl_ModeChangeForceStop.Size = new System.Drawing.Size(153, 45);
            this.lbl_ModeChangeForceStop.TabIndex = 1;
            this.lbl_ModeChangeForceStop.Text = "Mode Change Force Stop";
            this.lbl_ModeChangeForceStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel3);
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.groupBox3.Location = new System.Drawing.Point(557, 382);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(325, 196);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "6. Lamp Tower Status Mapping";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.cbox_MaintenanceLampStatus, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.cbox_AlarmLampStatus, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.cbox_RunLampStatus, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.cbox_IdleLampStatus, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_RunLampStatus, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbl_IdleLampStatus, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lbl_AlarmLampStatus, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.lbl_MaintenanceLampStatus, 0, 3);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(319, 174);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // cbox_MaintenanceLampStatus
            // 
            this.cbox_MaintenanceLampStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbox_MaintenanceLampStatus.FormattingEnabled = true;
            this.cbox_MaintenanceLampStatus.Location = new System.Drawing.Point(168, 141);
            this.cbox_MaintenanceLampStatus.Name = "cbox_MaintenanceLampStatus";
            this.cbox_MaintenanceLampStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbox_MaintenanceLampStatus.Size = new System.Drawing.Size(142, 23);
            this.cbox_MaintenanceLampStatus.TabIndex = 12;
            this.cbox_MaintenanceLampStatus.Text = "Yellow";
            // 
            // cbox_AlarmLampStatus
            // 
            this.cbox_AlarmLampStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbox_AlarmLampStatus.FormattingEnabled = true;
            this.cbox_AlarmLampStatus.Location = new System.Drawing.Point(168, 96);
            this.cbox_AlarmLampStatus.Name = "cbox_AlarmLampStatus";
            this.cbox_AlarmLampStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbox_AlarmLampStatus.Size = new System.Drawing.Size(142, 23);
            this.cbox_AlarmLampStatus.TabIndex = 11;
            this.cbox_AlarmLampStatus.Text = "Red";
            // 
            // cbox_RunLampStatus
            // 
            this.cbox_RunLampStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbox_RunLampStatus.FormattingEnabled = true;
            this.cbox_RunLampStatus.Location = new System.Drawing.Point(168, 53);
            this.cbox_RunLampStatus.Name = "cbox_RunLampStatus";
            this.cbox_RunLampStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbox_RunLampStatus.Size = new System.Drawing.Size(142, 23);
            this.cbox_RunLampStatus.TabIndex = 10;
            this.cbox_RunLampStatus.Text = "Green";
            // 
            // cbox_IdleLampStatus
            // 
            this.cbox_IdleLampStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbox_IdleLampStatus.FormattingEnabled = true;
            this.cbox_IdleLampStatus.Location = new System.Drawing.Point(168, 10);
            this.cbox_IdleLampStatus.Name = "cbox_IdleLampStatus";
            this.cbox_IdleLampStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbox_IdleLampStatus.Size = new System.Drawing.Size(142, 23);
            this.cbox_IdleLampStatus.TabIndex = 9;
            this.cbox_IdleLampStatus.Text = "Yellow";
            // 
            // lbl_RunLampStatus
            // 
            this.lbl_RunLampStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RunLampStatus.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_RunLampStatus.Location = new System.Drawing.Point(3, 43);
            this.lbl_RunLampStatus.Name = "lbl_RunLampStatus";
            this.lbl_RunLampStatus.Size = new System.Drawing.Size(153, 43);
            this.lbl_RunLampStatus.TabIndex = 2;
            this.lbl_RunLampStatus.Text = "Run Lamp Status";
            this.lbl_RunLampStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_IdleLampStatus
            // 
            this.lbl_IdleLampStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_IdleLampStatus.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_IdleLampStatus.Location = new System.Drawing.Point(3, 0);
            this.lbl_IdleLampStatus.Name = "lbl_IdleLampStatus";
            this.lbl_IdleLampStatus.Size = new System.Drawing.Size(153, 43);
            this.lbl_IdleLampStatus.TabIndex = 0;
            this.lbl_IdleLampStatus.Text = "Idle Lamp Status";
            this.lbl_IdleLampStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AlarmLampStatus
            // 
            this.lbl_AlarmLampStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_AlarmLampStatus.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_AlarmLampStatus.Location = new System.Drawing.Point(3, 86);
            this.lbl_AlarmLampStatus.Name = "lbl_AlarmLampStatus";
            this.lbl_AlarmLampStatus.Size = new System.Drawing.Size(153, 43);
            this.lbl_AlarmLampStatus.TabIndex = 3;
            this.lbl_AlarmLampStatus.Text = "Alarm Lamp Status ";
            this.lbl_AlarmLampStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_MaintenanceLampStatus
            // 
            this.lbl_MaintenanceLampStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_MaintenanceLampStatus.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lbl_MaintenanceLampStatus.Location = new System.Drawing.Point(3, 129);
            this.lbl_MaintenanceLampStatus.Name = "lbl_MaintenanceLampStatus";
            this.lbl_MaintenanceLampStatus.Size = new System.Drawing.Size(153, 45);
            this.lbl_MaintenanceLampStatus.TabIndex = 1;
            this.lbl_MaintenanceLampStatus.Text = "Maintenance Lamp Status";
            this.lbl_MaintenanceLampStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_ApplyCancel
            // 
            this.pnl_ApplyCancel.ColumnCount = 2;
            this.pnl_ApplyCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_ApplyCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_ApplyCancel.Controls.Add(this.btn_Cancel, 1, 0);
            this.pnl_ApplyCancel.Controls.Add(this.btn_Apply, 0, 0);
            this.pnl_ApplyCancel.Location = new System.Drawing.Point(682, 634);
            this.pnl_ApplyCancel.Name = "pnl_ApplyCancel";
            this.pnl_ApplyCancel.RowCount = 1;
            this.pnl_ApplyCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_ApplyCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_ApplyCancel.Size = new System.Drawing.Size(200, 51);
            this.pnl_ApplyCancel.TabIndex = 5;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Cancel.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btn_Cancel.Location = new System.Drawing.Point(103, 3);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(94, 45);
            this.btn_Cancel.TabIndex = 13;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Apply
            // 
            this.btn_Apply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Apply.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btn_Apply.Location = new System.Drawing.Point(3, 3);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(94, 45);
            this.btn_Apply.TabIndex = 12;
            this.btn_Apply.Text = "Apply";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // SettingGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_ApplyCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBox_LogSetting);
            this.Controls.Add(this.grpBox_EtherCAT_Setting);
            this.Controls.Add(this.grpbox_RobotParameter);
            this.Name = "SettingGUI";
            this.Size = new System.Drawing.Size(1000, 750);
            this.Load += new System.EventHandler(this.SettingGUI_Load);
            this.grpbox_RobotParameter.ResumeLayout(false);
            this.pnl_RobotParameter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Velo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_MaxVelo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Decel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Accel)).EndInit();
            this.grpBox_EtherCAT_Setting.ResumeLayout(false);
            this.pnl_EtherCAT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_TimeOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_RetryCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_ReadCycle)).EndInit();
            this.grpBox_LogSetting.ResumeLayout(false);
            this.pnl_LogSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_MaxDisplayLogCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_LogRetentionPeriod)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.pnl_ApplyCancel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbox_RobotParameter;
        private System.Windows.Forms.TableLayoutPanel pnl_RobotParameter;
        private System.Windows.Forms.NumericUpDown nUpDown_Velo;
        private System.Windows.Forms.NumericUpDown nUpDown_MaxVelo;
        private System.Windows.Forms.NumericUpDown nUpDown_Decel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Accel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nUpDown_Accel;
        private System.Windows.Forms.Button btn_ParameterSet;
        private System.Windows.Forms.GroupBox grpBox_EtherCAT_Setting;
        private System.Windows.Forms.TableLayoutPanel pnl_EtherCAT;
        private System.Windows.Forms.NumericUpDown nUpDown_TimeOut;
        private System.Windows.Forms.NumericUpDown nUpDown_RetryCount;
        private System.Windows.Forms.Label lbl_EtherCATreadCycle;
        private System.Windows.Forms.NumericUpDown nUpDown_ReadCycle;
        private System.Windows.Forms.Label lbl_ConnectionTimeOut;
        private System.Windows.Forms.Label lbl_ReconnectRetryCount;
        private System.Windows.Forms.GroupBox grpBox_LogSetting;
        private System.Windows.Forms.TableLayoutPanel pnl_LogSetting;
        private System.Windows.Forms.Label lbl_ShowDebugLog;
        private System.Windows.Forms.Label lbl_MaxDisplayLogCount;
        private System.Windows.Forms.NumericUpDown nUpDown_MaxDisplayLogCount;
        private System.Windows.Forms.Label LogRetentionPeriod;
        private System.Windows.Forms.NumericUpDown nUpDown_LogRetentionPeriod;
        private System.Windows.Forms.ComboBox cbox_ShowDebugLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_DefaultRecipeSavePath;
        private System.Windows.Forms.Button btn_OpenRecipeFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbl_AlarmAutoStop;
        private System.Windows.Forms.Label lbl_DoorOpenInterlock;
        private System.Windows.Forms.Label lbl_AutoStopAlarmLevel;
        private System.Windows.Forms.Label lbl_ModeChangeForceStop;
        private System.Windows.Forms.ComboBox cbox_ModeChangeForceStop;
        private System.Windows.Forms.ComboBox cbox_AutoStopAlarmLevel;
        private System.Windows.Forms.ComboBox cbox_AlarmAutoStop;
        private System.Windows.Forms.ComboBox cbox_DoorOpenInterlock;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox cbox_MaintenanceLampStatus;
        private System.Windows.Forms.ComboBox cbox_AlarmLampStatus;
        private System.Windows.Forms.ComboBox cbox_RunLampStatus;
        private System.Windows.Forms.ComboBox cbox_IdleLampStatus;
        private System.Windows.Forms.Label lbl_RunLampStatus;
        private System.Windows.Forms.Label lbl_IdleLampStatus;
        private System.Windows.Forms.Label lbl_AlarmLampStatus;
        private System.Windows.Forms.Label lbl_MaintenanceLampStatus;
        private System.Windows.Forms.TableLayoutPanel pnl_ApplyCancel;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Apply;
    }
}
