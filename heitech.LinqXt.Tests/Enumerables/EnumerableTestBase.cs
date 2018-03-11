using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace heitech.LinqXt.Tests.Enumerables
{
    [TestClass]
    public class EnumerableTestBase
    {
        protected TestEnumerables TestEnumerable { get; } = new TestEnumerables();

        protected string[] Array => TestEnumerable.Array;
        protected Dictionary<string, string> Dictionary => TestEnumerable.dictionary;

        protected List<string> List => TestEnumerables.List;
    }
}
