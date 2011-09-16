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
        private string time = "";
        private bool heartRateConnected = true;

        public VirtBike()
        {
            Reset();
            Random i = new Random();
            this.ID = i.Next();
        }
        ~VirtBike()
        {
        }
        public void Reset() 
        {
            this.power = 25;
            this.currentPower = 0;
        }
        public void SetPower(int power) 
        {
            this.power = power;
        }
        public int GetPower() 
        { 
            return this.power; 
        }
        public int GetCurrentPower() 
        { 
            return this.currentPower;
        }
        public int GetHeartRate() 
        { 
            return this.heartRate;
        }

        public int GetDistance()
        {
            return this.distance;
        }

        public void SetHeartRate(int heartRate)
        {
            this.heartRate = heartRate;
        }
        public int GetRPM() 
        { 
            return this.RPM;
        }
        public decimal GetSpeed()
        {
            return this.speed;
        }
        public int GetBikeID() 
        { 
            return this.ID;
        }
        public string GetTime()
        {
            return time;
        }

        public void SetHeartRateConnected(bool b)
        {
            heartRateConnected = b;
        }

        public bool GetHeartRateConnected()
        {
            return heartRateConnected;
        }

        public int GetEnergy()
        {
            return energy;
        }

        public void SetRPM(int p)
        {
            RPM = p;
        }

        public void SetSpeed(int p)
        {
            speed = p;
        }
    
        public void SetDistance(int p)
        {
            distance = p;
        }

        public void SetEnergy(int p)
        {
            energy = p;
        }

        public void SetCurrentPower(int p)
        {
            currentPower = p;
        }

        public void SetTime(string p)
        {
            time = p;
        }
    }
}
