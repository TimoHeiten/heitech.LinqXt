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
    }
}
