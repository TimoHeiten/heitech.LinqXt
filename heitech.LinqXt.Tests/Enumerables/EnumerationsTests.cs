using heitech.LinqXt.Enumerables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [TestMethod]
        public void Enumerations_NotAll_returns_false_Where_All_Returns_true()
        {
            string[] items = new string[] { "1", "2", "3" };

            bool valid_forall = items.All(x => int.TryParse(x, out int i));
            bool not_valid_forall = items.NotAll(x => int.TryParse(x, out int i));

            Assert.IsTrue(valid_forall);
            Assert.IsFalse(not_valid_forall);
        }

        [TestMethod]
        public async Task Enumerations_ToTask_converts_to_Task_List_for_Task_Whenall()
        {
            var toTaskItems = new []{ new ToTaskItem(), new ToTaskItem(), new ToTaskItem(), };

            IEnumerable<Task> tasks = toTaskItems.ToTaskList(x => x.DoWork());

            await Task.WhenAll(tasks);
            foreach (var to in toTaskItems)
                Assert.IsTrue(to.WasSet);
        }

        internal class ToTaskItem
        {
            internal bool WasSet;
            public Task DoWork()
            {
                WasSet = true;
                return Task.CompletedTask;
            }
        }
    }
}
