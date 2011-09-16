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

        public VirtSettings(VirtBike bike)
        {
            this.bike = bike;
            InitializeComponent();
        }

        private void heartRateBar_Scroll(object sender, EventArgs e)
        {
            bike.SetHeartRate(heartRateBar.Value);
            Console.WriteLine(heartRateBar.Value);
        }
    }
}
