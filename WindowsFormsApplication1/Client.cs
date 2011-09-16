using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication1
{
    public partial class Client : Form
    {
        private Bike bike;
        private List<BikeData> data;
        private VirtSettings virtSettings;

        //Temp place for the points
        //as soon as BikeData is Serializeble
        //they will get copied the BikeData
        private List<Point> pointsHeartrate = new List<Point>();
        private List<Point> pointsRPM = new List<Point>();
        private List<Point> pointsSpeed = new List<Point>();
        private List<Point> pointsDistance = new List<Point>();
        private List<Point> pointsPower = new List<Point>();
        private List<Point> pointsEnergy = new List<Point>();
        private List<Point> pointsCurrentPower = new List<Point>();

        private String selectedData = "";
        private Point oldPoint;
        
        public Client()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            oldPoint = new Point(panel1.Width, (int)-(25 / 2.3));
            UpdateStyles();
            this.DoubleBuffered = true;
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            data = new List<BikeData>();
             virtSettings = new VirtSettings(new VirtBike());
             setBike(new PhysBike(Program.COM_PORT));
        }

        private void physicalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setBike(new PhysBike(Program.COM_PORT));
        }

        private void setBike(Bike b)
        {
            if (b is PhysBike)
            {
                resetLabels();
                physicalToolStripMenuItem.Checked = true;
                virtualToolStripMenuItem.Checked = false;
                bike = new PhysBike(Program.COM_PORT);
                virtSettings.Close();
            }
            else if (b is VirtBike)
            {
                resetLabels();
                virtualToolStripMenuItem.Checked = true;
                physicalToolStripMenuItem.Checked = false;
                bike = new VirtBike();
                virtSettings = new VirtSettings((VirtBike)bike);
                virtSettings.Show();
            }
        }

        private void virtualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setBike(new VirtBike());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlTextWriter writer = new XmlTextWriter("C:\\testbikedata.xml", Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartElement("BikeData");
            writer.WriteStartElement("HeartRate");
            writer.WriteString("132");
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            g.SmoothingMode = SmoothingMode.HighQuality;
            Pen p = new Pen(panel1.ForeColor, 3);
            p.LineJoin = LineJoin.Bevel;
            SizeF stringsize = g.MeasureString(selectedData,panel1.Font);
            g.DrawString(selectedData,panel1.Font,p.Brush,new Point(panel1.Width-(int)stringsize.Width,0));

            if (bike is VirtBike)
            {
                if (comboBox1.Text == "Heart rate")
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetHeartRate() / 1.5+5));
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
                }
                if (comboBox1.Text == "RPM")
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetRPM() / 1.2+5));
                    pointsRPM.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < pointsRPM.Count; i++)
                    {
                        Point point = pointsRPM[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsRPM[i] = point;
                    }
                    for (int j = pointsRPM.Count - 1; j > 0; j--)
                    {
                        Point point = pointsRPM[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsRPM[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < pointsRPM.Count; k++)
                    {
                        if (pointsRPM[k].X < 0)
                        {
                            pointsRPM.RemoveAt(k);
                        }
                    }
                }
                if (comboBox1.Text == "Speed")
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-((int)bike.GetSpeed() / 4.2+5));
                    pointsSpeed.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < pointsSpeed.Count; i++)
                    {
                        Point point = pointsSpeed[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsSpeed[i] = point;
                    }
                    for (int j = pointsSpeed.Count - 1; j > 0; j--)
                    {
                        Point point = pointsSpeed[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsSpeed[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < pointsSpeed.Count; k++)
                    {
                        if (pointsSpeed[k].X < 0)
                        {
                            pointsSpeed.RemoveAt(k);
                        }
                    }
                }
                if (comboBox1.Text == "Distance")
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetDistance() / 600+5));
                    pointsDistance.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < pointsDistance.Count; i++)
                    {
                        Point point = pointsDistance[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsDistance[i] = point;
                    }
                    for (int j = pointsDistance.Count - 1; j > 0; j--)
                    {
                        Point point = pointsDistance[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsDistance[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < pointsDistance.Count; k++)
                    {
                        if (pointsDistance[k].X < 0)
                        {
                            pointsDistance.RemoveAt(k);
                        }
                    }
                }
                if (comboBox1.Text == "Power")
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetPower() / 2.4+5));
                    pointsPower.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < pointsPower.Count; i++)
                    {
                        Point point = pointsPower[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsPower[i] = point;
                    }
                    for (int j = pointsPower.Count - 1; j > 0; j--)
                    {
                        Point point = pointsPower[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsPower[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < pointsPower.Count; k++)
                    {
                        if (pointsPower[k].X < 0)
                        {
                            pointsPower.RemoveAt(k);
                        }
                    }
                }
                if (comboBox1.Text == "Energy")
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetEnergy() / 600 + 5));
                    pointsEnergy.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < pointsEnergy.Count; i++)
                    {
                        Point point = pointsEnergy[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsEnergy[i] = point;
                    }
                    for (int j = pointsEnergy.Count - 1; j > 0; j--)
                    {
                        Point point = pointsEnergy[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsEnergy[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < pointsEnergy.Count; k++)
                    {
                        if (pointsEnergy[k].X < 0)
                        {
                            pointsEnergy.RemoveAt(k);
                        }
                    }
                }
                if (comboBox1.Text == "CurrentPower")
                {
                    Console.WriteLine("Current Power");
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetCurrentPower() / 2.4+5));
                    pointsCurrentPower.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < pointsCurrentPower.Count; i++)
                    {
                        Point point = pointsCurrentPower[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsCurrentPower[i] = point;
                    }
                    for (int j = pointsCurrentPower.Count - 1; j > 0; j--)
                    {
                        Point point = pointsCurrentPower[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        pointsCurrentPower[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < pointsCurrentPower.Count; k++)
                    {
                        if (pointsCurrentPower[k].X < 0)
                        {
                            pointsCurrentPower.RemoveAt(k);
                        }
                    }
                }
            }
            else
            {

            }
            base.OnPaint(e);

            g.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bike is VirtBike)
            {
                updateLabels();
            }
            panel1.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedData = comboBox1.Text;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateLabels();
        }

        private void resetLabels()
        {
            heartRateData.Text = "0";
            RPMData.Text = "0";
            speedData.Text = "0";
            distanceData.Text = "0";
            powerData.Text = "0";
            energyData.Text = "0";
            currentPowerData.Text = "0";
            timeData.Text = "00:00:00";
        }

        private void updateLabels()
        {
            if (bike is VirtBike)
            {
                if (((VirtBike)bike).GetHeartRateConnected())
                {
                    heartRateData.Text = bike.GetHeartRate().ToString();
                }
                else
                {
                    heartRateData.Text = "0";
                }
            }
            else if (bike is PhysBike)
            {
                heartRateData.Text = bike.GetHeartRate().ToString();
            }
            RPMData.Text = bike.GetRPM().ToString();
            speedData.Text = bike.GetSpeed().ToString();
            distanceData.Text = bike.GetDistance().ToString();
            powerData.Text = bike.GetPower().ToString();
            energyData.Text = bike.GetEnergy().ToString();
            currentPowerData.Text = bike.GetCurrentPower().ToString();
            timeData.Text = bike.GetTime();
        }
    }
}
