
using Problems.Problem52;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Problem52
{
    public class Problem52Tests
    {
        Problem52Solver solver;

        public Problem52Tests()
        {
            solver = new Problem52Solver();
        }

        [Theory]
        [InlineData(1000000000)]
        public void MyTestMethod(int max)
        {
            var solution = solver.Solve(max);

            Assert.Equal(1231, solution);
        }
    }
    
}
