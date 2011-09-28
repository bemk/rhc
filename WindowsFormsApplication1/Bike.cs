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
        int GetDistance(); // Distance in meters
        int GetPower(); // Maximum power in Watt
        int GetCurrentPower(); // Current power in Watt
        int GetHeartRate(); // Heartbeat in beats per minute
        int GetRPM(); // Rotations of the wheel per minute (rounds per minute)
        int GetEnergy(); // Energy in KiloJoule
        decimal GetSpeed(); // (Speed / 10) is in km/h
        int GetBikeID();
        string GetTime();
    }
}
