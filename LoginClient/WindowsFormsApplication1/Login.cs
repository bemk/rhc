using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

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

        private string encryptPassword(string input)
        {
            // step 1 make new UnicodeEncoding
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(input);

            // step 2 make new hashString
            SHA512Managed hashString = new SHA512Managed();
            string output = "";

            // step 3 computeHash
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                output += String.Format("{0:x2}", x);
            }
            // step 4 return the encrypted password
            return output;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // SHA512 encryption AKA bikea5
            string password = "a61d7b88176aa337fed0e2e63f21c45c9a93eafb839672915375a961f55b98932c44e775b289ff1f10d8ace1fdf85d3a3b28b367f4c6ebebc88632bdfb33e640";
            if ((textBox1.Text == "arts01") && (encryptPassword(textBox2.Text) == password))
            {
                this.Dispose(false);

                new MasterClient().Show();
            }
            else
            {
                MessageBox.Show("Invalid Username and/or Password!", "Login",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ClearTextBoxes();
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            button1.PerformClick();
        }


    }
}