using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class RecipeGUI : UserControl
    {
        private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        private readonly Dictionary<string, Control> recipeInputs = new Dictionary<string, Control>();
        private MainGUI main;
        private ProcessRecipe processRecipeGUI;
        private string currentPm = "PM A";
        private string currentRecipePath;
        private bool isProcessRecipeMode;

        public RecipeGUI()
        {
            InitializeComponent();
            InitializeRecipeScreen();
        }

        public RecipeGUI(MainGUI mainGUI)
        {
            InitializeComponent();
            main = mainGUI;
            InitializeRecipeScreen();
        }

        internal void ShowDefaultPmA()
        {
            SelectPm("PM A");
        }

        private void InitializeRecipeScreen()
        {
            listView_Recipe.View = View.List;
            listView_Recipe.FullRowSelect = true;
            listView_Recipe.MultiSelect = false;
            listView_Recipe.Font = new Font(listView_Recipe.Font.FontFamily, 22F, FontStyle.Regular);
            listView_Recipe.SelectedIndexChanged += listView_Recipe_SelectedIndexChanged;

            btn_RecipeNew.Click += btn_RecipeNew_Click;
            btn_RecipeSave.Click += btn_RecipeSave_Click;
            btn_RecipeSaveAs.Click += btn_RecipeSaveAs_Click;
            btn_RecipeDelete.Click += btn_RecipeDelete_Click;
            btn_OpenFolder.Click += btn_OpenFolder_Click;

            SelectPm("PM A");
        }

        private void btn_PMA_Click(object sender, EventArgs e)
        {
            SelectPm("PM A");
        }

        private void btn_PMB_Click(object sender, EventArgs e)
        {
            SelectPm("PM B");
        }

        private void btn_PMC_Click(object sender, EventArgs e)
        {
            SelectPm("PM C");
        }

        private void SelectPm(string pmName)
        {
            ShowChamberRecipeView();
            currentPm = pmName;
            currentRecipePath = null;

            EnsureRecipeFolders();
            ApplyPmButtonStyle();
            BuildRecipeEditor(null);
            LoadRecipeList();
        }

        private void ApplyPmButtonStyle()
        {
            ApplyPmButtonStyle(btn_PMA, !isProcessRecipeMode && currentPm == "PM A");
            ApplyPmButtonStyle(btn_PMB, !isProcessRecipeMode && currentPm == "PM B");
            ApplyPmButtonStyle(btn_PMC, !isProcessRecipeMode && currentPm == "PM C");
            ApplyPmButtonStyle(btn_ProcessRecipe, isProcessRecipeMode);
        }

        private void ApplyPmButtonStyle(Button button, bool isActive)
        {
            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = isActive ? 2 : 1;
            button.FlatAppearance.BorderColor = isActive ? Color.White : Color.Black;
            button.BackColor = isActive ? Color.SkyBlue : Color.FromArgb(60, 60, 60);
            button.ForeColor = isActive ? Color.White : Color.DimGray;
        }

        private void LoadRecipeList()
        {
            listView_Recipe.Items.Clear();

            string folderPath = GetCurrentPmFolderPath();
            if (!Directory.Exists(folderPath)) return;

            foreach (string filePath in Directory.GetFiles(folderPath, "*.json").OrderBy(Path.GetFileNameWithoutExtension))
            {
                ListViewItem item = new ListViewItem(Path.GetFileNameWithoutExtension(filePath));
                item.Tag = filePath;
                listView_Recipe.Items.Add(item);
            }
        }

        private void BuildRecipeEditor(RecipeData recipe)
        {
            List<FieldDefinition> fields = GetFieldDefinitions();

            recipeInputs.Clear();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel2.Controls.Clear();
            tableLayoutPanel2.RowStyles.Clear();
            tableLayoutPanel2.ColumnStyles.Clear();
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.RowCount = fields.Count;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53F));

            for (int row = 0; row < fields.Count; row++)
            {
                FieldDefinition field = fields[row];
                tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / fields.Count));

                Label label = new Label();
                label.Dock = DockStyle.Fill;
                label.Font = new Font("맑은 고딕", 12F);
                label.Margin = new Padding(0, 0, 14, 0);
                label.Text = field.Label;
                label.TextAlign = ContentAlignment.MiddleRight;

                Control input = CreateInputControl(field);
                input.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                input.Margin = new Padding(0, 0, 0, 0);
                SetInputValue(input, GetRecipeValue(recipe, field.Key));

                tableLayoutPanel2.Controls.Add(label, 0, row);
                tableLayoutPanel2.Controls.Add(input, 1, row);
                recipeInputs[field.Key] = input;
            }

            tableLayoutPanel2.ResumeLayout();
        }

        private Control CreateInputControl(FieldDefinition field)
        {
            if (field.Options != null && field.Options.Length > 0)
            {
                ComboBox comboBox = new ComboBox();
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.Font = new Font("맑은 고딕", 12F);
                comboBox.Height = 30;
                comboBox.Items.AddRange(field.Options);
                if (comboBox.Items.Count > 0) comboBox.SelectedIndex = 0;
                return comboBox;
            }

            TextBox textBox = new TextBox();
            textBox.AutoSize = false;
            textBox.Font = new Font("맑은 고딕", 12F);
            textBox.Height = 30;
            textBox.ReadOnly = field.IsReadOnly;
            return textBox;
        }

        private string GetRecipeValue(RecipeData recipe, string key)
        {
            if (recipe == null)
            {
                if (key == "Created By") return GetCurrentUserName();
                if (key == "Modified Date") return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return string.Empty;
            }

            if (recipe.Fields != null && recipe.Fields.ContainsKey(key)) return recipe.Fields[key];
            if (key == "Recipe Name") return recipe.RecipeName;
            if (key == "PPID") return recipe.PPID;
            if (key == "Description") return recipe.Description;
            if (key == "Created By") return recipe.CreatedBy;
            if (key == "Modified Date") return recipe.ModifiedDate;
            return string.Empty;
        }

        private void SetInputValue(Control input, string value)
        {
            ComboBox comboBox = input as ComboBox;
            if (comboBox != null)
            {
                int index = comboBox.Items.IndexOf(value);
                comboBox.SelectedIndex = index >= 0 ? index : 0;
                return;
            }

            input.Text = value;
        }

        private string GetInputValue(string key)
        {
            if (!recipeInputs.ContainsKey(key)) return string.Empty;

            ComboBox comboBox = recipeInputs[key] as ComboBox;
            if (comboBox != null) return Convert.ToString(comboBox.SelectedItem);

            return recipeInputs[key].Text.Trim();
        }

        private void btn_RecipeNew_Click(object sender, EventArgs e)
        {
            currentRecipePath = null;
            listView_Recipe.SelectedItems.Clear();
            BuildRecipeEditor(null);
        }

        private void btn_RecipeSave_Click(object sender, EventArgs e)
        {
            SaveRecipe(GetCurrentPmFolderPath(), true);
        }

        private void btn_RecipeSaveAs_Click(object sender, EventArgs e)
        {
            EnsureRecipeFolders();

            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Recipe 저장 폴더를 선택하세요.";
                dialog.SelectedPath = GetCurrentPmFolderPath();
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    SaveRecipe(dialog.SelectedPath, false);
                }
            }
        }

        private void btn_RecipeDelete_Click(object sender, EventArgs e)
        {
            if (listView_Recipe.SelectedItems.Count == 0)
            {
                MessageBox.Show("삭제할 Recipe를 선택하세요.", "Recipe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ListViewItem selectedItem = listView_Recipe.SelectedItems[0];
            string filePath = selectedItem.Tag as string;
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return;

            DialogResult result = MessageBox.Show(
                selectedItem.Text + " Recipe를 삭제하시겠습니까?",
                "Recipe Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            File.Delete(filePath);
            currentRecipePath = null;
            LoadRecipeList();
            BuildRecipeEditor(null);
        }

        private void btn_OpenFolder_Click(object sender, EventArgs e)
        {
            EnsureRecipeFolders();
            Process.Start("explorer.exe", GetCurrentPmFolderPath());
        }

        private void listView_Recipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Recipe.SelectedItems.Count == 0) return;

            string filePath = listView_Recipe.SelectedItems[0].Tag as string;
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return;

            try
            {
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                RecipeData recipe = jsonSerializer.Deserialize<RecipeData>(json);
                currentRecipePath = filePath;
                BuildRecipeEditor(recipe);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Recipe 파일을 읽을 수 없습니다.\r\n" + ex.Message, "Recipe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveRecipe(string folderPath, bool replaceDefaultFile)
        {
            string validationMessage;
            RecipeData recipe = CreateRecipeDataFromInputs(out validationMessage);
            if (recipe == null)
            {
                MessageBox.Show(validationMessage, "Recipe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, SanitizeFileName(recipe.RecipeName) + ".json");
            string duplicatePath;
            if (HasDuplicatePpid(recipe.PPID, filePath, replaceDefaultFile, out duplicatePath))
            {
                MessageBox.Show(
                    "이미 같은 PPID를 사용하는 Recipe가 있습니다.\r\n" + duplicatePath,
                    "Recipe",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string json = jsonSerializer.Serialize(recipe);
            File.WriteAllText(filePath, json, Encoding.UTF8);

            if (replaceDefaultFile && !string.IsNullOrWhiteSpace(currentRecipePath) &&
                !PathsEqual(currentRecipePath, filePath) && File.Exists(currentRecipePath))
            {
                File.Delete(currentRecipePath);
            }

            currentRecipePath = filePath;
            LoadRecipeList();
            SelectRecipeInList(filePath);
            MessageBox.Show("Recipe가 저장되었습니다.", "Recipe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private RecipeData CreateRecipeDataFromInputs(out string validationMessage)
        {
            validationMessage = string.Empty;

            string recipeName = GetInputValue("Recipe Name");
            if (string.IsNullOrWhiteSpace(recipeName))
            {
                validationMessage = "Recipe Name을 입력하세요.";
                return null;
            }

            foreach (FieldDefinition field in GetFieldDefinitions())
            {
                string value = GetInputValue(field.Key);
                int intValue;
                double doubleValue;

                if (field.Key != "Description" && string.IsNullOrWhiteSpace(value))
                {
                    validationMessage = field.Label + "을(를) 입력하세요.";
                    return null;
                }

                if (field.ValueType == RecipeValueType.Int && !int.TryParse(value, out intValue))
                {
                    validationMessage = field.Label + "에는 정수를 입력하세요.";
                    return null;
                }

                if (field.ValueType == RecipeValueType.Double &&
                    !double.TryParse(value, NumberStyles.Float, CultureInfo.CurrentCulture, out doubleValue) &&
                    !double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out doubleValue))
                {
                    validationMessage = field.Label + "에는 숫자를 입력하세요.";
                    return null;
                }
            }

            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Dictionary<string, string> fields = new Dictionary<string, string>();
            foreach (FieldDefinition field in GetFieldDefinitions())
            {
                fields[field.Key] = GetInputValue(field.Key);
            }

            fields["Created By"] = GetCurrentUserName();
            fields["Modified Date"] = now;

            SetInputValue(recipeInputs["Created By"], fields["Created By"]);
            SetInputValue(recipeInputs["Modified Date"], fields["Modified Date"]);

            RecipeData recipe = new RecipeData();
            recipe.PM = currentPm;
            recipe.RecipeType = GetRecipeType(currentPm);
            recipe.RecipeName = recipeName;
            recipe.PPID = GetInputValue("PPID");
            recipe.Description = GetInputValue("Description");
            recipe.CreatedBy = fields["Created By"];
            recipe.ModifiedDate = fields["Modified Date"];
            recipe.Fields = fields;
            return recipe;
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

        private List<FieldDefinition> GetFieldDefinitions()
        {
            List<FieldDefinition> fields = new List<FieldDefinition>();
            fields.Add(new FieldDefinition("Recipe Name", "Recipe Name", RecipeValueType.Text));
            fields.Add(new FieldDefinition("PPID", "PPID", RecipeValueType.Text));
            fields.Add(new FieldDefinition("Description", "Description", RecipeValueType.Text));
            fields.Add(new FieldDefinition("Process Time", "Process Time (sec)", RecipeValueType.Int));

            if (currentPm == "PM A")
            {
                fields.Add(new FieldDefinition("Temperature", "Temperature (℃)", RecipeValueType.Double));
                fields.Add(new FieldDefinition("Pressure", "Pressure (Torr)", RecipeValueType.Double));
                fields.Add(new FieldDefinition("Gas Flow", "Gas Flow (sccm)", RecipeValueType.Double));
                fields.Add(new FieldDefinition("RF Power", "RF Power (W)", RecipeValueType.Double));
            }
            else if (currentPm == "PM B")
            {
                fields.Add(new FieldDefinition("Platen Speed", "Platen Speed (RPM)", RecipeValueType.Int));
                fields.Add(new FieldDefinition("Carrier Speed", "Carrier Speed (RPM)", RecipeValueType.Int));
                fields.Add(new FieldDefinition("Down Force", "Down Force (psi)", RecipeValueType.Double));
                fields.Add(new FieldDefinition("Slurry Flow", "Slurry Flow (ml/min)", RecipeValueType.Double));
                fields.Add(new FieldDefinition("Pad Type", "Pad Type", RecipeValueType.Text, new[] { "Soft", "Hard" }));
            }
            else
            {
                fields.Add(new FieldDefinition("Cleaning Mode", "Cleaning Mode", RecipeValueType.Text, new[] { "DI", "Chemical" }));
                fields.Add(new FieldDefinition("DI Water Flow", "DI Water Flow (L/min)", RecipeValueType.Double));
                fields.Add(new FieldDefinition("Chemical Flow", "Chemical Flow (ml/min)", RecipeValueType.Double));
                fields.Add(new FieldDefinition("Spin Speed", "Spin Speed (RPM)", RecipeValueType.Int));
                fields.Add(new FieldDefinition("Dry Time", "Dry Time (sec)", RecipeValueType.Int));
            }

            fields.Add(new FieldDefinition("Created By", "Created By", RecipeValueType.Text, null, true));
            fields.Add(new FieldDefinition("Modified Date", "Modified Date", RecipeValueType.Text, null, true));
            return fields;
        }

        private string GetRecipeType(string pmName)
        {
            if (pmName == "PM A") return "CVD";
            if (pmName == "PM B") return "CMP";
            return "Cleaning";
        }

        private string GetCurrentUserName()
        {
            if (main != null && main.tBox_ID != null && !string.IsNullOrWhiteSpace(main.tBox_ID.Text))
            {
                return main.tBox_ID.Text.Trim();
            }

            return Environment.UserName;
        }

        private void EnsureRecipeFolders()
        {
            Directory.CreateDirectory(GetDefaultRecipeRootPath());
            Directory.CreateDirectory(Path.Combine(GetDefaultRecipeRootPath(), "PM A"));
            Directory.CreateDirectory(Path.Combine(GetDefaultRecipeRootPath(), "PM B"));
            Directory.CreateDirectory(Path.Combine(GetDefaultRecipeRootPath(), "PM C"));
        }

        private string GetCurrentPmFolderPath()
        {
            return Path.Combine(GetDefaultRecipeRootPath(), currentPm);
        }

        private string GetDefaultRecipeRootPath()
        {
            return Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..", "Recipe"));
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

        private bool HasDuplicatePpid(string ppid, string targetFilePath, bool ignoreCurrentRecipePath, out string duplicatePath)
        {
            duplicatePath = null;
            if (string.IsNullOrWhiteSpace(ppid)) return false;

            List<string> searchFolders = new List<string>();
            searchFolders.Add(Path.Combine(GetDefaultRecipeRootPath(), "PM A"));
            searchFolders.Add(Path.Combine(GetDefaultRecipeRootPath(), "PM B"));
            searchFolders.Add(Path.Combine(GetDefaultRecipeRootPath(), "PM C"));

            string targetFolder = Path.GetDirectoryName(targetFilePath);
            if (!string.IsNullOrWhiteSpace(targetFolder) &&
                !searchFolders.Any(folder => PathsEqual(folder, targetFolder)))
            {
                searchFolders.Add(targetFolder);
            }

            foreach (string folder in searchFolders)
            {
                if (!Directory.Exists(folder)) continue;

                foreach (string filePath in Directory.GetFiles(folder, "*.json"))
                {
                    if (PathsEqual(filePath, targetFilePath)) continue;
                    if (ignoreCurrentRecipePath &&
                        !string.IsNullOrWhiteSpace(currentRecipePath) &&
                        PathsEqual(filePath, currentRecipePath))
                    {
                        continue;
                    }

                    string savedPpid = ReadPpidFromRecipeFile(filePath);
                    if (string.Equals(savedPpid, ppid, StringComparison.OrdinalIgnoreCase))
                    {
                        duplicatePath = filePath;
                        return true;
                    }
                }
            }

            return false;
        }

        private string ReadPpidFromRecipeFile(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                RecipeData recipe = jsonSerializer.Deserialize<RecipeData>(json);
                if (recipe == null) return string.Empty;
                if (!string.IsNullOrWhiteSpace(recipe.PPID)) return recipe.PPID.Trim();
                if (recipe.Fields != null && recipe.Fields.ContainsKey("PPID")) return recipe.Fields["PPID"].Trim();
            }
            catch
            {
                return string.Empty;
            }

            return string.Empty;
        }

        private class RecipeData
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

        private class FieldDefinition
        {
            public FieldDefinition(string key, string label, RecipeValueType valueType)
                : this(key, label, valueType, null, false)
            {
            }

            public FieldDefinition(string key, string label, RecipeValueType valueType, string[] options)
                : this(key, label, valueType, options, false)
            {
            }

            public FieldDefinition(string key, string label, RecipeValueType valueType, string[] options, bool isReadOnly)
            {
                Key = key;
                Label = label;
                ValueType = valueType;
                Options = options;
                IsReadOnly = isReadOnly;
            }

            public string Key { get; private set; }
            public string Label { get; private set; }
            public RecipeValueType ValueType { get; private set; }
            public string[] Options { get; private set; }
            public bool IsReadOnly { get; private set; }
        }

        private enum RecipeValueType
        {
            Text,
            Int,
            Double
        }

        private void btn_ProcessRecipe_Click(object sender, EventArgs e)
        {
            ShowProcessRecipeView();
        }

        private void ShowProcessRecipeView()
        {
            if (processRecipeGUI == null)
            {
                processRecipeGUI = new ProcessRecipe();
            }

            isProcessRecipeMode = true;
            RecipeGUI_pnl.Controls.Clear();
            processRecipeGUI.Dock = DockStyle.Fill;
            RecipeGUI_pnl.Controls.Add(processRecipeGUI);
            ApplyPmButtonStyle();
        }

        private void ShowChamberRecipeView()
        {
            if (!isProcessRecipeMode && RecipeGUI_pnl.Controls.Contains(listView_Recipe)) return;

            isProcessRecipeMode = false;
            RecipeGUI_pnl.Controls.Clear();
            RecipeGUI_pnl.Controls.Add(tableLayoutPanel2);
            RecipeGUI_pnl.Controls.Add(listView_Recipe);
            RecipeGUI_pnl.Controls.Add(tableLayoutPanel1);
            ApplyPmButtonStyle();

        }
    }
}
