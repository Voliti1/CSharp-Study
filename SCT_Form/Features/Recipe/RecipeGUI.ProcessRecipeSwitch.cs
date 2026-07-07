using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SCT_Form
{
    // "Process Recipe" 버튼으로 Chamber Recipe 화면(PM별 개별 레시피)과 Process Recipe
    // 화면(여러 PM을 순서대로 묶은 경로)을 전환하는 로직. ModernFolderPicker는 구식
    // FolderBrowserDialog 대신 Windows Vista 이후 스타일 폴더 선택 대화상자를
    // IFileOpenDialog COM 인터페이스로 직접 호출하는 정적 헬퍼(Recipe 다른 이름으로 저장에 사용).
    public partial class RecipeGUI
    {
        private void btn_ProcessRecipe_Click(object sender, EventArgs e)
        {
            ShowProcessRecipeView();
        }

        private void ShowProcessRecipeView()
        {
            if (processRecipeGUI == null)
            {
                processRecipeGUI = new ProcessRecipe(main);
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

        private static class ModernFolderPicker
        {
            private const int S_OK = 0;
            private const int ERROR_CANCELLED = unchecked((int)0x800704C7);

            public static bool TryPickFolder(IntPtr ownerHandle, string title, string initialPath, out string selectedPath)
            {
                selectedPath = string.Empty;
                IFileOpenDialog dialog = null;
                IShellItem initialFolder = null;
                IShellItem result = null;

                try
                {
                    dialog = (IFileOpenDialog)new FileOpenDialog();
                    dialog.SetOptions(FOS.PickFolders | FOS.ForceFileSystem | FOS.PathMustExist | FOS.NoChangeDir);
                    dialog.SetTitle(title);
                    dialog.SetOkButtonLabel("Select");

                    if (Directory.Exists(initialPath) &&
                        SHCreateItemFromParsingName(initialPath, IntPtr.Zero, typeof(IShellItem).GUID, out initialFolder) == S_OK)
                    {
                        dialog.SetFolder(initialFolder);
                    }

                    int resultCode = dialog.Show(ownerHandle);
                    if (resultCode == ERROR_CANCELLED) return false;
                    if (resultCode != S_OK) Marshal.ThrowExceptionForHR(resultCode);

                    dialog.GetResult(out result);
                    result.GetDisplayName(SIGDN.FileSysPath, out selectedPath);
                    return !string.IsNullOrWhiteSpace(selectedPath);
                }
                finally
                {
                    if (result != null) Marshal.ReleaseComObject(result);
                    if (initialFolder != null) Marshal.ReleaseComObject(initialFolder);
                    if (dialog != null) Marshal.ReleaseComObject(dialog);
                }
            }

            [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = true)]
            private static extern int SHCreateItemFromParsingName(
                [MarshalAs(UnmanagedType.LPWStr)] string path,
                IntPtr bindingContext,
                [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
                [MarshalAs(UnmanagedType.Interface)] out IShellItem shellItem);

            [ComImport]
            [Guid("DC1C5A9C-E88A-4DDE-A5A1-60F82A20AEF7")]
            private class FileOpenDialog
            {
            }

            [ComImport]
            [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            private interface IShellItem
            {
                void BindToHandler(IntPtr bindingContext, [MarshalAs(UnmanagedType.LPStruct)] Guid bhid, [MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr ppv);
                void GetParent(out IShellItem parent);
                void GetDisplayName(SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
                void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);
                void Compare(IShellItem psi, uint hint, out int order);
            }

            [ComImport]
            [Guid("D57C7288-D4AD-4768-BE02-9D969532D960")]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            private interface IFileOpenDialog
            {
                [PreserveSig]
                int Show(IntPtr parent);
                void SetFileTypes(uint cFileTypes, IntPtr rgFilterSpec);
                void SetFileTypeIndex(uint iFileType);
                void GetFileTypeIndex(out uint piFileType);
                void Advise(IntPtr pfde, out uint pdwCookie);
                void Unadvise(uint dwCookie);
                void SetOptions(FOS fos);
                void GetOptions(out FOS pfos);
                void SetDefaultFolder(IShellItem psi);
                void SetFolder(IShellItem psi);
                void GetFolder(out IShellItem ppsi);
                void GetCurrentSelection(out IShellItem ppsi);
                void SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);
                void GetFileName([MarshalAs(UnmanagedType.LPWStr)] out string pszName);
                void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);
                void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);
                void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);
                void GetResult(out IShellItem ppsi);
                void AddPlace(IShellItem psi, FDAP fdap);
                void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);
                void Close(int hr);
                void SetClientGuid([MarshalAs(UnmanagedType.LPStruct)] Guid guid);
                void ClearClientData();
                void SetFilter(IntPtr pFilter);
                void GetResults(out IntPtr ppenum);
                void GetSelectedItems(out IntPtr ppsai);
            }

            [Flags]
            private enum FOS : uint
            {
                NoChangeDir = 0x00000008,
                PickFolders = 0x00000020,
                ForceFileSystem = 0x00000040,
                PathMustExist = 0x00000800
            }

            private enum FDAP
            {
                Bottom = 0,
                Top = 1
            }

            private enum SIGDN : uint
            {
                FileSysPath = 0x80058000
            }
        }
    }
}
