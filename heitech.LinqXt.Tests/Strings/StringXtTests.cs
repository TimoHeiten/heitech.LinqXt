using heitech.LinqXt.String;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;

namespace heitech.LinqXt.Tests.Strings
{
    [TestClass]
    public class StringXtTests
    {
        [TestMethod]
        public void StringXt_ReplaceWhitespace_WithDefaultChar_RemovesAllWhitespace_In_A_String()
        {
            string withWhiteSpace = "string with a l o t of w h i t e s p a c e ";
            string expected = "stringwithalotofwhitespace";

            string withoutWihteSpace = withWhiteSpace.ReplaceWhitespaceWith();

            Assert.AreEqual(expected, withoutWihteSpace);
        }

        [TestMethod]
        public void StringXt_ReplaceWhiteSpace_WithNoneDefaultChar_ReplacesWhiteSpace_With_Char()
        {
            string change = "abc affe schnee";
            string expected = "abc_affe_schnee";

            string result = change.ReplaceWhitespaceWith('_');

            Assert.AreEqual(expected, result);
        }

        [DataRow(0, "abcaffeschnee", "abcaffeschnee")]
        [DataRow(1, "abcaffeschnee", "bcaffeschnee")]
        [DataRow(-10, "abcaffeschnee", "nee")]
        [DataRow(100, "abcaffeschnee", "")]
        [DataTestMethod]
        public void StringXt_RemoveIndexFromLeft(int index, string input, string expected)
        {
            string actual = input.RemoveCharsFromLeft(index);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(0, "abcaffeschnee", "abcaffeschnee")]
        [DataRow(1, "abcaffeschnee", "abcaffeschne")]
        [DataRow(-10, "abcaffeschnee", "abc")]
        [DataRow(100, "abcaffeschnee", "")]
        [DataTestMethod]
        public void StringXt_RemoveIndexFromRight(int index, string _same, string expected)
        {
            string actual = _same.RemoveCharsFromRight(index);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StringXt_Shorten()
        {
            var array = new char[77];
            for (int i = 0; i < 77; i++)
                array[i] = 'a';

            var bigArray = array.Concat(array).ToArray();
            string longstring = new string(bigArray);
            string expected = new string(array) + "...";

            string result = longstring.Shorten();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StringXt_Shorten_IfCharAmount_gt_Source_returns_Source()
        {
            string source = "to short for shortening";

            Assert.AreEqual(source, source.Shorten());
        }

        [TestMethod]
        public void StringXt_RemoveAll_Removes_allCharsOfGivenChar()
        {
            char[] spec = new[] { 'a', 'b', '_', ' ', '1' };
            string toRemove = "abc_def_ghi_1234 56789";
            string expected = "cdefghi23456789";

            string result = toRemove.RemoveAll(spec);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StringXt_RemoveAllWithoutSpecReturnsOriginal()
        {
            char[] specs = new char[] { };
            string toRemove = "abcaffeschnee    ";
            string expected = toRemove;

            string result = toRemove.RemoveAll();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StringXt_FromBytes()
        {
            string source = "toBytes";
            byte[] bytes = Encoding.Unicode.GetBytes(source);

            Assert.AreEqual(bytes.Length, source.ToBytes().Length);
            Assert.AreEqual("toBytes", bytes.FromBytes());
        }

        [DataRow("", true)]
        [DataRow(null, true)]
        [DataRow("abc", false)]
        [DataRow(" ", false)]
        [DataRow("\t", false)]
        [DataTestMethod]
        public void StringXt_IsNullOrEmpty(string i, bool expected)
        {
            bool result = i.IsNullOrEmpty();
            Assert.AreEqual(expected, result);
        }

        [DataRow("", true)]
        [DataRow(" ", true)]
        [DataRow("\t", true)]
        [DataRow(null, true)]
        [DataRow("abc", false)]
        [DataTestMethod]
        public void StringXt_IsNullOrWhiteSpace(string i, bool expected)
        {
            bool result = i.IsNullOrWhiteSpace();
            Assert.AreEqual(expected, result);
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

        [DataRow("PascalCase", "pascal.case", '.')]
        [DataRow("ABC", "a_b_c", '_')]
        [DataTestMethod]
        public void ChangeCasing(string input, string expected, char sep)
        {
             var result = input.CasingToSeperator(sep);
             Assert.AreEqual(expected, result);
        }
    }
}
