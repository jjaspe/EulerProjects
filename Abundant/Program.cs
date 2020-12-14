using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundant
{
    class Program
    {
        static void Main(string[] args)
        {
            AbundantSolver solver = new AbundantSolver();
            Console.WriteLine(solver.solve());
        }
    }
}
