using System;
using System.Collections;
using System.Collections.Generic;

namespace heitech.LinqXt.Enumerables
{
    public class Slice<T> : IEnumerable<T>
    {
        public int? Start { get; }
        public int? End { get; }
        public int? Stride { get; }
        private T[] array;
        private readonly List<T> sliced = new List<T>();

        internal Slice(int? start, int? end, int? stride, T[] backingStore)
        {
            End = end;
            Start = start;
            Stride = stride;
            array = backingStore;
            SliceIt();
        }

        private void SliceIt()
        {
            int length = array.Length;
            var resultArray = new List<T>();

            int _end = Adjust(End, length, length);
            int _start = Adjust(Start, length, 0);
            int _step = Stride ?? 1;

            if (_start > array.Length)
                _start = 0;
            if (_step != 1)
            {
                int index = 0;
                if (_step < 0)
                    array = array.ReverseAsArray();
                foreach (T item in array)
                {
                    if (index == 0 || index % _step == 0)
                        resultArray.Add(item);
                    index++;
                }
            }
            else
            {
                for (int i = _start; i < _end; i++)
                    resultArray.Add(array[i]);
            }
            sliced.AddRange(resultArray);
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

        public IEnumerator<T> GetEnumerator()
        {
            return sliced.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return $"Slice: [{Start} : {End} : {Stride}]";
        }
    }
}