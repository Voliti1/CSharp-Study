using System.IO;
using System.Windows.Forms;

namespace SCT_Form
{
    internal static class AppDataPaths
    {
        public static string RootPath
        {
            get { return Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..", "data")); }
        }

        public static string RecipeRootPath
        {
            get { return EquipmentSettingsService.Current.DefaultRecipeSavePath; }
        }

        public static string DefaultRecipeRootPath
        {
            get { return Path.Combine(RootPath, "Recipe"); }
        }

        public static string AccountFilePath
        {
            get { return Path.Combine(RootPath, "Account.json"); }
        }

        public static string SettingsFilePath
        {
            get { return Path.Combine(RootPath, "Settings.json"); }
        }

        public static string LogFolderPath
        {
            get { return Path.Combine(RootPath, "Log"); }
        }

        public static void EnsureBaseFolders()
        {
            Directory.CreateDirectory(RootPath);
            Directory.CreateDirectory(RecipeRootPath);
            Directory.CreateDirectory(LogFolderPath);
        }
    }
}
