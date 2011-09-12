using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private BikeConnection connection = new BikeConnection();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!connection.Connect())
            {
                string error = "Could not connect to the bike.";
                setStatus(error);
                MessageBox.Show(error);
                return;
            }
            setStatus("Recording.");
        }

        private void setStatus(string status)
        {
            statusLabel.Text = status;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempChartLabel.Text = comboBox1.Text;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
