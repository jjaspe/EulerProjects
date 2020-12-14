using Problems.Problem_58;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Problem58
{
    public class Problem58Tests
    {
        Problem58Solver solver;

        public Problem58Tests()
        {
            solver = new Problem58Solver();
        }

//        If this process is continued, what is the side length of the square spiral for 
////which the ratio of primes along both diagonals first falls below 10%?
        [Theory]
        //[InlineData(7, 61)]
        //[InlineData(29, 40)]
        //[InlineData(999, 15)]
        //[InlineData(1999, 13)]
        //[InlineData(26239, 10)]
        [InlineData(26241, 9)]
        public void Problem58Test(int size, int expected)
        {
            var percent = solver.GetPercent(size);

            Assert.Equal(expected, (int)percent);
        }

        [Fact]
        public void Problem58SolverTest()
        {
            var side = solver.GetFirstLowerThanPercent(10);

            Assert.Equal(26241, side);
        }
    }
}
