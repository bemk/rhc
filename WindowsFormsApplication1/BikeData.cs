using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class BikeData
    {
        
        private int heartRate = 0;
        private int RPM = 0;
        private int speed = 0;
        private int distance = 0;
        private int power = 0;
        private int energy = 0;
        private int currentPower = 0;
        private string time = "";

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
            //this.pointsHeartrate = pointsHeartrate;
            //.pointsRPM = pointsRPM;
            //this.pointsSpeed = pointsSpeed;
            //this.pointsDistance = pointsDistance;
            //this.pointsPower = pointsPower;
            //this.pointsEnergy = pointsEnergy;
            //this.pointsCurrentPower = pointsCurrentPower;
        }
    }
}
