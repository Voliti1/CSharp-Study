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
    public partial class CurrentStateGUI : UserControl
    {
        private MainGUI main;
        public CurrentStateGUI(MainGUI mainGUI)
        {
            InitializeComponent();
            this.main = mainGUI;
        }
    }
}
