using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Chart : System.Windows.Forms.Panel
    {
        private Client c;

        private List<Point> pointsHeartrate = new List<Point>();
        private List<Point> pointsRPM = new List<Point>();
        private List<Point> pointsSpeed = new List<Point>();
        private List<Point> pointsDistance = new List<Point>();
        private List<Point> pointsPower = new List<Point>();
        private List<Point> pointsEnergy = new List<Point>();
        private List<Point> pointsCurrentPower = new List<Point>();
        private Point oldPoint;

        public Chart(Client c)
        {
            this.c = c;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            oldPoint = new Point(c.GetPanel1().Width, (int)-(25 / 2.3));
            UpdateStyles();
            this.DoubleBuffered = true;
        }

        public void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            Pen p = new Pen(c.GetPanel1().ForeColor, 3);
            p.LineJoin = LineJoin.Bevel;
            string selectedData ="" +  c.GetComboBox1().SelectedItem;
            SizeF stringsize = g.MeasureString(selectedData, c.GetPanel1().Font);
            g.DrawString(selectedData, c.GetPanel1().Font, p.Brush, new Point(c.GetPanel1().Width - (int)stringsize.Width, 0));
            Console.WriteLine(selectedData);
            if (c.GetBike() is VirtBike)
            {
                // Hell no, prefer comboBox1.SelectedIndex in a switchstate :z
                switch (c.GetComboBox1().SelectedIndex)
                {
                    case 0:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetHeartRate() / 1.5 + 5));
                            pointsHeartrate.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < pointsHeartrate.Count; i++)
                            {
                                Point point = pointsHeartrate[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                pointsHeartrate[i] = point;
                            }
                            for (int j = pointsHeartrate.Count - 1; j > 0; j--)
                            {
                                Point point = pointsHeartrate[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                pointsHeartrate[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < pointsHeartrate.Count; k++)
                            {
                                if (pointsHeartrate[k].X < 0)
                                {
                                    pointsHeartrate.RemoveAt(k);
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetRPM() / 1.2 + 5));
                            this.pointsRPM.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.pointsRPM.Count; i++)
                            {
                                Point point = this.pointsRPM[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsRPM[i] = point;
                            }
                            for (int j = this.pointsRPM.Count - 1; j > 0; j--)
                            {
                                Point point = this.pointsRPM[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsRPM[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.pointsRPM.Count; k++)
                            {
                                if (this.pointsRPM[k].X < 0)
                                {
                                    this.pointsRPM.RemoveAt(k);
                                }
                            }
                            break;
                        }

                    case 2:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-((int)c.GetBike().GetSpeed() / 4.2 + 5));
                            this.pointsSpeed.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.pointsSpeed.Count; i++)
                            {
                                Point point = this.pointsSpeed[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsSpeed[i] = point;
                            }
                            for (int j = this.pointsSpeed.Count - 1; j > 0; j--)
                            {
                                Point point = this.pointsSpeed[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsSpeed[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.pointsSpeed.Count; k++)
                            {
                                if (this.pointsSpeed[k].X < 0)
                                {
                                    this.pointsSpeed.RemoveAt(k);
                                }
                            }
                            break;
                        }

                    case 3:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetDistance() / 600 + 5));
                            this.pointsDistance.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.pointsDistance.Count; i++)
                            {
                                Point point = this.pointsDistance[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsDistance[i] = point;
                            }
                            for (int j = this.pointsDistance.Count - 1; j > 0; j--)
                            {
                                Point point = this.pointsDistance[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsDistance[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.pointsDistance.Count; k++)
                            {
                                if (this.pointsDistance[k].X < 0)
                                {
                                    this.pointsDistance.RemoveAt(k);
                                }
                            }
                            break;
                        }

                    case 4:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetPower() / 2.4 + 5));
                            this.pointsPower.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.pointsPower.Count; i++)
                            {
                                Point point = this.pointsPower[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsPower[i] = point;
                            }
                            for (int j = this.pointsPower.Count - 1; j > 0; j--)
                            {
                                Point point = this.pointsPower[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsPower[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.pointsPower.Count; k++)
                            {
                                if (this.pointsPower[k].X < 0)
                                {
                                    this.pointsPower.RemoveAt(k);
                                }
                            }
                            break;
                        }

                    case 5:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetEnergy() / 600 + 5));
                            this.pointsEnergy.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.pointsEnergy.Count; i++)
                            {
                                Point point = this.pointsEnergy[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsEnergy[i] = point;
                            }
                            for (int j = this.pointsEnergy.Count - 1; j > 0; j--)
                            {
                                Point point = this.pointsEnergy[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsEnergy[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.pointsEnergy.Count; k++)
                            {
                                if (this.pointsEnergy[k].X < 0)
                                {
                                    this.pointsEnergy.RemoveAt(k);
                                }
                            }
                            break;
                        }

                    case 6:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetCurrentPower() / 2.4 + 5));
                            this.pointsCurrentPower.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.pointsCurrentPower.Count; i++)
                            {
                                Point point = this.pointsCurrentPower[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsCurrentPower[i] = point;
                            }
                            for (int j = this.pointsCurrentPower.Count - 1; j > 0; j--)
                            {
                                Point point = this.pointsCurrentPower[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.pointsCurrentPower[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.pointsCurrentPower.Count; k++)
                            {
                                if (this.pointsCurrentPower[k].X < 0)
                                {
                                    this.pointsCurrentPower.RemoveAt(k);
                                }
                            }
                            break;
                        }
                }
                base.OnPaint(e);

                g.Dispose();

            }
        }
    }
}
