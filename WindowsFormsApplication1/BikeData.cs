using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    [Serializable]
    public class BikeData
    {
        public List<int> HeartRates;
        public List<int> RPMs;
        public List<int> Speeds;
        public List<int> Distances;
        public List<int> Powers;
        public List<int> Energies;
        public List<int> CurrentPowers;
        public string Time;

        public BikeData()
        {
            HeartRates = new List<int>();
            RPMs = new List<int>();
            Speeds = new List<int>();
            Distances = new List<int>();
            Powers = new List<int>();
            Energies = new List<int>();
            CurrentPowers = new List<int>();
            Time = "0:00";
        }

        public int Average(List<int> list)
        {
            int total = 0;
            foreach (int number in list)
            {
                total += number;
            }
            return total / list.Count;
        }
    }
}
