using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerCombinationsProblem29
{
    class Solver
    {
        int max = 100;
        int[] primes;
        List<int[]> usedNumbers = new List<int[]>();
        public Logger myLogger;

        internal int solve()
        {
            primes = Util.getPrimes(max);
            List<int[]> baseValues = new List<int[]>();
            for (int i = 2; i <= 100; i++)
                baseValues.Add(getPrimeFactorization(i));

            int count = 0;
            int[] current;
            for(int power=2;power<=100;power++)
            {
                myLogger("Power:" + power);
                foreach(int[] baseValue in baseValues)
                {
                    current=getPrimeFactorization(baseValue,power);
                    if(isNumberNew(current))
                    {
                        usedNumbers.Add(current);
                        count++;
                    }
                }
            }
            return count;
        }

        int[] getPrimeFactorization(int n)
        {
            int[] factorization = new int[primes.Length];
            for(int i=0;i<primes.Length;i++)
            {
                while(n%primes[i]==0)
                {
                    factorization[i]++;
                    n /= primes[i];
                }
            }
            return factorization;
        }

        int[] getPrimeFactorization(int[] factorizedNumber,int power)
        {
            int[] newFactorizedNumber = new int[factorizedNumber.Length];
            for (int i = 0; i < factorizedNumber.Length; i++)
            {
                newFactorizedNumber[i] = factorizedNumber[i]*power;
            }
            return newFactorizedNumber;
        }

        bool isNumberNew(int[] factorizedNumber)
        {
            bool foundEqual = false;

            foreach (int[] usedNumber in usedNumbers)
            {
                foundEqual = true;
                for (int i = 0; i < primes.Length; i++)
                {
                    if (factorizedNumber[i] != usedNumber[i])
                    {
                        foundEqual = false;
                        break;
                    }
                }
                if (foundEqual)
                    return false;
            }
            return true;
        }

        bool isNumberNew(int baseNumber,int power)
        {
            int[] factorizedNumber = getPrimeFactorization(baseNumber);
            factorizedNumber = getPrimeFactorization(factorizedNumber, power);
            bool foundEqual = false;

            foreach(int[] usedNumber in usedNumbers)
            {
                foundEqual = true;
                for (int i = 0; i < primes.Length; i++)
                {
                    if (factorizedNumber[i] != usedNumber[i])
                    {
                        foundEqual = false;
                        break;
                    }
                }
                if (foundEqual)
                    return false;
            }
            return true;
        }
    }
}
