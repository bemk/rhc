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

        public VirtSettings(VirtBike bike)
        {
            this.bike = bike;
            InitializeComponent();
        }

        private void VirtSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.setBikeMenuToPhysicalBike();
        }

        private void heartRateBar_Scroll(object sender, EventArgs e)
        {

        }

        private void heartRateConnected_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RPMBar_Scroll(object sender, EventArgs e)
        {

        }

        private void speedBar_Scroll(object sender, EventArgs e)
        {

        }

        private void distanceNumber_ValueChanged(object sender, EventArgs e)
        {

        }

        private void powerBar_Scroll(object sender, EventArgs e)
        {
            bike.setPower(powerBar.Value);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void currentPowerBar_Scroll(object sender, EventArgs e)
        {

        }

        private void Hour_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Minutes_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Seconds_ValueChanged(object sender, EventArgs e)
        {

        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
           // Console.WriteLine(virtbike.getPower());
            client.setVirtBike(virtbike);
        }

        private void heartRateBar_Scroll(object sender, EventArgs e)
        {
            bike.SetHeartRate(heartRateBar.Value);
            Console.WriteLine(heartRateBar.Value);
        }
    }
}
