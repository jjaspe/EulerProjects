using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinSums
{
    public class Solver
    {
        public int valueToMatch = 200;
        class Coin
        {
            public int maxNumber;
            public int value;
        }

        LinkedList<Coin> coins = new LinkedList<Coin>();

        public Solver()
        {
            createCoins();
        }

        void createCoins()
        {
            Coin oneCent = new Coin() { maxNumber = valueToMatch / 1, value = 1 },
                twoCents = new Coin() { maxNumber = valueToMatch / 2, value = 2 },
                fiveCents = new Coin() { maxNumber = valueToMatch / 5, value = 5 },
                tenCents = new Coin() { maxNumber = valueToMatch / 10, value = 10 },
                twentyCents = new Coin() { maxNumber = valueToMatch / 20, value = 20 },
                fiftyCents = new Coin() { maxNumber = valueToMatch / 50, value = 50 },
                onePound= new Coin() { maxNumber = valueToMatch / 100, value = 100 },
                twoPounds = new Coin() { maxNumber = valueToMatch / 200, value = 200 };
            coins.AddLast(twoPounds);
            coins.AddLast(onePound);
            coins.AddLast(fiftyCents);
            coins.AddLast(twentyCents);
            coins.AddLast(tenCents);
            coins.AddLast(fiveCents);
            coins.AddLast(twoCents);
            coins.AddLast(oneCent);
            
        }

        int forLoopRunner(LinkedListNode<Coin> currentCoin,int previousValue,int sums)
        {
            int current;
            for (int j = 0; j <= currentCoin.Value.maxNumber; j++)
            {
                current = previousValue + j * currentCoin.Value.value;
                if (current == valueToMatch)
                {
                    sums++;
                    break;
                }
                else if (current > valueToMatch)
                    break;
                if (currentCoin.Next != null)
                    sums = forLoopRunner(currentCoin.Next, current, sums);
            }
            return sums;
        }

        internal int solve()
        {
            int sums = 0;
            sums = forLoopRunner(coins.First, 0, sums);
            return sums;
            
        }
    }
}
