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

        private String selectedData = "";
        private int x = 0;
        private Point oldPoint = new Point(0, (int)-(25 / 2.3));

        public Client()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
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

            if (true)
            {
                Console.WriteLine("virtbike");
                if (comboBox1.Text == "Power")
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(x, (int)-(bike.GetPower()/2.3));
                    x+=2;
                    //if (x > panel1.Width)
                //    {
                //        g.TranslateTransform(-newPoint.X, 0);
               //     }
                    g.DrawLine(p, oldPoint, newPoint);
                    oldPoint = newPoint;
                    //g.TranslateTransform(-(points[k].X-panel1.Width+20), 0);
                }
            }
            else
            {

            }
            base.OnPaint(e);
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
