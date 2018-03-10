using System;
using System.Collections.Generic;
using System.Linq;

namespace heitech.LinqXt.Enumerables
{
    public static class Conversions
    {
        public static IEnumerable<TConverted> Convert<TSource, TConverted>(this IEnumerable<TSource> source, Func<TSource, TConverted> conversion)
        {
            foreach (TSource item in source)
                yield return conversion(item);
        }

        public static IEnumerable<TSource> Substitute<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate, TSource itemToExchange)
        {
            foreach (TSource item in source)
                if (predicate(item))
                    yield return itemToExchange;
                else
                    yield return item;
        }

        public static IEnumerable<TSource> Zip<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> other, TSource fallback= default(TSource))
            => throw new NotImplementedException();

        public static IEnumerable<(TSource first, TSndSource snd)> CombineToOne<TSource, TSndSource>(this IEnumerable<TSource> source, IEnumerable<TSndSource> other, TSource fallback = default(TSource), TSndSource fallbackSnd = default(TSndSource))
        {
            TSndSource[] _sndSource = other.ToArray();
            TSource[] _source = source.ToArray();

            if (_source.Length > _sndSource.Length)
                foreach (var item in Iterate(_source, _sndSource, fallbackSnd))
                    yield return item;
            else
                foreach (var (first, snd) in Iterate(_sndSource, _source, fallback))
                    yield return (snd, first);
        }

        private static IEnumerable<(MoreCount, LessCount)> Iterate<MoreCount, LessCount>(MoreCount[] more, LessCount[] less, LessCount fallback)
        {
            var array = new (MoreCount m, LessCount l)[more.Length];
            for (int i = 0; i < more.Length; i++)
            {
                LessCount lessItem = fallback;
                if (i < less.Length)
                    lessItem = less[i];
                array[i] = (more[i], lessItem);
            }
            return array;
        }
    }
}
