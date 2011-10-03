using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace WindowsFormsApplication1
{
    class PhysBike : Bike
    {
        public const UInt32 HEARTBEAT    = 0;
        public const UInt32 RPM          = 1;
        public const UInt32 SPEED        = 2;
        public const UInt32 DISTANCE     = 3;
        public const UInt32 POWER        = 4;
        public const UInt32 ENERGY       = 5;
        public const UInt32 TIME         = 6;
        public const UInt32 CURRENTPOWER = 7;

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
                return null;
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
            sendMsg("CD");
            Thread.Sleep(100);
            sendMsg("PW " + power.ToString());
        }

        public int GetPower()
        {
            String pw = getMsg("ST");
            if (pw != "ERROR" || pw != "RUN\r")
            {
                String[] split = pw.Split('\t'); 
                if(split.Length > 1)
                return Int32.Parse(split[POWER]);
            }
            return -1;
        }

        public int GetCurrentPower()
        {
            String pw = getMsg("ST");
            if (pw != "ERROR" || pw != "RUN\r")
            {
                String[] split = pw.Split('\t');
                if (split.Length > 1)
                return Int32.Parse(split[CURRENTPOWER]);
            }
            return -1;
        }

        public int GetEnergy()
        {
            String pw = getMsg("ST");
            if (pw != "ERROR" || pw != "RUN\r")
            {
                String[] split = pw.Split('\t');
                if (split.Length > 1)
                return Int32.Parse(split[ENERGY]); // 5 or 6.
            }
            return -1;
        }

        public int GetDistance()
        {
            string pw = getMsg("ST");
            if(pw != "ERR")
            {
                string[] split = pw.Split('\t');
                if (split.Length > 1)
                return Int32.Parse(split[DISTANCE]);
            }
            return -1;
        }
        public int GetHeartRate()
        {
            String hb = getMsg("ST");
            if (hb != "ERR")
            {
                String[] split = hb.Split('\t');
                if (split.Length > 1)
                    return Int32.Parse(split[HEARTBEAT]);
            }
            return -1;
        }
        public decimal GetSpeed()
        {
            String spd = getMsg("ST");
            if (spd != "ERROR" || spd != "RUN\r")
            {
                String[] split = spd.Split('\t');
                if (split.Length > 1)
                return decimal.Parse(split[SPEED]) / 10;
            }
            return -1;
        }
        public int GetRPM()
        {
            String rpm = getMsg("ST");
            if (rpm != "ERROR" || rpm != "RUN\r")
            {
                String[] split = rpm.Split('\t');
                if (split.Length > 1)
                return Int32.Parse(split[RPM]);
            }
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
            String time = getMsg("ST");
            if (time != "ERR")
            {
                String[] split = time.Split('\t');
                if (split.Length > 1)
                return split[TIME];
            }
            return "-1:-1:-1";
        }
    }
}
