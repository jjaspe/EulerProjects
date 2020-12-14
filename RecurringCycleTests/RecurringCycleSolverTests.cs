using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecurringCycle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RecurringCycle.Tests
{
    [TestClass()]
    public class RecurringCycleSolverTests
    {
        public static RecurringCycleSolver solver;
		[ClassInitialize()]
		public static void init(TestContext t)
        {
	        solver=new RecurringCycleSolver();
		}     
        
        [TestMethod()]
        public void solveTest()
        {
            Assert.AreEqual(7,solver.solve(10));
        }

        [TestMethod()]
        public void solveTestMaxUpTo15()
        {
            Assert.AreEqual(7, solver.solve(15));
        }

        [TestMethod()]
        public void solveTestMaxUpTo18()
        {
            Assert.AreEqual(17, solver.solve(18));
        }

        [TestMethod()]
        public void solveTestMaxUpTo20()
        {
            Assert.AreEqual(19, solver.solve(20));
        }
    }
}
