using System;
using Xunit;
using MathUtil;

namespace MathUtilTests
{
    
    public class MathUtilTests
    {
        [Fact]
        public void FromBinary()
        {
            double value = 5; // 101

            var result = MathUtil.MathUtil.IndecesFromBinary(value);

            Assert.Equal(0, result[0]);
            Assert.Equal(2, result[1]);
        }
    }
}
