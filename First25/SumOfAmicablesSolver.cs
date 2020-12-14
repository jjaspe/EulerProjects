using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amicables
{
    public class SumOfAmicablesSolver
    {
        double[] sumsOfFactors;

        public double solve(int n)
        {
            sumsOfFactors = Util.getSumOfFactorsUpTo(n);
            double amicableSum = 0,currentEuler;
            for(int i=2;i<sumsOfFactors.Length;i++)
            {
                currentEuler = sumsOfFactors[i];
                if (currentEuler < sumsOfFactors.Length && sumsOfFactors[(int)currentEuler] == i
                    && (int)currentEuler!=i)
                    amicableSum += (currentEuler + i);
            }
            amicableSum /= 2;
            return amicableSum;
        }
    }
}
