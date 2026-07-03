using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

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
        private int currentProcessStepIndex = -1;
        private bool isProcessRecipeRunning;
        private bool isProcessRecipePaused;
        private ChamberProcessState pmaProcessState;
        private ChamberProcessState pmbProcessState;
        private ChamberProcessState pmcProcessState;

        public CurrentStateGUI(MainGUI mainGUI)
        {
            InitializeComponent();
            main = mainGUI;

            InitializeProcessRecipeSelector();
            HidePmRecipeSettingButtons();

            InitializeProgressBar(pnl_PMA_progressbar);
            InitializeProgressBar(pnl_PMB_progressbar);
            InitializeProgressBar(pnl_PMC_progressbar);

            chamberProcessTimer.Interval = 1000;
            chamberProcessTimer.Tick += chamberProcessTimer_Tick;
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

        private ChamberProcessState CreateProcessStateForModule(string module, string ppid, int totalSeconds)
        {
            string normalizedModule = NormalizeModule(module);

            if (normalizedModule == "PM A")
            {
                return CreateProcessState(
                    ppid,
                    totalSeconds,
                    lbl_PMA_recipename,
                    lbl_PMA_stepname,
                    lbl_PMA_recipetime,
                    lbl_PMA_steptime,
                    lbl_PMA_stepnum,
                    lbl_PMA_messagecontent,
                    pnl_PMA_progressbar,
                    lbl_PMA_Status,
                    main.lbl_PMAStatus);
            }

            if (normalizedModule == "PM B")
            {
                return CreateProcessState(
                    ppid,
                    totalSeconds,
                    lbl_PMB_recipename,
                    lbl_PMB_stepname,
                    lbl_PMB_recipetime,
                    lbl_PMB_steptime,
                    lbl_PMB_stepnum,
                    lbl_PMB_messagecontent,
                    pnl_PMB_progressbar,
                    lbl_PMB_Status,
                    main.lbl_PMBStatus);
            }

            if (normalizedModule == "PM C")
            {
                return CreateProcessState(
                    ppid,
                    totalSeconds,
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

            return null;
        }

        private void AssignProcessState(string module, ChamberProcessState state)
        {
            string normalizedModule = NormalizeModule(module);
            if (normalizedModule == "PM A")
            {
                pmaProcessState = state;
            }
            else if (normalizedModule == "PM B")
            {
                pmbProcessState = state;
            }
            else if (normalizedModule == "PM C")
            {
                pmcProcessState = state;
            }
        }

        private ChamberProcessState GetProcessState(string module)
        {
            string normalizedModule = NormalizeModule(module);
            if (normalizedModule == "PM A") return pmaProcessState;
            if (normalizedModule == "PM B") return pmbProcessState;
            if (normalizedModule == "PM C") return pmcProcessState;
            return null;
        }

        private void chamberProcessTimer_Tick(object sender, EventArgs e)
        {
            if (isProcessRecipeRunning)
            {
                if (isProcessRecipePaused) return;

                if (!AdvanceCurrentProcessRecipeStep())
                {
                    chamberProcessTimer.Stop();
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

        private bool AdvanceCurrentProcessRecipeStep()
        {
            ChamberProcessState state = GetCurrentProcessRecipeState();
            if (state == null)
            {
                return StartCurrentProcessRecipeStep();
            }

            if (AdvanceChamberProcess(state))
            {
                return true;
            }

            currentProcessStepIndex++;
            return StartCurrentProcessRecipeStep();
        }

        private bool StartCurrentProcessRecipeStep()
        {
            if (currentProcessRecipe == null || currentProcessRecipe.Steps == null ||
                currentProcessStepIndex < 0 || currentProcessStepIndex >= currentProcessRecipe.Steps.Count)
            {
                CompleteProcessRecipeRun();
                return false;
            }

            ProcessRecipeStep step = currentProcessRecipe.Steps[currentProcessStepIndex];
            if (step == null || step.Recipe == null)
            {
                currentProcessStepIndex++;
                return StartCurrentProcessRecipeStep();
            }

            ChamberRecipeSelection recipe = step.Recipe;
            ChamberProcessState state = CreateProcessStateForModule(recipe.Module, recipe.RecipePPID, recipe.ProcessTime);
            if (state == null)
            {
                currentProcessStepIndex++;
                return StartCurrentProcessRecipeStep();
            }

            state.Module = NormalizeModule(recipe.Module);
            state.RecipeName = recipe.RecipeName;
            state.ProcessStepNumber = currentProcessStepIndex + 1;
            state.ProcessStepTotal = currentProcessRecipe.Steps.Count;
            AssignProcessState(state.Module, state);
            UpdateChamberProcessDisplay(state);
            return true;
        }

        private void CompleteProcessRecipeRun()
        {
            isProcessRecipeRunning = false;
            isProcessRecipePaused = false;
            currentProcessRecipe = null;
            currentProcessStepIndex = -1;
        }

        private ChamberProcessState GetCurrentProcessRecipeState()
        {
            if (currentProcessRecipe == null || currentProcessRecipe.Steps == null ||
                currentProcessStepIndex < 0 || currentProcessStepIndex >= currentProcessRecipe.Steps.Count)
            {
                return null;
            }

            ProcessRecipeStep step = currentProcessRecipe.Steps[currentProcessStepIndex];
            if (step == null || step.Recipe == null) return null;
            return GetProcessState(NormalizeModule(step.Recipe.Module));
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
            targetLabel.ForeColor = sourceLabel.ForeColor;
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
            currentProcessStepIndex = 0;
            isProcessRecipeRunning = true;
            isProcessRecipePaused = false;

            if (StartCurrentProcessRecipeStep())
            {
                chamberProcessTimer.Start();
            }

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
            CompleteProcessRecipeRun();
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
    }
}
