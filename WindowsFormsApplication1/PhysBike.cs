using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace WindowsFormsApplication1
{
    class PhysBike : Bike
    {
        public const UInt32 HEARTBEAT    = 0;
        public const UInt32 SPEED        = 2;
        public const UInt32 DISTANCE     = 3;
        public const UInt32 POWER        = 4;
        public const UInt32 ENERGY       = 5;
        public const UInt32 CURRENTPOWER = 7;
        public const UInt32 TIME = 6;

        private SerialPort connection;

        private void sendMsg(String Msg)
        {
            try
            {
                connection.WriteLine(Msg);
            }
            catch (Exception e)
            {
                if (Program.DEBUG) Console.WriteLine("ERROR: Sending message to bike failed, " + e.Message);
            }
        }

        private String getMsg(String Msg)
        {
            try
            {
                connection.WriteLine(Msg);
            }
            catch (Exception e)
            {
                if (Program.DEBUG) Console.WriteLine("ERROR: Getting message from bike failed, " + e.Message);
                return "ERR";
            }
            return (connection.ReadLine());
        }

        private void connect(String comport)
        {
            connection = new SerialPort(comport);
            try
            {
                if (!connection.IsOpen)
                {
                    connection.Open();
                    Console.WriteLine("bike connected!");
                }
            }
            catch (Exception e)
            {
                if(Program.DEBUG) Console.WriteLine("ERROR: Connecting to bike failed, " + e.Message);
            }
        }

        public PhysBike(string comport)
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
                return Int32.Parse(split[POWER]);
            }
            return -1;
        }

        public int GetCurrentPower()
        {
            String pw = getMsg("PW");
            if (pw != "ERR")
            {
                String[] split = pw.Split('\t');
                return Int32.Parse(split[CURRENTPOWER]);
            }
            return -1;
        }

        public int GetEnergy()
        {
            String pw = getMsg("PW");
            if (pw != "ERR")
            {
                String[] split = pw.Split('\t');
                return Int32.Parse(split[ENERGY]); // 5 or 6.
            }
            return -1;
        }

        public int GetDistance()
        {
            string pw = getMsg("PW");
            if(pw != "ERR")
            {
                string[] split = pw.Split('\t');
                return Int32.Parse(split[DISTANCE]);
            }
            return -1;
        }
        public int GetHeartRate()
        {
            String hb = getMsg("PW");
            if (hb != "ERR")
            {
                String[] split = hb.Split('\t');
                return Int32.Parse(split[HEARTBEAT]);
            }
            return -1;
        }
        public decimal GetSpeed()
        {
            String spd = getMsg("PW");
            if (spd != "ERR")
            {
                String[] split = spd.Split('\t');
                return decimal.Parse(split[SPEED]) / 10;
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
            String time = getMsg("PW");
            if (time != "ERR")
            {
                String[] split = time.Split('\t');
                return split[TIME];
            }
            return "-1:-1:-1";
        }
    }
}
