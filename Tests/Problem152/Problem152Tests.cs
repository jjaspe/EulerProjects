using Problems.Problem152;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Problem152
{
    public class Problem152Tests
    {
        Problem152Solver solver;

        public Problem152Tests()
        {
            solver = new Problem152Solver();
        }

        [Theory]
        //[InlineData(35,35,1)]
        //[InlineData(46, 35, 3)]
        //[InlineData(55, 35, 8)] // 1 sec
        //[InlineData(60, 35, 34)] // 1 sec
        //[InlineData(65, 35, 53)] // 1 sec
        //[InlineData(70, 35, 124)] // 2 sec
        //[InlineData(75, 35, 284)] // 5 sec
        //[InlineData(77, 40, 285)] // 5 sec
        //[InlineData(80, 40, 285)] // 8 sec
        [InlineData(80, 35, 298)] // 8 sec
        public void SolveWithTreesAndPartialFractionsOfDecimals2(int max, int split, int expected)
        {
            var solutions = solver.Solve(max, 35);
            var keys1 = new List<string>();
            foreach (var item in solutions)
            {
                item.Sort();
                var stringItem = String.Join("_", item.Select(n => n.ToString()));
                keys1.Add(stringItem);
            }

            solver = new Problem152Solver();
            var solutions2 = solver.Solve(max, 36);
            var keys2 = new List<string>();
            foreach (var item in solutions2)
            {
                item.Sort();
                var stringItem = String.Join("_", item.Select(n => n.ToString()));
                keys2.Add(stringItem);
            }

            var intersect = keys2.Intersect(keys1);
            var diff = keys2.Where(n => !intersect.Contains(n)).ToList();
            var diff2 = keys1.Where(n => !intersect.Contains(n)).ToList();
            Assert.Equal(expected, solutions.Count);
        }

        [Theory]
        //[InlineData(35,35,1)]
        //[InlineData(46, 35, 3)]
        //[InlineData(55, 35, 8)] // 1 sec
        //[InlineData(60, 35, 34)] // 1 sec
        //[InlineData(65, 35, 53)] // 1 sec
        //[InlineData(70, 35, 124)] // 2 sec
        //[InlineData(75, 35, 284)] // 5 sec
        //[InlineData(77, 40, 285)] // 5 sec
        //[InlineData(80, 40, 285)] // 8 sec
        [InlineData(80, 40, 301)] // 8 sec
        public void SolveWithTreesAndPartialFractionsOfDecimals(int max, int split, int expected)
        {
            var solutions = solver.Solve(max, split);
            //var keys1 = new List<string>();
            //foreach (var item in solutions)
            //{
            //    item.Sort();
            //    var stringItem = String.Join("_", item.Select(n => n.ToString()));
            //    keys1.Add(stringItem);
            //}
            //var index = keys1.IndexOf("2_3_4_5_8_12_13_21_24_30_35_39_40_52_56");

            Assert.Equal(expected, solutions.Count);
        }
    }
}
