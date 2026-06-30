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
    public partial class AutoProcessControl : UserControl
    {
        // 부모 메인 GUI를 담아둘 변수
        private MainGUI main;

        // 기본 생성자를 메인 GUI를 받도록 수정
        public AutoProcessControl(MainGUI mainGUI)
        {
            InitializeComponent();
            this.main = mainGUI; // 부모 등록
        }

        private void btn_AutoStart_Click(object sender, EventArgs e)
        {
            main.WriteSystemLog("INFO", "자동 공정 시퀀스 가동 시작");

            // MainGUI에 있는 EtherCAT 마스터 객체에 직접 명령을 내립니다!
            main.EtherCAT_M.Digital_Output(3, true);
        }
    }
}
