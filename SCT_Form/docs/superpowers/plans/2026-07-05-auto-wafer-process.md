# Auto Wafer Process Sequencer Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Replace `CurrentStateGUI`'s simulated Start/Pause/Continue/Abort timer with a real step-by-step sequencer that drives one wafer from FOUP A slot 1, through each PM listed in the selected Process Recipe's Steps (in order), to FOUP B slot 1.

**Architecture:** Hardware actions currently duplicated inside `MaintGUI.cs` click handlers get extracted into reusable `internal` methods on `MainGUI`. A new `WaferAutoSequencer` class runs a flat list of `AutoStep` records (Action / WaitSensor / WaitElapsed) driven by the existing 1-second `chamberProcessTimer` already in `CurrentStateGUI`. A separate `AutoSequenceBuilder` turns a Process Recipe's module list into that flat `AutoStep` list using position/IO constants centralized in a new `EquipmentLayout` class.

**Tech Stack:** C# / .NET Framework 4.7.2 WinForms, existing `IEG3268_Dll` EtherCAT wrapper (`EtherCAT_M`), MSBuild for verification (no test project exists in this solution).

**Verification note:** This solution has no unit test project and the code drives real EtherCAT hardware, so "tests" in this plan are MSBuild compile checks plus a manual behavior checklist. Each task's build check uses:
```
& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "C:\Users\User\Desktop\CSharp-Study\SCT_Form\SCT_Form.csproj" /p:Configuration=Debug /t:Build /v:minimal /nologo
```
Expected output ends with `SCT_Form -> ...\bin\Debug\SCT_Form.exe` and no `error` lines.

---

### Task 1: Add `EquipmentLayout` — centralized position/IO constants

**Files:**
- Create: `Main/EquipmentLayout.cs`

- [ ] **Step 1: Write the file**

```csharp
using System;
using System.Collections.Generic;

namespace SCT_Form
{
    internal static class EquipmentLayout
    {
        internal class ModuleProfile
        {
            public long LR;
            public long UDDown;
            public long UDUp;
            public int DoorOpenOutput;
            public int DoorCloseOutput;
            public int DoorUpSensor;
            public int DoorDownSensor;
            public int LampOutput;
        }

        internal class FoupProfile
        {
            public long LR;
            public long Wafer1Down;
            public long Wafer1Up;
        }

        private static readonly Dictionary<string, ModuleProfile> Modules = new Dictionary<string, ModuleProfile>
        {
            { "PM A", new ModuleProfile { LR = -59064, UDDown = 806931, UDUp = 1156931, DoorOpenOutput = 5, DoorCloseOutput = 4, DoorUpSensor = 6, DoorDownSensor = 7, LampOutput = 3 } },
            { "PM B", new ModuleProfile { LR = -190823, UDDown = 806931, UDUp = 1156931, DoorOpenOutput = 8, DoorCloseOutput = 7, DoorUpSensor = 8, DoorDownSensor = 9, LampOutput = 6 } },
            { "PM C", new ModuleProfile { LR = -322000, UDDown = 806931, UDUp = 1156931, DoorOpenOutput = 11, DoorCloseOutput = 10, DoorUpSensor = 10, DoorDownSensor = 11, LampOutput = 9 } },
        };

        private static readonly Dictionary<string, FoupProfile> Foups = new Dictionary<string, FoupProfile>
        {
            { "FOUP A", new FoupProfile { LR = 13140, Wafer1Down = 100379, Wafer1Up = 302380 } },
            { "FOUP B", new FoupProfile { LR = -395093, Wafer1Down = 100379, Wafer1Up = 302380 } },
        };

        internal static string NormalizeModule(string module)
        {
            if (string.Equals(module, "PM B", StringComparison.OrdinalIgnoreCase)) return "PM B";
            if (string.Equals(module, "PM C", StringComparison.OrdinalIgnoreCase)) return "PM C";
            return "PM A";
        }

        internal static ModuleProfile GetModule(string module)
        {
            return Modules[NormalizeModule(module)];
        }

        internal static FoupProfile GetFoup(string foup)
        {
            return Foups[foup];
        }
    }
}
```

- [ ] **Step 2: Build verification**

Run the MSBuild command above.
Expected: `SCT_Form -> ...\SCT_Form.exe`, no errors (this file has no external dependents yet, so it just needs to compile standalone).

- [ ] **Step 3: Commit**

```bash
git add Main/EquipmentLayout.cs
git commit -m "Add EquipmentLayout with centralized PM/FOUP position and IO constants"
```

---

### Task 2: Add shared hardware-action methods to `MainGUI`

**Files:**
- Modify: `Main/MainGUI.cs` (add methods after `SetChamberDoorStatus`, around line 264)

- [ ] **Step 1: Add the constant and methods**

Insert directly after the closing brace of `SetChamberDoorStatus` (`Main/MainGUI.cs:264`):

```csharp
        private const long RobotFacingToleranceCounts = 500;

        internal void OpenChamberDoor(string module)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            EtherCAT_M.Digital_Output(profile.DoorOpenOutput, true);
            EtherCAT_M.Digital_Output(profile.DoorCloseOutput, false);
            SetChamberDoorStatus(EquipmentLayout.NormalizeModule(module), true);
        }

        internal void CloseChamberDoor(string module)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            EtherCAT_M.Digital_Output(profile.DoorOpenOutput, false);
            EtherCAT_M.Digital_Output(profile.DoorCloseOutput, true);
            SetChamberDoorStatus(EquipmentLayout.NormalizeModule(module), false);
        }

        internal void SetChamberLamp(string module, bool on)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            EtherCAT_M.Digital_Output(profile.LampOutput, on);
            EtherCAT_M.Digital_Output(1, !on);
            EtherCAT_M.Digital_Output(2, on);
        }

        internal void MoveCylinderFront()
        {
            EtherCAT_M.Digital_Output(13, false);
            EtherCAT_M.Digital_Output(12, true);
        }

        internal void MoveCylinderBack()
        {
            EtherCAT_M.Digital_Output(12, false);
            EtherCAT_M.Digital_Output(13, true);
        }

        internal void SetWaferSuction(bool on)
        {
            EtherCAT_M.Digital_Output(14, on);
        }

        internal bool IsCylinderForward()
        {
            return EtherCAT_M.Digital_Input(13) && !EtherCAT_M.Digital_Input(12);
        }

        internal bool IsCylinderBack()
        {
            return EtherCAT_M.Digital_Input(12) && !EtherCAT_M.Digital_Input(13);
        }

        internal bool IsChamberDoorOpen(string module)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            return EtherCAT_M.Digital_Input(profile.DoorDownSensor) && !EtherCAT_M.Digital_Input(profile.DoorUpSensor);
        }

        internal bool IsChamberDoorClosed(string module)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            return EtherCAT_M.Digital_Input(profile.DoorUpSensor) && !EtherCAT_M.Digital_Input(profile.DoorDownSensor);
        }

        internal bool IsRobotFacingModule(string module)
        {
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);
            long current;
            if (!long.TryParse(EtherCAT_M.Axis2_is_PosData(), out current)) return false;
            return Math.Abs(current - profile.LR) <= RobotFacingToleranceCounts;
        }
```

- [ ] **Step 2: Build verification**

Run the MSBuild command. Expected: build succeeds, no errors.

- [ ] **Step 3: Commit**

```bash
git add Main/MainGUI.cs
git commit -m "Add shared chamber door/lamp/cylinder/suction control methods to MainGUI"
```

---

### Task 3: Refactor `MaintGUI.cs` manual buttons to call the shared methods

Behavior-preserving refactor: every DO index and call order stays identical, just routed through the new `MainGUI` methods instead of raw `Digital_Output` calls. Panel color updates and `WriteSystemLog` calls stay in `MaintGUI` unchanged.

**Files:**
- Modify: `Features/Maint/MaintGUI.cs:53-333` (chamber A/B/C door+lamp handlers, cylinder handlers) and `:356-362` (`IsRobotCylinderForward`)

- [ ] **Step 1: Replace the Chamber A block (lines 53-105)**

```csharp
        // --- Chamber A 제어 영역 ---
        private void btn_Cham_A_Door_OPEN_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber A 도어 OPEN 명령 요청");

            main.OpenChamberDoor("PM A");
            pnl_Cham_A_Door.BackColor = Color.Red;

            main.WriteSystemLog("INFO", "Chamber A 도어 OPEN 완료");
        }

        private void btn_Cham_A_Door_CLOSE_Click(object sender, EventArgs e)
        {
            if (!CanCloseChamberDoor()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber A 도어 CLOSE 명령 요청");

            main.CloseChamberDoor("PM A");
            pnl_Cham_A_Door.BackColor = Color.LightGray;

            main.WriteSystemLog("INFO", "Chamber A 도어 CLOSE 완료");
        }

        private void btn_Cham_A_Lamp_ON_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber A 램프 ON 명령 요청");

            main.SetChamberLamp("PM A", true);
            pnl_Cham_A_Lamp.BackColor = Color.LimeGreen;

            main.WriteSystemLog("INFO", "Chamber A 램프 ON 완료 (박막생성 공정 시작)");
        }

        private void btn_Cham_A_Lamp_OFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber A 램프 OFF 명령 요청");

            main.SetChamberLamp("PM A", false);
            pnl_Cham_A_Lamp.BackColor = Color.LightGray;

            main.WriteSystemLog("INFO", "Chamber A 램프 OFF 완료 (박막생성 공정 종료)");
        }
```

- [ ] **Step 2: Replace the Chamber B block (was lines 108-160)**

```csharp
        // --- Chamber B 제어 영역 ---
        private void btn_Cham_B_Door_OPEN_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber B 도어 OPEN 명령 요청");

            main.OpenChamberDoor("PM B");
            pnl_Cham_B_Door.BackColor = Color.Red;

            main.WriteSystemLog("INFO", "Chamber B 도어 OPEN 완료");
        }

        private void btn_Cham_B_Door_CLOSE_Click(object sender, EventArgs e)
        {
            if (!CanCloseChamberDoor()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber B 도어 CLOSE 명령 요청");

            main.CloseChamberDoor("PM B");
            pnl_Cham_B_Door.BackColor = Color.LightGray;

            main.WriteSystemLog("INFO", "Chamber B 도어 CLOSE 완료");
        }

        private void btn_Cham_B_Lamp_ON_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber B 램프 ON 명령 요청");

            main.SetChamberLamp("PM B", true);
            pnl_Cham_B_Lamp.BackColor = Color.LimeGreen;

            main.WriteSystemLog("INFO", "Chamber B 램프 ON 완료 (CMP 공정 시작)");
        }

        private void btn_Cham_B_Lamp_OFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber B 램프 OFF 명령 요청");

            main.SetChamberLamp("PM B", false);
            pnl_Cham_B_Lamp.BackColor = Color.LightGray;

            main.WriteSystemLog("INFO", "Chamber B 램프 OFF 완료 (CMP 공정 종료)");
        }
```

- [ ] **Step 3: Replace the Chamber C block (was lines 163-214)**

```csharp
        // --- Chamber C 제어 영역 ---
        private void btn_Cham_C_Door_OPEN_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber C 도어 OPEN 명령 요청");

            main.OpenChamberDoor("PM C");
            pnl_Cham_C_Door.BackColor = Color.Red;

            main.WriteSystemLog("INFO", "Chamber C 도어 OPEN 완료");
        }

        private void btn_Cham_C_Door_CLOSE_Click(object sender, EventArgs e)
        {
            if (!CanCloseChamberDoor()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber C 도어 CLOSE 명령 요청");

            main.CloseChamberDoor("PM C");
            pnl_Cham_C_Door.BackColor = Color.LightGray;

            main.WriteSystemLog("INFO", "Chamber C 도어 CLOSE 완료");
        }

        private void btn_Cham_C_Lamp_ON_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber C 램프 ON 명령 요청");

            main.SetChamberLamp("PM C", true);
            pnl_Cham_C_Lamp.BackColor = Color.LimeGreen;

            main.WriteSystemLog("INFO", "Chamber C 램프 ON 완료 (세정 공정 시작)");
        }

        private void btn_Cham_C_Lamp_OFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber C 램프 OFF 명령 요청");

            main.SetChamberLamp("PM C", false);
            pnl_Cham_C_Lamp.BackColor = Color.LightGray;

            main.WriteSystemLog("INFO", "Chamber C 램프 OFF 완료 (세정 공정 종료)");
        }
```

- [ ] **Step 4: Replace cylinder handlers (was lines 321-333)**

```csharp
        private void btn_moveFront_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.MoveCylinderFront();
        }

        private void btn_moveBack_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.MoveCylinderBack();
        }
```

- [ ] **Step 5: Replace suction handlers (was lines 297-307)**

```csharp
        private void btn_InOn_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.SetWaferSuction(true);
        }

        private void btn_InOFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.SetWaferSuction(false);
        }
```

- [ ] **Step 6: Replace `IsRobotCylinderForward` (lines 356-362) to delegate to the shared method**

```csharp
        private bool IsRobotCylinderForward()
        {
            return main.IsCylinderForward();
        }
```

Leave `RobotCylinderFrontSensorInput`/`RobotCylinderBackSensorInput` constants and `GetChamberDoorStatusText` untouched — they're only used for the door-status label text, not for gating moves, and changing them isn't needed for this feature.

- [ ] **Step 7: Build verification**

Run the MSBuild command. Expected: build succeeds, no errors.

- [ ] **Step 8: Manual spot-check (no hardware needed)**

Open `Features/Maint/MaintGUI.cs` in the diff and confirm every `Digital_Output`/`Digital_Input` call that existed before Step 1-6 still happens with the same index and boolean value, just moved into `MainGUI`. This guards against silently changing a DO index during the refactor.

- [ ] **Step 9: Commit**

```bash
git add Features/Maint/MaintGUI.cs
git commit -m "Refactor MaintGUI manual buttons to call shared MainGUI hardware actions"
```

---

### Task 4: Add `WaferAutoSequencer` — step state machine

**Files:**
- Create: `Features/Operation/WaferAutoSequencer.cs`

- [ ] **Step 1: Write the file**

```csharp
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

            if (step.Kind == AutoStepKind.WaitElapsed)
            {
                step.ElapsedSeconds++;
                if (step.ElapsedSeconds >= step.TotalSeconds)
                {
                    AdvanceToNextExecutableStep();
                }

                return;
            }

            if (step.IsSatisfied())
            {
                AdvanceToNextExecutableStep();
                return;
            }

            sensorWaitElapsedSeconds++;
            if (sensorWaitElapsedSeconds >= step.TimeoutSeconds)
            {
                Abort(step.Description + " 단계에서 센서 응답이 " + step.TimeoutSeconds + "초 내에 확인되지 않았습니다.");
            }
        }

        internal void Abort(string reason)
        {
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
```

- [ ] **Step 2: Build verification**

Run the MSBuild command. Expected: build succeeds (this class has no dependents yet).

- [ ] **Step 3: Commit**

```bash
git add Features/Operation/WaferAutoSequencer.cs
git commit -m "Add WaferAutoSequencer step state machine"
```

---

### Task 5: Add `AutoSequenceBuilder` — turns a Process Recipe into an `AutoStep` list

**Files:**
- Create: `Features/Operation/AutoSequenceBuilder.cs`

- [ ] **Step 1: Write the file**

```csharp
using System;
using System.Collections.Generic;

namespace SCT_Form
{
    internal static class AutoSequenceBuilder
    {
        private const string FoupASource = "FOUP A";
        private const string FoupBDestination = "FOUP B";

        internal static List<WaferAutoSequencer.AutoStep> Build(MainGUI main, List<ChamberRecipeSelection> recipeSteps)
        {
            List<WaferAutoSequencer.AutoStep> steps = new List<WaferAutoSequencer.AutoStep>();
            if (main == null || recipeSteps == null || recipeSteps.Count == 0) return steps;

            string firstModule = EquipmentLayout.NormalizeModule(recipeSteps[0].Module);
            string lastModule = EquipmentLayout.NormalizeModule(recipeSteps[recipeSteps.Count - 1].Module);

            AddPickFromFoup(steps, main, FoupASource, firstModule, firstModule + " 웨이퍼 로딩 준비 중 (FOUP A)");

            foreach (ChamberRecipeSelection recipeStep in recipeSteps)
            {
                string module = EquipmentLayout.NormalizeModule(recipeStep.Module);
                AddPlaceIntoModule(steps, main, module);
                AddProcessWait(steps, main, module, recipeStep.ProcessTime);
                AddPickFromModule(steps, main, module);
            }

            AddPlaceIntoFoup(steps, main, FoupBDestination, lastModule, lastModule + " 웨이퍼 언로딩 중 (FOUP B)");

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
                    main.WriteSystemLog("INFO", "자동공정: " + description);
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

        private static void AddPickFromFoup(List<WaferAutoSequencer.AutoStep> steps, MainGUI main, string foup, string displayModule, string label)
        {
            EquipmentLayout.FoupProfile profile = EquipmentLayout.GetFoup(foup);

            AddAction(steps, main, displayModule, label + " - 위치 이동", () => main.MoveAxis2LRTo(profile.LR));
            AddAction(steps, main, displayModule, label + " - 진입 높이 하강", () => main.MoveAxis1UDTo(profile.Wafer1Down));
            AddAction(steps, main, displayModule, label + " - 실린더 전진", main.MoveCylinderFront);
            AddAction(steps, main, displayModule, label + " - 웨이퍼 들어올림", () => main.MoveAxis1UDTo(profile.Wafer1Up));
            AddAction(steps, main, displayModule, label + " - 진공 흡착", () => main.SetWaferSuction(true));
            AddAction(steps, main, displayModule, label + " - 실린더 후진", main.MoveCylinderBack);
            AddWaitSensor(steps, displayModule, label + " - 후진 확인 대기", main.IsCylinderBack);
        }

        private static void AddPlaceIntoModule(List<WaferAutoSequencer.AutoStep> steps, MainGUI main, string module)
        {
            string label = module + " 웨이퍼 로딩 중";
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);

            AddAction(steps, main, module, label + " - 위치 이동", () => main.MoveAxis2LRTo(profile.LR));
            AddAction(steps, main, module, label + " - 진입 높이 상승", () => main.MoveAxis1UDTo(profile.UDUp));
            AddWaitSensor(steps, module, label + " - 로봇 방향 확인", () => main.IsRobotFacingModule(module));
            AddAction(steps, main, module, label + " - 문 열기", () => main.OpenChamberDoor(module));
            AddWaitSensor(steps, module, label + " - 문 열림 확인", () => main.IsChamberDoorOpen(module));
            AddAction(steps, main, module, label + " - 실린더 전진", main.MoveCylinderFront);
            AddAction(steps, main, module, label + " - 진공 해제", () => main.SetWaferSuction(false));
            AddAction(steps, main, module, label + " - 로봇 하강", () => main.MoveAxis1UDTo(profile.UDDown));
            AddAction(steps, main, module, label + " - 실린더 후진", main.MoveCylinderBack);
            AddWaitSensor(steps, module, label + " - 후진 확인 대기", main.IsCylinderBack);
            AddAction(steps, main, module, label + " - 문 닫기", () => main.CloseChamberDoor(module));
            AddWaitSensor(steps, module, label + " - 문 닫힘 확인", () => main.IsChamberDoorClosed(module));
        }

        private static void AddProcessWait(List<WaferAutoSequencer.AutoStep> steps, MainGUI main, string module, int processTimeSeconds)
        {
            AddAction(steps, main, module, module + " 공정 진행 중", () => main.SetChamberLamp(module, true));

            steps.Add(new WaferAutoSequencer.AutoStep
            {
                Kind = WaferAutoSequencer.AutoStepKind.WaitElapsed,
                Module = module,
                Description = module + " 공정 진행 중",
                TotalSeconds = Math.Max(1, processTimeSeconds)
            });

            AddAction(steps, main, module, module + " 공정 완료", () => main.SetChamberLamp(module, false));
        }

        private static void AddPickFromModule(List<WaferAutoSequencer.AutoStep> steps, MainGUI main, string module)
        {
            string label = module + " 웨이퍼 언로딩 중";
            EquipmentLayout.ModuleProfile profile = EquipmentLayout.GetModule(module);

            AddWaitSensor(steps, module, label + " - 로봇 방향 확인", () => main.IsRobotFacingModule(module));
            AddAction(steps, main, module, label + " - 문 열기", () => main.OpenChamberDoor(module));
            AddWaitSensor(steps, module, label + " - 문 열림 확인", () => main.IsChamberDoorOpen(module));
            AddAction(steps, main, module, label + " - 실린더 전진", main.MoveCylinderFront);
            AddAction(steps, main, module, label + " - 진입 높이 하강", () => main.MoveAxis1UDTo(profile.UDDown));
            AddAction(steps, main, module, label + " - 웨이퍼 들어올림", () => main.MoveAxis1UDTo(profile.UDUp));
            AddAction(steps, main, module, label + " - 진공 흡착", () => main.SetWaferSuction(true));
            AddAction(steps, main, module, label + " - 실린더 후진", main.MoveCylinderBack);
            AddWaitSensor(steps, module, label + " - 후진 확인 대기", main.IsCylinderBack);
            AddAction(steps, main, module, label + " - 문 닫기", () => main.CloseChamberDoor(module));
        }

        private static void AddPlaceIntoFoup(List<WaferAutoSequencer.AutoStep> steps, MainGUI main, string foup, string displayModule, string label)
        {
            EquipmentLayout.FoupProfile profile = EquipmentLayout.GetFoup(foup);

            AddAction(steps, main, displayModule, label + " - 위치 이동", () => main.MoveAxis2LRTo(profile.LR));
            AddAction(steps, main, displayModule, label + " - 진입 높이 상승", () => main.MoveAxis1UDTo(profile.Wafer1Up));
            AddAction(steps, main, displayModule, label + " - 실린더 전진", main.MoveCylinderFront);
            AddAction(steps, main, displayModule, label + " - 진공 해제", () => main.SetWaferSuction(false));
            AddAction(steps, main, displayModule, label + " - 로봇 하강", () => main.MoveAxis1UDTo(profile.Wafer1Down));
            AddAction(steps, main, displayModule, label + " - 실린더 후진", main.MoveCylinderBack);
            AddWaitSensor(steps, displayModule, label + " - 후진 확인 대기", main.IsCylinderBack);
        }
    }
}
```

- [ ] **Step 2: Build verification**

Run the MSBuild command. Expected: build succeeds.

- [ ] **Step 3: Commit**

```bash
git add Features/Operation/AutoSequenceBuilder.cs
git commit -m "Add AutoSequenceBuilder to translate Process Recipe steps into AutoStep list"
```

---

### Task 6: Wire `CurrentStateGUI` to the real sequencer and remove the dead simulation path

**Files:**
- Modify: `Features/Operation/CurrentStateGUI.cs`

- [ ] **Step 1: Add the sequencer field and wire the `Aborted` event in the constructor**

Add near the other fields (after `private ChamberProcessState pmcProcessState;` at line 31):

```csharp
        private readonly WaferAutoSequencer waferSequencer = new WaferAutoSequencer();
```

Remove this now-unused field (line 26):
```csharp
        private int currentProcessStepIndex = -1;
```

In the constructor, after `chamberProcessTimer.Tick += chamberProcessTimer_Tick;` (line 47), add:

```csharp
            waferSequencer.Aborted += reason =>
            {
                main.WriteSystemLog("WARN", "자동공정 Abort: " + reason);
                MessageBox.Show(reason, "Process Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
```

- [ ] **Step 2: Replace `chamberProcessTimer_Tick` (lines 402-425)**

```csharp
        private void chamberProcessTimer_Tick(object sender, EventArgs e)
        {
            if (isProcessRecipeRunning)
            {
                if (isProcessRecipePaused) return;

                waferSequencer.Tick();
                UpdateAutoSequenceDisplay();

                if (!waferSequencer.IsRunning)
                {
                    isProcessRecipeRunning = false;
                    chamberProcessTimer.Stop();

                    if (!waferSequencer.IsAborted)
                    {
                        MessageBox.Show("Process Recipe가 완료되었습니다.", "Process Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Label stepNumberLabel = GetModuleStepNumberLabel(module);
            Label recipeTimeLabel = GetModuleRecipeTimeLabel(module);
            Panel progressPanel = GetModuleProgressPanel(module);

            messageLabel.Text = waferSequencer.CurrentDescription;
            stepNumberLabel.Text = waferSequencer.CurrentStepIndex + " / " + waferSequencer.TotalStepCount + " step";

            if (waferSequencer.CurrentKind == WaferAutoSequencer.AutoStepKind.WaitElapsed)
            {
                recipeTimeLabel.Text = waferSequencer.CurrentElapsedSeconds + " / " + waferSequencer.CurrentTotalSeconds;
                UpdateProgressBar(progressPanel, GetProgressFillPanel(progressPanel), waferSequencer.CurrentElapsedSeconds, waferSequencer.CurrentTotalSeconds);
            }
        }
```

- [ ] **Step 3: Delete the now-dead simulation methods**

Delete these four methods entirely (they were only reachable from the old `AdvanceCurrentProcessRecipeStep`/`isProcessRecipeRunning` branch just replaced in Step 2): `AdvanceCurrentProcessRecipeStep`, `StartCurrentProcessRecipeStep`, `CompleteProcessRecipeRun`, `GetCurrentProcessRecipeState` (originally lines 443-512). Leave `AdvanceChamberProcess`, `ChamberProcessState`, and the PM-Setting-button simulation path untouched — they're a separate (currently hidden-by-UI) feature and out of scope.

- [ ] **Step 4: Replace `btn_Start_Click` (lines 906-950)**

```csharp
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
            isProcessRecipeRunning = true;
            isProcessRecipePaused = false;

            List<ChamberRecipeSelection> moduleSteps = recipe.Steps.Select(s => s.Recipe).ToList();
            waferSequencer.Start(AutoSequenceBuilder.Build(main, moduleSteps));
            UpdateAutoSequenceDisplay();
            chamberProcessTimer.Start();
        }
```

- [ ] **Step 5: Replace `btn_Abort_Click` (lines 972-988)**

```csharp
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

            if (isProcessRecipeRunning)
            {
                waferSequencer.Abort("사용자 Abort 요청");
                UpdateAutoSequenceDisplay();
                isProcessRecipeRunning = false;
            }

            isProcessRecipePaused = false;
            currentProcessRecipe = null;
        }
```

`btn_Pause_Click`/`btn_Continue_Click` need no changes — they already stop/start the shared `chamberProcessTimer`, which is exactly what gates `waferSequencer.Tick()`, so pausing already freezes both the elapsed-time and sensor-timeout counters without any sequencer-side flag.

- [ ] **Step 6: Build verification**

Run the MSBuild command. Expected: build succeeds, no errors, no "unused field/method" concerns beyond normal warnings.

- [ ] **Step 7: Commit**

```bash
git add Features/Operation/CurrentStateGUI.cs
git commit -m "Drive Start/Pause/Continue/Abort with real WaferAutoSequencer instead of timer simulation"
```

---

### Task 7: Manual hardware verification (cannot be automated)

**Files:** none — this is a checklist to run against the real machine.

- [ ] Home both axes (Maint screen), confirm `IsChamberDoorClosed`/`IsCylinderBack` read true at rest via a quick Maint-screen sensor check.
- [ ] Create/select a Process Recipe JSON with a single step targeting PM A and a short `ProcessTime` (e.g. 10s) for a fast smoke test.
- [ ] Load a wafer into FOUP A slot 1 physically, click `Start` on the Operation screen.
- [ ] Watch the PM A panel's message label step through: 로딩 준비 → 로딩 중 → 공정 진행 중 (progress bar fills over ~10s) → 언로딩 중.
- [ ] Confirm the wafer physically ends up in FOUP B slot 1 and the "Process Recipe가 완료되었습니다" dialog appears.
- [ ] Repeat with a 2-step recipe (PM A then PM B) and confirm the robot goes directly from PM A to PM B without visiting a FOUP in between.
- [ ] Test Abort mid-sequence: confirm it stops immediately and shows the confirmation dialog; then use the Maint screen to manually return the robot to a safe position.
- [ ] Test the sensor timeout: temporarily block a door sensor (or door) so `IsChamberDoorOpen` never returns true, confirm the run auto-aborts within ~10s with the "센서 응답이 ... 확인되지 않았습니다" message.

- [ ] **Commit** (only if the checklist above surfaces a fix — otherwise nothing to commit for this task)
