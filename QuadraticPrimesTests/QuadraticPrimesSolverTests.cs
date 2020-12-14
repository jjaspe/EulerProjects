using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuadraticPrimes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace QuadraticPrimes.Tests
{
    [TestClass()]
    public class QuadraticPrimesSolverTests
    {
        public static QuadraticPrimesSolver solver;
		[ClassInitialize()]
		public static void init(TestContext t)
        {
	        solver=new QuadraticPrimesSolver();
		}

        [TestMethod()]
        public void solverTest2Sequence()
        {
            Result best=solver.solver(3);
            Assert.AreEqual(1, best.a);
            Assert.AreEqual(3, best.b);
        }
    }
}
