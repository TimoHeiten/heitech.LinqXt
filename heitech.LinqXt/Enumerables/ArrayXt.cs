using System;
using System.Collections.Generic;
using System.Linq;

namespace heitech.LinqXt.Enumerables
{
    public static class ArrayXt
    {
        public static T[] WhereAsArray<T>(this T[] array, Func<T, bool> func)
        {
            var list = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                T item = array[i];
                if (func(item))
                    list.Add(item);
            }
            return list.ToArray();
        }

        public static R[] ConvertAsArray<T, R>(this T[] array, Func<T, R> conversion)
        {
            var list = new List<R>();
            for (int i = 0; i < array.Length; i++)
                list.Add(conversion(array[i]));
            return list.ToArray();
        }

        public static T[] SubstitueAsArray<T>(this T[] array, Predicate<T> pred, T substitute)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (pred(array[i]))
                    array[i] = substitute;
            }
            return array;
        }

        public static T[] AppendAsArray<T>(this T[] array, params T[] append)
        {
            var list = array.ToList();
            list.AddRange(append);
            return list.ToArray();
        }

        public static C[] CastAsArray<T, C>(this T[] array)
        {
            var list = new List<C>();
            for (int i = 0; i < array.Length; i++)
                list.Add(((C)(object)array[i]));
            return list.ToArray();
        }

        internal static T[] ArrayToListWithFillAction<T>(this T[] array, Action<List<T>> action)
        {
            var list = array.ToList();
            action(list);
            return list.ToArray();
        }

        public static T[] ConcatAsArray<T>(this T[] array, IEnumerable<T> extend)
            => array.Concat(extend).ToArray();

        public static T[] DistinctAsArray<T>(this T[] array)
            => array.Distinct().ToArray();

        public static T[] ExceptAsArray<T>(this T[] array, IEnumerable<T> second)
            => array.Except(second).ToArray();

        public static T[] IntersectAsArray<T>(this T[] array, IEnumerable<T> second)
            => array.Intersect(second).ToArray();

        public static R[] OfTypeAsArray<T, R>(this T[] array)
            => array.OfType<R>().ToArray();

        public static T[] ReverseAsArray<T>(this T[] array)
        {
            var arr2 = new T[array.Length];
            int index = 0;
            for (int i = array.Length-1; i >= 0 ; i--)
            {
                arr2[index] = array[i];
                index++;
            }
            return arr2;
        }

        public static T[] SkipAsArray<T>(this T[] array, int count)
        => count < 0 ? array : array.SliceArray(count);

        public static T[] TakeAsArray<T>(this T[] array, int take)
            => take < 0 ? new T[0] : array.SliceArray(null, take);

        public static T[] UnionAsArray<T>(this T[] array, IEnumerable<T> union)
            => array.Union(union).ToArray();


        public static T[] SwapItemAt<T>(this T[] array, int fromIndex, int toIndex)
        {
            var result = new T[array.Length];
            array.CopyTo(result, 0);

            if (fromIndex < 0) fromIndex = 0;
            if (toIndex > array.Length - 1) toIndex = array.Length - 1;

            result[toIndex] = array[fromIndex];
            result[fromIndex] = array[toIndex];

            return result;
        }

        public static T[] SwapItemAt<T>(this T[] array, Predicate<T> from_item, Predicate<T> to_item)
        {
            var result = new T[array.Length];
            array.CopyTo(result, 0);

            (T from, int index) match_from = (default(T), -1);
            (T to, int index) match_to = (default(T), -1);

            for (int i = 0; i < array.Length; i++)
            {
                T item = array[i];
                if (from_item(item))
                {
                    match_from.from = item;
                    match_from.index = i;
                }
                if (to_item(item))
                {
                    match_to.to = item;
                    match_to.index = i;
                }
            }

            if (CanSwap(match_from, match_to, array))
            {
                result[match_to.index] = array[match_from.index];
                result[match_from.index] = array[match_to.index];
            }

            return result;
        }

        private static bool CanSwap<T>((T obj, int index) tuple_from,
            (T obj, int index) tuple_to,
            T[] array) => tuple_from.index != tuple_to.index
            && InBounds(tuple_from.index, array) 
            && InBounds(tuple_to.index, array);

        private static bool InBounds<T>(int index, T[] array)
            => index >= 0 && index <= array.Length - 1;
    }
}
