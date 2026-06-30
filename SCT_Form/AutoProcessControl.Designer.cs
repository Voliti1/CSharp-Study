namespace SCT_Form
{
    partial class AutoProcessControl
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
            this.pnl_ControlContainer = new System.Windows.Forms.Panel();
            this.grpbox_AutoProcessControl = new System.Windows.Forms.GroupBox();
            this.panel_AutoProcess = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_CurrentStep = new System.Windows.Forms.Label();
            this.lbl_ProcessControl = new System.Windows.Forms.Label();
            this.lbl_RecipeSetting = new System.Windows.Forms.Label();
            this.pnl_RecipeSetting = new System.Windows.Forms.TableLayoutPanel();
            this.cbox_RecipeSetting = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnl_ProcessControl = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ProcessPause = new System.Windows.Forms.Button();
            this.btn_ProcessStop = new System.Windows.Forms.Button();
            this.btn_ProcessStart = new System.Windows.Forms.Button();
            this.pnl_CurrentStep = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_CurrentProcessTime = new System.Windows.Forms.Label();
            this.lbl_ProcessStatus = new System.Windows.Forms.Label();
            this.lbl_ProcessTime = new System.Windows.Forms.Label();
            this.lbl_CurrentStatus = new System.Windows.Forms.Label();
            this.pnl_ControlContainer.SuspendLayout();
            this.grpbox_AutoProcessControl.SuspendLayout();
            this.panel_AutoProcess.SuspendLayout();
            this.pnl_RecipeSetting.SuspendLayout();
            this.pnl_ProcessControl.SuspendLayout();
            this.pnl_CurrentStep.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ControlContainer
            // 
            this.pnl_ControlContainer.Controls.Add(this.grpbox_AutoProcessControl);
            this.pnl_ControlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnl_ControlContainer.Name = "pnl_ControlContainer";
            this.pnl_ControlContainer.Size = new System.Drawing.Size(582, 134);
            this.pnl_ControlContainer.TabIndex = 48;
            // 
            // grpbox_AutoProcessControl
            // 
            this.grpbox_AutoProcessControl.Controls.Add(this.panel_AutoProcess);
            this.grpbox_AutoProcessControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpbox_AutoProcessControl.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grpbox_AutoProcessControl.Location = new System.Drawing.Point(0, 0);
            this.grpbox_AutoProcessControl.Name = "grpbox_AutoProcessControl";
            this.grpbox_AutoProcessControl.Size = new System.Drawing.Size(582, 134);
            this.grpbox_AutoProcessControl.TabIndex = 48;
            this.grpbox_AutoProcessControl.TabStop = false;
            this.grpbox_AutoProcessControl.Text = "Auto Process Control";
            // 
            // panel_AutoProcess
            // 
            this.panel_AutoProcess.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel_AutoProcess.ColumnCount = 3;
            this.panel_AutoProcess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panel_AutoProcess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panel_AutoProcess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panel_AutoProcess.Controls.Add(this.lbl_CurrentStep, 2, 0);
            this.panel_AutoProcess.Controls.Add(this.lbl_ProcessControl, 1, 0);
            this.panel_AutoProcess.Controls.Add(this.lbl_RecipeSetting, 0, 0);
            this.panel_AutoProcess.Controls.Add(this.pnl_RecipeSetting, 0, 1);
            this.panel_AutoProcess.Controls.Add(this.pnl_ProcessControl, 1, 1);
            this.panel_AutoProcess.Controls.Add(this.pnl_CurrentStep, 2, 1);
            this.panel_AutoProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_AutoProcess.Location = new System.Drawing.Point(3, 17);
            this.panel_AutoProcess.Name = "panel_AutoProcess";
            this.panel_AutoProcess.RowCount = 2;
            this.panel_AutoProcess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.panel_AutoProcess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.panel_AutoProcess.Size = new System.Drawing.Size(576, 114);
            this.panel_AutoProcess.TabIndex = 0;
            // 
            // lbl_CurrentStep
            // 
            this.lbl_CurrentStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CurrentStep.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_CurrentStep.Location = new System.Drawing.Point(386, 1);
            this.lbl_CurrentStep.Name = "lbl_CurrentStep";
            this.lbl_CurrentStep.Size = new System.Drawing.Size(186, 11);
            this.lbl_CurrentStep.TabIndex = 2;
            this.lbl_CurrentStep.Text = "Current Step";
            this.lbl_CurrentStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ProcessControl
            // 
            this.lbl_ProcessControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProcessControl.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_ProcessControl.Location = new System.Drawing.Point(195, 1);
            this.lbl_ProcessControl.Name = "lbl_ProcessControl";
            this.lbl_ProcessControl.Size = new System.Drawing.Size(184, 11);
            this.lbl_ProcessControl.TabIndex = 1;
            this.lbl_ProcessControl.Text = "Process Control";
            this.lbl_ProcessControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RecipeSetting
            // 
            this.lbl_RecipeSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RecipeSetting.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_RecipeSetting.Location = new System.Drawing.Point(4, 1);
            this.lbl_RecipeSetting.Name = "lbl_RecipeSetting";
            this.lbl_RecipeSetting.Size = new System.Drawing.Size(184, 11);
            this.lbl_RecipeSetting.TabIndex = 0;
            this.lbl_RecipeSetting.Text = "Recipe Setting";
            this.lbl_RecipeSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_RecipeSetting
            // 
            this.pnl_RecipeSetting.ColumnCount = 1;
            this.pnl_RecipeSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_RecipeSetting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_RecipeSetting.Controls.Add(this.cbox_RecipeSetting, 0, 1);
            this.pnl_RecipeSetting.Controls.Add(this.label3, 0, 0);
            this.pnl_RecipeSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_RecipeSetting.Location = new System.Drawing.Point(4, 16);
            this.pnl_RecipeSetting.Name = "pnl_RecipeSetting";
            this.pnl_RecipeSetting.RowCount = 2;
            this.pnl_RecipeSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_RecipeSetting.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_RecipeSetting.Size = new System.Drawing.Size(184, 94);
            this.pnl_RecipeSetting.TabIndex = 3;
            // 
            // cbox_RecipeSetting
            // 
            this.cbox_RecipeSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbox_RecipeSetting.FormattingEnabled = true;
            this.cbox_RecipeSetting.Location = new System.Drawing.Point(3, 50);
            this.cbox_RecipeSetting.Name = "cbox_RecipeSetting";
            this.cbox_RecipeSetting.Size = new System.Drawing.Size(178, 20);
            this.cbox_RecipeSetting.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 47);
            this.label3.TabIndex = 1;
            this.label3.Text = "Recipe Selection";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_ProcessControl
            // 
            this.pnl_ProcessControl.ColumnCount = 2;
            this.pnl_ProcessControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_ProcessControl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_ProcessControl.Controls.Add(this.btn_ProcessPause, 0, 1);
            this.pnl_ProcessControl.Controls.Add(this.btn_ProcessStop, 1, 0);
            this.pnl_ProcessControl.Controls.Add(this.btn_ProcessStart, 0, 0);
            this.pnl_ProcessControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_ProcessControl.Location = new System.Drawing.Point(195, 16);
            this.pnl_ProcessControl.Name = "pnl_ProcessControl";
            this.pnl_ProcessControl.RowCount = 2;
            this.pnl_ProcessControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_ProcessControl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_ProcessControl.Size = new System.Drawing.Size(184, 94);
            this.pnl_ProcessControl.TabIndex = 4;
            // 
            // btn_ProcessPause
            // 
            this.pnl_ProcessControl.SetColumnSpan(this.btn_ProcessPause, 2);
            this.btn_ProcessPause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProcessPause.Location = new System.Drawing.Point(3, 50);
            this.btn_ProcessPause.Name = "btn_ProcessPause";
            this.btn_ProcessPause.Size = new System.Drawing.Size(178, 41);
            this.btn_ProcessPause.TabIndex = 2;
            this.btn_ProcessPause.Text = "ABORT / PAUSE";
            this.btn_ProcessPause.UseVisualStyleBackColor = true;
            // 
            // btn_ProcessStop
            // 
            this.btn_ProcessStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProcessStop.Location = new System.Drawing.Point(95, 3);
            this.btn_ProcessStop.Name = "btn_ProcessStop";
            this.btn_ProcessStop.Size = new System.Drawing.Size(86, 41);
            this.btn_ProcessStop.TabIndex = 1;
            this.btn_ProcessStop.Text = "STOP";
            this.btn_ProcessStop.UseVisualStyleBackColor = true;
            // 
            // btn_ProcessStart
            // 
            this.btn_ProcessStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProcessStart.Location = new System.Drawing.Point(3, 3);
            this.btn_ProcessStart.Name = "btn_ProcessStart";
            this.btn_ProcessStart.Size = new System.Drawing.Size(86, 41);
            this.btn_ProcessStart.TabIndex = 0;
            this.btn_ProcessStart.Text = "START";
            this.btn_ProcessStart.UseVisualStyleBackColor = true;
            // 
            // pnl_CurrentStep
            // 
            this.pnl_CurrentStep.ColumnCount = 2;
            this.pnl_CurrentStep.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.pnl_CurrentStep.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnl_CurrentStep.Controls.Add(this.lbl_CurrentProcessTime, 1, 1);
            this.pnl_CurrentStep.Controls.Add(this.lbl_ProcessStatus, 0, 0);
            this.pnl_CurrentStep.Controls.Add(this.lbl_ProcessTime, 0, 1);
            this.pnl_CurrentStep.Controls.Add(this.lbl_CurrentStatus, 1, 0);
            this.pnl_CurrentStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_CurrentStep.Location = new System.Drawing.Point(386, 16);
            this.pnl_CurrentStep.Name = "pnl_CurrentStep";
            this.pnl_CurrentStep.RowCount = 2;
            this.pnl_CurrentStep.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_CurrentStep.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_CurrentStep.Size = new System.Drawing.Size(186, 94);
            this.pnl_CurrentStep.TabIndex = 5;
            // 
            // lbl_CurrentProcessTime
            // 
            this.lbl_CurrentProcessTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CurrentProcessTime.Location = new System.Drawing.Point(77, 47);
            this.lbl_CurrentProcessTime.Name = "lbl_CurrentProcessTime";
            this.lbl_CurrentProcessTime.Size = new System.Drawing.Size(106, 47);
            this.lbl_CurrentProcessTime.TabIndex = 3;
            // 
            // lbl_ProcessStatus
            // 
            this.lbl_ProcessStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProcessStatus.Location = new System.Drawing.Point(3, 0);
            this.lbl_ProcessStatus.Name = "lbl_ProcessStatus";
            this.lbl_ProcessStatus.Size = new System.Drawing.Size(68, 47);
            this.lbl_ProcessStatus.TabIndex = 0;
            this.lbl_ProcessStatus.Text = "STATUS";
            this.lbl_ProcessStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ProcessTime
            // 
            this.lbl_ProcessTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProcessTime.Location = new System.Drawing.Point(3, 47);
            this.lbl_ProcessTime.Name = "lbl_ProcessTime";
            this.lbl_ProcessTime.Size = new System.Drawing.Size(68, 47);
            this.lbl_ProcessTime.TabIndex = 1;
            this.lbl_ProcessTime.Text = "PROCESS TIME";
            this.lbl_ProcessTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CurrentStatus
            // 
            this.lbl_CurrentStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CurrentStatus.Location = new System.Drawing.Point(77, 0);
            this.lbl_CurrentStatus.Name = "lbl_CurrentStatus";
            this.lbl_CurrentStatus.Size = new System.Drawing.Size(106, 47);
            this.lbl_CurrentStatus.TabIndex = 2;
            // 
            // AutoProcessControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_ControlContainer);
            this.Name = "AutoProcessControl";
            this.Size = new System.Drawing.Size(583, 134);
            this.pnl_ControlContainer.ResumeLayout(false);
            this.grpbox_AutoProcessControl.ResumeLayout(false);
            this.panel_AutoProcess.ResumeLayout(false);
            this.pnl_RecipeSetting.ResumeLayout(false);
            this.pnl_ProcessControl.ResumeLayout(false);
            this.pnl_CurrentStep.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_ControlContainer;
        private System.Windows.Forms.GroupBox grpbox_AutoProcessControl;
        private System.Windows.Forms.TableLayoutPanel panel_AutoProcess;
        private System.Windows.Forms.Label lbl_CurrentStep;
        private System.Windows.Forms.Label lbl_ProcessControl;
        private System.Windows.Forms.Label lbl_RecipeSetting;
        private System.Windows.Forms.TableLayoutPanel pnl_RecipeSetting;
        private System.Windows.Forms.ComboBox cbox_RecipeSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel pnl_ProcessControl;
        private System.Windows.Forms.Button btn_ProcessPause;
        private System.Windows.Forms.Button btn_ProcessStop;
        private System.Windows.Forms.Button btn_ProcessStart;
        private System.Windows.Forms.TableLayoutPanel pnl_CurrentStep;
        private System.Windows.Forms.Label lbl_CurrentProcessTime;
        private System.Windows.Forms.Label lbl_ProcessStatus;
        private System.Windows.Forms.Label lbl_ProcessTime;
        private System.Windows.Forms.Label lbl_CurrentStatus;
    }
}
