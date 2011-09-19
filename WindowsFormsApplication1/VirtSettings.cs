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
    public partial class VirtSettings : Form
    {
        private VirtBike bike;
        private Client c;

        public VirtSettings(VirtBike b, Client c)
        {
            InitializeComponent();
            bike = b;
        }

        private void heartRateBar_Scroll(object sender, EventArgs e)
        {
            bike.SetHeartRate(heartRateBar.Value);
        }

        private void heartRateConnected_CheckedChanged(object sender, EventArgs e)
        {
            bike.SetHeartRateConnected(heartRateConnected.Checked);
        }

        private void RPMBar_Scroll(object sender, EventArgs e)
        {
            bike.SetRPM(RPMBar.Value);
        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {
            bike.SetSpeed(speedBar.Value);
        }

        private void distanceNumber_ValueChanged(object sender, EventArgs e)
        {
            bike.SetDistance((int)distanceNumber.Value);
        }

        private void powerBar_Scroll(object sender, EventArgs e)
        {
            // Algorithm so it makes steps of 5
            int barValue = powerBar.Value;
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
            currentPowerBar.Maximum = barValue;
            if (currentPowerBar.Value >= barValue)
            {
                bike.SetCurrentPower(barValue);
            }
        }

        private void energyNumber_ValueChanged(object sender, EventArgs e)
        {
            bike.SetEnergy((int)energyNumber.Value);
        }

        private void currentPowerBar_Scroll(object sender, EventArgs e)
        {
            if (currentPowerBar.Value <= powerBar.Value)
            {
                bike.SetCurrentPower(currentPowerBar.Value);
            }
            else
            {
                if (Program.DEBUG) Console.WriteLine("ERROR: Currentpower is higher than max power :C");
            }
        }

        private void Hour_ValueChanged(object sender, EventArgs e)
        {
            bike.SetTime(hour.Value + ":" + minutes.Value + ":" + seconds.Value);
        }

        private void Minutes_ValueChanged(object sender, EventArgs e)
        {
            bike.SetTime(hour.Value + ":" + minutes.Value + ":" + seconds.Value);
        }

        private void Seconds_ValueChanged(object sender, EventArgs e)
        {
            bike.SetTime(hour.Value + ":" + minutes.Value + ":" + seconds.Value);
        }

       // private void timer1_Tick(object sender, EventArgs e)
     //   {
       //    int heartrate;
         //   if(!bike.GetHeartRateConnected())
         //       heartrate = 0;
        //    else
          //      heartrate = bike.GetHeartRate();
          //  BikeData bikedata = new BikeData(heartrate, bike.GetRPM(), (int)bike.GetSpeed(), bike.GetDistance(), bike.GetPower(), bike.GetEnergy(), bike.GetCurrentPower(), bike.GetTime());
            //c.addBikeDataToList(bikedata);
       // }
    }
}
