using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collatz
{
    public class CollatzSolver
    {
        public int[] sequences;

        public double nextEven(double n)
        {
            return n / 2;
        }

        public double nextOdd(double n)
        {
            return 3 * n + 1;
        }

        public double next(double n)
        {
            return n % 2 == 0 ? nextEven(n) : nextOdd(n);
        }

       

        public int solve(int max)
        {
            int[] counts=new int[max*3+1];
            Queue<double> previ = new Queue<double>();
            double current=1;
            int currentCount = 0;

            //Fill in powers of 2
            for (int i = 1; current < max;i++)
            {
                counts[(int)current] = i;
                current *= 2;
            }

            for (int i = 2; i < max; i++)
            {
                current = i;
                currentCount = 0;
                while (current > 1)
                {
                    if(current==1)
                    {
                        //Down to one so go up and break
                        ++currentCount;
                        break;
                    }
                    //Not 1, so keep going
                    if (current < counts.Length && counts[(int)current] > 0)//Check if possible it was in counts
                    {
                        currentCount += counts[(int)current];                        
                        //We are done so break;
                        break;
                    }
                    else
                    {
                        //Not in counts so just get next and keep going
                        currentCount++;
                        //Save in stack to solve back
                        previ.Enqueue(current);
                        current = next(current);
                    }
                }
                //Go through the stack and save those too, if array big enough
                while(previ.Count>0)
                {
                    current = previ.Dequeue();
                    if (current < counts.Length)
                        counts[(int)current] = currentCount--;
                    else
                        currentCount--;
                }
            }
            sequences = counts;
            return  Util.findMax(counts);            
        }

        public int getSequenceCount(double n, int[] counts)
        {
            int current;
            if (n < counts.Length && counts[(int)n] == 0)
            {
                if (n == 1)
                    counts[(int)n] = 1;
                else
                    counts[(int)n] = 1 + getSequenceCount(next(n), counts);
                current = counts[(int)n];
            }
            else
                current = 1 + getSequenceCount(next(n), counts);           
                
            return current;
        }

        public void printCollatz(double n)
        {
            int i=0;
            while(n>1)
            {
                Console.WriteLine(i++ + ":" + n);
                n = next(n);
            }
            Console.WriteLine(i++ + ":" + n);
        }
            
    }
}
