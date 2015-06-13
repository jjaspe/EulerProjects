using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteOutNumbers
{
    public class NumberWriter
    {
        string[] units ={"one","two","three","four","five","six","seven","eight","nine"},
                teens={"ten","eleven","twelve","thirteen","fourteen","fifteen","sixteen","seventeen","eighteen",
                        "nineteen"},
                tens = { "twenty","thirty","forty","fifty","sixty","seventy","eighty","ninety"};
        string hundred="hundred",and="and",thousand="thousand";

        public void writer()
        {

        }

        public string[] getBelow10()
        {
            return units;
        }

        public string[] getTeens()
        {
            return teens;
        }

        public string[] getTens()
        {
            string[] allTens=new string[tens.Length*(units.Length+1)];//+1 to count the ones that end in 0
            for(int i=0;i<tens.Length;i++)
            {
                allTens[i*(units.Length+1)]=tens[i];
                for(int j=0;j<units.Length;j++)
                {
                    allTens[i*(units.Length+1)+j+1]=tens[i]+units[j];
                }
            }
            return allTens;
        }

        public string[] getBelowOneHundred()
        {
            string[] allTens=getTens();
            string[] belowHundred = new string[allTens.Length + teens.Length + units.Length];
            
            for (int i = 0; i < units.Length; i++)
                belowHundred[i] = units[i];

            for (int i = 0; i < teens.Length;i++ )
                belowHundred[units.Length + i] = teens[i];

            for (int i = 0; i < allTens.Length; i++)
                belowHundred[i + units.Length + teens.Length] = allTens[i];

            return belowHundred;
        }

        public string[] getHundreds()
        {
            string[] allTens=getTens();
            int hundredLength=(allTens.Length + teens.Length + units.Length + 1);
            string[] allHundreds = new string[9*hundredLength];

            //Loop through hundreds
            for(int i=0;i<9;i++)
            {
                //Do hundred and units
                allHundreds[i * (hundredLength)]=units[i] + hundred;
                for (int j = 0; j < units.Length;j++ )
                {
                    allHundreds[i * (hundredLength) + j + 1] = units[i] + hundred + and + units[j];
                }

                //Do teens
                for (int j = 0; j < teens.Length;j++ )
                {
                    allHundreds[i * (hundredLength) + units.Length + 1 + j] = units[i] + hundred + and + teens[j];
                }

                //Loop through tens
                for (int j = 0; j < allTens.Length; j++)
                {
                    allHundreds[i * (hundredLength) + units.Length + 1 + teens.Length + j] = units[i] + hundred + and + allTens[j];
                }
            }
            return allHundreds;
        }

        public string getOneThousand()
        {
            return units[0] + thousand;
        }

        public void printStrings(string[] strings)
        {
            foreach (string s in strings)
                Console.WriteLine(s);
        }

        public int getLetterCount(string[] strings)
        {
            int sum = 0;
            foreach (string s in strings)
                sum += s.Length;
            return sum;
        }


    }
}
