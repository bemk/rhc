using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace RHC
{
    class Program
    {
        public SerialPort SERIAL_PORT = new SerialPort("COM3");
        public string GET_DATA = "pw";

        public static void Main()
        {
            try
            {
                Program p = new Program();
                while (true)
                {
                    p.ReadData(Console.ReadLine());
                }
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

        public void SearchCommands()
        {
            using (SERIAL_PORT)
            {
                SERIAL_PORT.Open();
                for (char a = 'a'; a <= 'z'; a++)
                {
                    for (char b = 'a'; b <= 'z'; b++)
                    {
                        Console.WriteLine("" + a + b);
                        for (int c = 0; c <= 5; c++)
                        {
                            string write = "" + a + b + " " + c;
                            SERIAL_PORT.WriteLine(write);

                            string read = SERIAL_PORT.ReadLine();
                            if (!read.Contains("ERR"))
                            {
                                Console.WriteLine("Command: " + write);
                                Console.WriteLine(read);
                            }
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

        public void ReadData(string command)
        {
            int heartRate, RPM, speed, distance, power, energy, currentPower;
            string time;
            using (SERIAL_PORT)
            {
                SERIAL_PORT.Open();
                //while (true)
                //{
                    SERIAL_PORT.WriteLine(command);

                    string read = SERIAL_PORT.ReadLine();
                    if (!read.Contains("ERR"))
                    {
                        string[] split = read.Split('\t');
                        heartRate = Convert.ToInt32(split[0]);
                        RPM = Convert.ToInt32(split[1]);
                        speed = Convert.ToInt32(split[2]);
                        distance = Convert.ToInt32(split[3]);
                        power = Convert.ToInt32(split[4]);
                        energy = Convert.ToInt32(split[5]);
                        time = split[6];
                        currentPower = Convert.ToInt32(split[7]);
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
                    else
                    {
                        Console.WriteLine("Command " + command + " not found.");
                    }
                    // Delay 1000 ms.
                    //System.Threading.Thread.Sleep(1000);
               // }
            }
        }
    }
}
