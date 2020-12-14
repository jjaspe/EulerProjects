
using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PascalsTriangleDivisibleBy7Problem149
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver MySolver = new Solver(IOUtil.ConsolePrinter);
            MySolver.getAllNotDivisibles();
        }

        /// <summary>
        /// Goal: Get number of numbers in pascal's triangle of size 1Billion that are not divisible by 7
        /// Strat 1: Generate line by using previous, count not divisibles (Too slow)
        /// Strat 2: Generate numbers using combinatorics, check prime factorizations for number of 7s
        /// </summary>
        public class Solver
        {
            int size = 100;
            int numberToCheck = 7;
            List<double> previousLine=new List<double>();
            Logger MyLogger;

            public Solver(Logger logger)
            {
                MyLogger = logger;
            }

            public double getAllNotDivisibles()
            {
                previousLine = new List<double> { 1,1 };
                double notDivisibles = 3;//top lines
                for(int i=2;i<=size;i++)
                {
                    if(i%(size/100)==0)
                    {
                        MyLogger("Line number:" + i);
                        MyLogger("Not Divisibles So Far:" + notDivisibles);
                    }
                    
                    notDivisibles += getNotDivisiblesForLineWithNumberStrat2(i);
                }
                return notDivisibles;
            }

            private double getNotDivisiblesForLineWithNumberStrat2(int lineNumber)
            {
                return getNewLineUsingCombinations(lineNumber);
            }

            /// <summary>
            /// 1)Create new line
            /// 2)Add one to start
            /// 3)Create other elements by using previous line
            /// 4)Add one to end
            /// 5)Check divisibles
            /// 6)Switch previous line with new one
            /// </summary>
            /// <param name="lineNumber"></param>
            /// <returns></returns>
            public double getNotDivisiblesForLineWithNumberStrat1(int lineNumber)
            {
                List<double> newLine = new List<double>();
                double notDivisibles = getNewLineByAddingPreviousLine(newLine,lineNumber);
                return notDivisibles;
            }

            private double getNewLineUsingCombinations(int lineNumber)
            {
                double currentRemainder = 0;
                int log=(int)Math.Log(lineNumber,numberToCheck);
                double notDivisibles=0;
                currentRemainder = lineNumber % Math.Pow(numberToCheck, log);
                notDivisibles = (currentRemainder+1) * Math.Floor(lineNumber / Math.Pow(numberToCheck, log))+currentRemainder;
                return notDivisibles;
            }


            private double getNewLineByAddingPreviousLine(List<double> newLine, int lineNumber)
            {
                double previousSize = lineNumber-1;
                //Start at 2 because of 1's at start and end
                double notDivisibles = 2, current=-1;

                newLine.Add(1);
                for (int i = 0; i < previousSize / 2; i++)
                {
                    current = previousLine[i] + previousLine[i + 1];
                    newLine.Add(current);
                    if (current % numberToCheck != 0)
                        notDivisibles+=2;
                }
                if(previousSize%2==1)
                {
                    //If middle counted for sum, it should only count once but was counted twice so remove one
                    if (current % numberToCheck != 0)
                        notDivisibles--;
                }
                else
                {
                    newLine.Add(current);
                }

                previousLine = newLine;
                return notDivisibles;
            }
        }
    }
}
