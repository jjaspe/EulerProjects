using MathUtilCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Problems.Problem_55
{
    /*
     * If we take 47, reverse and add, 47 + 74 = 121, which is palindromic.

Not all numbers produce palindromes so quickly. For example,

349 + 943 = 1292,
1292 + 2921 = 4213
4213 + 3124 = 7337

That is, 349 took three iterations to arrive at a palindrome.

Although no one has proved it yet, it is thought that some numbers, like 196, never produce a palindrome. 
A number that never forms a palindrome through the reverse and add process is called a Lychrel number. 
Due to the theoretical nature of these numbers, and for the purpose of this problem, we shall assume that a number is Lychrel until proven otherwise. 
In addition you are given that for every number below ten-thousand, 
it will either (i) become a palindrome in less than fifty iterations, 
or, (ii) no one, with all the computing power that exists, has managed so far to map it to a palindrome. 
In fact, 10677 is the first number to be shown to require over fifty iterations before producing a palindrome: 4668731596684224866951378664 (53 iterations, 28-digits).

Surprisingly, there are palindromic numbers that are themselves Lychrel numbers; the first example is 4994.

How many Lychrel numbers are there below ten-thousand?

NOTE: Wording was modified slightly on 24 April 2007 to emphasise the theoretical nature of Lychrel numbers.
*/
    public class Problem55Solver
    {
        //Start at 2 digit number
        //Create list of number used in this iteration (CC)
        //Check existing list of already checked (AC)
        //If in list, answer from AC is answer for current CC
        //else
        //Add current and current flipped to CC
        //Check sum for palindrome
        //If palindrome, add CC to AC as palindromes
        //else continue
        //When iterations > 50, stop add CC to AC as Lychrels
        public int Solve(int max)
        {
            List<BigInteger> NotLychrel = new List<BigInteger>();
            List<BigInteger> Lychrel = new List<BigInteger>();
            for (int i = 10; i < max; i++)
            {
                List<BigInteger> CC = new List<BigInteger>();
                BigInteger sum = 0;
                BigInteger toCheck = i;
                var reverse = Reverse(i);
                int j = 0;
                for (j = 0; j < 49; j++)
                {
                    var (index, inList) = Util.BinaryAlgorithms.GetIndex(NotLychrel, toCheck);
                    if (inList)
                    {
                        /*CC
                        .ForEach(n => {
                            if (n < max)
                                Util.BinaryAlgorithms.Insert(NotLychrel, n, false);
                        });*/
                        break;
                    }

                    (index, inList) = Util.BinaryAlgorithms.GetIndex(Lychrel, toCheck);
                    if(inList)
                    {

                        //if (inList)
                        //{
                        //    CC
                        //    .ForEach(n => {
                        //        if (n < max)
                        //            Util.BinaryAlgorithms.Insert(Lychrel, n, false);
                        //    });
                        //    break;
                        //}
                        break;
                    }
                    //CC.Add(toCheck);
                    //CC.Add(reverse);
                    sum = toCheck + reverse;
                    var sumReverse = Reverse(sum);
                    if (sum == sumReverse)
                    {
                        Util.BinaryAlgorithms.Insert(NotLychrel, i, false);
                        /*CC
                        .ForEach(n => {
                            if (n < max)
                                Util.BinaryAlgorithms.Insert(NotLychrel, n, false);
                        });*/
                        break;
                    }
                    else
                    {
                        toCheck = sum;
                        reverse = sumReverse;
                    }
                }
                if(j == 49)
                {
                    /*CC
                    .ForEach(n => {
                        if (n < max)
                            Util.BinaryAlgorithms.Insert(Lychrel, n, false);
                    });*/
                    Util.BinaryAlgorithms.Insert(Lychrel, i, false);                    
                }
            }
            //var missing = GetMissing(max, Lychrel, NotLychrel);
            return Lychrel.Where(n => n < max).Count();
        }

        BigInteger Reverse(BigInteger number)
        {
            BigInteger reverse = 0;
            BigInteger current = number;
            while(current >= 1)
            {
                reverse = 10 * reverse + current % 10;
                current -= (current % 10);
                current /= 10;
            }
            return reverse;
        }

        List<BigInteger> GetMissing(int max, List<BigInteger> Lychrel,
            List<BigInteger> NotLychrel)
        {
            List<BigInteger> missing = new List<BigInteger>();
            for (int i = 1; i < max; i++)
            {
                if (!Util.BinaryAlgorithms.GetIndex(NotLychrel, i).Item2
                    && !Util.BinaryAlgorithms.GetIndex(Lychrel, i).Item2)
                    missing.Add(i);
            }
            return missing;
        }
    }
}
