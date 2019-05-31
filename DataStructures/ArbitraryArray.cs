using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class ArbitraryArray<T> : IEnumerable<T>
    {
        private readonly T[] _data;
        private readonly int _shift;
        public int LeftLimitIndex { get => _shift; }
        public int RightLimitIndex { get; } // _shift+data.Count

        public T this[int index]
        {
            get => _data[_getRealIndex(index)];

            set => _data[_getRealIndex(index)] = value;
        }

        public ArbitraryArray(int countElements)
            : this(0, countElements) { }

        public ArbitraryArray(int startIndex, int countElements)
        {
            if (countElements <= 0)
                throw new ArgumentException($"{nameof(countElements)} cannot be less than zero");

            _data = new T[countElements];
            _shift = startIndex;
            RightLimitIndex = startIndex + countElements;
        }

        private int _getRealIndex(int index)
        {
            return index < LeftLimitIndex || index >= RightLimitIndex
            ? throw new ArgumentException("index was outside the bounds of the array", nameof(index))
            : index - _shift;
        }

        public IEnumerator<T> GetEnumerator() => _data.Cast<T>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }
}
