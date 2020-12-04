using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using MathUtilCore;
using MathUtilCore.Helpers;

namespace Problems
{
    public static class Problem152Drafts
    {
        #region Properties
        public static Dictionary<int, int> PrimeDecompositionHighestPrime
            = new Dictionary<int, int>();
        static Dictionary<int, int> PrimeDecompositionLowestPrime
            = new Dictionary<int, int>();
        static Dictionary<int, List<int>> Powers = new Dictionary<int, List<int>>();
        static int[][] PrimeDecompositions;
        static int[] primes;
        static int[] AllowedDenoms;
        static decimal[] MaxLefts;
        static SortedList<string, List<int>> FoundFractions
            = new SortedList<string, List<int>>();
        #endregion

        public static int Ways(int max)
        {            
            var matches = 0;
            var emptyList = new List<int>();
            var bounds = BuildBounds(max);
            SubsetHelper.RunForPartitions(max, (combinationBinary) =>
            {
                var denoms = SubsetHelper.IndecesFromBinary(combinationBinary);
                if(ObeysBounds(denoms, bounds))
                {
                    denoms = denoms.Select(n => n + 1).ToList();
                    var (num, den) = FractionParts(denoms);
                    if (den == 2 * num)
                    {
                        matches++;
                    }
                }
            });
            return matches;
        }

        #region Naive

        /// <summary>
        /// Find how many ways 1/2 = 1/a1^2 + 1/a2^2 ... + 1/an^2, where a(i-1) < ai < m
        /// </summary>
        /// <strategy>
        /// After combining fractions and rearranging, you get:
        /// (a1*a2*a3...an)^2 = 2 * [ (a2*a3..an)^2 + (a1*a3...an)^2 + ... (a1*a2...a(n-1))^2]
        /// There is an upper bound for each ai,
        /// a1 <= sqrt(2m)
        /// a2 <= a1*SQRT[2*(n-(2-1))/(a1^2-a1)]
        /// ak <= a1*a2..ak-1 * SQRT[2*(n - (k-2))/((a1*a2...ak-1)^2 - 2*SUM(ai,aj,1,k))
        /// </strategy>
        /// <param name="max"></param>
        /// <returns></returns>
        /// 

        public static bool ObeysBounds(List<int> denoms, List<int> bounds)
        {
            for (int i = 0; i < denoms.Count; i++)
            {
                if (denoms[i] > bounds[i])
                    return false;
            }
            return true;
        }

        public static List<int> BuildBounds(int max)
        {
            var list = new List<int>();
            var bounds = new List<int>();
            for (int i = 1; i <= max; i++)
            {
                bounds.Add(UpperBound(list, i, max));
                list.Add((i + 1));
            }
            return bounds;
        }

        public static int UpperBound(List<int> previous, int k, int max)
        {
            var product = previous.Aggregate((BigInteger)1, (a, b) => a * b);
            var sums = previous.Aggregate((BigInteger)0, (a, b) => a + (product / b)*(product / b));
            var denominator = (product * product) - 2 * sums;
            var numerator = (BigInteger) 2 * (max - k + 1);
            var squared = (numerator * product * product / denominator);
            var bound = (int)Math.Floor(Math.Sqrt( (double)squared));
            
            return bound;
        }
        #endregion

        #region Utils

        public static (BigInteger,BigInteger) FractionParts(List<int> denoms)
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

        public static (double, double) FractionPartsDouble(List<int> denoms)
        {
            double fullSum = DenominatorDouble(denoms);
            var numeratorSum = (double)0;
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

        public static double DenominatorDouble(List<int> denoms)
        {
            var currentProduct = (double)1;
            for (int i = 0; i < denoms.Count; i++)
            {
                currentProduct *= (double)(denoms[i] * denoms[i]);
            }
            return currentProduct;
        }

        public static bool IsSolution(List<int> denoms)
        {
            var (numerator, denominator) = FractionParts(denoms);
            return denominator == 2 * numerator;
        }

        public static bool IsSolutionBySum(List<int> denoms)
        {
            decimal result = 0;
            for (int i = 0; i < denoms.Count; i++)
            {
                result += (decimal)1 / (denoms[i] * denoms[i]);
            }
            return EqualWIthError(result);
        }

        public static bool EqualWIthError(decimal result, decimal error = 0.00000000000000000001m)
        {
            var difference = 0.5m - result;
            return -1 * error < difference && difference < error;
        }
        #endregion

        #region Largest N

        //Finding largest number of fractions
        //If you have n fractions, and m is the largest denom, the largest fraction
        //(one with lowest denom), is at least 1/(m-n+1).
        //So the sum is at least Sum(1/(m-n+1)^2 + ... + 1/m^2)
        //This method calculates that sum given n and m
        public static double SumOfInverseSquareds(int start, int end)
        {
            var sum = 0.0d;
            for (int i = end - start; i <= end; i++)
            {
                float fraction = (float)1 / (i * i);
                sum += fraction;
            }
            return sum;
        }

        public static int LargestN(int max)
        {
            for (int i = 4; i < max; i++)
            {
                if (SumOfInverseSquareds(i, max) > (float)1 / 2)
                    return i;
            }
            return 80;
        }
        #endregion

        #region Numbers Not Allowed

        public static List<int> GetNumbersNotAllowed(int max)
        {
            var primes = Util.getPrimes(max);
            //For each prime p, get n = max/primes rounded down
            //Then find all sums a1^2+a2^2...ax^2, where ai <=n
            //If p does not divide any combination, it's not allowed
            var notAllowed = new List<int>();
            foreach(var prime in primes)
            {
                int timesFit = max / prime;
                var allowed = false;
                SubsetHelper.RunForPartitions(timesFit, (cb) =>
                {
                    var denoms = SubsetHelper.IndecesFromBinary(cb);
                    var sum = denoms.Aggregate((BigInteger)0, (a, b) => a + b * b);
                    if(sum > 0 && sum%prime == 0)
                    {
                        allowed = true;
                        return false;
                    }
                    return true;
                });
                if (!allowed)
                {
                    var multiple = 1;
                    while(prime*multiple < max)
                        notAllowed.Add(prime*multiple++);
                    
                }
            }
            return notAllowed;
        }
        #endregion

        #region using Trees

        public static int[] GetAllowedDenoms(int max)
        {
            var notAllowed = GetNumbersNotAllowed(max);
            var allowed = new List<int>();
            for (int i = 2; i < max; i++)
            {
                if (!notAllowed.Contains(i))
                    allowed.Add(i);
            }
            return allowed.ToArray();
        }

        public static decimal[] GetMaxLeftsHigher()
        {
            decimal[] lefts = new decimal[AllowedDenoms.Length];
            for (int i = 0; i < AllowedDenoms.Length; i++)
            {
                lefts[i] = CalculateMaxLeftHigher(i, AllowedDenoms);
            }
            return lefts;
        }

        public class Leaf
        {
            public List<int> myDenomIndexes = new List<int>();
            public double maxLeft = 0;
            public Leaf Parent;
            public decimal value = 0;
            public int denom;
        }

        public static int SolveWithTrees(int max)
        {
            AllowedDenoms = GetAllowedDenoms(max);
            MaxLefts = GetMaxLeftsHigher();
            Leaf head = new Leaf()
            {
                value = 0
            };
            return FindSolutions(head, 0, 0, max);
        }

        public static int FindSolutions(Leaf headLeaf, int nextLowest, int level, int max)
        {
            var solutions = 0;
            for (int i = nextLowest; i < AllowedDenoms.Length; i++)
            {
                var newLeaf = new Leaf()
                {
                    value = headLeaf.value + (decimal)1 / (AllowedDenoms[i] * AllowedDenoms[i]),
                    denom = AllowedDenoms[i],
                    Parent = headLeaf
                };
                if (EqualWIthError(newLeaf.value))
                {
                    solutions++;
                }
                if(newLeaf.value + MaxLefts[i] >= 0.5m && newLeaf.value < 0.5m && level < max)
                {
                    solutions += FindSolutions(newLeaf, i + 1, level + 1, max);
                }
            }
            return solutions;
        }

        static List<int> GetDenoms(Leaf leaf)
        {
            List<int> denoms = new List<int>();
            while(leaf.denom != 0)
            {
                denoms.Add(leaf.denom);
                leaf = leaf.Parent;
            }
            return denoms;
        }

        static decimal CalculateMaxLeftHigher(int lowestIndex, int[] AllowedDenoms)
        {
            var sum = 0.0m;
            for (int i = lowestIndex; i < AllowedDenoms.Length; i++)
            {
                var denom = AllowedDenoms[i];
                sum += (decimal)1 / (denom * denom);
            }
            return sum;
        }
        #endregion

        #region PrimeDecompositions
        
        static void GetPrimeDecompositionHighestPrime(int max, int[] primes)
        {
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
        }

        static void GetPrimeDecompositionLowestPrime(int max, int[] primes)
        {
            for (int i = 2; i <= max; i++)
            {
                for (int j = 0; j < primes.Length && i >= primes[j]; j++)
                {
                    if (i % primes[j] == 0)
                    {
                        PrimeDecompositionLowestPrime[i] = primes[j];
                        break;
                    }
                }
            }
        }

        public static int[] GetAllowedDenomsByHand(int max)
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
            allowed.Remove(25);
            allowed.Remove(27);
            allowed.Remove(2);
            return allowed
                .Where(n => n <= max)
                .ToArray();
        }
        #endregion

        #region Denominator Combination

        public static void SetupsForCombinationSolutions(int max)
        {
            primes = Util.getPrimes(max);
            //BuildPowersArray(primes, max);
            GetPrimeDecompositionHighestPrime(max, primes);
            GetPrimeDecompositionLowestPrime(max, primes);
            MaxLefts = GetMaxLeftsAll(max);
            AllowedDenoms = GetAllowedDenomsByHand(max); 
        }

        private static void BuildPowersArray(int[] primes, int max)
        {
            foreach (var prime in primes)
            {
                List<int> primePowers = new List<int>();
                var current = prime;
                while (current < max)
                {
                    primePowers.Add(current);
                    current *= prime;
                }
                Powers[prime] = primePowers;
            }
        }

        private static bool ListInList(List<List<int>> solutions, List<int> newOne)
        {
            foreach (var existing in solutions)
            {
                if (existing.Count == newOne.Count)
                {
                    var same = true;
                    for (int i = 0; i < newOne.Count; i++)
                    {
                        if (existing[i] != newOne[i])
                        {
                            same = false;
                            break;
                        }
                    }
                    if (same)
                        return true;

                }
            }
            return false;
        }

        public static bool[] GetAllowedForMultiple(int prime, int max)
        {
            bool[] allowed = new bool[max+1];
            //values from 1 to max/prime
            var numberOfMultiples = (max / prime);
            //Get combinations of denominators
            var partitions = SubsetHelper.GetBinaryValuesForPartitions(numberOfMultiples);
            //for each combination
            List<string> added = new List<string>();
            var prime2 = prime * prime;
            foreach (var partition in partitions)
            {
                var denoms = SubsetHelper.IndecesFromBinary(partition)
                    .Select(n => n + 1).ToList();
                //Get sum of n-1 squared products 
                var sum = GetSquaredProductSum(denoms);
                //Filter combinations not divisible by prime
                if (sum % prime2 == 0)
                {
                    denoms
                        .Select(n => n * prime)
                        .ToList()
                        .ForEach(n => allowed[n] = true);
                }
            }
            return allowed;
        }

        public static List<List<int>> GetValidCombinations(int prime, int max)
        {
            var validDictionary = new Dictionary<string, List<int>>();
            //values from 1 to max/prime
            var numberOfMultiples = (max / prime);
            //Get combinations of denominators
            var partitions = SubsetHelper.GetBinaryValuesForPartitions(numberOfMultiples);
            //for each combination
            List<string> added = new List<string>();
            var prime2 = prime * prime;
            foreach (var partition in partitions)
            {
                var denoms = SubsetHelper.IndecesFromBinary(partition)
                    .Select(n => n + 1).ToList();
                //Get sum of n-1 squared products 
                var sum = GetSquaredProductSum(denoms);
                //Filter combinations not divisible by prime
                if (sum % prime2 == 0)
                {
                    var combo = denoms
                        .Where(n => FilterDecompositionLowerPrimes(n, prime))
                        .Select(n => n * prime)
                        .ToList();
                    var id = String.Concat(combo.Select(n => n.ToString()));
                    validDictionary[id] = combo;
                }
            }
            return validDictionary.ToList()
                .Select(n => n.Value).ToList();
        }

        public static List<List<int>> GetValid2Combinations(int max)
        {
            //values from 1 to max/prime
            var numberOfMultiples = (int)Math.Log(max, 2);
            //We know 1/2^2 needs to be used since otherwise it would be too small,
            //So just get how many ways you can use the *other* powers of 2, and append 1/2^2
            var partitions = SubsetHelper.GetBinaryValuesForPartitions(numberOfMultiples - 1);
            var combinations = new List<List<int>>();
            foreach (var partition in partitions)
            {
                List<int> denomsFromPartition = GetPowerDenomsFromPartitionForPrime(2, partition);
                //Double the denominators since we're manually going to add 2 at the front
                denomsFromPartition = denomsFromPartition.Select(n => n * 2)
                    .ToList();
                denomsFromPartition.Insert(0, 2);
                combinations.Add(denomsFromPartition);
            }
            return combinations;
        }

        public static List<List<int>> GetValid23Combinations(int max)
        {
            List<List<int>> combos = new List<List<int>>();
            var multiplesOf2Or3 = PrimeDecompositionHighestPrime.Where(n => n.Value <= 3)
                .Select(n => n.Key)
                .ToList();
            //subract 1 because we're know 2 has to be a denom, so we'll just build the combinations without, and add it at the end
            var partitions = SubsetHelper.GetBinaryValuesForPartitions(multiplesOf2Or3.Count - 1);
            foreach (var partition in partitions)
            {
                var denoms = SubsetHelper.IndecesFromBinary(partition)
                    .Select(n => multiplesOf2Or3[n+1]).ToList();
                denoms.Add(2);
                combos.Add(denoms);
            }
            return combos;
        }

        public static List<List<int>> GetValid235Combinations(int max)
        {
            List<List<int>> combos = new List<List<int>>();
            var multiplesOf2Or3 = PrimeDecompositionHighestPrime.Where(n => n.Value <= 5)
                .Select(n => n.Key)
                .ToList();
            //subract 1 because we're know 2 has to be a denom, so we'll just build the combinations without, and add it at the end
            var partitions = SubsetHelper.GetBinaryValuesForPartitions(multiplesOf2Or3.Count - 1);
            foreach (var partition in partitions)
            {
                var denoms = SubsetHelper.IndecesFromBinary(partition)
                    .Select(n => multiplesOf2Or3[n + 1]).ToList();
                denoms.Add(2);
                combos.Add(denoms);
            }
            return combos;
        }

        public static List<List<int>> GetValidHigherThanPrimeCombinations(int prime)
        {
            List<List<int>> combos = new List<List<int>>();
            var multiplesOf2Or3 = PrimeDecompositionHighestPrime
                .Where(n => n.Value > prime)
                .Select(n => n.Key)
                .Intersect(AllowedDenoms)
                .ToList();
            var partitions = SubsetHelper.GetBinaryValuesForPartitions(multiplesOf2Or3.Count);
            foreach (var partition in partitions)
            {
                var denoms = SubsetHelper.IndecesFromBinary(partition)
                    .Select(n => multiplesOf2Or3[n]).ToList();
                combos.Add(denoms);
            }
            return combos;
        }

        private static List<int> GetPowerDenomsFromPartitionForPrime(int prime, int partition)
        {
            var denoms = new List<int>();
            denoms.AddRange(
                SubsetHelper.IndecesFromBinary(partition)
                .Select(n => (int)Math.Pow(prime, n + 1))
                .ToList()
            );

            return denoms;
        }
        #endregion

        #region Save Partial Fractions

        public static List<List<int>> GetSolutionsUsingCombinationsAndPartialFractions(int max)
        {
            SetupsForCombinationSolutions(max);
            var combos = GetPartialFractionCombinations(max);
            var solutions = new List<List<int>>();
            for (int i = 0; i < combos[0].Count; i++)
            {
                var rightSideCombo = IsSolutionUsingPartialFractions(combos[0][i]);
                if (rightSideCombo != null && rightSideCombo.Count > 0)
                {
                    solutions.Add(Combine(combos[0][i], rightSideCombo));
                }
            }
            return solutions;
        }

        public static List<List<List<int>>> GetPartialFractionCombinations(int max)
        {
            var combos23 = GetFullValidPrimeCombinations(3);
            var combosHigherThan3 = GetPartialValidHigherThanPrimeCombinations(3);
            SaveFractions(combosHigherThan3);
            return new List<List<List<int>>>()
            {
                combos23
            };
        }

        public static List<List<int>> GetFullValidPrimeCombinations(int prime)
        {
            List<List<int>> combos = new List<List<int>>();
            var multiples = PrimeDecompositionLowestPrime.Where(n => n.Value <= prime)
                .Select(n => n.Key)
                .ToList();
            //subract 1 because we're know 2 has to be a denom, so we'll just build the combinations without, and add it at the end
            var partitions = SubsetHelper.GetBinaryValuesForPartitions(multiples.Count - 1);
            foreach (var partition in partitions)
            {
                var denoms = SubsetHelper.IndecesFromBinary(partition)
                    .Select(n => multiples[n + 1]).ToList();
                denoms.Add(2);
                combos.Add(denoms);
            }
            return combos;
        }

        public static List<List<int>> GetPartialValidHigherThanPrimeCombinations(int prime)
        {
            List<List<int>> combos = new List<List<int>>();
            var multiples = PrimeDecompositionLowestPrime
                .Where(n => n.Value > prime)
                .Select(n => n.Key)
                .Intersect(AllowedDenoms)
                .ToList();
            var partitions = SubsetHelper.GetBinaryValuesForPartitions(multiples.Count);
            foreach (var partition in partitions)
            {
                var denoms = SubsetHelper.IndecesFromBinary(partition)
                    .Select(n => multiples[n]).ToList();
                combos.Add(denoms);
            }
            return combos;
        }

        static List<int> IsSolutionUsingPartialFractions(List<int> denoms)
        {
            var parts = FractionParts(denoms);
            var reduced = ReduceFractionParts(parts, 2, 3);
            var opposite = GetReducedOppositeFractionParts(reduced, 2, 3, 5, 7, 13);
            var value = FoundFractions.TryGetValue(GetStringIdFromFractionParts(opposite), out List<int> combo);
            return combo;
        }

        public static (int,int) GetFoundFractionIndecesFromParts((BigInteger, BigInteger) parts)
        {
            return (Int32.Parse(parts.Item1.ToString()),
                    Int32.Parse(parts.Item1.ToString()));
        }

        static void SaveFractions(List<List<int>> combinations)
        {
            for (int i = 0; i < combinations.Count; i++)
            {
                var parts = FractionParts(combinations[i]);
                var reducedParts = ReduceFractionParts(parts, 2, 3, 5, 7, 13);
                FoundFractions[GetStringIdFromFractionParts(reducedParts)] = combinations[i];
            }
        }

        static (BigInteger, BigInteger) ReduceFractionParts((BigInteger, BigInteger) parts, params int[] primes)
        {
            for (int i = 0; i < primes.Length; i++)
            {
                while (parts.Item1 % primes[i] == 0 && parts.Item2 % primes[i] == 0)
                {
                    parts.Item1 /= primes[i];
                    parts.Item2 /= primes[i];
                }
            }
            return parts;
        }

        static (BigInteger, BigInteger) GetReducedOppositeFractionParts((BigInteger, BigInteger) rightTerm, params int[] primes)
        {
            // a/b + rt.1/rt.2 = 1/2 => a/b= (rt.2-2*rt.1)/2*rt.2
            var newNumerator = rightTerm.Item2 - 2 * rightTerm.Item1;
            var newDenominator = rightTerm.Item2 * 2;
            var reduced = ReduceFractionParts((newNumerator, newDenominator), primes);
            return reduced;
        }

        static string GetStringIdFromFractionParts((BigInteger,BigInteger) parts)
        {
            return parts.Item1.ToString() + "_" + parts.Item2.ToString();
        }
        #endregion

        #region Get Solutions Using Valid Combinations

        static List<List<int>> RunCombinations(Func<List<int>, bool> solutionChecker, List<List<List<int>>> combos)
        {
            var solutions = new List<List<int>>();
            solutions = CombineCombos(new List<int>(), combos, 0, solutionChecker);
            return solutions;
        }

        static List<List<int>> CombineCombos(List<int> previous, List<List<List<int>>> combos, int index,
            Func<List<int>, bool> solutionChecker)
        {
            var solutions = new List<List<int>>();
            for (int i = 0; i < combos[index].Count; i++)
            {
                var combined = Combine(previous, combos[index][i]);
                if (index == combos.Count - 1)
                {
                    //Last one, get solutions
                    if (combined.Count > 0)
                    {
                        if (solutionChecker(combined))
                        {
                            solutions.Add(combined);
                        }
                    }
                }
                else
                {
                    //Not last one, keep combining
                    solutions.AddRange(CombineCombos(combined, combos, index + 1, solutionChecker));
                }
            }
            return solutions;
        }

        public static List<List<int>> GetSolutionsUsingCombinations(int max)
        {
            SetupsForCombinationSolutions(max);
            var combos = GetCombinations(max);
            return RunCombinations(IsSolution, combos);
        }

        public static List<List<List<int>>> GetCombinations(int max)
        {
            var combos23 = GetValid23Combinations(max);
            var combosHigherThan3 = GetValidHigherThanPrimeCombinations(3);
            /*var combos5 = GetValidCombinations(5, max);
            var combos7 = GetValidCombinations(7, max);
            var combos13 = GetValidCombinations(13, max);*/

            return new List<List<List<int>>>()
            {
                combos23,combosHigherThan3
            };
        }

        public static decimal[] GetMaxLeftsAll(int max)
        {
            decimal[] lefts = new decimal[max];
            for (int i = 0; i < max; i++)
            {
                lefts[i] = CalculateMaxLeftAll(i+1, max);
            }
            return lefts;
        }

        public static decimal CalculateMaxLeftAll(int lowest, int max)
        {
            var sum = 0.0m;
            for (int denom = lowest; denom <= max; denom++)
            {
                sum += (decimal)1 / (denom * denom);
            }
            return sum;
        }

        static List<int> Combine(params List<int>[] combos)
        {
            var combined = new List<int>();
            foreach(var combo in combos)
            {
                combined = combined.Concat(combo)
                    .ToList();
            }
            return combined;
        }

        #region Range
        static bool ContainsAllInRange(List<int> list, params int[] elements)
        {
            foreach(var element in elements)
            {
                if(list.IndexOf(element) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        static bool MissingAllInRange(List<int> list, params int[] elements)
        {
            foreach (var element in elements)
            {
                if (list.IndexOf(element) != -1)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool WithinBounds(List<int> denoms)
        {
            if(denoms.Count > 0)
            {
                var last = denoms.Last();
                double current = denoms.Aggregate((double)0, (a, b) =>
                {
                    return a + (double)1 / (b * b);
                });
                return (current <= 0.5d);
            }
            else
            {
                return true;
            }
        }
        #endregion

        private static bool FilterDecompositionLowerPrimes(int n, int prime)
        {
            return PrimeDecompositionHighestPrime[n*prime] <= prime;
        }

        private static bool FilterDecompositionHigherPrimes(int n, int prime)
        {
            return PrimeDecompositionLowestPrime[n * prime] >= prime;
        }

        private static List<int> GetMultiples(int prime, int max)
        {
            var multiples = new List<int>();
            for (int i = 1; i <= max/prime; i++)
            {
                multiples.Add(prime * i);
            }
            return multiples;
        }

        private static BigInteger GetOneSkippedSquaredProduct(List<int> denoms, int toSkip)
        {
            BigInteger Product = 1;
            foreach(var value in denoms)
            {
                if(value != toSkip)
                {
                    Product *= value * value;
                }
            }
            return Product;
        }

        private static BigInteger GetSquaredProductSum(List<int> denoms)
        {
            BigInteger sum = 0;
            foreach(var toSkip in denoms)
            {
                var product = GetOneSkippedSquaredProduct(denoms, toSkip);
                sum += product;
            }
            return sum;
        }
        #endregion

        #region Get Solutions Using Valid Combinations Fractions
        public static List<BigInteger> GetValidCombinationsFractions(int prime, int max)
        {
            var validCombinations = new List<BigInteger>();
            //Get valid denominators for prime
            var multiples = GetMultiples(prime, max);
            //Get combinations of denominators
            var partitions = SubsetHelper.GetBinaryValuesForPartitions(multiples.Count);
            //for each combination
            HashSet<string> added = new HashSet<string>();
            foreach (var partition in partitions)
            {
                var denoms = SubsetHelper.IndecesFromBinary(partition)
                    .Select(n => n + 1).ToList();
                //Get sum of n-1 squared products 
                var sum = GetSquaredProductSum(denoms);
                //Filter combinations not divisible by prime
                if ((prime == 2 && sum % 2 == 0) || (prime != 2 && sum % (prime * prime) == 0))
                {
                    var combo = denoms
                        .Where(n => FilterDecompositionLowerPrimes(n, prime))
                        .Select(n => n * prime)
                        .ToList();

                    var (numerator, denominator) = FractionParts(combo);
                    var result = (numerator / denominator);
                    //var id = result.ToString();
                    //if (!added.Contains(id))
                    //{
                        validCombinations.Add(result);
                        //added.Add(id);
                    //}
                }
            }
            return validCombinations;
        }

        public static List<List<BigInteger>> GetCombinationsFractions(int max)
        {
            var combos2 = GetValidCombinationsFractions(2, max);
            var combos3 = GetValidCombinationsFractions(3, max);
            var combos5 = GetValidCombinationsFractions(5, max);
            var combos7 = GetValidCombinationsFractions(7, max);

            return new List<List<BigInteger>>()
            {
                combos2,combos3,combos5, combos7
            };
        }

        public static int GetSolutionsUsingCombinationsFractions(int max)
        {
            SetupsForCombinationSolutions(max);
            var combos = GetCombinationsFractions(max);
            var combos2 = combos[0];
            var combos3 = combos[1];
            var combos5 = combos[2];
            var combos7 = combos[3];
            var solutionCount = 0;

            for (int i2 = 0; i2 < combos2.Count; i2++)
            {
                for (int i3 = 0; i3 < combos3.Count; i3++)
                {
                    for (int i5 = 0; i5 < combos5.Count; i5++)
                    {
                        for (int i7 = 0; i7 < combos7.Count; i7++)
                        {
                            var combined = CombineFractions(combos2[i2], combos3[i3], combos5[i5], combos7[i7]);
                            if (combined.Equals(0.5m))
                            {
                                solutionCount++;
                            }
                        }
                    }
                }
            }

            return solutionCount;
        }

        private static BigInteger CombineFractions(params BigInteger[] integers)
        {
            return integers.Aggregate((BigInteger)0, (a, b) => a + b);
        }
        #endregion

        #region Get Solutions Using Combinations And Prime Decompositions

        public static List<List<int>> GetSolutionsUsingCombinationsAndPrimeDecompositions(int max)
        {
            SetupsForCombinationSolutions(max);
            var combos = GetCombinations(max);
            PrimeDecompositions = Util.GetAllPrimeDecompositions(max, primes);
            return RunCombinations(IsSolutionWithFractions, combos);
        }

        public static bool IsSolutionWithFractions(List<int> denoms)
        {
            var decomps = denoms.Select(n => PrimeDecompositions[n].Select(m => m).ToArray())
                .ToList();
            var (numerator, denominator) = GetFractionSumUsingPrimeDecomposition(decomps, primes);
            return numerator == 1 && Util.GetValueFromDecomposition(denominator, primes) == 2;
        }

        public static (double, int[]) GetFractionSumUsingPrimeDecomposition(List<int[]> denominatorDecompositions,
            int[] primes)
        {
            double currentNumerator = 1;
            var currentDenominator = Util.SquareDecomp(denominatorDecompositions[0]);
            for (int i = 1; i < denominatorDecompositions.Count; i++)
            {
                (currentNumerator, currentDenominator) =
                    AddFractionToCurrentFractionSum(currentNumerator, currentDenominator,
                    Util.SquareDecomp(denominatorDecompositions[i]), primes);
            }
            return (currentNumerator, currentDenominator);
        }

        public static (double, int[]) AddFractionToCurrentFractionSum(double numerator1, int[] denom1, int[] denom2, int[] primes)
        {
            var denum2Value = Util.GetValueFromDecomposition(denom2, primes);
            if (denum2Value == 1)
            {
                return (numerator1, denom1);
            }
            //Get combined denominator
            var newDenominator = Util.MultiplyDecompositions(denom1, denom2);
            //Get numerator
            var newNumerator = numerator1 * denum2Value;
            newNumerator += Util.GetValueFromDecomposition(denom1, primes);
            //Reduce Fraction
            return Util.ReduceFraction(newNumerator, newDenominator, primes);
        }
        #endregion

        #region Get Solutions Using Combinations and straight sum
        public static List<List<int>> GetSolutionsUsingCombinationsAndStratightSum(int max)
        {
            SetupsForCombinationSolutions(max);
            var combos = GetCombinations(max);
            return RunCombinations(IsSolutionBySum, combos);
        }
        #endregion

        #region Save Partial Fractions With Trees
        public class DenomsWithValues
        {
            public List<int> Denoms = new List<int>();
            public decimal Value { get; set; }
        }

        static List<decimal> PreAdded = new List<decimal>();
        static List<DenomsWithValues> PreAddedDenoms = new List<DenomsWithValues>();
        public static bool IsSolutionWithinErrors(List<decimal> preAdded, decimal toCheck, decimal error = .00001m)
        {
            var diff = Math.Abs(0.5m - toCheck);
            var index = GetLowerIndex(preAdded, diff, 0, preAdded.Count - 1);
            diff = 0.5m - (preAdded[index] + toCheck);
            return -1 * error < diff && diff < error;
        }

        public static (bool, List<int>) IsSolutionWithinErrors(List<DenomsWithValues> preAdded, decimal toCheck, decimal error = .0000000000000000001m)
        {
            var diff = Math.Abs(0.25m - toCheck);
            var index = GetLowerIndex(preAdded, diff, 0, preAdded.Count - 1);
            diff = 0.25m - (preAdded[index].Value + toCheck);
            return (-1 * error < diff && diff < error, preAdded[index].Denoms);
        }

        public static int GetLowerIndex(List<DenomsWithValues> preAdded, decimal toCheck, int low, int high)
        {
            if(high <= low + 1)
            {
                return toCheck >= preAdded[high].Value ? high : low;
            } 
            else
            {
                int halfway = low + (high - low) / 2;
                if(preAdded[halfway].Value > toCheck)
                {
                    return GetLowerIndex(preAdded, toCheck, low, halfway);
                }
                else
                {
                    return GetLowerIndex(preAdded, toCheck, halfway, high);
                }
            }            
        }

        public static int GetLowerIndex(List<decimal> preAdded, decimal toCheck, int low, int high)
        {
            if (high <= low + 1)
            {
                return toCheck >= preAdded[high] ? high : low;
            }
            else
            {
                int halfway = low + (high - low) / 2;
                if (preAdded[halfway] > toCheck)
                {
                    return GetLowerIndex(preAdded, toCheck, low, halfway);
                }
                else
                {
                    return GetLowerIndex(preAdded, toCheck, halfway, high);
                }
            }
        }

        public static void PreAddSums(int nextLowest, Stack<int> denomsUsed = null, decimal runningValue = 0)
        {
            for (int i = nextLowest; i < AllowedDenoms.Length; i++)
            {
                denomsUsed.Push(AllowedDenoms[i]);
                var newFraction = (decimal)1 / (AllowedDenoms[i] * AllowedDenoms[i]);
                runningValue += newFraction;
                //PreAdded.Add(runningValue);
                PreAddedDenoms.Add(new DenomsWithValues()
                {
                    Denoms = denomsUsed.ToList(),
                    Value = runningValue
                });
                if(i < (AllowedDenoms.Length - 1))
                {
                    PreAddSums(i+1, denomsUsed, runningValue);
                }
                runningValue -= newFraction;
                denomsUsed.Pop();
            }
        }

        public static int TryAllErrors(int max, int split)
        {
            int maxFound = 0;
            decimal error = 0.1m;
            for (int i = 0; i < 100; i++)
            {
                var found = GetSolutionsWithPartialFractionsAndTrees(max, split);
                if (found.Count > maxFound)
                    maxFound = found.Count;
                error = error / 10;
            }
            return maxFound;
        }

        public static (Stack<int>,int) Setup(int max, int split)
        {
            primes = Util.getPrimes(max);
            GetPrimeDecompositionHighestPrime(max, primes);
            var denoms = GetAllowedDenomsByHand(max).ToList();
            denoms.Sort();
            AllowedDenoms = denoms.ToArray();
            MaxLefts = GetMaxLeftsHigher();
            var stack = new Stack<int>();
            var splitIndex = denoms.IndexOf(split);
            PreAddSums(splitIndex, new Stack<int>(), 0);
            PreAddedDenoms = PreAddedDenoms.OrderBy(n => n.Value)
                .ToList();
            return (stack, splitIndex);
        }

        public static List<List<int>> GetSolutionsWithPartialFractionsAndTrees(int max, int split, decimal error = 0.0000000000000000001m)
        {
            var (stack, splitIndex) = Setup(max, split);
            var closeSolutions = FindSolutionsWithStackAndPreadded(stack, (decimal)0, 1, splitIndex, error);
            var realSolutions = closeSolutions
                .Where(n => IsSolution(n))
                .ToList();
            return realSolutions;
        }

        public static List<List<int>> FindSolutionsWithStackAndPreadded(Stack<int> denomsUsed, decimal value, int nextLowest, int max, decimal error)
        {
            var solutions = new List<List<int>>();
            for (int i = nextLowest; i < max; i++)
            {
                denomsUsed.Push(AllowedDenoms[i]);
                var newFraction = (decimal)1 / (AllowedDenoms[i] * AllowedDenoms[i]);
                value += newFraction;
                var solutionResult = IsSolutionWithinErrors(PreAddedDenoms, value, error);
                if (solutionResult.Item1)
                {
                    var solution = denomsUsed.ToList();
                    solution.AddRange(solutionResult.Item2);
                    solutions.Add(solution);
                }

                if (value + (decimal)MaxLefts[i] >= 0.25m)// && level < max)
                {
                    if (value < 0.25m)
                    {
                        solutions.AddRange(FindSolutionsWithStackAndPreadded(denomsUsed, value, i + 1, max, error));
                    }
                }
                else //Since the value so far, plus the sum of all possible later values is less that 1/2
                        //we dont need to check subsequent combinations, since they will be smaller;
                {
                    denomsUsed.Pop();
                    break;
                }

                value -= newFraction;
                denomsUsed.Pop();
            }
            return solutions;
        }

        public static List<Stack<int>> FindSolutionsWithStack(Stack<int> denomsUsed, decimal value, int nextLowest, int level, int max)
        {
            var solutions = new List<Stack<int>>();
            for (int i = nextLowest; i < AllowedDenoms.Length; i++)
            {
                denomsUsed.Push(AllowedDenoms[i]);
                var newFraction = (decimal)1 / (AllowedDenoms[i] * AllowedDenoms[i]);
                value += newFraction;                
                if (EqualWIthError(value))
                {
                    solutions.Add(denomsUsed);
                }
                else
                {
                    if (value + (decimal)MaxLefts[i] >= 0.5m)// && level < max)
                    {
                        if (value < 0.5m)
                        {
                            solutions.AddRange(FindSolutionsWithStack(denomsUsed, value, i + 1, level + 1, max));
                        }
                    }
                    else //Since the value so far, plus the sum of all possible later values is less that 1/2
                         //we dont need to check subsequent combinations, since they will be smaller;
                    {
                        denomsUsed.Pop();
                        break;
                    }
                }
                value -= newFraction;
                denomsUsed.Pop();
            }
            return solutions;
        }
        #endregion

    }
}
