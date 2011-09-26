using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class DatabaseManager
    {
        public void Insert(string row)
        {
            //append row to file
        }

        public string SelectLatest()
        {
            //read last row from file
            return "";
        }

        public List<String> SelectAll()
        {
            //read whole file
            return new List<string>();
        }

        /// <summary>
        /// LINQ/Lambda expressions sample. Select from list based on string length.
        /// </summary>
        /// <param name="size">The string length</param>
        /// <returns>A list of strings of the size you choose</returns>
        public List<string> SelectStringBySize(int size)
        {
            List<string> list = new List<string>();

            //Lambda
            List<string> result2 = list.Where(l => l.Length == size).ToList();

            //LINQ
            var result = from l in list
                         where l.Length == size
                         select l;

            return result.ToList();
        }
    }
}
