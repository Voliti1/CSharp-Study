using System;
using System.Collections.Generic;

namespace SCT_Form
{
    internal class WaferAutoSequencer
    {
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

        internal event Action<string> Aborted;

        internal string CurrentModule => IsCurrentStepValid ? steps[currentIndex].Module : null;
        internal string CurrentDescription => IsCurrentStepValid ? steps[currentIndex].Description : string.Empty;
        internal AutoStepKind CurrentKind => IsCurrentStepValid ? steps[currentIndex].Kind : AutoStepKind.Action;
        internal int CurrentElapsedSeconds => IsCurrentStepValid ? steps[currentIndex].ElapsedSeconds : 0;
        internal int CurrentTotalSeconds => IsCurrentStepValid ? steps[currentIndex].TotalSeconds : 0;
        internal int CurrentStepIndex => currentIndex + 1;
        internal int TotalStepCount => steps.Count;

        private bool IsCurrentStepValid => currentIndex >= 0 && currentIndex < steps.Count;

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

        internal void Abort(string reason)
        {
            if (!IsRunning) return;

            IsRunning = false;
            IsAborted = true;
            Aborted?.Invoke(reason);
        }

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
