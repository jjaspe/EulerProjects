using MathUtilCore.Helpers;
using System;
using System.Linq;
using Xunit;

namespace MathUtilCoreTests
{
    public class PrimeTests
    {
        [Theory]
        [InlineData(35, 5)]
        [InlineData(35, 10)]
        [InlineData(35, 20)]
        public void GetPrimeDecompositions(int max, int value)
        {
            var primes = Util.getPrimes(max);

            var decomp = Util.GetPrimeDecompositions(primes, value);

            Assert.NotEmpty(decomp);
        }

        [Fact]
        public void GetMultiples()
        {
            var multiples = Util.GetMultiples(100);

            Assert.Equal(26, 100 - multiples.ToList().Count(n => n));
        }
    }
}
