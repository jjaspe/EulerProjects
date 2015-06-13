using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EulerMisc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace EulerMisc.Tests
{
    [TestClass()]
    public class UtilTests
    {
        [TestMethod()]
        public void toDecimalArrayTest()
        {
            double value = 1245;
            int[] expected = {5,4, 2, 1 };
            Assert.IsTrue(Util.arraysEqual(expected, Util.toDecimalArray(value)));
        }

        [TestMethod()]
        public void getTupleTest()
        {
            double[] bas = { 12, 35 };
            int[,] expected = { { 2, 1 }, { 5, 3 } };
            Assert.AreEqual(expected[1, 1], Util.getTuple(bas)[1, 1]);
        }

        [TestMethod()]
        public void combineTupleTest()
        {
            //12+350=362={2,6,3}
            int[,] expected = { { 2, 1 }, { 5, 3 } };
            Assert.IsTrue(Util.arraysEqual( new int[]{2,6,3},  Util.combineTuple(expected,2)));
        }

        [TestMethod()]
        public void combineTupleTestMoreTuplesThanDigits()
        {
            //12+350+5400=5762={2,6,7,5}
            int[,] expected = { { 2, 1 }, { 5, 3 },{4,5} };
            Assert.IsTrue(Util.arraysEqual(new int[] { 2, 6,7, 5}, Util.combineTuple(expected, 3)));
        }

        [TestMethod()]
        public void combineTupleTestMoreTuplesThanDigitsWithCarry()
        {
            //99+990+9900=10989={9,8,9,0,1}
            int[,] expected = { { 9, 9 }, { 9, 9 }, { 9, 9 } };
            Assert.IsTrue(Util.arraysEqual(new int[] { 9,8,9,0,1 }, Util.combineTuple(expected, 3)));
        }

        [TestMethod()]
        public void combineTupleTestLessTuplesThanDigits()
        {
            //512 + 4350=4862={2,6,8,4}
            int[,] expected = { { 2, 1,5 }, { 5, 3,4}};
            Assert.IsTrue(Util.arraysEqual(new int[] { 2, 6, 8, 4 }, Util.combineTuple(expected, 2)));
        }

        [TestMethod()]
        public void combineTupleTestLessTuplesThanDigitsWithCarry()
        {
            //999 + 9990=10989={9,8,9,0,1}
            int[,] expected = { { 9,9,9 }, { 9,9,9 } };
            Assert.IsTrue(Util.arraysEqual(new int[] { 9, 8, 9, 0, 1 }, Util.combineTuple(expected, 2)));
        }

        [TestMethod()]
        public void getPowerDecimalTest32Pow3()
        {
            double[] pDecimal=Util.getPowerDecimal(new int[] { 2, 3 }, 3);
            int[,] tuples=Util.getTuple(pDecimal);
            //32^3=32678={8,6,7,2,3}
            int[] actual = Util.combineTuple(tuples,pDecimal.Length);
            Assert.IsTrue(Util.arraysEqual(new int[]{8,6,7,2,3},actual));
        }

        [TestMethod()]
        public void getPowerDecimalTest32Pow5()
        {
            double[] pDecimal = Util.getPowerDecimal(new int[] { 2, 3 }, 5);
            int[,] tuples = Util.getTuple(pDecimal);
            //2^25=32^5=33554432={2,3,4,4,5,5,3,3}
            int[] actual = Util.combineTuple(tuples, pDecimal.Length);
            Assert.IsTrue(Util.arraysEqual(new int[] { 2, 3, 4, 4, 5, 5, 3, 3 }, actual));
        }

        [TestMethod()]
        public void multiplyByDigitTest()
        {
            int[] number = { 9, 9 };
            Assert.IsTrue(Util.arraysEqual(new int[] { 1, 9, 8 }, Util.multiplyByDigit(number, 9)));
        }

        [TestMethod()]
        public void addArraysTest()
        {
            int[] a1 = { 1, 8 }, a2 = { 3, 4 };
            int[] expected = { 4, 2, 1 };
            Assert.IsTrue(Util.arraysEqual(expected, Util.addArrays(a1, a2)));
        }

        [TestMethod()]
        public void multiplyArraysTest()
        {
            int[] a1 = { 1, 8 }, a2 = { 3, 4 };
            int[] expected = { 3,8,4,3 };
            Assert.IsTrue(Util.arraysEqual(expected, Util.multiplyArrays(a1, a2)));
        }

        [TestMethod()]
        public void multiplyArraysTest2()
        {
            int[] a1 = { 1, 8,4,5 }, a2 = { 3, 4,1};
            int[] expected = { 3,8,7,3,8,7};
            Assert.IsTrue(Util.arraysEqual(expected, Util.multiplyArrays(a1, a2)));
        }

        [TestMethod()]
        [Ignore]
        public void addDigitsAsArrayTest()
        {
            int[] number = { 3, 2 };
            Assert.IsTrue(Util.arraysEqual(new int[]{5,0}, Util.addDigitsAsArray(number)));
        }

        [TestMethod()]
        public void addDigitsAsArrayTest2()
        {
            int[] number = { 8,9 };
            Assert.IsTrue(Util.arraysEqual(new int[] { 7, 1 }, Util.addDigitsAsArray(number)));
        }

        [TestMethod()]
        public void trimRightTest()
        {
            int[] number = { 0,0, 8, 0, 8, 6 };
            Assert.IsTrue(Util.arraysEqual(new int[]{8,0,8,6},Util.trimStart(number)));
        }

        [TestMethod()]
        public void trimLeftTest()
        {
            int[] number = { 8, 0, 8, 6,0,0};
            Assert.IsTrue(Util.arraysEqual(new int[] { 8, 0, 8, 6 }, Util.trimEnd(number)));
        }

        [TestMethod()]
        public void getEulerFunctionTestFirstFive()
        {
            double[] actual = Util.getEulerFunction(5);
            double[] expected = { 1, 1, 2, 2, 3, 2 };
            Assert.IsTrue(Util.arraysEqual(expected, actual));
        }

        [TestMethod()]
        public void getEulerFunctionTestFirst20()
        {
            double[] actual = Util.getEulerFunction(20);
            double[] expected = { 1, 1, 2, 2, 3, 2,4,2,4,3,4,2,6,2,4,4,5,2,6,2,6};
            Assert.IsTrue(Util.arraysEqual(expected, actual));
        }

        [TestMethod()]
        public void getSumOfFactorsUpToTestTo10()
        {
            double[] actual = Util.getSumOfFactorsUpTo(15);
            double[] expected = {0,1, 1, 1, 3, 1,6,1,7,4,8,1,16,1,10,9};
            Assert.IsTrue(Util.arraysEqual(expected, actual));
        }

        [TestMethod()]
        public void fileParserTest()
        {
            string[] s = Util.fileParser("../../../files/p022_names.txt");
            Assert.IsNotNull(s);
        }
        
    }
}
