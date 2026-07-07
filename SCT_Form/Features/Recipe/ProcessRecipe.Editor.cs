using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCT_Form
{
    // 상세 정보 폼(Recipe Name/PPID/Total Process Time 등)과, Step 순서를 보여주는
    // Process Flow 그리드(Step/Module/Recipe PPID/Process Time 열) 구성. Step 추가/이동/삭제
    // 버튼도 여기서 처리한다. 각 Step의 Module 콤보박스가 바뀌면 그 PM 폴더에 저장된 Chamber
    // Recipe 목록(GetChamberRecipeSelections)에서 PPID 콤보박스를 다시 채운다.
    public partial class ProcessRecipe
    {
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

        private string NormalizeModule(string module)
        {
            if (string.Equals(module, "PM B", StringComparison.OrdinalIgnoreCase)) return "PM B";
            if (string.Equals(module, "PM C", StringComparison.OrdinalIgnoreCase)) return "PM C";
            return "PM A";
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
