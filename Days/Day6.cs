using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day6
    {
        public Day6()
        {

        }

        public string SolvePart1()
        {
            var ints = new List<int>(data.Split(',').Select(int.Parse));
            var int2 = new List<int>();
            for (int days = 0; days < 80; days++)
            {
                int counter = 0;
                for (int i = 0; i < ints.Count; i++)
                {
                    if (ints[i] == 0)
                    {
                        counter++;
                        int2.Add(6);
                    }
                    else
                    {
                        int2.Add(ints[i] - 1);
                    }
                }
                for (int i = 0; i < counter; i++)
                {
                    int2.Add(8);
                }
                ints = int2;
                int2 = new();
            }
            return ints.Count.ToString();
        }

        public string SolvePart2()
        {
            Dictionary<int, long> array = new();
            Dictionary<int, long> array2 = new();
            for (int i = 0; i <= 8; i++)
            {
                array[i] = 0;
            }
            foreach (var item in data.Split(',').Select(int.Parse))
            {
                array[item]++;
            }
            for (int i = 0; i < 256; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    array2[j-1] = array[j];
                }
                array2[6] += array[0];
                array2[8] = array[0];
                array = array2;
                array2 = new();
            }
            return array.Select(kvp => kvp.Value).Sum().ToString();
            
        }

        public static string tdata = "3,4,3,1,2";

        public static string data = @"5,3,2,2,1,1,4,1,5,5,1,3,1,5,1,2,1,4,1,2,1,2,1,4,2,4,1,5,1,3,5,4,3,3,1,4,1,3,4,4,1,5,4,3,3,2,5,1,1,3,1,4,3,2,2,3,1,3,1,3,1,5,3,5,1,3,1,4,2,1,4,1,5,5,5,2,4,2,1,4,1,3,5,5,1,4,1,1,4,2,2,1,3,1,1,1,1,3,4,1,4,1,1,1,4,4,4,1,3,1,3,4,1,4,1,2,2,2,5,4,1,3,1,2,1,4,1,4,5,2,4,5,4,1,2,1,4,2,2,2,1,3,5,2,5,1,1,4,5,4,3,2,4,1,5,2,2,5,1,4,1,5,1,3,5,1,2,1,1,1,5,4,4,5,1,1,1,4,1,3,3,5,5,1,5,2,1,1,3,1,1,3,2,3,4,4,1,5,5,3,2,1,1,1,4,3,1,3,3,1,1,2,2,1,2,2,2,1,1,5,1,2,2,5,2,4,1,1,2,4,1,2,3,4,1,2,1,2,4,2,1,1,5,3,1,4,4,4,1,5,2,3,4,4,1,5,1,2,2,4,1,1,2,1,1,1,1,5,1,3,3,1,1,1,1,4,1,2,2,5,1,2,1,3,4,1,3,4,3,3,1,1,5,5,5,2,4,3,1,4";
    }
}
