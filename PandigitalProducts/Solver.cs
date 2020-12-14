using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandigitalProducts
{
    public class Solver
    {
        public double solve()
        {
            double sum = 0;
            for(int i=1234;i<9876;i++)
            {
                if (isPandigital(i))
                    sum += i;
            }
            return sum;
        }

        public bool isPandigital(int value)
        {
            List<EulerMisc.Util.FactorTuple> tuples = Util.getFactorTuples(value);
            foreach(EulerMisc.Util.FactorTuple tuple in tuples)
            {
                List<int> factor1digits = Util.GetDigits(tuple.factor1),
                    factor2digits = Util.GetDigits(tuple.factor2),
                    valueDigits = Util.GetDigits(value);

                factor1digits.AddRange(factor2digits);
                factor1digits.AddRange(valueDigits);
                if(areAllDigitsPresent(factor1digits))
                {
                    return true;
                }
            }
            
            return false;
        }

        public bool areAllDigitsPresent(List<int> digits)
        {
            bool[] presents = new bool[10];
            foreach(int i in digits)
            {
                presents[i] = true;
            }

            for(int i=1;i<10;i++)
            {
                if (!presents[i])
                    return false;
            }

            return true;
        }
    }
}
