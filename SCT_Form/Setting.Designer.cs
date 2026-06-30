namespace SCT_Form
{
    partial class Setting
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
            this.grpbox_RobotParameter.SuspendLayout();
            this.pnl_RobotParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Velo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_MaxVelo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Decel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Accel)).BeginInit();
            this.SuspendLayout();
            // 
            // grpbox_RobotParameter
            // 
            this.grpbox_RobotParameter.Controls.Add(this.pnl_RobotParameter);
            this.grpbox_RobotParameter.Location = new System.Drawing.Point(12, 12);
            this.grpbox_RobotParameter.Name = "grpbox_RobotParameter";
            this.grpbox_RobotParameter.Size = new System.Drawing.Size(325, 196);
            this.grpbox_RobotParameter.TabIndex = 0;
            this.grpbox_RobotParameter.TabStop = false;
            this.grpbox_RobotParameter.Text = "Transfer Robot Parameter";
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
            this.pnl_RobotParameter.Location = new System.Drawing.Point(3, 17);
            this.pnl_RobotParameter.Name = "pnl_RobotParameter";
            this.pnl_RobotParameter.RowCount = 5;
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_RobotParameter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnl_RobotParameter.Size = new System.Drawing.Size(319, 176);
            this.pnl_RobotParameter.TabIndex = 0;
            // 
            // nUpDown_Velo
            // 
            this.nUpDown_Velo.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_Velo.Location = new System.Drawing.Point(167, 113);
            this.nUpDown_Velo.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_Velo.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nUpDown_Velo.Name = "nUpDown_Velo";
            this.nUpDown_Velo.Size = new System.Drawing.Size(144, 21);
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
            this.nUpDown_MaxVelo.Location = new System.Drawing.Point(167, 78);
            this.nUpDown_MaxVelo.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_MaxVelo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nUpDown_MaxVelo.Name = "nUpDown_MaxVelo";
            this.nUpDown_MaxVelo.Size = new System.Drawing.Size(144, 21);
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
            this.nUpDown_Decel.Location = new System.Drawing.Point(167, 43);
            this.nUpDown_Decel.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_Decel.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nUpDown_Decel.Name = "nUpDown_Decel";
            this.nUpDown_Decel.Size = new System.Drawing.Size(144, 21);
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
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 35);
            this.label2.TabIndex = 2;
            this.label2.Text = "Deceleration (감속도) :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Accel
            // 
            this.lbl_Accel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Accel.Location = new System.Drawing.Point(3, 0);
            this.lbl_Accel.Name = "lbl_Accel";
            this.lbl_Accel.Size = new System.Drawing.Size(153, 35);
            this.lbl_Accel.TabIndex = 0;
            this.lbl_Accel.Text = "Acceleration (가속도) :";
            this.lbl_Accel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 35);
            this.label3.TabIndex = 3;
            this.label3.Text = "Max Velocity (최대 속도) :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Veloctiy (속도) :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nUpDown_Accel
            // 
            this.nUpDown_Accel.Dock = System.Windows.Forms.DockStyle.Top;
            this.nUpDown_Accel.Location = new System.Drawing.Point(167, 8);
            this.nUpDown_Accel.Margin = new System.Windows.Forms.Padding(8);
            this.nUpDown_Accel.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nUpDown_Accel.Name = "nUpDown_Accel";
            this.nUpDown_Accel.Size = new System.Drawing.Size(144, 21);
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
            this.btn_ParameterSet.Location = new System.Drawing.Point(3, 143);
            this.btn_ParameterSet.Name = "btn_ParameterSet";
            this.btn_ParameterSet.Size = new System.Drawing.Size(313, 30);
            this.btn_ParameterSet.TabIndex = 8;
            this.btn_ParameterSet.Text = "Transfer Robot Parameter Set";
            this.btn_ParameterSet.UseVisualStyleBackColor = true;
            this.btn_ParameterSet.Click += new System.EventHandler(this.btn_ParameterSet_Click);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grpbox_RobotParameter);
            this.Name = "Setting";
            this.Text = "Settings";
            this.grpbox_RobotParameter.ResumeLayout(false);
            this.pnl_RobotParameter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Velo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_MaxVelo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Decel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUpDown_Accel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbox_RobotParameter;
        private System.Windows.Forms.TableLayoutPanel pnl_RobotParameter;
        private System.Windows.Forms.Label lbl_Accel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nUpDown_Velo;
        private System.Windows.Forms.NumericUpDown nUpDown_MaxVelo;
        private System.Windows.Forms.NumericUpDown nUpDown_Decel;
        private System.Windows.Forms.NumericUpDown nUpDown_Accel;
        private System.Windows.Forms.Button btn_ParameterSet;
    }
}