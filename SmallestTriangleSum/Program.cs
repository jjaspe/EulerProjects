using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallestTriangleSum
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver mySolver = new Solver(ConsoleLogger) { myPrinter = ConsolePrinter};
            Console.WriteLine(mySolver.getSmallestSum(mySolver.fromTopFiller));
        }

        private static void ConsoleLogger(string s)
        {
            Console.WriteLine(s);
        }

        private static void ConsolePrinter(int z, int x, int height, double sum)
        {
            Console.WriteLine("Z:" + z + "X:" + x + "Height:" + height);
            Console.WriteLine("Value" + sum);
        }


    }
}
