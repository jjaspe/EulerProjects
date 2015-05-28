using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EulerProjects;
using EulerMisc;

namespace FirstTenDigitsTests
{
    [TestClass()]
    public class FirstTenDigitsSolverTests
    {
        static FirstTenDigitsSolver solver;
        [ClassInitialize()]
        public static void init(TestContext context)
        {
            solver = new FirstTenDigitsSolver();
        }

        [TestMethod()]
        public void getStringsTest()
        {
            string[] s = solver.getStrings();
            Assert.AreEqual(100, s.Length);
        }

        [TestMethod()]
        public void toDecimalArrayTest()
        {
            int[] numbers = solver.toDecimalArray(solver.getStrings());
            Assert.IsNotNull(numbers);
        }

        [TestMethod()]
        public void toTripleArrayTest()
        {
            int[,] tripleArray = solver.toTripleArray(solver.toDecimalArray(solver.getStrings()));
            Assert.IsNotNull(tripleArray);
        }

        [TestMethod()]
        public void solveTestNoCarry()
        {
            string s = "1234567890 2345678910";
            //3580246800;
            int[] expected = { 0, 0, 8, 6, 4, 2, 0, 8, 5, 3, 0, 0 };
            Assert.IsTrue(Util.arraysEqual(expected, solver.solve(s)));
        }
        [TestMethod()]
        public void solveTestCarry()
        {
            string s = "999 999 999 999 999 999 999 999 999 999 999";
            int[] expected = { 9, 8, 9, 0, 1};
            Assert.IsTrue(Util.arraysEqual(expected, solver.solve(s)));
        }

        
    }
}
