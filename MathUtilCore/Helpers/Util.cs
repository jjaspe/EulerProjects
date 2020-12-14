using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MathUtilCore.Helpers
{
    public static class Util
    {
        public static class PrimeDecompositions
        {
            /// <summary>
            /// 
            /// </summary>
            /// <returns>Dict[n] returns highest prime m in prime decomposition of n</returns>
            public static Dictionary<int,int> GetPrimeDecompositionHighestPrime(int max, int[] primes)
            {
                var PrimeDecompositionHighestPrime
                    = new Dictionary<int, int>();
                for (int i = 2; i <= max; i++)
                {
                    PrimeDecompositionHighestPrime[i] = 0;
                    for (int j = 0; j < primes.Length && i >= primes[j]; j++)
                    {
                        if (i % primes[j] == 0 && primes[j] > PrimeDecompositionHighestPrime[i])
                        {
                            PrimeDecompositionHighestPrime[i] = primes[j];
                        }
                    }
                }
                return PrimeDecompositionHighestPrime;
            }
        }
        public static int[] getPrimes(int n)
        {
            LinkedList<int> primes = new LinkedList<int>();
            primes.AddFirst(2);
            for (int i = 3; i <= n; i++)
            {
                if (isPrime(i, primes))
                {
                    primes.AddLast(i);
                }
            }

            return primes.ToArray<int>();
        }

        public static bool isPrime(int n, LinkedList<int> primes)
        {
            foreach (int i in primes)
            {
                if (i * i > n)
                    return true;
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        public static int[] GetPrimeDecompositions(int[] primes, int number)
        {
            var current = number;
            int[] decomp = new int[primes.Length];
            for (int i = 0; i < primes.Length && current > 1; i++)
            {
                while (current % primes[i] == 0)
                {
                    decomp[i]++;
                    current /= primes[i];
                }
            }
            return decomp;
        }

        public static int[] SquareDecomp(int[] decomp)
        {
            for (int i = 0; i < decomp.Length; i++)
            {
                decomp[i] *= 2;
            }
            return decomp;
        }

        public static int[][] GetAllPrimeDecompositions(int max, int[] primes)
        {
            int[][] decompositions = new int[max + 1][];
            for (int i = 1; i <= max; i++)
            {
                var decomp = GetPrimeDecompositions(primes, i);
                decompositions[i] = decomp;
            }
            return decompositions;
        }

        public static int[] MultiplyDecompositions(int[] decomp1, int[] decomp2)
        {
            int[] result = new int[Math.Max(decomp1.Length, decomp2.Length)];
            for (int i = 0; i < result.Length; i++)
            {
                int value1 = decomp1.Length > i ? decomp1[i] : 0;
                int value2 = decomp2.Length > i ? decomp2[i] : 0;
                result[i] = value1 + value2;
            }
            return result;
        }

        public static double GetValueFromDecomposition(int[] decomp, int[] primes)
        {
            double current = 1;
            for (int i = 0; i < decomp.Length; i++)
            {
                var index = decomp[i];
                while (index > 0)
                {
                    current *= primes[i];
                    index--;
                }
            }
            return current;
        }

        public static (double, int[]) ReduceFraction(double numerator, int[] denominator, int[] primes)
        {
            for (int i = 0; i < denominator.Length; i++)
            {
                while (denominator[i] > 0 && numerator % primes[i] == 0)
                {
                    denominator[i]--;
                    numerator /= primes[i];
                }
            }
            return (numerator, denominator);
        }

        public static class BinaryAlgorithms
        {
            public static (int,bool) GetIndex<T>(List<T> sortedList, T toFind, Func<T,T,int> comparator)
            {
                var low = 0;
                var high = sortedList.Count - 1;
                var middle = 0;
                //[1,10], 11
                while (low <= high)
                {
                    middle = (high + low) / 2;
                    var comparison = comparator(toFind,sortedList[middle]);
                    if (comparison == 0)
                    {
                        return (low,true);
                    } 
                    else if(comparison > 0)
                    {
                        low = middle + 1;
                    } else
                    {
                        high = middle - 1;
                    }
                }
                return (low, false);
            }

            public static void Insert<T>(List<T> sortedList, T toInsert, Func<T,T,int> comparator, bool allowDupes = true)
            {
                var (index, found) = GetIndex<T>(sortedList, toInsert, comparator);
                if(allowDupes || !found)
                    sortedList.Insert(index, toInsert);
            }

            public static void Insert(List<double> sortedList, double toInsert, bool allowDupes = true)
            {
                Insert<double>(sortedList, toInsert, (a, b) => (int)(a - b), allowDupes);
            }

            public static (int,bool) GetIndex(List<double> sortedList, double toInsert)
            {
                return GetIndex<double>(sortedList, toInsert, (a, b) => (int)(a - b));
            }

            public static void Insert(List<BigInteger> sortedList, BigInteger toInsert, bool allowDupes = true)
            {
                Insert<BigInteger>(sortedList, toInsert, (a, b) => (int)(a - b), allowDupes);
            }

            public static (int, bool) GetIndex(List<BigInteger> sortedList, BigInteger toInsert)
            {
                return GetIndex<BigInteger>(sortedList, toInsert, (a, b) => {
                    var result = (a - b);
                    return result > 0 ? 1 : result < 0 ? -1 : 0;
                });
            }
        }

        public static class Factorial
        {
            public static int GetNextOne(int previous, int n, int newR)
            {
                return previous * (n - (newR - 1)) / newR;
            }
        }

        public static class FileManipulation
        {
            public static string[] fileParser(string filename)
            {
                StreamReader r = new StreamReader(filename);

                string[] strings = r.ReadToEnd().Split(',');
                return strings;

            }
        }

        public static bool[] GetMultiples(int n)
        {
            var multiples = new bool[n+1];
            var sqrt = Math.Sqrt(n);
            for (var i = 2; i <= sqrt; i++)
            {
                if (multiples[i])
                    continue;
                var multiplier2 = i;
                while ((i * multiplier2) <= n && (i*multiplier2>0))
                {
                    multiples[i * multiplier2] = true;
                    multiplier2++;
                }
            }
            return multiples;
        }
    }
}
