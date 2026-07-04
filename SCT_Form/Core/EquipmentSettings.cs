using System;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace SCT_Form
{
    internal static class EquipmentSettingsService
    {
        private static readonly JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();
        private static EquipmentSettings current;

        public static EquipmentSettings Current
        {
            get
            {
                if (current == null)
                {
                    current = Load();
                }

                return current;
            }
        }

        public static void Save(EquipmentSettings settings)
        {
            current = settings ?? new EquipmentSettings();
            current.Normalize();

            string folderPath = Path.GetDirectoryName(AppDataPaths.SettingsFilePath);
            if (!string.IsNullOrWhiteSpace(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            File.WriteAllText(AppDataPaths.SettingsFilePath, JsonSerializer.Serialize(current), Encoding.UTF8);
        }

        public static EquipmentSettings Load()
        {
            try
            {
                if (!File.Exists(AppDataPaths.SettingsFilePath))
                {
                    return CreateDefault();
                }

                EquipmentSettings settings = JsonSerializer.Deserialize<EquipmentSettings>(
                    File.ReadAllText(AppDataPaths.SettingsFilePath, Encoding.UTF8));
                if (settings == null)
                {
                    settings = CreateDefault();
                }

                settings.Normalize();
                return settings;
            }
            catch
            {
                return CreateDefault();
            }
        }

        public static EquipmentSettings CreateDefault()
        {
            EquipmentSettings settings = new EquipmentSettings();
            settings.Normalize();
            return settings;
        }
    }

    internal class EquipmentSettings
    {
        public EquipmentSettings()
        {
            EtherCatReadCycleMs = 300;
            ReconnectRetryCount = 3;
            ConnectionTimeoutMs = 5000;
            LogRetentionDays = 30;
            MaxDisplayLogCount = 5000;
            ShowDebugLog = true;
            DefaultRecipeSavePath = string.Empty;
            DoorOpenInterlock = true;
            AlarmAutoStop = true;
            AutoStopAlarmLevel = "ERROR";
            ModeChangeForceStop = true;
            IdleLampStatus = "Yellow";
            RunLampStatus = "Green";
            AlarmLampStatus = "Red";
            MaintenanceLampStatus = "Yellow";
        }

        public int EtherCatReadCycleMs { get; set; }
        public int ReconnectRetryCount { get; set; }
        public int ConnectionTimeoutMs { get; set; }
        public int LogRetentionDays { get; set; }
        public int MaxDisplayLogCount { get; set; }
        public bool ShowDebugLog { get; set; }
        public string DefaultRecipeSavePath { get; set; }
        public bool DoorOpenInterlock { get; set; }
        public bool AlarmAutoStop { get; set; }
        public string AutoStopAlarmLevel { get; set; }
        public bool ModeChangeForceStop { get; set; }
        public string IdleLampStatus { get; set; }
        public string RunLampStatus { get; set; }
        public string AlarmLampStatus { get; set; }
        public string MaintenanceLampStatus { get; set; }

        public void Normalize()
        {
            EtherCatReadCycleMs = Clamp(EtherCatReadCycleMs, 100, 1000);
            ReconnectRetryCount = Clamp(ReconnectRetryCount, 0, 10);
            ConnectionTimeoutMs = Clamp(ConnectionTimeoutMs, 1000, 30000);
            LogRetentionDays = Clamp(LogRetentionDays, 7, 365);
            MaxDisplayLogCount = Clamp(MaxDisplayLogCount, 500, 50000);
            DefaultRecipeSavePath = NormalizePath(DefaultRecipeSavePath);
            AutoStopAlarmLevel = NormalizeLevel(AutoStopAlarmLevel);
            IdleLampStatus = NormalizeLampStatus(IdleLampStatus, "Yellow");
            RunLampStatus = NormalizeLampStatus(RunLampStatus, "Green");
            AlarmLampStatus = NormalizeLampStatus(AlarmLampStatus, "Red");
            MaintenanceLampStatus = NormalizeLampStatus(MaintenanceLampStatus, "Yellow");
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private static string NormalizePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return AppDataPaths.DefaultRecipeRootPath;
            }

            return Path.GetFullPath(path.Trim());
        }

        private static string NormalizeLevel(string level)
        {
            string upperLevel = string.IsNullOrWhiteSpace(level) ? "ERROR" : level.Trim().ToUpper();
            if (upperLevel == "WARN" || upperLevel == "ERROR" || upperLevel == "FATAL") return upperLevel;
            return "ERROR";
        }

        private static string NormalizeLampStatus(string value, string defaultValue)
        {
            string text = string.IsNullOrWhiteSpace(value) ? defaultValue : value.Trim();
            if (string.Equals(text, "Off", StringComparison.OrdinalIgnoreCase)) return "Off";
            if (string.Equals(text, "Red", StringComparison.OrdinalIgnoreCase)) return "Red";
            if (string.Equals(text, "Yellow", StringComparison.OrdinalIgnoreCase)) return "Yellow";
            if (string.Equals(text, "Green", StringComparison.OrdinalIgnoreCase)) return "Green";
            return defaultValue;
        }
    }
}
