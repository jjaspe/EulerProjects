using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerCombinationsProblem29
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver mySolver = new Solver();
            mySolver.myLogger = IOUtil.ConsolePrinter;
            Console.WriteLine(mySolver.solve());
        }
    }
}
