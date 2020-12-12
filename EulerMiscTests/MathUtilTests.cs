using EulerMisc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerMiscTests
{
    [TestClass]
    public class MathUtilTests
    {
        [TestMethod]
        public void FromBinary()
        {
            double value = 5; // 101

            var result = MathUtil.IndecesFromBinary(value);

            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(2, result[1]);
        }
    }
}
