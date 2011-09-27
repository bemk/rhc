using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace SslTcpClient
{
    class SslTcpClient
    {
        private const string server = "192.168.0.2";
        // According to documentation 1234
        private const int port = 1234;

        public SslTcpClient()
        {
            runClient();
        }

        private static bool validateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }

        private void runClient()
        {
            TcpClient client = new TcpClient(server, port);
            Console.WriteLine("Client connected ...");

            // Create ssl stream
            SslStream stream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(validateServerCertificate), null);

            stream.AuthenticateAsClient(server);

            // write message to server
            byte[] output = Encoding.UTF8.GetBytes("Message from client :D<EOF>");
            stream.Write(output);
            stream.Flush();

            // read message from server
            string input = readMessage(stream);
            Console.WriteLine("Received: {0}", input);

            // close everything
            stream.Close();
            client.Close();
            Console.WriteLine("Client closed connection ...");
            // Press any key to continue ...
            Console.ReadKey();
        }

        private string readMessage(SslStream stream)
        {
            byte[] buffer = new byte[2048];
            StringBuilder input = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = stream.Read(buffer, 0, buffer.Length);

                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                input.Append(chars);

                if (input.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }
            }
            while (bytes != 0);

            return input.ToString();
        }

        static void Main(string[] args)
        {
            new SslTcpClient();
        }
    }
}
