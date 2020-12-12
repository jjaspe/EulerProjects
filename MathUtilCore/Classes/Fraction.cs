using System;
using System.Numerics;

namespace MathUtilCore
{
    public class Fraction
    {
        public BigInteger Numerator { get; set; }
        public BigInteger Denominator { get; set; }
        public string Id { get { return Numerator.ToString() + "_" + Denominator.ToString(); } }

        public Fraction(BigInteger numerator, BigInteger denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        public Fraction Reduce(int[] primes)
        {
            for (int i = 0; i < primes.Length; i++)
            {
                while (this.Numerator % primes[i] == 0 && this.Denominator % primes[i] == 0)
                {
                    this.Numerator /= primes[i];
                    this.Denominator /= primes[i];
                }
            }
            return this;
        }

        public Fraction Add(Fraction sumand)
        {
            var result = this + sumand;
            this.Numerator = result.Numerator;
            this.Denominator = result.Denominator;
            return this;
        }

        public Fraction Subtract(Fraction sumand)
        {
            var result = this - sumand;
            this.Numerator = result.Numerator;
            this.Denominator = result.Denominator;
            return this;
        }

        public static Fraction operator +(Fraction fraction, Fraction fraction2)
        {
            var Numerator = fraction.Numerator * fraction2.Denominator + fraction.Denominator * fraction2.Numerator;
            var Denominator = fraction.Denominator * fraction2.Denominator;
            return new Fraction(Numerator, Denominator);
        }

        public static Fraction operator -(Fraction fraction, Fraction fraction2)
        {
            var Numerator = fraction.Numerator * fraction2.Denominator - fraction.Denominator * fraction2.Numerator;
            var Denominator = fraction.Denominator * fraction2.Denominator;
            return new Fraction(Numerator, Denominator);
        }

        public static bool operator < (Fraction fraction, decimal compare)
        {
            return (decimal)(fraction.Numerator / fraction.Denominator) < compare;
        }

        public static bool operator > (Fraction fraction, decimal compare)
        {
            return (decimal)(fraction.Numerator / fraction.Denominator) > compare;
        }

        public static bool operator >(Fraction fraction, Fraction compare)
        {
            return fraction.Numerator * compare.Denominator > fraction.Denominator * compare.Numerator;
        }

        public static bool operator <(Fraction fraction, Fraction compare)
        {
            return fraction.Numerator * compare.Denominator < fraction.Denominator * compare.Numerator;
        }

        public static bool operator >=(Fraction fraction, Fraction compare)
        {
            return fraction.Numerator * compare.Denominator >= fraction.Denominator * compare.Numerator;
        }

        public static bool operator <=(Fraction fraction, Fraction compare)
        {
            return fraction.Numerator * compare.Denominator <= fraction.Denominator * compare.Numerator;
        }
    }
}
