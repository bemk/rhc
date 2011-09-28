using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Web;
using OAuth; 


namespace WindowsFormsApplication2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.twitter.com/JNoodelijk");
        }

        private void setStatus(string message, Color color)
        {
            status.Text = message;
            status.ForeColor = color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
        
            try
            {
                String personName = "";

                string consumerKey = "key";
            string consumerSecret = "secret";
            Uri uri = new Uri("https://api.twitter.com/oauth/request_token");

            OAuthBase oAuth = new OAuthBase();
            string nonce = oAuth.GenerateNonce();
            string timeStamp = oAuth.GenerateTimeStamp();
            string sig = oAuth.GenerateSignature(uri,
                consumerKey, consumerSecret, 
                string.Empty, string.Empty,
                "GET", timeStamp, nonce,
                OAuthBase.SignatureTypes.HMACSHA1);

            sig = HttpUtility.UrlEncode(sig);

            StringBuilder stb = new StringBuilder(uri.ToString());
            stb.AppendFormat("?oauth_consumer_key={0}&", consumerKey);
            stb.AppendFormat("oauth_nonce={0}&", nonce);
            stb.AppendFormat("oauth_timestamp={0}&", timeStamp);
            stb.AppendFormat("oauth_signature_method={0}&", "HMAC-SHA1");
            stb.AppendFormat("oauth_version={0}&", "1.0");
            stb.AppendFormat("oauth_signature={0}", sig);

            System.Diagnostics.Debug.WriteLine(stb.ToString()); 

                StringBuilder sb = new StringBuilder();
                byte[] buf = new byte[8192];

                string user =
        Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(txtID.Text + ":" +
        txtPasswd.Text));

                HttpWebRequest request =
        (HttpWebRequest)WebRequest.Create("http://twitter.com/account/verify_credentials.xml");

                request.Method = "GET";

                request.Headers.Add("Authorization", "Basic " + user);
                request.ContentType = "application/x-www-form-urlencoded";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();

                string tempStream = null;
                int count = 0;

                do
                {
                    count = resStream.Read(buf, 0, buf.Length);

                    if (count != 0)
                    {
                        tempStream = Encoding.ASCII.GetString(buf, 0, count);
                        sb.Append(tempStream);
                    }
                }
                while (count > 0);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sb.ToString());

                XmlNodeList nodeList = doc.SelectNodes("/user/name");
                foreach (XmlNode node in nodeList)
                {
                    personName = node.InnerText;
                }

                lblMessage.Text = "Welcome, " + personName + "!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Message.Contains("Unauthorized"))
                {
                    lblMessage.Text = "Invalid User ID and/or Password.";
                }
                else if (ex.Message.Contains("Service Unavailable"))
                {
                    lblMessage.Text = "Twitter is busy, try again later";
                }
                else
                {
                    lblMessage.Text = ex.Message;
                }
            }
        }

     
    }
}
        

      




        

    

