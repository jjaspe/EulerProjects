using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LexicographicPermutation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LexicographicPermutation.Tests
{
    [TestClass()]
    public class LexiPermutationSolverTests
    {
        private static LexiPermutationSolver solver;
        private static bool[] digits;
        
        [ClassInitialize()]
        public static void init(TestContext t)
        {
            solver = new LexiPermutationSolver();
            
        }
        [TestMethod()]
        public void getNextIntTest()
        {
            solver.digits = new bool[10];
            digits = new bool[10];
            solver.perm = 0;
            Assert.AreEqual(0,solver.getNextInt(digits,10));
        }

        [TestMethod()]
        public void getNextIntTestFirstNotAvailable()
        {
            solver.digits=new bool[10];
            digits = new bool[10];
            digits[0] = true;
            solver.perm = 1;
            Assert.AreEqual(1, solver.getNextInt(digits, 10));
        }

        [TestMethod()]
        public void solveTest3DigitsPerm1()
        {
            solver.digits = new bool[3];
            solver.perm = 0;
            Assert.AreEqual("012", solver.solve());
        }

        [TestMethod()]
        public void solveTest3DigitsPerm6()
        {
            solver.digits = new bool[3];
            solver.perm = 5;
            Assert.AreEqual("210", solver.solve());
        }

        [TestMethod()]
        public void solveTest3DigitsPerm2()
        {
            solver.digits = new bool[3];
            solver.perm = 2;
            Assert.AreEqual("102", solver.solve());
        }

        [TestMethod()]
        public void solveTest10DigitsPerm0()
        {
            solver.digits = new bool[10];
            solver.perm = 0;
            Assert.AreEqual("0123456789", solver.solve());
        }

        [TestMethod()]
        public void solveTest10DigitsPerm1()
        {
            solver.digits = new bool[10];
            solver.perm = 1;
            Assert.AreEqual("0123456798", solver.solve());
        }
    }
}
