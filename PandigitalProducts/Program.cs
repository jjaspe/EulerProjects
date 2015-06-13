using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PandigitalProducts
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver mySolver = new Solver();
            Console.WriteLine(mySolver.solve());
        }
    }
}
