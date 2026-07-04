using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class MaintGUI : UserControl
    {
        // ⭐ 부모인 MainGUI의 자원(EtherCAT, 로그, 패널색상)을 쓰기 위한 참조 변수
        private MainGUI main;
        private const int RobotCylinderFrontSensorInput = 13;
        private const int RobotCylinderBackSensorInput = 12;
        
        // 기본 생성자 (디자이너 뷰 호환용)
        public MaintGUI()
        {
            InitializeComponent();
        }

        // ⭐ 실전 가동용 생성자 (MainGUI에서 호출할 때 this를 받아옴)
        public MaintGUI(MainGUI mainGUI)
        {
            InitializeComponent();
            this.main = mainGUI;
        }

        private bool CanOperateEquipment()
        {
            return main == null || main.EnsureEquipmentOperationAllowed();
        }

        // --- Chamber A 제어 영역 ---
        private void btn_Cham_A_Door_OPEN_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber A 도어 OPEN 명령 요청");

            main.EtherCAT_M.Digital_Output(5, true);
            main.EtherCAT_M.Digital_Output(4, false);
            pnl_Cham_A_Door.BackColor = Color.Red;
            main.SetChamberDoorStatus("PM A", true);

            main.WriteSystemLog("INFO", "Chamber A 도어 OPEN 완료");
        }

        private void btn_Cham_A_Door_CLOSE_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber A 도어 CLOSE 명령 요청");

            main.EtherCAT_M.Digital_Output(5, false);
            main.EtherCAT_M.Digital_Output(4, true);

            pnl_Cham_A_Door.BackColor = Color.LightGray;
            main.SetChamberDoorStatus("PM A", false);
            main.WriteSystemLog("INFO", "Chamber A 도어 CLOSE 완료");
        }

        private void btn_Cham_A_Lamp_ON_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber A 램프 ON 명령 요청");

            main.EtherCAT_M.Digital_Output(3, true);
            pnl_Cham_A_Lamp.BackColor = Color.LimeGreen;
            main.WriteSystemLog("INFO", "Chamber A 램프 ON 완료 (박막생성 공정 시작)");

            main.EtherCAT_M.Digital_Output(1, false);
            main.EtherCAT_M.Digital_Output(2, true);
            main.WriteSystemLog("INFO", "타워램프 자동 변경: 황색등 ➡️ 녹색등(Green) ON (공정중)");
        }

        private void btn_Cham_A_Lamp_OFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber A 램프 OFF 명령 요청");

            main.EtherCAT_M.Digital_Output(3, false);
            pnl_Cham_A_Lamp.BackColor = Color.LightGray;
            main.WriteSystemLog("INFO", "Chamber A 램프 OFF 완료 (박막생성 공정 종료)");

            main.EtherCAT_M.Digital_Output(2, false);
            main.EtherCAT_M.Digital_Output(1, true);
            main.WriteSystemLog("INFO", "타워램프 자동 변경: 전 챔버 공정 종료 ➡️ 황색등(Yellow) ON");
        }

        // --- Chamber B 제어 영역 ---
        private void btn_Cham_B_Door_OPEN_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber B 도어 OPEN 명령 요청");

            main.EtherCAT_M.Digital_Output(8, true);
            main.EtherCAT_M.Digital_Output(7, false);

            pnl_Cham_B_Door.BackColor = Color.Red;
            main.SetChamberDoorStatus("PM B", true);
            main.WriteSystemLog("INFO", "Chamber B 도어 OPEN 완료");
        }

        private void btn_Cham_B_Door_CLOSE_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber B 도어 CLOSE 명령 요청");

            main.EtherCAT_M.Digital_Output(8, false);
            main.EtherCAT_M.Digital_Output(7, true);

            pnl_Cham_B_Door.BackColor = Color.LightGray;
            main.SetChamberDoorStatus("PM B", false);
            main.WriteSystemLog("INFO", "Chamber B 도어 CLOSE 완료");
        }

        private void btn_Cham_B_Lamp_ON_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber B 램프 ON 명령 요청");

            main.EtherCAT_M.Digital_Output(6, true);
            pnl_Cham_B_Lamp.BackColor = Color.LimeGreen;
            main.WriteSystemLog("INFO", "Chamber B 램프 ON 완료 (CMP 공정 시작)");

            main.EtherCAT_M.Digital_Output(1, false);
            main.EtherCAT_M.Digital_Output(2, true);
            main.WriteSystemLog("INFO", "타워램프 자동 변경: 황색등 ➡️ 녹색등(Green) ON (공정중)");
        }

        private void btn_Cham_B_Lamp_OFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber B 램프 OFF 명령 요청");

            main.EtherCAT_M.Digital_Output(6, false);
            pnl_Cham_B_Lamp.BackColor = Color.LightGray;
            main.WriteSystemLog("INFO", "Chamber B 램프 OFF 완료 (CMP 공정 종료)");

            main.EtherCAT_M.Digital_Output(1, true);
            main.EtherCAT_M.Digital_Output(2, false);
            main.WriteSystemLog("INFO", "타워램프 자동 변경: 전 챔버 공정 종료 ➡️ 황색등(Yellow) ON");
        }

        // --- Chamber C 제어 영역 ---
        private void btn_Cham_C_Door_OPEN_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber C 도어 OPEN 명령 요청");

            main.EtherCAT_M.Digital_Output(11, true);
            main.EtherCAT_M.Digital_Output(10, false);
            pnl_Cham_C_Door.BackColor = Color.Red;
            main.SetChamberDoorStatus("PM C", true);
            main.WriteSystemLog("INFO", "Chamber C 도어 OPEN 완료");
        }

        private void btn_Cham_C_Door_CLOSE_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber C 도어 CLOSE 명령 요청");

            main.EtherCAT_M.Digital_Output(11, false);
            main.EtherCAT_M.Digital_Output(10, true);
            pnl_Cham_C_Door.BackColor = Color.LightGray;
            main.SetChamberDoorStatus("PM C", false);
            main.WriteSystemLog("INFO", "Chamber C 도어 CLOSE 완료");
        }

        private void btn_Cham_C_Lamp_ON_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber C 램프 ON 명령 요청");

            main.EtherCAT_M.Digital_Output(9, true);
            pnl_Cham_C_Lamp.BackColor = Color.LimeGreen;
            main.WriteSystemLog("INFO", "Chamber C 램프 ON 완료 (세정 공정 시작)");

            main.EtherCAT_M.Digital_Output(1, false);
            main.EtherCAT_M.Digital_Output(2, true);
            main.WriteSystemLog("INFO", "타워램프 자동 변경: 황색등 ➡️ 녹색등(Green) ON (공정중)");
        }

        private void btn_Cham_C_Lamp_OFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.WriteSystemLog("INFO", "수동 제어: Chamber C 램프 OFF 명령 요청");

            main.EtherCAT_M.Digital_Output(9, false);
            pnl_Cham_C_Lamp.BackColor = Color.LightGray;
            main.WriteSystemLog("INFO", "Chamber C 램프 OFF 완료 (세정 공정 종료)");


            main.EtherCAT_M.Digital_Output(1, true);
            main.EtherCAT_M.Digital_Output(2, false);
            main.WriteSystemLog("INFO", "타워램프 자동 변경: 전 챔버 공정 종료 ➡️ 황색등(Yellow) ON");
        }

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
            main.EtherCAT_M.Axis1_UD_Homming();
        }

        private void btn_LRBasic_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.EtherCAT_M.Axis2_LR_Homming();
        }

        private void btn_MoveUp_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            Int64 currentUDPos = GetCurrentPosition(lbl_UDcurrentPos, "UD");
            Int64 pos = currentUDPos + Convert.ToInt64(nUpDown_MovementDistance.Value);

            main.EtherCAT_M.Axis1_UD_POS_Update(pos);
            main.EtherCAT_M.Axis1_UD_Move_Send();
        }

        private void btn_MoveDown_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            Int64 currentUDPos = GetCurrentPosition(lbl_UDcurrentPos, "UD");
            Int64 pos = currentUDPos - Convert.ToInt64(nUpDown_MovementDistance.Value);

            main.EtherCAT_M.Axis1_UD_POS_Update(pos);
            main.EtherCAT_M.Axis1_UD_Move_Send();
        }

        private void btn_MoveLeft_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            Int64 currentLRPos = GetCurrentPosition(lbl_LRcurrentPos, "LR");
            Int64 pos = currentLRPos + Convert.ToInt64(nUpDown_MovementDistance.Value);

            main.EtherCAT_M.Axis2_LR_POS_Update(pos);
            main.EtherCAT_M.Axis2_LR_Move_Send();
        }

        private void btn_MoveRight_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            Int64 currentLRPos = GetCurrentPosition(lbl_LRcurrentPos, "LR");
            Int64 pos = currentLRPos - Convert.ToInt64(nUpDown_MovementDistance.Value);

            main.EtherCAT_M.Axis2_LR_POS_Update(pos);
            main.EtherCAT_M.Axis2_LR_Move_Send();
        }

        private void btn_UDMove_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            main.EtherCAT_M.Axis1_UD_POS_Update((Int64)pnl_TargetPosition.Value);
            main.EtherCAT_M.Axis1_UD_Move_Send();
        }

        private void btn_LRMove_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            if (IsRobotCylinderForward())
            {
                MessageBox.Show("웨이퍼 이송 실린더가 전진되어 있어 이동할 수 없습니다.");
                return;
            }

            main.EtherCAT_M.Axis2_LR_POS_Update((Int64)pnl_TargetPosition.Value);
            main.EtherCAT_M.Axis2_LR_Move_Send();
        }

        private void btn_InOn_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.EtherCAT_M.Digital_Output(14, true);
        }

        private void btn_InOFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.EtherCAT_M.Digital_Output(14, false);
        }

        private void btn_ExON_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.EtherCAT_M.Digital_Output(15, true);
        }

        private void btn_ExOFF_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.EtherCAT_M.Digital_Output(15, false);
        }

        private void btn_moveFront_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.EtherCAT_M.Digital_Output(13, false);
            main.EtherCAT_M.Digital_Output(12, true);
        }

        private void btn_moveBack_Click(object sender, EventArgs e)
        {
            if (!CanOperateEquipment()) return;
            main.EtherCAT_M.Digital_Output(12, false);
            main.EtherCAT_M.Digital_Output(13, true);
        }

        public void SetCurrentPositionLabel(string currentUDPos, string currentLRPos)
        {
            lbl_UDcurrentPos.Text = currentUDPos;
            lbl_LRcurrentPos.Text = currentLRPos;

            bool isFront = main.EtherCAT_M.Digital_Input(RobotCylinderFrontSensorInput);
            bool isBack = main.EtherCAT_M.Digital_Input(RobotCylinderBackSensorInput);

            label1.Text = isFront.ToString();
            label2.Text = isBack.ToString();
        }

        private long GetCurrentPosition(Label currentPositionLabel, string axisName)
        {
            if (long.TryParse(currentPositionLabel.Text, NumberStyles.Integer | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out long currentPosition))
            {
                return currentPosition;
            }

            main?.WriteSystemLog("WARN", $"{axisName} current position parse failed. Value: {currentPositionLabel.Text}");
            return 0;
        }

        private bool IsRobotCylinderForward()
        {
            bool isFront = main.EtherCAT_M.Digital_Input(RobotCylinderFrontSensorInput);
            bool isBack = main.EtherCAT_M.Digital_Input(RobotCylinderBackSensorInput);

            return isFront && !isBack;
        }
    }
}
