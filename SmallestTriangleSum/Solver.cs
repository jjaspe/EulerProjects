using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallestTriangleSum
{
    public delegate double[,] triangleFiller(double[,] grid,int baseSize);
    public delegate void printer(int z,int x,int height,double sum);
    public delegate void logger(string s);

    public class TriangleSum
    {
        public int X;
        public int Z;
        public int Height;
        public double Value;
    }

    public class Solver
    {
        double next = 0;
        double[,] triangleSums;
        double[,] lineSums;
        double[,] grid;
        int size = 1000;
        int doubleSize;
        public printer myPrinter;
        public logger myLogger;
        double[] Sequence=new double[]{15,-14,-7,20,-13,-5,-3,8,
        23,-26,1,-4,-5,-18,5,-16,31,2,9,28,3};

        public Solver(logger logger)
        {
            myLogger = logger;
            doubleSize = 2 * size;
            grid = new double[size, doubleSize];
            triangleSums = new double[size, doubleSize];
            lineSums = new double[size, doubleSize];
            myLogger("Done initializing grid");
        }

        double getNextRandom()
        {
            double result = next;
            if (result % 2 == 1)
                result *= -1;
            next++;
            return result;
        }

        double getNextPseudoRandom()
        {
            next = (615949 * next + 797807) % Math.Pow(2, 20);
            return next - Math.Pow(2, 19);
        }

        double getSequence()
        {
            return Sequence[(int)next++];
        }

        public double[,] myFiller(double[,] grid,int baseSize)
        {
            for(int i=0;i<baseSize;i++)
            {
                for(int j=i;j<2*baseSize-i;j+=2)
                {
                    grid[i, j] = getNextPseudoRandom();
                }
            }
            return grid;
        }

        public double[,] fromTopFiller(double[,] grid,int baseSize)
        {
            double current = 0;
            for(int i=baseSize-1;i>=0;i--)
            {
                for(int j=i;j<2*baseSize-i;j+=2)
                {
                    current=getNextPseudoRandom();
                    grid[i, j] = current;
                }
            }
            return grid;
        }

        public double getSmallestSum(triangleFiller filler)
        {
            TriangleSum result, smallest = new TriangleSum() { Value = 1000000 };
            grid = filler(grid, size);
            myLogger("Done creating grid");
            myLogger("Checking height" + 0);
            result = getSmallestWithSizeI(grid, 0);
            for(int i=1;i<size;i++)
            {
                myLogger("Checking height" + i);
                result=getSmallestWithSizeI(grid, i);
                smallest = smallest.Value <= result.Value ? smallest : result;

                myPrinter(smallest.X, smallest.Z, smallest.Height, smallest.Value);
            }
            return smallest.Value;
        }

        private TriangleSum getSmallestWithSizeI(double[,] grid, int i)
        {
            double current=100000;
            TriangleSum smallest = new TriangleSum() { Value = 100000,Height=i };
            double[,] newTriangleSums = new double[size, doubleSize],
                newLineSums=new double[size,doubleSize];
            for(int z=0;z<size;z++)
            {
                for(int x=z;x<doubleSize-z;x+=2)
                {
                    if(i==0)
                    {
                        current=newTriangleSums[z, x] = grid[z, x];
                        newLineSums[z, x ] = grid[z, x];
                    }
                    else
                    {
                        if(z+i<size && x+2*i<doubleSize)
                            newLineSums[z, x+i*2] = lineSums[z, x+i*2 ] + grid[z + i, x + i];
                        if(x<doubleSize-z-2*i)
                            current = newTriangleSums[z, x ] = triangleSums[z, x ] + newLineSums[z, x+2*i ];
                        
                    }
                    if(smallest.Value>current)
                    {
                        smallest.X=x;
                        smallest.Z=z;
                        smallest.Value=current;
                    }
                }
            }
            triangleSums = newTriangleSums;
            lineSums = newLineSums;
            return smallest;
        }

        private double getSumOfSide(int height, int x, int i,double[,] grid)
        {
            double sum = 0;
            for(int z=0;z<=i;z++)
            {
                sum += grid[height+z, x - z];
            }
            return sum;
        }


    }
}
