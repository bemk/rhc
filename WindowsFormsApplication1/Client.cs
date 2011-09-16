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
        private Bike bike;
        private List<BikeData> data;
        private VirtSettings virtSettings;

        public Client()
        {
            InitializeComponent();
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
    }
}
