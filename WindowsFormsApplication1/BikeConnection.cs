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
    }
}