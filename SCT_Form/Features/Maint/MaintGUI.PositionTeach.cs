using System;
using System.Windows.Forms;

namespace SCT_Form
{
    // FOUP A/B 슬롯 1~5, PM A/B/C의 LR/UD 좌표로 로봇을 직접 이동시키는 "포지션 티칭" 버튼들.
    // 좌표값은 EquipmentLayout에 있는 것과 동일한 상수를 그대로 하드코딩하고 있다(설비 셋업 시
    // 좌표를 눈으로 확인하며 잡는 용도라 EquipmentLayout을 참조하지 않고 여기 직접 적혀 있음).
    public partial class MaintGUI
    {
        private void btn_FOUPA_LRPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            main.MoveAxis2LRTo(13140);
        }

        private void btn_FOUPA_Wafer5_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(2818463);
        }

        private void btn_FOUPA_Wafer5_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(3018457);
        }

        private void btn_FOUPA_Wafer4_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(2119399);
        }

        private void btn_FOUPA_Wafer4_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(2332102);
        }

        private void btn_FOUPA_Wafer3_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(1432388);
        }

        private void btn_FOUPA_Wafer3_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(1627604);
        }

        private void btn_FOUPA_Wafer2_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(781878);
        }

        private void btn_FOUPA_Wafer2_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(982378);
        }

        private void btn_FOUPA_Wafer1_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(100379);
        }

        private void btn_FOUPA_Wafer1_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(302380);
        }

        private void btn_FOUPB_LRPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            main.MoveAxis2LRTo(-395093);
        }

        private void btn_FOUPB_Wafer5_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(2818463);
        }

        private void btn_FOUPB_Wafer5_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(3018457);
        }

        private void btn_FOUPB_Wafer4_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(2119399);
        }

        private void btn_FOUPB_Wafer4_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(2332102);
        }

        private void btn_FOUPB_Wafer3_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(1432388);
        }

        private void btn_FOUPB_Wafer3_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(1627604);
        }

        private void btn_FOUPB_Wafer2_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(781878);
        }

        private void btn_FOUPB_Wafer2_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(982378);
        }

        private void btn_FOUPB_Wafer1_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(100379);
        }

        private void btn_FOUPB_Wafer1_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(302380);
        }

        private void btn_PMA_LRPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            main.MoveAxis2LRTo(-59064);
        }

        private void btn_PMA_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(806931);
        }

        private void btn_PMA_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(1156931);
        }

        private void btn_PMB_LRPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            main.MoveAxis2LRTo(-190823);
        }

        private void btn_PMB_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(806931);
        }

        private void btn_PMB_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(1156931);
        }

        private void btn_PMC_LRPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            main.MoveAxis2LRTo(-322000);
        }

        private void btn_PMC_DownPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(806931);
        }

        private void btn_PMC_UpPos_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;

            main.MoveAxis1UDTo(1156931);
        }
    }
}
