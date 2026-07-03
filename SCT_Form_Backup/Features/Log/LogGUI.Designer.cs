namespace SCT_Form
{
    partial class LogGUI
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
            this.pnl_LogGUI_Menu = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Alarm = new System.Windows.Forms.Button();
            this.btn_FullLog = new System.Windows.Forms.Button();
            this.pnl_LogGUI = new System.Windows.Forms.TableLayoutPanel();
            this.Log = new System.Windows.Forms.GroupBox();
            this.LogView = new System.Windows.Forms.ListView();
            this.pnl_Button = new System.Windows.Forms.TableLayoutPanel();
            this.btn_LogDelete = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.btn_ClearFilter = new System.Windows.Forms.Button();
            this.pnl_Filter = new System.Windows.Forms.TableLayoutPanel();
            this.txtbox_3 = new System.Windows.Forms.TextBox();
            this.cbox_Column3 = new System.Windows.Forms.ComboBox();
            this.txtbox_2 = new System.Windows.Forms.TextBox();
            this.cbox_Column2 = new System.Windows.Forms.ComboBox();
            this.cbox_Column1 = new System.Windows.Forms.ComboBox();
            this.txtbox_1 = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_SelectMode = new System.Windows.Forms.Button();
            this.btn_RandomAlarm = new System.Windows.Forms.Button();
            this.pnl_LogGUI_Menu.SuspendLayout();
            this.pnl_LogGUI.SuspendLayout();
            this.Log.SuspendLayout();
            this.pnl_Button.SuspendLayout();
            this.pnl_Filter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_LogGUI_Menu
            // 
            this.pnl_LogGUI_Menu.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnl_LogGUI_Menu.ColumnCount = 4;
            this.pnl_LogGUI_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_LogGUI_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_LogGUI_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.pnl_LogGUI_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_LogGUI_Menu.Controls.Add(this.btn_Alarm, 0, 0);
            this.pnl_LogGUI_Menu.Controls.Add(this.btn_FullLog, 1, 0);
            this.pnl_LogGUI_Menu.Controls.Add(this.btn_RandomAlarm, 3, 0);
            this.pnl_LogGUI_Menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_LogGUI_Menu.Location = new System.Drawing.Point(0, 0);
            this.pnl_LogGUI_Menu.Name = "pnl_LogGUI_Menu";
            this.pnl_LogGUI_Menu.RowCount = 1;
            this.pnl_LogGUI_Menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_LogGUI_Menu.Size = new System.Drawing.Size(1000, 50);
            this.pnl_LogGUI_Menu.TabIndex = 41;
            // 
            // btn_Alarm
            // 
            this.btn_Alarm.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Alarm.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Alarm.FlatAppearance.BorderSize = 2;
            this.btn_Alarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Alarm.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_Alarm.ForeColor = System.Drawing.Color.White;
            this.btn_Alarm.Location = new System.Drawing.Point(1, 1);
            this.btn_Alarm.Margin = new System.Windows.Forms.Padding(1);
            this.btn_Alarm.Name = "btn_Alarm";
            this.btn_Alarm.Size = new System.Drawing.Size(198, 48);
            this.btn_Alarm.TabIndex = 37;
            this.btn_Alarm.Text = "Alarm";
            this.btn_Alarm.UseVisualStyleBackColor = false;
            // 
            // btn_FullLog
            // 
            this.btn_FullLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_FullLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_FullLog.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_FullLog.ForeColor = System.Drawing.Color.Silver;
            this.btn_FullLog.Location = new System.Drawing.Point(201, 1);
            this.btn_FullLog.Margin = new System.Windows.Forms.Padding(1);
            this.btn_FullLog.Name = "btn_FullLog";
            this.btn_FullLog.Size = new System.Drawing.Size(198, 48);
            this.btn_FullLog.TabIndex = 38;
            this.btn_FullLog.Text = "Full Log";
            this.btn_FullLog.UseVisualStyleBackColor = true;
            // 
            // pnl_LogGUI
            // 
            this.pnl_LogGUI.ColumnCount = 2;
            this.pnl_LogGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.pnl_LogGUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_LogGUI.Controls.Add(this.Log, 0, 0);
            this.pnl_LogGUI.Controls.Add(this.pnl_Button, 1, 0);
            this.pnl_LogGUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_LogGUI.Location = new System.Drawing.Point(0, 50);
            this.pnl_LogGUI.Name = "pnl_LogGUI";
            this.pnl_LogGUI.RowCount = 1;
            this.pnl_LogGUI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_LogGUI.Size = new System.Drawing.Size(1000, 700);
            this.pnl_LogGUI.TabIndex = 1;
            // 
            // Log
            // 
            this.Log.Controls.Add(this.btn_SelectMode);
            this.Log.Controls.Add(this.LogView);
            this.Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Log.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.Log.Location = new System.Drawing.Point(3, 3);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(744, 694);
            this.Log.TabIndex = 41;
            this.Log.TabStop = false;
            this.Log.Text = "Alarm Log";
            // 
            // LogView
            // 
            this.LogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogView.FullRowSelect = true;
            this.LogView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LogView.HideSelection = false;
            this.LogView.Location = new System.Drawing.Point(3, 30);
            this.LogView.Name = "LogView";
            this.LogView.Size = new System.Drawing.Size(738, 661);
            this.LogView.TabIndex = 0;
            this.LogView.UseCompatibleStateImageBehavior = false;
            this.LogView.View = System.Windows.Forms.View.Details;
            // 
            // pnl_Button
            // 
            this.pnl_Button.ColumnCount = 1;
            this.pnl_Button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_Button.Controls.Add(this.btn_LogDelete, 0, 4);
            this.pnl_Button.Controls.Add(this.btn_Export, 0, 3);
            this.pnl_Button.Controls.Add(this.btn_ClearFilter, 0, 2);
            this.pnl_Button.Controls.Add(this.pnl_Filter, 0, 0);
            this.pnl_Button.Controls.Add(this.btn_Search, 0, 1);
            this.pnl_Button.Location = new System.Drawing.Point(753, 3);
            this.pnl_Button.Name = "pnl_Button";
            this.pnl_Button.RowCount = 5;
            this.pnl_Button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_Button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.75F));
            this.pnl_Button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.75F));
            this.pnl_Button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.75F));
            this.pnl_Button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.75F));
            this.pnl_Button.Size = new System.Drawing.Size(244, 694);
            this.pnl_Button.TabIndex = 42;
            // 
            // btn_LogDelete
            // 
            this.btn_LogDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_LogDelete.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_LogDelete.Location = new System.Drawing.Point(3, 566);
            this.btn_LogDelete.Name = "btn_LogDelete";
            this.btn_LogDelete.Size = new System.Drawing.Size(238, 125);
            this.btn_LogDelete.TabIndex = 47;
            this.btn_LogDelete.Text = "Log Delete";
            this.btn_LogDelete.UseVisualStyleBackColor = true;
            this.btn_LogDelete.Click += new System.EventHandler(this.btn_LogDelete_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Export.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_Export.Location = new System.Drawing.Point(3, 436);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(238, 124);
            this.btn_Export.TabIndex = 46;
            this.btn_Export.Text = "Log Export";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_ClearFilter
            // 
            this.btn_ClearFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ClearFilter.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_ClearFilter.Location = new System.Drawing.Point(3, 306);
            this.btn_ClearFilter.Name = "btn_ClearFilter";
            this.btn_ClearFilter.Size = new System.Drawing.Size(238, 124);
            this.btn_ClearFilter.TabIndex = 45;
            this.btn_ClearFilter.Text = "Clear Filter";
            this.btn_ClearFilter.UseVisualStyleBackColor = true;
            this.btn_ClearFilter.Click += new System.EventHandler(this.btn_ClearFilter_Click);
            // 
            // pnl_Filter
            // 
            this.pnl_Filter.ColumnCount = 1;
            this.pnl_Filter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_Filter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnl_Filter.Controls.Add(this.txtbox_3, 0, 5);
            this.pnl_Filter.Controls.Add(this.cbox_Column3, 0, 4);
            this.pnl_Filter.Controls.Add(this.txtbox_2, 0, 3);
            this.pnl_Filter.Controls.Add(this.cbox_Column2, 0, 2);
            this.pnl_Filter.Controls.Add(this.cbox_Column1, 0, 0);
            this.pnl_Filter.Controls.Add(this.txtbox_1, 0, 1);
            this.pnl_Filter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Filter.Location = new System.Drawing.Point(3, 3);
            this.pnl_Filter.Name = "pnl_Filter";
            this.pnl_Filter.RowCount = 6;
            this.pnl_Filter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_Filter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_Filter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_Filter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_Filter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_Filter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_Filter.Size = new System.Drawing.Size(238, 167);
            this.pnl_Filter.TabIndex = 42;
            // 
            // txtbox_3
            // 
            this.txtbox_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbox_3.Location = new System.Drawing.Point(3, 138);
            this.txtbox_3.Name = "txtbox_3";
            this.txtbox_3.Size = new System.Drawing.Size(232, 21);
            this.txtbox_3.TabIndex = 6;
            // 
            // cbox_Column3
            // 
            this.cbox_Column3.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbox_Column3.FormattingEnabled = true;
            this.cbox_Column3.Location = new System.Drawing.Point(3, 111);
            this.cbox_Column3.Name = "cbox_Column3";
            this.cbox_Column3.Size = new System.Drawing.Size(232, 20);
            this.cbox_Column3.TabIndex = 5;
            // 
            // txtbox_2
            // 
            this.txtbox_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbox_2.Location = new System.Drawing.Point(3, 84);
            this.txtbox_2.Name = "txtbox_2";
            this.txtbox_2.Size = new System.Drawing.Size(232, 21);
            this.txtbox_2.TabIndex = 4;
            // 
            // cbox_Column2
            // 
            this.cbox_Column2.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbox_Column2.FormattingEnabled = true;
            this.cbox_Column2.Location = new System.Drawing.Point(3, 57);
            this.cbox_Column2.Name = "cbox_Column2";
            this.cbox_Column2.Size = new System.Drawing.Size(232, 20);
            this.cbox_Column2.TabIndex = 3;
            // 
            // cbox_Column1
            // 
            this.cbox_Column1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbox_Column1.FormattingEnabled = true;
            this.cbox_Column1.Location = new System.Drawing.Point(3, 3);
            this.cbox_Column1.Name = "cbox_Column1";
            this.cbox_Column1.Size = new System.Drawing.Size(232, 20);
            this.cbox_Column1.TabIndex = 1;
            // 
            // txtbox_1
            // 
            this.txtbox_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbox_1.Location = new System.Drawing.Point(3, 30);
            this.txtbox_1.Name = "txtbox_1";
            this.txtbox_1.Size = new System.Drawing.Size(232, 21);
            this.txtbox_1.TabIndex = 2;
            // 
            // btn_Search
            // 
            this.btn_Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Search.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_Search.Location = new System.Drawing.Point(3, 176);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(238, 124);
            this.btn_Search.TabIndex = 44;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click_1);
            // 
            // btn_SelectMode
            // 
            this.btn_SelectMode.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.btn_SelectMode.Location = new System.Drawing.Point(638, 3);
            this.btn_SelectMode.Name = "btn_SelectMode";
            this.btn_SelectMode.Size = new System.Drawing.Size(106, 23);
            this.btn_SelectMode.TabIndex = 1;
            this.btn_SelectMode.Text = "Select Mode";
            this.btn_SelectMode.UseVisualStyleBackColor = true;
            // 
            // btn_RandomAlarm
            // 
            this.btn_RandomAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_RandomAlarm.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.btn_RandomAlarm.ForeColor = System.Drawing.Color.Silver;
            this.btn_RandomAlarm.Location = new System.Drawing.Point(801, 1);
            this.btn_RandomAlarm.Margin = new System.Windows.Forms.Padding(1);
            this.btn_RandomAlarm.Name = "btn_RandomAlarm";
            this.btn_RandomAlarm.Size = new System.Drawing.Size(198, 48);
            this.btn_RandomAlarm.TabIndex = 39;
            this.btn_RandomAlarm.Text = "Create Random\r\nAlarm";
            this.btn_RandomAlarm.UseVisualStyleBackColor = true;
            // 
            // LogGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_LogGUI);
            this.Controls.Add(this.pnl_LogGUI_Menu);
            this.Name = "LogGUI";
            this.Size = new System.Drawing.Size(1000, 750);
            this.pnl_LogGUI_Menu.ResumeLayout(false);
            this.pnl_LogGUI.ResumeLayout(false);
            this.Log.ResumeLayout(false);
            this.pnl_Button.ResumeLayout(false);
            this.pnl_Filter.ResumeLayout(false);
            this.pnl_Filter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.TableLayoutPanel pnl_LogGUI_Menu;
        internal System.Windows.Forms.Button btn_Alarm;
        internal System.Windows.Forms.Button btn_FullLog;
        private System.Windows.Forms.TableLayoutPanel pnl_LogGUI;
        internal System.Windows.Forms.GroupBox Log;
        internal System.Windows.Forms.ListView LogView;
        private System.Windows.Forms.TableLayoutPanel pnl_Button;
        private System.Windows.Forms.TableLayoutPanel pnl_Filter;
        private System.Windows.Forms.ComboBox cbox_Column1;
        private System.Windows.Forms.TextBox txtbox_1;
        private System.Windows.Forms.TextBox txtbox_3;
        private System.Windows.Forms.ComboBox cbox_Column3;
        private System.Windows.Forms.TextBox txtbox_2;
        private System.Windows.Forms.ComboBox cbox_Column2;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_ClearFilter;
        private System.Windows.Forms.Button btn_LogDelete;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.Button btn_SelectMode;
        internal System.Windows.Forms.Button btn_RandomAlarm;
    }
}
