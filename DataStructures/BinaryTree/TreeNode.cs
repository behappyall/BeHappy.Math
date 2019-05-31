using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.BinaryTree
{
    class TreeNode<T> : IComparable<T>, IEnumerable<T> where T : IComparable<T>
    {
        readonly T _data;
        internal TreeNode<T> LeftChild;
        internal TreeNode<T> RightChild;
        internal TreeNode<T> Parent;

        public TreeNode(T data)
        {
            _data = data;
        }

        public bool HasChild() => LeftChild != null || RightChild != null;

        public int CompareTo(T other) => _data.CompareTo(other);

        public IEnumerator<T> GetEnumerator()
        {
            if (LeftChild != null)
                foreach (T item in LeftChild)
                    yield return item;

            yield return _data;

            if (RightChild != null)
                foreach (T item in RightChild)
                    yield return item;

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static implicit operator T(TreeNode<T> node) => node._data;
    }
}
