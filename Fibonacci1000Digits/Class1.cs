using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci1000Digits
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            Fibonnacci1000Solver solver = new Fibonnacci1000Solver();
            Console.WriteLine(solver.solve(1000));
        }
    }
}
