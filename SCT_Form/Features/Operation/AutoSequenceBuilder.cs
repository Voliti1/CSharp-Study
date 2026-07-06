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
        private const long Axis1PositionToleranceCounts = 5000;

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

            AddPickFromFoup(steps, main, FoupASource, firstModule, firstModule + " load from FOUP A", setFoupSlotState);

            foreach (ChamberRecipeSelection recipeStep in recipeSteps)
            {
                string module = EquipmentLayout.NormalizeModule(recipeStep.Module);
                AddPlaceIntoModule(steps, main, module, setModuleWaferState);
                AddProcessWait(steps, main, module, recipeStep.ProcessTime);
                AddPickFromModule(steps, main, module, setModuleWaferState);
            }

            AddPlaceIntoFoup(steps, main, FoupBDestination, lastModule, lastModule + " unload to FOUP B", setFoupSlotState);

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
            AddWaitSensor(steps, module, description + " target reached wait", main.IsAxis2TargetReached, AxisMoveTimeoutSeconds);
        }

        private static void AddPickFromFoup(
            List<WaferAutoSequencer.AutoStep> steps,
            MainGUI main,
            string foup,
            string displayModule,
            string label,
            Action<string, int, bool> setFoupSlotState)
        {
            EquipmentLayout.FoupProfile profile = EquipmentLayout.GetFoup(foup);

            AddAxis2Move(steps, main, displayModule, label + " - LR move", profile.LR);
            AddAxis1Move(steps, main, displayModule, label + " - UD down", profile.Wafer1Down);
            AddAction(steps, main, displayModule, label + " - cylinder front", main.MoveCylinderFront);
            AddWaitSensor(steps, displayModule, label + " - cylinder front wait", main.IsCylinderForward, CylinderMoveTimeoutSeconds);
            AddAxis1Move(steps, main, displayModule, label + " - UD up", profile.Wafer1Up);
            AddAction(steps, main, displayModule, label + " - vacuum on", () => main.SetWaferSuction(true));
            AddWaitElapsed(steps, displayModule, label + " - vacuum settle", VacuumSettleSeconds);
            AddAction(steps, main, displayModule, label + " - cylinder back", main.MoveCylinderBack);
            AddWaitSensor(steps, displayModule, label + " - cylinder back wait", main.IsCylinderBack, CylinderMoveTimeoutSeconds);
            AddAction(steps, main, displayModule, label + " - FOUP slot empty", () => setFoupSlotState?.Invoke(foup, 1, false));
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

            AddAction(steps, main, module, module + " process complete", () => main.SetChamberLamp(module, false));
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
            string displayModule,
            string label,
            Action<string, int, bool> setFoupSlotState)
        {
            EquipmentLayout.FoupProfile profile = EquipmentLayout.GetFoup(foup);

            AddAxis2Move(steps, main, displayModule, label + " - LR move", profile.LR);
            AddAxis1Move(steps, main, displayModule, label + " - UD up", profile.Wafer1Up);
            AddAction(steps, main, displayModule, label + " - cylinder front", main.MoveCylinderFront);
            AddWaitSensor(steps, displayModule, label + " - cylinder front wait", main.IsCylinderForward, CylinderMoveTimeoutSeconds);
            AddAction(steps, main, displayModule, label + " - vacuum off", () => main.SetWaferSuction(false));
            AddAction(steps, main, displayModule, label + " - exhaust on", () => main.SetWaferExhaust(true));
            AddWaitElapsed(steps, displayModule, label + " - vacuum settle", VacuumSettleSeconds);
            AddAxis1Move(steps, main, displayModule, label + " - UD down", profile.Wafer1Down);
            AddAction(steps, main, displayModule, label + " - cylinder back", main.MoveCylinderBack);
            AddWaitSensor(steps, displayModule, label + " - cylinder back wait", main.IsCylinderBack, CylinderMoveTimeoutSeconds);
            AddAction(steps, main, displayModule, label + " - exhaust off", () => main.SetWaferExhaust(false));
            AddAction(steps, main, displayModule, label + " - FOUP slot present", () => setFoupSlotState?.Invoke(foup, 1, true));
        }
    }
}
