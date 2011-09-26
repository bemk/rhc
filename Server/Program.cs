
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Server
{
    class Program
    {
        public const bool active = true;
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 1234);
            listener.Start();

            while (active)
            {
                Console.WriteLine("Waiting for connection");
                //AcceptTcpClient waits for a connection from the client
                TcpClient client = listener.AcceptTcpClient();
                //start a new thread to handle this connection so we can go back 
                //to waiting for another client
                Thread thread = new Thread(HandleClientThread);
                thread.Start(client);
            }

        }

        static void HandleClientThread(object obj)
        {
            TcpClient client = obj as TcpClient;
            try
            {
                Debug.WriteLine("Connection accepted");
                Console.WriteLine("Connection accepted");

                bool done = false;
                while (!done)
                {
                    string received = ReadMessage(client);
                    Console.WriteLine("Received: {0}", received);
                    done = received.Equals("-=+CleanCloseConnection+=-", StringComparison.CurrentCultureIgnoreCase);
                    if (done) SendResponse(client, "-=+CleanCloseConnection+=-");
                    else SendResponse(client, "OK");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Close();
                Debug.WriteLine("Connection closed");
                Console.WriteLine("Connection closed");
            }
        }

        private static string ReadMessage(TcpClient client)
        {
            byte[] buffer = new byte[256];
            int totalRead = 1;
            //read bytes until stream indicates there are no more
            do
            {
                int read = client.GetStream().Read(buffer, totalRead, buffer.Length - totalRead);
                totalRead += read;
                Console.WriteLine("ReadMessage: " + read);
            } while (client.GetStream().DataAvailable);

            return Encoding.Unicode.GetString(buffer, 1, totalRead);
        }

        private static void SendResponse(TcpClient client, string message)
        {
            //make sure the other end decodes with the same format!
            byte[] bytes = Encoding.Unicode.GetBytes(message);
            client.GetStream().Write(bytes, 0, bytes.Length);
        }
    }
}

