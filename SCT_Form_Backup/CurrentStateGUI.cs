using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        private ChamberProcessState pmaProcessState;
        private ChamberProcessState pmbProcessState;
        private ChamberProcessState pmcProcessState;

        public CurrentStateGUI(MainGUI mainGUI)
        {
            InitializeComponent();
            main = mainGUI;

            btn_PMA_Setting.Click += btn_PMA_Setting_Click;
            btn_PMB_Setting.Click += btn_PMB_Setting_Click;
            btn_PMC_Setting.Click += btn_PMC_Setting_Click;

            InitializeProgressBar(pnl_PMA_progressbar);
            InitializeProgressBar(pnl_PMB_progressbar);
            InitializeProgressBar(pnl_PMC_progressbar);

            chamberProcessTimer.Interval = 1000;
            chamberProcessTimer.Tick += chamberProcessTimer_Tick;
        }

        private void btn_FOUPA_Full_Click(object sender, EventArgs e)
        {
            SetFoupAColor(FoupFullColor);
        }

        private void btn_FOUPA_Empty_Click(object sender, EventArgs e)
        {
            SetFoupAColor(FoupEmptyColor);
        }

        private void btn_FOUPB_Full_Click(object sender, EventArgs e)
        {
            SetFoupBColor(FoupFullColor);
        }

        private void btn_FOUPB_Empty_Click(object sender, EventArgs e)
        {
            SetFoupBColor(FoupEmptyColor);
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
                    lbl_PMA_Status);
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
                    lbl_PMB_Status);
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
                    lbl_PMC_Status);
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
            Label statusLabel)
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
            state.ElapsedSeconds = 0;
            return state;
        }

        private void chamberProcessTimer_Tick(object sender, EventArgs e)
        {
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
            state.MessageLabel.Text = state.IsCompleted
                ? "[3] step complete"
                : "[" + stepProgress.StepNumber + "] step wait";
            state.StatusLabel.ForeColor = state.IsCompleted ? Color.Silver : Color.Lime;

            UpdateProgressBar(state.ProgressPanel, state.ProgressFillPanel, state.ElapsedSeconds, state.TotalSeconds);
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

        private class ChamberProcessState
        {
            public string PPID { get; set; }
            public int TotalSeconds { get; set; }
            public int ElapsedSeconds { get; set; }
            public int[] StepDurations { get; set; }
            public bool IsCompleted { get; set; }
            public Label RecipeNameLabel { get; set; }
            public Label StepNameLabel { get; set; }
            public Label RecipeTimeLabel { get; set; }
            public Label StepTimeLabel { get; set; }
            public Label StepNumberLabel { get; set; }
            public Label MessageLabel { get; set; }
            public Label StatusLabel { get; set; }
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
    }
}
