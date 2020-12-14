using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProjects
{
    class Program
    {

        static void Main(string[] args)
        {
            LargestProductSolver solver = new LargestProductSolver();
            Console.WriteLine("Product:" + solver.solve());
            int i = solver.largest.index / solver.size, j = solver.largest.index % solver.size;
            Console.WriteLine("Location:" + i + "," + j);
            string type="";
            switch(solver.largest.type)
            {
                case 0:
                    type = "Horizontal";
                    break;
                case 1:
                    type = "Vertical";
                    break;
                case 2:
                    type = "Diagonal Down";
                    break;
                case 3:
                    type = "Diagonal Up";
                    break;
            }
            Console.WriteLine("Type:" + type);

        }

        
    }
}
