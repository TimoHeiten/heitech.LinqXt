using heitech.LinqXt.Enumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace heitech.LinqXt.Tests.Enumerables
{
    [TestClass]
    public class ArrayXtTests : EnumerableTestBase
    {
        private string[] numbers = new string[] { "1", "2", "3", "4" };
        private string[] mixed = new string[] { "1", "2", "a", "b" };
        private string[] chars = new string[] { "a", "b", "c", "d" };
        private int[] int_numbers = new int[] { 1, 2, 3, 4 };

        [TestMethod]
        public void ArrayXt_WhereReturnsAllThatSatisfyCondition()
        {
            int expected = 2;
            Assert.AreEqual(expected, mixed.WhereAsArray(x => int.TryParse(x, out int i)).Length);
        }

        [TestMethod]
        public void ArrayXt_WhereReturnsEmptyArrayIfNoConditionIsSatisfied()
        {
            int expected = 0;
            Assert.AreEqual(expected, chars.WhereAsArray(x => int.TryParse(x, out int i)).Length);
        }

        [TestMethod]
        public void ArrayXt_SubstituteExchangesItemsAtPredicate()
        {
            Predicate<string> predicate = s => s != "hunny";
            string[] result = Array.SubstitueAsArray(predicate, "_");
            foreach (var item in result)
                Assert.AreEqual("_", item);
        }

        [TestMethod]
        public void ArrayXt_ArrayReverseReturnsREversedArray()
        {
            var reversed = int_numbers.ReverseAsArray();

            Assert.AreEqual(4, reversed.ElementAt(0));
            Assert.AreEqual(3, reversed.ElementAt(1));
            Assert.AreEqual(2, reversed.ElementAt(2));
            Assert.AreEqual(1, reversed.ElementAt(3));
        }

        [TestMethod]
        public void ArrayXt_SkipAsArraySkipWorksLikeEnumerableSkip()
        {
            AssertSkipped(0);
            AssertSkipped(1);
            AssertSkipped(2);
            AssertSkipped(3);
            AssertSkipped(4);
            AssertSkipped(100);
        }

        [TestMethod]
        public void ArrayXt_SkipReturnsFullArrayIfCountIsLtZero()
        {
            AssertSkipped(-1);
            AssertSkipped(-2);
            AssertSkipped(-3);
            AssertSkipped(-100);
        }

        private void AssertSkipped(int c)
            => AssertArrays(int_numbers.SkipAsArray(c), int_numbers.Skip(c));

        private void AssertArrays<T>(T[] arr, IEnumerable<T> other)
        {
            for (int i = 0; i < arr.Length; i++)
                Assert.AreEqual(other.ElementAt(i), arr[i]);
        }

        private void AssertTake(int c)
            => AssertArrays(int_numbers.TakeAsArray(c), int_numbers.Take(c));

        [TestMethod]
        public void ArrayXt_TakeWorksAsSliceEnd()
        {
            AssertTake(0);
            AssertTake(1);
            AssertTake(2);
            AssertTake(3);
            AssertTake(4);
            AssertTake(100);
        }

        [TestMethod]
        public void ArrayXt_IfTakeCount_is_Lt_Zero_returnsNothing()
        {
            var result = int_numbers.Take(-1);
            var arr_result = int_numbers.TakeAsArray(-1);

            Assert.AreEqual(result.Count(), arr_result.Length);
        }
    }
}
