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
        public List<Point> pointsHeartrate{get; set;}
        public List<Point> pointsRPM {get; set;}
        public List<Point> pointsSpeed {get; set;}
        public List<Point> pointsDistance { get; set; }
        public List<Point> pointsPower { get; set; }
        public List<Point> pointsEnergy { get; set; }
        public List<Point> pointsCurrentPower { get; set; }
        private int heartRate { get; set; }
        private int RPM { get; set; }
        private int speed { get; set; }
        private int distance { get; set; }
        private int power { get; set; }
        private int energy { get; set; }
        private int currentPower { get; set; }
        private string time { get; set; }

        public BikeData(int heartRate, int RPM, int speed, int distance, int power, int energy, int currentPower, string time,
                        List<Point> pointsHeartrate, List<Point> pointsRPM, List<Point> pointsSpeed, List<Point> pointsDistance, 
                        List<Point> pointsPower, List<Point> pointsEnergy, List<Point> pointsCurrentPower)
        {
            this.heartRate = heartRate;
            this.RPM = RPM;
            this.speed = speed;
            this.distance = distance;
            this.power = power;
            this.energy = energy;
            this.currentPower = currentPower;
            this.time = time;
            this.pointsHeartrate = pointsHeartrate;
            this.pointsRPM = pointsRPM;
            this.pointsSpeed = pointsSpeed;
            this.pointsDistance = pointsDistance;
            this.pointsPower = pointsPower;
            this.pointsEnergy = pointsEnergy;
            this.pointsCurrentPower = pointsCurrentPower;
        }
    }
}
