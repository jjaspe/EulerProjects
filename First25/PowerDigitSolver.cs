using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerDigitSum
{
    public class PowerDigitSolver
    {
        public int bas=2;
        public int power=1000;
        public int factor1 = 25, factor2 = 4, factor3 = 10;

        public int solveAdd(int toAdd=0)
        {            
            int[] number=solveArray();
            int sum = 0;
            if (toAdd == 0)
                toAdd = number.Length;
            for(int i=0;i<toAdd;i++)
            {
                sum += number[number.Length - i - 1];
            }
            return sum;
        }


        /// <summary>
        /// 1) Get 2^25 in decimal array
        /// 2)Multiply it 4 times to get (2^25)^4=2^100
        /// 3)Multiply that 10 times to get 2^1000
        /// </summary>
        /// <returns></returns>
        public int[] solveArray()
        {
            int[] stArray = getStarting();
            int[] current=stArray;
            for (int i = 1; i < factor2; i++)
            {
                current = Util.multiplyArrays(current, stArray);
            }

            stArray = current;
            for (int i = 1; i < factor3; i++)
            {
                current = Util.multiplyArrays(current, stArray);
            }
            return current;
        }

        public int[] getStarting()
        {
            return Util.toDecimalArray(Math.Pow(bas, factor1));
        }

        

        

        
    }
}
