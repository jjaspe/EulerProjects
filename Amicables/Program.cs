using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amicables
{
    class Program
    {
        static void Main(string[] args)
        {
            SumOfAmicablesSolver solver = new SumOfAmicablesSolver();
            Console.Write(solver.solve(10000));
        }
    }
}
