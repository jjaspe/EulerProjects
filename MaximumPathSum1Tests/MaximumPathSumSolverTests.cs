using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaximumPathSum1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MaximumPathSum1.Tests
{
    [TestClass()]
    public class MaximumPathSumSolverTests
    {
        static MaximumPathSumSolver solver;
        [ClassInitialize()]
        public static void init(TestContext c)
        {
            solver = new MaximumPathSumSolver();
            solver.init();
        }

        [TestMethod()]
        public void nextThreeTestTop()
        {
            Path[] paths = solver.nextThree(0, 0);

            Assert.AreEqual(187, paths[0].sum);
            Assert.AreEqual(217, paths[1].sum);
            Assert.AreEqual(221, paths[2].sum);
        }

        [TestMethod()]
        public void nextThreeTestAt4And2()
        {
            Path[] paths = solver.nextThree(4, 2);

            Assert.AreEqual(182, paths[0].sum);
            Assert.AreEqual(230, paths[1].sum);
            Assert.AreEqual(164, paths[2].sum);
        }

        [TestMethod()]
        public void getNextRowTestAtI12()
        {
            Path[] paths = solver.solve(new Spot(12, 0));
            Assert.IsNotNull(paths);
        }

        [TestMethod()]
        public void getNextRowTestAtI12J10()
        {
            Path[] paths = solver.solve(new Spot(12, 10));
            Assert.AreEqual(134,paths[0].sum);
            Assert.AreEqual(167, paths[1].sum);
            Assert.AreEqual(174, paths[2].sum);
        }
        [TestMethod()]
        public void getNextRowTestAtI11J10()
        {
            Path[] paths = solver.solve(new Spot(11, 10));
            Assert.AreEqual(134+17, paths[0].sum);
            Assert.AreEqual(186, paths[1].sum);
            Assert.AreEqual(193, paths[2].sum);
            Assert.AreEqual(90, paths[3].sum);
        }

        [TestMethod()]
        public void getNextRowTestAt0Left()
        {
            Path[] paths = solver.solve(new Spot(0, 0));
            Assert.AreEqual(794,paths[0].sum);
        }

        [TestMethod()]
        public void getNextRowTestAt0Right()
        {
            Path[] paths = solver.solve(new Spot(0, 0));
            Assert.AreEqual(724, paths[paths.Length-1].sum);
        }


    }
}
