using IEG3268_Dll;
using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class MainGUI : Form
    {
        // log4net 로거 객체 선언
        private static readonly ILog log = LogManager.GetLogger(typeof(MainGUI));

        internal IEG3268 EtherCAT_M = new IEG3268();

        internal bool isConnect = false;
        internal bool isGreenLightOn = false;
        internal bool isChamALampOn = false;
        internal bool isChamBLampOn = false;
        internal bool isChamCLampOn = false;
        internal bool isChamADoorOpen = false;
        internal bool isChamBDoorOpen = false;
        internal bool isChamCDoorOpen = false;
        internal bool isServoMotorOn = false;
        internal bool isAxisMoving = false;

        internal string currentUbarState = "Operate";

        private const long DefaultRobotAcceleration = 1000000;
        private const long DefaultRobotDeceleration = 1000000;
        private const long DefaultRobotMaxVelocity = 100000000;
        private const long DefaultRobotVelocity = 1000000;

        private CurrentStateGUI currentGUI;
        private MaintGUI maintGUI;
        private LogGUI logGUI;
        private RecipeGUI recipeGUI;
        private SettingGUI settingGUI;
        private LoginState loginStateGUI;
        private AccountInfo currentAccount;
        private EquipmentSettings settings;
        private readonly List<SystemLogEntry> systemLogs = new List<SystemLogEntry>();
        private readonly HashSet<string> systemLogKeys = new HashSet<string>();
        private readonly Dictionary<string, Color> pmStatusColors = new Dictionary<string, Color>();
        private readonly HashSet<string> activePmAlarms = new HashSet<string>();
        private long nextLogId;
        private const int MaxSystemLogCount = 20000;
        
        public MainGUI()
        {
            InitializeComponent();

            
        //grpbox_Tower.Enabled = false;
        //btn_Auto.Enabled = false;
        //btn_Manual.Enabled = false;

        //btn_Auto.FlatStyle = FlatStyle.Flat;
        //btn_Auto.FlatAppearance.BorderSize = 0;
        //btn_Manual.FlatStyle = FlatStyle.Flat;
        //btn_Manual.FlatAppearance.BorderSize = 0;

        //LogView.View = View.Details;
        //LogView.FullRowSelect = true;
        //LogView.GridLines = true;
        //LogView.OwnerDraw = false;

        //LogView.Columns.Clear();
        //LogView.Columns.Add("시간", 90, HorizontalAlignment.Center);
        //LogView.Columns.Add("레벨", 70, HorizontalAlignment.Center);
        //LogView.Columns.Add("메시지", 400, HorizontalAlignment.Left);

            settings = EquipmentSettingsService.Current;
            InitializePmStatusColors();
            LoadSystemLogsFromFiles();

            currentGUI = new CurrentStateGUI(this);
            maintGUI = new MaintGUI(this);
            logGUI = new LogGUI(this);
            recipeGUI = new RecipeGUI(this);
            settingGUI = new SettingGUI(this);

            InitializeLoginEntryPoints();

            SystemConnect();
            servoMotorON();
            isServoMotorOn = true;
            setBasicPoint();

            Mainpnl_CurrentStateGUI();
            UpdateDateTimeLabels();

            timer1.Interval = 200;
            timer1.Start();

            WriteSystemLog("INFO", "시스템 초기화 완료 (초기 모드: AUTO)");
        }

        private void InitializeLoginEntryPoints()
        {
            tBox_ID.ReadOnly = true;
            tBox_PW.ReadOnly = true;
            tBox_ID.BackColor = Color.White;
            tBox_PW.BackColor = Color.White;
            tBox_PW.UseSystemPasswordChar = true;
            tBox_ID.Cursor = Cursors.Hand;
            tBox_PW.Cursor = Cursors.Hand;
            tBox_ID.Click += LoginTextBox_Click;
            tBox_PW.Click += LoginTextBox_Click;
            UpdateLoginDisplay();
        }

        private void LoginTextBox_Click(object sender, EventArgs e)
        {
            ShowLoginDialog();
        }

        private void ShowLoginDialog()
        {
            using (LogInGUI loginGUI = new LogInGUI())
            {
                if (loginGUI.ShowDialog(this) != DialogResult.OK) return;

                currentAccount = loginGUI.LoggedInAccount;
                ShowLoginStatePanel();
                WriteSystemLog("User", "INFO", "로그인: " + currentAccount.UserId);
                MessageBox.Show("로그인되었습니다.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateLoginDisplay()
        {
            if (currentAccount == null)
            {
                tBox_ID.Text = string.Empty;
                tBox_PW.Text = string.Empty;
                return;
            }

            tBox_ID.Text = currentAccount.UserId;
            tBox_PW.Text = "********";
        }

        private void ShowLoginInputPanel()
        {
            pnl_LoginChange.Controls.Clear();
            pnl_LogIn.Controls.Clear();
            pnl_LogIn.Controls.Add(lbl_ID, 0, 0);
            pnl_LogIn.Controls.Add(lbl_PW, 1, 0);
            pnl_LogIn.Controls.Add(tBox_ID, 0, 1);
            pnl_LogIn.Controls.Add(tBox_PW, 1, 1);
            pnl_LoginChange.Controls.Add(pnl_LogIn);
            UpdateLoginDisplay();
        }

        private void ShowLoginStatePanel()
        {
            pnl_LoginChange.Controls.Clear();
            loginStateGUI = new LoginState(currentAccount);
            loginStateGUI.Dock = DockStyle.Fill;
            loginStateGUI.LogoutRequested += LoginStateGUI_LogoutRequested;
            pnl_LoginChange.Controls.Add(loginStateGUI);
        }

        private void LoginStateGUI_LogoutRequested(object sender, EventArgs e)
        {
            string logoutUserId = currentAccount == null ? string.Empty : currentAccount.UserId;
            currentAccount = null;
            loginStateGUI = null;
            ShowLoginInputPanel();
            WriteSystemLog("User", "INFO", "로그아웃: " + logoutUserId);
            MessageBox.Show("로그아웃되었습니다.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal bool IsLoggedIn
        {
            get { return currentAccount != null; }
        }

        internal bool IsAdminLoggedIn
        {
            get { return AccountService.IsAdmin(currentAccount); }
        }

        internal bool EnsureEquipmentOperationAllowed()
        {
            if (!IsLoggedIn)
            {
                MessageBox.Show("장비 동작을 하려면 로그인해주세요", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return EnsureDoorInterlockAllowed();
        }

        private bool EnsureDoorInterlockAllowed()
        {
            if (settings == null || !settings.DoorOpenInterlock) return true;
            if (!isChamADoorOpen && !isChamBDoorOpen && !isChamCDoorOpen) return true;

            MessageBox.Show("Door Open Interlock 상태입니다. Chamber Door를 닫은 후 동작해주세요.", "Safety Interlock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            WriteSystemLog("Alarm", "WARN", "Door Open Interlock: 장비 동작 차단");
            return false;
        }

        internal bool EnsureAdminSettingAllowed()
        {
            if (IsAdminLoggedIn) return true;

            MessageBox.Show("설정을 하려면 관리자 계정으로 로그인해주세요", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        internal void SetChamberDoorStatus(string pmName, bool isOpen)
        {
            string normalizedPmName = NormalizePmName(pmName);

            if (normalizedPmName == "PM A")
            {
                isChamADoorOpen = isOpen;
            }
            else if (normalizedPmName == "PM B")
            {
                isChamBDoorOpen = isOpen;
            }
            else if (normalizedPmName == "PM C")
            {
                isChamCDoorOpen = isOpen;
            }

            if (currentGUI != null && !currentGUI.IsDisposed)
            {
                currentGUI.SetDoorStatus(normalizedPmName, isOpen);
            }
        }

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

        private void InitializePmStatusColors()
        {
            pmStatusColors["PM A"] = lbl_PMAStatus.ForeColor;
            pmStatusColors["PM B"] = lbl_PMBStatus.ForeColor;
            pmStatusColors["PM C"] = lbl_PMCStatus.ForeColor;
        }

        internal void RaisePmAlarm(string pmName, string level, string message)
        {
            string normalizedPmName = NormalizePmName(pmName);
            System.Windows.Forms.Label statusLabel = GetPmStatusLabel(normalizedPmName);
            if (statusLabel == null) return;

            activePmAlarms.Add(normalizedPmName);
            statusLabel.ForeColor = Color.Red;

            string alarmMessage = normalizedPmName + " " + (message ?? string.Empty);
            WriteSystemLog("Alarm", level, alarmMessage.Trim());

            if (settings != null)
            {
                ApplyTowerLampStatus(settings.AlarmLampStatus);
            }
        }

        internal bool TrySyncPmStatusLabel(System.Windows.Forms.Label targetLabel, Color statusColor)
        {
            string pmName = GetPmNameByStatusLabel(targetLabel);
            if (string.IsNullOrEmpty(pmName)) return false;

            pmStatusColors[pmName] = statusColor;
            targetLabel.ForeColor = activePmAlarms.Contains(pmName) ? Color.Red : statusColor;
            return true;
        }

        private void ResetPmAlarms()
        {
            if (activePmAlarms.Count == 0)
            {
                WriteSystemLog("Alarm", "INFO", "Alarm Reset 요청: Active PM Alarm 없음");
                return;
            }

            foreach (string pmName in activePmAlarms.ToList())
            {
                System.Windows.Forms.Label statusLabel = GetPmStatusLabel(pmName);
                if (statusLabel != null)
                {
                    Color statusColor;
                    statusLabel.ForeColor = pmStatusColors.TryGetValue(pmName, out statusColor) ? statusColor : Color.Gray;
                }
            }

            activePmAlarms.Clear();
            ApplyTowerLampForMode();
            WriteSystemLog("Alarm", "INFO", "Alarm Reset 완료: PM Alarm 표시 해제");
        }

        private string NormalizePmName(string pmName)
        {
            string value = string.IsNullOrWhiteSpace(pmName) ? string.Empty : pmName.Trim().ToUpper();
            if (value == "PMA" || value == "PM A") return "PM A";
            if (value == "PMB" || value == "PM B") return "PM B";
            if (value == "PMC" || value == "PM C") return "PM C";
            return string.Empty;
        }

        private System.Windows.Forms.Label GetPmStatusLabel(string pmName)
        {
            if (pmName == "PM A") return lbl_PMAStatus;
            if (pmName == "PM B") return lbl_PMBStatus;
            if (pmName == "PM C") return lbl_PMCStatus;
            return null;
        }

        private string GetPmNameByStatusLabel(System.Windows.Forms.Label statusLabel)
        {
            if (statusLabel == lbl_PMAStatus) return "PM A";
            if (statusLabel == lbl_PMBStatus) return "PM B";
            if (statusLabel == lbl_PMCStatus) return "PM C";
            return string.Empty;
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
                    foreach (string line in File.ReadLines(filePath))
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

        private void SystemConnect()
        {
            WriteSystemLog("INFO", "EtherCAT 마스터 연결 시도 중...");
            try
            {
                bool isConnected = false;
                int retryCount = settings == null ? 3 : settings.ReconnectRetryCount;
                for (int attempt = 0; attempt <= retryCount; attempt++)
                {
                    if (EtherCAT_M.CIFX_50RE_Connect() == true)
                    {
                        isConnected = true;
                        break;
                    }

                    if (attempt < retryCount)
                    {
                        WriteSystemLog("WARN", "EtherCAT 마스터 연결 재시도: " + (attempt + 1) + "/" + retryCount);
                    }
                }

                if (isConnected)
                {
                    WriteSystemLog("INFO", "EtherCAT 마스터 연결 성공 (Connect OK)");
                    isConnect = true;

                    EtherCAT_M.ReadData_Send_Start(settings.EtherCatReadCycleMs);
                    EtherCAT_M.ReadData_Timer_Start();
                    // 타이머 시작 같은 개발 분석용 세부 로직은 라벨을 가리지 않고 파일에만 남김
                    log.Debug("EtherCAT 데이터 리드 타이머 시작 (주기: " + settings.EtherCatReadCycleMs + "ms)");

                    lbl_CurrentConnect.ForeColor = Color.Lime;

                    // 플래그 초기화
                    isChamALampOn = false; isChamBLampOn = false; isChamCLampOn = false;
                    isChamADoorOpen = false; isChamBDoorOpen = false; isChamCDoorOpen = false;
                    if (currentGUI != null && !currentGUI.IsDisposed)
                    {
                        currentGUI.RefreshDoorStatusLabels();
                    }

                    currentUbarState = "Operate";

                    // 상단버튼 활성화
                    btn_Operate.Enabled = true;
                    btn_Maint.Enabled = true;

                    // 모드에 맞게 버튼 색상 스타일 출력
                    UpdateModeButtonStyles();

                    // 모든 챔버 문 초기 닫기 출력
                    EtherCAT_M.Digital_Output(5, false);
                    EtherCAT_M.Digital_Output(4, true);
                    EtherCAT_M.Digital_Output(8, false);
                    EtherCAT_M.Digital_Output(7, true);
                    EtherCAT_M.Digital_Output(11, false);
                    EtherCAT_M.Digital_Output(10, true);
                    WriteSystemLog("INFO", "장비 초기화 세팅: 모든 챔버 도어 CLOSE 명령 출력");

                    // 황색등 점등
                    ApplyTowerLampStatus(settings.IdleLampStatus);
                    WriteSystemLog("INFO", "타워램프 상태 변경: " + settings.IdleLampStatus + " (장비 대기)");
                }
                else
                {
                    WriteSystemLog("WARN", "EtherCAT 마스터 연결 실패 (하드웨어 감지 안 됨)");
                }
            }
            catch (Exception ex)
            {
                log.Error("EtherCAT 연결 처리 중 예외 발생: ", ex);
                WriteSystemLog("ERROR", $"연결 예외 오류: {ex.Message}");
            }
        }
        private void Reconnect_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            if (isConnect) return;
            SystemConnect();
            servoMotorON();
        }
        // --- 타워 램프 제어 영역 ---
        private void RedLightOn_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(0, true);
            WriteSystemLog("INFO", "수동 제어: 타워램프 적색등(Red) ON");
        }

        private void RedLightOff_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(0, false);
            WriteSystemLog("INFO", "수동 제어: 타워램프 적색등(Red) OFF");
        }

        private void YellowLightOn_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(1, true);
            WriteSystemLog("INFO", "수동 제어: 타워램프 황색등(Yellow) ON");
        }

        private void YellowLightOff_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(1, false);
            WriteSystemLog("INFO", "수동 제어: 타워램프 황색등(Yellow) OFF");
        }

        private void GreenLightOn_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(2, true);
            isGreenLightOn = true;
            WriteSystemLog("INFO", "수동 제어: 타워램프 녹색등(Green) ON");
        }

        private void GreenLightOff_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(2, false);
            isGreenLightOn = false;
            WriteSystemLog("INFO", "수동 제어: 타워램프 녹색등(Green) OFF");
        }

        private void AllLightOn_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(0, true);
            EtherCAT_M.Digital_Output(1, true);
            EtherCAT_M.Digital_Output(2, true);
            WriteSystemLog("INFO", "수동 제어: 타워램프 전체 등 ON");
        }

        private void AllLightOff_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            EtherCAT_M.Digital_Output(0, false);
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(2, false);
            WriteSystemLog("INFO", "수동 제어: 타워램프 전체 등 OFF");
        }
        // --- 프로그램 종료 처리 ---
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            WriteSystemLog("INFO", "Application 중단 감지: 안전 시퀀스(Abnormal Stop) 가동");
            try
            {
                // 타워등, 챔버등, 실린더 오프 가동
                EtherCAT_M.Digital_Output(0, false);
                EtherCAT_M.Digital_Output(1, false);
                EtherCAT_M.Digital_Output(2, false);
                EtherCAT_M.Digital_Output(3, false);
                EtherCAT_M.Digital_Output(6, false);
                EtherCAT_M.Digital_Output(9, false);

                EtherCAT_M.Digital_Output(5, false);
                EtherCAT_M.Digital_Output(4, true);
                EtherCAT_M.Digital_Output(8, false);
                EtherCAT_M.Digital_Output(7, true);
                EtherCAT_M.Digital_Output(11, false);
                EtherCAT_M.Digital_Output(10, true);
                WriteSystemLog("INFO", "안전 셧다운 완료: 모든 램프 소등 및 도어 폐쇄 완료");

                setBasicPoint();
                servoMotorOFF();
                isServoMotorOn = false;
                WriteSystemLog("INFO", "안전 셧다운 완료: 로봇 초기 위치 설정 및 서보 모터 종료");

                EtherCAT_M.CIFX_50RE_Disconnect();
                WriteSystemLog("INFO", "EtherCAT 마스터 통신 채널 정상 해제 완료");
            }
            catch (Exception ex)
            {
                log.Fatal("폼 종료 안전 제어 중 예외 오류: ", ex);
            }
        }

        // --- 상단 모드 변경 조작 ---
        private void btn_Operate_Click(object sender, EventArgs e)
        {
            if (currentUbarState == "Operate") return;

            currentUbarState = "Operate";

            UpdateModeButtonStyles();

            if (settings.ModeChangeForceStop)
            {
                ForceStopAllChambers();
            }

            Mainpnl_CurrentStateGUI();

            ApplyTowerLampForMode();
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            btn_Operate_Click(sender, e);
        }

        private void btn_AlarmReset_Click(object sender, EventArgs e)
        {
            ResetPmAlarms();
        }

        private void btn_maint_Click(object sender, EventArgs e)
        {
            if (!EnsureEquipmentOperationAllowed()) return;
            if (currentUbarState == "Maint") return;

            currentUbarState = "Maint";

            UpdateModeButtonStyles();

            if (settings.ModeChangeForceStop)
            {
                ForceStopAllChambers();
            }

            Mainpnl_MaintGUI();

            ApplyTowerLampForMode();
        }
        private void btn_Recipe_Click(object sender, EventArgs e)
        {
            if (!EnsureAdminSettingAllowed()) return;
            if (currentUbarState == "Recipe") return;

            currentUbarState = "Recipe";

            UpdateModeButtonStyles();

            if (settings.ModeChangeForceStop)
            {
                ForceStopAllChambers();
            }

            Mainpnl_RecipeGUI();

            ApplyTowerLampForMode();
        }

        private void btn_Log_Click(object sender, EventArgs e)
        {
            if (currentUbarState == "Log") return;

            currentUbarState = "Log";

            UpdateModeButtonStyles();

            if (settings.ModeChangeForceStop)
            {
                ForceStopAllChambers();
            }

            Mainpnl_LogGUI();

            ApplyTowerLampForMode();
        }
        private void btn_Setting_Click(object sender, EventArgs e)
        {
            if (!EnsureAdminSettingAllowed()) return;
            if (currentUbarState == "Setting") return;

            currentUbarState = "Setting";

            UpdateModeButtonStyles();

            if (settings.ModeChangeForceStop)
            {
                ForceStopAllChambers();
            }

            Mainpnl_SettingGUI();

            ApplyTowerLampForMode();
        }

        private void ApplyTowerLampForMode()
        {
            if (settings == null) return;

            if (currentUbarState == "Maint" || currentUbarState == "Recipe" || currentUbarState == "Setting")
            {
                ApplyTowerLampStatus(settings.MaintenanceLampStatus);
                return;
            }

            ApplyTowerLampStatus(settings.IdleLampStatus);
        }

        private void ApplyTowerLampStatus(string status)
        {
            if (!isConnect || EtherCAT_M == null) return;

            string lampStatus = string.IsNullOrWhiteSpace(status) ? "Off" : status.Trim();
            bool redOn = string.Equals(lampStatus, "Red", StringComparison.OrdinalIgnoreCase);
            bool yellowOn = string.Equals(lampStatus, "Yellow", StringComparison.OrdinalIgnoreCase);
            bool greenOn = string.Equals(lampStatus, "Green", StringComparison.OrdinalIgnoreCase);

            EtherCAT_M.Digital_Output(0, redOn);
            EtherCAT_M.Digital_Output(1, yellowOn);
            EtherCAT_M.Digital_Output(2, greenOn);

            isGreenLightOn = greenOn;
        }
        private void ForceStopAllChambers()
        {
            WriteSystemLog("INFO", "모드 변경에 따른 전 공정 인터록(Force Stop) 가동");
            EtherCAT_M.Digital_Output(0, false);
            EtherCAT_M.Digital_Output(1, false);
            EtherCAT_M.Digital_Output(2, false);
            EtherCAT_M.Digital_Output(3, false);
            EtherCAT_M.Digital_Output(6, false);
            EtherCAT_M.Digital_Output(9, false);

            isChamALampOn = false;
            isChamBLampOn = false;
            isChamCLampOn = false;

            log.Debug("인터록 완료: 모든 공정 인터록 및 GUI 상태 리셋 완료");
        }

        private void btnMode_Paint(object sender, PaintEventArgs e)
        {
            // 화면 렌더링 영역은 부하 조절 및 가독성을 위해 로그 생략
            Button btn = (Button)sender;
            bool isActive = (currentUbarState == "Operate" && btn == btn_Operate) ||
                            (currentUbarState == "Maint" && btn == btn_Maint) ||
                            (currentUbarState == "Recipe" && btn == btn_Recipe) ||
                            (currentUbarState == "Log" && btn == btn_Log) ||
                            (currentUbarState == "Setting" && btn == btn_Setting);

            Color highlight = isActive ? Color.Gray : Color.White;
            Color shadow = isActive ? Color.White : Color.Gray;
            int borderThickness = isActive ? 2 : 1;

            ControlPaint.DrawBorder(e.Graphics, btn.ClientRectangle,
                highlight, borderThickness, ButtonBorderStyle.Solid,
                highlight, borderThickness, ButtonBorderStyle.Solid,
                shadow, borderThickness, ButtonBorderStyle.Solid,
                shadow, borderThickness, ButtonBorderStyle.Solid);
        }

        private void UpdateModeButtonStyles()
        {
            ApplyModeButtonStyle(btn_Operate, "Operate");
            ApplyModeButtonStyle(btn_Maint, "Maint");
            ApplyModeButtonStyle(btn_Recipe, "Recipe");
            ApplyModeButtonStyle(btn_Log, "Log");
            ApplyModeButtonStyle(btn_Setting, "Setting");
        }

        private void ApplyModeButtonStyle(Button button, string modeName)
        {
            bool isActive = currentUbarState == modeName;

            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = isActive ? 2 : 1;
            button.FlatAppearance.BorderColor = isActive ? Color.White : Color.Black;
            button.BackColor = isActive ? Color.SkyBlue : Color.FromArgb(60, 60, 60);
            button.ForeColor = isActive ? Color.White : Color.DimGray;
            button.Invalidate();
        }

        internal void servoMotorON()
        {
            ApplySettingRobotAxisConfig();
            EtherCAT_M.Axis1_ON();
            EtherCAT_M.Axis2_ON();
        }

        internal void servoMotorOFF()
        {
            EtherCAT_M.Axis1_OFF();
            EtherCAT_M.Axis2_OFF();
        }
        internal void setBasicPoint()
        {
            EtherCAT_M.Axis1_UD_Homming(); //상하 원점복귀
            EtherCAT_M.Axis2_LR_Homming(); //좌우 원점복귀
        }

        internal void SetRobotAxisConfig(long acceleration, long deceleration, long maxVelocity, long velocity)
        {
            if (!isConnect || EtherCAT_M == null) return;

            EtherCAT_M.Axis1_UD_Config_Update(acceleration, deceleration, maxVelocity, velocity);
            EtherCAT_M.Axis2_LR_Config_Update(acceleration, deceleration, maxVelocity, velocity);
        }

        private void ApplySettingRobotAxisConfig()
        {
            if (settingGUI != null)
            {
                settingGUI.ApplyRobotAxisConfig();
                return;
            }

            SetRobotAxisConfig(DefaultRobotAcceleration, DefaultRobotDeceleration, DefaultRobotMaxVelocity, DefaultRobotVelocity);
        }
        
        private void Mainpnl_CurrentStateGUI() {
            Mainpnl.Controls.Clear();
            currentGUI.Dock = DockStyle.Fill;
            Mainpnl.Controls.Add(currentGUI);
        }

        private void Mainpnl_MaintGUI()
        {
            Mainpnl.Controls.Clear();
            maintGUI.Dock = DockStyle.Fill;
            Mainpnl.Controls.Add(maintGUI);
        }

        private void Mainpnl_RecipeGUI()
        {
            Mainpnl.Controls.Clear();
            recipeGUI.ShowDefaultPmA();
            recipeGUI.Dock = DockStyle.Fill;
            Mainpnl.Controls.Add(recipeGUI);
        }
        private void Mainpnl_LogGUI()
        {
            Mainpnl.Controls.Clear();
            logGUI.Dock = DockStyle.Fill;
            Mainpnl.Controls.Add(logGUI);
        }

        private void Mainpnl_SettingGUI()
        {
            Mainpnl.Controls.Clear();
            settingGUI.Dock = DockStyle.Fill;
            Mainpnl.Controls.Add(settingGUI);
        }

        private void UpdateDateTimeLabels()
        {
            DateTime now = DateTime.Now;
            lbl_Date.Text = now.ToString("yyyy-MM-dd");
            lbl_Time.Text = now.ToString("HH:mm:ss");
        }

        internal void timer1_Tick(object sender, EventArgs e)
        {
            UpdateDateTimeLabels();
            if (logGUI != null && !logGUI.IsDisposed)
            {
                logGUI.RefreshLogs(false);
            }

            if (!isConnect || EtherCAT_M == null) return;

            try
            {
                string currentUDPos = EtherCAT_M.Axis1_is_PosData();
                string currentLRPos = EtherCAT_M.Axis2_is_PosData();

                if (maintGUI != null)
                {
                    maintGUI.SetCurrentPositionLabel(currentUDPos, currentLRPos);
                }
            }
            catch (Exception ex)
            {
                log.Error("UI 모니터링 타이머 처리 중 예외 발생: ", ex);
                WriteSystemLog("ERROR", $"UI 모니터링 타이머 처리 중 예외 발생: {ex.Message}");
            }
        }

    }
}
