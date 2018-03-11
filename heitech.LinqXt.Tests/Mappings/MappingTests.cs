using heitech.LinqXt.Mappings;
using heitech.LinqXt.Tests.Enumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace heitech.LinqXt.Tests.Mappings
{
    [TestClass]
    public class MappingTests : EnumerableTestBase
    {
        private readonly string newKey = "newKey";
        private readonly string newValue = "newValue";

        // MappingXt_
        [TestMethod]
        public void MappingXt_TrySubstituteKeyReturnsFalseIfKeyDoesNotExist()
            => Assert.IsFalse(Dictionary.TrySubstituteKey("noKey", newKey));

        [TestMethod]
        public void MappingXt_TrySubstituteKeyReturnsTrueIfKeyDoesExistAndWasExchanged()
        {
            Assert.IsTrue(Dictionary.TrySubstituteKey("key_1", newKey));
            Assert.IsTrue(Dictionary.ContainsKey(newKey));
        }

        [TestMethod]
        public void MappingXt_TrySubstituteValueReturnsFalseIfKeyDoesNotExist()
            => Assert.IsFalse(Dictionary.TrySubstituteValue("abcaffeschnee", newValue));

        [TestMethod]
        public void MappingXt_TrySubstituteValueReturnsTrueIfKeyDoesExistAndWasExchanged()
        {
            Assert.IsTrue(Dictionary.TrySubstituteValue("a", newValue));
            Assert.IsTrue(Dictionary.Values.Any(x => x == newValue));
        }

        [TestMethod]
        public void MappingXt_TrySubstituteKeyValueReturnsFalseIfKeyDoesNotExist()
            => Assert.IsFalse(Dictionary.TrySubstituteKeyValuePair(("_", "v"), ("--", "vv")));

        [TestMethod]
        public void MappingXt_TrySubstituteKeyValueReturnsTrueIfKeyValuePairExistsAsGiven()
        {
            Assert.IsTrue(Dictionary.TrySubstituteKeyValuePair(("key_1", "a"), (newKey, newValue)));
            Assert.IsTrue(Dictionary.ContainsKey(newKey));
            Assert.IsTrue(Dictionary.Any(x => x.Value == newValue));
        }
    }
}
