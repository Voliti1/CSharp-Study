using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SCT_Form
{
    // 로봇 팔의 현재 위치/방향/실린더 전후진/웨이퍼 보유 여부를 화면에 그려 보여주는
    // 그래픽 패널(RobotMapPanel). 실제 로봇 좌표(LR)를 5개 스테이션 좌표와 비교해서
    // 가장 가까운 스테이션을 "바라보는 방향"으로 판단해 회전각을 정한다.
    public partial class CurrentStateGUI
    {
        // 디자이너의 빈 pnl_Robot 자리에 더블버퍼링 되는 RobotMapPanel을 얹어
        // 로봇 상태(방향/전진·후진/웨이퍼 보유)를 그린다.
        private void InitializeRobotPositionMap()
        {
            if (robotPanel != null) return;

            robotPanel = new RobotMapPanel();
            robotPanel.Location = pnl_Robot.Location;
            robotPanel.Size = pnl_Robot.Size;
            robotPanel.Anchor = pnl_Robot.Anchor;
            robotPanel.Margin = pnl_Robot.Margin;
            robotPanel.BackColor = Color.White;
            robotPanel.BorderStyle = BorderStyle.FixedSingle;
            robotPanel.Paint += robotPanel_Paint;

            Controls.Remove(pnl_Robot);
            Controls.Add(robotPanel);
            robotPanel.BringToFront();
        }

        internal void SetRobotWaferState(bool hasWafer)
        {
            robotHasWafer = hasWafer;
            if (robotPanel != null) robotPanel.Invalidate();
        }

        internal void SetRobotCylinderState(bool isForward, bool isBack)
        {
            robotCylinderForward = isForward;
            robotCylinderBack = isBack;
            if (robotPanel != null) robotPanel.Invalidate();
        }

        internal void UpdateRobotPosition(string currentLRPos)
        {
            long parsed;
            if (!long.TryParse(currentLRPos, out parsed))
            {
                // 순간적인 통신 딜레이나 파싱 실패 시, 이전의 정상적인 로봇 그래픽 위치를 유지하여
                // 그래픽이 FOUP A(디폴트 각도) 방향으로 오작동하여 멈춰버리는 현상을 방지합니다.
                return;
            }

            currentRobotLRPosition = parsed;
            RobotTarget nearest = GetNearestRobotTarget(parsed);
            currentRobotFacingName = nearest == null ? "UNKNOWN" : nearest.Name;
            currentRobotFacingDiff = nearest == null ? 0 : Math.Abs(parsed - nearest.LR);
            if (robotPanel != null) robotPanel.Invalidate();
        }

        private RobotTarget GetNearestRobotTarget(long lrPosition)
        {
            RobotTarget nearest = null;
            long nearestDiff = long.MaxValue;

            foreach (RobotTarget target in GetRobotTargets())
            {
                long diff = Math.Abs(lrPosition - target.LR);
                if (diff >= nearestDiff) continue;

                nearest = target;
                nearestDiff = diff;
            }

            return nearest;
        }

        private RobotTarget GetRobotTarget(string targetName)
        {
            foreach (RobotTarget target in GetRobotTargets())
            {
                if (string.Equals(target.Name, targetName, StringComparison.OrdinalIgnoreCase)) return target;
            }

            return null;
        }

        // 각 스테이션을 바라볼 때의 회전 각도(12시=0도, 시계방향 +).
        // PM A=9시(270도), PM B=12시(0도), PM C=3시(90도),
        // FOUP A/B는 화면 배치에 맞춰 좌하단/우하단.
        private List<RobotTarget> GetRobotTargets()
        {
            return new List<RobotTarget>
            {
                new RobotTarget("PM A", EquipmentLayout.GetModule("PM A").LR, 270F),
                new RobotTarget("PM B", EquipmentLayout.GetModule("PM B").LR, 0F),
                new RobotTarget("PM C", EquipmentLayout.GetModule("PM C").LR, 90F),
                new RobotTarget("FOUP A", EquipmentLayout.GetFoup("FOUP A").LR, 225F),
                new RobotTarget("FOUP B", EquipmentLayout.GetFoup("FOUP B").LR, 135F)
            };
        }

        private void robotPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = robotPanel.ClientRectangle;
            PointF robotCenter = new PointF(bounds.Width / 2F, bounds.Height / 2F);
            RobotTarget nearest = currentRobotLRPosition.HasValue ? GetNearestRobotTarget(currentRobotLRPosition.Value) : GetRobotTarget("FOUP A");
            bool isFacingTarget = nearest != null && currentRobotFacingDiff <= RobotFacingDisplayToleranceCounts;
            float rotationDegrees = nearest == null ? 0F : nearest.AngleDegrees;

            DrawPhotoStyleRobot(g, robotCenter, rotationDegrees, isFacingTarget);
        }

        private void DrawPhotoStyleRobot(Graphics g, PointF center, float rotationDegrees, bool isFacingTarget)
        {
            // 실린더 전진 시 그립(엔드이펙터)이 몸체에서 더 멀리 뻗고, 후진 시 몸체 쪽으로 당겨진다.
            float gripCenterY;
            if (robotCylinderForward && !robotCylinderBack) gripCenterY = -98F;
            else if (!robotCylinderForward && robotCylinderBack) gripCenterY = -60F;
            else gripCenterY = -80F;

            GraphicsState state = g.Save();
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(rotationDegrees);

            using (Pen outlinePen = new Pen(Color.Black, 4F))
            using (Pen armPen = new Pen(isFacingTarget ? Color.SeaGreen : Color.DarkOrange, 7F))
            using (Brush bodyBrush = new SolidBrush(Color.FromArgb(238, 241, 246)))
            using (Brush bodyShadowBrush = new SolidBrush(Color.FromArgb(206, 214, 224)))
            using (Brush waferBrush = new SolidBrush(robotHasWafer ? Color.Gold : Color.White))
            using (GraphicsPath bodyPath = new GraphicsPath())
            {
                armPen.StartCap = LineCap.Round;
                armPen.EndCap = LineCap.Round;

                bodyPath.AddRectangle(new RectangleF(-14F, -34F, 28F, 72F));
                g.FillPath(bodyBrush, bodyPath);
                g.FillRectangle(bodyShadowBrush, 3F, -30F, 8F, 64F);
                g.DrawPath(outlinePen, bodyPath);

                // 몸체 -> 그립을 잇는 암 (전진/후진에 따라 길이 변화)
                g.DrawLine(armPen, 0F, -30F, 0F, gripCenterY + 22F);

                RectangleF waferGripRect = new RectangleF(-24F, gripCenterY - 24F, 48F, 48F);
                g.FillEllipse(waferBrush, waferGripRect);
                g.DrawEllipse(outlinePen, waferGripRect);
            }

            g.Restore(state);
        }

        private class RobotTarget
        {
            public RobotTarget(string name, long lr, float angleDegrees)
            {
                Name = name;
                LR = lr;
                AngleDegrees = angleDegrees;
            }

            public string Name { get; private set; }
            public long LR { get; private set; }
            public float AngleDegrees { get; private set; }
        }

        private class RobotMapPanel : Panel
        {
            public RobotMapPanel()
            {
                DoubleBuffered = true;
                ResizeRedraw = true;
            }
        }
    }
}
