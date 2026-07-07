using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace SCT_Form
{
    // 이 파일은 필드 선언과 생성자만 담당한다. 나머지 책임은 분리되어 있다:
    // - CurrentStateGUI.RobotDisplay.cs: 로봇 위치/자세 그래픽
    // - CurrentStateGUI.EquipmentDisplay.cs: 도어/FOUP/PM 웨이퍼 상태 표시
    // - CurrentStateGUI.RecipeData.cs: Process Recipe 콤보/파일 읽기·검증, 관련 DTO
    // - CurrentStateGUI.ChamberRecipePreview.cs: PM Setting 버튼 미리보기(시뮬레이션) 진행률
    // - CurrentStateGUI.AutoSequence.cs: 실제 Start/Pause/Continue/Abort 자동 시퀀스("메인 프로세스")
    // - CurrentStateGUI.Initialize.cs: Initialize 버튼 안전 시퀀스, 버튼 활성화 상태 계산
    public partial class CurrentStateGUI : UserControl
    {
        private static readonly Color FoupFullColor = ColorTranslator.FromHtml("#FFF200");
        private static readonly Color FoupEmptyColor = Color.White;

        private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        private readonly Timer chamberProcessTimer = new Timer();

        private MainGUI main;
        private string selectedPmaRecipePath;
        private string selectedPmbRecipePath;
        private string selectedPmcRecipePath;
        private string selectedProcessRecipePath;
        private ProcessRecipeData currentProcessRecipe;
        private bool isProcessRecipeRunning;
        private bool isProcessRecipePaused;
        private bool isUserAbortRecoveryRunning;
        private bool isInitializing = false;
        private bool isProcessCompleted = false;
        private ChamberProcessState pmaProcessState;
        private ChamberProcessState pmbProcessState;
        private ChamberProcessState pmcProcessState;
        private readonly WaferAutoSequencer waferSequencer = new WaferAutoSequencer();
        private bool robotHasWafer;
        private bool robotCylinderForward;
        private bool robotCylinderBack;
        private long? currentRobotLRPosition;
        private string currentRobotFacingName = "FOUP A";
        private long currentRobotFacingDiff;
        private const long RobotFacingDisplayToleranceCounts = 5000;
        private RobotMapPanel robotPanel;

        public CurrentStateGUI(MainGUI mainGUI)
        {
            InitializeComponent();
            main = mainGUI;

            InitializeProcessRecipeSelector();
            HidePmRecipeSettingButtons();

            InitializeProgressBar(pnl_PMA_progressbar);
            InitializeProgressBar(pnl_PMB_progressbar);
            InitializeProgressBar(pnl_PMC_progressbar);
            RefreshDoorStatusLabels();
            ResetAutoWaferDisplay();
            InitializeRobotPositionMap();

            chamberProcessTimer.Interval = 1000;
            chamberProcessTimer.Tick += chamberProcessTimer_Tick;

            waferSequencer.Aborted += reason =>
            {
                main.WriteSystemLog("WARN", "자동공정 Abort: " + reason);
                if (!isUserAbortRecoveryRunning)
                {
                    main.SafeAbortAndHome();
                }
                MessageBox.Show(reason, "Process Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }
    }
}
