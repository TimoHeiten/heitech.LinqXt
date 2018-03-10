using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using heitech.LinqXt.Enumerables;
using System.Linq;

namespace heitech.LinqXt.Tests.Enumerables
{
    [TestClass]
    public class ExtensionTests
    {
        private IEnumerable<string> List => TestEnumerables.List;

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
    }
}
