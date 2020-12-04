using MathUtilCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

/*
 * Starting with 1 and spiralling anticlockwise in the following way, 
a square spiral with side length 7 is formed.

37 36 35 34 33 32 31
38 17 16 15 14 13 30
39 18  5  4  3 12 29
40 19  6  1  2 11 28
41 20  7  8  9 10 27
42 21 22 23 24 25 26
43 44 45 46 47 48 49

It is interesting to note that the odd squares lie along the bottom right diagonal, 
but what is more interesting is that 8 out of the 13 numbers lying along both diagonals 
are prime; that is, a ratio of 8/13 ≈ 62%.

If one complete new layer is wrapped around the spiral above, a square spiral with side length 9 
will be formed. If this process is continued, what is the side length of the square spiral for 
which the ratio of primes along both diagonals first falls below 10%?
*/

//a0 = 1 + 2 + 8*0 = a0
//a1 = a0 + 2 + 8*1 = 1 + 2*2 + 8*(0+1)
//a2 = a1 + 2 + 8*2 = 1 + 2*3 + 8*(0+1+2)
//an = 1 + 2*(n+1) + 8*(n*n-1)/2=1 + 2(n+1) + 4n(n+1)

//2: 2 4 6 8 10 12
//3: 9 15 21 27
//5: 25 35 55 65 5*17 5*19

namespace Problems.Problem_58
{
    public class Problem58Solver
    {
        bool[] multiples;

        public int GetFirstLowerThanPercent(decimal percent)
        {
            var n = 1;
            var currentCount = 0;
            var diagonalCount = 1;
            multiples = Util.GetMultiples(2000000000);
            do
            {
                n += 2;
                currentCount = GetPrimeCount(currentCount, n);
                diagonalCount = n * 2 - 1;
            } while (((decimal)currentCount * 100 / diagonalCount) >= percent);
            return n;
        }

        public int GetPrimeCount(int previousCount, int size)
        {
            var primeCount = previousCount;
            var i = (size - 1 - 2) / 2;
            var jump = 1 + 4 * i * (i + 1);
            //top right
            if (multiples[2 * (i + 1) + jump] == false)
                primeCount++;
            //top left
            if (multiples[4 * (i + 1) + jump] == false)
                primeCount++;
            //bottom left
            if (multiples[6 * (i + 1) + jump] == false)
                primeCount++;
            //bottom right
            if (multiples[8 * (i + 1) + jump] == false)
                primeCount++;
            return primeCount;
        }

        public decimal GetPercent(int size)
        {
            var multiples = Util.GetMultiples(size*size);
            int diagonalCount = size * 2 - 1;
            var primeCount = 0;
            for (int i = 0; i < (size-1)/2; i++)
            {
                var jump = 1 + 4*i*(i+1);
                //top right
                if (multiples[2*(i+1) + jump] == false)
                    primeCount++;
                //top left
                if (multiples[4 * (i + 1) + jump] == false)
                    primeCount++;
                //bottom left
                if (multiples[6 * (i + 1) + jump] == false)
                    primeCount++;
                //bottom right
                if (multiples[8 * (i + 1) + jump] == false)
                    primeCount++;
            }
            return ((decimal)100*primeCount / diagonalCount);
        }


    }
}
