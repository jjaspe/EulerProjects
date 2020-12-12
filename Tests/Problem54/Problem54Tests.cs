
using Problems.Problem_54;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Problem54
{
    public class Problem54Tests
    {
        Problem54Solver solver;

        public Problem54Tests()
        {
            solver = new Problem54Solver();
        }

        [Theory]
        //[InlineData(3, 9)]
        //[InlineData(4, 13)]
        //[InlineData(5, 13)]
        //[InlineData(6, 13)]
        //[InlineData(7, 13)]
        //[InlineData(8, 37)]
        //[InlineData(15,40)]
        [InlineData(99, 972)]
        //[InlineData(100, 263)]
        public void MyTestMethod(int max, int expected)
        {
            var solution2 = solver.Solve(max);

            Assert.Equal(expected, solution2);
        }

    }

}
