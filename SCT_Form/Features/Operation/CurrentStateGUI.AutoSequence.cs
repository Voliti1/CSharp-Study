using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SCT_Form
{
    // "메인 프로세스" 실제 실행 경로: Start/Pause/Continue/Abort 버튼과, 1초마다 도는
    // chamberProcessTimer가 WaferAutoSequencer를 진행시키는 부분.
    // 흐름: btn_Start_Click가 AutoSequenceBuilder.Build()로 스텝 리스트를 만들어
    // waferSequencer.Start()에 넘김 → chamberProcessTimer_Tick이 매초 waferSequencer.Tick()을
    // 호출해 진행시킴 → UpdateAutoSequenceDisplay/UpdateAutoProcessTimeDisplay가 PM 정보
    // 패널(메시지/스텝 번호/Time/진행률 바)을 그 진행 상황에 맞춰 갱신.
    //
    // 참고(작업 시 확인 필요): 현재 5개 FOUP 슬롯을 전부 도는 것은 AutoSequenceBuilder.Build()
    // 안에서 한 번에 끝나는 하나의 긴 스텝 리스트로 처리된다. 이 파일 쪽에서 "지금 몇 번째
    // 웨이퍼(슬롯)를 처리 중인지"를 화면에 보여주는 표시는 아직 없다 - UpdateAutoProcessTimeDisplay는
    // PM 이름과 elapsed/total 초만 보여준다. 슬롯 진행 상황을 PM 정보창에 노출하려면 이 부분에
    // 손을 대야 한다.
    public partial class CurrentStateGUI
    {
        // chamberProcessTimer(1초 tick)의 실제 핸들러. 두 가지 경로로 갈라진다:
        // 1) isProcessRecipeRunning=true면 real 자동 시퀀스(WaferAutoSequencer) 진행 - 이게
        //    Start 버튼으로 시작한 "메인 프로세스"다.
        // 2) 아니면 PM Setting 버튼으로 미리보기 중인 시뮬레이션(AdvanceChamberProcess, 실제
        //    하드웨어를 움직이지 않는 진행률 표시 전용)을 진행시킨다.
        private void chamberProcessTimer_Tick(object sender, EventArgs e)
        {
            if (isProcessRecipeRunning)
            {
                if (isProcessRecipePaused) return;

                // wasProcessTimeStep/previousModule/previousElapsedSeconds/previousTotalSeconds를
                // Tick() 호출 "전"에 미리 캡처해두는 이유: Tick() 안에서 현재 스텝이 완료되어
                // 다음 스텝(로봇 언로딩 등)으로 즉시 넘어가 버리면, waferSequencer.Current*
                // 프로퍼티가 이미 다음 스텝 것으로 바뀌어 있어서 "방금 끝난 PM 프로세스 스텝"의
                // 최종 100% 상태(elapsedSeconds>=totalSeconds)를 표시할 기회를 놓치기 때문이다.
                string previousModule = waferSequencer.CurrentModule;
                bool wasProcessTimeStep = IsAutoProcessTimeStep();
                int previousElapsedSeconds = waferSequencer.CurrentElapsedSeconds;
                int previousTotalSeconds = waferSequencer.CurrentTotalSeconds;

                waferSequencer.Tick();

                if (wasProcessTimeStep)
                {
                    // PM 정보창 Time이 "종료"로 표시되는 시점(elapsedSeconds>=totalSeconds)이
                    // 바로 여기다. 같은 tick 안에서 AutoSequenceBuilder.AddProcessWait의
                    // "process complete" 액션(BlinkChamberLamp 호출)도 waferSequencer.Tick()을
                    // 통해 이미 실행된 뒤이므로, 이 표시 갱신과 램프 깜빡임 시작은 항상 같은 tick에 맞물린다.
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

        // 자동 시퀀스가 축 이동/도어/센서 대기 등 "PM 공정 진행 시간이 아닌" 스텝을 지나는 동안
        // 메시지 라벨만 현재 스텝 설명(Description)으로 갱신한다. 공정 진행 시간 스텝이면
        // UpdateAutoProcessTimeDisplay로 위임해서 시간/스텝번호/진행률 바까지 갱신한다.
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

        // 현재 waferSequencer가 가리키는 스텝이 AutoSequenceBuilder.AddProcessWait가 만든
        // "PM 공정 진행 시간" WaitElapsed 스텝인지 판별한다(Description 문자열로 식별).
        private bool IsAutoProcessTimeStep()
        {
            return waferSequencer.CurrentKind == WaferAutoSequencer.AutoStepKind.WaitElapsed &&
                string.Equals(waferSequencer.CurrentDescription, waferSequencer.CurrentModule + " process running", StringComparison.OrdinalIgnoreCase);
        }

        // PM 정보 패널의 메시지/스텝번호(1~3)/Time(elapsed / total)/진행률 바/상태 라벨 색상을
        // 한 번에 갱신한다. isCompleted가 true면(공정 진행 시간이 끝에 도달했으면) 메시지를
        // "process complete"로 바꾸고 상태색을 Silver로 내린다 - 이때가 챔버등 깜빡임이 시작되는
        // 시점과 같은 tick이다(호출부인 chamberProcessTimer_Tick 주석 참고).
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

        // 공정 진행 시간을 3등분해서 "지금 1/2/3 스텝 중 어디쯤인지"를 표시용으로만 계산한다
        // (실제 자동 시퀀스에는 이런 3단계 구분이 없고, PM 정보 패널 표시 형식을 예전 시뮬레이션
        // 화면과 맞추기 위한 것이다).
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

        // 자동 시퀀스 시작점. 선택된 Process Recipe를 읽고 검증한 뒤,
        // AutoSequenceBuilder.Build()로 (FOUP A 슬롯 1~5 x 선택된 PM 경로) 전체 스텝 리스트를
        // 만들어 waferSequencer.Start()에 넘기고 chamberProcessTimer를 돌리기 시작한다.
        // 이미 뭔가 실행 중이면 사용자에게 확인 후 중단하고 새로 시작한다.
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

        // chamberProcessTimer만 멈춘다 - waferSequencer 자체는 상태를 그대로 들고 있으므로,
        // 다시 Start(실은 Continue)하면 멈췄던 스텝부터 이어서 진행된다. 진행 중이던 하드웨어
        // 동작(축 이동 등)을 되돌리지는 않는다.
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

        // 사용자가 명시적으로 중단을 요청한 경로. waferSequencer.Abort()로 자동 시퀀스를 멈추고,
        // main.SafeAbortAndHome()으로 흡착/램프를 끄고 실린더 후진 확인 후 원점복귀까지 실행한다.
        // isUserAbortRecoveryRunning 플래그는 waferSequencer.Aborted 이벤트 핸들러(생성자에서 등록)가
        // 이 경로로 인한 Abort일 때 SafeAbortAndHome을 중복 호출하지 않도록 구분하는 용도다.
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
    }
}
