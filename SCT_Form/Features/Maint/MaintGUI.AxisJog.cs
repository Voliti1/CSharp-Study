using System;
using System.Windows.Forms;

namespace SCT_Form
{
    // 수동 조그 이동(위/아래/좌/우 버튼, 거리 입력 nUpDown_MovementDistance)과 목표 좌표 직접
    // 입력 이동(pnl_TargetPosition), 그리고 timer1_Tick에서 매 200ms 갱신되는 현재 좌표/도어
    // 상태 라벨(SetCurrentPositionLabel) 표시.
    public partial class MaintGUI
    {
        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(Convert.ToInt64(nUpDown_MovementDistance.Value));
        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(-Convert.ToInt64(nUpDown_MovementDistance.Value));
        }

        private void btn_MoveLeft_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            main.MoveAxis2LRTo(Convert.ToInt64(nUpDown_MovementDistance.Value));
        }

        private void btn_MoveRight_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            main.MoveAxis2LRTo(-Convert.ToInt64(nUpDown_MovementDistance.Value));
        }

        private void btn_UDMove_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo((Int64)pnl_TargetPosition.Value);
        }

        private void btn_LRMove_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            main.MoveAxis2LRTo((Int64)pnl_TargetPosition.Value);
        }

        public void SetCurrentPositionLabel(string currentUDPos, string currentLRPos)
        {
            lbl_UDcurrentPos.Text = currentUDPos;
            lbl_LRcurrentPos.Text = currentLRPos;

            label1.Text = GetChamberDoorStatusText("A", ChamberADoorUpSensorInput, ChamberADoorDownSensorInput);
            label2.Text = GetChamberDoorStatusText("B", ChamberBDoorUpSensorInput, ChamberBDoorDownSensorInput);
            label3.Text = GetChamberDoorStatusText("C", ChamberCDoorUpSensorInput, ChamberCDoorDownSensorInput);
        }

        private string GetChamberDoorStatusText(string chamberName, int upSensorInput, int downSensorInput)
        {
            bool isDoorUp = main.EtherCAT_M.Digital_Input(upSensorInput);
            bool isDoorDown = main.EtherCAT_M.Digital_Input(downSensorInput);

            if (isDoorUp && !isDoorDown) return chamberName + " 도어 닫힘";
            if (!isDoorUp && isDoorDown) return chamberName + " 도어 열림";

            return chamberName + " 도어 확인 필요";
        }
    }
}
