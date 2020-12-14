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
            UtilArray values = new UtilArray(2*b);

            for (int i = -b+1; i < b; i++)
            {
                values[i] = i * i + a * i + b;
            }

            return values;
        }

        public int getValue(int a,int b,int n)
        {
            return n * n + a * n + b;
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

        /// <summary>
        /// Also if a is even, 1^2+1*a+b=even, so a must be odd.            
        /// </summary>
        /// <param name="index"></param>
        /// <param name="primes"></param>
        /// <param name="isPrime"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Result getBestProductBruteForce(int index,int[] primes,bool[] isPrime,int max)
        {
            int n=0, bestSequenceCount = 0, bestProduct = 0;
            int b = primes[index],value;

            //Run through all a's
            for(int a=-max+1;a<max-1;a+=2)
            {
                n = 0;
                if (a == -b)
                    continue;
                do
                {                    
                    //Get formula value and check if prime
                    value = getValue(a, b, n++);
                    if (value > 0 && !isPrime[value])
                        break;
                    if (value < 0 && !isPrime[-value])
                        break;
                } while (n<b);

                //Update best if needed
                if (bestSequenceCount < n)
                {
                    bestProduct = a * b;
                    bestSequenceCount = n;   
                }
            }

            return new Result(bestProduct/b, b, bestSequenceCount);
        }

        public int getNextInSequence(int index,int a)
        {
            return index * index + index * a;
        }

        //b must be prime for n=0 to work. Also if a is even, 1^2+1*a+b=even, so a must be odd.
        
        public Result solver(int n)
        {
            int max = 2 * n * n + n;
            int[] primes=Util.getPrimes(2*max);
            bool[] isPrime=new bool[2*max];
            int indexOfLastPrime = 0,indexOfLastB=0;
            for (int i = 0; i < primes.Length; i++)
            {
                isPrime[primes[i]] = true;
                if (primes[i] > n && indexOfLastB==0)
                    indexOfLastB = i - 1;
                if (primes[i] > max)
                {
                    break;
                }                    
            }
               
            //Run through all possible primes, get the best product for each
            Result current,bestResult=new Result(1,2,1);
            for (int i = 0; i<=indexOfLastB;i++ )
            {
                //Get positive
                current = getBestProductBruteForce(i, primes, isPrime,n);
                if (current.count > bestResult.count)
                    bestResult = current;
            }

            return bestResult;
        }
    }
}
