using System;
using System.Collections.Generic;
using System.Linq;

namespace heitech.LinqXt.Enumerables
{
    public static class Slicing
    {
        /// <summary>
        /// Returns a slice of the enumerable.
        /// see docs on how it works (or see python)
        /// </summary>
        public static IEnumerable<TSource> Slice<TSource>(this IEnumerable<TSource> source, int? start = null, int? end = null, int? step = null)
            => source.ToArray().SliceArray(start, end, step);

        public static T[] SliceArray<T>(this T[] array, int? start = null, int? end = null, int? step = null)
        {
            int length = array.Length;
            var resultArray = new List<T>();

            int _end = Adjust(end, length, length);
            int _start = Adjust(start, length, 0);
            int _step = step ?? 1;

            if (_start > array.Length)
                _start = 0;
            if (_step != 1)
            {
                int index = 0;
                if (_step < 0)
                    array = array.ReverseAsArray();
                foreach (T item in array)
                {
                    if (index == 0 || index % step == 0)
                        resultArray.Add(item);
                    index++;
                }
            }
            else
            {
                for (int i = _start; i < _end; i++)
                    resultArray.Add(array[i]);
            }
            return resultArray.ToArray();
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

        public static IEnumerable<T[]> Split<T>(this IEnumerable<T> source, int chunk_size)
        {
            // split source in equal parts, according to chunk size
            // source.count/chunk_size = 
            throw new NotImplementedException();
        }


    }
}
