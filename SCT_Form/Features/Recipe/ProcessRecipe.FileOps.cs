using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCT_Form
{
    // Process Recipe 목록 로드, 새로 만들기/저장/다른 이름으로 저장/삭제/폴더 열기,
    // 입력값으로부터 ProcessRecipeData 조립 및 검증, PPID 중복 체크.
    public partial class ProcessRecipe
    {
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

        // 로그인된 계정 ID(main.tBox_ID.Text)를 Created By에 쓴다. main이 없거나 로그인
        // 텍스트박스가 비어 있으면 Windows 로그인 계정(Environment.UserName)으로 대체한다.
        private string GetCurrentUserName()
        {
            if (main != null && main.tBox_ID != null && !string.IsNullOrWhiteSpace(main.tBox_ID.Text))
            {
                return main.tBox_ID.Text.Trim();
            }

            return Environment.UserName;
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
    }
}
