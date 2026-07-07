using System;
using System.Collections.Generic;

namespace SCT_Form
{
    // Process Recipe(선택된 PM 경로 + 각 PM의 Process Time)를 받아서, 실제 로봇/챔버 하드웨어를
    // 순서대로 움직이는 WaferAutoSequencer.AutoStep 리스트로 변환하는 빌더.
    // 여기서 만든 리스트를 CurrentStateGUI.btn_Start_Click이 WaferAutoSequencer.Start()에 넘기면
    // chamberProcessTimer(1초 tick)가 그 리스트를 순서대로 실행한다.
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

        // 진입점. FOUP A 슬롯 1~5(WaferSlotCount)를 순서대로 돌면서, 각 슬롯마다
        // "FOUP A에서 웨이퍼를 꺼냄 → 선택된 Process Recipe의 PM들을 순서대로 처리 →
        // FOUP B 같은 슬롯 번호에 넣음"을 반복하는 하나의 연속된 스텝 리스트를 만든다.
        // 슬롯 2~5로 넘어갈 때 별도의 "인식" 로직은 없다 - 그냥 리스트에서 다음 순서가 그 스텝이라
        // WaferAutoSequencer가 이전 슬롯을 끝내자마자 자연스럽게 이어서 실행한다.
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

        // 즉시 실행되고 끝나는 하드웨어 동작 1개(축 이동 명령, 도어 열기, 램프 on/off 등)를
        // AutoStep으로 감싼다. WaferAutoSequencer는 Action 스텝을 만나면 그 자리에서 Execute()를
        // 호출하고 바로 다음 스텝으로 넘어간다(대기하지 않음).
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

        // 센서 조건(isSatisfied)이 true가 될 때까지 매 tick마다 확인하며 기다리는 스텝.
        // timeoutSeconds 안에 만족되지 않으면 WaferAutoSequencer.Abort()로 시퀀스가 중단된다.
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

        // 정해진 초(seconds)만큼 그냥 시간이 흐르기를 기다리는 스텝(센서 확인 없음).
        // PM 프로세스 진행 시간(AddProcessWait)이나 진공/배기 안정화 대기에 쓰인다.
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
        // UD(상하) 축 이동 명령 + "목표 위치 도달 확인 대기"를 한 쌍으로 묶어서 추가한다.
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

        // LR(좌우) 축 이동 명령 + "목표 위치 도달 확인 대기"를 한 쌍으로 묶어서 추가한다.
        // AddAxis1Move와 동일한 패턴이며 대상 축만 다르다.
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

        // FoupProfile의 Wafer1Up~Wafer5Up 중 슬롯 번호에 맞는 값을 골라준다.
        // (필드가 슬롯별로 따로 선언되어 있어서 배열/딕셔너리 대신 switch로 매핑)
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

        // GetWaferUp과 동일하되 Wafer1Down~Wafer5Down(하강 위치)을 매핑한다.
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

        // FOUP(A/B) 지정 슬롯에서 웨이퍼 1장을 꺼내 로봇 팔에 흡착시키는 전체 동작 시퀀스:
        // 위치 이동 → 하강 → 실린더 전진 → 상승(웨이퍼 들어올림) → 흡착 on → 실린더 후진.
        // 마지막에 setFoupSlotState로 "이 슬롯은 비었음" UI 갱신 콜백을 호출한다.
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

        // 로봇 팔에 흡착된 웨이퍼를 지정 PM 챔버 안에 내려놓는 전체 동작 시퀀스:
        // 위치 이동 → 로봇이 챔버 쪽을 보고 있는지 확인 → 도어 열기 확인 → 실린더 전진 →
        // 흡착 해제 + 배기 on → 하강(웨이퍼를 챔버 바닥에 내려놓음) → 실린더 후진 → 도어 닫기 확인.
        // 마지막에 setModuleWaferState로 "이 PM에 웨이퍼 있음" UI 갱신 콜백을 호출한다.
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

        // PM에서 실제 공정이 진행되는 구간. 램프 on → ProcessTime만큼 대기(PM 정보창 Time이
        // 여기서 elapsed/total로 올라간다) → Time이 total에 도달하면(=공정 종료) 램프를 즉시 끄는 대신
        // BlinkChamberLamp로 1초 간격 On/Off 5회 깜빡이게 한다(로봇의 다음 동작은 이 깜빡임과
        // 무관하게 바로 이어서 진행됨 - BlinkChamberLamp는 백그라운드 타이머라 여기서 블로킹하지 않는다).
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

        // 공정이 끝난 웨이퍼를 PM 챔버에서 꺼내 로봇 팔에 흡착시키는 전체 동작 시퀀스.
        // AddPlaceIntoModule의 역순(도어 열기 → 하강 → 상승/흡착 → 도어 닫기)이며,
        // 마지막에 setModuleWaferState로 "이 PM은 비었음" UI 갱신 콜백을 호출한다.
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

        // 로봇 팔에 흡착된 웨이퍼를 FOUP(A/B) 지정 슬롯에 넣는 전체 동작 시퀀스.
        // AddPickFromFoup의 역순(상승 → 흡착 해제 → 하강 → 실린더 후진)이며,
        // 마지막에 setFoupSlotState로 "이 슬롯에 웨이퍼 있음" UI 갱신 콜백을 호출한다.
        // slot 매개변수는 Build()에서 현재 몇 번째 웨이퍼 사이클인지에 따라 1~5로 전달된다.
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
