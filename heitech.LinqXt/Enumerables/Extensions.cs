using System;
using System.Collections.Generic;
using System.Linq;

namespace heitech.LinqXt.Enumerables
{
    public static class Extensions
    {
        /// <summary>
        /// AddRange style appending. Enumerable is iterated over!
        /// </summary>
        public static IEnumerable<TSource> Extend<TSource>(this IEnumerable<TSource> source, params TSource[] items)
        {
            var list = source.ToList();
            list.AddRange(items);
            return list;
        }

        /// <summary>
        /// AddRange style appending. Enumerable is iterated over!
        /// </summary>
        public static IEnumerable<TSource> Extend<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate, params TSource[] items)
        {
            var list = source.ToList();
            items.ForAll(x =>
            {
                if (predicate(x))
                    list.Add(x);
            });
            return list;
        }
    }
}
