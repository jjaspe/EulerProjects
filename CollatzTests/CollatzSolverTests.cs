using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collatz;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Collatz.Tests
{
    [TestClass()]
    public class CollatzSolverTests
    {
        static CollatzSolver solver;
        [ClassInitialize()]
        public static void init(TestContext context)
        {
            solver = new CollatzSolver();
        }

        [TestMethod()]
        public void solveTest()
        {
            solver.solve(1000000);
            Assert.AreEqual(522 + 3, solver.sequences[837799]);
        }
    }
}
