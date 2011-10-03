using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class MasterClient : Form
    {
        public MasterClient()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void setStatus(string message, Color color)
        {
            statusLabel.Text = message;
            statusLabel.ForeColor = color;
        }

        public void startServer()
        {
            new SslTcpServer();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadStart serverThread = new ThreadStart(startServer);
            Thread thread = new Thread(serverThread);
            thread.Start();

            setStatus("Connection established!", Color.Green);
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            setStatus("No connection!", Color.Red);
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            new About().Show();
        }

    }
}
