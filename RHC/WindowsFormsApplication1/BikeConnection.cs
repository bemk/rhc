using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace WindowsFormsApplication1
{
    public class BikeConnection
    {
        private SerialPort connection;
        private BikeData data;

        public BikeConnection()
        {
            connection = new SerialPort("COM3");
            data = new BikeData();
        }

        public bool Connect()
        {
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public BikeData GetBikeData()
        {
            return data;
        }

        public bool IsConnected()
        {
            return connection.IsOpen;
        }

        public void GetData()
        {
            // Check if the connection is open.
            if (!connection.IsOpen) return;

            connection.WriteLine("pw");
            string read = connection.ReadLine();
            if (!read.Contains("ERR"))
            {
                string[] split = read.Split('\t');
                data.HeartRates.Add(Convert.ToInt32(split[0]));
            }
        }
    }
}