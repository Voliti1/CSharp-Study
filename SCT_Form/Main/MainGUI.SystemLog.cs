using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SCT_Form
{
    // 화면(LogGUI)에 보여줄 시스템 로그를 메모리에 쌓고, 동시에 log4net으로 파일에도 남긴다.
    // 앱 시작 시 최근 로그 파일들을 다시 읽어 메모리 목록에 채워 넣는 것도 이 파일이 담당한다.
    public partial class MainGUI
    {
        internal List<SystemLogEntry> GetSystemLogSnapshot()
        {
            lock (systemLogs)
            {
                return systemLogs.Select(item => item.Clone()).ToList();
            }
        }

        internal bool ShowDebugLog
        {
            get { return settings == null || settings.ShowDebugLog; }
        }

        internal int MaxDisplayLogCount
        {
            get { return settings == null ? 5000 : settings.MaxDisplayLogCount; }
        }

        internal void ApplyEquipmentSettings(EquipmentSettings newSettings)
        {
            settings = newSettings ?? EquipmentSettingsService.Current;
            TrimSystemLogs();

            if (isConnect && EtherCAT_M != null)
            {
                EtherCAT_M.ReadData_Send_Start(settings.EtherCatReadCycleMs);
                WriteSystemLog("Communication", "INFO", "EtherCAT Read Cycle 적용: " + settings.EtherCatReadCycleMs + " ms");
            }

            if (logGUI != null && !logGUI.IsDisposed)
            {
                logGUI.RefreshLogs(true);
            }
        }

        internal void DeleteSystemLogs(IEnumerable<long> logIds)
        {
            HashSet<long> targetIds = new HashSet<long>(logIds);
            if (targetIds.Count == 0) return;

            lock (systemLogs)
            {
                systemLogs.RemoveAll(item => targetIds.Contains(item.Id));
            }

            if (logGUI != null && !logGUI.IsDisposed)
            {
                logGUI.RefreshLogs(true);
            }
        }

        private void LoadSystemLogsFromFiles()
        {
            if (!Directory.Exists(AppDataPaths.LogFolderPath)) return;

            CleanupExpiredLogFiles();
            DateTime minimumLogTime = DateTime.Now.Date.AddDays(-EquipmentSettingsService.Current.LogRetentionDays);

            foreach (string filePath in Directory.GetFiles(AppDataPaths.LogFolderPath, "Log_*.log").OrderBy(item => item))
            {
                try
                {
                    foreach (string line in ReadLogFileLines(filePath))
                    {
                        SystemLogEntry entry;
                        if (TryParseLogFileLine(line, out entry) && entry.Time >= minimumLogTime)
                        {
                            AddSystemLogEntry(entry, true);
                        }
                    }
                }
                catch
                {
                    // 로그 조회용 파일 로드 실패는 장비 시작을 막지 않는다.
                }
            }
        }

        // 로그 파일은 UTF-8로 기록되지만(App.config appender encoding), 과거 빌드(ANSI/CP949)가
        // 기록한 줄이 한 파일 안에 섞여 있을 수 있다. 줄 단위로 UTF-8 해석을 시도하고
        // 실패한 줄만 CP949로 재해석해서, 혼합 인코딩 파일도 전부 정상 표시되게 한다.
        private static List<string> ReadLogFileLines(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            UTF8Encoding utf8Strict = new UTF8Encoding(false, true);
            Encoding cp949 = Encoding.GetEncoding(949);
            List<string> lines = new List<string>();

            int lineStart = 0;
            for (int i = 0; i <= bytes.Length; i++)
            {
                if (i != bytes.Length && bytes[i] != (byte)'\n') continue;

                int lineLength = i - lineStart;
                if (lineLength > 0 && bytes[lineStart + lineLength - 1] == (byte)'\r') lineLength--;

                if (lineLength > 0)
                {
                    // UTF-8 BOM(EF BB BF) 제거
                    if (lineLength >= 3 && bytes[lineStart] == 0xEF && bytes[lineStart + 1] == 0xBB && bytes[lineStart + 2] == 0xBF)
                    {
                        lineStart += 3;
                        lineLength -= 3;
                    }

                    string line;
                    try
                    {
                        line = utf8Strict.GetString(bytes, lineStart, lineLength);
                    }
                    catch (DecoderFallbackException)
                    {
                        line = cp949.GetString(bytes, lineStart, lineLength);
                    }

                    lines.Add(line);
                }
                else
                {
                    lines.Add(string.Empty);
                }

                lineStart = i + 1;
            }

            return lines;
        }

        private void CleanupExpiredLogFiles()
        {
            DateTime minimumLogDate = DateTime.Now.Date.AddDays(-EquipmentSettingsService.Current.LogRetentionDays);
            foreach (string filePath in Directory.GetFiles(AppDataPaths.LogFolderPath, "Log_*.log"))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                if (string.IsNullOrWhiteSpace(fileName) || !fileName.StartsWith("Log_")) continue;

                DateTime logDate;
                if (!DateTime.TryParseExact(fileName.Substring(4), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out logDate)) continue;
                if (logDate >= minimumLogDate) continue;

                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                    // 실행 중 잠긴 로그 파일은 다음 기동 때 다시 정리한다.
                }
            }
        }

        private bool TryParseLogFileLine(string line, out SystemLogEntry entry)
        {
            entry = null;
            if (string.IsNullOrWhiteSpace(line) || line.Length < 26) return false;

            DateTime logTime;
            if (!DateTime.TryParseExact(
                line.Substring(0, 23),
                "yyyy-MM-dd HH:mm:ss,fff",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out logTime))
            {
                return false;
            }

            string rest = line.Substring(23).Trim();
            string level;
            string message;
            if (!TryParseLogBody(rest, out level, out message)) return false;

            entry = new SystemLogEntry
            {
                Time = logTime,
                Category = ResolveLogCategory(level, message),
                Level = level,
                Message = message
            };

            return true;
        }

        private bool TryParseLogBody(string text, out string level, out string message)
        {
            level = string.Empty;
            message = string.Empty;
            if (string.IsNullOrWhiteSpace(text) || text[0] != '[') return false;

            int closeBracketIndex = text.IndexOf(']');
            if (closeBracketIndex <= 1) return false;

            string bracketValue = text.Substring(1, closeBracketIndex - 1).Trim();
            string body = text.Substring(closeBracketIndex + 1).TrimStart();

            if (IsLogLevel(bracketValue))
            {
                level = bracketValue;
                message = body;
                return true;
            }

            int levelEndIndex = body.IndexOf(' ');
            if (levelEndIndex <= 0) return false;

            level = body.Substring(0, levelEndIndex).Trim().ToUpper();
            if (!IsLogLevel(level)) return false;

            string remainder = body.Substring(levelEndIndex).TrimStart();
            int messageSeparatorIndex = remainder.IndexOf(" - ");
            message = messageSeparatorIndex >= 0
                ? remainder.Substring(messageSeparatorIndex + 3)
                : remainder;
            return true;
        }

        private bool IsLogLevel(string value)
        {
            string upperValue = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToUpper();
            return upperValue == "DEBUG" || upperValue == "INFO" || upperValue == "WARN" || upperValue == "ERROR" || upperValue == "FATAL";
        }

        private void AddSystemLogEntry(SystemLogEntry entry, bool skipDuplicate)
        {
            if (entry == null) return;

            lock (systemLogs)
            {
                string key = GetSystemLogKey(entry);
                if (skipDuplicate && systemLogKeys.Contains(key)) return;

                entry.Id = ++nextLogId;
                systemLogs.Add(entry);
                systemLogKeys.Add(key);
                TrimSystemLogs();
            }
        }

        private void TrimSystemLogs()
        {
            int maxCount = settings == null ? MaxSystemLogCount : settings.MaxDisplayLogCount;
            if (systemLogs.Count <= maxCount) return;

            int removeCount = systemLogs.Count - maxCount;
            for (int index = 0; index < removeCount; index++)
            {
                systemLogKeys.Remove(GetSystemLogKey(systemLogs[index]));
            }

            systemLogs.RemoveRange(0, removeCount);
        }

        private string GetSystemLogKey(SystemLogEntry entry)
        {
            return entry.Time.ToString("yyyy-MM-dd HH:mm:ss,fff", CultureInfo.InvariantCulture)
                + "|" + entry.Level
                + "|" + entry.Category
                + "|" + entry.Message;
        }

        // 파일 로그(log4net) 저장과 화면 로그 목록 갱신을 동시에 수행하는 전용 메서드
        public void WriteSystemLog(string level, string message)
        {
            WriteSystemLog(ResolveLogCategory(level, message), level, message);
        }

        public void WriteSystemLog(string category, string level, string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => WriteSystemLog(category, level, message)));
                return;
            }

            string upperLevel = string.IsNullOrWhiteSpace(level) ? "INFO" : level.Trim().ToUpper();
            string logMessage = message ?? string.Empty;
            string logCategory = string.IsNullOrWhiteSpace(category) ? ResolveLogCategory(upperLevel, logMessage) : category.Trim();

            switch (upperLevel)
            {
                case "WARN":
                    log.Warn(logMessage);
                    break;
                case "ERROR":
                    log.Error(logMessage);
                    break;
                case "FATAL":
                    log.Fatal(logMessage);
                    break;
                default:
                    log.Info(logMessage);
                    break;
            }

            SystemLogEntry entry = new SystemLogEntry
            {
                Time = DateTime.Now,
                Category = logCategory,
                Level = upperLevel,
                Message = logMessage
            };

            AddSystemLogEntry(entry, false);

            if (logGUI != null && !logGUI.IsDisposed)
            {
                logGUI.RefreshLogs(false);
            }

            if (ShouldAutoStop(upperLevel))
            {
                ForceStopAllChambers();
                ApplyTowerLampStatus(settings.AlarmLampStatus);
            }
        }

        private bool ShouldAutoStop(string level)
        {
            if (settings == null || !settings.AlarmAutoStop) return false;

            int currentLevel = GetAlarmLevelRank(level);
            int thresholdLevel = GetAlarmLevelRank(settings.AutoStopAlarmLevel);
            return currentLevel > 0 && currentLevel >= thresholdLevel;
        }

        private int GetAlarmLevelRank(string level)
        {
            string upperLevel = string.IsNullOrWhiteSpace(level) ? string.Empty : level.Trim().ToUpper();
            if (upperLevel == "WARN") return 1;
            if (upperLevel == "ERROR") return 2;
            if (upperLevel == "FATAL") return 3;
            return 0;
        }

        private string ResolveLogCategory(string level, string message)
        {
            string upperLevel = string.IsNullOrWhiteSpace(level) ? "INFO" : level.Trim().ToUpper();
            string text = message ?? string.Empty;

            if (upperLevel == "WARN" || upperLevel == "ERROR" || upperLevel == "FATAL") return "Alarm";
            if (text.Contains("로그인") || text.Contains("로그아웃") || text.Contains("계정")) return "User";
            if (text.IndexOf("Recipe", StringComparison.OrdinalIgnoreCase) >= 0 || text.Contains("레시피")) return "Recipe";
            if (text.Contains("모드")) return "Mode";
            if (text.IndexOf("Setting", StringComparison.OrdinalIgnoreCase) >= 0 || text.Contains("설정")) return "System Setting";
            if (text.Contains("수동 제어")) return "Manual Control";
            if (text.IndexOf("Alarm", StringComparison.OrdinalIgnoreCase) >= 0 || text.Contains("알람")) return "Alarm";
            if (text.IndexOf("EtherCAT", StringComparison.OrdinalIgnoreCase) >= 0 || text.Contains("통신") || text.Contains("연결")) return "Communication";
            if (text.IndexOf("Export", StringComparison.OrdinalIgnoreCase) >= 0 || text.Contains("내보내기")) return "Data Export";
            if (text.Contains("초기화") || text.Contains("종료") || text.IndexOf("Application", StringComparison.OrdinalIgnoreCase) >= 0) return "System";
            return "Equipment Operation";
        }
    }
}
