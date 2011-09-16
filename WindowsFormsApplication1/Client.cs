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
            Console.WriteLine(this.DoubleBuffered);
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            data = new List<BikeData>();
            setBike(new PhysBike(Program.COM_PORT));
            virtSettings = new VirtSettings(new VirtBike());
        }

        private void physicalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setBike(new PhysBike(Program.COM_PORT));
        }

        private void setBike(Bike bike)
        {
            if (bike is PhysBike)
            {
                physicalToolStripMenuItem.Checked = true;
                virtualToolStripMenuItem.Checked = false;
                bike = new PhysBike(Program.COM_PORT);
                virtSettings.Close();
            }
            else if (bike is VirtBike)
            {
                virtualToolStripMenuItem.Checked = true;
                physicalToolStripMenuItem.Checked = false;
                bike = new VirtBike();
                virtSettings = new VirtSettings((VirtBike)bike);
                virtSettings.Show();
            }
        }
        public void setBikeMenuToPhysicalBike()
        {
            setBike(Program.PHYSICALBIKE);
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
                if (comboBox1.Text.Equals("Power"))
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(x, (int)-(virtbike.getPower()/2.3));
                    x+=2;
                    if (x > panel1.Width)
                    {
                        g.TranslateTransform(-newPoint.X, 0);
                    }
                    g.DrawLine(p, oldPoint, newPoint);
                    oldPoint = newPoint;
                }
            }
            else
            {
                
            }
            base.OnPaint(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedData = comboBox1.Text;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }


        public void setVirtBike(VirtBike virtbike)
        {
            this.virtbike = virtbike;
        }
    }
}
