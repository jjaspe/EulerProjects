using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorialDigitSum
{
    public class FactorialDigitSumSolver
    {
        public string solve()
        {            
            double start = Util.factorial(15);

            //Since 0s on left dont influence digit sum, lets remove them
            while (start % 10 == 0)
                start /= 10;
            int[] dec = Util.toDecimalArray(start);
            for (int i = 16; i < 100; i++)
            {
                dec = Util.multiplyArrays(dec, Util.toDecimalArray(i));
                dec = Util.trimStart(dec);
            }
                

            return Util.getIntArrayAsString(Util.reverse(Util.addDigitsAsArray(dec)));
        }
    }
}
