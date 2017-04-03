using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace HonasProtractor
{
    public partial class ProtractorCtrl : UserControl
    {
        public delegate void SizeChangeReqDelegate(SizeF s);
        public event SizeChangeReqDelegate sizeChangeReqEvent;

        public Pen DrawPen = new Pen(Brushes.Black);

        public ProtractorCtrl()
        {
            InitializeComponent();

            this.ResizeRedraw = true;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawProtractor(e.Graphics);
        }



        void DrawProtractor(Graphics g)
        {
            Debug.WriteLine("DrawProtractor");

            float radius = this.Width / 2;
            float centerx = 0;
            float centery = 0;

            g.DrawArc(DrawPen, 0, 0, this.Width, this.Width, 180, 180);


            PointF compensatedCenter = AxisCompensation(new PointF(centerx, centery));
            PointF compensatedStart = compensatedCenter;
            int splitCount = 10;

            // reverse direction of watch 
            for (float degree = 0; degree >= -180; degree -= 1)
            {
                PointF circlep = PointOnCircle(radius, degree, new PointF(centerx, centery));
                PointF compensatedCirclePoint = AxisCompensation(circlep);

                if (degree % 10 == 0)
                {
                    splitCount = 5;
                }
                else if (degree % 5 == 0)
                {
                    splitCount = 10;
                }
                else
                    splitCount = 15;

                float slope = (centery - circlep.Y) / (centerx - circlep.X);
                IList<PointF> points = SplitLine(new PointF(centerx, centery), new PointF(circlep.X, circlep.Y), splitCount);
                PointF startPoint = points[points.Count - 2];

                //if (degree != -90)
                if (degree % 90 != 0)
                {
                    compensatedStart = AxisCompensation(startPoint);
                    
                    // Draw a line between a point on circle and a point away from splitCount.
                    g.DrawLine(DrawPen, compensatedStart, compensatedCirclePoint);
                    
                    // Display degree text and Draw short line for every 10 degrees.
                    if (degree % 10 == 0)
                    {
                        PointF textPos = AxisCompensation(points[points.Count - 3]);
                        g.DrawString((180 + degree).ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, textPos);

                        // Draw a line between center point and a point away from splitCount.
                        startPoint = points[2];
                        compensatedStart = AxisCompensation(startPoint);
                        g.DrawLine(DrawPen, compensatedCenter, compensatedStart);
                    }
                }
                else
                {
                    PointF textPos = AxisCompensation(points[points.Count - 3]);
                    g.DrawString((180 + degree).ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, textPos);

                    compensatedStart = compensatedCenter;
                    // Draw a line between a point on circle and a point away from splitCount.
                    g.DrawLine(DrawPen, compensatedStart, compensatedCirclePoint);
                }
            }
        }
        
        public PointF PointOnCircle(float radius, float angleInDegrees, PointF center)
        {
            // Convert from degrees to radians via multiplication by PI/180        
            float x = (float)(radius * Math.Cos(angleInDegrees * Math.PI / 180F)) + center.X;
            float y = (float)(radius * Math.Sin(angleInDegrees * Math.PI / 180F)) + center.Y;

            return new PointF(x, y);
        }

        public PointF AxisCompensation(PointF orig)
        {
            return new PointF(orig.X + this.Width / 2, orig.Y + this.Width / 2);
        }


        public IList<PointF> SplitLine(PointF a, PointF b, int count)
        {
            count = count + 1;

            Double d = Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y)) / count;
            Double fi = Math.Atan2(b.Y - a.Y, b.X - a.X);

            List<PointF> points = new List<PointF>(count + 1);

            for (int i = 0; i <= count; ++i)
                points.Add(new PointF((float)(a.X + i * d * Math.Cos(fi)), (float)(a.Y + i * d * Math.Sin(fi))));

            return points;
        }


    }
}
