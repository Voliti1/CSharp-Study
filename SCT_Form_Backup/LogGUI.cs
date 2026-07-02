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
    public partial class LogGUI : UserControl
    {
        private MainGUI main;
        public LogGUI()
        {
            InitializeComponent();
        }

        public LogGUI(MainGUI mainGUI)
        {
            InitializeComponent();
            this.main = mainGUI;
        }
    }
}
