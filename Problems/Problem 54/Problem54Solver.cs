using System;
using System.Collections.Generic;
using System.Text;

namespace Problems.Problem_54
{
    /*
     * A googol (10^100) is a massive number: one followed by one-hundred zeros; 
     * 100^100 is almost unimaginably large: one followed by two-hundred zeros. 
     * Despite their size, the sum of the digits in each number is only 1.
        Considering natural numbers of the form, a^b, where a, b < 100, 
        what is the maximum digital sum?
     */
    public class Problem54Solver
    {
        public int Solve(int na)
        {
            int highest = 0;
            for (int i = 2; i <= na; i++)
            {
                var a = new int[2] { na % 10, na / 10 };
                var digits = new List<int>() { na % 10, na / 10 };
                var sum = 0;
                for (int j = 1; j <= na; j++)
                {
                    (sum, digits) = Multiply(digits, a);
                    if (sum > highest)
                        highest = sum;
                }
            }
            return highest;
        }

        private (int,List<int>) Multiply(List<int> current, int[] a)
        {
            var newList = new List<int>();
            int carry = 0;
            int sum = 0;
            for (int i = 0; i < current.Count; i++)
            {
                var valueLow = (current[i] * a[0] + carry) % 10;
                sum += valueLow;
                newList.Add(valueLow);
                carry = (current[i] * a[0] + carry) / 10 + current[i]*a[1];
            }
            while(carry > 0)
            {
                newList.Add(carry%10);
                sum += carry%10;
                carry /= 10;
            }
            return (sum, newList);
        }
    }


}
