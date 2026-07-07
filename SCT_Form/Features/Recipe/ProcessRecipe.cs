using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class ProcessRecipe : UserControl
    {
        private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        private readonly Dictionary<string, Control> detailInputs = new Dictionary<string, Control>();
        private readonly List<ProcessRecipeStep> processSteps = new List<ProcessRecipeStep>();
        private string currentRecipePath;
        private int selectedStepIndex = -1;
        private MainGUI main;

        public ProcessRecipe()
        {
            InitializeComponent();
            InitializeProcessRecipeScreen();
        }

        public ProcessRecipe(MainGUI mainGUI) : this()
        {
            main = mainGUI;
        }

        private string GetCurrentUserName()
        {
            if (main != null && main.tBox_ID != null && !string.IsNullOrWhiteSpace(main.tBox_ID.Text))
            {
                return main.tBox_ID.Text.Trim();
            }

            return Environment.UserName;
        }

        private void InitializeProcessRecipeScreen()
        {
            listView_Recipe.View = View.List;
            listView_Recipe.FullRowSelect = true;
            listView_Recipe.MultiSelect = false;
            listView_Recipe.Font = new Font(listView_Recipe.Font.FontFamily, 22F, FontStyle.Regular);
            listView_Recipe.SelectedIndexChanged += listView_Recipe_SelectedIndexChanged;

            btn_ProcessRecipeNew.Click += btn_ProcessRecipeNew_Click;
            btn_ProcessRecipeSave.Click += btn_ProcessRecipeSave_Click;
            btn_ProcessRecipeSaveAs.Click += btn_ProcessRecipeSaveAs_Click;
            btn_ProcessRecipeDelete.Click += btn_ProcessRecipeDelete_Click;
            btn_ProcessRecipeOpenFolder.Click += btn_ProcessRecipeOpenFolder_Click;

            btn_AddStep.Click += btn_AddStep_Click;
            btn_MoveUp.Click += btn_MoveUp_Click;
            btn_MoveDown.Click += btn_MoveDown_Click;
            btn_DelStep.Click += btn_DelStep_Click;

            pnl_ProcessFlow.AutoScroll = true;
            EnsureProcessRecipeFolder();
            BuildDetailEditor(null);
            RebuildProcessFlow();
            LoadProcessRecipeList();
        }

        private void btn_ProcessRecipeNew_Click(object sender, EventArgs e)
        {
            currentRecipePath = null;
            selectedStepIndex = -1;
            processSteps.Clear();
            listView_Recipe.SelectedItems.Clear();
            BuildDetailEditor(null);
            RebuildProcessFlow();
        }

        private void btn_ProcessRecipeSave_Click(object sender, EventArgs e)
        {
            SaveProcessRecipe(GetDefaultProcessRecipeFolderPath(), true);
        }

        private void btn_ProcessRecipeSaveAs_Click(object sender, EventArgs e)
        {
            EnsureProcessRecipeFolder();

            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Process Recipe save folder";
                dialog.SelectedPath = GetDefaultProcessRecipeFolderPath();
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    SaveProcessRecipe(dialog.SelectedPath, false);
                }
            }
        }

        private void btn_ProcessRecipeDelete_Click(object sender, EventArgs e)
        {
            if (listView_Recipe.SelectedItems.Count == 0)
            {
                MessageBox.Show("Delete할 Process Recipe를 선택하세요.", "Process Recipe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListViewItem selectedItem = listView_Recipe.SelectedItems[0];
            string filePath = selectedItem.Tag as string;
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return;

            DialogResult result = MessageBox.Show(
                selectedItem.Text + " Process Recipe를 삭제하시겠습니까?",
                "Process Recipe Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            File.Delete(filePath);
            currentRecipePath = null;
            selectedStepIndex = -1;
            processSteps.Clear();
            LoadProcessRecipeList();
            BuildDetailEditor(null);
            RebuildProcessFlow();
        }

        private void btn_ProcessRecipeOpenFolder_Click(object sender, EventArgs e)
        {
            EnsureProcessRecipeFolder();
            Process.Start("explorer.exe", GetDefaultProcessRecipeFolderPath());
        }

        private void listView_Recipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Recipe.SelectedItems.Count == 0) return;

            string filePath = listView_Recipe.SelectedItems[0].Tag as string;
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return;

            try
            {
                ProcessRecipeData recipe = ReadProcessRecipe(filePath);
                if (recipe == null) return;

                currentRecipePath = filePath;
                selectedStepIndex = -1;
                processSteps.Clear();
                if (recipe.Steps != null) processSteps.AddRange(recipe.Steps);

                NormalizeStepNumbers();
                BuildDetailEditor(recipe);
                RebuildProcessFlow();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Process Recipe 파일을 읽을 수 없습니다.\r\n" + ex.Message, "Process Recipe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_AddStep_Click(object sender, EventArgs e)
        {
            ChamberRecipeSelection selection = GetDefaultChamberRecipeSelection("PM A");
            ProcessRecipeStep step = new ProcessRecipeStep();
            step.StepNo = processSteps.Count + 1;
            step.Recipe = selection ?? new ChamberRecipeSelection { Module = "PM A" };

            processSteps.Add(step);
            selectedStepIndex = processSteps.Count - 1;
            NormalizeStepNumbers();
            RebuildProcessFlow();
            RefreshTotalProcessTime();
        }

        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            if (selectedStepIndex <= 0) return;

            ProcessRecipeStep selectedStep = processSteps[selectedStepIndex];
            processSteps[selectedStepIndex] = processSteps[selectedStepIndex - 1];
            processSteps[selectedStepIndex - 1] = selectedStep;
            selectedStepIndex--;
            NormalizeStepNumbers();
            RebuildProcessFlow();
        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            if (selectedStepIndex < 0 || selectedStepIndex >= processSteps.Count - 1) return;

            ProcessRecipeStep selectedStep = processSteps[selectedStepIndex];
            processSteps[selectedStepIndex] = processSteps[selectedStepIndex + 1];
            processSteps[selectedStepIndex + 1] = selectedStep;
            selectedStepIndex++;
            NormalizeStepNumbers();
            RebuildProcessFlow();
        }

        private void btn_DelStep_Click(object sender, EventArgs e)
        {
            if (selectedStepIndex < 0 || selectedStepIndex >= processSteps.Count)
            {
                MessageBox.Show("삭제할 Step을 선택하세요.", "Process Step", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            processSteps.RemoveAt(selectedStepIndex);
            if (selectedStepIndex >= processSteps.Count) selectedStepIndex = processSteps.Count - 1;
            NormalizeStepNumbers();
            RebuildProcessFlow();
            RefreshTotalProcessTime();
        }

        private void LoadProcessRecipeList()
        {
            listView_Recipe.Items.Clear();
            EnsureProcessRecipeFolder();

            foreach (string filePath in Directory.GetFiles(GetDefaultProcessRecipeFolderPath(), "*.json").OrderBy(Path.GetFileNameWithoutExtension))
            {
                ListViewItem item = new ListViewItem(Path.GetFileNameWithoutExtension(filePath));
                item.Tag = filePath;
                listView_Recipe.Items.Add(item);
            }
        }

        private void BuildDetailEditor(ProcessRecipeData recipe)
        {
            detailInputs.Clear();
            pnl_RecipeDetail.SuspendLayout();
            pnl_RecipeDetail.Controls.Clear();
            pnl_RecipeDetail.RowStyles.Clear();
            pnl_RecipeDetail.ColumnStyles.Clear();
            pnl_RecipeDetail.ColumnCount = 2;
            pnl_RecipeDetail.RowCount = 6;
            pnl_RecipeDetail.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            pnl_RecipeDetail.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

            AddDetailRow(0, "RecipeName", "Recipe Name", GetRecipeValue(recipe, "RecipeName"), false, false);
            AddDetailRow(1, "PPID", "PPID", GetRecipeValue(recipe, "PPID"), false, false);
            AddDetailRow(2, "TotalProcessTime", "Total Process Time", GetRecipeValue(recipe, "TotalProcessTime"), true, false);
            AddDetailRow(3, "CreatedBy", "Created By", GetRecipeValue(recipe, "CreatedBy"), true, false);
            AddDetailRow(4, "ModifiedDate", "Modified Date", GetRecipeValue(recipe, "ModifiedDate"), true, false);
            AddDetailRow(5, "Description", "Description", GetRecipeValue(recipe, "Description"), false, true);

            pnl_RecipeDetail.ResumeLayout();
        }

        private void AddDetailRow(int row, string key, string labelText, string value, bool readOnly, bool multiline)
        {
            pnl_RecipeDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 6F));

            Label label = new Label();
            label.Dock = DockStyle.Fill;
            label.Font = new Font("맑은 고딕", labelText == "Description" ? 10F : 11F, FontStyle.Bold);
            label.Margin = new Padding(0);
            label.Text = labelText;
            label.TextAlign = ContentAlignment.MiddleCenter;

            TextBox textBox = new TextBox();
            textBox.Font = new Font("맑은 고딕", 10F);
            textBox.Multiline = multiline;
            textBox.ReadOnly = readOnly;
            textBox.Text = value;
            if (multiline)
            {
                textBox.Dock = DockStyle.Fill;
                textBox.Margin = new Padding(4);
            }
            else
            {
                textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                textBox.AutoSize = false;
                textBox.Height = 26;
                textBox.Margin = new Padding(4, 0, 4, 0);
            }

            pnl_RecipeDetail.Controls.Add(label, 0, row);
            pnl_RecipeDetail.Controls.Add(textBox, 1, row);
            detailInputs[key] = textBox;
        }

        private void RebuildProcessFlow()
        {
            pnl_ProcessFlow.SuspendLayout();
            pnl_ProcessFlow.Controls.Clear();
            pnl_ProcessFlow.RowStyles.Clear();
            pnl_ProcessFlow.ColumnStyles.Clear();
            pnl_ProcessFlow.ColumnCount = 4;
            pnl_ProcessFlow.RowCount = Math.Max(2, processSteps.Count + 1);
            pnl_ProcessFlow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11F));
            pnl_ProcessFlow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 29F));
            pnl_ProcessFlow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            pnl_ProcessFlow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            pnl_ProcessFlow.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));

            AddFlowHeader(lbl_Step, 0);
            AddFlowHeader(lbl_Module, 1);
            AddFlowHeader(lbl_RecipePPID, 2);
            AddFlowHeader(lbl_ProcessTime, 3);

            for (int i = 0; i < processSteps.Count; i++)
            {
                pnl_ProcessFlow.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
                AddFlowStepRow(i);
            }

            if (processSteps.Count == 0)
            {
                pnl_ProcessFlow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            }

            pnl_ProcessFlow.ResumeLayout();
        }

        private void AddFlowHeader(Label header, int column)
        {
            header.Dock = DockStyle.Fill;
            header.Margin = new Padding(0);
            pnl_ProcessFlow.Controls.Add(header, column, 0);
        }

        private void AddFlowStepRow(int index)
        {
            ProcessRecipeStep step = processSteps[index];
            if (step.Recipe == null) step.Recipe = new ChamberRecipeSelection { Module = "PM A" };

            int row = index + 1;
            Color rowColor = index == selectedStepIndex ? Color.LightSkyBlue : Color.White;

            TextBox stepBox = CreateReadonlyFlowTextBox(step.StepNo.ToString(), rowColor);
            AddSelectableFlowControl(stepBox, index, 0, row);

            ComboBox moduleComboBox = new ComboBox();
            moduleComboBox.Dock = DockStyle.Fill;
            moduleComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            moduleComboBox.Font = new Font("맑은 고딕", 10F);
            moduleComboBox.Margin = new Padding(2);
            moduleComboBox.BackColor = rowColor;
            moduleComboBox.Items.AddRange(new object[] { "PM A", "PM B", "PM C" });
            moduleComboBox.SelectedItem = NormalizeModule(step.Recipe.Module);
            moduleComboBox.SelectedIndexChanged += delegate
            {
                selectedStepIndex = index;
                string selectedModule = Convert.ToString(moduleComboBox.SelectedItem);
                processSteps[index].Recipe = GetDefaultChamberRecipeSelection(selectedModule) ?? new ChamberRecipeSelection { Module = selectedModule };
                RebuildProcessFlow();
                RefreshTotalProcessTime();
            };
            AddSelectableFlowControl(moduleComboBox, index, 1, row);

            ComboBox ppidComboBox = new ComboBox();
            ppidComboBox.Dock = DockStyle.Fill;
            ppidComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ppidComboBox.Font = new Font("맑은 고딕", 10F);
            ppidComboBox.Margin = new Padding(2);
            ppidComboBox.BackColor = rowColor;

            List<ChamberRecipeSelection> selections = GetChamberRecipeSelections(step.Recipe.Module);
            foreach (ChamberRecipeSelection selection in selections)
            {
                ppidComboBox.Items.Add(selection.RecipePPID);
            }

            if (!string.IsNullOrWhiteSpace(step.Recipe.RecipePPID) && ppidComboBox.Items.Contains(step.Recipe.RecipePPID))
            {
                ppidComboBox.SelectedItem = step.Recipe.RecipePPID;
            }
            else if (ppidComboBox.Items.Count > 0)
            {
                ppidComboBox.SelectedIndex = 0;
                processSteps[index].Recipe = selections[0];
            }

            ppidComboBox.SelectedIndexChanged += delegate
            {
                selectedStepIndex = index;
                string selectedPpid = Convert.ToString(ppidComboBox.SelectedItem);
                ChamberRecipeSelection selection = selections.FirstOrDefault(item =>
                    string.Equals(item.RecipePPID, selectedPpid, StringComparison.OrdinalIgnoreCase));
                if (selection != null) processSteps[index].Recipe = selection;
                RebuildProcessFlow();
                RefreshTotalProcessTime();
            };
            AddSelectableFlowControl(ppidComboBox, index, 2, row);

            int processTime = step.Recipe == null ? 0 : step.Recipe.ProcessTime;
            TextBox processTimeBox = CreateReadonlyFlowTextBox(processTime.ToString(), rowColor);
            AddSelectableFlowControl(processTimeBox, index, 3, row);
        }

        private TextBox CreateReadonlyFlowTextBox(string text, Color backColor)
        {
            TextBox textBox = new TextBox();
            textBox.Dock = DockStyle.Fill;
            textBox.Font = new Font("맑은 고딕", 10F);
            textBox.Margin = new Padding(2);
            textBox.ReadOnly = true;
            textBox.Text = text;
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.BackColor = backColor;
            return textBox;
        }

        private void AddSelectableFlowControl(Control control, int index, int column, int row)
        {
            if (control is ComboBox)
            {
                control.MouseDown += delegate
                {
                    selectedStepIndex = index;
                };
            }
            else
            {
                control.Click += delegate
                {
                    selectedStepIndex = index;
                    RebuildProcessFlow();
                };
            }

            pnl_ProcessFlow.Controls.Add(control, column, row);
        }

        private void SaveProcessRecipe(string folderPath, bool replaceDefaultFile)
        {
            string validationMessage;
            ProcessRecipeData recipe = CreateProcessRecipeDataFromInputs(out validationMessage);
            if (recipe == null)
            {
                MessageBox.Show(validationMessage, "Process Recipe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, SanitizeFileName(recipe.RecipeName) + ".json");

            string duplicatePath;
            if (HasDuplicatePpid(recipe.PPID, filePath, out duplicatePath))
            {
                MessageBox.Show("같은 PPID를 사용하는 Process Recipe가 있습니다.\r\n" + duplicatePath, "Process Recipe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            File.WriteAllText(filePath, jsonSerializer.Serialize(recipe), Encoding.UTF8);

            if (replaceDefaultFile && !string.IsNullOrWhiteSpace(currentRecipePath) &&
                !PathsEqual(currentRecipePath, filePath) && File.Exists(currentRecipePath))
            {
                File.Delete(currentRecipePath);
            }

            currentRecipePath = filePath;
            LoadProcessRecipeList();
            SelectRecipeInList(filePath);
        }

        private ProcessRecipeData CreateProcessRecipeDataFromInputs(out string validationMessage)
        {
            validationMessage = string.Empty;

            string recipeName = GetDetailInputValue("RecipeName");
            string ppid = GetDetailInputValue("PPID");

            if (string.IsNullOrWhiteSpace(recipeName))
            {
                validationMessage = "Recipe Name을 입력하세요.";
                return null;
            }

            if (string.IsNullOrWhiteSpace(ppid))
            {
                validationMessage = "PPID를 입력하세요.";
                return null;
            }

            if (processSteps.Count == 0)
            {
                validationMessage = "Process Step을 1개 이상 추가하세요.";
                return null;
            }

            foreach (ProcessRecipeStep step in processSteps)
            {
                if (step.Recipe == null ||
                    string.IsNullOrWhiteSpace(step.Recipe.Module) ||
                    string.IsNullOrWhiteSpace(step.Recipe.RecipePPID))
                {
                    validationMessage = "모든 Step의 Module과 Recipe PPID를 선택하세요.";
                    return null;
                }

                if (step.Recipe.ProcessTime <= 0)
                {
                    validationMessage = "Process Time이 0보다 큰 Recipe만 Step에 사용할 수 있습니다.";
                    return null;
                }
            }

            NormalizeStepNumbers();

            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string createdBy = string.IsNullOrWhiteSpace(GetDetailInputValue("CreatedBy"))
                ? GetCurrentUserName()
                : GetDetailInputValue("CreatedBy");

            ProcessRecipeData recipe = new ProcessRecipeData();
            recipe.RecipeName = recipeName;
            recipe.PPID = ppid;
            recipe.Description = GetDetailInputValue("Description");
            recipe.TotalProcessTime = GetTotalProcessTime();
            recipe.CreatedBy = createdBy;
            recipe.ModifiedDate = now;
            recipe.Steps = processSteps.Select(CloneProcessStep).ToList();

            SetDetailInputValue("TotalProcessTime", recipe.TotalProcessTime.ToString());
            SetDetailInputValue("CreatedBy", recipe.CreatedBy);
            SetDetailInputValue("ModifiedDate", recipe.ModifiedDate);
            return recipe;
        }

        private ProcessRecipeStep CloneProcessStep(ProcessRecipeStep source)
        {
            ProcessRecipeStep clone = new ProcessRecipeStep();
            clone.StepNo = source.StepNo;
            clone.Recipe = source.Recipe == null ? null : source.Recipe.Clone();
            return clone;
        }

        private string GetRecipeValue(ProcessRecipeData recipe, string key)
        {
            if (recipe == null)
            {
                if (key == "CreatedBy") return GetCurrentUserName();
                if (key == "ModifiedDate") return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (key == "TotalProcessTime") return GetTotalProcessTime().ToString();
                return string.Empty;
            }

            if (key == "RecipeName") return recipe.RecipeName;
            if (key == "PPID") return recipe.PPID;
            if (key == "TotalProcessTime") return recipe.TotalProcessTime.ToString();
            if (key == "CreatedBy") return recipe.CreatedBy;
            if (key == "ModifiedDate") return recipe.ModifiedDate;
            if (key == "Description") return recipe.Description;
            return string.Empty;
        }

        private string GetDetailInputValue(string key)
        {
            if (!detailInputs.ContainsKey(key)) return string.Empty;
            return detailInputs[key].Text.Trim();
        }

        private void SetDetailInputValue(string key, string value)
        {
            if (detailInputs.ContainsKey(key)) detailInputs[key].Text = value;
        }

        private void RefreshTotalProcessTime()
        {
            SetDetailInputValue("TotalProcessTime", GetTotalProcessTime().ToString());
        }

        private int GetTotalProcessTime()
        {
            return processSteps.Sum(step => step.Recipe == null ? 0 : step.Recipe.ProcessTime);
        }

        private void NormalizeStepNumbers()
        {
            for (int i = 0; i < processSteps.Count; i++)
            {
                processSteps[i].StepNo = i + 1;
            }
        }

        private ChamberRecipeSelection GetDefaultChamberRecipeSelection(string module)
        {
            return GetChamberRecipeSelections(module).FirstOrDefault();
        }

        private List<ChamberRecipeSelection> GetChamberRecipeSelections(string module)
        {
            string normalizedModule = NormalizeModule(module);
            string folderPath = Path.Combine(GetDefaultRecipeRootPath(), normalizedModule);
            List<ChamberRecipeSelection> selections = new List<ChamberRecipeSelection>();

            if (!Directory.Exists(folderPath)) return selections;

            foreach (string filePath in Directory.GetFiles(folderPath, "*.json").OrderBy(Path.GetFileNameWithoutExtension))
            {
                ChamberRecipeData recipe = ReadChamberRecipe(filePath);
                string ppid = GetChamberRecipePpid(recipe);
                if (string.IsNullOrWhiteSpace(ppid)) continue;

                ChamberRecipeSelection selection = new ChamberRecipeSelection();
                selection.Module = normalizedModule;
                selection.RecipePath = filePath;
                selection.RecipeName = GetChamberRecipeName(recipe, filePath);
                selection.RecipePPID = ppid;
                selection.ProcessTime = GetChamberRecipeProcessTime(recipe);
                selections.Add(selection);
            }

            return selections;
        }

        private ChamberRecipeData ReadChamberRecipe(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return null;
                return jsonSerializer.Deserialize<ChamberRecipeData>(File.ReadAllText(filePath, Encoding.UTF8));
            }
            catch
            {
                return null;
            }
        }

        private ProcessRecipeData ReadProcessRecipe(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return null;
                return jsonSerializer.Deserialize<ProcessRecipeData>(File.ReadAllText(filePath, Encoding.UTF8));
            }
            catch
            {
                return null;
            }
        }

        private string GetChamberRecipeName(ChamberRecipeData recipe, string filePath)
        {
            if (recipe != null && !string.IsNullOrWhiteSpace(recipe.RecipeName)) return recipe.RecipeName;
            return Path.GetFileNameWithoutExtension(filePath);
        }

        private string GetChamberRecipePpid(ChamberRecipeData recipe)
        {
            if (recipe == null) return string.Empty;
            if (!string.IsNullOrWhiteSpace(recipe.PPID)) return recipe.PPID.Trim();
            if (recipe.Fields != null && recipe.Fields.ContainsKey("PPID")) return recipe.Fields["PPID"].Trim();
            return string.Empty;
        }

        private int GetChamberRecipeProcessTime(ChamberRecipeData recipe)
        {
            if (recipe == null || recipe.Fields == null || !recipe.Fields.ContainsKey("Process Time")) return 0;

            int processTime;
            return int.TryParse(recipe.Fields["Process Time"], out processTime) ? processTime : 0;
        }

        private void SelectRecipeInList(string filePath)
        {
            foreach (ListViewItem item in listView_Recipe.Items)
            {
                if (PathsEqual(item.Tag as string, filePath))
                {
                    item.Selected = true;
                    item.Focused = true;
                    item.EnsureVisible();
                    break;
                }
            }
        }

        private bool HasDuplicatePpid(string ppid, string targetFilePath, out string duplicatePath)
        {
            duplicatePath = null;
            if (string.IsNullOrWhiteSpace(ppid)) return false;

            string folderPath = Path.GetDirectoryName(targetFilePath);
            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath)) return false;

            foreach (string filePath in Directory.GetFiles(folderPath, "*.json"))
            {
                if (PathsEqual(filePath, targetFilePath)) continue;
                if (!string.IsNullOrWhiteSpace(currentRecipePath) && PathsEqual(filePath, currentRecipePath)) continue;

                ProcessRecipeData recipe = ReadProcessRecipe(filePath);
                if (recipe != null && string.Equals(recipe.PPID, ppid, StringComparison.OrdinalIgnoreCase))
                {
                    duplicatePath = filePath;
                    return true;
                }
            }

            return false;
        }

        private void EnsureProcessRecipeFolder()
        {
            Directory.CreateDirectory(GetDefaultRecipeRootPath());
            Directory.CreateDirectory(GetDefaultProcessRecipeFolderPath());
        }

        private string GetDefaultProcessRecipeFolderPath()
        {
            return Path.Combine(GetDefaultRecipeRootPath(), "Process");
        }

        private string GetDefaultRecipeRootPath()
        {
            return AppDataPaths.RecipeRootPath;
        }

        private string NormalizeModule(string module)
        {
            if (string.Equals(module, "PM B", StringComparison.OrdinalIgnoreCase)) return "PM B";
            if (string.Equals(module, "PM C", StringComparison.OrdinalIgnoreCase)) return "PM C";
            return "PM A";
        }

        private string SanitizeFileName(string fileName)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidChar, '_');
            }

            return fileName.Trim();
        }

        private bool PathsEqual(string firstPath, string secondPath)
        {
            if (string.IsNullOrWhiteSpace(firstPath) || string.IsNullOrWhiteSpace(secondPath)) return false;
            return string.Equals(
                Path.GetFullPath(firstPath).TrimEnd(Path.DirectorySeparatorChar),
                Path.GetFullPath(secondPath).TrimEnd(Path.DirectorySeparatorChar),
                StringComparison.OrdinalIgnoreCase);
        }

        public class ProcessRecipeData
        {
            public string RecipeName { get; set; }
            public string PPID { get; set; }
            public string Description { get; set; }
            public int TotalProcessTime { get; set; }
            public string CreatedBy { get; set; }
            public string ModifiedDate { get; set; }
            public List<ProcessRecipeStep> Steps { get; set; }
        }

        public class ProcessRecipeStep
        {
            public int StepNo { get; set; }
            public ChamberRecipeSelection Recipe { get; set; }
        }

        public class ChamberRecipeData
        {
            public string PM { get; set; }
            public string RecipeType { get; set; }
            public string RecipeName { get; set; }
            public string PPID { get; set; }
            public string Description { get; set; }
            public string CreatedBy { get; set; }
            public string ModifiedDate { get; set; }
            public Dictionary<string, string> Fields { get; set; }
        }
    }
}
