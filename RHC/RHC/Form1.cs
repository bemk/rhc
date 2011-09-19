using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RHC
{
    public partial class Form1 : Form
    {
        private Program program;

        public Form1(Program program)
        {
            InitializeComponent();
            this.program = program;
            program.StartReadData();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            program.NewDataRead += OnDataRead;
        }        

        private void buttonStop_Click(object sender, EventArgs e)
        {
            program.NewDataRead -= OnDataRead;
        }

        void OnDataRead(string data)
        {
            textBoxOutput.AppendText(String.Format("{0}\n", "HEART   RPM     SPEED   DIST.   POWER   ENERGY  TIME    HUIDIG POWER"));
            textBoxOutput.AppendText(String.Format("{0}\n\n", data));           
        }
    }
}
