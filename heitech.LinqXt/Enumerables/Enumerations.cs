using System;
using System.Collections.Generic;
using System.Linq;

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
            foreach (var item in source)
                if (predicate(item))
                    return false;
            return true;
        }
    }
}
