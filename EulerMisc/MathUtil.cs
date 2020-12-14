using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerMisc
{
    public static class MathUtil
    {
        public static List<int> IndecesFromBinary(double binary)
        {
            var max = Math.Log(binary, 2);
            List<int> indeces = new List<int>();
            for (int i = 0; i < max; i++)
            {
                if(binary % 2 == 1)
                {
                    binary -= 1;
                    indeces.Add(i);
                }
                binary /= 2;
            }
            return indeces;
        }

        public static List<double> GetPartitions(int max)
        {
            var partitions = new List<double>();
            for (int i = 0; i < Math.Pow(2, max); i++)
            {
                partitions.Add(i);
            }
            return partitions;
        }
    }
}
