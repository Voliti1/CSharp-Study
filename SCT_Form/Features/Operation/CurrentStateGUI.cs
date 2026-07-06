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

        // 디자이너의 빈 pnl_Robot 자리에 더블버퍼링 되는 RobotMapPanel을 얹어
        // 로봇 상태(방향/전진·후진/웨이퍼 보유)를 그린다.
        private void InitializeRobotPositionMap()
        {
            if (robotPanel != null) return;

            robotPanel = new RobotMapPanel();
            robotPanel.Location = pnl_Robot.Location;
            robotPanel.Size = pnl_Robot.Size;
            robotPanel.Anchor = pnl_Robot.Anchor;
            robotPanel.Margin = pnl_Robot.Margin;
            robotPanel.BackColor = Color.White;
            robotPanel.BorderStyle = BorderStyle.FixedSingle;
            robotPanel.Paint += robotPanel_Paint;

            Controls.Remove(pnl_Robot);
            Controls.Add(robotPanel);
            robotPanel.BringToFront();
        }

        internal void SetRobotWaferState(bool hasWafer)
        {
            robotHasWafer = hasWafer;
            if (robotPanel != null) robotPanel.Invalidate();
        }

        internal void SetRobotCylinderState(bool isForward, bool isBack)
        {
            robotCylinderForward = isForward;
            robotCylinderBack = isBack;
            if (robotPanel != null) robotPanel.Invalidate();
        }

        internal void UpdateRobotPosition(string currentLRPos)
        {
            long parsed;
            if (!long.TryParse(currentLRPos, out parsed))
            {
                // 순간적인 통신 딜레이나 파싱 실패 시, 이전의 정상적인 로봇 그래픽 위치를 유지하여
                // 그래픽이 FOUP A(디폴트 각도) 방향으로 오작동하여 멈춰버리는 현상을 방지합니다.
                return;
            }

            currentRobotLRPosition = parsed;
            RobotTarget nearest = GetNearestRobotTarget(parsed);
            currentRobotFacingName = nearest == null ? "UNKNOWN" : nearest.Name;
            currentRobotFacingDiff = nearest == null ? 0 : Math.Abs(parsed - nearest.LR);
            if (robotPanel != null) robotPanel.Invalidate();
        }

        private RobotTarget GetNearestRobotTarget(long lrPosition)
        {
            RobotTarget nearest = null;
            long nearestDiff = long.MaxValue;

            foreach (RobotTarget target in GetRobotTargets())
            {
                long diff = Math.Abs(lrPosition - target.LR);
                if (diff >= nearestDiff) continue;

                nearest = target;
                nearestDiff = diff;
            }

            return nearest;
        }

        private RobotTarget GetRobotTarget(string targetName)
        {
            foreach (RobotTarget target in GetRobotTargets())
            {
                if (string.Equals(target.Name, targetName, StringComparison.OrdinalIgnoreCase)) return target;
            }

            return null;
        }

        // 각 스테이션을 바라볼 때의 회전 각도(12시=0도, 시계방향 +).
        // PM A=9시(270도), PM B=12시(0도), PM C=3시(90도),
        // FOUP A/B는 화면 배치에 맞춰 좌하단/우하단.
        private List<RobotTarget> GetRobotTargets()
        {
            return new List<RobotTarget>
            {
                new RobotTarget("PM A", EquipmentLayout.GetModule("PM A").LR, 270F),
                new RobotTarget("PM B", EquipmentLayout.GetModule("PM B").LR, 0F),
                new RobotTarget("PM C", EquipmentLayout.GetModule("PM C").LR, 90F),
                new RobotTarget("FOUP A", EquipmentLayout.GetFoup("FOUP A").LR, 225F),
                new RobotTarget("FOUP B", EquipmentLayout.GetFoup("FOUP B").LR, 135F)
            };
        }

        private void robotPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = robotPanel.ClientRectangle;
            PointF robotCenter = new PointF(bounds.Width / 2F, bounds.Height / 2F);
            RobotTarget nearest = currentRobotLRPosition.HasValue ? GetNearestRobotTarget(currentRobotLRPosition.Value) : GetRobotTarget("FOUP A");
            bool isFacingTarget = nearest != null && currentRobotFacingDiff <= RobotFacingDisplayToleranceCounts;
            float rotationDegrees = nearest == null ? 0F : nearest.AngleDegrees;

            DrawPhotoStyleRobot(g, robotCenter, rotationDegrees, isFacingTarget);
        }

        private void DrawPhotoStyleRobot(Graphics g, PointF center, float rotationDegrees, bool isFacingTarget)
        {
            // 실린더 전진 시 그립(엔드이펙터)이 몸체에서 더 멀리 뻗고, 후진 시 몸체 쪽으로 당겨진다.
            float gripCenterY;
            if (robotCylinderForward && !robotCylinderBack) gripCenterY = -98F;
            else if (!robotCylinderForward && robotCylinderBack) gripCenterY = -60F;
            else gripCenterY = -80F;

            GraphicsState state = g.Save();
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(rotationDegrees);

            using (Pen outlinePen = new Pen(Color.Black, 4F))
            using (Pen armPen = new Pen(isFacingTarget ? Color.SeaGreen : Color.DarkOrange, 7F))
            using (Brush bodyBrush = new SolidBrush(Color.FromArgb(238, 241, 246)))
            using (Brush bodyShadowBrush = new SolidBrush(Color.FromArgb(206, 214, 224)))
            using (Brush waferBrush = new SolidBrush(robotHasWafer ? Color.Gold : Color.White))
            using (GraphicsPath bodyPath = new GraphicsPath())
            {
                armPen.StartCap = LineCap.Round;
                armPen.EndCap = LineCap.Round;

                bodyPath.AddRectangle(new RectangleF(-14F, -34F, 28F, 72F));
                g.FillPath(bodyBrush, bodyPath);
                g.FillRectangle(bodyShadowBrush, 3F, -30F, 8F, 64F);
                g.DrawPath(outlinePen, bodyPath);

                // 몸체 -> 그립을 잇는 암 (전진/후진에 따라 길이 변화)
                g.DrawLine(armPen, 0F, -30F, 0F, gripCenterY + 22F);

                RectangleF waferGripRect = new RectangleF(-24F, gripCenterY - 24F, 48F, 48F);
                g.FillEllipse(waferBrush, waferGripRect);
                g.DrawEllipse(outlinePen, waferGripRect);
            }

            g.Restore(state);
        }

        private class RobotTarget
        {
            public RobotTarget(string name, long lr, float angleDegrees)
            {
                Name = name;
                LR = lr;
                AngleDegrees = angleDegrees;
            }

            public string Name { get; private set; }
            public long LR { get; private set; }
            public float AngleDegrees { get; private set; }
        }

        private class RobotMapPanel : Panel
        {
            public RobotMapPanel()
            {
                DoubleBuffered = true;
                ResizeRedraw = true;
            }
        }

        internal void RefreshDoorStatusLabels()
        {
            if (main == null) return;

            SetDoorStatusLabel(lbl_PMA_DoorStatus, "PM A");
            SetDoorStatusLabel(lbl_PMB_DoorStatus, "PM B");
            SetDoorStatusLabel(lbl_PMC_DoorStatus, "PM C");
        }

        internal void SetDoorStatus(string pmName, bool isOpen)
        {
            if (pmName == "PM A")
            {
                SetDoorStatusLabel(lbl_PMA_DoorStatus, pmName);
            }
            else if (pmName == "PM B")
            {
                SetDoorStatusLabel(lbl_PMB_DoorStatus, pmName);
            }
            else if (pmName == "PM C")
            {
                SetDoorStatusLabel(lbl_PMC_DoorStatus, pmName);
            }
        }

        private void SetDoorStatusLabel(Label label, string module)
        {
            if (label == null) return;

            bool isOpen = main != null && main.IsChamberDoorOpen(module);
            bool isClosed = main != null && main.IsChamberDoorClosed(module);

            if (isOpen && !isClosed)
            {
                label.Text = "Door Open";
                label.ForeColor = Color.Goldenrod;
            }
            else if (!isOpen && isClosed)
            {
                label.Text = "Door Close";
                label.ForeColor = Color.DimGray;
            }
            else
            {
                label.Text = "Door Check";
                label.ForeColor = Color.Firebrick;
            }
        }

        private void InitializeProcessRecipeSelector()
        {
            cbox_ProcessRecipe.DropDownStyle = ComboBoxStyle.DropDownList;
            cbox_ProcessRecipe.SelectedIndexChanged += cbox_ProcessRecipe_SelectedIndexChanged;
            cbox_ProcessRecipe.DropDown += cbox_ProcessRecipe_DropDown;
            LoadProcessRecipeSelector();
        }

        private void HidePmRecipeSettingButtons()
        {
            HidePmRecipeSettingButton(btn_PMA_Setting);
            HidePmRecipeSettingButton(btn_PMB_Setting);
            HidePmRecipeSettingButton(btn_PMC_Setting);
        }

        private void HidePmRecipeSettingButton(Button button)
        {
            button.Visible = false;
            button.Enabled = false;
            button.TabStop = false;
        }

        private void cbox_ProcessRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessRecipeComboItem item = cbox_ProcessRecipe.SelectedItem as ProcessRecipeComboItem;
            selectedProcessRecipePath = item == null ? null : item.FilePath;
        }

        private void cbox_ProcessRecipe_DropDown(object sender, EventArgs e)
        {
            LoadProcessRecipeSelector();
        }

        private void LoadProcessRecipeSelector()
        {
            string previouslySelectedPath = selectedProcessRecipePath;
            cbox_ProcessRecipe.Items.Clear();

            EnsureProcessRecipeFolder();

            foreach (string filePath in Directory.GetFiles(GetDefaultProcessRecipeFolderPath(), "*.json").OrderBy(Path.GetFileNameWithoutExtension))
            {
                cbox_ProcessRecipe.Items.Add(new ProcessRecipeComboItem(filePath));
            }

            if (cbox_ProcessRecipe.Items.Count == 0)
            {
                selectedProcessRecipePath = null;
                return;
            }

            int selectedIndex = 0;
            for (int i = 0; i < cbox_ProcessRecipe.Items.Count; i++)
            {
                ProcessRecipeComboItem item = cbox_ProcessRecipe.Items[i] as ProcessRecipeComboItem;
                if (item != null && PathsEqual(item.FilePath, previouslySelectedPath))
                {
                    selectedIndex = i;
                    break;
                }
            }

            cbox_ProcessRecipe.SelectedIndex = selectedIndex;
        }

        private void btn_FOUPA_Full_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            SetFoupAColor(FoupFullColor);
        }

        private void btn_FOUPA_Empty_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            SetFoupAColor(FoupEmptyColor);
        }

        private void btn_FOUPB_Full_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            SetFoupBColor(FoupFullColor);
        }

        private void btn_FOUPB_Empty_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            SetFoupBColor(FoupEmptyColor);
        }

        private bool CanOperateEquipment()
        {
            return main == null || main.EnsureEquipmentOperationAllowed();
        }

        private void SetFoupAColor(Color color)
        {
            SetPanelColors(color, pnl_FOUP_A_5, pnl_FOUP_A_4, pnl_FOUP_A_3, pnl_FOUP_A_2, pnl_FOUP_A_1);
        }

        private void SetFoupBColor(Color color)
        {
            SetPanelColors(color, pnl_FOUP_B_5, pnl_FOUP_B_4, pnl_FOUP_B_3, pnl_FOUP_B_2, pnl_FOUP_B_1);
        }

        private void ResetAutoWaferDisplay()
        {
            SetFoupAColor(FoupFullColor);
            SetFoupBColor(FoupEmptyColor);
            SetModuleWaferState("PM A", false);
            SetModuleWaferState("PM B", false);
            SetModuleWaferState("PM C", false);
        }

        private void SetFoupSlotState(string foup, int slot, bool hasWafer)
        {
            Panel slotPanel = GetFoupSlotPanel(foup, slot);
            if (slotPanel == null) return;

            slotPanel.BackColor = hasWafer ? FoupFullColor : FoupEmptyColor;
        }

        private Panel GetFoupSlotPanel(string foup, int slot)
        {
            bool isFoupA = string.Equals(foup, "FOUP A", StringComparison.OrdinalIgnoreCase);
            bool isFoupB = string.Equals(foup, "FOUP B", StringComparison.OrdinalIgnoreCase);
            if (!isFoupA && !isFoupB) return null;

            if (isFoupA)
            {
                if (slot == 1) return pnl_FOUP_A_1;
                if (slot == 2) return pnl_FOUP_A_2;
                if (slot == 3) return pnl_FOUP_A_3;
                if (slot == 4) return pnl_FOUP_A_4;
                if (slot == 5) return pnl_FOUP_A_5;
            }

            if (slot == 1) return pnl_FOUP_B_1;
            if (slot == 2) return pnl_FOUP_B_2;
            if (slot == 3) return pnl_FOUP_B_3;
            if (slot == 4) return pnl_FOUP_B_4;
            if (slot == 5) return pnl_FOUP_B_5;

            return null;
        }

        private void SetModuleWaferState(string module, bool hasWafer)
        {
            WaferControl waferControl = GetModuleWaferControl(module);
            if (waferControl == null) return;

            waferControl.State = hasWafer ? WaferControl.WaferState.Present : WaferControl.WaferState.Empty;
        }

        private WaferControl GetModuleWaferControl(string module)
        {
            string normalizedModule = EquipmentLayout.NormalizeModule(module);
            if (normalizedModule == "PM A") return waferControl2;
            if (normalizedModule == "PM B") return waferControl1;
            if (normalizedModule == "PM C") return waferControl3;
            return null;
        }

        private void SetPanelColors(Color color, params Panel[] panels)
        {
            foreach (Panel panel in panels)
            {
                panel.BackColor = color;
            }
        }

        private void btn_PMA_Setting_Click(object sender, EventArgs e)
        {
            OpenChamberRecipeSetting("PM A");
        }

        private void btn_PMB_Setting_Click(object sender, EventArgs e)
        {
            OpenChamberRecipeSetting("PM B");
        }

        private void btn_PMC_Setting_Click(object sender, EventArgs e)
        {
            OpenChamberRecipeSetting("PM C");
        }

        private void OpenChamberRecipeSetting(string pmName)
        {
            if (main != null && !main.EnsureAdminSettingAllowed()) return;

            using (ChamberRecipeSetting recipeSetting = new ChamberRecipeSetting(pmName))
            {
                if (recipeSetting.ShowDialog(this) != DialogResult.OK) return;

                ApplySelectedChamberRecipe(
                    recipeSetting.SelectedPM,
                    recipeSetting.SelectedRecipePath,
                    recipeSetting.SelectedRecipeName,
                    recipeSetting.SelectedPPID);
            }
        }

        private void ApplySelectedChamberRecipe(string pmName, string recipePath, string recipeName, string ppid)
        {
            string displayPpid = string.IsNullOrWhiteSpace(ppid) ? string.Empty : ppid;
            RecipeData recipe = ReadRecipe(recipePath);
            int processTime = GetProcessTime(recipe);

            if (pmName == "PM A")
            {
                selectedPmaRecipePath = recipePath;
                pmaProcessState = CreateProcessState(
                    displayPpid,
                    processTime,
                    lbl_PMA_recipename,
                    lbl_PMA_stepname,
                    lbl_PMA_recipetime,
                    lbl_PMA_steptime,
                    lbl_PMA_stepnum,
                    lbl_PMA_messagecontent,
                    pnl_PMA_progressbar,
                    lbl_PMA_Status,
                    main.lbl_PMAStatus);
                UpdateChamberProcessDisplay(pmaProcessState);
            }
            else if (pmName == "PM B")
            {
                selectedPmbRecipePath = recipePath;
                pmbProcessState = CreateProcessState(
                    displayPpid,
                    processTime,
                    lbl_PMB_recipename,
                    lbl_PMB_stepname,
                    lbl_PMB_recipetime,
                    lbl_PMB_steptime,
                    lbl_PMB_stepnum,
                    lbl_PMB_messagecontent,
                    pnl_PMB_progressbar,
                    lbl_PMB_Status,
                    main.lbl_PMBStatus);
                UpdateChamberProcessDisplay(pmbProcessState);
            }
            else if (pmName == "PM C")
            {
                selectedPmcRecipePath = recipePath;
                pmcProcessState = CreateProcessState(
                    displayPpid,
                    processTime,
                    lbl_PMC_recipename,
                    lbl_PMC_stepname,
                    lbl_PMC_recipetime,
                    lbl_PMC_steptime,
                    lbl_PMC_stepnum,
                    lbl_PMC_messagecontent,
                    pnl_PMC_progressbar,
                    lbl_PMC_Status,
                    main.lbl_PMCStatus);
                UpdateChamberProcessDisplay(pmcProcessState);
            }

            if (!chamberProcessTimer.Enabled)
            {
                chamberProcessTimer.Start();
            }
        }

        private ChamberProcessState CreateProcessState(
            string ppid,
            int totalSeconds,
            Label recipeNameLabel,
            Label stepNameLabel,
            Label recipeTimeLabel,
            Label stepTimeLabel,
            Label stepNumberLabel,
            Label messageLabel,
            Panel progressPanel,
            Label statusLabel,
            Label mainStatusLabel)
        {
            ChamberProcessState state = new ChamberProcessState();
            state.PPID = ppid;
            state.TotalSeconds = Math.Max(3, totalSeconds);
            state.StepDurations = SplitStepDurations(state.TotalSeconds);
            state.RecipeNameLabel = recipeNameLabel;
            state.StepNameLabel = stepNameLabel;
            state.RecipeTimeLabel = recipeTimeLabel;
            state.StepTimeLabel = stepTimeLabel;
            state.StepNumberLabel = stepNumberLabel;
            state.MessageLabel = messageLabel;
            state.ProgressPanel = progressPanel;
            state.ProgressFillPanel = GetProgressFillPanel(progressPanel);
            state.StatusLabel = statusLabel;
            state.MainStatusLabel = mainStatusLabel;
            state.ElapsedSeconds = 0;
            return state;
        }

        private void chamberProcessTimer_Tick(object sender, EventArgs e)
        {
            if (isProcessRecipeRunning)
            {
                if (isProcessRecipePaused) return;

                string previousModule = waferSequencer.CurrentModule;
                bool wasProcessTimeStep = IsAutoProcessTimeStep();
                int previousElapsedSeconds = waferSequencer.CurrentElapsedSeconds;
                int previousTotalSeconds = waferSequencer.CurrentTotalSeconds;

                waferSequencer.Tick();

                if (wasProcessTimeStep)
                {
                    int elapsedSeconds = Math.Min(previousTotalSeconds, previousElapsedSeconds + 1);
                    UpdateAutoProcessTimeDisplay(previousModule, elapsedSeconds, previousTotalSeconds, elapsedSeconds >= previousTotalSeconds);
                }
                else
                {
                    UpdateAutoSequenceDisplay();
                }

                if (!waferSequencer.IsRunning)
                {
                    isProcessRecipeRunning = false;
                    chamberProcessTimer.Stop();

                    if (!waferSequencer.IsAborted)
                    {
                        isProcessCompleted = true;
                    }
                }

                return;
            }

            bool hasRunningProcess = false;
            hasRunningProcess |= AdvanceChamberProcess(pmaProcessState);
            hasRunningProcess |= AdvanceChamberProcess(pmbProcessState);
            hasRunningProcess |= AdvanceChamberProcess(pmcProcessState);

            if (!hasRunningProcess)
            {
                chamberProcessTimer.Stop();
            }
        }

        private Label GetModuleMessageLabel(string module)
        {
            string normalized = NormalizeModule(module);
            if (normalized == "PM B") return lbl_PMB_messagecontent;
            if (normalized == "PM C") return lbl_PMC_messagecontent;
            return lbl_PMA_messagecontent;
        }

        private Label GetModuleStepNumberLabel(string module)
        {
            string normalized = NormalizeModule(module);
            if (normalized == "PM B") return lbl_PMB_stepnum;
            if (normalized == "PM C") return lbl_PMC_stepnum;
            return lbl_PMA_stepnum;
        }

        private Label GetModuleRecipeTimeLabel(string module)
        {
            string normalized = NormalizeModule(module);
            if (normalized == "PM B") return lbl_PMB_recipetime;
            if (normalized == "PM C") return lbl_PMC_recipetime;
            return lbl_PMA_recipetime;
        }

        private Panel GetModuleProgressPanel(string module)
        {
            string normalized = NormalizeModule(module);
            if (normalized == "PM B") return pnl_PMB_progressbar;
            if (normalized == "PM C") return pnl_PMC_progressbar;
            return pnl_PMA_progressbar;
        }

        private void UpdateAutoSequenceDisplay()
        {
            string module = waferSequencer.CurrentModule;
            if (string.IsNullOrEmpty(module)) return;

            Label messageLabel = GetModuleMessageLabel(module);
            messageLabel.Text = waferSequencer.CurrentDescription;

            if (IsAutoProcessTimeStep())
            {
                UpdateAutoProcessTimeDisplay(module, waferSequencer.CurrentElapsedSeconds, waferSequencer.CurrentTotalSeconds, false);
            }
        }

        private bool IsAutoProcessTimeStep()
        {
            return waferSequencer.CurrentKind == WaferAutoSequencer.AutoStepKind.WaitElapsed &&
                string.Equals(waferSequencer.CurrentDescription, waferSequencer.CurrentModule + " process running", StringComparison.OrdinalIgnoreCase);
        }

        private void UpdateAutoProcessTimeDisplay(string module, int elapsedSeconds, int totalSeconds, bool isCompleted)
        {
            if (string.IsNullOrEmpty(module)) return;

            elapsedSeconds = Math.Max(0, Math.Min(elapsedSeconds, Math.Max(1, totalSeconds)));
            totalSeconds = Math.Max(1, totalSeconds);

            Label messageLabel = GetModuleMessageLabel(module);
            Label stepNumberLabel = GetModuleStepNumberLabel(module);
            Label recipeTimeLabel = GetModuleRecipeTimeLabel(module);
            Panel progressPanel = GetModuleProgressPanel(module);
            Label statusLabel = GetModuleStatusLabel(module);
            Label mainStatusLabel = GetModuleMainStatusLabel(module);

            int stepNumber = GetProcessTimeStepNumber(elapsedSeconds, totalSeconds);
            messageLabel.Text = isCompleted ? module + " process complete" : module + " process running";
            stepNumberLabel.Text = stepNumber + " / 3 step";
            recipeTimeLabel.Text = elapsedSeconds + " / " + totalSeconds;
            UpdateProgressBar(progressPanel, GetProgressFillPanel(progressPanel), elapsedSeconds, totalSeconds);

            if (statusLabel != null)
            {
                statusLabel.ForeColor = isCompleted ? Color.Silver : Color.Lime;
                SyncStatusLabel(statusLabel, mainStatusLabel);
            }
        }

        private int GetProcessTimeStepNumber(int elapsedSeconds, int totalSeconds)
        {
            if (elapsedSeconds <= 0) return 1;
            return Math.Max(1, Math.Min(3, (int)Math.Ceiling((double)elapsedSeconds * 3 / Math.Max(1, totalSeconds))));
        }

        private Label GetModuleStatusLabel(string module)
        {
            string normalized = NormalizeModule(module);
            if (normalized == "PM B") return lbl_PMB_Status;
            if (normalized == "PM C") return lbl_PMC_Status;
            return lbl_PMA_Status;
        }

        private Label GetModuleMainStatusLabel(string module)
        {
            string normalized = NormalizeModule(module);
            if (main == null) return null;
            if (normalized == "PM B") return main.lbl_PMBStatus;
            if (normalized == "PM C") return main.lbl_PMCStatus;
            return main.lbl_PMAStatus;
        }

        private bool AdvanceChamberProcess(ChamberProcessState state)
        {
            if (state == null || state.IsCompleted) return false;
            if (state.IsPaused) return true;

            state.ElapsedSeconds++;
            if (state.ElapsedSeconds >= state.TotalSeconds)
            {
                state.ElapsedSeconds = state.TotalSeconds;
                state.IsCompleted = true;
            }

            UpdateChamberProcessDisplay(state);
            return !state.IsCompleted;
        }

        private void UpdateChamberProcessDisplay(ChamberProcessState state)
        {
            if (state == null) return;

            StepProgress stepProgress = GetStepProgress(state);

            state.RecipeNameLabel.Text = state.PPID;
            state.StepNameLabel.Text = "Step " + stepProgress.StepNumber;
            state.RecipeTimeLabel.Text = state.ElapsedSeconds + " / " + state.TotalSeconds;
            state.StepTimeLabel.Text = stepProgress.StepElapsedSeconds + " / " + stepProgress.StepTotalSeconds;
            state.StepNumberLabel.Text = stepProgress.StepNumber + " / 3 step";
            state.MessageLabel.Text = GetChamberProcessMessage(state, stepProgress);
            state.StatusLabel.ForeColor = GetChamberProcessStatusColor(state);
            SyncStatusLabel(state.StatusLabel, state.MainStatusLabel);

            UpdateProgressBar(state.ProgressPanel, state.ProgressFillPanel, state.ElapsedSeconds, state.TotalSeconds);
        }

        private string GetChamberProcessMessage(ChamberProcessState state, StepProgress stepProgress)
        {
            if (state.IsAborted) return "Process aborted";
            if (state.IsPaused) return "Process paused";

            string message = state.IsCompleted
                ? "[" + stepProgress.StepNumber + "] step complete"
                : "[" + stepProgress.StepNumber + "] step wait";

            if (state.ProcessStepTotal > 0)
            {
                message = "Process " + state.ProcessStepNumber + "/" + state.ProcessStepTotal + " - " + message;
            }

            return message;
        }

        private Color GetChamberProcessStatusColor(ChamberProcessState state)
        {
            if (state.IsAborted) return Color.Red;
            if (state.IsPaused) return Color.Gold;
            return state.IsCompleted ? Color.Silver : Color.Lime;
        }

        private void SyncStatusLabel(Label sourceLabel, Label targetLabel)
        {
            if (sourceLabel == null || targetLabel == null) return;

            targetLabel.Text = sourceLabel.Text;
            if (main == null || !main.TrySyncPmStatusLabel(targetLabel, sourceLabel.ForeColor))
            {
                targetLabel.ForeColor = sourceLabel.ForeColor;
            }
        }

        private StepProgress GetStepProgress(ChamberProcessState state)
        {
            int accumulated = 0;

            for (int i = 0; i < state.StepDurations.Length; i++)
            {
                int stepDuration = state.StepDurations[i];
                if (state.ElapsedSeconds < accumulated + stepDuration || i == state.StepDurations.Length - 1)
                {
                    int stepElapsed = Math.Max(0, Math.Min(stepDuration, state.ElapsedSeconds - accumulated));
                    return new StepProgress(i + 1, stepElapsed, stepDuration);
                }

                accumulated += stepDuration;
            }

            return new StepProgress(3, state.StepDurations[2], state.StepDurations[2]);
        }

        private int[] SplitStepDurations(int totalSeconds)
        {
            int[] durations = new int[3];
            int baseDuration = totalSeconds / 3;
            int remainder = totalSeconds % 3;

            for (int i = 0; i < durations.Length; i++)
            {
                durations[i] = baseDuration;
                if (i < remainder)
                {
                    durations[i]++;
                }
            }

            return durations;
        }

        private int GetProcessTime(RecipeData recipe)
        {
            int processTime;
            string value = GetRecipeFieldValue(recipe, "Process Time");
            if (int.TryParse(value, out processTime) && processTime > 0)
            {
                return processTime;
            }

            return 3;
        }

        private string GetRecipeFieldValue(RecipeData recipe, string key)
        {
            if (recipe == null || recipe.Fields == null || !recipe.Fields.ContainsKey(key)) return string.Empty;
            return recipe.Fields[key];
        }

        private RecipeData ReadRecipe(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return null;
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                return jsonSerializer.Deserialize<RecipeData>(json);
            }
            catch
            {
                return null;
            }
        }

        private ProcessRecipeData ReadProcessRecipe(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return null;
                string json = File.ReadAllText(filePath, Encoding.UTF8);
                return jsonSerializer.Deserialize<ProcessRecipeData>(json);
            }
            catch
            {
                return null;
            }
        }

        private bool ValidateProcessRecipe(ProcessRecipeData recipe, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (recipe == null)
            {
                validationMessage = "Process Recipe file could not be loaded.";
                return false;
            }

            if (recipe.Steps == null || recipe.Steps.Count == 0)
            {
                validationMessage = "Process Recipe has no steps.";
                return false;
            }

            foreach (ProcessRecipeStep step in recipe.Steps)
            {
                if (step == null || step.Recipe == null)
                {
                    validationMessage = "Process Recipe has an empty step.";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(step.Recipe.Module) ||
                    string.IsNullOrWhiteSpace(step.Recipe.RecipePPID))
                {
                    validationMessage = "Process Recipe step is missing Module or Recipe PPID.";
                    return false;
                }

                if (step.Recipe.ProcessTime <= 0)
                {
                    validationMessage = "Process Recipe step has invalid Process Time.";
                    return false;
                }
            }

            return true;
        }

        private void EnsureProcessRecipeFolder()
        {
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

        private string NormalizeModule(string module)
        {
            if (string.Equals(module, "PM B", StringComparison.OrdinalIgnoreCase)) return "PM B";
            if (string.Equals(module, "PM C", StringComparison.OrdinalIgnoreCase)) return "PM C";
            return "PM A";
        }

        private bool PathsEqual(string firstPath, string secondPath)
        {
            if (string.IsNullOrWhiteSpace(firstPath) || string.IsNullOrWhiteSpace(secondPath)) return false;
            return string.Equals(
                Path.GetFullPath(firstPath).TrimEnd(Path.DirectorySeparatorChar),
                Path.GetFullPath(secondPath).TrimEnd(Path.DirectorySeparatorChar),
                StringComparison.OrdinalIgnoreCase);
        }

        private void InitializeProgressBar(Panel progressPanel)
        {
            progressPanel.BackColor = Color.White;
            progressPanel.Controls.Clear();

            Panel fillPanel = new Panel();
            fillPanel.BackColor = Color.RoyalBlue;
            fillPanel.Dock = DockStyle.Left;
            fillPanel.Width = 0;
            progressPanel.Controls.Add(fillPanel);
        }

        private Panel GetProgressFillPanel(Panel progressPanel)
        {
            if (progressPanel.Controls.Count == 0)
            {
                InitializeProgressBar(progressPanel);
            }

            return progressPanel.Controls[0] as Panel;
        }

        private void UpdateProgressBar(Panel progressPanel, Panel fillPanel, int elapsedSeconds, int totalSeconds)
        {
            if (progressPanel == null || fillPanel == null) return;

            double ratio = Math.Max(0, Math.Min(1, (double)elapsedSeconds / Math.Max(1, totalSeconds)));
            fillPanel.Width = (int)Math.Round(progressPanel.ClientSize.Width * ratio);
        }

        private void ResetAllChamberProcessDisplays()
        {
            pmaProcessState = null;
            pmbProcessState = null;
            pmcProcessState = null;

            ResetChamberProcessDisplay(
                lbl_PMA_recipename,
                lbl_PMA_stepname,
                lbl_PMA_recipetime,
                lbl_PMA_steptime,
                lbl_PMA_stepnum,
                lbl_PMA_messagecontent,
                pnl_PMA_progressbar,
                lbl_PMA_Status,
                main.lbl_PMAStatus);

            ResetChamberProcessDisplay(
                lbl_PMB_recipename,
                lbl_PMB_stepname,
                lbl_PMB_recipetime,
                lbl_PMB_steptime,
                lbl_PMB_stepnum,
                lbl_PMB_messagecontent,
                pnl_PMB_progressbar,
                lbl_PMB_Status,
                main.lbl_PMBStatus);

            ResetChamberProcessDisplay(
                lbl_PMC_recipename,
                lbl_PMC_stepname,
                lbl_PMC_recipetime,
                lbl_PMC_steptime,
                lbl_PMC_stepnum,
                lbl_PMC_messagecontent,
                pnl_PMC_progressbar,
                lbl_PMC_Status,
                main.lbl_PMCStatus);
        }

        private void ResetChamberProcessDisplay(
            Label recipeNameLabel,
            Label stepNameLabel,
            Label recipeTimeLabel,
            Label stepTimeLabel,
            Label stepNumberLabel,
            Label messageLabel,
            Panel progressPanel,
            Label statusLabel,
            Label mainStatusLabel)
        {
            recipeNameLabel.Text = string.Empty;
            stepNameLabel.Text = string.Empty;
            recipeTimeLabel.Text = "0 / 0";
            stepTimeLabel.Text = "0 / 0";
            stepNumberLabel.Text = "0 / 0 step";
            messageLabel.Text = string.Empty;
            statusLabel.ForeColor = Color.Silver;
            SyncStatusLabel(statusLabel, mainStatusLabel);
            UpdateProgressBar(progressPanel, GetProgressFillPanel(progressPanel), 0, 1);
        }

        private void SetPauseStateForActiveProcesses(bool isPaused)
        {
            SetPauseState(pmaProcessState, isPaused);
            SetPauseState(pmbProcessState, isPaused);
            SetPauseState(pmcProcessState, isPaused);
        }

        private void SetPauseState(ChamberProcessState state, bool isPaused)
        {
            if (state == null || state.IsCompleted || state.IsAborted) return;

            state.IsPaused = isPaused;
            UpdateChamberProcessDisplay(state);
        }

        private bool HasPausedProcess()
        {
            return IsPausedProcess(pmaProcessState) || IsPausedProcess(pmbProcessState) || IsPausedProcess(pmcProcessState);
        }

        private bool IsPausedProcess(ChamberProcessState state)
        {
            return state != null && state.IsPaused && !state.IsCompleted && !state.IsAborted;
        }

        private void AbortActiveProcesses()
        {
            AbortProcessState(pmaProcessState);
            AbortProcessState(pmbProcessState);
            AbortProcessState(pmcProcessState);
        }

        private void AbortProcessState(ChamberProcessState state)
        {
            if (state == null || state.IsCompleted) return;

            state.IsPaused = false;
            state.IsAborted = true;
            UpdateChamberProcessDisplay(state);
        }

        private class ChamberProcessState
        {
            public string PPID { get; set; }
            public string Module { get; set; }
            public string RecipeName { get; set; }
            public int ProcessStepNumber { get; set; }
            public int ProcessStepTotal { get; set; }
            public int TotalSeconds { get; set; }
            public int ElapsedSeconds { get; set; }
            public int[] StepDurations { get; set; }
            public bool IsCompleted { get; set; }
            public bool IsPaused { get; set; }
            public bool IsAborted { get; set; }
            public Label RecipeNameLabel { get; set; }
            public Label StepNameLabel { get; set; }
            public Label RecipeTimeLabel { get; set; }
            public Label StepTimeLabel { get; set; }
            public Label StepNumberLabel { get; set; }
            public Label MessageLabel { get; set; }
            public Label StatusLabel { get; set; }
            public Label MainStatusLabel { get; set; }
            public Panel ProgressPanel { get; set; }
            public Panel ProgressFillPanel { get; set; }
        }

        private class StepProgress
        {
            public StepProgress(int stepNumber, int stepElapsedSeconds, int stepTotalSeconds)
            {
                StepNumber = stepNumber;
                StepElapsedSeconds = stepElapsedSeconds;
                StepTotalSeconds = stepTotalSeconds;
            }

            public int StepNumber { get; private set; }
            public int StepElapsedSeconds { get; private set; }
            public int StepTotalSeconds { get; private set; }
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

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            isProcessCompleted = false;

            LoadProcessRecipeSelector();

            if (string.IsNullOrWhiteSpace(selectedProcessRecipePath) || !File.Exists(selectedProcessRecipePath))
            {
                MessageBox.Show("Start할 Process Recipe를 선택하세요.", "Process Recipe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (isProcessRecipeRunning || chamberProcessTimer.Enabled)
            {
                DialogResult result = MessageBox.Show(
                    "진행 중인 공정을 중단하고 선택한 Process Recipe를 시작할까요?",
                    "Process Recipe Start",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
            }

            ProcessRecipeData recipe = ReadProcessRecipe(selectedProcessRecipePath);
            string validationMessage;
            if (!ValidateProcessRecipe(recipe, out validationMessage))
            {
                MessageBox.Show(validationMessage, "Process Recipe", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            chamberProcessTimer.Stop();
            ResetAllChamberProcessDisplays();

            currentProcessRecipe = recipe;
            isProcessRecipeRunning = true;
            isProcessRecipePaused = false;

            List<ChamberRecipeSelection> moduleSteps = recipe.Steps.Select(s => s.Recipe).ToList();
            ResetAutoWaferDisplay();
            waferSequencer.Start(AutoSequenceBuilder.Build(main, moduleSteps, SetFoupSlotState, SetModuleWaferState));
            UpdateAutoSequenceDisplay();
            chamberProcessTimer.Start();
        }

        private void btn_Pause_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (!chamberProcessTimer.Enabled) return;

            isProcessRecipePaused = isProcessRecipeRunning;
            SetPauseStateForActiveProcesses(true);
            chamberProcessTimer.Stop();
        }

        private void btn_Continue_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (!isProcessRecipePaused && !HasPausedProcess()) return;

            isProcessRecipePaused = false;
            SetPauseStateForActiveProcesses(false);
            chamberProcessTimer.Start();
        }

        private void btn_Abort_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (!isProcessRecipeRunning && !chamberProcessTimer.Enabled && !HasPausedProcess()) return;

            DialogResult result = MessageBox.Show(
                "진행 중인 공정을 Abort 하시겠습니까?",
                "Process Recipe Abort",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (result != DialogResult.OK) return;

            chamberProcessTimer.Stop();
            AbortActiveProcesses();
            isUserAbortRecoveryRunning = true;

            if (isProcessRecipeRunning)
            {
                waferSequencer.Abort("사용자 Abort 요청");
                UpdateAutoSequenceDisplay();
                isProcessRecipeRunning = false;
            }

            isProcessRecipePaused = false;
            currentProcessRecipe = null;
            main.SafeAbortAndHome();
            isUserAbortRecoveryRunning = false;
        }

        private class ProcessRecipeComboItem
        {
            public ProcessRecipeComboItem(string filePath)
            {
                FilePath = filePath;
            }

            public string FilePath { get; private set; }

            public override string ToString()
            {
                return Path.GetFileNameWithoutExtension(FilePath);
            }
        }

        private class ProcessRecipeData
        {
            public string RecipeName { get; set; }
            public string PPID { get; set; }
            public string Description { get; set; }
            public int TotalProcessTime { get; set; }
            public string CreatedBy { get; set; }
            public string ModifiedDate { get; set; }
            public List<ProcessRecipeStep> Steps { get; set; }
        }

        private class ProcessRecipeStep
        {
            public int StepNo { get; set; }
            public ChamberRecipeSelection Recipe { get; set; }
        }

        public void UpdateControlButtons()
        {
            if (main == null) return;

            // 1. 초기화 중이거나 연결 해제 상태인 경우 모든 버튼 비활성화
            if (isInitializing || !main.isConnect || main.EtherCAT_M == null)
            {
                btn_Start.Enabled = false;
                btn_Pause.Enabled = false;
                btn_Continue.Enabled = false;
                btn_Abort.Enabled = false;
                btn_Initialize.Enabled = !isInitializing && main.isConnect; // 초기화 중이 아니고 연결된 경우에만 활성화 가능
                return;
            }

            // 2. 센서 상태 읽기 및 범위 체크 (10000 ~ -10000)
            long udPos = 0;
            long lrPos = 0;
            bool hasUd = long.TryParse(main.EtherCAT_M.Axis1_is_PosData(), out udPos);
            bool hasLr = long.TryParse(main.EtherCAT_M.Axis2_is_PosData(), out lrPos);

            bool isUdOutOfRange = !hasUd || udPos < -10000 || udPos > 10000;
            bool isLrOutOfRange = !hasLr || lrPos < -10000 || lrPos > 10000;
            bool isCylinderNotSafe = main.IsCylinderForward() || !main.IsCylinderBack();
            bool isAnyDoorOpen = main.IsChamberDoorOpen("PM A") || main.IsChamberDoorOpen("PM B") || main.IsChamberDoorOpen("PM C");

            // 3. 가동 상태에 따른 활성화 처리
            if (isProcessRecipeRunning)
            {
                // 가동 중 상태
                btn_Start.Enabled = false;
                btn_Initialize.Enabled = false;

                if (isProcessRecipePaused)
                {
                    btn_Pause.Enabled = false;
                    btn_Continue.Enabled = true;
                    btn_Abort.Enabled = true;
                }
                else
                {
                    btn_Pause.Enabled = true;
                    btn_Continue.Enabled = false;
                    btn_Abort.Enabled = true;
                }
            }
            else
            {
                // 정지/대기 상태
                btn_Pause.Enabled = false;
                btn_Continue.Enabled = false;
                btn_Abort.Enabled = false;
                btn_Initialize.Enabled = true; // 정상 대기 상태에서도 임의로 초기화(원점 복귀)를 누를 수 있도록 활성화
                btn_Start.Enabled = true;      // 대기 상태에서는 항상 Start와 Initialize가 활성화되며, 시작 클릭 시점에 안전 검증을 수행합니다.
            }
        }

        private async void btn_Initialize_Click(object sender, EventArgs e)
        {
            if (isInitializing) return;

            isProcessCompleted = false;
            isInitializing = true;
            UpdateControlButtons(); // 모든 버튼 즉시 비활성화
            ResetAllChamberProcessDisplays(); // PM 정보 표시 패널 디폴트로 초기화

            try
            {
                main.WriteSystemLog("INFO", "장비 초기화(Initialize) 시퀀스 시작");

                // 1단계: 무조건 실린더 후진 먼저 체크하고 후진시키기
                if (!main.IsCylinderBack())
                {
                    main.WriteSystemLog("WARN", "장비 초기화: 실린더가 후진상태가 아닙니다. 후진을 시작합니다.");
                    main.MoveCylinderBack();

                    int cylinderTimeoutMs = 10000;
                    int cylinderElapsedMs = 0;
                    while (!main.IsCylinderBack() && cylinderElapsedMs < cylinderTimeoutMs)
                    {
                        await Task.Delay(100);
                        cylinderElapsedMs += 100;
                    }

                    if (!main.IsCylinderBack())
                    {
                        main.WriteSystemLog("ERROR", "장비 초기화 실패: 실린더 후진 센서 확인 불가");
                        MessageBox.Show("실린더 후진 확인에 실패했습니다. 실린더 상태 및 센서를 확인하십시오.", "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                main.WriteSystemLog("INFO", "장비 초기화: 실린더 후진 확인 완료");

                // 2단계: 상하 원점복귀, 좌우 원점복귀
                main.WriteSystemLog("INFO", "장비 초기화: 축 원점복귀(UD & LR)를 시작합니다.");
                main.HomeAxis1UD();
                main.HomeAxis2LR();

                int axisTimeoutMs = 60000;
                int axisElapsedMs = 0;
                while (axisElapsedMs < axisTimeoutMs)
                {
                    bool udAtHome = main.IsAxis1AtPosition(0, 10000);
                    bool lrAtHome = main.IsAxis2AtPosition(0, 10000);

                    if (udAtHome && lrAtHome) break;

                    await Task.Delay(200);
                    axisElapsedMs += 200;
                }

                if (!main.IsAxis1AtPosition(0, 10000) || !main.IsAxis2AtPosition(0, 10000))
                {
                    main.WriteSystemLog("ERROR", "장비 초기화 실패: 축 원점복귀(Homing) 확인 불가 또는 타임아웃");
                    MessageBox.Show("축 원점복귀에 실패했습니다. 서보 드라이브 상태를 확인하십시오.", "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                main.WriteSystemLog("INFO", "장비 초기화: 축 원점복귀 확인 완료");

                // 3단계: PM 문(Chamber Door) 열림 여부 센서로 체크해서 닫기
                main.WriteSystemLog("INFO", "장비 초기화: 챔버 도어 상태 검사 및 닫기 시작");
                bool doorCloseTriggered = false;
                foreach (string pm in new[] { "PM A", "PM B", "PM C" })
                {
                    if (main.IsChamberDoorOpen(pm))
                    {
                        main.WriteSystemLog("WARN", $"장비 초기화: {pm} 도어가 열려있습니다. 도어를 닫습니다.");
                        main.CloseChamberDoor(pm);
                        doorCloseTriggered = true;
                    }
                }

                if (doorCloseTriggered)
                {
                    int doorTimeoutMs = 30000;
                    int doorElapsedMs = 0;
                    while (doorElapsedMs < doorTimeoutMs)
                    {
                        bool allClosed = true;
                        foreach (string pm in new[] { "PM A", "PM B", "PM C" })
                        {
                            if (main.IsChamberDoorOpen(pm) || !main.IsChamberDoorClosed(pm))
                            {
                                allClosed = false;
                            }
                        }

                        if (allClosed) break;

                        await Task.Delay(200);
                        doorElapsedMs += 200;
                    }

                    bool finalClosed = true;
                    foreach (string pm in new[] { "PM A", "PM B", "PM C" })
                    {
                        if (!main.IsChamberDoorClosed(pm))
                        {
                            finalClosed = false;
                            main.WriteSystemLog("ERROR", $"장비 초기화 실패: {pm} 도어 닫힘 상태가 확인되지 않았습니다.");
                        }
                    }

                    if (!finalClosed)
                    {
                        MessageBox.Show("챔버 도어가 닫히지 않아 초기화를 중단합니다.", "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                main.WriteSystemLog("INFO", "장비 초기화 완료: 장비가 정상 정렬 상태입니다.");
            }
            catch (Exception ex)
            {
                main.WriteSystemLog("ERROR", $"장비 초기화 중 예외 오류 발생: {ex.Message}");
                MessageBox.Show($"초기화 중 오류가 발생했습니다:\r\n{ex.Message}", "Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isInitializing = false;
                UpdateControlButtons(); // 최종 버튼 상태로 갱신
            }
        }
    }
}
