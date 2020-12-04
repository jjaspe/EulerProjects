using System;
using System.Collections.Generic;

namespace MathUtil
{
    public static class MathUtil
    {
        public static List<int> IndecesFromBinary(double binary)
        {
            var max = Math.Floor(Math.Log(binary, 2)+1);
            List<int> indeces = new List<int>();
            for (int i = 0; i < max; i++)
            {
                if (binary % 2 == 1)
                {
                    binary -= 1;
                    indeces.Add(i);
                }
                binary /= 2;
            }
            return indeces;
        }

        public static List<int> GetBinaryValuesForPartitions(int maxPower)
        {
            var partitions = new List<int>();
            for (int i = 0; i < Math.Pow(2, maxPower); i++)
            {
                partitions.Add(i);
            }
            return partitions;
        }

        public static void RunForPartitions(int max, Action<double> action)
        {
            for (int i = 0; i < Math.Pow(2, max); i++)
            {
                action(i);
            }
        }

        public static void RunForPartitions(int max, Func<double, bool> action)
        {
            for (int i = 0; i < Math.Pow(2, max); i++)
            {
                if (!action(i))
                {
                    break;
                }
            }
        }
    }
}
