using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    interface Bike
    {
        void reset();
        void setPower(int power); // Set maximum power in Watt
        int getPower(); // Maximum power in Watt
        int getCurrentPower(); // Current power in Watt
        int getHeartBeat(); // Heartbeat in beats per minute
        int getRPM(); // Rotations of the wheel per minute
        int getBikeID();
        String getTime();
    }
}
