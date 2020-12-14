using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicographicPermutation
{
    public class LexiPermutationSolver
    {
        //We know that 9!<10^6 and 10!>10^6,
        //so the 10^6th permutation starts with 0
        public double perm = 999999;
        public bool[] digits;

        public string solve()
        {
            if(digits==null)
                digits = new bool[10];
            int digitCount = digits.Length,current=-1;
            string value = "";
            for (int i = 0; i < digits.Length;i++ )
            {
                current = getNextInt(digits, digitCount);
                value += current.ToString();
                digitCount--;
                digits[current] = true;
            }

            return value;
        }

        public int getNextInt(bool[] usedDigits,int digitCount)
        {
            //Get how many perms we can do with the available digits -1, since the next
            // is fixed (i.e. how many with 0 in front, or how many with 1 in front,etc)
            int combos =(int) Util.factorial(digitCount - 1);

            //Get how many times combos fits in permNumber, for each one, we discard
            //one digit as the next one
            int count=(int)perm/combos,i=0,j=0;
            perm =perm-count * combos;
            //Discard count digits, but dont count -1 since those have been used already.
            do
            {
                //Dont count this one, but move index
                if (usedDigits[j])
                    j++;
                else//Count and move index
                {
                    j++;
                    i++;
                }
            } while (i <= count && j<usedDigits.Length);

            return --j;

        }
    }
}
