using MathUtilCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MathUtilCoreTests
{   

    public class FractionTests
    {
        int[] primes = new int[12]
        {
            2,3,5,7,11,13,17,19,23,29,31,37
        };

        [Fact]
        public void AddsAndSubtractsCorrectly()
        {
            int min = 8;
            int max = 32;
            Fraction f1 = new Fraction(1, 4);
            List<Fraction> fractions = new List<Fraction>();
            for(int i = min; i <= max; i++)
            {
                var fraction = new Fraction(1, i * i);
                f1 += fraction;
                fractions.Add(fraction);
                f1.Reduce(primes);
            }

            for (int i = fractions.Count - 1; i >= 0; i--)
            {
                f1 -= fractions[i];
                f1.Reduce(primes);
            }

            f1.Reduce(primes);

            Assert.Equal(1, f1.Numerator);
            Assert.Equal(4, f1.Denominator);
        }
    }
}
