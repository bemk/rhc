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
using System.Net.Sockets;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Client : Form
    {
        private Bike bike;
        private List<BikeData> data = new List<BikeData>();
        private VirtSettings virtSettings;
        private Chart chart;
        ClientChatModule Chat = new ClientChatModule();
        
        public Client()
        {
            // Please no methods here ;)
            InitializeComponent();  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart = new Chart(this);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.chart.panel1_Paint);
            setBike(new PhysBike(Program.COM_PORT));
           // openFileDialog1.ShowDialog();
            data.Add(new BikeData(0, 0, 0, 0, 25, 0, 0, "00:00:00"));
            timer2.Start();
            timer1.Start();
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
                if(virtSettings != null)
                    virtSettings.Close();
            }
            else if (b is VirtBike)
            {
                if (!virtualToolStripMenuItem.Checked)
                {
                    resetLabels();
                    virtualToolStripMenuItem.Checked = true;
                    physicalToolStripMenuItem.Checked = false;
                    virtSettings = new VirtSettings((VirtBike)b, this);
                    virtSettings.Show();
                }
            }
            bike = b;
        }

        public void addBikeDataToList(BikeData bd)
        {
            data.Add(bd);
        }

        public BikeData getLastBikeDataFromList()
        {
            return data.ElementAt(data.Count - 1);
        }

        private void virtualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setBike(new VirtBike());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            updateLabels();
            panel1.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
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
                    heartRateData.Text = getLastBikeDataFromList().heartRate.ToString();
                }
                else
                {
                    heartRateData.Text = "0";
                }
            }
            else if (bike is PhysBike)
            {
                heartRateData.Text = getLastBikeDataFromList().heartRate.ToString();
            }
            RPMData.Text = getLastBikeDataFromList().RPM.ToString();
            speedData.Text = getLastBikeDataFromList().speed.ToString();
            distanceData.Text = getLastBikeDataFromList().distance.ToString();
            powerData.Text = getLastBikeDataFromList().power.ToString();
            energyData.Text = getLastBikeDataFromList().energy.ToString();
            currentPowerData.Text = getLastBikeDataFromList().currentPower.ToString();
            timeData.Text = getLastBikeDataFromList().time;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            data.ElementAt(data.Count - 1).pointsHeartrate = chart.PointsHeartrate;
            data.ElementAt(data.Count - 1).pointsRPM = chart.PointsRPM;
            data.ElementAt(data.Count - 1).pointsSpeed = chart.PointsSpeed;
            data.ElementAt(data.Count - 1).pointsDistance = chart.PointsDistance;
            data.ElementAt(data.Count - 1).pointsPower = chart.PointsPower;
            data.ElementAt(data.Count - 1).pointsEnergy = chart.PointsEnergy;
            data.ElementAt(data.Count - 1).pointsCurrentPower = chart.PointsCurrentPower;
            ObjectToFile(data,saveFileDialog1.FileName);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            FileToObject(openFileDialog1.FileName);
        }
        /// <summary>
        /// Function to save object to external file
        /// </summary>
        /// <param name="Object">object to save</param>
        /// <param name="FileName">File name to save object</param>
        /// <returns>Return true if object save successfully, if not return false</returns>
        public bool ObjectToFile(object Object, string FileName)
        {
            try
            {
                // create new memory stream
                System.IO.MemoryStream MemoryStream = new System.IO.MemoryStream();

                // create new BinaryFormatter
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter BinaryFormatter
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                // Serializes an object, or graph of connected objects, to the given stream.
                BinaryFormatter.Serialize(MemoryStream, Object);

                // convert stream to byte array
                byte[] ByteArray = MemoryStream.ToArray();

                // Open file for writing
                System.IO.FileStream FileStream = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                FileStream.Write(ByteArray.ToArray(), 0, ByteArray.Length);
                
                // close file stream
                FileStream.Close();

                // cleanup
                MemoryStream.Close();
                MemoryStream.Dispose();
                MemoryStream = null;
                ByteArray = null;

                return true;
            }
            catch (Exception Exception)
            {
                // Error
                if(Program.DEBUG) Console.WriteLine("Exception caught in process: {0}", Exception.ToString());
            }
            // Error occured, return null
            return false;
        }

        public bool FileToObject(string FileName)
        {
            try
            {
                System.IO.FileStream fileStream = new FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                this.data = (List<BikeData>)_BinaryFormatter.Deserialize(fileStream);
                updatePoints();
                updateBike();
                return true;
            }
            catch (Exception Exception)
            {
                // Error
				if(Program.DEBUG) Console.WriteLine("Exception caught in process: {0}", Exception.ToString());
            }
            //Error occured, return null;
            return false;
        }

        private void updatePoints()
        {
            chart.PointsHeartrate = data.ElementAt(data.Count - 1).pointsHeartrate;
            chart.PointsRPM = data.ElementAt(data.Count - 1).pointsRPM;
            chart.PointsSpeed = data.ElementAt(data.Count - 1).pointsSpeed;
            chart.PointsDistance = data.ElementAt(data.Count - 1).pointsDistance;
            chart.PointsPower = data.ElementAt(data.Count - 1).pointsPower;
            chart.PointsEnergy = data.ElementAt(data.Count - 1).pointsEnergy;
            chart.PointsCurrentPower = data.ElementAt(data.Count - 1).pointsCurrentPower;
            panel1.Invalidate();
        }

        private void updateBike()
        {
            this.bike = new VirtBike(data.ElementAt(data.Count - 1).heartRate, data.ElementAt(data.Count - 1).RPM, data.ElementAt(data.Count - 1).speed, data.ElementAt(data.Count - 1).distance, data.ElementAt(data.Count - 1).power, data.ElementAt(data.Count - 1).energy, data.ElementAt(data.Count - 1).currentPower, data.ElementAt(data.Count - 1).time);
            virtSettings = new VirtSettings((VirtBike)bike, this);
            virtSettings.Show();
        }

        protected void OnDataReceive(string message)
        {
            Console.WriteLine(message);
            //chatOutput.AppendText(message + '\n');
            //met een delegate de gui update doen
        }

        private void SendMessage(string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                if (Chat.SendMessage(message))
                {
                    chatOutput.AppendText(message + '\n');
                    chatInput.Clear();
                }
                else
                {
                    chatOutput.AppendText("Error sending message\n");
                }
            }
        }

        private void OnUserInputType(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendMessage(chatInput.Text);
            }
            e.Handled = true;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            SendMessage(chatInput.Text);           
        }

        public ComboBox GetComboBox1()
        {
            return this.comboBox1;
        }

        public Bike GetBike()
        {
            return this.bike;
        }

        public Panel GetPanel1()
        {
            return panel1;
        }

        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            addBikeDataToList(new BikeData(bike.GetHeartRate(), bike.GetRPM(), (int)bike.GetSpeed(), bike.GetDistance(), bike.GetPower(), bike.GetEnergy(), bike.GetCurrentPower(), bike.GetTime()));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // Algorithm so it makes steps of 5
            int barValue = trackBar1.Value;
            int rest = barValue % 5;
            if (rest == 1 || rest == 2)
            {
                barValue -= rest + 5;
            }
            else if (rest == 3 || rest == 4)
            {
                barValue -= rest;
            }
            bike.SetPower(barValue);
        }
    }
}