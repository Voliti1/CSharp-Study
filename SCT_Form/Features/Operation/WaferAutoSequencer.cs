using System;
using System.Collections.Generic;

namespace SCT_Form
{
    // AutoSequenceBuilder가 만든 AutoStep 리스트를 처음부터 끝까지 실행하는 상태 머신.
    // CurrentStateGUI의 chamberProcessTimer(1초 tick)가 Tick()을 매초 호출해서 진행시킨다.
    // 하드웨어를 직접 다루지 않고(Execute 델리게이트로 위임), "지금 몇 번째 스텝인지 / 그 스텝이
    // 끝났는지"만 관리한다 - 그래서 이 클래스 자체는 어떤 PM/축/센서인지 전혀 몰라도 된다.
    internal class WaferAutoSequencer
    {
        // Action: 그 자리에서 즉시 실행하고 바로 다음 스텝으로 넘어가는 하드웨어 동작(문 열기, 램프 on 등).
        // WaitSensor: IsSatisfied()가 true가 될 때까지 매 tick 확인하며 대기, TimeoutSeconds 넘으면 Abort.
        // WaitElapsed: TotalSeconds만큼 그냥 시간이 흐르기를 대기(센서 확인 없음, PM 공정 진행 시간 등).
        internal enum AutoStepKind { Action, WaitSensor, WaitElapsed }

        internal class AutoStep
        {
            public AutoStepKind Kind;
            public string Module;
            public string Description;
            public Action Execute;
            public Func<bool> IsSatisfied;
            public int TimeoutSeconds;
            public int TotalSeconds;
            public int ElapsedSeconds;
        }

        private List<AutoStep> steps = new List<AutoStep>();
        private int currentIndex = -1;
        private int sensorWaitElapsedSeconds;

        internal bool IsRunning { get; private set; }
        internal bool IsAborted { get; private set; }

        // Abort() 호출 시 발생. CurrentStateGUI가 이 이벤트를 구독해서 사용자에게 중단 사유를 보여준다.
        internal event Action<string> Aborted;

        internal string CurrentModule => IsCurrentStepValid ? steps[currentIndex].Module : null;
        internal string CurrentDescription => IsCurrentStepValid ? steps[currentIndex].Description : string.Empty;
        internal AutoStepKind CurrentKind => IsCurrentStepValid ? steps[currentIndex].Kind : AutoStepKind.Action;
        internal int CurrentElapsedSeconds => IsCurrentStepValid ? steps[currentIndex].ElapsedSeconds : 0;
        internal int CurrentTotalSeconds => IsCurrentStepValid ? steps[currentIndex].TotalSeconds : 0;
        internal int CurrentStepIndex => currentIndex + 1;
        internal int TotalStepCount => steps.Count;

        private bool IsCurrentStepValid => currentIndex >= 0 && currentIndex < steps.Count;

        // 새 스텝 리스트로 시퀀스를 시작한다. 맨 앞에 연속으로 있는 Action 스텝들은
        // (WaitSensor/WaitElapsed를 만날 때까지) 이 호출 안에서 즉시 다 실행해버린다.
        internal void Start(List<AutoStep> builtSteps)
        {
            steps = builtSteps ?? new List<AutoStep>();
            currentIndex = -1;
            sensorWaitElapsedSeconds = 0;
            IsAborted = false;
            IsRunning = steps.Count > 0;

            if (IsRunning)
            {
                AdvanceToNextExecutableStep();
            }
        }

        // 1초마다 호출됨(CurrentStateGUI.chamberProcessTimer). 현재 스텝이:
        // - WaitElapsed면 경과초를 늘리고, 목표 시간에 도달하면 다음 스텝으로 진행.
        // - WaitSensor면 조건을 확인해서 만족되면 다음 스텝으로 진행하고,
        //   아니면 대기초를 늘려서 타임아웃 넘으면 Abort() 처리.
        internal void Tick()
        {
            if (!IsRunning || !IsCurrentStepValid) return;

            AutoStep step = steps[currentIndex];

            switch (step.Kind)
            {
                case AutoStepKind.WaitElapsed:
                    step.ElapsedSeconds++;
                    if (step.ElapsedSeconds >= step.TotalSeconds)
                    {
                        AdvanceToNextExecutableStep();
                    }
                    return;

                case AutoStepKind.WaitSensor:
                    if (step.IsSatisfied == null)
                    {
                        throw new InvalidOperationException("AutoStep '" + step.Description + "' is a WaitSensor step but has no IsSatisfied delegate.");
                    }

                    if (step.IsSatisfied())
                    {
                        AdvanceToNextExecutableStep();
                        return;
                    }

                    sensorWaitElapsedSeconds++;
                    if (sensorWaitElapsedSeconds >= step.TimeoutSeconds)
                    {
                        Abort(step.Description + " sensor wait timeout after " + step.TimeoutSeconds + " sec.");
                    }
                    return;

                default:
                    throw new InvalidOperationException("Tick() reached an Action step (" + step.Description + "); Action steps should never be the current step.");
            }
        }

        // 시퀀스를 즉시 중단한다. 하드웨어를 원상복귀시키지는 않는다 - 그건 호출 쪽(예:
        // MainGUI.SafeAbortAndHome)이 별도로 처리한다. Aborted 이벤트로 사유만 알린다.
        internal void Abort(string reason)
        {
            if (!IsRunning) return;

            IsRunning = false;
            IsAborted = true;
            Aborted?.Invoke(reason);
        }

        // 다음 인덱스로 넘어간 뒤, 그 자리부터 연속으로 있는 Action 스텝을 전부 즉시 실행하고,
        // WaitSensor/WaitElapsed를 만나면 거기서 멈춰 Tick()이 이어받게 한다.
        // 리스트 끝까지 갔으면(더 이상 실행할 스텝이 없으면) IsRunning을 false로 내린다 -
        // 5슬롯 전체 사이클이 다 끝났을 때도 여기서 자연스럽게 멈춘다.
        private void AdvanceToNextExecutableStep()
        {
            sensorWaitElapsedSeconds = 0;
            currentIndex++;

            while (IsCurrentStepValid && steps[currentIndex].Kind == AutoStepKind.Action)
            {
                steps[currentIndex].Execute();
                currentIndex++;
            }

            if (!IsCurrentStepValid)
            {
                IsRunning = false;
            }
        }
    }
}
