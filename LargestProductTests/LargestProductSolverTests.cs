using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EulerProjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace EulerProjects.Tests
{
    [TestClass()]
    public class LargestProductSolverTests
    {
        static LargestProductSolver solver;
        [ClassInitialize()]
        public static void init(TestContext c)
        {
            solver = new LargestProductSolver();
        }
        [TestMethod()]
        public void toArrayTest()
        {
            int[] array = solver.toArray();
            Assert.IsNotNull(array);
        }

        [TestMethod()]
        public void solveTest()
        {
            string st = "01 01 01 01 02 " +
                        "01 01 01 01 05 " +
                        "05 01 08 01 06 " +
                        "05 01 08 01 07 " +
                        "01 01 03 02 08";
            solver.numbers=st;
            Assert.AreEqual(1680, solver.solve());
        }
    }
}
