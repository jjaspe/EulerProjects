using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundant
{
    public class AbundantSolver
    {
        public int max = 28123;

        /// <summary>
        /// 1)Get sums
        /// 2) Pick an abundant, add to all higher abundants
        /// </summary>
        /// <returns></returns>
        public double solve()
        {
            double[] sums = Util.getSumOfFactorsUpTo(max);
            bool[] abundantsAdded = new bool[max + 1];
            LinkedList<double> abundants = new LinkedList<double>();
            LinkedListNode<double> currentNode,runnerNode;
            double sum = 0;

            //FIll up list of abundants
            for (int i = 0; i < sums.Length; i++)
            {
                if (sums[i] > i)
                {
                    abundants.AddLast(i);
                }
            }

            currentNode=abundants.First;
            for (int i = 0; i < abundants.Count;i++ )
            {
                runnerNode=currentNode;
                for (int j = i; j < abundants.Count;j++ )
                {
                    if (runnerNode.Value + currentNode.Value > max)
                        break;
                    if(abundantsAdded[(int)currentNode.Value+(int)runnerNode.Value]==false)
                    {
                        abundantsAdded[(int)currentNode.Value + (int)runnerNode.Value] = true;
                        sum += currentNode.Value + runnerNode.Value;
                    }
                    runnerNode = runnerNode.Next;

                }
                currentNode = currentNode.Next;
            }

            return maxSum()-sum;
        }

        public double maxSum()
        {
            return Util.sumUpTo(max);
        }
    }
}
