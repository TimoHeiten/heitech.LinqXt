using heitech.LinqXt.Enumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace heitech.LinqXt.Tests.Enumerables
{
    [TestClass]
    public class EnumerationsTests : EnumerableTestBase
    {
        [TestMethod]
        public void Enumerations_ForAllIteratesOverEachItemAndAppliesSpecifiedAction()
        {
            int counter = 0;
            Array.ForAll(linq => counter++);

            Assert.AreEqual(3, counter);
        }

        [TestMethod]
        public void Enumerations_NotAnyIsEquivalentToAnyWithInversePredicate()
        {
            bool anyResult = !Array.Any(x => int.TryParse(x, out int i));
            bool notAnyResult = Array.NotAny(x => int.TryParse(x, out int i));

            Assert.AreEqual(anyResult, notAnyResult);
        }

        [TestMethod]
        public void Enumerations_NotAnyReturnsFalseWhereAnyReturnsTrue()
        {
            Predicate<string> predicate = x => int.TryParse(x, out int i);
            var items = new string[] { "1", "a", "b" };

            bool any = items.Any(x => predicate(x));
            bool notAny = items.NotAny(x => predicate(x));

            Assert.IsFalse(notAny);
            Assert.IsTrue(any);
        }
    }
}
