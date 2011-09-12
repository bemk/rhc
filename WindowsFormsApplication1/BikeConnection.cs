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
            catch (Exception ex)
            {
                if(Program.DEBUG) Console.WriteLine("Foutje in BikeConnection.Connect() " + ex.Message);
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
                data.RPMs.Add(Convert.ToInt32(split[1]));
                data.Speeds.Add(Convert.ToInt32(split[2]));
                data.Distances.Add(Convert.ToInt32(split[3]));
                data.Powers.Add(Convert.ToInt32(split[4]));
                data.Energies.Add(Convert.ToInt32(split[5]));
                data.Time = split[6];
                data.CurrentPowers.Add(Convert.ToInt32(split[7]));
            }
        }
    }
}