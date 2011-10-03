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
using System.IO.Ports;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Client : Form
    {
        private Bike bike;
        private List<BikeData> data = new List<BikeData>();
        private VirtSettings virtSettings;
        private Chart chart;
        ClientChatModule Chat = new ClientChatModule();

        private delegate void addBikeDataToListD(BikeData data);
        
        public Client()
        {
            // Please no methods here ;)
            InitializeComponent();
        }

        public BikeData getData()
        {
            return data.ElementAt(data.Count - 1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart = new Chart(this);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.chart.panel1_Paint);
            COM_port_Selection comSelection = new COM_port_Selection();
            if (comSelection.ShowDialog() == DialogResult.OK)
            {
                if (comSelection.getSelected() != null)
                    setBike(new PhysBike(comSelection.getSelected()));
                else
                    setBike(new VirtBike());
            }
            else
            {
                Environment.Exit(0);
            }
            data.Add(new BikeData(0, 0, 0, 0, 25, 0, 0, "00:00:00"));
 
            Thread thread = new Thread(new ThreadStart(timer2_Tick));
            thread.Start();
            Thread uploadThread = new Thread(new ThreadStart(saveToServer));
            uploadThread.Start();
        }

        private void saveToServer()
        {
            while (true)
            {
                if (data.Count > 125)
                {
                    data.RemoveAt(0);
                }
            }
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
                resetLabels();
                virtualToolStripMenuItem.Checked = true;
                physicalToolStripMenuItem.Checked = false;
                virtSettings = new VirtSettings((VirtBike)b, this);
                virtSettings.Show();
            }
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
            if (bike == null || bike is PhysBike)
                bike = new VirtBike();
            setBike(bike);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        // So much sh!t, created a new class Chart for it,
        // all what's left to do is move it :(
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;            
            g.SmoothingMode = SmoothingMode.HighQuality;
            Pen p = new Pen(panel1.ForeColor, 3);
            p.LineJoin = LineJoin.Bevel;
            string selectedData = comboBox1.SelectedText;
            SizeF stringsize = g.MeasureString(selectedData,panel1.Font);
            g.DrawString(selectedData,panel1.Font,p.Brush,new Point(panel1.Width-(int)stringsize.Width,0));

            if (bike is VirtBike)
            {
                // Hell no, prefer comboBox1.SelectedIndex in a switchstate :z
                if (comboBox1.Text == "Heart rate") // index 0
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetHeartRate() / 1.5+5));
                    this.pointsHeartrate.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < this.pointsHeartrate.Count; i++)
                    {
                        Point point = this.pointsHeartrate[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsHeartrate[i] = point;
                    }
                    for (int j = this.pointsHeartrate.Count - 1; j > 0; j--)
                    {
                        Point point = this.pointsHeartrate[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsHeartrate[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < this.pointsHeartrate.Count; k++)
                    {
                        if (this.pointsHeartrate[k].X < 0)
                        {
                            this.pointsHeartrate.RemoveAt(k);
                        }
                    }
                }
                if (comboBox1.Text == "RPM") // index 1
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetRPM() / 1.2+5));
                    this.pointsRPM.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < this.pointsRPM.Count; i++)
                    {
                        Point point = this.pointsRPM[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsRPM[i] = point;
                    }
                    for (int j = this.pointsRPM.Count - 1; j > 0; j--)
                    {
                        Point point = this.pointsRPM[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsRPM[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < this.pointsRPM.Count; k++)
                    {
                        if (this.pointsRPM[k].X < 0)
                        {
                            this.pointsRPM.RemoveAt(k);
                        }
                    }
                }
                
                if (comboBox1.Text == "Speed") // index 2
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-((int)bike.GetSpeed() / 4.2+5));
                    this.pointsSpeed.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < this.pointsSpeed.Count; i++)
                    {
                        Point point = this.pointsSpeed[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsSpeed[i] = point;
                    }
                    for (int j = this.pointsSpeed.Count - 1; j > 0; j--)
                    {
                        Point point = this.pointsSpeed[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsSpeed[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < this.pointsSpeed.Count; k++)
                    {
                        if (this.pointsSpeed[k].X < 0)
                        {
                            this.pointsSpeed.RemoveAt(k);
                        }
                    }
                }
                
                if (comboBox1.Text == "Distance") // index 3
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetDistance() / 600+5));
                    this.pointsDistance.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < this.pointsDistance.Count; i++)
                    {
                        Point point = this.pointsDistance[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsDistance[i] = point;
                    }
                    for (int j = this.pointsDistance.Count - 1; j > 0; j--)
                    {
                        Point point = this.pointsDistance[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsDistance[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < this.pointsDistance.Count; k++)
                    {
                        if (this.pointsDistance[k].X < 0)
                        {
                            this.pointsDistance.RemoveAt(k);
                        }
                    }
                }
                
                if (comboBox1.Text == "Power") // index 4
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetPower() / 2.4+5));
                    this.pointsPower.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < this.pointsPower.Count; i++)
                    {
                        Point point = this.pointsPower[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsPower[i] = point;
                    }
                    for (int j = this.pointsPower.Count - 1; j > 0; j--)
                    {
                        Point point = this.pointsPower[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsPower[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < this.pointsPower.Count; k++)
                    {
                        if (this.pointsPower[k].X < 0)
                        {
                            this.pointsPower.RemoveAt(k);
                        }
                    }
                }
                
                if (comboBox1.Text == "Energy") // index 5
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetEnergy() / 600 + 5));
                    this.pointsEnergy.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < this.pointsEnergy.Count; i++)
                    {
                        Point point = this.pointsEnergy[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsEnergy[i] = point;
                    }
                    for (int j = this.pointsEnergy.Count - 1; j > 0; j--)
                    {
                        Point point = this.pointsEnergy[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsEnergy[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < this.pointsEnergy.Count; k++)
                    {
                        if (this.pointsEnergy[k].X < 0)
                        {
                            this.pointsEnergy.RemoveAt(k);
                        }
                    }
                }
                
                if (comboBox1.Text == "CurrentPower") // index 6
                {
                    g.TranslateTransform(0, panel1.Height);
                    Point newPoint = new Point(panel1.Width, (int)-(bike.GetCurrentPower() / 2.4+5));
                    this.pointsCurrentPower.Add(newPoint);
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    for (int i = 0; i < this.pointsCurrentPower.Count; i++)
                    {
                        Point point = this.pointsCurrentPower[i];
                        point.X -= 4;
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsCurrentPower[i] = point;
                    }
                    for (int j = this.pointsCurrentPower.Count - 1; j > 0; j--)
                    {
                        Point point = this.pointsCurrentPower[j];
                        path.AddLine(oldPoint, point);
                        oldPoint = point;
                        this.pointsCurrentPower[j] = point;
                    }
                    g.DrawPath(p, path);
                    for (int k = 0; k < this.pointsCurrentPower.Count; k++)
                    {
                        if (this.pointsCurrentPower[k].X < 0)
                        {
                            this.pointsCurrentPower.RemoveAt(k);
                        }
                    }
                }
                // Easy enough huh?
            }
            else
            {

            }
            base.OnPaint(e);

            g.Dispose();
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

        #region Labels
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
        #endregion
        #region FileIO
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
            ObjectToFile(data, saveFileDialog1.FileName);
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            FileToObject(openFileDialog1.FileName);
        }
        /// <summary>
        /// Function to save object to external file
        /// </summary>
        /// <param name="_Object">object to save</param>
        /// <param name="_FileName">File name to save object</param>
        /// <returns>Return true if object save successfully, if not return false</returns>
        public bool ObjectToFile(object _Object, string _FileName)
        {
            try
            {
                // create new memory stream
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream();

                // create new BinaryFormatter
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                // Serializes an object, or graph of connected objects, to the given stream.
                _BinaryFormatter.Serialize(_MemoryStream, _Object);

                // convert stream to byte array
                byte[] _ByteArray = _MemoryStream.ToArray();

                // Open file for writing
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                FileStream.Write(ByteArray.ToArray(), 0, ByteArray.Length);
                
                // close file stream
                _FileStream.Close();

                // cleanup
                _MemoryStream.Close();
                _MemoryStream.Dispose();
                _MemoryStream = null;
                _ByteArray = null;

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                if(Program.DEBUG) Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // Error occured, return null
            return false;
        }

        public bool FileToObject(string _FileName)
        {
            try
            {
                System.IO.FileStream fileStream = new FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                this.data = (List<BikeData>)_BinaryFormatter.Deserialize(fileStream);
                Console.WriteLine(data.Count);
                Console.WriteLine("?");
                updateLabels();
                updatePoints();
                updateBike();
                return true;
            }
            catch (Exception _Exception)
            {
                //Error
                if(Program.DEBUG) Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
            //Error occured, return null;
            return false;
        }
        #endregion
        private void updateBike()
        {
            this.bike = new VirtBike(data.ElementAt(data.Count - 1).heartRate, data.ElementAt(data.Count - 1).RPM, data.ElementAt(data.Count - 1).speed, data.ElementAt(data.Count - 1).distance, data.ElementAt(data.Count - 1).power, data.ElementAt(data.Count - 1).energy, data.ElementAt(data.Count - 1).currentPower, data.ElementAt(data.Count - 1).time);
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

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Chat.Close();
        }
        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void timer2_Tick()
        {
            while (true)
            {
                if (!InvokeRequired)
                {
                    addBikeDataToList(new BikeData(bike.GetHeartRate(), bike.GetRPM(), (int)bike.GetSpeed(), bike.GetDistance(), bike.GetPower(), bike.GetEnergy(), bike.GetCurrentPower(), bike.GetTime()));
                }
                else
                {
                    addBikeDataToListD delegatedata = addBikeDataToList;
                    BikeData data = new BikeData(bike.GetHeartRate(), bike.GetRPM(), (int)bike.GetSpeed(), bike.GetDistance(), bike.GetPower(), bike.GetEnergy(), bike.GetCurrentPower(), bike.GetTime());
                    delegatedata(data);
                }
                Thread.Sleep(60);
            }
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
            updateLabels();
        }
    }
}