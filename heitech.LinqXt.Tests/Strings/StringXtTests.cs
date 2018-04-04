using heitech.LinqXt.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace heitech.LinqXt.Tests.Strings
{
    [TestClass]
    public class StringXtTests
    {
        [TestMethod]
        public void StringXt_ReplaceWhitespace()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void StringXt_RemoveIndexFromLeft()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void StringXt_RemoveIndexFromRight()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void StringXt_Shorten()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void StringXt_FromBytes()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void StringXt_ToBytes()
        {
            Assert.Fail();
        }

        [DataRow("UpAndDown", "uPaNDdOWN")]
        [DataRow("uPaNDdOWN", "UpAndDown")]
        [DataTestMethod]
        public void StringXt_SwapCase_SwapsLowerToUpperAndViceVersa(string upndown, string expected)
            => Assert.AreEqual(expected, upndown.SwapCase());

        [TestMethod]
        public void StringXt_FlattenSequence_returns_sequenceOfStrings_withSeperator()
        {
            string expected = "1,2,3";
            string result = new[] { 1, 2, 3 }.FlattenSequence(x => x.ToString());
            Assert.AreEqual(expected, result);
        }
    }
}
