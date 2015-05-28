using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadraticPrimes
{
    public class Result
    {
        public int a, b, count;
        public int product
        {
            get { return a * b; }
        }
        public Result(int a, int b, int count)
        {
            this.a = a;
            this.b = b;
            this.count = count;
        }
    }

    public class QuadraticPrimesSolver
    {
        public int max = 1000;

        public UtilArray getFormulaValues(int a, int b)
        {
            UtilArray values = new UtilArray(4 * max * max);

            for (int i = 0; i < max; i++)
            {
                values[i] = i * i + a * i + b;
            }

            return values;
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">spot of p in primes</param>
        /// <param name="primes">list of allowed primes</param>
        /// <param name="isPrime">isPrime[i]=true if i is prime</param>
        /// <returns></returns>
        public Result getBestProductForPrime(int index,int[] primes,bool[] isPrime,int max)
        {            
            int i, best = 0,bestProduct=0;
            int coefB = primes[index];
            int diff=0,nextPrimeInSequence;
            for(int j=index+1;j<=max;j++)
            {
                i = 0;
                //Check positive a values first
                diff = primes[j] - primes[index];//positive difference

                //Since primes[j]-primes[index]=1+a,a=diff-1
                do
                {
                    nextPrimeInSequence = coefB + getNextInSequence(++i, diff - 1);
                    if (nextPrimeInSequence > 0 && !isPrime[nextPrimeInSequence])
                        break;
                    if (nextPrimeInSequence < 0 && !isPrime[-nextPrimeInSequence])
                        break;
                } while (true);

                if (best < i)
                {
                    bestProduct = (diff - 1) * coefB;//a*b
                    best = i;
                }
            }

            for (int j = index -1; j >=0; j--)
            {
                i = 0;
                //Check negative a values now
                diff = primes[j] - primes[index];//negative difference
                //Since primes[j]-primes[index]=1+a,a=diff-1
                do
                {
                    nextPrimeInSequence = coefB + getNextInSequence(++i, diff - 1);
                    if (nextPrimeInSequence > 0 && !isPrime[nextPrimeInSequence])
                        break;
                    if (nextPrimeInSequence < 0 && !isPrime[-nextPrimeInSequence])
                        break;
                } while (true);

                if (best < i)
                {
                    bestProduct = (diff - 1) * coefB;//a*b
                    best = i;
                }
            }

            return new Result(diff -1, coefB,best);
        }

        public int getNextInSequence(int index,int a)
        {
            return index * index + index * a;
        }

        //b must be prime for n=0 to work. Also b|a, then a=mb,
        
        public Result solver(int n)
        {
            int[] primes=Util.getPrimes(2*n*n+n);
            bool[] isPrime=new bool[2*n*n+n];
            int avIndex = 0;
            for (int i = 0; i < primes.Length; i++)
            {
                isPrime[i] = true;
                if (primes[i] > n && avIndex == 0)
                    avIndex = i - 1;
            }
               
            //Run through all possible primes, get the best product for each
            Result current,bestResult=new Result(1,2,1);
            for (int i = 0; i < avIndex;i++ )
            {
                //Get positive
                current = getBestProductForPrime(i, primes, isPrime,avIndex);
                if (current.count > bestResult.count)
                    bestResult = current;
            }

            return bestResult;
        }
    }
}
