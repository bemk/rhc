using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public interface Bike
    {
        void Reset();
        void SetPower(int power); // Set maximum power in Watt
        int GetDistance();
        int GetPower(); // Maximum power in Watt
        int GetCurrentPower(); // Current power in Watt
        int GetHeartRate(); // Heartbeat in beats per minute
        int GetRPM(); // Rotations of the wheel per minute
        int GetEnergy();
        decimal GetSpeed();
        int GetBikeID();
        string GetTime();
    }
}
