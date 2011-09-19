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

        public List<Point> PointsHeartrate = new List<Point>();
        public List<Point> PointsRPM = new List<Point>();
        public List<Point> PointsSpeed = new List<Point>();
        public List<Point> PointsDistance = new List<Point>();
        public List<Point> PointsPower = new List<Point>();
        public List<Point> PointsEnergy = new List<Point>();
        public List<Point> PointsCurrentPower = new List<Point>();
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
                            PointsHeartrate.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < PointsHeartrate.Count; i++)
                            {
                                Point point = PointsHeartrate[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                PointsHeartrate[i] = point;
                            }
                            for (int j = PointsHeartrate.Count - 1; j > 0; j--)
                            {
                                Point point = PointsHeartrate[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                PointsHeartrate[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < PointsHeartrate.Count; k++)
                            {
                                if (PointsHeartrate[k].X < 0)
                                {
                                    PointsHeartrate.RemoveAt(k);
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetRPM() / 1.2 + 5));
                            this.PointsRPM.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.PointsRPM.Count; i++)
                            {
                                Point point = this.PointsRPM[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsRPM[i] = point;
                            }
                            for (int j = this.PointsRPM.Count - 1; j > 0; j--)
                            {
                                Point point = this.PointsRPM[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsRPM[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.PointsRPM.Count; k++)
                            {
                                if (this.PointsRPM[k].X < 0)
                                {
                                    this.PointsRPM.RemoveAt(k);
                                }
                            }
                            break;
                        }

                    case 2:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-((int)c.GetBike().GetSpeed() / 4.2 + 5));
                            this.PointsSpeed.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.PointsSpeed.Count; i++)
                            {
                                Point point = this.PointsSpeed[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsSpeed[i] = point;
                            }
                            for (int j = this.PointsSpeed.Count - 1; j > 0; j--)
                            {
                                Point point = this.PointsSpeed[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsSpeed[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.PointsSpeed.Count; k++)
                            {
                                if (this.PointsSpeed[k].X < 0)
                                {
                                    this.PointsSpeed.RemoveAt(k);
                                }
                            }
                            break;
                        }

                    case 3:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetDistance() / 600 + 5));
                            this.PointsDistance.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.PointsDistance.Count; i++)
                            {
                                Point point = this.PointsDistance[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsDistance[i] = point;
                            }
                            for (int j = this.PointsDistance.Count - 1; j > 0; j--)
                            {
                                Point point = this.PointsDistance[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsDistance[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.PointsDistance.Count; k++)
                            {
                                if (this.PointsDistance[k].X < 0)
                                {
                                    this.PointsDistance.RemoveAt(k);
                                }
                            }
                            break;
                        }

                    case 4:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetPower() / 2.4 + 5));
                            this.PointsPower.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.PointsPower.Count; i++)
                            {
                                Point point = this.PointsPower[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsPower[i] = point;
                            }
                            for (int j = this.PointsPower.Count - 1; j > 0; j--)
                            {
                                Point point = this.PointsPower[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsPower[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.PointsPower.Count; k++)
                            {
                                if (this.PointsPower[k].X < 0)
                                {
                                    this.PointsPower.RemoveAt(k);
                                }
                            }
                            break;
                        }

                    case 5:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetEnergy() / 600 + 5));
                            this.PointsEnergy.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.PointsEnergy.Count; i++)
                            {
                                Point point = this.PointsEnergy[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsEnergy[i] = point;
                            }
                            for (int j = this.PointsEnergy.Count - 1; j > 0; j--)
                            {
                                Point point = this.PointsEnergy[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsEnergy[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.PointsEnergy.Count; k++)
                            {
                                if (this.PointsEnergy[k].X < 0)
                                {
                                    this.PointsEnergy.RemoveAt(k);
                                }
                            }
                            break;
                        }

                    case 6:
                        {
                            g.TranslateTransform(0, c.GetPanel1().Height);
                            Point newPoint = new Point(c.GetPanel1().Width, (int)-(c.GetBike().GetCurrentPower() / 2.4 + 5));
                            this.PointsCurrentPower.Add(newPoint);
                            GraphicsPath path = new GraphicsPath();
                            path.StartFigure();
                            for (int i = 0; i < this.PointsCurrentPower.Count; i++)
                            {
                                Point point = this.PointsCurrentPower[i];
                                point.X -= 4;
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsCurrentPower[i] = point;
                            }
                            for (int j = this.PointsCurrentPower.Count - 1; j > 0; j--)
                            {
                                Point point = this.PointsCurrentPower[j];
                                path.AddLine(oldPoint, point);
                                oldPoint = point;
                                this.PointsCurrentPower[j] = point;
                            }
                            g.DrawPath(p, path);
                            for (int k = 0; k < this.PointsCurrentPower.Count; k++)
                            {
                                if (this.PointsCurrentPower[k].X < 0)
                                {
                                    this.PointsCurrentPower.RemoveAt(k);
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
