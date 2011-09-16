﻿using System;
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

        public VirtSettings(VirtBike b)
        {
            bike = b;
            InitializeComponent();
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
            bike.SetPower(powerBar.Value);
        }

        private void energyNumber_ValueChanged(object sender, EventArgs e)
        {
            bike.SetEnergy((int)energyNumber.Value);
        }

        private void currentPowerBar_Scroll(object sender, EventArgs e)
        {
            bike.SetCurrentPower(currentPowerBar.Value);
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
    }
}
