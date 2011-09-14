using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private BikeConnection connection = new BikeConnection();
        private String selected = "";
        private Point oldPoint = new Point();

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!connection.Connect())
            {
                setStatus("Could not connect, check cable.", Color.Red);
                return;
            }
            setStatus("Recording.", Color.Green);
            
            // Timer to retrieve the data.
            System.Timers.Timer updater = new System.Timers.Timer();
            updater.Elapsed += new System.Timers.ElapsedEventHandler(UpdateData);
            updater.Interval = 500;
            updater.Start();
        }

        public void UpdateData(object sender, EventArgs e)
        {
            try
            {
                if (connection.IsConnected())
                {
                    connection.GetData();
                    //connection.SetPower(setPowerBar.Value);
                    updateLabels();
                    
                }
                else
                {
                    setStatus("Reconnecting, please wait.", Color.Orange);
                    if (connection.Connect())
                    {
                        if (Program.DEBUG) Console.WriteLine("Succesfully reconnected.");
                        setStatus("Succesfully reconnected.", Color.Green);
                    }
                    else
                    {
                        if (Program.DEBUG) Console.WriteLine("Reconnecting failed.");
                    }
                }
            }
            catch (IOException ex)
            {
                if (Program.DEBUG) Console.WriteLine("Connection lost " + ex.Message);
                setStatus("Connection lost.", Color.Red);

                connection.Close();
            }
        }

        private void updateLabels()
        {
            try
            {
                heartRateData.Text = connection.GetBikeData().HeartRates.Last().ToString();
                RPMData.Text = connection.GetBikeData().RPMs.Last().ToString();
                speedData.Text = ((decimal)connection.GetBikeData().Speeds.Last() / 10).ToString() + " km/h";
                distanceData.Text = ((decimal)connection.GetBikeData().Distances.Last() / 10).ToString() + " km";
                powerData.Text = connection.GetBikeData().Powers.Last().ToString();
                energyData.Text = connection.GetBikeData().Energies.Last().ToString();
                currentPowerData.Text = connection.GetBikeData().CurrentPowers.Last().ToString();
                timeData.Text = connection.GetBikeData().Time;
            }
            catch(Exception ex)
            {
                if(Program.DEBUG) Console.WriteLine("Updaten van labels failed: " + ex.Message);
            }
        }

        private void setStatus(string status, Color kleur)
        {
            statusLabel.Text = status;
            statusLabel.ForeColor = kleur;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected = comboBox1.Text;
            //Console.WriteLine(selected);
            chart.Update();
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

        private void chart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = chart.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Pen p = new Pen(chart.ForeColor, 3);
            p.LineJoin = LineJoin.Bevel;
            //Point newPoint = new Point(connection.GetBikeData().Speeds.las
       //     g.DrawLine(p, oldPoint, newPoint);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            chart.Refresh();
        }
    }
}
