using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci1000Digits
{
    public class Fibonnacci1000Solver
    {
        public class Result
        {
            public int count;
            public UtilArray current, previous;
        }
        public int solve(int digits)
        {
            Result result=getBigFIbonnaci(digits);
            Console.WriteLine("Previous Length:" + result.previous.Length);
            Console.WriteLine("Current length:"+result.current.Length);
            return result.count;
        }

        public Result getBigFIbonnaci(double n)
        {
            UtilArray first = new UtilArray(1), second = new UtilArray(1),
                next,previous;
            first[0] = 1;
            second[0] = 1;
            int count = 2;
            do
            {
                next = Util.getNextBigFibonnacci(first, second).trimEnd();
                previous = first;
                first = second;
                second = next;
                count++;
            } while (next.Length < n);


            return new Result() { count = count, current = second, previous = first };
        }
    }
}
