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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCT_Form
{
    // 이 파일은 폼 필드 선언, 생성자, 폼 생명주기(FormClosing/Closed), 상단 패널 전환
    // (Mainpnl_*), 상단 모드 버튼 클릭(btn_Operate/Maint/Recipe/Log/Setting), 200ms 타이머
    // (timer1_Tick)만 담당한다. 로그인/하드웨어 액션/PM 상태/시스템 로그/EtherCAT 연결·타워램프
    // /축 이동 로직은 각각 MainGUI.Login.cs, MainGUI.ChamberHardware.cs, MainGUI.PmStatus.cs,
    // MainGUI.SystemLog.cs, MainGUI.Connection.cs, MainGUI.AxisMotion.cs로 분리되어 있다.
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
        internal bool isAxisPositionFault = false;
        internal bool isWaferSuctionOn = false;
        private int consecutiveConnectionFailures = 0;

        internal string currentUbarState = "Operate";

        private const long DefaultRobotAcceleration = 1000000;
        private const long DefaultRobotDeceleration = 1000000;
        private const long DefaultRobotMaxVelocity = 100000000;
        private const long DefaultRobotVelocity = 1000000;
        private const int Axis1TargetPositionWriteOffset = 3;
        private const int Axis2TargetPositionWriteOffset = 26;

        // UD(상하) 축 위치 확인 안전장치: 이동 명령 후 목표값 ±허용범위 내에 도달하는지
        // timer1(200ms)로 비동기 확인한다. 시간 내 도달하지 못하면 Axis Position Fault를
        // 걸어 EnsureEquipmentOperationAllowed()를 통해 모든 수동/자동 이동을 차단한다.
        private const long Axis1PositionToleranceCounts = 70000;
        private const int Axis1PositionVerifyTimeoutMs = 60000;
        private long? pendingAxis1TargetPosition;
        private DateTime pendingAxis1VerifyDeadline;

        // UD 축 상한 소프트 리밋: 특정 이동 명령과 무관하게 timer1(200ms)마다 항상 확인해서,
        // 실제 좌표가 이 값을 넘어가면(과도한 튐/기계적 리밋 접근 등 원인 불문) 즉시 Axis
        // Position Fault를 걸어 추가 이동을 전부 차단한다. 물리적 충돌을 막기 위한 안전장치.
        private const long Axis1UpperSoftLimit = 3100000;
        private const int AxisTargetPositionByteCount = 4;
        private const long Axis1ShutdownHomePositionLimit = 2000;
        private const int Axis1ShutdownHomeTimeoutMs = 30000;
        private const int AxisMoveCompleteTimeoutMs = 60000;
        private const int StartupCylinderBackTimeoutMs = 10000;

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
            FormClosing += Form1_FormClosing;


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

            Mainpnl_CurrentStateGUI();
            UpdateDateTimeLabels();

            timer1.Interval = 200;
            timer1.Start();

            WriteSystemLog("INFO", "시스템 초기화 완료 (초기 모드: AUTO)");
        }

        // --- 프로그램 종료 처리 ---
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 프로그램 종료 시 자동 원점복귀(Homing) 기능이 제거되었고, 재기동 시 Initialize 시퀀스에서
            // 실린더 후진을 안전하게 먼저 체크하고 후진시키도록 수정되었으므로, 종료 시점의 실린더 센서 차단 락은 제거합니다.
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            WriteSystemLog("INFO", "Application 중단 감지: 안전 시퀀스(Abnormal Stop) 가동");
            try
            {
                // 강제적인 기구 동작(실린더 이동/도어 닫기)을 셧다운 중에 유발하지 않기 위해
                // 전/후진 밸브 출력을 모두 OFF로 바꾸어 동력을 차단합니다.
                EtherCAT_M.Digital_Output(12, false);
                EtherCAT_M.Digital_Output(13, false);

                // 타워등, 챔버등, 진공 소등
                EtherCAT_M.Digital_Output(0, false);
                EtherCAT_M.Digital_Output(1, false);
                EtherCAT_M.Digital_Output(2, false);
                EtherCAT_M.Digital_Output(3, false);
                EtherCAT_M.Digital_Output(6, false);
                EtherCAT_M.Digital_Output(9, false);

                SetWaferSuction(false);
                EtherCAT_M.Digital_Output(15, false);

                servoMotorOFF();
                isServoMotorOn = false;
                WriteSystemLog("INFO", "안전 셧다운 완료: 램프 소등 및 서보 모터 OFF 완료");

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
        // Recipe/Log 화면은 조회 성격이라 모드 변경 인터록(Force Stop)을 걸지 않는다.
        private void btn_Recipe_Click(object sender, EventArgs e)
        {
            if (!EnsureAdminSettingAllowed()) return;
            if (currentUbarState == "Recipe") return;

            currentUbarState = "Recipe";

            UpdateModeButtonStyles();

            Mainpnl_RecipeGUI();

            ApplyTowerLampForMode();
        }

        private void btn_Log_Click(object sender, EventArgs e)
        {
            if (currentUbarState == "Log") return;

            currentUbarState = "Log";

            UpdateModeButtonStyles();

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

                CheckAxis1PositionVerification();
                CheckAxis1SoftLimit();

                if (maintGUI != null)
                {
                    maintGUI.SetCurrentPositionLabel(currentUDPos, currentLRPos);
                }

                if (currentGUI != null && !currentGUI.IsDisposed)
                {
                    currentGUI.RefreshDoorStatusLabels();
                    currentGUI.UpdateRobotPosition(currentLRPos);
                    currentGUI.SetRobotWaferState(isWaferSuctionOn);
                    currentGUI.SetRobotCylinderState(IsCylinderForward(), IsCylinderBack());
                    currentGUI.UpdateControlButtons();
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
