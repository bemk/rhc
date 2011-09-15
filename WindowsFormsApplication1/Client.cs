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
        private int x = 0;
        private int y = 0;
        private List<Point> chart = new List<Point>();

        public Client()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
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
                if (comboBox1.Text.Equals("Power"))
                {
                    Point newPoint = new Point(x, (int)(virtbike.getPower()/2.2));
                    x+=2;
                   // Console.WriteLine(virtbike.getPower());
                    chart.Add(newPoint);
                    Point[] points = new Point[100];
                    int k;
                    for (int i = 0; i < chart.Count;i++ )
                    {
                        try
                        {
                            points[i] = (chart.ElementAt(i));
                            k = i;
                        }
                        catch (Exception ex)
                        {
                            for (k = 1; k < points.Length-2; k++)
                            {
                                points[k - 1] = points[k];
                            }
                            points[points.Length - 1] = chart.ElementAt(i);
                        }
                    }
                    g.TranslateTransform(-(points[k].X-panel1.Width+20), 0);
                    for (int j = 1; j < chart.Count; j++)
                    {
                        g.DrawLine(p, points[j - 1], points[j]);
                    }                    
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
