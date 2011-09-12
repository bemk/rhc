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

        public BikeData()
        {
            HeartRates = new List<int>();
        }

        public int Last(List<int> list)
        {
            return list[list.Count - 1];
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
