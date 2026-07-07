using System;
using System.Drawing;
using System.Windows.Forms;

namespace SCT_Form
{
    // PM A/B/C "Setting" 버튼으로 개별 챔버에 레시피를 지정했을 때, 실제 장비를 움직이지
    // 않고 PM 정보 패널(진행률 바/스텝/시간)만 3등분 시뮬레이션으로 채워 보여주는 미리보기 경로.
    // chamberProcessTimer_Tick의 두 분기(자동 시퀀스 vs 이 시뮬레이션) 중 후자가 여기서
    // AdvanceChamberProcess를 통해 매초 진행된다. Start 버튼을 눌러 실제 자동 시퀀스가
    // 시작되면(isProcessRecipeRunning=true) 이 시뮬레이션 경로는 더 이상 진행되지 않는다.
    public partial class CurrentStateGUI
    {
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
    }
}
