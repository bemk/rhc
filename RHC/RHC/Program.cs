using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace RHC
{
    public class Program
    {
        public SerialPort SERIAL_PORT = new SerialPort("COM3");
        public string GET_DATA = "pw";

        public delegate void DataEvent(String data);
        public event DataEvent NewDataRead;

        public static void Main()
        {
            try
            {
                Program p = new Program();
                Form1 form = new Form1(p);
                Application.Run(form);                
                //p.SearchCommands();
            }
            catch (Exception e)
            {
                Console.WriteLine("Progam not started: " + e);
            }
        }

        public Program()
        {
            

        }

        public void StartReadData()
        {
            //Event Driven Programming
            SERIAL_PORT.Open();
            SERIAL_PORT.DataReceived += OnDataReceived;
            SERIAL_PORT.WriteLine(GET_DATA);
        }

        private class Pokeman : Exception {}
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //On Event, do stuff, and initiate the event again (trigger)
            try
            {                
                string data = (sender as SerialPort).ReadLine();
                if (!data.Contains("ERR"))
                {
                    NewDataRead.Invoke(data);
                }
                SERIAL_PORT.WriteLine(GET_DATA); //event trigger
            }
            catch (Pokeman)
            {
                //Catch all the things!
                //http://troll.me/images/x-all-the-things/catch-all-the-pokemans.jpg
            }

        }

        public void SearchCommands()
        {
            using (SERIAL_PORT)
            {
                SERIAL_PORT.Open();
                for (char a = 'a'; a <= 'z'; a++)
                {
                    for (char b = 'a'; b <= 'z'; b++)
                    {
                        string write = "" + a + b;
                        SERIAL_PORT.WriteLine(write);

                        string read = SERIAL_PORT.ReadLine();
                        if (!read.Contains("ERR"))
                        {
                            Console.WriteLine("Commando: " + write);
                            Console.WriteLine(read);
                        }
                    }
                }
                SERIAL_PORT.Close();
            }
        }

        public void ReadLine()
        {
            using (SERIAL_PORT)
            {
                SERIAL_PORT.Open();
                for (char a = 'a'; a <= 'z'; a++)
                {
                    for (char b = 'a'; b <= 'z'; b++)
                    {
                        string write = "" + a + b;
                        SERIAL_PORT.WriteLine(write);

                        string read = SERIAL_PORT.ReadLine();
                        if (!read.Contains("ERR"))
                        {
                            Console.WriteLine("Command: " + write);
                            Console.WriteLine(read);
                        }
                    }
                }
                SERIAL_PORT.Close();
            }
        }

        public void ReadDataRuk()
        {
            using (SERIAL_PORT) //Verkeerd gebruik van using keyword
            {
                SERIAL_PORT.Open();
                while (true)
                {
                    SERIAL_PORT.WriteLine(GET_DATA);

                    string read = SERIAL_PORT.ReadLine();
                    if (!read.Contains("ERR"))
                    {
                        string[] split = read.Split('\t');
                        int heartRate = Convert.ToInt32(split[0]);
                        int RPM = Convert.ToInt32(split[1]);
                        int speed = Convert.ToInt32(split[2]);
                        int distance = Convert.ToInt32(split[3]);
                        int power = Convert.ToInt32(split[4]);
                        int energy = Convert.ToInt32(split[5]);
                        string time = split[6];
                        int currentPower = Convert.ToInt32(split[7]);
                        //Console.WriteLine("HEART   RPM     SPEED   DIST.   POWER   ENERGY  TIME    HUIDIG POWER");
                        //Console.WriteLine(read);
                        Console.WriteLine("HeartRate: " + heartRate);
                        Console.WriteLine("RPM: " + RPM);
                        Console.WriteLine("Speed: " + speed);
                        Console.WriteLine("Distance: " + distance);
                        Console.WriteLine("Power: " + power);
                        Console.WriteLine("Energy: " + energy);
                        Console.WriteLine("Time: " + time);
                        Console.WriteLine("CurrentPower: " + currentPower);

                        // Empty line
                        Console.WriteLine();                   
                    }
                    // Delay 1000 ms.
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
