namespace SCT_Form
{
    partial class ChamberRecipeSetting
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
            this.listView_ChamberRecipe = new System.Windows.Forms.ListView();
            this.pnl_ChamberRecipeSetting_Menu = new System.Windows.Forms.TableLayoutPanel();
            this.btn_PMC = new System.Windows.Forms.Button();
            this.btn_PMA = new System.Windows.Forms.Button();
            this.btn_PMB = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_RecipeName = new System.Windows.Forms.Label();
            this.lbl_ProcessTime = new System.Windows.Forms.Label();
            this.lbl_Temperature = new System.Windows.Forms.Label();
            this.lbl_Pressure = new System.Windows.Forms.Label();
            this.lbl_GasFlow = new System.Windows.Forms.Label();
            this.lbl_RFPower = new System.Windows.Forms.Label();
            this.btn_Select = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pnl_ChamberRecipeSetting_Menu.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_ChamberRecipe
            // 
            this.listView_ChamberRecipe.HideSelection = false;
            this.listView_ChamberRecipe.Location = new System.Drawing.Point(12, 72);
            this.listView_ChamberRecipe.Name = "listView_ChamberRecipe";
            this.listView_ChamberRecipe.Size = new System.Drawing.Size(171, 326);
            this.listView_ChamberRecipe.TabIndex = 0;
            this.listView_ChamberRecipe.UseCompatibleStateImageBehavior = false;
            // 
            // pnl_ChamberRecipeSetting_Menu
            // 
            this.pnl_ChamberRecipeSetting_Menu.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnl_ChamberRecipeSetting_Menu.ColumnCount = 4;
            this.pnl_ChamberRecipeSetting_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.pnl_ChamberRecipeSetting_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.pnl_ChamberRecipeSetting_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.pnl_ChamberRecipeSetting_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.pnl_ChamberRecipeSetting_Menu.Controls.Add(this.btn_PMC, 2, 0);
            this.pnl_ChamberRecipeSetting_Menu.Controls.Add(this.btn_PMA, 0, 0);
            this.pnl_ChamberRecipeSetting_Menu.Controls.Add(this.btn_PMB, 1, 0);
            this.pnl_ChamberRecipeSetting_Menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ChamberRecipeSetting_Menu.Location = new System.Drawing.Point(0, 0);
            this.pnl_ChamberRecipeSetting_Menu.Name = "pnl_ChamberRecipeSetting_Menu";
            this.pnl_ChamberRecipeSetting_Menu.RowCount = 1;
            this.pnl_ChamberRecipeSetting_Menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_ChamberRecipeSetting_Menu.Size = new System.Drawing.Size(595, 50);
            this.pnl_ChamberRecipeSetting_Menu.TabIndex = 40;
            // 
            // btn_PMC
            // 
            this.btn_PMC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PMC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_PMC.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btn_PMC.ForeColor = System.Drawing.Color.Silver;
            this.btn_PMC.Location = new System.Drawing.Point(179, 1);
            this.btn_PMC.Margin = new System.Windows.Forms.Padding(1);
            this.btn_PMC.Name = "btn_PMC";
            this.btn_PMC.Size = new System.Drawing.Size(87, 48);
            this.btn_PMC.TabIndex = 45;
            this.btn_PMC.Text = "PM C";
            this.btn_PMC.UseVisualStyleBackColor = true;
            // 
            // btn_PMA
            // 
            this.btn_PMA.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_PMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PMA.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PMA.FlatAppearance.BorderSize = 2;
            this.btn_PMA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PMA.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btn_PMA.ForeColor = System.Drawing.Color.White;
            this.btn_PMA.Location = new System.Drawing.Point(1, 1);
            this.btn_PMA.Margin = new System.Windows.Forms.Padding(1);
            this.btn_PMA.Name = "btn_PMA";
            this.btn_PMA.Size = new System.Drawing.Size(87, 48);
            this.btn_PMA.TabIndex = 37;
            this.btn_PMA.Text = "PM A";
            this.btn_PMA.UseVisualStyleBackColor = false;
            // 
            // btn_PMB
            // 
            this.btn_PMB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PMB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_PMB.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btn_PMB.ForeColor = System.Drawing.Color.Silver;
            this.btn_PMB.Location = new System.Drawing.Point(90, 1);
            this.btn_PMB.Margin = new System.Windows.Forms.Padding(1);
            this.btn_PMB.Name = "btn_PMB";
            this.btn_PMB.Size = new System.Drawing.Size(87, 48);
            this.btn_PMB.TabIndex = 38;
            this.btn_PMB.Text = "PM B";
            this.btn_PMB.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lbl_RecipeName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_ProcessTime, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbl_RFPower, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lbl_GasFlow, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.lbl_Pressure, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lbl_Temperature, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(198, 72);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(264, 326);
            this.tableLayoutPanel2.TabIndex = 43;
            // 
            // lbl_RecipeName
            // 
            this.lbl_RecipeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RecipeName.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_RecipeName.Location = new System.Drawing.Point(3, 0);
            this.lbl_RecipeName.Name = "lbl_RecipeName";
            this.lbl_RecipeName.Size = new System.Drawing.Size(126, 46);
            this.lbl_RecipeName.TabIndex = 0;
            this.lbl_RecipeName.Text = "Recipe Name";
            this.lbl_RecipeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ProcessTime
            // 
            this.lbl_ProcessTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProcessTime.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_ProcessTime.Location = new System.Drawing.Point(3, 46);
            this.lbl_ProcessTime.Name = "lbl_ProcessTime";
            this.lbl_ProcessTime.Size = new System.Drawing.Size(126, 46);
            this.lbl_ProcessTime.TabIndex = 10;
            this.lbl_ProcessTime.Text = "Process Time";
            this.lbl_ProcessTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Temperature
            // 
            this.lbl_Temperature.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_Temperature.Location = new System.Drawing.Point(3, 92);
            this.lbl_Temperature.Name = "lbl_Temperature";
            this.lbl_Temperature.Size = new System.Drawing.Size(126, 32);
            this.lbl_Temperature.TabIndex = 11;
            this.lbl_Temperature.Text = "Temperature";
            this.lbl_Temperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Pressure
            // 
            this.lbl_Pressure.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_Pressure.Location = new System.Drawing.Point(3, 138);
            this.lbl_Pressure.Name = "lbl_Pressure";
            this.lbl_Pressure.Size = new System.Drawing.Size(126, 32);
            this.lbl_Pressure.TabIndex = 12;
            this.lbl_Pressure.Text = "Pressure";
            this.lbl_Pressure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_GasFlow
            // 
            this.lbl_GasFlow.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_GasFlow.Location = new System.Drawing.Point(3, 184);
            this.lbl_GasFlow.Name = "lbl_GasFlow";
            this.lbl_GasFlow.Size = new System.Drawing.Size(126, 32);
            this.lbl_GasFlow.TabIndex = 13;
            this.lbl_GasFlow.Text = "Gas Flow";
            this.lbl_GasFlow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RFPower
            // 
            this.lbl_RFPower.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_RFPower.Location = new System.Drawing.Point(3, 230);
            this.lbl_RFPower.Name = "lbl_RFPower";
            this.lbl_RFPower.Size = new System.Drawing.Size(126, 32);
            this.lbl_RFPower.TabIndex = 14;
            this.lbl_RFPower.Text = "RF Power";
            this.lbl_RFPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Select
            // 
            this.btn_Select.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.btn_Select.Location = new System.Drawing.Point(488, 72);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(83, 46);
            this.btn_Select.TabIndex = 44;
            this.btn_Select.Text = "Select";
            this.btn_Select.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(488, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 46);
            this.button1.TabIndex = 45;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ChamberRecipeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 412);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Select);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.pnl_ChamberRecipeSetting_Menu);
            this.Controls.Add(this.listView_ChamberRecipe);
            this.Name = "ChamberRecipeSetting";
            this.Text = "Chamber Recipe Setting";
            this.pnl_ChamberRecipeSetting_Menu.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_ChamberRecipe;
        internal System.Windows.Forms.TableLayoutPanel pnl_ChamberRecipeSetting_Menu;
        internal System.Windows.Forms.Button btn_PMC;
        internal System.Windows.Forms.Button btn_PMA;
        internal System.Windows.Forms.Button btn_PMB;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbl_RecipeName;
        private System.Windows.Forms.Label lbl_ProcessTime;
        private System.Windows.Forms.Label lbl_Temperature;
        private System.Windows.Forms.Label lbl_RFPower;
        private System.Windows.Forms.Label lbl_Pressure;
        private System.Windows.Forms.Label lbl_GasFlow;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.Button button1;
    }
}