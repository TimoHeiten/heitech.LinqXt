using heitech.LinqXt.Enumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace heitech.LinqXt.Tests.Enumerables
{
    [TestClass]
    public class SlicingTests : EnumerableTestBase
    {
        [TestMethod]
        public void Slicing__SliceWithoutStartEndStepReturnsCollection()
           => Assert.AreEqual(3, Array.Slice().Count());

        [TestMethod]
        public void Slicing__SliceWithPositiveStartAndStartOnly_ReturnsPartOfCollectionCountingOfStart()
        {
            Assert.AreEqual(3, Array.Slice(0).Count());
            Assert.AreEqual(2, Array.Slice(1).Count());
            Assert.AreEqual(1, Array.Slice(2).Count());
            Assert.AreEqual(0, Array.Slice(3).Count());
        }

        [TestMethod]
        public void Slicing__SliceWithStartGreaterThanCountHandlesItLikeStartEqualsZero()
            => Assert.AreEqual(0, Array.Slice(124).Count());

        [TestMethod]
        public void Slicing__SliceWithEndGreaterThanCollectionReturnsCollection()
            => Assert.AreEqual(3, Array.Slice(null, 112).Count());

        [TestMethod]
        public void Slicing__SliceWithEndSmallerThanCollectionCountsOnlyToThisPoint()
        {
            Assert.AreEqual(0, Array.Slice(0, 0).Count()); //start 0 and null are equivalent
            Assert.AreEqual(1, Array.Slice(0, 1).Count());
            Assert.AreEqual(2, Array.Slice(0, 2).Count());
            Assert.AreEqual(3, Array.Slice(0, 3).Count());
        }

        [TestMethod]
        public void Slicing__NegativeEndCutsOffEnd()
        {
            Assert.AreEqual(0, Array.Slice(0, -3).Count());
            Assert.AreEqual(0, Array.Slice(0, -0).Count());
            Assert.AreEqual(1, Array.Slice(0, -2).Count());
            Assert.AreEqual(2, Array.Slice(0, -1).Count());
            Assert.AreEqual(0, Array.Slice(0, -14).Count());
        }

        [TestMethod]
        public void Slicing__NegativeStartCountsFromBack_ReversesSoToSpeak()
        {
            AssertSlice(-0, 3);
            AssertSlice(-1, 1);
            AssertSlice(-2, 2);
            AssertSlice(-3, 3);
            AssertSlice(-12, 3);
        }

        private void AssertSlice(int start, int expectedNumber)
        {
            var slice = Array.Slice(start);
            Assert.AreEqual(expectedNumber, slice.Count());
        }

        [TestMethod]
        public void Slicing__SliceStepSkipsOverAmountOfSpecifiedItems()
        {
            Assert.AreEqual(3, Array.Slice(null, null, 1).Count());
            var slice = Array.Slice(null, null, 2);
            Assert.AreEqual(2, slice.Count());
            Assert.AreEqual(Array.First(), slice.First());
            Assert.AreEqual(Array.Last(), slice.Last());

            Assert.AreEqual(1, Array.Slice(null, null, 3).Count());
            Assert.AreEqual(1, Array.Slice(null, null, 4).Count());
        }

        [TestMethod]
        public void Slicing__SliceWithNegativeStepReversesCollection()
        {
            Assert.AreEqual(3, Array.Slice(null, null, -1).Count());
            var slice = Array.Slice(null, null, -2);
            Assert.AreEqual(2, slice.Count());
            Assert.AreEqual(Array.Last(), slice.First());
            Assert.AreEqual(Array.First(), slice.Last());

            Assert.AreEqual(1, Array.Slice(null, null, -3).Count());
            Assert.AreEqual(1, Array.Slice(null, null, -4).Count());
        }

        private int[] items = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        [TestMethod]
        public void Slicing__Split_ReturnsEquallyParted_CollectionChunks()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Slicing_Split_onZeroChunkSize_ReturnsSingleArrayCollection()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Slicing_Split_Returns()
        {
            Assert.Fail();
        }
    }
}
