using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace WindowsFormsApplication1
{
    class PhysBike : Bike
    {
        private SerialPort connection;

        private void sendMsg(String Msg)
        {
            checkConnection();
            connection.WriteLine(Msg);
        }

        private String getMsg(String Msg)
        {
            checkConnection();
            connection.WriteLine(Msg);
            return (connection.ReadLine());
        }

        private void checkConnection()
        {
            if (connection == null)
                Console.WriteLine("ERROR: Connection doesn't exist!");
            if (!connection.IsOpen)
                Console.WriteLine("ERROR: Connection isn't open!");
        }

        private void connect(String comport)
        {
            connection = new SerialPort(comport);
            try
            {
                if (!connection.IsOpen)
                    connection.Open();
            }
            catch
            {

            }
        }
        public PhysBike(String comport)
        {
            connect(comport);
        }
        ~PhysBike()
        {
            if (connection.IsOpen)
                connection.Close();
        }

        public void Reset()
        {
            sendMsg("RS");
        }

        public void SetPower(int power)
        {
            sendMsg("CM");
            sendMsg("PW " + power.ToString());
        }

        public int GetPower()
        {
            String pw = getMsg("PW");
            if (pw != "ERR")
            {
                String[] split = pw.Split('\t');
                return Int32.Parse(split[4]);
            }
            return -1;
        }

        public int GetCurrentPower()
        {
            String pw = getMsg("PW");
            if (pw != "ERR")
            {
                String[] split = pw.Split('\t');
                return Int32.Parse(split[7]);
            }
            return -1;
        }

        public int GetEnergy()
        {
            String pw = getMsg("PW");
            if (pw != "ERR")
            {
                String[] split = pw.Split('\t');
                return Int32.Parse(split[6]); // 5 or 6.
            }
            return -1;
        }

        public int GetDistance()
        {
            string pw = getMsg("PW");
            if(pw != "ERR")
            {
                string[] split = pw.Split('\t');
                return Int32.Parse(split[3]);
            }
            return -1;
        }
        public int GetHeartRate()
        {
            String hb = getMsg("PW");
            if (hb != "ERR")
            {
                String[] split = hb.Split('\t');
                return Int32.Parse(split[0]);
            }
            return -1;
        }
        public decimal GetSpeed()
        {
            String spd = getMsg("PW");
            if (spd != "ERR")
            {
                String[] split = spd.Split('\t');
                return decimal.Parse(split[2])/10;
            }
            return -1;
        }
        public int GetRPM()
        {
            String RPM = getMsg("VS");
            if (RPM != "ERR")
                return Int32.Parse(RPM);
            return -1;
        }
        public int GetBikeID()
        {
            String ID = getMsg("ID");
            if (ID != "ERR") 
               return Int32.Parse(ID);
            return -1;
        }
        public String GetTime()
        {
            String time = getMsg("TE");
            if (time != "ERR")
                return time;
            return null;
        }
    }
}
