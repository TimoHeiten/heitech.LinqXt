using System.Collections.Generic;
using System.Linq;

namespace heitech.LinqXt.Enumerables
{
    public static class Slicing
    {
        /// <summary>
        /// Returns a slice of the enumerable.
        /// </summary>
        public static IEnumerable<TSource> Slice<TSource>(this IEnumerable<TSource> source, int? start = null, int? end = null, int? step = null)
        {
            return new Slice<TSource>(start, end, step, source.ToArray());
        }

        ///<summary>
        /// Slice an array
        /// <para />
        /// 1.) all null returns same array
        /// <para />
        /// 2.) start and end slice the array - [0, 1, 2, 3].Slice(1,2) --> [1]
        /// <para />
        /// negative values are also supported. See hte documentation for full explanation at https://t-heiten.net/best-linq-extension
        ///</summary>
        public static T[] SliceArray<T>(this T[] array, int? start = null, int? end = null, int? step = null)
        {
            var slice = new Slice<T>(start, end, step, array);
            return slice.ToArray();
        }

        ///<summary>
        /// Split into chunksize applied array chunks
        ///</summary>
        public static IEnumerable<T[]> Split<T>(this IEnumerable<T> source, int chunk_size)
        {
            var list = new List<T[]>();
            int count = source.Count();
            if (chunk_size >= count || chunk_size == 0)
                return new List<T[]> { source.ToArray() };

            for (int i = 0; i < count; i+= chunk_size)
            {
                chunk_size = AdjustedChunkSize(count, i, chunk_size);
                var array = new T[chunk_size];

                for (int j = 0; j < chunk_size; j++)
                    array[j] = source.ElementAt(i + j);

                list.Add(array);
            }

            return list;
        }


        private static int AdjustedChunkSize(int count, int index, int chunk_size)
        {
            if (count - index < chunk_size)
                chunk_size = count - index;
            return chunk_size;
        }
    }
}
