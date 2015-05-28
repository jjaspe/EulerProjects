using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerMisc
{
    public class UtilArray
    {
        public int[] myArray;
        public UtilArray(int length)
        {
            myArray = new int[length];
        }

        public int this[int i]
        {
            get
            {
                return myArray[i];
            }
            set
            {
                myArray[i] = value;
            }
        }

        public int Length
        {
            get
            {
                return myArray.Length;
            }
        }

        public UtilArray trimStart()
        {
            myArray = Util.trimStart(myArray);
            return this;
        }
        public UtilArray trimEnd()
        {
            myArray = Util.trimEnd(myArray);
            return this;
        }
        public UtilArray reverse()
        {
            myArray = Util.reverse(myArray);
            return this;
        }

        public UtilArray trimAny(int t)
        {
            int[] array = new int[myArray.Length];
            int count=0;
            for(int i=0;i<myArray.Length;i++)
            {
                if (myArray[i] != t)
                    array[count++] = myArray[i];
            }
            if(t!=0)
            {
                for(int i=count;i<array.Length;i++)
                {
                    array[i] = t;
                }
            }
            myArray = Util.trimEnd(array,t);
            return this;
        }

        public string toString()
        {
            return Util.getIntArrayAsString(myArray);
        }
    }
}
