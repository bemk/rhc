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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "arts01") && (textBox2.Text == "bikea5"))
            {
                this.Dispose(false);

                new MasterClient().Show();
            }
            else if ((textBox1.Text == "arts01") && (textBox2.Text != "bikea5"))
            {
                MessageBox.Show("Invalid Password!", "Login",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ClearTextBoxes();
            }
            else if ((textBox1.Text != "arts01") && (textBox2.Text == "bikea5"))
            {
                MessageBox.Show("Invalid Username!", "Login",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ClearTextBoxes();
            }
            else if ((textBox1.Text != "arts01") && (textBox2.Text != "bikea5"))
            {
                MessageBox.Show("Invalid Username and/or Password!", "Login",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ClearTextBoxes();
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


    }
}