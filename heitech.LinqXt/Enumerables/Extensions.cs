using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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

        /// <summary>
        /// Removes the last items of a given collection.
        /// </summary>
        public static IEnumerable<TSource> RemoveLastItems<TSource>(this IEnumerable<TSource> source, int? index = null)
        {            
            if (index.HasValue == false)
                return source;

            int count = GetCountOfSource(source);
            if (count < index)
                return source.Skip(count);

            return source.Reverse().Skip(index.Value).Reverse();
        }

        private static int GetCountOfSource<TSource>(IEnumerable<TSource> source)
        {
            if (source is IList<TSource> list)
                return list.Count;
            else if (source is Array array)
                return array.Length;
            else
                return source.Count();
        }
    }
}
