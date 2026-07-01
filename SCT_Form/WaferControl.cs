using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SCT_Form
{
    public partial class WaferControl : UserControl
    {
        public enum WaferState
        {
            Empty,
            Present
        }

        private WaferState state = WaferState.Empty;

        public WaferState State
        {
            get => state;
            set
            {
                state = value;
                Invalidate(); 
            }
        }

        public WaferControl()
        {
            InitializeComponent();

            Size = new Size(120, 120);
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color fillColor;

            switch (State)
            {
                case WaferState.Present:
                    fillColor = Color.Gold;      // 웨이퍼 있음
                    break;

                default:
                    fillColor = Color.Transparent;     // 웨이퍼 없음
                    break;
            }

            Rectangle rect = new Rectangle(2, 2, Width - 4, Height - 4);

            using (SolidBrush brush = new SolidBrush(fillColor))
            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.FillEllipse(brush, rect);
                e.Graphics.DrawEllipse(pen, rect);
            }
        }
    }
}