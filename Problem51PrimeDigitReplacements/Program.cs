using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem51PrimeDigitReplacements
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver solver = new Solver();
            Problem51PrimeDigitReplacements.Program.Solver.Result answer=solver.Solve();
            Util.printIntList(answer.combination);
        }

        public class Solver
        {
            public class Result
            {
                public int count;
                public List<int> combination;
            }

            public int currentMax=1000000;

            public Result Solve()
            {
                int[] primes=Util.getPrimes(currentMax);
                bool[] isPrime=Util.GetPrimesBoolArray(primes);
                Result current, highest = new Result() { count = 0 };
                for(int i=4;i<primes.Length;i++)
                {
                    current = getHighestPrimeReplacementCount(primes[i], isPrime);
                    if (current.count> highest.count)
                    {
                        highest = current;
                        Console.WriteLine("New Highest Count:" + highest.count);
                        Console.WriteLine("Prime:" + primes[i]);
                        Console.WriteLine("Lowest Relative:" + getLowestPrimeReplacement(current.combination, primes[i], isPrime));
                    }
                        
                }
                return highest;
            }

            Result getHighestPrimeReplacementCount(int number, bool[] isPrime)
            {
                List<int> digits = Util.GetDigits(number);
                int highestCount = 0,current;
                List<int> bestCombination=null;
                for(int i=1;i<digits.Count;i++)
                {
                    List<List<int>> digitCombinations = Util.GetCombinationsOfDigits(number, i);
                    foreach(List<int> combination in digitCombinations)
                    {
                        current = getPrimeCountFromReplacement(combination, digits, isPrime);
                        if(current>highestCount)
                        {
                            highestCount = current;
                            bestCombination = combination;
                        }
                    }
                }
                return new Result() { count = highestCount, combination = bestCombination };
            }

            int getPrimeCountFromReplacement(List<int> replacementIndices, List<int> digits, bool[] isPrime)
            {
                List<int> current=Util.CopyList(digits);
                int count = 0;
                //Take our combination, replace those digits in its indices with same value,check if prime
                for (int i = 0; i < 10;i++ )
                {
                    foreach(int index in replacementIndices)
                    {
                        current[index] = i;
                    }
                    if (current[current.Count-1]!=0 && isPrime[Util.GetNumberFromDigits(current)])
                        count++;
                }
                return count;
            }

            int getLowestPrimeReplacement(List<int> combination,int prime,bool[] isPrime)
            {
                List<int> digits = Util.GetDigits(prime);
                int lowest = prime;
                List<int> current = Util.CopyList(digits);

                for (int i = 0; i < 10; i++)
                {
                    foreach (int index in combination)
                    {
                        current[index] = i;
                    }
                    if (current[current.Count - 1] != 0 && isPrime[Util.GetNumberFromDigits(current)] && 
                        Util.GetNumberFromDigits(current)<lowest)
                        lowest=Util.GetNumberFromDigits(current);
                }
                return lowest;
            }

        }
    }
}
