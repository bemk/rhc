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
        private List<BikeData> data = new List<BikeData>();
        private VirtSettings virtSettings;

        //Temp place for the points
        //as soon as BikeData is Serializeble
        //they will get copied the BikeData
        private List<Point> pointsHeartrate = new List<Point>();
        private List<Point> pointsRPM = new List<Point>();
        private List<Point> pointsSpeed = new List<Point>();
        private List<Point> pointsDistance = new List<Point>();
        private List<Point> pointsPower = new List<Point>();
        private List<Point> pointsEnergy = new List<Point>();
        private List<Point> pointsCurrentPower = new List<Point>();

        private Point oldPoint;
        
        public Client()
        {
            // Please no methods here ;)
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setBike(new VirtBike());
            //openFileDialog1.ShowDialog();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            oldPoint = new Point(panel1.Width, (int)-(25 / 2.3));
            UpdateStyles();
            this.DoubleBuffered = true;
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
                bike = new PhysBike(Program.COM_PORT);
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
            g.DrawString(selectedData, panel1.Font, p.Brush, new Point(panel1.Width - (int)stringsize.Width, 0));

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
            if (bike is VirtBike)
            {
                updateLabels();
            }
            panel1.Invalidate();
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
                    heartRateData.Text = data.ElementAt(data.Count - 1).heartRate.ToString();
                }
                else
                {
                    heartRateData.Text = "0";
                }
            }
            else if (bike is PhysBike)
            {
                heartRateData.Text = data.ElementAt(data.Count-1).heartRate.ToString();
            }
            RPMData.Text = data.ElementAt(data.Count -1).RPM.ToString();
            speedData.Text = data.ElementAt(data.Count - 1).speed.ToString();
            distanceData.Text = data.ElementAt(data.Count - 1).distance.ToString();
            powerData.Text = data.ElementAt(data.Count - 1).power.ToString();
            energyData.Text = data.ElementAt(data.Count - 1).energy.ToString();
            currentPowerData.Text = data.ElementAt(data.Count - 1).currentPower.ToString();
            timeData.Text = data.ElementAt(data.Count - 1).time;
            data.ElementAt(data.Count - 1).pointsHeartrate = this.pointsHeartrate;
            data.ElementAt(data.Count - 1).pointsRPM = this.pointsRPM;
            data.ElementAt(data.Count - 1).pointsSpeed = this.pointsSpeed;
            data.ElementAt(data.Count - 1).pointsDistance = this.pointsDistance;
            data.ElementAt(data.Count - 1).pointsPower = this.pointsPower;
            data.ElementAt(data.Count - 1).pointsEnergy = this.pointsEnergy;
            data.ElementAt(data.Count - 1).pointsCurrentPower = this.pointsCurrentPower;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            //Console.WriteLine(saveFileDialog1.FileName);
            ObjectToFile(data,saveFileDialog1.FileName);
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
                _FileStream.Write(_ByteArray.ToArray(), 0, _ByteArray.Length);

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

        private void updatePoints()
        {
            this.pointsHeartrate = data.ElementAt(data.Count - 1).pointsHeartrate;
            this.pointsRPM = data.ElementAt(data.Count - 1).pointsRPM;
            this.pointsSpeed = data.ElementAt(data.Count - 1).pointsSpeed;
            this.pointsDistance = data.ElementAt(data.Count - 1).pointsDistance;
            this.pointsPower = data.ElementAt(data.Count - 1).pointsPower;
            this.pointsEnergy = data.ElementAt(data.Count - 1).pointsEnergy;
            this.pointsCurrentPower = data.ElementAt(data.Count - 1).pointsCurrentPower;
            panel1.Invalidate();
        }

        private void updateBike()
        {
            this.bike = new VirtBike(data.ElementAt(data.Count - 1).heartRate, data.ElementAt(data.Count - 1).RPM, data.ElementAt(data.Count - 1).speed, data.ElementAt(data.Count - 1).distance, data.ElementAt(data.Count - 1).power, data.ElementAt(data.Count - 1).energy, data.ElementAt(data.Count - 1).currentPower, data.ElementAt(data.Count - 1).time);
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            string input = chatInput.Text;
            chatOutput.Text += input;
        }
    }
}