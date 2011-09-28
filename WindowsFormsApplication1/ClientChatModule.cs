using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace WindowsFormsApplication1
{
    class ClientChatModule
    {
        public delegate void DataReceive(string data);
        public event DataReceive OnDataReceive;
        TcpClient socket;
        Thread _THREAD;
        private int timeout = 100;
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                if (value > 1)
                {
                    timeout = value;
                }
                else
                {
                    timeout = 1;
                }
            }
        }

        public ClientChatModule():this ("127.0.0.1", 1234, true) {}
        public ClientChatModule(string ip, int port, bool autoStartReceiving)
        {
            if (Connect(ip, port))
            {
                if (autoStartReceiving)
                {
                    StartReceiving();
                }
            }
            else
            {
                throw new Exception(String.Format("Error connecting to server: {0}:{1}", ip, port));
            }
        }

        public bool SendMessage(string message)
        {
            try
            {
                byte[] buffer = Encoding.Unicode.GetBytes(message);
                socket.GetStream().Write(buffer, 0, buffer.Length-1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //throw ex;
                return false;
            }
            return true;
        }
        private string ReadResponse(TcpClient client)
        {
            try //if(noErrors)
            {
                byte[] buffer = new byte[256];
                int totalRead = 0;
                //read bytes until there are none left
                do
                {
                    int read = client.GetStream().Read(buffer, totalRead, buffer.Length - totalRead);
                    totalRead += read;
                } while (client.GetStream().DataAvailable);
                string message = Encoding.Unicode.GetString(buffer, 0, totalRead);
                if (message.Equals("-=+CleanCloseConnection+=-", StringComparison.CurrentCultureIgnoreCase))
                {
                    Close();
                }
                return message;
            }
            catch (Exception e) //else
            {
                Debug.WriteLine(e.Message);
                return "";
            }
        }
        private void PollServerForMessages()
        {
            //Poll all the things
            while (true)
            {
                try
                {
                    //Lees die shit
                    string data = ReadResponse(socket);
                    //Als de shit geen shit was
                    if (!String.IsNullOrEmpty(data))
                    {
                        //Stuur die shit door
                        OnDataReceive.Invoke(data);
                    }
                    //slapie doen
                    Thread.Sleep(Timeout);
                }
                catch (Exception ex)
                {
                    //Shit hit the fan
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        public void StartReceiving()
        {
            if (!socket.Connected)
            {
                throw new Exception("Not connected to a server, please Connect first.");
            }
            _THREAD = new Thread(new ThreadStart(PollServerForMessages));
            _THREAD.Start();
        }
        public void StopReceiving()
        {
            _THREAD.Abort();
        }
        public bool Connect(string ip, int port)
        {
            try
            {
                socket = new TcpClient(ip, port);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        public void Close()
        {
            SendMessage("-=+CleanCloseConnection+=-");
            socket.Close();
            StopReceiving();
        }
    }
}
