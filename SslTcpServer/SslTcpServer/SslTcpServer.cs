using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;

namespace ConsoleApplication1
{
    class SslTcpServer
    {
        // According to the documentation our port is 1234
        private const int port = 1234;

        public SslTcpServer()
        {
            runServer();
        }

        private void runServer()
        {
            // Create tcp listener
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            // while server is running
            while (true)
            {
                Console.WriteLine("Waiting for clients to connect ...");
                TcpClient client = listener.AcceptTcpClient();
                processClient(client);
            }
        }

        private void processClient(TcpClient client)
        {
            X509Certificate certificate = new X509Certificate("..\\..\\..\\Certificate\\Certificate.pfx", "KTYy77216");
            // SslStream; leaveInnerStreamOpen = false;
            SslStream stream = new SslStream(client.GetStream(), false);
            try
            {
                // clientCertificateRequired = false
                // checkCertificateRevocation = true;
                stream.AuthenticateAsServer(certificate, false, SslProtocols.Tls, true);
                Console.WriteLine("Waiting for client message ...");

                // Read a message from the client
                string input = readMessage(stream);
                Console.WriteLine("Received: {0}", input);

                // Write a message to the client
                byte[] message = Encoding.UTF8.GetBytes("Hello client, this is a message from the server :)<EOF>");
                Console.WriteLine("Sending message to client ...");
                stream.Write(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                stream.Close();
                client.Close();
                return;
            }
            finally
            {
                stream.Close();
                client.Close();
            }
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
            new SslTcpServer();
        }
    }
}
