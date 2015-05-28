using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuinticSumsProblem30
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver mySolver = new Solver();
            Console.WriteLine(mySolver.findSumOfQuintics());
        }
    }
}
