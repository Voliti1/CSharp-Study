namespace _20260624_WinForm
{
    partial class Form1
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
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnCustomForm = new System.Windows.Forms.Button();
            this.btnCustomForm2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(15, 12);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(226, 115);
            this.btnRetry.TabIndex = 0;
            this.btnRetry.Text = "Retry";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnCustomForm
            // 
            this.btnCustomForm.Location = new System.Drawing.Point(247, 12);
            this.btnCustomForm.Name = "btnCustomForm";
            this.btnCustomForm.Size = new System.Drawing.Size(226, 115);
            this.btnCustomForm.TabIndex = 1;
            this.btnCustomForm.Text = "CustomForm";
            this.btnCustomForm.UseVisualStyleBackColor = true;
            this.btnCustomForm.Click += new System.EventHandler(this.btnCustomForm_Click);
            // 
            // btnCustomForm2
            // 
            this.btnCustomForm2.Location = new System.Drawing.Point(479, 12);
            this.btnCustomForm2.Name = "btnCustomForm2";
            this.btnCustomForm2.Size = new System.Drawing.Size(226, 115);
            this.btnCustomForm2.TabIndex = 2;
            this.btnCustomForm2.Text = "CustomForm2";
            this.btnCustomForm2.UseVisualStyleBackColor = true;
            this.btnCustomForm2.Click += new System.EventHandler(this.btnCustomForm2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 450);
            this.Controls.Add(this.btnCustomForm2);
            this.Controls.Add(this.btnCustomForm);
            this.Controls.Add(this.btnRetry);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Button btnCustomForm;
        private System.Windows.Forms.Button btnCustomForm2;
    }
}

