using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace heitech.LinqXt.Enumerables
{
    public static class Enumerations
    {
        /// <summary>
        /// Use Instead of foreach. The source still uses its
        /// </summary>
        public static void ForAll<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (TSource item in source)
                action(item);
        }

        public static bool NotAny<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            foreach (TSource item in source)
                if (predicate(item))
                    return false;
            return true;
        }

        public static bool NotAny<TSource>(this IEnumerable<TSource> source) 
            => !source.Any();

        public static bool NotAll<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
            => !source.All(x => predicate(x));

        public static IEnumerable<Task> ToTaskList<T>(this IEnumerable<T> source, Func<T, Task> func)
        {
            var list = new List<Task>();
            source.ForAll(x => list.Add(func(x)));
            return list;
        }

        // do: 
        // enumerate with index

        public static void EnumerateWithIndex<T>(this IEnumerable<T> source, Action<int, T> _do)
        {
            int index = 0;
            foreach (var item in source)
            {
                _do(index, item);
                index++;
            }
        }
    }
}
