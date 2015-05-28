using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerDigitSum
{
    class Program
    {
        static PowerDigitSolver solver = new PowerDigitSolver();
        static void Main(string[] args)
        {
            print();
        }

        static void print()
        {
            solver.factor1 = 25;
            solver.factor2 =4;
            solver.factor3 = 10;
            Console.WriteLine(solver.solveAdd());
        }
    }
}
