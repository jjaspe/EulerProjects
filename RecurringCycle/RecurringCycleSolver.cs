using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurringCycle
{
    public class RecurringCycleSolver
    {
        public int solve(int n)
        {
            int winningD = 3, winningCycle = 1; ;
            int[] found = new int[n+1];
            for(int i=3;i<=n;i++)
            {
                found[i] = getRecurringLength(i,found);
                if(found[i]>winningCycle)
                {
                    winningD = i;
                    winningCycle = found[i];
                }
            }


            return winningD;
        }

        /// <summary>
        /// Dividing by 5 or 2 doesn't change the cycle length
        /// Multiply by 10 until above n,store mod, increase count,
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int getRecurringLength(int n,int[] found)
        {
            //Divide out the 2s and 5s
            while (n % 2 == 0)
                n /= 2;
            while (n % 5 == 0)
                n /= 5;
            if (n == 1)
                return 0;
            if (found[n] > 0)
                return found[n];

            int[] digitPosition = new int[1000];
            int count=1;
            int dividend = 1;

            //No need to check for mod=0 because it should never happen
            // because we took out the 2s and 5s
            do
            {
                
                dividend *= 10;
                //Get mod for next step
                dividend = dividend % n;
                if (digitPosition[dividend] > 0)//mod already present, get previous cycle length
                {
                    return count - digitPosition[dividend];
                }
                else
                    digitPosition[dividend] = count++;
                
            } while (true);
        }
    }
}
