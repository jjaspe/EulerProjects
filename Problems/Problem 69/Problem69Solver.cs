using MathUtilCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problems.Problem_69
{
    /*
     * Euler's Totient function, φ(n) [sometimes called the phi function], 
     * is used to determine the number of numbers less than n which are relatively prime to n. 
     * For example, as 1, 2, 4, 5, 7, and 8, are all less than nine and relatively prime to nine, φ(9)=6.
     * It can be seen that n=6 produces a maximum n/φ(n) for n ≤ 10.
     * Find the value of n ≤ 1,000,000 for which n/φ(n) is a maximum.
    */

    /*
    if a is prime, tau(n) = n-1. If a is prime, tau(a^p) = a^p - a^(p-1)
    (a,b) = 1, tau(a*b) = a*b - phi(a)*b - phi(b)*a - 1 = a*b - b - a + 1;
    tau(8) = 4, tau(5) = 4, 
    [1,3,5,7], [1,2,3,4]
    [1,3,7,9,11,13,17,19,21,23,27,29,31,33,37,39]
     with (a,b) = 1, tau(a^n*b)=tau(a*b)*a^(n-1)=(a*b - b - a + 1)*a^(n-1)
    tau(24)= tau(2^3*3) = (6 - 2 -3 + 1)*4 = 8.
    tau(9*4) = tau(3^2*2^2) = (6-2-3+1)*3*2 = 12

    tau(2*13)=(2-1)*tau(13)=12
    tau(2*13*5) = 
    
    tau(2*3) = 6 - 6/2 - 6/3 + 6/6 = 3*(2-1) -(2-1) = tau(2)*(3-1)= tau(2)*tau(3)
    tau(2*3*5) = tau(2*3)*5 - 30/5 + 30/10 + 30/15 - 30/30 = tau(2*3)*5 - (6 - 3 - 2 +1)
    tau(2*3*5) = 30 - 30/2 - 30/3 - 30/5 + 30/6 + 30/10 + 30/15 - 30/30= 30 -15 - 10 - 6 + 5 + 3 + 2 - 1 = -1 + 10 -1 = 8 =tau(2)*tau(3)*tau(5)
    tau(2*3*5*7) = (tau(2*3*5)-1)*7
    tau(2*3*5) = 2*3*5 - phi(2*3*5)
    So for primes if a = p1^a1*p2^a2...pn^an, find tau(p1*p2*p3....pn) with the inclusion exclusion method, then
    multiply that by p1^(a1-1)*p2^(a2-1)...pn^(an-1).
    If (a,p1*p2) = 1, then (a + x*(p1*p2), (p1*p2)) = 1, since if b|(a+ x(p1*p2)) and b|(p1*p2), then b|a, a contradiction.

    let r(n) = n/tau(n). If n = p1^a1*b, with (p,b)=1, then r(n*p1)=r(n).
    Proof: the tau goes up by a factor of p1, same as n.

    r(3) = 3/2, r(9) = 9/6 = 3/2
    r(6) = 6/2 = 3, r(6*2*3) = 36/(2*3*2) = 6/2 = 3;

    No point in adding extra powers. The only thing that changes r(n) is changing the base primes.
    No solitary prime will be the answer, since if p1 < p2 then p1/(p1-1) > p2/(p2-1), and r(7)<r(9)
    Then we need to at least have a pair, which means the largest prime must be < 1 million/2

    Adding smaller primes to the decomposition increases r(n) faster, since p/p-1 decreases as p increases.
    Adding more primes increases the product since p/p-1 > 1 for all p.
    So answer should be the product of the maxima set of primes whose product is < 1000000
    */

    public class Problem69Solver
    {
        public decimal Solve()
        {
            var primesArray = Util.GetMultiples(500000);
            var primes = new List<int>();
            var product = 1m;
            for (int i = 1; i < primesArray.Length; i++)
            {
                if (!primesArray[i])
                {
                    primes.Add(i);
                    if((product*i) < 1000000)
                        product *= i;
                }                    
            }
            return product;
        }
        //Find primes less than max = 1 million/2
        //Make number using these primes <= max, all with power 1, save prime decomposition.
        //calculate tau from decomposition, using inclusiong exclusion method.
    }
}
