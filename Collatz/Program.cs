using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collatz
{
    class Program
    {
        static void Main(string[] args)
        {
            CollatzSolver solver = new CollatzSolver();
            solver.solve(1000000);

            //Util.printIntArray(solver.sequences);
            
            solver.printCollatz(Util.findIndexOfMax(solver.sequences));
            Console.WriteLine(Util.findMax(solver.sequences));
            Console.WriteLine(Util.findIndexOfMax(solver.sequences));
            //solver.printCollatz(837799);
        }
    }
}
