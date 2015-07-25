using EulerMisc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PentagonalNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            PentagonalNumbers number=new PentagonalNumbers();
            IOUtil.WatchedPrint(number.solveProblemPentagonalNumbers);
        }

        public class PentagonalNumbers
        {
            public double max=100000000000;
            public double getPentagonal(int n)
            {
                double nTh=n*(3*n-1)/2;
                return nTh;
            }

            public bool isPentagonal(double m)
            {
                double n = (1 + Math.Sqrt(1 + 24 * m)) / 6;
                return (n - Math.Floor(n)) == 0;
            }

            public double solveProblemPentagonalNumbers()
            {
                List<double> pentagonals = new List<double>();
                for (int i = 1; i < max; i++)
			    {
                    double current = getPentagonal(i);
                    for (int j = pentagonals.Count-1; j >=0 ; j--)
                    {
                        if(isPentagonal(current + pentagonals[j])&&isPentagonal(current-pentagonals[j]))
                        {
                            return current-pentagonals[j];
                        }
                    }
                    pentagonals.Add(getPentagonal(i));
			    }
                return -1;

            }

        }

        public class PrimePermutationsProblem49
        {
            bool[] primes;
            public double solveProblem49()
            {
                primes=Util.GetPrimesBoolArray(Util.getPrimes(10000));

                return -1;
            }
        }
    }
}
