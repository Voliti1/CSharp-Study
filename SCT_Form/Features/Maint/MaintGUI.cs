using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class MaintGUI : UserControl
    {
        // 부모인 MainGUI의 자원(EtherCAT, 로그, 패널색상)을 쓰기 위한 참조 변수
        private MainGUI main;
        private const int RobotCylinderFrontSensorInput = 13;
        private const int RobotCylinderBackSensorInput = 12;
        private const int ChamberADoorUpSensorInput = 6;
        private const int ChamberADoorDownSensorInput = 7;
        private const int ChamberBDoorUpSensorInput = 8;
        private const int ChamberBDoorDownSensorInput = 9;
        private const int ChamberCDoorUpSensorInput = 10;
        private const int ChamberCDoorDownSensorInput = 11;
        
        // 기본 생성자 (디자이너 뷰 호환용)
        public MaintGUI()
        {
            InitializeComponent();
        }

        // 실전 가동용 생성자 (MainGUI에서 호출할 때 this를 받아옴)
        public MaintGUI(MainGUI mainGUI)
        {
            InitializeComponent();
            this.main = mainGUI;
        }

        private bool CanOperateEquipment()
        {
            return main == null || main.EnsureEquipmentOperationAllowed();
        }

        private bool CanOperateCylinder()
        {
            if (main == null || main.IsLoggedIn) return true;

            MessageBox.Show("Login is required to operate equipment.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        private bool CanCloseChamberDoor()
        {
            if (main == null || main.IsLoggedIn) return true;

            MessageBox.Show("장비 동작을 하려면 로그인해주세요", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

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

        private bool IsRobotCylinderForward()
        {
            return main.IsCylinderForward();
        }

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
