using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class ChamberRecipeSetting : Form
    {
        private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        private readonly Dictionary<string, TextBox> previewValues = new Dictionary<string, TextBox>();
        private string currentPm = "PM A";

        public ChamberRecipeSetting()
        {
            InitializeComponent();
            InitializeChamberRecipeSetting();
        }

        public ChamberRecipeSetting(string pmName)
        {
            InitializeComponent();
            InitializeChamberRecipeSetting();
            SelectPm(NormalizePmName(pmName));
        }

        public string SelectedPM { get; private set; }
        public string SelectedRecipePath { get; private set; }
        public string SelectedRecipeName { get; private set; }
        public string SelectedPPID { get; private set; }
        public ChamberRecipeSelection SelectedRecipeSelection { get; private set; }

        private void InitializeChamberRecipeSetting()
        {
            listView_ChamberRecipe.View = View.List;
            listView_ChamberRecipe.FullRowSelect = true;
            listView_ChamberRecipe.MultiSelect = false;
            listView_ChamberRecipe.SelectedIndexChanged += listView_ChamberRecipe_SelectedIndexChanged;
            listView_ChamberRecipe.DoubleClick += listView_ChamberRecipe_DoubleClick;

            btn_PMA.Click += btn_PMA_Click;
            btn_PMB.Click += btn_PMB_Click;
            btn_PMC.Click += btn_PMC_Click;
            btn_Select.Click += btn_Select_Click;
            button1.Click += button1_Click;

            BuildPreviewTable(null);
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
            currentPm = pmName;
            Text = "Chamber Recipe Setting - " + currentPm;
            ApplyPmButtonStyle();
            LoadRecipeList();
            BuildPreviewTable(null);
        }

        private void ApplyPmButtonStyle()
        {
            ApplyPmButtonStyle(btn_PMA, currentPm == "PM A");
            ApplyPmButtonStyle(btn_PMB, currentPm == "PM B");
            ApplyPmButtonStyle(btn_PMC, currentPm == "PM C");
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
            listView_ChamberRecipe.Items.Clear();

            string folderPath = GetCurrentPmFolderPath();
            if (!Directory.Exists(folderPath)) return;

            foreach (string filePath in Directory.GetFiles(folderPath, "*.json").OrderBy(Path.GetFileNameWithoutExtension))
            {
                RecipeData recipe = ReadRecipe(filePath);
                string recipeName = GetRecipeName(recipe, filePath);
                string ppid = GetPpid(recipe);
                string displayText = string.IsNullOrWhiteSpace(ppid) ? recipeName : ppid + " - " + recipeName;

                ListViewItem item = new ListViewItem(displayText);
                item.Tag = filePath;
                listView_ChamberRecipe.Items.Add(item);
            }
        }

        private void listView_ChamberRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_ChamberRecipe.SelectedItems.Count == 0)
            {
                BuildPreviewTable(null);
                return;
            }

            string filePath = listView_ChamberRecipe.SelectedItems[0].Tag as string;
            BuildPreviewTable(ReadRecipe(filePath));
        }

        private void listView_ChamberRecipe_DoubleClick(object sender, EventArgs e)
        {
            ApplySelectedRecipe();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            ApplySelectedRecipe();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ApplySelectedRecipe()
        {
            if (listView_ChamberRecipe.SelectedItems.Count == 0)
            {
                MessageBox.Show("선택할 Recipe를 고르세요.", "Chamber Recipe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string filePath = listView_ChamberRecipe.SelectedItems[0].Tag as string;
            RecipeData recipe = ReadRecipe(filePath);
            if (recipe == null)
            {
                MessageBox.Show("Recipe 파일을 읽을 수 없습니다.", "Chamber Recipe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedPM = currentPm;
            SelectedRecipePath = filePath;
            SelectedRecipeName = GetRecipeName(recipe, filePath);
            SelectedPPID = GetPpid(recipe);
            SelectedRecipeSelection = new ChamberRecipeSelection();
            SelectedRecipeSelection.Module = SelectedPM;
            SelectedRecipeSelection.RecipePath = SelectedRecipePath;
            SelectedRecipeSelection.RecipeName = SelectedRecipeName;
            SelectedRecipeSelection.RecipePPID = SelectedPPID;
            SelectedRecipeSelection.ProcessTime = GetProcessTime(recipe);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BuildPreviewTable(RecipeData recipe)
        {
            List<FieldDefinition> fields = GetPreviewFields();

            previewValues.Clear();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel2.Controls.Clear();
            tableLayoutPanel2.RowStyles.Clear();
            tableLayoutPanel2.ColumnStyles.Clear();
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.RowCount = fields.Count;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));

            for (int row = 0; row < fields.Count; row++)
            {
                FieldDefinition field = fields[row];
                tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / fields.Count));

                Label label = new Label();
                label.Dock = DockStyle.Fill;
                label.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
                label.Margin = new Padding(0, 0, 8, 0);
                label.Text = field.Label;
                label.TextAlign = ContentAlignment.MiddleRight;

                TextBox valueBox = new TextBox();
                valueBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                valueBox.AutoSize = false;
                valueBox.Font = new Font("맑은 고딕", 10F);
                valueBox.Height = 26;
                valueBox.ReadOnly = true;
                valueBox.Text = GetRecipeValue(recipe, field.Key);

                tableLayoutPanel2.Controls.Add(label, 0, row);
                tableLayoutPanel2.Controls.Add(valueBox, 1, row);
                previewValues[field.Key] = valueBox;
            }

            tableLayoutPanel2.ResumeLayout();
        }

        private List<FieldDefinition> GetPreviewFields()
        {
            List<FieldDefinition> fields = new List<FieldDefinition>();
            fields.Add(new FieldDefinition("Recipe Name", "Recipe Name"));
            fields.Add(new FieldDefinition("PPID", "PPID"));
            fields.Add(new FieldDefinition("Description", "Description"));
            fields.Add(new FieldDefinition("Process Time", "Process Time"));

            if (currentPm == "PM A")
            {
                fields.Add(new FieldDefinition("Temperature", "Temperature"));
                fields.Add(new FieldDefinition("Pressure", "Pressure"));
                fields.Add(new FieldDefinition("Gas Flow", "Gas Flow"));
                fields.Add(new FieldDefinition("RF Power", "RF Power"));
            }
            else if (currentPm == "PM B")
            {
                fields.Add(new FieldDefinition("Platen Speed", "Platen Speed"));
                fields.Add(new FieldDefinition("Carrier Speed", "Carrier Speed"));
                fields.Add(new FieldDefinition("Down Force", "Down Force"));
                fields.Add(new FieldDefinition("Slurry Flow", "Slurry Flow"));
                fields.Add(new FieldDefinition("Pad Type", "Pad Type"));
            }
            else
            {
                fields.Add(new FieldDefinition("Cleaning Mode", "Cleaning Mode"));
                fields.Add(new FieldDefinition("DI Water Flow", "DI Water Flow"));
                fields.Add(new FieldDefinition("Chemical Flow", "Chemical Flow"));
                fields.Add(new FieldDefinition("Spin Speed", "Spin Speed"));
                fields.Add(new FieldDefinition("Dry Time", "Dry Time"));
            }

            fields.Add(new FieldDefinition("Created By", "Created By"));
            fields.Add(new FieldDefinition("Modified Date", "Modified Date"));
            return fields;
        }

        private string GetRecipeValue(RecipeData recipe, string key)
        {
            if (recipe == null) return string.Empty;

            if (recipe.Fields != null && recipe.Fields.ContainsKey(key)) return recipe.Fields[key];
            if (key == "Recipe Name") return recipe.RecipeName;
            if (key == "PPID") return recipe.PPID;
            if (key == "Description") return recipe.Description;
            if (key == "Created By") return recipe.CreatedBy;
            if (key == "Modified Date") return recipe.ModifiedDate;
            return string.Empty;
        }

        private RecipeData ReadRecipe(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return null;
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                return jsonSerializer.Deserialize<RecipeData>(json);
            }
            catch
            {
                return null;
            }
        }

        private string GetRecipeName(RecipeData recipe, string filePath)
        {
            if (recipe != null && !string.IsNullOrWhiteSpace(recipe.RecipeName)) return recipe.RecipeName;
            return Path.GetFileNameWithoutExtension(filePath);
        }

        private string GetPpid(RecipeData recipe)
        {
            if (recipe == null) return string.Empty;
            if (!string.IsNullOrWhiteSpace(recipe.PPID)) return recipe.PPID;
            if (recipe.Fields != null && recipe.Fields.ContainsKey("PPID")) return recipe.Fields["PPID"];
            return string.Empty;
        }

        private int GetProcessTime(RecipeData recipe)
        {
            if (recipe == null || recipe.Fields == null || !recipe.Fields.ContainsKey("Process Time")) return 0;

            int processTime;
            return int.TryParse(recipe.Fields["Process Time"], out processTime) ? processTime : 0;
        }

        private string GetCurrentPmFolderPath()
        {
            return Path.Combine(GetDefaultRecipeRootPath(), currentPm);
        }

        private string GetDefaultRecipeRootPath()
        {
            return Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..", "Recipe"));
        }

        private string NormalizePmName(string pmName)
        {
            if (string.Equals(pmName, "PM B", StringComparison.OrdinalIgnoreCase)) return "PM B";
            if (string.Equals(pmName, "PM C", StringComparison.OrdinalIgnoreCase)) return "PM C";
            return "PM A";
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
            public FieldDefinition(string key, string label)
            {
                Key = key;
                Label = label;
            }

            public string Key { get; private set; }
            public string Label { get; private set; }
        }
    }
}
