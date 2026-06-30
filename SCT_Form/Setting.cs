using IEG3268_Dll;
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
    public partial class Setting : Form
    {
        private MainGUI _mainGUI;
        private IEG3268 EtherCAT_M;

        public Setting(MainGUI mainGUI)
        {
            InitializeComponent();

            this._mainGUI = mainGUI;
            // 3. 메인 GUI가 생성해서 이미 하드웨어 포트를 열어둔 EtherCAT_M 객체를 그대로 이어받습니다.
            this.EtherCAT_M = mainGUI.EtherCAT_M;
        }

        private void btn_ParameterSet_Click(object sender, EventArgs e)
        {
            EtherCAT_M.Axis1_UD_Config_Update((Int64)nUpDown_Accel.Value, (Int64)nUpDown_Decel.Value, (Int64)nUpDown_MaxVelo.Value, (Int64)nUpDown_Velo.Value);
            EtherCAT_M.Axis2_LR_Config_Update((Int64)nUpDown_Accel.Value, (Int64)nUpDown_Decel.Value, (Int64)nUpDown_MaxVelo.Value, (Int64)nUpDown_Velo.Value);

            _mainGUI.WriteSystemLog("INFO", "Setting 화면: 축(Axis 1, 2) 파라미터 설정 업데이트 완료");
        }
    }
}
