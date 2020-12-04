
using Problems.Problem_55;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Problem55
{
    public class Problem55Tests
    {
        Problem55Solver solver;

        public Problem55Tests()
        {
            solver = new Problem55Solver();
        }

        [Theory]
        [InlineData(1000, 14)]
        [InlineData(10000, 263)]
        public void MyTestMethod(int max, int expected)
        {
            var solution = solver.Solve(max);

            Assert.Equal(expected, solution);
        }

    }

}
