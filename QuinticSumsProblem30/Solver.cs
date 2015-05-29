using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuinticSumsProblem30
{
    class Solver
    {
        double UpperBound = 0;
        public int power = 4;
        public Logger logger;
        List<PermutativeSum> previousSums = new List<PermutativeSum>();

        internal string findSumOfQuintics()
        {
            UpperBound = findUpperBound();
            List<PermutativeSum> newSums;
            for(int i=2;i<=UpperBound;i++)
            {
                newSums = getSumsWithIDigits(i);
            }
            return "";
        }

        private List<PermutativeSum> getSumsWithIDigits(int digits)
        {
            List<PermutativeSum> newSums = new List<PermutativeSum>();
            PermutativeSum current;
            foreach(PermutativeSum sum in previousSums)
            {
                for(int i=0;i<10;i++)
                {
                    current=sum.Copy();
                    current.sum+=Math.Pow(i,power);
                    current.digitAmounts[i]++;
                    newSums.Add(current);
                }
            }
            return newSums;
        }

        private double findUpperBound()
        {
            double i = 5;
            while(true)
            {
                if (i * Math.Pow(9, 5) < Math.Pow(10, i - 1))
                    break;
                else
                    i++;
            }
            return i;
        }
    }

    class PermutativeSum
    {
        public int[] digitAmounts = new int[10];
        public double sum;
        public PermutativeSum Copy()
        {
            PermutativeSum copy=new PermutativeSum();
            copy.sum=this.sum;
            for(int i=0;i<10;i++)
            {
                copy.digitAmounts[i]=this.digitAmounts[i];
            }
            return copy;
        }
    }
}
