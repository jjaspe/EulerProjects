using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Names
{
    public class NamesSolver
    {
        public string[] removeGarbage(string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                names[i]=names[i].Substring(1,names[i].Length-2);
            }
            return names;
        }

        public int sComparer(string s1,string s2)
        {
            return s1.CompareTo(s2);
        }

        public double solve()
        {
            string[] names = Util.fileParser("../../../files/p022_names.txt");
            names = removeGarbage(names);
            List<string> nameList = names.ToList<string>();
            nameList.Sort(sComparer);
            names=nameList.ToArray<string>();

            int[] values = new int[names.Length];
            double sum = 0;
            for(int i=0;i<names.Length;i++)
            {
                values[i] = (i+1) * getCharValueSum(names[i]);
                sum += values[i];
            }

            return sum;
            
        }

        public int getCharValueSum(string s)
        {
            int sum = 0;
            foreach (char c in s)
                sum += ((int)c-64);
            return sum;
        }


    }
}
