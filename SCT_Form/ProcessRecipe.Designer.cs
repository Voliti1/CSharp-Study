namespace SCT_Form
{
    partial class ProcessRecipe
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
            this.listView_Recipe = new System.Windows.Forms.ListView();
            this.pnl_ProcessFlow = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_ProcessRecipeList = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ProcessRecipeList = new System.Windows.Forms.Label();
            this.pnl_ProcessRecipeBtn = new System.Windows.Forms.TableLayoutPanel();
            this.btn_ProcessRecipeOpenFolder = new System.Windows.Forms.Button();
            this.btn_ProcessRecipeDelete = new System.Windows.Forms.Button();
            this.btn_ProcessRecipeSaveAs = new System.Windows.Forms.Button();
            this.btn_ProcessRecipeSave = new System.Windows.Forms.Button();
            this.btn_ProcessRecipeNew = new System.Windows.Forms.Button();
            this.bigpnl_ProcessFlow = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ProcessFlow = new System.Windows.Forms.Label();
            this.lbl_Step = new System.Windows.Forms.Label();
            this.lbl_Module = new System.Windows.Forms.Label();
            this.lbl_RecipePPID = new System.Windows.Forms.Label();
            this.lbl_ProcessTime = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_RecipeDetail = new System.Windows.Forms.Label();
            this.pnl_RecipeDetail = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_RecipeName = new System.Windows.Forms.Label();
            this.lbl_PPID = new System.Windows.Forms.Label();
            this.lbl_TotalProcessTime = new System.Windows.Forms.Label();
            this.lbl_CreatedBy = new System.Windows.Forms.Label();
            this.lbl_ModifiedDate = new System.Windows.Forms.Label();
            this.lbl_Description = new System.Windows.Forms.Label();
            this.pnl_ProcessRecipeBtn2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_MoveUp = new System.Windows.Forms.Button();
            this.btn_MoveDown = new System.Windows.Forms.Button();
            this.btn_AddStep = new System.Windows.Forms.Button();
            this.btn_DelStep = new System.Windows.Forms.Button();
            this.pnl_ProcessFlow.SuspendLayout();
            this.pnl_ProcessRecipeList.SuspendLayout();
            this.pnl_ProcessRecipeBtn.SuspendLayout();
            this.bigpnl_ProcessFlow.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnl_RecipeDetail.SuspendLayout();
            this.pnl_ProcessRecipeBtn2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_Recipe
            // 
            this.listView_Recipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Recipe.HideSelection = false;
            this.listView_Recipe.Location = new System.Drawing.Point(3, 33);
            this.listView_Recipe.Name = "listView_Recipe";
            this.listView_Recipe.Size = new System.Drawing.Size(259, 267);
            this.listView_Recipe.TabIndex = 41;
            this.listView_Recipe.UseCompatibleStateImageBehavior = false;
            // 
            // pnl_ProcessFlow
            // 
            this.pnl_ProcessFlow.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.pnl_ProcessFlow.ColumnCount = 4;
            this.pnl_ProcessFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.08911F));
            this.pnl_ProcessFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.71287F));
            this.pnl_ProcessFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.pnl_ProcessFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.pnl_ProcessFlow.Controls.Add(this.lbl_ProcessTime, 3, 0);
            this.pnl_ProcessFlow.Controls.Add(this.lbl_RecipePPID, 2, 0);
            this.pnl_ProcessFlow.Controls.Add(this.lbl_Module, 1, 0);
            this.pnl_ProcessFlow.Controls.Add(this.lbl_Step, 0, 0);
            this.pnl_ProcessFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_ProcessFlow.Location = new System.Drawing.Point(3, 33);
            this.pnl_ProcessFlow.Name = "pnl_ProcessFlow";
            this.pnl_ProcessFlow.RowCount = 6;
            this.pnl_ProcessFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.pnl_ProcessFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_ProcessFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_ProcessFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_ProcessFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_ProcessFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_ProcessFlow.Size = new System.Drawing.Size(455, 504);
            this.pnl_ProcessFlow.TabIndex = 44;
            // 
            // pnl_ProcessRecipeList
            // 
            this.pnl_ProcessRecipeList.ColumnCount = 1;
            this.pnl_ProcessRecipeList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_ProcessRecipeList.Controls.Add(this.listView_Recipe, 0, 1);
            this.pnl_ProcessRecipeList.Controls.Add(this.lbl_ProcessRecipeList, 0, 0);
            this.pnl_ProcessRecipeList.Location = new System.Drawing.Point(13, 16);
            this.pnl_ProcessRecipeList.Name = "pnl_ProcessRecipeList";
            this.pnl_ProcessRecipeList.RowCount = 2;
            this.pnl_ProcessRecipeList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnl_ProcessRecipeList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.pnl_ProcessRecipeList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnl_ProcessRecipeList.Size = new System.Drawing.Size(265, 303);
            this.pnl_ProcessRecipeList.TabIndex = 45;
            // 
            // lbl_ProcessRecipeList
            // 
            this.lbl_ProcessRecipeList.BackColor = System.Drawing.Color.DarkBlue;
            this.lbl_ProcessRecipeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProcessRecipeList.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_ProcessRecipeList.ForeColor = System.Drawing.Color.White;
            this.lbl_ProcessRecipeList.Location = new System.Drawing.Point(3, 0);
            this.lbl_ProcessRecipeList.Name = "lbl_ProcessRecipeList";
            this.lbl_ProcessRecipeList.Size = new System.Drawing.Size(259, 30);
            this.lbl_ProcessRecipeList.TabIndex = 42;
            this.lbl_ProcessRecipeList.Text = "Process Recipe List";
            this.lbl_ProcessRecipeList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_ProcessRecipeBtn
            // 
            this.pnl_ProcessRecipeBtn.ColumnCount = 1;
            this.pnl_ProcessRecipeBtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_ProcessRecipeBtn.Controls.Add(this.btn_ProcessRecipeOpenFolder, 0, 4);
            this.pnl_ProcessRecipeBtn.Controls.Add(this.btn_ProcessRecipeDelete, 0, 3);
            this.pnl_ProcessRecipeBtn.Controls.Add(this.btn_ProcessRecipeSaveAs, 0, 2);
            this.pnl_ProcessRecipeBtn.Controls.Add(this.btn_ProcessRecipeSave, 0, 1);
            this.pnl_ProcessRecipeBtn.Controls.Add(this.btn_ProcessRecipeNew, 0, 0);
            this.pnl_ProcessRecipeBtn.Location = new System.Drawing.Point(13, 323);
            this.pnl_ProcessRecipeBtn.Name = "pnl_ProcessRecipeBtn";
            this.pnl_ProcessRecipeBtn.RowCount = 5;
            this.pnl_ProcessRecipeBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_ProcessRecipeBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_ProcessRecipeBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_ProcessRecipeBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_ProcessRecipeBtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_ProcessRecipeBtn.Size = new System.Drawing.Size(265, 360);
            this.pnl_ProcessRecipeBtn.TabIndex = 46;
            // 
            // btn_ProcessRecipeOpenFolder
            // 
            this.btn_ProcessRecipeOpenFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProcessRecipeOpenFolder.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_ProcessRecipeOpenFolder.Location = new System.Drawing.Point(1, 289);
            this.btn_ProcessRecipeOpenFolder.Margin = new System.Windows.Forms.Padding(1);
            this.btn_ProcessRecipeOpenFolder.Name = "btn_ProcessRecipeOpenFolder";
            this.btn_ProcessRecipeOpenFolder.Size = new System.Drawing.Size(263, 70);
            this.btn_ProcessRecipeOpenFolder.TabIndex = 4;
            this.btn_ProcessRecipeOpenFolder.Text = "Open Folder";
            this.btn_ProcessRecipeOpenFolder.UseVisualStyleBackColor = true;
            // 
            // btn_ProcessRecipeDelete
            // 
            this.btn_ProcessRecipeDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProcessRecipeDelete.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_ProcessRecipeDelete.Location = new System.Drawing.Point(1, 217);
            this.btn_ProcessRecipeDelete.Margin = new System.Windows.Forms.Padding(1);
            this.btn_ProcessRecipeDelete.Name = "btn_ProcessRecipeDelete";
            this.btn_ProcessRecipeDelete.Size = new System.Drawing.Size(263, 70);
            this.btn_ProcessRecipeDelete.TabIndex = 3;
            this.btn_ProcessRecipeDelete.Text = "Delete";
            this.btn_ProcessRecipeDelete.UseVisualStyleBackColor = true;
            // 
            // btn_ProcessRecipeSaveAs
            // 
            this.btn_ProcessRecipeSaveAs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProcessRecipeSaveAs.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_ProcessRecipeSaveAs.Location = new System.Drawing.Point(1, 145);
            this.btn_ProcessRecipeSaveAs.Margin = new System.Windows.Forms.Padding(1);
            this.btn_ProcessRecipeSaveAs.Name = "btn_ProcessRecipeSaveAs";
            this.btn_ProcessRecipeSaveAs.Size = new System.Drawing.Size(263, 70);
            this.btn_ProcessRecipeSaveAs.TabIndex = 2;
            this.btn_ProcessRecipeSaveAs.Text = "Save as";
            this.btn_ProcessRecipeSaveAs.UseVisualStyleBackColor = true;
            // 
            // btn_ProcessRecipeSave
            // 
            this.btn_ProcessRecipeSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProcessRecipeSave.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_ProcessRecipeSave.Location = new System.Drawing.Point(1, 73);
            this.btn_ProcessRecipeSave.Margin = new System.Windows.Forms.Padding(1);
            this.btn_ProcessRecipeSave.Name = "btn_ProcessRecipeSave";
            this.btn_ProcessRecipeSave.Size = new System.Drawing.Size(263, 70);
            this.btn_ProcessRecipeSave.TabIndex = 1;
            this.btn_ProcessRecipeSave.Text = "Save";
            this.btn_ProcessRecipeSave.UseVisualStyleBackColor = true;
            // 
            // btn_ProcessRecipeNew
            // 
            this.btn_ProcessRecipeNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProcessRecipeNew.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.btn_ProcessRecipeNew.Location = new System.Drawing.Point(1, 1);
            this.btn_ProcessRecipeNew.Margin = new System.Windows.Forms.Padding(1);
            this.btn_ProcessRecipeNew.Name = "btn_ProcessRecipeNew";
            this.btn_ProcessRecipeNew.Size = new System.Drawing.Size(263, 70);
            this.btn_ProcessRecipeNew.TabIndex = 0;
            this.btn_ProcessRecipeNew.Text = "New";
            this.btn_ProcessRecipeNew.UseVisualStyleBackColor = true;
            // 
            // bigpnl_ProcessFlow
            // 
            this.bigpnl_ProcessFlow.ColumnCount = 1;
            this.bigpnl_ProcessFlow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bigpnl_ProcessFlow.Controls.Add(this.lbl_ProcessFlow, 0, 0);
            this.bigpnl_ProcessFlow.Controls.Add(this.pnl_ProcessFlow, 0, 1);
            this.bigpnl_ProcessFlow.Location = new System.Drawing.Point(293, 16);
            this.bigpnl_ProcessFlow.Name = "bigpnl_ProcessFlow";
            this.bigpnl_ProcessFlow.RowCount = 2;
            this.bigpnl_ProcessFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.bigpnl_ProcessFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bigpnl_ProcessFlow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.bigpnl_ProcessFlow.Size = new System.Drawing.Size(461, 540);
            this.bigpnl_ProcessFlow.TabIndex = 46;
            // 
            // lbl_ProcessFlow
            // 
            this.lbl_ProcessFlow.BackColor = System.Drawing.Color.DarkBlue;
            this.lbl_ProcessFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProcessFlow.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_ProcessFlow.ForeColor = System.Drawing.Color.White;
            this.lbl_ProcessFlow.Location = new System.Drawing.Point(3, 0);
            this.lbl_ProcessFlow.Name = "lbl_ProcessFlow";
            this.lbl_ProcessFlow.Size = new System.Drawing.Size(455, 30);
            this.lbl_ProcessFlow.TabIndex = 42;
            this.lbl_ProcessFlow.Text = "Process Flow";
            this.lbl_ProcessFlow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Step
            // 
            this.lbl_Step.AutoSize = true;
            this.lbl_Step.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Step.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.lbl_Step.Location = new System.Drawing.Point(1, 1);
            this.lbl_Step.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Step.Name = "lbl_Step";
            this.lbl_Step.Size = new System.Drawing.Size(49, 29);
            this.lbl_Step.TabIndex = 0;
            this.lbl_Step.Text = "Step";
            this.lbl_Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Module
            // 
            this.lbl_Module.AutoSize = true;
            this.lbl_Module.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Module.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_Module.Location = new System.Drawing.Point(51, 1);
            this.lbl_Module.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Module.Name = "lbl_Module";
            this.lbl_Module.Size = new System.Drawing.Size(129, 29);
            this.lbl_Module.TabIndex = 1;
            this.lbl_Module.Text = "Module";
            this.lbl_Module.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_RecipePPID
            // 
            this.lbl_RecipePPID.AutoSize = true;
            this.lbl_RecipePPID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RecipePPID.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_RecipePPID.Location = new System.Drawing.Point(181, 1);
            this.lbl_RecipePPID.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_RecipePPID.Name = "lbl_RecipePPID";
            this.lbl_RecipePPID.Size = new System.Drawing.Size(135, 29);
            this.lbl_RecipePPID.TabIndex = 2;
            this.lbl_RecipePPID.Text = "Recipe PPID";
            this.lbl_RecipePPID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ProcessTime
            // 
            this.lbl_ProcessTime.AutoSize = true;
            this.lbl_ProcessTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProcessTime.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_ProcessTime.Location = new System.Drawing.Point(317, 1);
            this.lbl_ProcessTime.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_ProcessTime.Name = "lbl_ProcessTime";
            this.lbl_ProcessTime.Size = new System.Drawing.Size(137, 29);
            this.lbl_ProcessTime.TabIndex = 3;
            this.lbl_ProcessTime.Text = "Process Time";
            this.lbl_ProcessTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_RecipeDetail, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnl_RecipeDetail, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(760, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(237, 537);
            this.tableLayoutPanel1.TabIndex = 47;
            // 
            // lbl_RecipeDetail
            // 
            this.lbl_RecipeDetail.BackColor = System.Drawing.Color.DarkBlue;
            this.lbl_RecipeDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RecipeDetail.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_RecipeDetail.ForeColor = System.Drawing.Color.White;
            this.lbl_RecipeDetail.Location = new System.Drawing.Point(3, 0);
            this.lbl_RecipeDetail.Name = "lbl_RecipeDetail";
            this.lbl_RecipeDetail.Size = new System.Drawing.Size(231, 30);
            this.lbl_RecipeDetail.TabIndex = 42;
            this.lbl_RecipeDetail.Text = "Recipe Detail";
            this.lbl_RecipeDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_RecipeDetail
            // 
            this.pnl_RecipeDetail.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.pnl_RecipeDetail.ColumnCount = 2;
            this.pnl_RecipeDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.pnl_RecipeDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnl_RecipeDetail.Controls.Add(this.lbl_RecipeName, 0, 0);
            this.pnl_RecipeDetail.Controls.Add(this.lbl_PPID, 0, 1);
            this.pnl_RecipeDetail.Controls.Add(this.lbl_TotalProcessTime, 0, 2);
            this.pnl_RecipeDetail.Controls.Add(this.lbl_CreatedBy, 0, 3);
            this.pnl_RecipeDetail.Controls.Add(this.lbl_ModifiedDate, 0, 4);
            this.pnl_RecipeDetail.Controls.Add(this.lbl_Description, 0, 5);
            this.pnl_RecipeDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_RecipeDetail.Location = new System.Drawing.Point(3, 33);
            this.pnl_RecipeDetail.Name = "pnl_RecipeDetail";
            this.pnl_RecipeDetail.RowCount = 6;
            this.pnl_RecipeDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_RecipeDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_RecipeDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_RecipeDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_RecipeDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_RecipeDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.pnl_RecipeDetail.Size = new System.Drawing.Size(231, 501);
            this.pnl_RecipeDetail.TabIndex = 44;
            // 
            // lbl_RecipeName
            // 
            this.lbl_RecipeName.AutoSize = true;
            this.lbl_RecipeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_RecipeName.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_RecipeName.Location = new System.Drawing.Point(1, 1);
            this.lbl_RecipeName.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_RecipeName.Name = "lbl_RecipeName";
            this.lbl_RecipeName.Size = new System.Drawing.Size(91, 82);
            this.lbl_RecipeName.TabIndex = 0;
            this.lbl_RecipeName.Text = "Recipe Name";
            this.lbl_RecipeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PPID
            // 
            this.lbl_PPID.AutoSize = true;
            this.lbl_PPID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_PPID.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_PPID.Location = new System.Drawing.Point(1, 84);
            this.lbl_PPID.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_PPID.Name = "lbl_PPID";
            this.lbl_PPID.Size = new System.Drawing.Size(91, 82);
            this.lbl_PPID.TabIndex = 1;
            this.lbl_PPID.Text = "PPID";
            this.lbl_PPID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TotalProcessTime
            // 
            this.lbl_TotalProcessTime.AutoSize = true;
            this.lbl_TotalProcessTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TotalProcessTime.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_TotalProcessTime.Location = new System.Drawing.Point(1, 167);
            this.lbl_TotalProcessTime.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_TotalProcessTime.Name = "lbl_TotalProcessTime";
            this.lbl_TotalProcessTime.Size = new System.Drawing.Size(91, 82);
            this.lbl_TotalProcessTime.TabIndex = 2;
            this.lbl_TotalProcessTime.Text = "Total Process Time";
            this.lbl_TotalProcessTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CreatedBy
            // 
            this.lbl_CreatedBy.AutoSize = true;
            this.lbl_CreatedBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_CreatedBy.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_CreatedBy.Location = new System.Drawing.Point(1, 250);
            this.lbl_CreatedBy.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_CreatedBy.Name = "lbl_CreatedBy";
            this.lbl_CreatedBy.Size = new System.Drawing.Size(91, 82);
            this.lbl_CreatedBy.TabIndex = 3;
            this.lbl_CreatedBy.Text = "Created By";
            this.lbl_CreatedBy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ModifiedDate
            // 
            this.lbl_ModifiedDate.AutoSize = true;
            this.lbl_ModifiedDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ModifiedDate.Font = new System.Drawing.Font("맑은 고딕", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_ModifiedDate.Location = new System.Drawing.Point(1, 333);
            this.lbl_ModifiedDate.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_ModifiedDate.Name = "lbl_ModifiedDate";
            this.lbl_ModifiedDate.Size = new System.Drawing.Size(91, 82);
            this.lbl_ModifiedDate.TabIndex = 4;
            this.lbl_ModifiedDate.Text = "Modified Date";
            this.lbl_ModifiedDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Description
            // 
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Description.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.lbl_Description.Location = new System.Drawing.Point(1, 416);
            this.lbl_Description.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(91, 84);
            this.lbl_Description.TabIndex = 5;
            this.lbl_Description.Text = "Description";
            this.lbl_Description.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_ProcessRecipeBtn2
            // 
            this.pnl_ProcessRecipeBtn2.ColumnCount = 4;
            this.pnl_ProcessRecipeBtn2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_ProcessRecipeBtn2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_ProcessRecipeBtn2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_ProcessRecipeBtn2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnl_ProcessRecipeBtn2.Controls.Add(this.btn_DelStep, 3, 0);
            this.pnl_ProcessRecipeBtn2.Controls.Add(this.btn_AddStep, 2, 0);
            this.pnl_ProcessRecipeBtn2.Controls.Add(this.btn_MoveDown, 1, 0);
            this.pnl_ProcessRecipeBtn2.Controls.Add(this.btn_MoveUp, 0, 0);
            this.pnl_ProcessRecipeBtn2.Location = new System.Drawing.Point(293, 563);
            this.pnl_ProcessRecipeBtn2.Name = "pnl_ProcessRecipeBtn2";
            this.pnl_ProcessRecipeBtn2.RowCount = 1;
            this.pnl_ProcessRecipeBtn2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_ProcessRecipeBtn2.Size = new System.Drawing.Size(461, 119);
            this.pnl_ProcessRecipeBtn2.TabIndex = 48;
            // 
            // btn_MoveUp
            // 
            this.btn_MoveUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MoveUp.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_MoveUp.Location = new System.Drawing.Point(3, 3);
            this.btn_MoveUp.Name = "btn_MoveUp";
            this.btn_MoveUp.Size = new System.Drawing.Size(109, 113);
            this.btn_MoveUp.TabIndex = 0;
            this.btn_MoveUp.Text = "Move\r\nUp";
            this.btn_MoveUp.UseVisualStyleBackColor = true;
            // 
            // btn_MoveDown
            // 
            this.btn_MoveDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_MoveDown.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_MoveDown.Location = new System.Drawing.Point(118, 3);
            this.btn_MoveDown.Name = "btn_MoveDown";
            this.btn_MoveDown.Size = new System.Drawing.Size(109, 113);
            this.btn_MoveDown.TabIndex = 1;
            this.btn_MoveDown.Text = "Move\r\nDown";
            this.btn_MoveDown.UseVisualStyleBackColor = true;
            // 
            // btn_AddStep
            // 
            this.btn_AddStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_AddStep.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_AddStep.Location = new System.Drawing.Point(233, 3);
            this.btn_AddStep.Name = "btn_AddStep";
            this.btn_AddStep.Size = new System.Drawing.Size(109, 113);
            this.btn_AddStep.TabIndex = 2;
            this.btn_AddStep.Text = "Add Step";
            this.btn_AddStep.UseVisualStyleBackColor = true;
            // 
            // btn_DelStep
            // 
            this.btn_DelStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_DelStep.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.btn_DelStep.Location = new System.Drawing.Point(348, 3);
            this.btn_DelStep.Name = "btn_DelStep";
            this.btn_DelStep.Size = new System.Drawing.Size(110, 113);
            this.btn_DelStep.TabIndex = 3;
            this.btn_DelStep.Text = "Delete\r\nStep";
            this.btn_DelStep.UseVisualStyleBackColor = true;
            // 
            // ProcessRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_ProcessRecipeBtn2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.bigpnl_ProcessFlow);
            this.Controls.Add(this.pnl_ProcessRecipeBtn);
            this.Controls.Add(this.pnl_ProcessRecipeList);
            this.Name = "ProcessRecipe";
            this.Size = new System.Drawing.Size(1000, 700);
            this.pnl_ProcessFlow.ResumeLayout(false);
            this.pnl_ProcessFlow.PerformLayout();
            this.pnl_ProcessRecipeList.ResumeLayout(false);
            this.pnl_ProcessRecipeBtn.ResumeLayout(false);
            this.bigpnl_ProcessFlow.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnl_RecipeDetail.ResumeLayout(false);
            this.pnl_RecipeDetail.PerformLayout();
            this.pnl_ProcessRecipeBtn2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_Recipe;
        private System.Windows.Forms.TableLayoutPanel pnl_ProcessFlow;
        private System.Windows.Forms.TableLayoutPanel pnl_ProcessRecipeList;
        private System.Windows.Forms.Label lbl_ProcessRecipeList;
        private System.Windows.Forms.TableLayoutPanel pnl_ProcessRecipeBtn;
        private System.Windows.Forms.Button btn_ProcessRecipeOpenFolder;
        private System.Windows.Forms.Button btn_ProcessRecipeDelete;
        private System.Windows.Forms.Button btn_ProcessRecipeSaveAs;
        private System.Windows.Forms.Button btn_ProcessRecipeSave;
        private System.Windows.Forms.Button btn_ProcessRecipeNew;
        private System.Windows.Forms.TableLayoutPanel bigpnl_ProcessFlow;
        private System.Windows.Forms.Label lbl_ProcessFlow;
        private System.Windows.Forms.Label lbl_ProcessTime;
        private System.Windows.Forms.Label lbl_RecipePPID;
        private System.Windows.Forms.Label lbl_Module;
        private System.Windows.Forms.Label lbl_Step;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_RecipeDetail;
        private System.Windows.Forms.TableLayoutPanel pnl_RecipeDetail;
        private System.Windows.Forms.Label lbl_RecipeName;
        private System.Windows.Forms.Label lbl_PPID;
        private System.Windows.Forms.Label lbl_TotalProcessTime;
        private System.Windows.Forms.Label lbl_CreatedBy;
        private System.Windows.Forms.Label lbl_ModifiedDate;
        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.TableLayoutPanel pnl_ProcessRecipeBtn2;
        private System.Windows.Forms.Button btn_DelStep;
        private System.Windows.Forms.Button btn_AddStep;
        private System.Windows.Forms.Button btn_MoveDown;
        private System.Windows.Forms.Button btn_MoveUp;
    }
}
