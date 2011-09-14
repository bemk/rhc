using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class VirtBike : Bike
    {
        private Int32 HeartBeat = 0;
        private Int32 Speed = 0;
        private Int32 Power = 0;
        private Int32 CurrentPower = 0;
        private Int32 RPM = 0;
        private Int32 ID = 0;

        public VirtBike()
        {
            this.reset();
            Random i = new Random();
            this.ID = i.Next();
        }
        ~VirtBike()
        {
        }
        public void reset() {
            this.Power = 65;
            this.CurrentPower = 0;
        }
        public void setPower(int power) {
            this.Power = power;
        }
        public int getPower() { 
            return this.Power; 
        }
        public int getCurrentPower() { 
            return this.CurrentPower;
        }
        public int getHeartBeat() { 
            return this.HeartBeat;
        }
        public int getRPM() { 
            return this.RPM;
        }
        public decimal getSpeed()
        {
            return this.Speed;
        }
        public int getBikeID() { 
            return this.ID;
        }
        public String getTime() { 
            return null;
        }
    }
}
