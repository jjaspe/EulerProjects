using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximumPathSum1
{
    class Program
    {
        static void Main(string[] args)
        {
            MaximumPathSumSolver solver = new MaximumPathSumSolver();
            solver.init();
            Console.WriteLine(solver.maxPath(solver.solve(new Spot(0, 0))).sum);

        }
    }
}
