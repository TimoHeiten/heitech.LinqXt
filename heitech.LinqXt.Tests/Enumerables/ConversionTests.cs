using heitech.LinqXt.Enumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace heitech.LinqXt.Tests.Enumerables
{
    [TestClass]
    public class ConversionTests
    {
        private readonly TestEnumerables enumerables = new TestEnumerables();
        internal IEnumerable<string> Array => enumerables.Array;

        [TestMethod]
        public void Conversion_ConvertTakesSuppliedConversionFunction()
        {
            Func<string, int> toInt = s =>
            {
                if (s == "a") return 0;
                if (s == "b") return 1;
                else return 2;
            };

            IEnumerable<int> _ints = Array.Convert(toInt);
            Assert.AreEqual(0, _ints.ElementAt(0));
            Assert.AreEqual(1, _ints.ElementAt(1));
            Assert.AreEqual(2, _ints.ElementAt(2));
        }

        [TestMethod]
        public void Conversion_SubstituteExchangesAtPredicate()
        {
            Predicate<string> predicate = s => s != "hunny";

            IEnumerable<string> result = Array.Substitute(predicate, "_");
            foreach (var item in result)
                Assert.AreEqual("_", item);
        }


        [TestMethod]
        public void Conversions_ZipZipsTwoSameCollectionsToOneWithAllItems()
            => Assert.ThrowsException<NotImplementedException>(() => Array.Zip(null));

        [TestMethod]
        public void Conversions_ZipZipsTwoNotEquallyCountedCollectionsFillsAllOfSmallerCountWithDefault()
            => Assert.ThrowsException<NotImplementedException>(() => Array.Zip(null));

        [TestMethod]
        public void Conversions_ZipZipsTwoNotEquallyCountedCollectionsFillsAllOfSmallerCountWith_UserSpecified_Default()
            => Assert.ThrowsException<NotImplementedException>(() => Array.Zip(null));

        [TestMethod]
        public void Conversion_CombineReturnsEnumerableWithTupleOfSameCount()
        {
            var tuples = Array.CombineToOne(Array);
            foreach (var (first,snd) in tuples)
                Assert.AreSame(first, snd);
        }

        [TestMethod]
        public void Conversion_CombineReturnsEnumerableWithTuplesAndLetsOneDefaultIfLessItems()
        {
            var tuples = Array.CombineToOne(new string[] { "1", "2" });
            Assert.AreEqual(("a", "1"), tuples.ElementAt(0));
            Assert.AreEqual(("b", "2"), tuples.ElementAt(1));
            Assert.AreEqual(("c", null), tuples.ElementAt(2));
        }

        [TestMethod]
        public void Conversion_CombineReturnsEnumerableWithTupleAnd_USerspecified_Default()
        {
            var tuples = new string[] { "1", "2" }.CombineToOne(Array);
            Assert.AreEqual(("1", "a"), tuples.ElementAt(0));
            Assert.AreEqual(("2", "b"), tuples.ElementAt(1));
            Assert.AreEqual((null, "c"), tuples.ElementAt(2));
        }

        [TestMethod]
        public void Conversion_CombineLetsUserSpecifyTheFallbacks()
        {
            var t = new string[] { "1" };
            var tuples = t.CombineToOne(new string[] { "2", "3" }, fallback: "fallback");

            Assert.AreEqual("fallback", tuples.ElementAt(1).first);
        }
    }
}
