using System;
using System.Drawing;
using System.Windows.Forms;

namespace SCT_Form
{
    // 도어 상태 라벨, FOUP A/B 슬롯 1~5 색상, PM 웨이퍼 유무 표시(WaferControl) 등
    // "지금 장비가 어떤 상태인지"를 보여주는 화면 갱신 전용 메서드 모음.
    // 자동 시퀀스(AutoSequenceBuilder)가 setFoupSlotState/setModuleWaferState 콜백으로
    // SetFoupSlotState/SetModuleWaferState를 호출해서 이 화면들을 갱신한다.
    public partial class CurrentStateGUI
    {
        internal void RefreshDoorStatusLabels()
        {
            if (main == null) return;

            SetDoorStatusLabel(lbl_PMA_DoorStatus, "PM A");
            SetDoorStatusLabel(lbl_PMB_DoorStatus, "PM B");
            SetDoorStatusLabel(lbl_PMC_DoorStatus, "PM C");
        }

        internal void SetDoorStatus(string pmName, bool isOpen)
        {
            if (pmName == "PM A")
            {
                SetDoorStatusLabel(lbl_PMA_DoorStatus, pmName);
            }
            else if (pmName == "PM B")
            {
                SetDoorStatusLabel(lbl_PMB_DoorStatus, pmName);
            }
            else if (pmName == "PM C")
            {
                SetDoorStatusLabel(lbl_PMC_DoorStatus, pmName);
            }
        }

        private void SetDoorStatusLabel(Label label, string module)
        {
            if (label == null) return;

            bool isOpen = main != null && main.IsChamberDoorOpen(module);
            bool isClosed = main != null && main.IsChamberDoorClosed(module);

            if (isOpen && !isClosed)
            {
                label.Text = "Door Open";
                label.ForeColor = Color.Goldenrod;
            }
            else if (!isOpen && isClosed)
            {
                label.Text = "Door Close";
                label.ForeColor = Color.DimGray;
            }
            else
            {
                label.Text = "Door Check";
                label.ForeColor = Color.Firebrick;
            }
        }

        private void SetFoupAColor(Color color)
        {
            SetPanelColors(color, pnl_FOUP_A_5, pnl_FOUP_A_4, pnl_FOUP_A_3, pnl_FOUP_A_2, pnl_FOUP_A_1);
        }

        private void SetFoupBColor(Color color)
        {
            SetPanelColors(color, pnl_FOUP_B_5, pnl_FOUP_B_4, pnl_FOUP_B_3, pnl_FOUP_B_2, pnl_FOUP_B_1);
        }

        private void ResetAutoWaferDisplay()
        {
            SetFoupAColor(FoupFullColor);
            SetFoupBColor(FoupEmptyColor);
            SetModuleWaferState("PM A", false);
            SetModuleWaferState("PM B", false);
            SetModuleWaferState("PM C", false);
        }

        private void SetFoupSlotState(string foup, int slot, bool hasWafer)
        {
            Panel slotPanel = GetFoupSlotPanel(foup, slot);
            if (slotPanel == null) return;

            slotPanel.BackColor = hasWafer ? FoupFullColor : FoupEmptyColor;
        }

        private Panel GetFoupSlotPanel(string foup, int slot)
        {
            bool isFoupA = string.Equals(foup, "FOUP A", StringComparison.OrdinalIgnoreCase);
            bool isFoupB = string.Equals(foup, "FOUP B", StringComparison.OrdinalIgnoreCase);
            if (!isFoupA && !isFoupB) return null;

            if (isFoupA)
            {
                if (slot == 1) return pnl_FOUP_A_1;
                if (slot == 2) return pnl_FOUP_A_2;
                if (slot == 3) return pnl_FOUP_A_3;
                if (slot == 4) return pnl_FOUP_A_4;
                if (slot == 5) return pnl_FOUP_A_5;
            }

            if (slot == 1) return pnl_FOUP_B_1;
            if (slot == 2) return pnl_FOUP_B_2;
            if (slot == 3) return pnl_FOUP_B_3;
            if (slot == 4) return pnl_FOUP_B_4;
            if (slot == 5) return pnl_FOUP_B_5;

            return null;
        }

        private void SetModuleWaferState(string module, bool hasWafer)
        {
            WaferControl waferControl = GetModuleWaferControl(module);
            if (waferControl == null) return;

            waferControl.State = hasWafer ? WaferControl.WaferState.Present : WaferControl.WaferState.Empty;
        }

        private WaferControl GetModuleWaferControl(string module)
        {
            string normalizedModule = EquipmentLayout.NormalizeModule(module);
            if (normalizedModule == "PM A") return waferControl2;
            if (normalizedModule == "PM B") return waferControl1;
            if (normalizedModule == "PM C") return waferControl3;
            return null;
        }

        private void SetPanelColors(Color color, params Panel[] panels)
        {
            foreach (Panel panel in panels)
            {
                panel.BackColor = color;
            }
        }
    }
}
