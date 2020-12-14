using System;
using System.Collections.Generic;
using System.Text;

namespace Problems.Problem_53
{
    /*
     *There are exactly ten ways of selecting three from five, 12345:

123, 124, 125, 134, 135, 145, 234, 235, 245, and 345

How many, not necessarily distinct, values of (n,r)
 for 1<=n<=100, are greater than one-million?
     */
    public class Problem53Solver
    {
        //If (n,r) > top, then any (n,p) where p > r and p<n/2 will also > top
        //Since (n,r) == (n,(n-r)), we only have to check the 1<=r<=n/2
        int top = 1000000;
        public int Solve(int max)
        {
            var found = new List<(int, int)>();
            for (int i = 1; i <= max; i++)
            {
                var newFound = CheckN(i);
                found.AddRange(newFound);
            }
            return found.Count;
        }

        public List<(int,int)> CheckN(int n)
        {
            var current = 1;
            var found = new List<(int, int)>();
            var keepAdding = false;
            for (int i = 1; i <= (n-n%2)/2; i++)
            {
                if (!keepAdding)
                {
                    current = GetNew(current, n, i);
                    if (current > top)
                    {
                        keepAdding = true;
                    }
                }
                if (keepAdding)
                {
                    found.Add((n, i));
                    if(i!=n-i)
                        found.Add((n, n - i));
                }                
            }
            return found;
        }

        //n!/(n-r)!r! = n!(n-(r-1))/((n-r+1)!(r-1)!*r) = A(r-1)*(n-(r-1))/r
        public int GetNew(int previous, int n, int newR)
        {
            return previous * (n - (newR-1))/newR;
        }
    }
}
