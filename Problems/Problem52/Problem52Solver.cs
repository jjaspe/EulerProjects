using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problems.Problem52
{
    public class Problem52Solver
    {
        /* It can be seen that the number, 125874, and its double, 251748, 
         * contain exactly the same digits, but in a different order.
         * Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, 
         * contain the same digits. */

        //First digit of x must be 1, bc if > 1 it 6x would have one more digit than x
        //Second digit 1-6
        public double Solve(int max)
        {
            double solution = 0;
            int highest10Power = 1;
            double powerOf10 = Math.Pow(10, highest10Power);
            bool found = false;

            while (!found && powerOf10 < max)
            {
                for (int i = 0; i < powerOf10; i++)
                {
                    if (AllSameDigits(i,highest10Power+1))
                        return i;
                }
                highest10Power++;
                powerOf10 = Math.Pow(10, highest10Power);
            }            

            return solution;
        }

        public bool AllSameDigits(double x, int decimalPlaces)
        {
            return SameDigits(x, 2, decimalPlaces)
                && SameDigits(x, 3, decimalPlaces)
                && SameDigits(x, 4, decimalPlaces)
                && SameDigits(x, 5, decimalPlaces)
                && SameDigits(x, 6, decimalPlaces);
        }

        public bool SameDigits(double x, double multiplier, int decimalPlaces)
        {
            var digits1 = GetDigits(x, decimalPlaces);
            var digits2 = GetDigits(multiplier * x, decimalPlaces);
            var intersect = digits1.Intersect(digits2);
            return digits2.Count == intersect.Count();
        }

        public List<int> GetDigits(double x, int decimalPlaces)
        {
            var digits = new List<int>();
            for (int i = 0; i < decimalPlaces; i++)
            {
                digits.Add((int)x % 10);
                x = x / 10;
            }
            return digits;
        }
    }
}
