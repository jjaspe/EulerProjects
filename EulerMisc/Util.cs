using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerMisc
{

    public class Util
    {
        public static double factorial(int n)
        {
            if (n <=1)
                return 1;
            double f = 1;
            for (int i = 2; i <= n; i++)
                f *= i;
            return f;
        }

        public static double partFactorial(int n, int r)
        {
            if (r > n || r<=0)
                return 1;

            double f = n;
            for (int i = n - 1; i > r; i--)
                f *= i;
            return f;
        }

        public static double nChooser(int n,int r)
        {
            if (r >= n)
                return 1;
            if (r == 0)
                return 1;
            double top = partFactorial(n,r), bot = factorial(r);
            return top /bot;
        }

        public static double sumUpTo(int n)
        {
            return (n * (n + 1) / 2);
        }

        public static List<int> GetDigits(double n)
        {
            List<int> digits = new List<int>();
            while(n>1)
            {
                digits.Add(((int)(n % 10)));
                n /= 10;
            }
            return digits;
        }

        public static int GetNumberFromDigits(List<int> perm)
        {
            int value = 0;
            for(int i=0;i<perm.Count;i++)
            {
                value = value * 10 + perm[i];
            }
            return value;
        }

        public static List<List<int>> GetPermutationsOfDigits(List<int> digits)
        {
            List<List<int>> perms = getPermutationsRecursive(digits);
            return perms;
        }

        static List<List<int>> getPermutationsRecursive(List<int> available)
        {
            int front;
            List<List<int>> currentPerms = new List<List<int>>(), myPerms = new List<List<int>>() ;
            List<int> copy;
            //Go throuhg all available numbers, take one out each time, get all possible permutations of remaining, 
            //stick the taken value to the front of those permutations
            //When we are down to two values, just return those two permutations
            if(available.Count==2)
            {
                List<int> permOne =new List<int> () { available[0], available[1] }, 
                    permTwo = new List<int>(){ available[1], available[0] };
                return new List<List<int>>(){permOne,permTwo};
            }

            if(available.Count==1)
                return new List<List<int>>() { new List<int>() {available[0]}};

            for(int i=0;i<available.Count;i++)
            {                
                front = available[i];
                copy = copyList(available);
                copy.RemoveAt(i);
                currentPerms = getPermutationsRecursive(copy);
                foreach(List<int> perm in currentPerms)
                {
                    List<int> newPerm = new List<int>() { front };
                    newPerm.AddRange(perm);
                    myPerms.Add(newPerm);
                }
            }
            return myPerms;
        }

        static List<int> copyList(List<int> original)
        {
            List<int> copy = new List<int>();
            foreach (int i in original)
                copy.Add(i);
            return copy;
        }
        #region PRIMES
        public static int[] getPrimes(int n)
        {
            LinkedList<int> primes = new LinkedList<int>();
            primes.AddFirst(2);
            for(int i=3;i<=n;i++)
            {
                if(isPrime(i,primes))
                {
                    primes.AddLast(i);
                }
            }

            return primes.ToArray<int>();
        }

        public static bool isPrime(int n,LinkedList<int> primes)
        {
            foreach(int i in primes)
            {
                if (i * i > n)
                    return true;
                if (n % i == 0)
                    return false;
            }
            return true;
        }
        #endregion

        #region FIBONNACI
        public static UtilArray getNextBigFibonnacci(UtilArray prev1,UtilArray prev2)
        {
            UtilArray newArray = new UtilArray(0);
            newArray.myArray= addArrays(prev1.myArray,prev2.myArray);
            return newArray;
        }
        
        #endregion

        #region FACTORS

        public class FactorTuple
        {
            public double factor1, factor2;
        }

        /// <summary>
        /// Returns an array where array[i]=number of divisors of i.
        /// if n=p1^a1*p2^a2....
        /// then euler=(a1+1)(a2+1)...
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[] getEulerFunction(int n)
        {
            int[] primes = getPrimes(n);
            double sum = 1;
            double[] euler=new double[n+1];
            euler[0]=1;
            euler[1]=1;
            UtilArray[] primeFact;

            for (int i = 2; i <=n;i++)
            {
                sum = 1;
                primeFact = getPrimeFactorization(i, primes);
                for (int j = 0; j < primeFact[0].Length; j++)
                {
                    sum *= (primeFact[1][j] + 1);
                }
                euler[i] = sum;
            }
                
            return euler;
        }

        public static double[] getSumOfFactorsUpTo(int n)
        {
            int[] primes=getPrimes(n);
            //Dynamic programming part
            //We will keep a list of the SumOfPrimePowers already worked out
            //Since 2^(log(n,2)+1)>max, using we use log(n,2)+2 as the highest power will work for all 
            //primes.
            //Every time we work one out we save it, and we send it to the 
            //function to make it faster.
            int maxPower=(int)Math.Log(n,2)+2;
            double[,] workedOut=new double[primes.Length,maxPower];
            UtilArray[] primeFact;
            double[] sums = new double[n + 1];
            sums[0] = 0;
            sums[1] = 1;
            for(int i=2;i<=n;i++)
            {
                primeFact = Util.getPrimeFactorizationInPrimesIndexes(i, primes);
                sums[i] = getSumOfFactors(primeFact, workedOut,primes) - i;
            }

            return sums;
            
        }

        public static double getSumOfFactors(UtilArray[] primeFact,double[,] workedOut,int[] primes)
        {
            double sum = 1;
            for(int i=0;i<primeFact[0].Length;i++)
            {
                if(workedOut[primeFact[0][i],primeFact[1][i]]==0)//Not saved
                {
                    //Save 
                    workedOut[primeFact[0][i],primeFact[1][i]]= 
                        getSumOfPrimePowers(primes[primeFact[0][i]], primeFact[1][i]);
                }
                sum *= workedOut[primeFact[0][i], primeFact[1][i]];                
            }
            return sum;
        }

        public static double getSumOfPrimePowers(int prime,int power)
        {
            return (Math.Pow(prime, power + 1) - 1) / (prime - 1);
        }
        /// <summary>
        /// Returns the prime factorization of n the following way:
        /// UtilArray[0] contains the indexes of the primes in primes array
        /// UtilArray[1] contains the power of each prime
        /// so if UtilArray[0][2]=3, and UtilArray[1][2]=2, then n is divisible by
        /// primes[3]^2;
        /// </summary>
        /// <param name="n"></param>
        /// <param name="primes"></param>
        /// <returns></returns>
        public static UtilArray[] getPrimeFactorizationInPrimesIndexes(int n, int[] primes)
        {
            UtilArray[] primeFact = new UtilArray[2];// primes.Length];
            int powerCount = 0;
            primeFact[0] = new UtilArray(primes.Length);
            primeFact[1] = new UtilArray(primes.Length);

            for (int i = 0; i < primes.Length; i++)
            {
                powerCount = 0;
                while (n % primes[i] == 0)
                {
                    n /= primes[i];
                    powerCount++;
                }

                if (powerCount > 0)//Keep at -1 for unset so we can trim
                    primeFact[0][i] = i;
                else
                    primeFact[0][i] = -1;
                primeFact[1][i] = powerCount;
            }
            primeFact[0].trimAny(-1);
            primeFact[1].trimAny(0);
            return primeFact;
        }

        public static UtilArray[] getPrimeFactorization(int n,int[] primes)
        {
            UtilArray[] primeFact = new UtilArray[2];// primes.Length];
            int powerCount = 0;
            primeFact[0] = new UtilArray(primes.Length);
            primeFact[1] = new UtilArray(primes.Length);

            for(int i=0;i<primes.Length;i++)
            {
                powerCount = 0;
                while(n%primes[i]==0)
                {
                    n /= primes[i];
                    powerCount++;
                }

                if(powerCount>0)//Keep at zero for unset so we can trim
                    primeFact[0][i] = primes[i];
                primeFact[1][i] = powerCount;                
            }
            primeFact[0].trimAny(0);
            primeFact[1].trimAny(0);
            return primeFact;
        }

        public static List<FactorTuple> getFactorTuples(double value)
        {
            List<FactorTuple> tuples = new List<FactorTuple>();
            for(int i=2;i*i<value;i++)
            {
                if (value % i == 0)
                    tuples.Add(new FactorTuple() { factor1 = i, factor2 = value / i });
            }
            return tuples;
        }

        #endregion

        #region LARGE NUMBER MANIP
        /// <summary>
        /// Creates an double array, where ith row represents the decimal representation
        /// of the ith element of bas, with the least significant at [i,0]
        /// </summary>
        /// <param name="bas"></param>
        /// <returns></returns>
        public static int[,] getTuple(double[] bas)
        {
            double largest = Util.findMaxD(bas);
            //Get size of largest base 10,thats how many digits it has
            int tupleLength = (int)Math.Log10(largest) + 1;
            int[,] tuples = new int[bas.Length, tupleLength];

            //Split elements into their digits
            double current = 0;
            for (int i = 0; i < bas.Length; i++)
            {
                current = bas[i];
                for (int j = 0; j < tupleLength; j++)
                {
                    tuples[i, j] = (int)current % 10;
                    current = (current - current % 10) / 10;
                }
            }
            return tuples;
        }

        /// <summary>
        /// Takes a tuple array and returns decimal representation of number
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        static public int[] combineTuple(int[,] tuple, int count)
        {
            int tupleLength = tuple.Length / count;
            int[] number = new int[count + tupleLength - 1];

            int i, sum, carry = 0;

            //Get all up to count
            for (i = 0; i < count; i++)
            {
                sum = carry;
                carry = 0;
                for (int j = 0; j < tupleLength; j++)
                {
                    if (i - j > -1)
                    {
                        sum += tuple[i - j, j];
                    }
                }
                number[i] = sum % 10;
                carry = (sum - sum % 10) / 10;
            }


            //Get next tupleLength-1
            //i should be at i=count, so we go from i to count+tupleLength-1 on dest array,            
            for (i = count; i < count + tupleLength - 1; i++)
            {
                sum = carry;
                //Get second from top, third from next to top, etc
                for (int j = i-count+1; j < tupleLength; j++)
                {
                    if (i - j < 0)
                        break;
                    sum += tuple[i - j, j];
                }
                number[i] = sum % 10;
                carry = (sum - sum % 10) / 10;
            }

            //If carry is over 0, get decimal representation,and append to array.
            if (carry > 0)
            {
                int[] carryArray = toDecimalArray(carry);
                number = appendArrays(number, carryArray);
            }

            return number;

        }

        static public int[] appendArrays(int[] a,int[] b)
        {
            int[] c = new int[a.Length + b.Length];
            for(int i=0;i<a.Length;i++)
            {
                c[i] = a[i];
            }
            for(int i=a.Length;i<a.Length+b.Length;i++)
            {
                c[i] = b[i - a.Length];
            }
            return c;
        }

        /// <summary>
        /// least significant in at 0, most significant at array[length-1]
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int[] toDecimalArray(double i)
        {
            List<int> ints = new List<int>();
            while(i>0)
            {
                ints.Add((int)i % 10);
                i = ((int)i)/10;
            }
            return ints.ToArray();
        }

        /// <summary>
        /// Returns an array the ith element = bas[i]^pow
        /// </summary>
        /// <param name="bas"></param>
        /// <param name="pow"></param>
        /// <returns></returns>
        public static double[] getPowerDecimal(int[] bas, int pow)
        {
            double[] dArray = new double[bas.Length];
            
            for(int i=0;i<bas.Length;i++)
            {
                dArray[i] = bas[i];
            }

            //Power loop
            double current;
            for (int i = 0; i < pow - 1; i++)
            {
                                
                //Elements of array loop
                for (int m = 0; m < dArray.Length; m++)
                {
                    current = dArray[m];
                    int deci = 1;
                    //Decimals loop for multiplication
                    for (int j = 0; j < bas.Length; j++)
                    {                        
                        //addition loop
                        for (int k = 0; k < bas[j] * deci; k++)
                        {
                            dArray[m] += current;
                        }
                        deci *= 10;
                    }

                    //Subtract once to account for fact that it was already there once
                    dArray[m] -= current;
                    
                }
            }

            

            return dArray;
        }

        /// <summary>
        /// Multiplies a number using decimal representation by a digit (0-9).
        /// Returns an array one element longer than the parameter one
        /// </summary>
        /// <param name="number"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        public static int[] multiplyByDigit(int[] number,int digit)
        {
            int[] newNumber = new int[number.Length+1];
            int current,carry=0;
            for(int i=0;i<number.Length;i++)
            {
                current = number[i] * digit+carry;
                carry = (current - current % 10) / 10;
                newNumber[i] = current%10;
            }
            if (carry > 0)
                newNumber[newNumber.Length - 1] = carry;
            return newNumber;
        }

        public static int[] addArrays(int[] a1,int[] a2)
        {
            int[] newNumber;
            int carry = 0,current=0;
            if(a1.Length>=a2.Length)
            {
                newNumber = new int[a1.Length + 1];                
                
                for(int i=0;i<a1.Length;i++)
                {
                    current = a1[i]+carry;
                    if(i<a2.Length)
                    {
                        current = current + a2[i];                        
                    }
                    newNumber[i] = current % 10;
                    carry = (current) / 10;
                }
                newNumber[newNumber.Length - 1] = carry;
            }else
            {
                newNumber = new int[a2.Length + 1];

                for (int i = 0; i < a2.Length; i++)
                {
                    current = a2[i] + carry;
                    if (i < a1.Length)
                    {
                        current = current + a1[i];
                    }
                    newNumber[i] = current % 10;
                    carry = (current) / 10;
                }
                newNumber[newNumber.Length - 1] = carry;
            }
            return newNumber;
        }

        public static int[] multiplyArrays(int[] a1,int[] a2)
        {
            int[] newNumber = new int[a1.Length + a2.Length],
                current=new int[a1.Length+a2.Length-1];
            int[] sum=new int[a2.Length+a1.Length-1];
            
            for(int i=0;i<a2.Length;i++)
            {
                current = new int[a2.Length + a1.Length-1];
                //Initialize current
                for(int j=0;j<a2.Length+a1.Length-1;j++)
                {
                    if (j-i < a1.Length && j>=i)
                        current[j] = a1[j-i];
                    else
                        current[j] = 0;
                }
                //Multiply by a2 digit
                current = multiplyByDigit(current, a2[i]);
                sum = addArrays(sum, current);
            }
            return trimEnd(sum);            
        }

        public static double addDigits(int[] number)
        {
            double sum = 0;
            for (int i = 0; i <number.Length ; i++)
            {
                sum += number[i];
            }
            return sum;
        }

        public static int[] addDigitsAsArray(int[] number)
        {
            int[] sum={number[0]};
            for(int i=1;i<number.Length;i++)
            {
                sum = addArrays(sum, new int[] { number[i] });
                sum = Util.trimEnd(sum);
            }
            return sum;
        }
        #endregion

        #region ARRAY MANIP
        public static int[] reverse(int [] array)
        {
            int[] reverse = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                reverse[reverse.Length - i - 1] = array[i];
            }
            return reverse;
        }
        public static int[] trimStart(int [] array,int number=0)
        {
            for (int i = 0; i <array.Length; i++)
            {
                if (array[i] != 0)
                {
                    if (i == 0)
                        return array;
                    int[] trimmed = new int[array.Length-i];
                    //copy from i up
                    for (int j = i; j < array.Length; j++)
                    {
                        trimmed[j-i] = array[j];
                    }
                    return trimmed;
                }
            }
            return array;
        }
        public static int[] trimEnd(int [] array,int number=0)
        {
            for(int i=array.Length-1;i>=0;i--)
            {
                if (array[i] != number)
                {
                    int[] trimmed = new int[i+1];
                    //copy up to zero
                    for(int j=0;j<trimmed.Length;j++)
                    {
                        trimmed[j] = array[j];
                    }
                    return trimmed;
                }
            }
            return array;
        }

        public static int findMax(int[] numbers)
        {
            int max = -1;
            foreach (int i in numbers)
                max = max > i ? max : i;
            return max;
        }

        public static double findMaxD(double[] numbers)
        {
            double max = -1;
            foreach (double i in numbers)
                max = max > i ? max : i;
            return max;
        }

        public static int findIndexOfMax(int[] numbers)
        {
            int max = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                max = numbers[max] > numbers[i] ? max : i;
            }
            return max;
        }

        public static void printIntArray(int[] array)
        {
            foreach (int i in array)
                Console.WriteLine(i + ",");
        }

        public static string getIntArrayAsString(int[] array)
        {
            string s = "";
            foreach (int i in array)
                s += i.ToString();
            return s;
        }

        public static bool arraysEqual(int[] a1, int[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i])
                    return false;
            }
            return true;
        }

        public static bool arraysEqual(double[] a1, double[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i])
                    return false;
            }
            return true;
        }

        #endregion

        public static string[][] parseStrings(string[]rows,char spaceChar=' ')
        {
            string[][] parsedStrings = new string[rows.Length][];
            for (int i = 0; i < rows.Length; i++)
            {
                parsedStrings[i] = rows[i].Split(spaceChar);
            }
            return parsedStrings;
        }

        #region INT GRID 

        /// <summary>
        /// ASSUMES EACH ROW HAS 2 MORE ELEMENTS THAN PREVIOUS
        /// Create a grid of strings with width being the width of the longests
        /// string, and with length being the number of strings. Strings will get centered
        /// when they are put into the grid, and padded with -1.
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static  int[][] toIntGrid(string[] rows)
        {

            string[][] parsedStrings = parseStrings(rows);

            int nRows = rows[rows.Length - 1].Length,
                nCols = rows.Length,
                gap = 0;
            int[][] grid = new int[nRows][];

            for (int i = 0; i < nRows; i++)
            {
                grid[i] = new int[nCols];
                gap = (nCols - 1) / 2 - 2 * i;
                for (int j = 0; j < nCols; j++)
                {
                    //fill length-1/2 -2*i left and right with -1
                    if (i < gap || i > (gap + 2 * i + 1))
                        grid[i][j] = -1;
                    else
                        grid[i][j] = Int32.Parse(parsedStrings[i][j]);
                }
            }

            return grid;
        }

        public static void printGrid(int[][] grid, int width)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    Console.Write("{0:00} ", grid[i][j]);
                }
                Console.WriteLine(" ");
            }
        }

        #endregion

        public static string[] fileParser(string filename)
        {
            StreamReader r = new StreamReader(filename);

            string[] strings = r.ReadToEnd().Split(',');
            return strings;          

        }

        public static bool[] GetPrimesBoolArray(int[] p)
        {
            int length = p[p.Length - 1];
            int decimalPlaces = (int)Math.Log10(length)+1;
            bool[] primeBools = new bool[(int)Math.Pow(10,decimalPlaces)];
            for(int i=0;i<p.Length;i++)
            {
                primeBools[p[i]] = true;
            }
            return primeBools;
        }



        
    }
}
