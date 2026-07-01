namespace SCT_Form
{
    partial class logPanel
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
            this.SystemLog = new System.Windows.Forms.GroupBox();
            this.LogView = new System.Windows.Forms.ListView();
            this.SystemLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // SystemLog
            // 
            this.SystemLog.Controls.Add(this.LogView);
            this.SystemLog.Location = new System.Drawing.Point(94, 162);
            this.SystemLog.Name = "SystemLog";
            this.SystemLog.Size = new System.Drawing.Size(773, 194);
            this.SystemLog.TabIndex = 40;
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
            this.LogView.Size = new System.Drawing.Size(767, 174);
            this.LogView.TabIndex = 0;
            this.LogView.UseCompatibleStateImageBehavior = false;
            this.LogView.View = System.Windows.Forms.View.Details;
            // 
            // logPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SystemLog);
            this.Name = "logPanel";
            this.Size = new System.Drawing.Size(960, 518);
            this.SystemLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox SystemLog;
        internal System.Windows.Forms.ListView LogView;
    }
}
