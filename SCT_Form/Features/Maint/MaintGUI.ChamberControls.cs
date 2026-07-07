using System;
using System.Drawing;

namespace SCT_Form
{
    // PM A/B/C 챔버 도어 열기/닫기, 챔버 램프 수동 on/off 버튼. 실제 하드웨어 제어는
    // MainGUI의 공용 메서드(OpenChamberDoor/CloseChamberDoor/SetChamberLamp)에 위임하고,
    // 여기서는 로그 기록과 패널 색상(빨강=열림, 연두=램프 on, 회색=기본) 갱신만 담당한다.
    public partial class MaintGUI
    {
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
    }
}
