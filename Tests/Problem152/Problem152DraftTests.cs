using System;
using Xunit;
using Problems;
using System.Collections.Generic;
using System.Linq;
using MathUtilCore.Helpers;

namespace Tests
{
    public class Problem152DraftTests
    {
        #region BinaryCombinationTests
        [Fact]
        public void Problem152_Test()
        {
            Assert.Equal(0, Problem152Drafts.Ways(23));
        }

        [Fact]
        public void Numerator_Test()
        {
            var list = new List<int>()
            {
                2,3,4,5
            };

            Assert.Equal(4 * 9 * 25 + 4 * 16 * 25 + 9 * 16 * 25 + 4 * 9 * 16, Problem152Drafts.FractionParts(list).Item1);
        }

        [Fact]
        public void Denominator_Test()
        {
            var list = new List<int>()
            {
                2,3,4
            };

            Assert.Equal(4*9*16, Problem152Drafts.Denominator(list));
        }

        [Fact]
        public void Denominator_Test_Long()
        {
            var list = new List<int>()
            {
                2,4,6,8,10,12,14,16,18
            };

            Assert.NotEqual(0, Problem152Drafts.Denominator(list));
        }

        [Fact]
        public void Bound_Test()
        {
            var list = new List<int>()
            {
                2,3,4
            };

            Assert.Equal(9, Problem152Drafts.UpperBound(new List<int>(), 1, 45));
        }

        [Fact]
        public void Bound_Test_A2()
        {
            var list = new List<int>()
            {
                2
            };

            Assert.Equal(13, Problem152Drafts.UpperBound(list, 2, 45));
        }

        [Fact]
        public void Bound_Test_A3()
        {
            var list = new List<int>()
            {
                2,3,4,5,6,7
            };

            Assert.Equal(13, Problem152Drafts.UpperBound(list, 7, 45));
        }

        [Fact]
        public void BuildBounds_Test()
        {
            var bounds = Problem152Drafts.BuildBounds(45);

            Assert.Equal(9, bounds[0]);
            Assert.Equal(13, bounds[1]);
        }
        #endregion

        #region BoundsTests

        [Fact]
        public void GetNBound80()
        {
            Assert.Equal(78, Problem152Drafts.LargestN(80));
        }

        [Fact]
        public void GetNBound45()
        {
            Assert.Equal(43, Problem152Drafts.LargestN(45));
        }

        [Fact]
        public void GetNBound78()
        {
            var x = Problem152Drafts.SumOfInverseSquareds(78, 80);
            Assert.True(0.5 < x);
        }

        [Fact]
        public void GetNBound77()
        {
            var x = Problem152Drafts.SumOfInverseSquareds(77, 80);
            Assert.True(0.5 > x);
        }

        #endregion

        #region 
        [Fact]
        public void TreesTests45()
        {
            Assert.Equal(0, Problem152Drafts.SolveWithTrees(13));
        }
        #endregion

        #region Not ALlowed
        [Fact]
        public void NotAllowed45()
        {
            var notAllowed = Problem152Drafts.GetNumbersNotAllowed(45);
            Assert.Equal(17, notAllowed.Count);
        }

        [Theory]
        //[InlineData(45,3)]
        //[InlineData(45, 5)]
        //[InlineData(45, 7)]
        //[InlineData(80, 3)]
        //[InlineData(80, 5)]
        //[InlineData(80, 7)]
        [InlineData(80, 13)]
        public void NotAllowedMultiples(int max, int prime)
        {
            var allowed = Problem152Drafts.GetAllowedForMultiple(prime, max);

            var allowedCount = allowed.ToList().Count(n => n == true);

            for (int i = 1; i < max/prime; i++)
            {
                Assert.True(allowed[i*prime], i.ToString());
            }
        }
        #endregion

        #region ValidCombinationTests
        [Theory]
        /*[InlineData(23, 45, 1)]
        [InlineData(17, 45, 1)]
        [InlineData(7, 45, 10)]
        [InlineData(5, 45, 70)]
        [InlineData(3, 45, 26965)]
        [InlineData(2, 45, 4170752)]*/
        [InlineData(13,80,2)]
        public void GetValidCombinations(int prime, int max, int expected)
        {
            Problem152Drafts.SetupsForCombinationSolutions(max);
            var validCombos = Problem152Drafts.GetValidCombinations(prime, max);

            Assert.Equal(expected, validCombos.Count);
        }

        [Theory]
        //[InlineData(10,0)]
        //[InlineData(35, 0, 32)]
        //[InlineData(40, 1)]
        //[InlineData(44, 1)]
        //[InlineData(45,3)] // 1 min
        //[InlineData(50,3)] //12 min
        [InlineData(80,1,100000)]
        public void GetCombinationsTests(int max, int comboIndex, int expected)
        {
            Problem152Drafts.SetupsForCombinationSolutions(max);

            var combos = Problem152Drafts.GetCombinations(max);

            var comboX = combos[comboIndex];
            Assert.Equal(expected, comboX.Count);
        }

        [Theory]
        //[InlineData(4, 0)]
        //[InlineData(10,0)]
        //[InlineData(35,1)]
        //[InlineData(40, 1)]
        //[InlineData(44, 1)] // 36 secs
        //[InlineData(45,3)] // 1 secs
        //[InlineData(50, 3)] // 22 secs
        [InlineData(55,8)] // 1 min
        //[InlineData(60, 8)] //
        //[InlineData(80, 8)] //
        public void GetSolutionsUsingCombinations(int max, int expected)
        {
            var solutions = Problem152Drafts.GetSolutionsUsingCombinations(max);

            Assert.Equal(expected, solutions.Count);
        }

        [Theory]
        [InlineData(35, 32)]
        public void GetValid2Combinations(int max, int expected)
        {
            var combinations = Problem152Drafts.GetValid2Combinations(max);

            Assert.Equal(expected, combinations.Count);
        }

        [Theory]
        [InlineData(80, 11)]
        [InlineData(80, 13)]
        [InlineData(80, 17)]
        [InlineData(80, 19)]
        [InlineData(80, 23)]
        [InlineData(80, 29)]
        [InlineData(80, 31)]
        [InlineData(80, 37)]
        public void GetValidCombination(int max, int prime)
        {
            Problem152Drafts.SetupsForCombinationSolutions(max);
            var combinations = Problem152Drafts.GetValidCombinations(prime, max);

            Assert.Equal(1, combinations.Count);
        }

        [Theory]
        //[InlineData(10,0)]
        [InlineData(35,1)]
        public void GetSolutionsUsingCombinationsFractions(int max, int expected)
        {
            var solutions = Problem152Drafts.GetSolutionsUsingCombinationsFractions(max);

            Assert.Equal(expected, solutions);
        }
        #endregion

        #region GetMaxLefts
        [Theory]
        [InlineData(80, 2)]
        [InlineData(80, 3)]
        [InlineData(80, 4)]
        [InlineData(80, 5)]
        [InlineData(80, 6)]
        public void IsMaxLeftGreaterThanHalf(int max, int denom)
        {
            var leftAlls = Problem152Drafts.CalculateMaxLeftAll(2, max);
            var toSubtract = (decimal)1 / (denom * denom);
            leftAlls -= toSubtract;

            Assert.True(leftAlls > 0.5m);
        }

        [Theory]
        [InlineData(80, 3, 4)]
        [InlineData(80, 4, 5)]
        public void IsMaxLeftGreaterThanHalf2Values(int max, int denom1, int denom2)
        {
            var leftAlls = Problem152Drafts.CalculateMaxLeftAll(2, max);
            var toSubtract = (decimal)1 / (denom1 * denom1);
            var toSubtract2 = (decimal)1 / (denom2 * denom2);
            leftAlls -= toSubtract;
            leftAlls -= toSubtract2;

            Assert.True(leftAlls > 0.5m, leftAlls.ToString());
        }
        #endregion

        #region Prime Decompositions
        [Theory]
        //[InlineData(10,0)]
        //[InlineData(35, 1)]
        //[InlineData(40, 1)]
        //[InlineData(44, 1)] // 1 secs
        //[InlineData(45,3)] // 2 secs
        [InlineData(50, 3)] // 22 secs
        public void GetSolutionsUsingCombinationsAndPrimeDecompositions(int max, int expected)
        {
            var solutions = Problem152Drafts.GetSolutionsUsingCombinationsAndPrimeDecompositions(max);

            Assert.Equal(expected, solutions.Count);
        }


        [Theory]
        //[InlineData(35, 2,3,5,36)]
        [InlineData(35, 2,0,1,4)]
        public void AddFractionUsingPrimeDecomposition(int max, int denom1, int denom2, double expectedNum, double expecteDenom)
        {
            var primes = Util.getPrimes(max);
            var decomp1 = Util.GetPrimeDecompositions(primes, denom1);
            var decomp2 = Util.GetPrimeDecompositions(primes, denom2);

            var sum = Problem152Drafts.AddFractionToCurrentFractionSum(1, decomp1, decomp2, primes);

            Assert.Equal(expectedNum, sum.Item1);
            Assert.Equal(expecteDenom, Util.GetValueFromDecomposition(sum.Item2, primes));             
        }
        #endregion

        #region Straight Sums
        [Theory]
        //[InlineData(4, 0)]
        //[InlineData(10,0)]
        //[InlineData(35,1)]
        //[InlineData(40, 1)]
        //[InlineData(44, 1)] // 1 secs
        //[InlineData(45,3)] // 1 secs
        //[InlineData(50, 3)] // 13 secs
        [InlineData(55, 8)] // 65 secs
        //[InlineData(60, 8)] //
        public void GetSolutionsUsingCombinationsAndStraightSum(int max, int expected)
        {
            var solutions = Problem152Drafts.GetSolutionsUsingCombinationsAndStratightSum(max);

            Assert.Equal(expected, solutions.Count);
        }

        [Fact]
        public void StraightSumWorks()
        {
            List<int> denoms = new List<int> () { 2, 3, 4, 5, 7, 12, 15, 20, 28, 35 };

            Assert.True(Problem152Drafts.IsSolutionBySum(denoms));
        }
        #endregion

        #region Combinations
        [Theory]
        [InlineData(35, 19200)]
        [InlineData(45, 228480)]
        [InlineData(80, 4413)]
        public void Combinations(int max, int expectedCount)
        {
            Problem152Drafts.SetupsForCombinationSolutions(max);
            var combos = Problem152Drafts.GetCombinations(max);

            int count = 1;
            for (int i = 0; i < combos.Count; i++)
            {
                count *= combos[i].Count;
            }

            Assert.Equal(expectedCount, count);
        }
        #endregion

        #region Combinations and Partial Fractions
        [Theory]
        //[InlineData(35,1)]
        //[InlineData(45,3)] // 1 secs
        [InlineData(50, 3)] // 10 secs
        //[InlineData(55, 8)] // 35 sec
        //[InlineData(60, 8)] // 4mins
        //[InlineData(80, 8)] //
        public void GetSolutionsUsingCombinationsAndPartialFractions(int max, int expected)
        {
            var solutions = Problem152Drafts.GetSolutionsUsingCombinationsAndPartialFractions(max);

            Assert.Equal(expected, solutions.Count);
        }
        #endregion

        #region Combinations and Partial Fractions
        [Theory]
        //[InlineData(35,1)]
        //[InlineData(45,3)] // 1 secs
        //[InlineData(50, 3)] // 4 secs
        //[InlineData(55, 8)] // 9 sec
        //[InlineData(60, 34)] // 31 sec
        [InlineData(80, 301)] //
        public void GetSolutionsWithPartialFractionsAndTrees(int max, int expected)
        {
            var solutions = Problem152Drafts.GetSolutionsWithPartialFractionsAndTrees(max,35);
            
            Assert.Equal(expected, solutions.Count);
        }

        [Theory]
        [InlineData(80, 253)]
        public void TryAll(int max, int expected)
        {
            var solutions = Problem152Drafts.TryAllErrors(max, 35);

            Assert.Equal(expected, solutions);
        }

        [Theory]
        [InlineData(.5, 0)]
        [InlineData(1.50,0)]
        [InlineData(3.50,2)]
        [InlineData(4,3)]
        [InlineData(5,4)]
        public void GetLowerIndex(decimal toCheck, int expected)
        {
            var added = new List<decimal>() { 1, 2, 3, 4, 5 };
            Assert.Equal(expected, Problem152Drafts.GetLowerIndex(added,toCheck,0,4));
        }
        #endregion
    }
}
