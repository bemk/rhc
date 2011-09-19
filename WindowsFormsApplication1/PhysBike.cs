using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace WindowsFormsApplication1
{
    class PhysBike : Bike
    {
        public const UInt32 CURRENTPOWER = 7;
        public const UInt32 POWER = 4;
        public const UInt32 SPEED = 2;
        public const UInt32 HEARTBEAT = 0;

        private SerialPort connection;

        private void sendMsg(String Msg)
        {
            if (connection == null)
            {
                Console.WriteLine("ERROR: Connection doesn't exist!");
            }
            if (!connection.IsOpen)
                Console.WriteLine("ERROR: Connection closed!");
            connection.WriteLine(Msg);
        }

        private String getMsg(String Msg)
        {
            if (connection == null)
                Console.WriteLine("ERROR: Connection doesn't exist!");
            if (!connection.IsOpen)
                Console.WriteLine("ERROR: Connection closed!");

            connection.WriteLine(Msg);
            return (connection.ReadLine());
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

        public void reset()
        {
            sendMsg("RS");
        }

        public void setPower(int power)
        {
            sendMsg("CM");
            sendMsg("PW " + power.ToString());
        }

        public int getPower()
        {
            String pw = getMsg("PW");
            if (pw != "ERR")
            {
                String[] split = pw.Split('\t');
                return Int32.Parse(split[POWER]);
            }
            return -1;
        }

        public int getCurrentPower()
        {
            String pw = getMsg("PW");
            if (pw != "ERR")
            {
                String[] split = pw.Split('\t');
                return Int32.Parse(split[CURRENTPOWER]);
            }
            return -1;
        }
        public int getHeartBeat()
        {
            String hb = getMsg("PW");
            if (hb != "ERR")
            {
                String[] split = hb.Split('\t');
                return Int32.Parse(split[HEARTBEAT]);
            }
            return -1;
        }
        public decimal getSpeed()
        {
            String spd = getMsg("PW");
            if (spd != "ERR")
            {
                String[] split = spd.Split('\t');
                return decimal.Parse(split[SPEED])/10;
            }
            return -1;
        }
        public int getRPM()
        {
            String RPM = getMsg("VS");
            if (RPM != "ERR")
                return Int32.Parse(RPM);
            return -1;
        }
        public int getBikeID()
        {
            String ID = getMsg("ID");
            if (ID != "ERR") 
               return Int32.Parse(ID);
            return -1;
        }
        public String getTime()
        {
            String time = getMsg("TE");
            if (time != "ERR")
                return time;
            return null;
        }
    }
}
