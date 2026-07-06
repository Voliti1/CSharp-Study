using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class SettingGUI : UserControl
    {
        private MainGUI main;
        private TextBox txtbox_DefaultRecipeSavePath;

        public SettingGUI()
        {
            InitializeComponent();
            InitializeSettingScreen();
        }

        public SettingGUI(MainGUI mainGUI)
            : this()
        {
            this.main = mainGUI;
            LoadSettingsToControls(EquipmentSettingsService.Current);
        }

        private void InitializeSettingScreen()
        {
            btn_ParameterSet.Click += btn_ParameterSet_Click;
            btn_OpenRecipeFolder.Click += btn_OpenRecipeFolder_Click;

            AddRecipePathTextBox();
            InitializeComboBox(cbox_ShowDebugLog, new[] { "True", "False" });
            InitializeComboBox(cbox_DoorOpenInterlock, new[] { "True", "False" });
            InitializeComboBox(cbox_AlarmAutoStop, new[] { "True", "False" });
            InitializeComboBox(cbox_AutoStopAlarmLevel, new[] { "WARN", "ERROR", "FATAL" });
            InitializeComboBox(cbox_ModeChangeForceStop, new[] { "True", "False" });
            InitializeComboBox(cbox_IdleLampStatus, new[] { "Off", "Red", "Yellow", "Green" });
            InitializeComboBox(cbox_RunLampStatus, new[] { "Off", "Red", "Yellow", "Green" });
            InitializeComboBox(cbox_AlarmLampStatus, new[] { "Off", "Red", "Yellow", "Green" });
            InitializeComboBox(cbox_MaintenanceLampStatus, new[] { "Off", "Red", "Yellow", "Green" });

            nUpDown_TimeOut.Minimum = 1000;
            LoadSettingsToControls(EquipmentSettingsService.Current);
        }

        private void AddRecipePathTextBox()
        {
            if (txtbox_DefaultRecipeSavePath != null) return;

            txtbox_DefaultRecipeSavePath = new TextBox();
            txtbox_DefaultRecipeSavePath.Dock = DockStyle.Fill;
            txtbox_DefaultRecipeSavePath.Font = SystemFonts.MessageBoxFont;
            txtbox_DefaultRecipeSavePath.ReadOnly = true;
            txtbox_DefaultRecipeSavePath.BackColor = Color.White;
            txtbox_DefaultRecipeSavePath.Cursor = Cursors.Hand;
            txtbox_DefaultRecipeSavePath.TextAlign = HorizontalAlignment.Center;
            txtbox_DefaultRecipeSavePath.Click += RecipePathTextBox_Click;
            tableLayoutPanel1.Controls.Add(txtbox_DefaultRecipeSavePath, 1, 0);
        }

        private void InitializeComboBox(ComboBox comboBox, string[] values)
        {
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Items.Clear();
            comboBox.Items.AddRange(values);
        }

        private void LoadSettingsToControls(EquipmentSettings settings)
        {
            if (settings == null) settings = EquipmentSettingsService.Current;

            nUpDown_ReadCycle.Value = ClampDecimal(settings.EtherCatReadCycleMs, nUpDown_ReadCycle.Minimum, nUpDown_ReadCycle.Maximum);
            nUpDown_RetryCount.Value = ClampDecimal(settings.ReconnectRetryCount, nUpDown_RetryCount.Minimum, nUpDown_RetryCount.Maximum);
            nUpDown_TimeOut.Value = ClampDecimal(settings.ConnectionTimeoutMs, nUpDown_TimeOut.Minimum, nUpDown_TimeOut.Maximum);
            nUpDown_LogRetentionPeriod.Value = ClampDecimal(settings.LogRetentionDays, nUpDown_LogRetentionPeriod.Minimum, nUpDown_LogRetentionPeriod.Maximum);
            nUpDown_MaxDisplayLogCount.Value = ClampDecimal(settings.MaxDisplayLogCount, nUpDown_MaxDisplayLogCount.Minimum, nUpDown_MaxDisplayLogCount.Maximum);

            txtbox_DefaultRecipeSavePath.Text = settings.DefaultRecipeSavePath;
            SetComboBoxValue(cbox_ShowDebugLog, settings.ShowDebugLog ? "True" : "False");
            SetComboBoxValue(cbox_DoorOpenInterlock, settings.DoorOpenInterlock ? "True" : "False");
            SetComboBoxValue(cbox_AlarmAutoStop, settings.AlarmAutoStop ? "True" : "False");
            SetComboBoxValue(cbox_AutoStopAlarmLevel, settings.AutoStopAlarmLevel);
            SetComboBoxValue(cbox_ModeChangeForceStop, settings.ModeChangeForceStop ? "True" : "False");
            SetComboBoxValue(cbox_IdleLampStatus, settings.IdleLampStatus);
            SetComboBoxValue(cbox_RunLampStatus, settings.RunLampStatus);
            SetComboBoxValue(cbox_AlarmLampStatus, settings.AlarmLampStatus);
            SetComboBoxValue(cbox_MaintenanceLampStatus, settings.MaintenanceLampStatus);
        }

        private decimal ClampDecimal(int value, decimal minimum, decimal maximum)
        {
            if (value < minimum) return minimum;
            if (value > maximum) return maximum;
            return value;
        }

        private void SetComboBoxValue(ComboBox comboBox, string value)
        {
            if (comboBox.Items.Contains(value))
            {
                comboBox.SelectedItem = value;
                return;
            }

            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private void btn_ParameterSet_Click(object sender, EventArgs e)
        {
            ApplyRobotAxisConfig();
        }

        internal void ApplyRobotAxisConfig()
        {
            if (main == null) return;

            main.SetRobotAxisConfig(
                Convert.ToInt64(nUpDown_Accel.Value),
                Convert.ToInt64(nUpDown_Decel.Value),
                Convert.ToInt64(nUpDown_MaxVelo.Value),
                Convert.ToInt64(nUpDown_Velo.Value));
        }

        private void RecipePathTextBox_Click(object sender, EventArgs e)
        {
            SelectRecipePath();
        }

        private void btn_OpenRecipeFolder_Click(object sender, EventArgs e)
        {
            OpenCurrentRecipeFolder();
        }

        private void SelectRecipePath()
        {
            string initialPath = Directory.Exists(txtbox_DefaultRecipeSavePath.Text)
                ? txtbox_DefaultRecipeSavePath.Text
                : AppDataPaths.RecipeRootPath;

            string selectedPath;
            if (!VistaFolderPicker.TryPickFolder(this.Handle, "Select Default Recipe Save Path", initialPath, out selectedPath)) return;

            txtbox_DefaultRecipeSavePath.Text = selectedPath;
        }

        private string ResolveSelectedFolder(string selectedPath)
        {
            if (Directory.Exists(selectedPath))
            {
                return Path.GetFullPath(selectedPath);
            }

            string folderPath = Path.GetDirectoryName(selectedPath);
            if (!string.IsNullOrWhiteSpace(folderPath) && Directory.Exists(folderPath))
            {
                return Path.GetFullPath(folderPath);
            }

            MessageBox.Show("선택한 Recipe 경로를 찾을 수 없습니다.", "Recipe Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return string.Empty;
        }

        private void OpenCurrentRecipeFolder()
        {
            string folderPath = txtbox_DefaultRecipeSavePath.Text;
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                folderPath = AppDataPaths.RecipeRootPath;
            }

            Directory.CreateDirectory(folderPath);
            Process.Start("explorer.exe", folderPath);
        }

        private void SettingGUI_Load(object sender, EventArgs e)
        {
            LoadSettingsToControls(EquipmentSettingsService.Current);
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            EquipmentSettings settings = BuildSettingsFromControls();
            EquipmentSettingsService.Save(settings);
            AppDataPaths.EnsureBaseFolders();
            main?.ApplyEquipmentSettings(settings);
            MessageBox.Show("Settings have been applied.", "Setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private EquipmentSettings BuildSettingsFromControls()
        {
            EquipmentSettings settings = new EquipmentSettings();
            settings.EtherCatReadCycleMs = Convert.ToInt32(nUpDown_ReadCycle.Value);
            settings.ReconnectRetryCount = Convert.ToInt32(nUpDown_RetryCount.Value);
            settings.ConnectionTimeoutMs = Convert.ToInt32(nUpDown_TimeOut.Value);
            settings.LogRetentionDays = Convert.ToInt32(nUpDown_LogRetentionPeriod.Value);
            settings.MaxDisplayLogCount = Convert.ToInt32(nUpDown_MaxDisplayLogCount.Value);
            settings.ShowDebugLog = ParseBool(cbox_ShowDebugLog.Text);
            settings.DefaultRecipeSavePath = txtbox_DefaultRecipeSavePath.Text;
            settings.DoorOpenInterlock = ParseBool(cbox_DoorOpenInterlock.Text);
            settings.AlarmAutoStop = ParseBool(cbox_AlarmAutoStop.Text);
            settings.AutoStopAlarmLevel = cbox_AutoStopAlarmLevel.Text;
            settings.ModeChangeForceStop = ParseBool(cbox_ModeChangeForceStop.Text);
            settings.IdleLampStatus = cbox_IdleLampStatus.Text;
            settings.RunLampStatus = cbox_RunLampStatus.Text;
            settings.AlarmLampStatus = cbox_AlarmLampStatus.Text;
            settings.MaintenanceLampStatus = cbox_MaintenanceLampStatus.Text;
            settings.Normalize();
            return settings;
        }

        private bool ParseBool(string value)
        {
            return string.Equals(value, "True", StringComparison.OrdinalIgnoreCase)
                || string.Equals(value, "Yes", StringComparison.OrdinalIgnoreCase);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            LoadSettingsToControls(EquipmentSettingsService.Current);
        }

        private static class VistaFolderPicker
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
