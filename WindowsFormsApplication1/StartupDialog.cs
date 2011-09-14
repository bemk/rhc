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
    public partial class StartupDialog : Form
    {
        public StartupDialog()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Close();
        }

        private void StartupDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (comboBox1.SelectedIndex != Program.PHYSICALBIKE
                && comboBox1.SelectedIndex != Program.VIRTUALBIKE)
            {
                Environment.Exit(0);
            }
        }
    }
}
