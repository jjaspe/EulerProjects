using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteOutNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberWriter writer = new NumberWriter();
            //writer.printStrings(writer.getBelowOneHundred());
            int below = writer.getLetterCount(writer.getBelowOneHundred()),
                above=writer.getLetterCount(writer.getHundreds()),
                thous=writer.getOneThousand().Length;
            Console.WriteLine(below + above + thous);

        }
    }
}
