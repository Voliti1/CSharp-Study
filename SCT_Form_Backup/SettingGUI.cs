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
    public partial class SettingGUI : UserControl
    {
        private MainGUI main;
        public SettingGUI()
        {
            InitializeComponent();
            btn_ParameterSet.Click += btn_ParameterSet_Click;
        }

        public SettingGUI(MainGUI mainGUI)
        {
            InitializeComponent();
            this.main = mainGUI;
            btn_ParameterSet.Click += btn_ParameterSet_Click;
        }

        private void btn_ParameterSet_Click(object sender, EventArgs e)
        {
            ApplyRobotAxisConfig();
        }

        internal void ApplyRobotAxisConfig()
        {
            if (main == null) return;

            main.SetRobotAxisConfig(
                Convert.ToInt64(nUpDown_Accel.Value),
                Convert.ToInt64(nUpDown_Decel.Value),
                Convert.ToInt64(nUpDown_MaxVelo.Value),
                Convert.ToInt64(nUpDown_Velo.Value));
        }
    }
}
