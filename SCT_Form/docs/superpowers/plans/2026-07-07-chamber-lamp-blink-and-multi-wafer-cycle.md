# Chamber Lamp Blink + 5-Slot Wafer Cycle Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** (1) Make the chamber lamp blink 5 times (On/Off, 1s interval) instead of immediately turning off when a PM process finishes. (2) Make the auto wafer sequencer process all 5 FOUP A slots (1-5) in sequence instead of just slot 1, unloading each wafer to the matching FOUP B slot.

**Architecture:** `MainGUI.SetChamberLamp` gets wrapped so it cancels any in-progress blink for that module before changing hardware state; a new `MainGUI.BlinkChamberLamp` drives a per-module `System.Windows.Forms.Timer` that toggles the lamp 9 times at 1-second intervals (5 On-phases, 5 Off-phases counting the initial forced-On, ending Off) without blocking the auto sequencer. `AutoSequenceBuilder.AddProcessWait` calls `BlinkChamberLamp` instead of `SetChamberLamp(module, false)`. Separately, `AutoSequenceBuilder.Build` wraps its existing pick/process/place logic in a `for` loop over FOUP slots 1-5, using two new helpers (`GetWaferUp`/`GetWaferDown`) to resolve the right `EquipmentLayout.FoupProfile` field per slot instead of the hardcoded `Wafer1Up`/`Wafer1Down`.

**Tech Stack:** C# / .NET Framework 4.7.2 WinForms, existing `IEG3268_Dll` EtherCAT wrapper (`EtherCAT_M`), MSBuild for verification (no test project exists in this solution).

**Verification note:** This solution has no unit test project and the code drives real EtherCAT hardware, so "tests" in this plan are MSBuild compile checks plus a manual behavior checklist. Each task's build check uses:
```
& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "C:\Users\User\Desktop\CSharp-Study\SCT_Form\SCT_Form.csproj" /p:Configuration=Debug /t:Build /v:minimal /nologo
```
Expected output ends with `SCT_Form -> ...\bin\Debug\SCT_Form.exe` and no `error` lines.

---

### Task 1: Add chamber lamp blink capability to `MainGUI`

**Files:**
- Modify: `Main/MainGUI.cs:322-328`

- [ ] **Step 1: Replace `SetChamberLamp` with the blink-aware version**

Replace the existing method (`Main/MainGUI.cs:322-328`):

```csharp
        internal void SetChamberLamp(string module, bool on)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            EtherCAT_M.Digital_Output(profile.LampOutput, on);
            EtherCAT_M.Digital_Output(1, !on);
            EtherCAT_M.Digital_Output(2, on);
        }
```

with:

```csharp
        private const int ChamberLampBlinkCount = 5;
        private const int ChamberLampBlinkIntervalMs = 1000;
        private readonly Dictionary<string, System.Windows.Forms.Timer> chamberLampBlinkTimers = new Dictionary<string, System.Windows.Forms.Timer>();

        internal void SetChamberLamp(string module, bool on)
        {
            string normalized = EquipmentLayout.NormalizeModule(module);
            StopChamberLampBlink(normalized);
            SetChamberLampOutput(normalized, on);
        }

        internal void BlinkChamberLamp(string module)
        {
            string normalized = EquipmentLayout.NormalizeModule(module);
            StopChamberLampBlink(normalized);

            int toggleCount = 0;
            int totalToggles = (ChamberLampBlinkCount * 2) - 1;
            SetChamberLampOutput(normalized, true);

            System.Windows.Forms.Timer blinkTimer = new System.Windows.Forms.Timer();
            blinkTimer.Interval = ChamberLampBlinkIntervalMs;
            blinkTimer.Tick += (sender, e) =>
            {
                toggleCount++;
                SetChamberLampOutput(normalized, toggleCount % 2 == 0);
                if (toggleCount >= totalToggles)
                {
                    StopChamberLampBlink(normalized);
                }
            };

            chamberLampBlinkTimers[normalized] = blinkTimer;
            blinkTimer.Start();
        }

        private void StopChamberLampBlink(string module)
        {
            System.Windows.Forms.Timer blinkTimer;
            if (chamberLampBlinkTimers.TryGetValue(module, out blinkTimer))
            {
                blinkTimer.Stop();
                blinkTimer.Dispose();
                chamberLampBlinkTimers.Remove(module);
            }
        }

        private void SetChamberLampOutput(string module, bool on)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            EtherCAT_M.Digital_Output(profile.LampOutput, on);
            EtherCAT_M.Digital_Output(1, !on);
            EtherCAT_M.Digital_Output(2, on);
        }
```

Note on the toggle math: `BlinkChamberLamp` forces the lamp On immediately (toggle 0), then the timer flips it 9 more times at 1s each (Off, On, Off, On, Off, On, Off, On, Off) — 5 On-phases and 5 Off-phases total, ending Off after ~9 seconds. `System.Windows.Forms.Timer` is used with its full namespace because `System.Threading` (already `using`d in this file) also defines a `Timer` type — the unqualified name would be ambiguous.

- [ ] **Step 2: Replace `SetAllChamberLamps` so it cancels blinks too**

Replace (`Main/MainGUI.cs`, originally lines 357-362, now shifted down by the insertion in Step 1 — locate by content, not line number):

```csharp
        internal void SetAllChamberLamps(bool on)
        {
            EtherCAT_M.Digital_Output(EquipmentLayout.GetModule("PM A").LampOutput, on);
            EtherCAT_M.Digital_Output(EquipmentLayout.GetModule("PM B").LampOutput, on);
            EtherCAT_M.Digital_Output(EquipmentLayout.GetModule("PM C").LampOutput, on);
        }
```

with:

```csharp
        internal void SetAllChamberLamps(bool on)
        {
            SetChamberLamp("PM A", on);
            SetChamberLamp("PM B", on);
            SetChamberLamp("PM C", on);
        }
```

This makes `SafeAbortAndHome`'s forced all-lamps-off call (`Main/MainGUI.cs:1245`) also cancel any in-progress blink timers, so an Abort can't have a blink timer turn a lamp back on after the forced Off.

- [ ] **Step 3: Build verification**

Run the MSBuild command from the plan header. Expected: build succeeds, no errors.

- [ ] **Step 4: Commit**

```bash
git add Main/MainGUI.cs
git commit -m "Add non-blocking chamber lamp blink on process complete"
```

---

### Task 2: Wire the blink into process-complete and process all 5 FOUP slots

**Files:**
- Modify: `Features/Operation/AutoSequenceBuilder.cs` (whole file replacement — every method from `AddPickFromFoup` through `AddPlaceIntoFoup` changes to carry a `slot` parameter)

- [ ] **Step 1: Replace the entire file contents**

```csharp
using System;
using System.Collections.Generic;

namespace SCT_Form
{
    internal static class AutoSequenceBuilder
    {
        private const string FoupASource = "FOUP A";
        private const string FoupBDestination = "FOUP B";
        private const int AxisMoveTimeoutSeconds = 60;
        private const int DoorMoveTimeoutSeconds = 30;
        private const int CylinderMoveTimeoutSeconds = 30;
        private const int VacuumSettleSeconds = 1;
        private const long Axis1PositionToleranceCounts = 70000;
        private const long Axis2PositionToleranceCounts = 30000;
        private const int WaferSlotCount = 5;

        internal static List<WaferAutoSequencer.AutoStep> Build(
            MainGUI main,
            List<ChamberRecipeSelection> recipeSteps,
            Action<string, int, bool> setFoupSlotState = null,
            Action<string, bool> setModuleWaferState = null)
        {
            List<WaferAutoSequencer.AutoStep> steps = new List<WaferAutoSequencer.AutoStep>();
            if (main == null || recipeSteps == null || recipeSteps.Count == 0) return steps;

            string firstModule = EquipmentLayout.NormalizeModule(recipeSteps[0].Module);
            string lastModule = EquipmentLayout.NormalizeModule(recipeSteps[recipeSteps.Count - 1].Module);

            for (int slot = 1; slot <= WaferSlotCount; slot++)
            {
                AddPickFromFoup(steps, main, FoupASource, slot, firstModule, firstModule + " load from FOUP A (slot " + slot + ")", setFoupSlotState);

                foreach (ChamberRecipeSelection recipeStep in recipeSteps)
                {
                    string module = EquipmentLayout.NormalizeModule(recipeStep.Module);
                    AddPlaceIntoModule(steps, main, module, setModuleWaferState);
                    AddProcessWait(steps, main, module, recipeStep.ProcessTime);
                    AddPickFromModule(steps, main, module, setModuleWaferState);
                }

                AddPlaceIntoFoup(steps, main, FoupBDestination, slot, lastModule, lastModule + " unload to FOUP B (slot " + slot + ")", setFoupSlotState);
            }

            return steps;
        }

        private static void AddAction(List<WaferAutoSequencer.AutoStep> steps, MainGUI main, string module, string description, Action execute)
        {
            steps.Add(new WaferAutoSequencer.AutoStep
            {
                Kind = WaferAutoSequencer.AutoStepKind.Action,
                Module = module,
                Description = description,
                Execute = () =>
                {
                    main.WriteSystemLog("INFO", "Auto Process: " + description);
                    execute();
                }
            });
        }

        private static void AddWaitSensor(List<WaferAutoSequencer.AutoStep> steps, string module, string description, Func<bool> isSatisfied, int timeoutSeconds = 10)
        {
            steps.Add(new WaferAutoSequencer.AutoStep
            {
                Kind = WaferAutoSequencer.AutoStepKind.WaitSensor,
                Module = module,
                Description = description,
                IsSatisfied = isSatisfied,
                TimeoutSeconds = timeoutSeconds
            });
        }

        private static void AddWaitElapsed(List<WaferAutoSequencer.AutoStep> steps, string module, string description, int seconds)
        {
            steps.Add(new WaferAutoSequencer.AutoStep
            {
                Kind = WaferAutoSequencer.AutoStepKind.WaitElapsed,
                Module = module,
                Description = description,
                TotalSeconds = Math.Max(1, seconds)
            });
        }

        // 도달 판정은 실제 위치(±5000)만 사용한다. 드라이브의 target-reached(PP_D) 비트는
        // 서보 정착이 끝날 때까지 5~7초 늦게, 그마저 흔들리며 켜져서(정착 꼬리 동안 chatter)
        // 스텝 진행을 불필요하게 지연시키는 것이 로그로 확인됐다.
        private static void AddAxis1Move(List<WaferAutoSequencer.AutoStep> steps, MainGUI main, string module, string description, long targetPosition)
        {
            AddAction(steps, main, module, description, () => main.MoveAxis1UDTo(targetPosition));
            AddWaitSensor(
                steps,
                module,
                description + " target reached wait",
                () => main.IsAxis1AtPosition(targetPosition, Axis1PositionToleranceCounts),
                AxisMoveTimeoutSeconds);
        }

        private static void AddAxis2Move(List<WaferAutoSequencer.AutoStep> steps, MainGUI main, string module, string description, long targetPosition)
        {
            AddAction(steps, main, module, description, () => main.MoveAxis2LRTo(targetPosition));
            AddWaitSensor(
                steps, 
                module, 
                description + " target reached wait", 
                () => main.IsAxis2AtPosition(targetPosition, Axis2PositionToleranceCounts), 
                AxisMoveTimeoutSeconds);
        }

        private static long GetWaferUp(EquipmentLayout.FoupProfile profile, int slot)
        {
            switch (slot)
            {
                case 1: return profile.Wafer1Up;
                case 2: return profile.Wafer2Up;
                case 3: return profile.Wafer3Up;
                case 4: return profile.Wafer4Up;
                case 5: return profile.Wafer5Up;
                default: throw new ArgumentOutOfRangeException(nameof(slot), slot, "FOUP slot must be between 1 and 5.");
            }
        }

        private static long GetWaferDown(EquipmentLayout.FoupProfile profile, int slot)
        {
            switch (slot)
            {
                case 1: return profile.Wafer1Down;
                case 2: return profile.Wafer2Down;
                case 3: return profile.Wafer3Down;
                case 4: return profile.Wafer4Down;
                case 5: return profile.Wafer5Down;
                default: throw new ArgumentOutOfRangeException(nameof(slot), slot, "FOUP slot must be between 1 and 5.");
            }
        }

        private static void AddPickFromFoup(
            List<WaferAutoSequencer.AutoStep> steps,
            MainGUI main,
            string foup,
            int slot,
            string displayModule,
            string label,
            Action<string, int, bool> setFoupSlotState)
        {
            EquipmentLayout.FoupProfile profile = EquipmentLayout.GetFoup(foup);

            AddAxis2Move(steps, main, displayModule, label + " - LR move", profile.LR);
            AddAxis1Move(steps, main, displayModule, label + " - UD down", GetWaferDown(profile, slot));
            AddAction(steps, main, displayModule, label + " - cylinder front", main.MoveCylinderFront);
            AddWaitSensor(steps, displayModule, label + " - cylinder front wait", main.IsCylinderForward, CylinderMoveTimeoutSeconds);
            AddAxis1Move(steps, main, displayModule, label + " - UD up", GetWaferUp(profile, slot));
            AddAction(steps, main, displayModule, label + " - vacuum on", () => main.SetWaferSuction(true));
            AddWaitElapsed(steps, displayModule, label + " - vacuum settle", VacuumSettleSeconds);
            AddAction(steps, main, displayModule, label + " - cylinder back", main.MoveCylinderBack);
            AddWaitSensor(steps, displayModule, label + " - cylinder back wait", main.IsCylinderBack, CylinderMoveTimeoutSeconds);
            AddAction(steps, main, displayModule, label + " - FOUP slot empty", () => setFoupSlotState?.Invoke(foup, slot, false));
        }

        private static void AddPlaceIntoModule(
            List<WaferAutoSequencer.AutoStep> steps,
            MainGUI main,
            string module,
            Action<string, bool> setModuleWaferState)
        {
            string label = module + " load";
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);

            AddAxis2Move(steps, main, module, label + " - LR move", profile.LR);
            AddAxis1Move(steps, main, module, label + " - UD up", profile.UDUp);
            AddWaitSensor(steps, module, label + " - robot facing wait", () => main.IsRobotFacingModule(module));
            AddAction(steps, main, module, label + " - door open", () => main.OpenChamberDoor(module));
            AddWaitSensor(steps, module, label + " - door open wait", () => main.IsChamberDoorOpen(module), DoorMoveTimeoutSeconds);
            AddAction(steps, main, module, label + " - cylinder front", main.MoveCylinderFront);
            AddWaitSensor(steps, module, label + " - cylinder front wait", main.IsCylinderForward, CylinderMoveTimeoutSeconds);
            AddAction(steps, main, module, label + " - vacuum off", () => main.SetWaferSuction(false));
            AddAction(steps, main, module, label + " - exhaust on", () => main.SetWaferExhaust(true));
            AddWaitElapsed(steps, module, label + " - vacuum settle", VacuumSettleSeconds);
            AddAxis1Move(steps, main, module, label + " - UD down", profile.UDDown);
            AddAction(steps, main, module, label + " - cylinder back", main.MoveCylinderBack);
            AddWaitSensor(steps, module, label + " - cylinder back wait", main.IsCylinderBack, CylinderMoveTimeoutSeconds);
            AddAction(steps, main, module, label + " - exhaust off", () => main.SetWaferExhaust(false));
            AddAction(steps, main, module, label + " - PM wafer present", () => setModuleWaferState?.Invoke(module, true));
            AddAction(steps, main, module, label + " - door close", () => main.CloseChamberDoor(module));
            AddWaitSensor(steps, module, label + " - door close wait", () => main.IsChamberDoorClosed(module), DoorMoveTimeoutSeconds);
        }

        private static void AddProcessWait(List<WaferAutoSequencer.AutoStep> steps, MainGUI main, string module, int processTimeSeconds)
        {
            AddAction(steps, main, module, module + " process start", () => main.SetChamberLamp(module, true));

            steps.Add(new WaferAutoSequencer.AutoStep
            {
                Kind = WaferAutoSequencer.AutoStepKind.WaitElapsed,
                Module = module,
                Description = module + " process running",
                TotalSeconds = Math.Max(1, processTimeSeconds)
            });

            AddAction(steps, main, module, module + " process complete", () => main.BlinkChamberLamp(module));
        }

        private static void AddPickFromModule(
            List<WaferAutoSequencer.AutoStep> steps,
            MainGUI main,
            string module,
            Action<string, bool> setModuleWaferState)
        {
            string label = module + " unload";
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);

            AddWaitSensor(steps, module, label + " - robot facing wait", () => main.IsRobotFacingModule(module));
            AddAction(steps, main, module, label + " - door open", () => main.OpenChamberDoor(module));
            AddWaitSensor(steps, module, label + " - door open wait", () => main.IsChamberDoorOpen(module), DoorMoveTimeoutSeconds);
            AddAxis1Move(steps, main, module, label + " - UD down", profile.UDDown);
            AddAction(steps, main, module, label + " - cylinder front", main.MoveCylinderFront);
            AddWaitSensor(steps, module, label + " - cylinder front wait", main.IsCylinderForward, CylinderMoveTimeoutSeconds);
            AddAxis1Move(steps, main, module, label + " - UD up", profile.UDUp);
            AddAction(steps, main, module, label + " - vacuum on", () => main.SetWaferSuction(true));
            AddWaitElapsed(steps, module, label + " - vacuum settle", VacuumSettleSeconds);
            AddAction(steps, main, module, label + " - cylinder back", main.MoveCylinderBack);
            AddWaitSensor(steps, module, label + " - cylinder back wait", main.IsCylinderBack, CylinderMoveTimeoutSeconds);
            AddAction(steps, main, module, label + " - PM wafer empty", () => setModuleWaferState?.Invoke(module, false));
            AddAction(steps, main, module, label + " - door close", () => main.CloseChamberDoor(module));
            AddWaitSensor(steps, module, label + " - door close wait", () => main.IsChamberDoorClosed(module), DoorMoveTimeoutSeconds);
        }

        private static void AddPlaceIntoFoup(
            List<WaferAutoSequencer.AutoStep> steps,
            MainGUI main,
            string foup,
            int slot,
            string displayModule,
            string label,
            Action<string, int, bool> setFoupSlotState)
        {
            EquipmentLayout.FoupProfile profile = EquipmentLayout.GetFoup(foup);

            AddAxis2Move(steps, main, displayModule, label + " - LR move", profile.LR);
            AddAxis1Move(steps, main, displayModule, label + " - UD up", GetWaferUp(profile, slot));
            AddAction(steps, main, displayModule, label + " - cylinder front", main.MoveCylinderFront);
            AddWaitSensor(steps, displayModule, label + " - cylinder front wait", main.IsCylinderForward, CylinderMoveTimeoutSeconds);
            AddAction(steps, main, displayModule, label + " - vacuum off", () => main.SetWaferSuction(false));
            AddAction(steps, main, displayModule, label + " - exhaust on", () => main.SetWaferExhaust(true));
            AddWaitElapsed(steps, displayModule, label + " - vacuum settle", VacuumSettleSeconds);
            AddAxis1Move(steps, main, displayModule, label + " - UD down", GetWaferDown(profile, slot));
            AddAction(steps, main, displayModule, label + " - cylinder back", main.MoveCylinderBack);
            AddWaitSensor(steps, displayModule, label + " - cylinder back wait", main.IsCylinderBack, CylinderMoveTimeoutSeconds);
            AddAction(steps, main, displayModule, label + " - exhaust off", () => main.SetWaferExhaust(false));
            AddAction(steps, main, displayModule, label + " - FOUP slot present", () => setFoupSlotState?.Invoke(foup, slot, true));
        }
    }
}
```

- [ ] **Step 2: Build verification**

Run the MSBuild command from the plan header. Expected: build succeeds, no errors.

- [ ] **Step 3: Commit**

```bash
git add Features/Operation/AutoSequenceBuilder.cs
git commit -m "Blink chamber lamp on process complete and cycle through all 5 FOUP slots"
```

---

### Task 3: Manual hardware verification (cannot be automated)

**Files:** none — this is a checklist to run against the real machine or a controlled bench test.

- [ ] Load a Process Recipe with a single short step (e.g. 10s) targeting PM A. Load a wafer only into FOUP A slot 1 (leave slots 2-5 empty for this first check, since occupancy isn't sensor-checked) and click Start.
- [ ] Confirm the PM A lamp turns on for the ~10s process, then blinks On/Off roughly 5 times at ~1s intervals (about 9-10s total) instead of turning off immediately, while the robot proceeds to unload the wafer without waiting for the blink to finish.
- [ ] Trigger `btn_Abort_Click` while a lamp is mid-blink (e.g. right after a process finishes). Confirm the lamp goes solid Off immediately and stays off (the blink timer does not turn it back on afterward).
- [ ] Load wafers into all 5 FOUP A slots and start a recipe. Confirm the sequence runs slot 1 (FOUP A slot 1 → PM route → FOUP B slot 1), then automatically returns to FOUP A slot 2 and repeats, through slot 5, without manual intervention between cycles.
- [ ] Confirm each wafer lands in the FOUP B slot matching its FOUP A source slot (slot 1 → B slot 1, ..., slot 5 → B slot 5) and the on-screen FOUP slot indicators (`pnl_FOUP_A_1..5`, `pnl_FOUP_B_1..5`) update accordingly after each cycle.
- [ ] Test Abort mid-cycle (e.g. during slot 3's PM step): confirm the run stops there and slots 4-5 are never attempted.

- [ ] **Commit** (only if the checklist above surfaces a fix — otherwise nothing to commit for this task)
