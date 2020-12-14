using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerMisc
{
    public delegate void Logger(string s);
    public class IOUtil
    {
        public static void ConsolePrinter(string s)
        {
            Console.WriteLine(s);
        }
    }
}
