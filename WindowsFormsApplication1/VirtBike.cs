using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class VirtBike : Bike
    {
        private Int32 heartBeat = 0;
        private Int32 speed = 0;
        private Int32 power = 0;
        private Int32 currentPower = 0;
        private Int32 RPM = 0;
        private Int32 ID = 0;

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
        public int GetHeartBeat() 
        { 
            return this.heartBeat;
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
        public String GetTime() 
        { 
            return null;
        }
    }
}
