using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace heitech.LinqXt.Enumerables
{
    public static class Extensions
    {
        /// <summary>
        /// AddRange style appending. Enumerable is iterated!
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

        /// <summary>
        /// Removes the last items of a given collection. If index exceeds collection count --> returns zero items
        /// </summary>
        public static IEnumerable<TSource> RemoveLastItems<TSource>(this IEnumerable<TSource> source, int? index = null)
        {
            var slice = new Slice<TSource>(0, -index, 1,source.ToArray());

            return slice;
        }
    }
}
