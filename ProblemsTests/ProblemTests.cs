using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProblemsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Problem152Base()
        {
            Assert.Equal(2, (new Problem152()).Ways(45));
        }
    }
}
