using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace RHC
{
    class Program
    {
        public static void Main()
        {
            Program p = new Program();
            p.ReadData();
            p.SearchCommands();
        }

        public void SearchCommands()
        {
            SerialPort sp = new SerialPort("COM3");
            using (sp)
            {
                sp.Open();
                for (char a = 'a'; a <= 'z'; a++)
                {
                    for (char b = 'a'; b <= 'z'; b++)
                    {
                        string write = "" + a + b;
                        sp.WriteLine(write);

                        string read = sp.ReadLine();
                        if (!read.Contains("ERR"))
                        {
                            Console.WriteLine("Commando: " + write);
                            Console.WriteLine(read);
                        }
                    }
                }
                sp.Close();
            }
        }

        public void ReadLine()
        {
            SerialPort sp = new SerialPort("COM3");
            using (sp)
            {
                sp.Open();
                for (char a = 'a'; a <= 'z'; a++)
                {
                    for (char b = 'a'; b <= 'z'; b++)
                    {
                        string write = "" + a + b;
                        sp.WriteLine(write);

                        string read = sp.ReadLine();
                        if (!read.Contains("ERR"))
                        {
                            Console.WriteLine("Commando: " + write);
                            Console.WriteLine(read);
                        }
                    }
                }
                sp.Close();
            }
        }

        public void ReadData()
        {
            SerialPort sp = new SerialPort("COM3");
            using (sp)
            {
                sp.Open();
                while (true)
                {
                    sp.WriteLine("pw");

                    string read = sp.ReadLine();
                    if (!read.Contains("ERR"))
                    {
                        Console.WriteLine("HEART   RPM     SPEED   DIST.   POWER   ENERGY  TIME    HUIDIG POWER");
                        Console.WriteLine(read);
                        Console.WriteLine();
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
