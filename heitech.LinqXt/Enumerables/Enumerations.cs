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

        /// <summary>
        /// Returns a slice of the enumerable.
        /// see docs on how it works (or see python)
        /// </summary>
        public static IEnumerable<TSource> Slice<TSource>(this IEnumerable<TSource> source, int? start = null, int? end = null, int? step = null)
        {
            var array = source.ToArray();
            int length = array.Length;

            int _end = Adjust(end, length, length);
            int _start = Adjust(start, length, 0);
            int _step = step ?? 1;

            if (_start > array.Length)
                _start = 0;
            if (_step != 1)
            {
                int index = 0;
                if (_step < 0)
                    source = source.Reverse();
                foreach (TSource item in source)
                {
                    if (index == 0)
                        yield return item;
                    else if (index % step == 0)
                        yield return item;
                    index++;
                }
            }
            else
            {
                for (int i = _start; i < _end; i++)
                    yield return array[i];
            }
        }

        private static int Adjust(int? index, int arrayLength, int fallback)
        {
            if (index == null)
                return fallback;

            int _index = index.Value;
            int result = _index;

            bool isNegative = _index < 0;
            if (isNegative)
                result = arrayLength + _index;

            if (Math.Abs(_index) > arrayLength)
            {
                result = arrayLength;
                if (isNegative)
                    result = 0;
            }

            return result;
        }
    }
}
