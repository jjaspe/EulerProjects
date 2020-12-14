using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderTheRainbow493
{
    public class Solver
    {
        int[] nChoose;
        double denom = 0;
        int colorCount = 3;
        int ballCount = 2;

        public Solver()
        {
            nChoose= new int[ballCount];
            for(int i=0;i<nChoose.Length;i++)
            {
                nChoose[i] = (int)(Util.factorial(nChoose.Length) / (Util.factorial(nChoose.Length - i - 1) * Util.factorial(i + 1)));
            }
            denom = Util.factorial(colorCount*ballCount) / (Util.factorial(colorCount*ballCount-2*ballCount) * Util.factorial(2*ballCount));
        }

        public List<int[]> getTupleAdditions(int count,int max)
        {
            List<int[]> tuples = new List<int[]>();
            return null;

        }

        public double solve()
        {
            return 0;
        }

        public double getProbabilityNumerator(int count)
        {
            double sum =0,multiplier= (int)(Util.factorial(colorCount - count) / (Util.factorial(colorCount - count) * Util.factorial(count)));
            sum = forLoop(count, 20);
            return sum*multiplier;
        }

        public double forLoop(int count,int match)
        {
            double sum = 0;
            if(count==2)
            {
                for(int i=1;i<=match;i++)
                {
                    sum += nChoose[i] * nChoose[match - i];
                }
            }
            else
            {
                //Outer fors can only go up to match - count+1, so inner loops can at least have 1 each
                for(int i=1;i<=match-count+1;i++)
                {
                    sum+=nChoose[i]*forLoop(count - 1, match - i);
                }
            }

            return sum;
        }
    }
}
