using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    [Serializable]
    public class BikeData 
    {
        // WHy the f*ck so many fields!? Clean up pl0x
        public List<Point> pointsHeartrate{get; set;}
        public List<Point> pointsRPM {get; set;}
        public List<Point> pointsSpeed {get; set;}
        public List<Point> pointsDistance { get; set; }
        public List<Point> pointsPower { get; set; }
        public List<Point> pointsEnergy { get; set; }
        public List<Point> pointsCurrentPower { get; set; }
        public int heartRate { get; set; }
        public int RPM { get; set; }
        public int speed { get; set; }
        public int distance { get; set; }
        public int power { get; set; }
        public int energy { get; set; }
        public int currentPower { get; set; }
        public string time { get; set; }

        public BikeData(int heartRate, int RPM, int speed, int distance, int power, int energy, int currentPower, string time)
        {
            this.heartRate = heartRate;
            this.RPM = RPM;
            this.speed = speed;
            this.distance = distance;
            this.power = power;
            this.energy = energy;
            this.currentPower = currentPower;
            this.time = time;
            this.pointsHeartrate = new List<Point>();
            this.pointsRPM = new List<Point>(); 
            this.pointsSpeed = new List<Point>(); 
            this.pointsDistance = new List<Point>(); 
            this.pointsPower = new List<Point>(); 
            this.pointsEnergy = new List<Point>(); 
            this.pointsCurrentPower = new List<Point>(); 
        }
    }
}
