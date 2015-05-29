using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadraticPrimes
{
    class Program
    {
        static void Main(string[] args)
        {
            QuadraticPrimesSolver solver = new QuadraticPrimesSolver();
            Result result=solver.solver(1000);
            Console.WriteLine("Product:"+result.product);
            Console.WriteLine("Count:" + result.count);
        }
    }
}
