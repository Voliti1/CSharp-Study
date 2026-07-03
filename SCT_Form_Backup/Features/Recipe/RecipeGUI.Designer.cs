namespace SCT_Form
{
    partial class RecipeGUI
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
            this.pnl_RecipeGUI_Menu = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ProcessRecipe = new System.Windows.Forms.Button();
            this.btn_PMC = new System.Windows.Forms.Button();
            this.btn_PMA = new System.Windows.Forms.Button();
            this.btn_PMB = new System.Windows.Forms.Button();
            this.listView_Recipe = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_OpenFolder = new System.Windows.Forms.Button();
            this.btn_RecipeDelete = new System.Windows.Forms.Button();
            this.btn_RecipeSaveAs = new System.Windows.Forms.Button();
            this.btn_RecipeSave = new System.Windows.Forms.Button();
            this.btn_RecipeNew = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Pressure = new System.Windows.Forms.Label();
            this.lbl_Temperature = new System.Windows.Forms.Label();
            this.lbl_ProcessTime = new System.Windows.Forms.Label();
            this.lbl_Description = new System.Windows.Forms.Label();
            this.lbl_GasFlow = new System.Windows.Forms.Label();
            this.lbl_RFPower = new System.Windows.Forms.Label();
            this.lbl_CreatedBy = new System.Windows.Forms.Label();
            this.lbl_CreatedDate = new System.Windows.Forms.Label();
            this.lbl_ppID = new System.Windows.Forms.Label();
            this.lbl_RecipeName = new System.Windows.Forms.Label();
            this.RecipeGUI_pnl = new System.Windows.Forms.Panel();
            this.pnl_RecipeGUI_Menu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.RecipeGUI_pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_RecipeGUI_Menu
            // 
            this.pnl_RecipeGUI_Menu.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnl_RecipeGUI_Menu.ColumnCount = 5;
            this.pnl_RecipeGUI_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnl_RecipeGUI_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnl_RecipeGUI_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnl_RecipeGUI_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnl_RecipeGUI_Menu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnl_RecipeGUI_Menu.Controls.Add(this.btn_ProcessRecipe, 3, 0);
            this.pnl_RecipeGUI_Menu.Controls.Add(this.btn_PMC, 2, 0);
            this.pnl_RecipeGUI_Menu.Controls.Add(this.btn_PMA, 0, 0);
            this.pnl_RecipeGUI_Menu.Controls.Add(this.btn_PMB, 1, 0);
            this.pnl_RecipeGUI_Menu.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_RecipeGUI_Menu.Location = new System.Drawing.Point(0, 0);
            this.pnl_RecipeGUI_Menu.Name = "pnl_RecipeGUI_Menu";
            this.pnl_RecipeGUI_Menu.RowCount = 1;
            this.pnl_RecipeGUI_Menu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_RecipeGUI_Menu.Size = new System.Drawing.Size(1000, 50);
            this.pnl_RecipeGUI_Menu.TabIndex = 39;
            // 
            // btn_ProcessRecipe
            // 
            this.btn_ProcessRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProcessRecipe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_ProcessRecipe.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_ProcessRecipe.ForeColor = System.Drawing.Color.Silver;
            this.btn_ProcessRecipe.Location = new System.Drawing.Point(301, 1);
            this.btn_ProcessRecipe.Margin = new System.Windows.Forms.Padding(1);
            this.btn_ProcessRecipe.Name = "btn_ProcessRecipe";
            this.btn_ProcessRecipe.Size = new System.Drawing.Size(98, 48);
            this.btn_ProcessRecipe.TabIndex = 46;
            this.btn_ProcessRecipe.Text = "Process";
            this.btn_ProcessRecipe.UseVisualStyleBackColor = true;
            this.btn_ProcessRecipe.Click += new System.EventHandler(this.btn_ProcessRecipe_Click);
            // 
            // btn_PMC
            // 
            this.btn_PMC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PMC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_PMC.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_PMC.ForeColor = System.Drawing.Color.Silver;
            this.btn_PMC.Location = new System.Drawing.Point(201, 1);
            this.btn_PMC.Margin = new System.Windows.Forms.Padding(1);
            this.btn_PMC.Name = "btn_PMC";
            this.btn_PMC.Size = new System.Drawing.Size(98, 48);
            this.btn_PMC.TabIndex = 45;
            this.btn_PMC.Text = "PM C";
            this.btn_PMC.UseVisualStyleBackColor = true;
            this.btn_PMC.Click += new System.EventHandler(this.btn_PMC_Click);
            // 
            // btn_PMA
            // 
            this.btn_PMA.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_PMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PMA.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PMA.FlatAppearance.BorderSize = 2;
            this.btn_PMA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PMA.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_PMA.ForeColor = System.Drawing.Color.White;
            this.btn_PMA.Location = new System.Drawing.Point(1, 1);
            this.btn_PMA.Margin = new System.Windows.Forms.Padding(1);
            this.btn_PMA.Name = "btn_PMA";
            this.btn_PMA.Size = new System.Drawing.Size(98, 48);
            this.btn_PMA.TabIndex = 37;
            this.btn_PMA.Text = "PM A";
            this.btn_PMA.UseVisualStyleBackColor = false;
            this.btn_PMA.Click += new System.EventHandler(this.btn_PMA_Click);
            // 
            // btn_PMB
            // 
            this.btn_PMB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PMB.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_PMB.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_PMB.ForeColor = System.Drawing.Color.Silver;
            this.btn_PMB.Location = new System.Drawing.Point(101, 1);
            this.btn_PMB.Margin = new System.Windows.Forms.Padding(1);
            this.btn_PMB.Name = "btn_PMB";
            this.btn_PMB.Size = new System.Drawing.Size(98, 48);
            this.btn_PMB.TabIndex = 38;
            this.btn_PMB.Text = "PM B";
            this.btn_PMB.UseVisualStyleBackColor = true;
            this.btn_PMB.Click += new System.EventHandler(this.btn_PMB_Click);
            // 
            // listView_Recipe
            // 
            this.listView_Recipe.HideSelection = false;
            this.listView_Recipe.Location = new System.Drawing.Point(33, 22);
            this.listView_Recipe.Name = "listView_Recipe";
            this.listView_Recipe.Size = new System.Drawing.Size(290, 640);
            this.listView_Recipe.TabIndex = 40;
            this.listView_Recipe.UseCompatibleStateImageBehavior = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btn_OpenFolder, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btn_RecipeDelete, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.btn_RecipeSaveAs, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_RecipeSave, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_RecipeNew, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(363, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 338);
            this.tableLayoutPanel1.TabIndex = 41;
            // 
            // btn_OpenFolder
            // 
            this.btn_OpenFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_OpenFolder.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_OpenFolder.Location = new System.Drawing.Point(3, 271);
            this.btn_OpenFolder.Name = "btn_OpenFolder";
            this.btn_OpenFolder.Size = new System.Drawing.Size(194, 64);
            this.btn_OpenFolder.TabIndex = 4;
            this.btn_OpenFolder.Text = "Open Folder";
            this.btn_OpenFolder.UseVisualStyleBackColor = true;
            // 
            // btn_RecipeDelete
            // 
            this.btn_RecipeDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_RecipeDelete.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_RecipeDelete.Location = new System.Drawing.Point(3, 204);
            this.btn_RecipeDelete.Name = "btn_RecipeDelete";
            this.btn_RecipeDelete.Size = new System.Drawing.Size(194, 61);
            this.btn_RecipeDelete.TabIndex = 3;
            this.btn_RecipeDelete.Text = "Delete";
            this.btn_RecipeDelete.UseVisualStyleBackColor = true;
            // 
            // btn_RecipeSaveAs
            // 
            this.btn_RecipeSaveAs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_RecipeSaveAs.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_RecipeSaveAs.Location = new System.Drawing.Point(3, 137);
            this.btn_RecipeSaveAs.Name = "btn_RecipeSaveAs";
            this.btn_RecipeSaveAs.Size = new System.Drawing.Size(194, 61);
            this.btn_RecipeSaveAs.TabIndex = 2;
            this.btn_RecipeSaveAs.Text = "Save as";
            this.btn_RecipeSaveAs.UseVisualStyleBackColor = true;
            // 
            // btn_RecipeSave
            // 
            this.btn_RecipeSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_RecipeSave.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_RecipeSave.Location = new System.Drawing.Point(3, 70);
            this.btn_RecipeSave.Name = "btn_RecipeSave";
            this.btn_RecipeSave.Size = new System.Drawing.Size(194, 61);
            this.btn_RecipeSave.TabIndex = 1;
            this.btn_RecipeSave.Text = "Save";
            this.btn_RecipeSave.UseVisualStyleBackColor = true;
            // 
            // btn_RecipeNew
            // 
            this.btn_RecipeNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_RecipeNew.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_RecipeNew.Location = new System.Drawing.Point(3, 3);
            this.btn_RecipeNew.Name = "btn_RecipeNew";
            this.btn_RecipeNew.Size = new System.Drawing.Size(194, 61);
            this.btn_RecipeNew.TabIndex = 0;
            this.btn_RecipeNew.Text = "New";
            this.btn_RecipeNew.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lbl_Pressure, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.lbl_Temperature, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lbl_ProcessTime, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lbl_Description, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbl_GasFlow, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lbl_RFPower, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.lbl_CreatedBy, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.lbl_CreatedDate, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.lbl_ppID, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.lbl_RecipeName, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(592, 22);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(372, 640);
            this.tableLayoutPanel2.TabIndex = 42;
            // 
            // lbl_Pressure
            // 
            this.lbl_Pressure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Pressure.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lbl_Pressure.Location = new System.Drawing.Point(3, 256);
            this.lbl_Pressure.Name = "lbl_Pressure";
            this.lbl_Pressure.Size = new System.Drawing.Size(180, 64);
            this.lbl_Pressure.TabIndex = 8;
            this.lbl_Pressure.Text = "Pressure";
            this.lbl_Pressure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Temperature
            // 
            this.lbl_Temperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Temperature.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lbl_Temperature.Location = new System.Drawing.Point(3, 192);
            this.lbl_Temperature.Name = "lbl_Temperature";
            this.lbl_Temperature.Size = new System.Drawing.Size(180, 64);
            this.lbl_Temperature.TabIndex = 6;
            this.lbl_Temperature.Text = "Temperature";
            this.lbl_Temperature.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ProcessTime
            // 
            this.lbl_ProcessTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProcessTime.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lbl_ProcessTime.Location = new System.Drawing.Point(3, 128);
            this.lbl_ProcessTime.Name = "lbl_ProcessTime";
            this.lbl_ProcessTime.Size = new System.Drawing.Size(180, 64);
            this.lbl_ProcessTime.TabIndex = 4;
            this.lbl_ProcessTime.Text = "Process Time";
            this.lbl_ProcessTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Description
            // 
            this.lbl_Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Description.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lbl_Description.Location = new System.Drawing.Point(3, 64);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(180, 64);
            this.lbl_Description.TabIndex = 2;
            this.lbl_Description.Text = "Description";
            this.lbl_Description.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_GasFlow
            // 
            this.lbl_GasFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_GasFlow.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lbl_GasFlow.Location = new System.Drawing.Point(3, 320);
            this.lbl_GasFlow.Name = "lbl_GasFlow";
            this.lbl_GasFlow.Size = new System.Drawing.Size(180, 64);
            this.lbl_GasFlow.TabIndex = 1;
            this.lbl_GasFlow.Text = "Gas Flow";
            this.lbl_GasFlow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RFPower
            // 
            this.lbl_RFPower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RFPower.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lbl_RFPower.Location = new System.Drawing.Point(3, 384);
            this.lbl_RFPower.Name = "lbl_RFPower";
            this.lbl_RFPower.Size = new System.Drawing.Size(180, 64);
            this.lbl_RFPower.TabIndex = 3;
            this.lbl_RFPower.Text = "RF Power";
            this.lbl_RFPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CreatedBy
            // 
            this.lbl_CreatedBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CreatedBy.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lbl_CreatedBy.Location = new System.Drawing.Point(3, 448);
            this.lbl_CreatedBy.Name = "lbl_CreatedBy";
            this.lbl_CreatedBy.Size = new System.Drawing.Size(180, 64);
            this.lbl_CreatedBy.TabIndex = 5;
            this.lbl_CreatedBy.Text = "Created By";
            this.lbl_CreatedBy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CreatedDate
            // 
            this.lbl_CreatedDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CreatedDate.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lbl_CreatedDate.Location = new System.Drawing.Point(3, 512);
            this.lbl_CreatedDate.Name = "lbl_CreatedDate";
            this.lbl_CreatedDate.Size = new System.Drawing.Size(180, 64);
            this.lbl_CreatedDate.TabIndex = 7;
            this.lbl_CreatedDate.Text = "Modified Date";
            this.lbl_CreatedDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ppID
            // 
            this.lbl_ppID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ppID.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lbl_ppID.Location = new System.Drawing.Point(3, 576);
            this.lbl_ppID.Name = "lbl_ppID";
            this.lbl_ppID.Size = new System.Drawing.Size(180, 64);
            this.lbl_ppID.TabIndex = 9;
            this.lbl_ppID.Text = "PP ID";
            this.lbl_ppID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RecipeName
            // 
            this.lbl_RecipeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RecipeName.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lbl_RecipeName.Location = new System.Drawing.Point(3, 0);
            this.lbl_RecipeName.Name = "lbl_RecipeName";
            this.lbl_RecipeName.Size = new System.Drawing.Size(180, 64);
            this.lbl_RecipeName.TabIndex = 0;
            this.lbl_RecipeName.Text = "Recipe Name";
            this.lbl_RecipeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RecipeGUI_pnl
            // 
            this.RecipeGUI_pnl.Controls.Add(this.tableLayoutPanel2);
            this.RecipeGUI_pnl.Controls.Add(this.listView_Recipe);
            this.RecipeGUI_pnl.Controls.Add(this.tableLayoutPanel1);
            this.RecipeGUI_pnl.Location = new System.Drawing.Point(0, 50);
            this.RecipeGUI_pnl.Margin = new System.Windows.Forms.Padding(0);
            this.RecipeGUI_pnl.Name = "RecipeGUI_pnl";
            this.RecipeGUI_pnl.Size = new System.Drawing.Size(1000, 700);
            this.RecipeGUI_pnl.TabIndex = 43;
            // 
            // RecipeGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RecipeGUI_pnl);
            this.Controls.Add(this.pnl_RecipeGUI_Menu);
            this.Name = "RecipeGUI";
            this.Size = new System.Drawing.Size(1000, 750);
            this.pnl_RecipeGUI_Menu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.RecipeGUI_pnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TableLayoutPanel pnl_RecipeGUI_Menu;
        internal System.Windows.Forms.Button btn_PMC;
        internal System.Windows.Forms.Button btn_PMA;
        internal System.Windows.Forms.Button btn_PMB;
        private System.Windows.Forms.ListView listView_Recipe;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_OpenFolder;
        private System.Windows.Forms.Button btn_RecipeDelete;
        private System.Windows.Forms.Button btn_RecipeSaveAs;
        private System.Windows.Forms.Button btn_RecipeSave;
        private System.Windows.Forms.Button btn_RecipeNew;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbl_RecipeName;
        private System.Windows.Forms.Label lbl_ppID;
        private System.Windows.Forms.Label lbl_Pressure;
        private System.Windows.Forms.Label lbl_CreatedDate;
        private System.Windows.Forms.Label lbl_Temperature;
        private System.Windows.Forms.Label lbl_CreatedBy;
        private System.Windows.Forms.Label lbl_ProcessTime;
        private System.Windows.Forms.Label lbl_RFPower;
        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.Label lbl_GasFlow;
        internal System.Windows.Forms.Button btn_ProcessRecipe;
        private System.Windows.Forms.Panel RecipeGUI_pnl;
    }
}
