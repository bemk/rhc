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
        private int bike = -1;
        private BikeData data;
        private String selectedData = "";
        private VirtBike virtbike = new VirtBike();
        private Point oldPoint;
        private int x = 0;

        public Client()
        {
            InitializeComponent();
            this.SetStyle(
                           ControlStyles.AllPaintingInWmPaint |
                             ControlStyles.UserPaint |
                              ControlStyles.DoubleBuffer, true);
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            data = new BikeData();
            setBike(Program.PHYSICALBIKE);
        }

        private void physicalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setBike(Program.PHYSICALBIKE);
        }

        private void setBike(int bike)
        {
            if (bike == Program.PHYSICALBIKE)
            {
                physicalToolStripMenuItem.Checked = true;
                virtualToolStripMenuItem.Checked = false;
                this.bike = Program.PHYSICALBIKE;
            }
            else if (bike == Program.VIRTUALBIKE)
            {
                virtualToolStripMenuItem.Checked = true;
                physicalToolStripMenuItem.Checked = false;
                this.bike = Program.VIRTUALBIKE;
            }
        }
        public void setBikeMenuToPhysicalBike()
        {
            setBike(Program.PHYSICALBIKE);
        }

        private void virtualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setBike(Program.VIRTUALBIKE);
            VirtSettings virtsettings = new VirtSettings(this,virtbike);
            virtsettings.Visible = true;
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
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p = new Pen(panel1.ForeColor, 3);
            p.LineJoin = LineJoin.Bevel;
            SizeF stringsize = g.MeasureString(selectedData,panel1.Font);
            g.DrawString(selectedData,panel1.Font,p.Brush,new Point(panel1.Width-(int)stringsize.Width,0));
            if (bike == Program.VIRTUALBIKE)
            {
                Point newPoint = new Point(x, virtbike.getPower());
                x++;
                Console.WriteLine(virtbike.getPower());
                g.DrawLine(p, oldPoint, newPoint);
                oldPoint = newPoint;
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
