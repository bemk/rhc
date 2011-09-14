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

namespace WindowsFormsApplication1
{
    public partial class Client : Form
    {
        private int bike = -1;
        private BikeData data;

        public Client()
        {
            InitializeComponent();
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
                bike = Program.PHYSICALBIKE;
            }
            else if (bike == Program.VIRTUALBIKE)
            {
                virtualToolStripMenuItem.Checked = true;
                physicalToolStripMenuItem.Checked = false;
                bike = Program.VIRTUALBIKE;
            }
        }

        private void virtualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setBike(Program.VIRTUALBIKE);
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
    }
}
