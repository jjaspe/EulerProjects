using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abundant;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abundant.Tests
{
    [TestClass()]
    public class AbundantSolverTests
    {
        static AbundantSolver solver;
        [ClassInitialize()]
        public static void init(TestContext t)
        {
            solver = new AbundantSolver();
        }

        [TestMethod()]
        public void solveTest()
        {
            solver.max = 32;
            Assert.AreEqual(442, solver.solve());
        }

        [TestMethod()]
        public void solveTest2()
        {
            solver.max = 36;
            Assert.AreEqual(544, solver.solve());
        }
    }
}
