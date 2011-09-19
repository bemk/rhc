using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class VirtBike : Bike
    {
        private Int32 heartRate = 0;
        private Int32 speed = 0;
        private Int32 power = 0;
        private Int32 currentPower = 0;
        private Int32 RPM = 0;
        private Int32 ID = 0;
        private Int32 distance = 0;
        private Int32 energy = 0;
        private string time = "00:00:00";
        private bool heartRateConnected = false; // The checkbox is unchecked by default.

        public VirtBike() 
        {
            Reset();
            Random i = new Random();
            this.ID = i.Next();
        }

        // Remove this constructor if possible ,,
        public VirtBike(int heartRate, int rpm, int speed, int distance, int power, int energy, int currentPower, string time)
        {
            this.heartRate = heartRate;
            this.RPM = rpm;
            this.speed = speed;
            this.distance = distance;
            this.power = power;
            this.energy = energy;
            this.currentPower = currentPower;
            Random i = new Random();
            this.ID = i.Next();
        }
        ~VirtBike()
        {
        }

        public void Reset() 
        {
            heartRate = 0;
            speed = 0;
            power = 25;
            currentPower = 0;
            RPM = 0;
            ID = 0;
            distance = 0;
            energy = 0;
            time = "00:00:00";
        }

        #region virtual bike setters

        public void SetHeartRateConnected(bool newHeartRateConnected)
        {
            heartRateConnected = newHeartRateConnected;
        }

        public void SetPower(int newPower) 
        {
            power = newPower;
        }

        public void SetHeartRate(int newHeartRate)
        {
            heartRate = newHeartRate;
        }

        public void SetRPM(int newRPM)
        {
            RPM = newRPM;
        }

        public void SetSpeed(int newSpeed)
        {
            speed = newSpeed;
        }

        public void SetDistance(int newDistance)
        {
            distance = newDistance;
        }

        public void SetEnergy(int newEnergy)
        {
            energy = newEnergy;
        }

        public void SetCurrentPower(int newCurrentPower)
        {
            currentPower = newCurrentPower;
        }

        public void SetTime(string newTime)
        {
            time = newTime;
        }

        #endregion

        #region virtual bike getters

        public int GetPower()
        {
            return power;
        }

        public int GetCurrentPower()
        {
            return currentPower;
        }

        public int GetHeartRate()
        {
            return heartRate;
        }

        public int GetDistance()
        {
            return distance;
        }

        public int GetRPM() 
        { 
            return RPM;
        }

        public decimal GetSpeed()
        {
            return speed / 10;
        }

        public int GetBikeID() 
        { 
            return ID;
        }

        public string GetTime()
        {
            return time;
        }

        public bool GetHeartRateConnected()
        {
            return heartRateConnected;
        }

        public int GetEnergy()
        {
            return energy;
        }

        #endregion
    }
}
