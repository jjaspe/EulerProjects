using MathUtilCore;
using MathUtilCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Problems.Problem152
{
    public class DenomListWithInverseSquareSum
    {
        public List<int> Denoms = new List<int>();
        public Fraction Value { get; set; }
    }

    public class Problem152Solver
    {
        int[] primes;
        public Dictionary<int, int> PrimeDecompositionHighestPrime
            = new Dictionary<int, int>();
        int[] AllowedDenoms;
        int[] ManualAllowedDenoms = { 2, 3, 4, 5, 8, 12, 13, 21, 24, 30, 35, 39, 40, 52, 56 };
        decimal[] MaxLefts;
        Fraction[] MaxLeftFractions;
        SortedList<string, List<DenomListWithInverseSquareSum>> PreAddedDenoms 
            = new SortedList<string,List<DenomListWithInverseSquareSum>>();
        Fraction half = new Fraction(1, 2);

        public List<List<int>> Solve()
        {
            return Solve(35, 35);
        }

        public List<List<int>> Solve(int max, int split)
        {
            Setup(max);
            var stack = new Stack<int>();
            stack.Push(2);
            var splitIndex = AllowedDenoms.ToList().IndexOf(split);
            PreAddSums(splitIndex, new Fraction(0,1), new Stack<int>());
            PreAddedDenoms["0_1"] = new List<DenomListWithInverseSquareSum>()
            {
               new DenomListWithInverseSquareSum()
               {
                   Value = new Fraction(0,1)
               }
            };
            var closeSolutions = FindSolutionsWithStackAndPreadded(stack, new Fraction(1,4), 1, splitIndex);
            var realSolutions = closeSolutions.Where(n => IsSolutionForReals(n)).ToList();
            return realSolutions;
        }

        private List<List<int>> FindSolutionsWithStackAndPreadded(Stack<int> denomsUsed, Fraction value, int nextLowest, int max)
        {
            var solutions = new List<List<int>>();
            bool stop = false;
            for (int i = nextLowest; i < max && !stop; i++)
            {
                denomsUsed.Push(AllowedDenoms[i]);
                var newFraction = new Fraction(1, AllowedDenoms[i] * AllowedDenoms[i]);
                value.Add(newFraction);
                value.Reduce(primes);
                var solutionResult = IsSolution(PreAddedDenoms, value);
                if (solutionResult.Item1)
                {
                    solutionResult.Item2.ForEach(n =>
                    {
                        var solution = denomsUsed.ToList();
                        solution.AddRange(n.Denoms);
                        solutions.Add(solution);
                    });                    
                }
                //var decimalValue = (decimal)value.Numerator / (decimal)value.Denominator;
                //if (decimalValue + MaxLefts[i] > 0.5m)// && level < max)
                if(value + MaxLeftFractions[i] >= half)
                {
                    //if (decimalValue < 0.5m)
                    //{
                    //if(value < half)
                        solutions.AddRange(FindSolutionsWithStackAndPreadded(denomsUsed, value, i + 1, max));
                    //}
                }
                else //Since the value so far, plus the sum of all possible later values is less that 1/2
                        //we dont need to check subsequent combinations, since they will be smaller;
                {
                    stop = false;
                }
                value.Subtract(newFraction);
                value.Reduce(primes);
                denomsUsed.Pop();
            }
            return solutions;
        }

        private (bool,List<DenomListWithInverseSquareSum>) IsSolution(SortedList<string, List<DenomListWithInverseSquareSum>> preAddedDenoms, 
            Fraction value)
        {
            Fraction left = new Fraction(1, 2);
            left.Subtract(value);
            left.Reduce(primes);
            var found = (preAddedDenoms.TryGetValue(left.Id, out var denomListWithInverseSquareSum));
            return (found, denomListWithInverseSquareSum);
        }

        #region Setup
        void Setup(int max)
        {
            primes = Util.getPrimes(max);
            this.PrimeDecompositionHighestPrime = 
                Util.PrimeDecompositions.GetPrimeDecompositionHighestPrime(max, primes);
            var denoms = GetAllowedDenomsByHand(max).ToList();
            denoms.Sort();
            AllowedDenoms = denoms.ToArray();
            //AllowedDenoms = ManualAllowedDenoms;
            MaxLefts = GetMaxLeftsHigher(AllowedDenoms);
            MaxLeftFractions = GetMaxLeftsHigherFractions(AllowedDenoms);
        }

        public int[] GetAllowedDenomsByHand(int max)
        {
            //All multiples of just 2, 3, 5 are allowed for 80
            var allowed = PrimeDecompositionHighestPrime
                .Where(n => n.Value <= 5)
                .Select(n => n.Key)
                .ToList();
            //For 7, all are allowed except 49
            allowed.AddRange(
                PrimeDecompositionHighestPrime
                .Where(n => n.Value == 7 && n.Key != 49)
                .Select(n => n.Key));
            //For 13, 13, 39, 52 are allowed
            allowed.Add(13);
            allowed.Add(39);
            allowed.Add(52);
            allowed.Remove(49);
            allowed.Remove(25);
            allowed.Remove(27);
            return allowed
                .Where(n => n <= max)
                .ToArray();
        }

        /// <summary>
        /// Using allowedDenoms, get how much the sums of inverse squares adds up to, for each starting index
        /// </summary>
        public decimal[] GetMaxLeftsHigher(int[] allowedDenoms)
        {
            decimal[] lefts = new decimal[allowedDenoms.Length];
            for (int i = 0; i < allowedDenoms.Length; i++)
            {
                lefts[i] = CalculateMaxLeftHigher(i, allowedDenoms);
            }
            return lefts;
        }

        decimal CalculateMaxLeftHigher(int lowestIndex, int[] allowedDenoms)
        {
            var sum = 0.0m;
            for (int i = lowestIndex; i < allowedDenoms.Length; i++)
            {
                var denom = allowedDenoms[i];
                sum += (decimal)1 / (denom * denom);
            }
            return sum;
        }

        public Fraction[] GetMaxLeftsHigherFractions(int[] allowedDenoms)
        {
            Fraction[] lefts = new Fraction[allowedDenoms.Length];
            for (int i = 0; i < allowedDenoms.Length; i++)
            {
                lefts[i] = CalculateMaxLeftHigherFraction(i, allowedDenoms);
            }
            return lefts;
        }

        Fraction CalculateMaxLeftHigherFraction(int lowestIndex, int[] allowedDenoms)
        {
            var sum = new Fraction(0,1);
            for (int i = lowestIndex; i < allowedDenoms.Length; i++)
            {
                var denom = allowedDenoms[i];
                sum += new Fraction(1, denom * denom);
            }
            return sum.Reduce(primes);
        }
        #endregion

        public void PreAddSums(int nextLowest, Fraction runningValue, Stack<int> denomsUsed = null)
        {
            for (int i = nextLowest; i < AllowedDenoms.Length; i++)
            {
                denomsUsed.Push(AllowedDenoms[i]);
                var newFraction = new Fraction(1, AllowedDenoms[i] * AllowedDenoms[i]);
                runningValue.Add(newFraction);
                runningValue.Reduce(primes);

                //Add Or Update
                var denomList = new DenomListWithInverseSquareSum()
                {
                    Denoms = denomsUsed.ToList(),
                    Value = runningValue
                };
                if (PreAddedDenoms.ContainsKey(runningValue.Id))
                {
                    PreAddedDenoms[runningValue.Id].Add(denomList);
                }
                else
                {
                    PreAddedDenoms.Add(runningValue.Id, new List<DenomListWithInverseSquareSum>() { denomList });
                }

                if (i < (AllowedDenoms.Length - 1))
                {
                    PreAddSums(i + 1, runningValue, denomsUsed);
                }
                runningValue.Subtract(newFraction);
                denomsUsed.Pop();
            }
        }

        public List<List<int>> FilterUnique(List<List<int>> solutions)
        {
            Dictionary<string, bool> exist = new Dictionary<string, bool>();
            List<List<int>> uniques = new List<List<int>>();
            foreach (var item in solutions)
            {
                item.Sort();
                var stringItem = String.Join("_", item.Select(n => n.ToString()));
                if (!exist.ContainsKey(stringItem))
                {
                    exist[stringItem] = true;
                    uniques.Add(item);
                }
            }
            return uniques;
        }

        public static bool IsSolutionForReals(List<int> denoms)
        {
            denoms.Sort();
            for (int i = 0; i < denoms.Count - 1; i++)
            {
                if (denoms[i] == denoms[i + 1])
                    return false;
            }
            var (numerator, denominator) = FractionParts(denoms);
            return denominator == 2 * numerator;
        }

        public static (BigInteger, BigInteger) FractionParts(List<int> denoms)
        {
            BigInteger fullSum = Denominator(denoms);
            var numeratorSum = (BigInteger)0;
            for (int i = 0; i < denoms.Count; i++)
            {
                var toAdd = fullSum / (denoms[i] * denoms[i]);
                numeratorSum += toAdd;
            }
            return (numeratorSum, fullSum);
        }

        public static BigInteger Denominator(List<int> denoms)
        {
            var currentProduct = (BigInteger)1;
            for (int i = 0; i < denoms.Count; i++)
            {
                currentProduct *= (BigInteger)(denoms[i] * denoms[i]);
            }
            return currentProduct;
        }
    }
}
