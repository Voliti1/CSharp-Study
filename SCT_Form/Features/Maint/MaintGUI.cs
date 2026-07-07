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
    // 이 파일은 필드/생성자와 공용 로그인 게이트(CanOperateEquipment 등)만 담당한다.
    // 나머지 책임은 분리되어 있다:
    // - MaintGUI.ChamberControls.cs: PM A/B/C 도어/램프 수동 버튼
    // - MaintGUI.CylinderAndServo.cs: 서보/원점복귀/흡착/배기/실린더 버튼
    // - MaintGUI.AxisJog.cs: 조그 이동, 좌표 직접 입력 이동, 현재 위치/도어 상태 라벨
    // - MaintGUI.PositionTeach.cs: FOUP/PM 슬롯별 좌표로 바로 이동하는 티칭 버튼
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
    }
}
