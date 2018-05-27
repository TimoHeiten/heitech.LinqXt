using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using heitech.LinqXt.Enumerables;
using System.Linq;

namespace heitech.LinqXt.Tests.Enumerables
{
    [TestClass]
    public class ExtensionTests : EnumerableTestBase
    {
        [TestMethod]
        public void Extensions_ExtendAddsAllItems()
        {
            IEnumerable<string> extended = List.Extend("abc", "def", "ghi");

            Assert.AreEqual(6, extended.Count());
        }

        [TestMethod]
        public void Extensions_ExtendWIthPredicateOverloadOnlyAddsThoseItemsWhichSatisfyPredicate()
        {
            IEnumerable<string> extended = List.Extend(s => s.Length >= 3 , "g", "h", "i");

            Assert.AreEqual(3, extended.Count()); // all not added
        }

        [TestMethod]
        public void Extensions_RemoveLast_N_Items_Removes_All_Items()
        {
            IEnumerable<string> removeItems = GetForRemoval();

            var result = removeItems.RemoveLastItems(3);

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void Extensions_RemoveLast_N_Items_Index_Is_Null_Returns_Collection_Unchanged()
        {
            IEnumerable<string> removeItems = GetForRemoval();

            var result = removeItems.RemoveLastItems();

            Assert.AreEqual(5, result.Count());

        }

        [TestMethod]
        public void Extensions_RemoveLasst_N_Items_Returns_Same_But_Empty_Colleciton_If_N_GT_Count()
        {
            IEnumerable<string> removeItems = GetForRemoval();

            var result = removeItems.RemoveLastItems(112);

            Assert.AreEqual(0, result.Count());
        }

        private IEnumerable<string> GetForRemoval()
            => new[] { "one", "two", "three", "four", "five" };
    }
}
