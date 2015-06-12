using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem35CircularPrimes
{
    public class Solver
    {
        internal static int solve()
        {
            int[] primes = Util.getPrimes(1000000);
            bool[] isPrime = Util.GetPrimesBoolArray(primes);
            List<List<int>> digitizedPrimes = removeObviousNonCirculars(digitize(primes));
            
            List<int> currentRotations;
            bool circular;

            int count = 0;
            for(int i=0;i<digitizedPrimes.Count;i++)
            {
                Console.WriteLine(i);
                currentRotations = getRotations(digitizedPrimes[i]);
                circular = true;
                foreach(int j in currentRotations)
                {
                    if(!isPrime[j])
                    {
                        circular = false;
                        break;                        
                    }
                }
                if (circular)
                {
                    count++;
                    Console.WriteLine("Prime:" + currentRotations[0]);
                }
            }
            return count;
        }

        static List<List<int>> digitize(int[] primes)
        {
            List<List<int>> digitizedPrimes = new List<List<int>>();
            foreach(int prime in primes)
            {
                digitizedPrimes.Add(Util.GetDigits(prime));
            }
            return digitizedPrimes;
        }

        public static List<int> getRotations(List<int> digits)
        {
            List<int> rotations = new List<int>();

            List<List<int>> rotatedDigits = Util.GetRotationsOfDigits(digits);
            foreach(List<int> rotation in rotatedDigits)
            {
                rotations.Add(Util.GetNumberFromDigits(rotation));
            }
            return rotations;
        }

        static List<List<int>> removeObviousNonCirculars(List<List<int>> digitized)
        {
            List<List<int>> digitizedSmall = new List<List<int>>();
            foreach(List<int> prime in digitized)
            {
                if (!prime.Contains(0) && !prime.Contains(2) && !prime.Contains(4) && !prime.Contains(5) && !prime.Contains(6) && !prime.Contains(8))
                    digitizedSmall.Add(prime);
            }
            return digitizedSmall;
        }
    }
}
