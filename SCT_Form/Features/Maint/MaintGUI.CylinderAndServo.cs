using System;

namespace SCT_Form
{
    // 서보 on/off, UD/LR 원점복귀, 흡착/배기, 실린더 전진/후진 등 축 이동이 아닌
    // 나머지 수동 하드웨어 버튼들.
    public partial class MaintGUI
    {
        private void btn_ServoON_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.servoMotorON();
        }

        private void btn_ServoOFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.servoMotorOFF();
        }

        private void btn_UDBasic_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.HomeAxis1UD();
        }

        private void btn_LRBasic_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.HomeAxis2LR();
        }

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

        private void btn_ExON_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.SetWaferExhaust(true);
        }

        private void btn_ExOFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.SetWaferExhaust(false);
        }

        private void btn_moveFront_Click(object sender, EventArgs e)
        {
            if (!CanOperateCylinder()) return;
            main.MoveCylinderFront();
        }

        private void btn_moveBack_Click(object sender, EventArgs e)
        {
            if (!CanOperateCylinder()) return;
            main.MoveCylinderBack();
        }

        private bool IsRobotCylinderForward()
        {
            return main.IsCylinderForward();
        }
    }
}
