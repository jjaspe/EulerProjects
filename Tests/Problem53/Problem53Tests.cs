
using Problems.Problem_53;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Problem53
{
    public class Problem53Tests
    {
        Problem53Solver solver;

        public Problem53Tests()
        {
            solver = new Problem53Solver();
        }

        [Theory]
        [InlineData(100, 14)]
        //[InlineData(1000, 14)]
        //[InlineData(100, 263)]
        public void MyTestMethod(int max, int expected)
        {
            var solution = solver.Solve(max);

            Assert.Equal(expected, solution);
        }

    }

}
