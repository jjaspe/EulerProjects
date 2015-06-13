using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecurringCycle
{
    public class Class1
    {
        static void Main(string[] args)
        {
            RecurringCycleSolver solver=new RecurringCycleSolver();
            Console.WriteLine(solver.solve(1000));
        }
    }
}
