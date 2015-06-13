using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names
{
    class Program
    {
        static void Main(string[] args)
        {
            NamesSolver solver = new NamesSolver();
            Console.WriteLine(solver.solve());
        }
    }
}
