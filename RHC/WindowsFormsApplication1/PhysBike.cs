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
            getMsg("PW");
            return -1;
        }

        public int getCurrentPower()
        {
            String power = getMsg("PW");
            return -1;
        }
        public int getHeartBeat()
        {
            String hb = getMsg("PW");
            return -1;
        }
        public int getRPM()
        {
            String RPM = getMsg("VS");
            return -1;
        }
        public int getBikeID()
        {
            String ID = getMsg("ID");
            return Int32.Parse(ID);
        }
        public String getTime()
        {
            return ("TE");
        }
    }
}
