using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class COM_port_Selection : Form
    {
        public COM_port_Selection()
        {
            InitializeComponent();
            comboBox1.DataSource = SerialPort.GetPortNames();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public string getSelected()
        {
            return (string)comboBox1.SelectedItem;
        }
    }
}
