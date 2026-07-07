using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SCT_Form
{
    // PM A/B/C 선택 버튼과, 선택된 PM에 맞는 필드 목록(PM마다 공정 파라미터가 다름)으로
    // 오른쪽 입력 폼(tableLayoutPanel2)을 동적으로 구성하는 로직.
    public partial class RecipeGUI
    {
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
    }
}
