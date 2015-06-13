using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SundaysOnTheFirst
{
    public class SundaysOnTheFirstSolver
    {
        public class FirstAndSunday
        {
            public bool first = false,
                sunday=false;
        }

        //36524
        FirstAndSunday[] day = new FirstAndSunday[36526];
        int[] daysInMonths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public int solve()
        {
            //Assign firsts
            day[0] = new FirstAndSunday() { first = true };
            int counter=0;
            for(int years=1;years<=100;years++)
            {
                for(int i=0;i<daysInMonths.Length;i++)
                {
                    if (i == 1 && years%4  == 0)
                        counter += 29;
                    else
                        counter += daysInMonths[i];
                    day[counter]=new FirstAndSunday() { first = true };
                }
            }

            int matches = 0;

            //Get starting day
            int d = 366%7;
            

            //Assign sundays, record matches

            for (int i = 7-d; i < day.Length; i += 7)
            {
                //day[counter].sunday = true;
                if (day[i] != null && day[i].first)
                    matches++;
            }

            return matches;
            

        }
    }
}
