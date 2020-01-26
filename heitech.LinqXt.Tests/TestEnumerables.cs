using System.Collections.Generic;
using System.Linq;

namespace heitech.LinqXt.Tests
{
    public class TestEnumerables
    {
        internal string[] Array { get; } = new string[] { "a", "b", "c" };
        internal static List<string> List { get; } = new List<string> { "a", "b", "c" };

        internal static IEnumerable<int> Numbers() 
        {
            foreach (var item in Enumerable.Range(1, 3))
            {
                yield return item;
            }
        }
        internal Dictionary<string, string> dictionary = new Dictionary<string, string>
        {
            ["key_1"] = "a",
            ["key_2"] = "b",
            ["key_3"] = "c"
        };
    }
}
