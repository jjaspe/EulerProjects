using Problems.Problem_69;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Problem69Tests
{
    public class Problem69Tests
    {
        Problem69Solver solver;

        public Problem69Tests()
        {
            solver = new Problem69Solver();
        }

        [Fact]
        public void IsFast()
        {
            solver.Solve();
        }

        [Fact]
        public void Solves()
        {
            var solution = solver.Solve();

            Assert.Equal(10m, solution);
        }
    }
}
