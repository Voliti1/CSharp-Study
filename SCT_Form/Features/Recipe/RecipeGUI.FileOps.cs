using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SCT_Form
{
    // Recipe 목록 로드, 새로 만들기/저장/다른 이름으로 저장/삭제/폴더 열기, PPID 중복 체크,
    // 로그인 계정(Created By) 채우기 등 Recipe 파일 자체를 다루는 로직.
    public partial class RecipeGUI
    {
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

            string selectedPath;
            if (ModernFolderPicker.TryPickFolder(Handle, "Recipe save folder", GetCurrentPmFolderPath(), out selectedPath))
            {
                SaveRecipe(selectedPath, false);
            }
            /*
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
            */
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
    }
}
