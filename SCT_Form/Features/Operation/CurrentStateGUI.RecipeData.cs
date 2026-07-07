using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCT_Form
{
    // Process Recipe 콤보박스 채우기, Recipe JSON 파일 읽기/검증, 관련 DTO(RecipeData,
    // ProcessRecipeData 등) 정의. btn_Start_Click이 여기서 읽은 ProcessRecipeData를
    // AutoSequenceBuilder.Build()에 넘겨서 실제 자동 시퀀스를 만든다.
    public partial class CurrentStateGUI
    {
        private void InitializeProcessRecipeSelector()
        {
            cbox_ProcessRecipe.DropDownStyle = ComboBoxStyle.DropDownList;
            cbox_ProcessRecipe.SelectedIndexChanged += cbox_ProcessRecipe_SelectedIndexChanged;
            cbox_ProcessRecipe.DropDown += cbox_ProcessRecipe_DropDown;
            LoadProcessRecipeSelector();
        }

        private void HidePmRecipeSettingButtons()
        {
            HidePmRecipeSettingButton(btn_PMA_Setting);
            HidePmRecipeSettingButton(btn_PMB_Setting);
            HidePmRecipeSettingButton(btn_PMC_Setting);
        }

        private void HidePmRecipeSettingButton(Button button)
        {
            button.Visible = false;
            button.Enabled = false;
            button.TabStop = false;
        }

        private void cbox_ProcessRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessRecipeComboItem item = cbox_ProcessRecipe.SelectedItem as ProcessRecipeComboItem;
            selectedProcessRecipePath = item == null ? null : item.FilePath;
        }

        private void cbox_ProcessRecipe_DropDown(object sender, EventArgs e)
        {
            LoadProcessRecipeSelector();
        }

        private void LoadProcessRecipeSelector()
        {
            string previouslySelectedPath = selectedProcessRecipePath;
            cbox_ProcessRecipe.Items.Clear();

            EnsureProcessRecipeFolder();

            foreach (string filePath in Directory.GetFiles(GetDefaultProcessRecipeFolderPath(), "*.json").OrderBy(Path.GetFileNameWithoutExtension))
            {
                cbox_ProcessRecipe.Items.Add(new ProcessRecipeComboItem(filePath));
            }

            if (cbox_ProcessRecipe.Items.Count == 0)
            {
                selectedProcessRecipePath = null;
                return;
            }

            int selectedIndex = 0;
            for (int i = 0; i < cbox_ProcessRecipe.Items.Count; i++)
            {
                ProcessRecipeComboItem item = cbox_ProcessRecipe.Items[i] as ProcessRecipeComboItem;
                if (item != null && PathsEqual(item.FilePath, previouslySelectedPath))
                {
                    selectedIndex = i;
                    break;
                }
            }

            cbox_ProcessRecipe.SelectedIndex = selectedIndex;
        }

        private bool CanOperateEquipment()
        {
            return main == null || main.EnsureEquipmentOperationAllowed();
        }

        private int GetProcessTime(RecipeData recipe)
        {
            int processTime;
            string value = GetRecipeFieldValue(recipe, "Process Time");
            if (int.TryParse(value, out processTime) && processTime > 0)
            {
                return processTime;
            }

            return 3;
        }

        private string GetRecipeFieldValue(RecipeData recipe, string key)
        {
            if (recipe == null || recipe.Fields == null || !recipe.Fields.ContainsKey(key)) return string.Empty;
            return recipe.Fields[key];
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

        private ProcessRecipeData ReadProcessRecipe(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return null;
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                return jsonSerializer.Deserialize<ProcessRecipeData>(json);
            }
            catch
            {
                return null;
            }
        }

        private bool ValidateProcessRecipe(ProcessRecipeData recipe, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (recipe == null)
            {
                validationMessage = "Process Recipe file could not be loaded.";
                return false;
            }

            if (recipe.Steps == null || recipe.Steps.Count == 0)
            {
                validationMessage = "Process Recipe has no steps.";
                return false;
            }

            foreach (ProcessRecipeStep step in recipe.Steps)
            {
                if (step == null || step.Recipe == null)
                {
                    validationMessage = "Process Recipe has an empty step.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(step.Recipe.Module) ||
                    string.IsNullOrWhiteSpace(step.Recipe.RecipePPID))
                {
                    validationMessage = "Process Recipe step is missing Module or Recipe PPID.";
                    return false;
                }

                if (step.Recipe.ProcessTime <= 0)
                {
                    validationMessage = "Process Recipe step has invalid Process Time.";
                    return false;
                }
            }

            return true;
        }

        private void EnsureProcessRecipeFolder()
        {
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

        private bool PathsEqual(string firstPath, string secondPath)
        {
            if (string.IsNullOrWhiteSpace(firstPath) || string.IsNullOrWhiteSpace(secondPath)) return false;
            return string.Equals(
                Path.GetFullPath(firstPath).TrimEnd(Path.DirectorySeparatorChar),
                Path.GetFullPath(secondPath).TrimEnd(Path.DirectorySeparatorChar),
                StringComparison.OrdinalIgnoreCase);
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

        private class ProcessRecipeComboItem
        {
            public ProcessRecipeComboItem(string filePath)
            {
                FilePath = filePath;
            }

            public string FilePath { get; private set; }

            public override string ToString()
            {
                return Path.GetFileNameWithoutExtension(FilePath);
            }
        }

        private class ProcessRecipeData
        {
            public string RecipeName { get; set; }
            public string PPID { get; set; }
            public string Description { get; set; }
            public int TotalProcessTime { get; set; }
            public string CreatedBy { get; set; }
            public string ModifiedDate { get; set; }
            public List<ProcessRecipeStep> Steps { get; set; }
        }

        private class ProcessRecipeStep
        {
            public int StepNo { get; set; }
            public ChamberRecipeSelection Recipe { get; set; }
        }
    }
}
