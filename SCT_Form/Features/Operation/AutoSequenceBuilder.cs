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
