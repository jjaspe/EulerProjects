using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static void WatchedPrint(Func<double> method)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("Answer:" + method.Invoke());
            watch.Stop();
            Console.WriteLine("Ellapsed time in ms:" + watch.ElapsedMilliseconds);
        }
    }
}
