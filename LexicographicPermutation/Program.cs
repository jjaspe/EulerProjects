using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicographicPermutation
{
    class Program
    {
        static void Main(string[] args)
        {
            LexiPermutationSolver solver = new LexiPermutationSolver();
            solver.perm = 999999;
            solver.digits = new bool[10];
            Console.WriteLine(solver.solve());
        }
    }
}
