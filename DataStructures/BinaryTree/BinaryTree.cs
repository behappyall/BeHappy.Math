using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.BinaryTree
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private TreeNode<T> _root;
        public event EventHandler<T> AddedNode;

        //private IComparer<T> _iTComparer;
        //public BinaryTree(IComparer<T> icomparer)
        //{
        //    _iTComparer = icomparer;
        //}

        public void Add(T data)
        {
            var nodeToAdd = new TreeNode<T>(data);
            if (_root == null)
                _root = nodeToAdd;
            else
            {
                var currentNode = _root;
                while (true)
                {
                    if (nodeToAdd.CompareTo(currentNode) > 0)
                    //if (_iTComparer.Compare(nodeToAdd, currentNode) > 0)
                    {
                        if (currentNode.RightChild == null)
                        {
                            nodeToAdd.Parent = currentNode;
                            currentNode.RightChild = nodeToAdd;
                            AddedNode?.Invoke(this, nodeToAdd);
                            return;
                        }
                        else
                            currentNode = currentNode.RightChild;
                    }
                    else
                    {
                        if (currentNode.LeftChild == null)
                        {
                            nodeToAdd.Parent = currentNode;
                            currentNode.LeftChild = nodeToAdd;
                            AddedNode?.Invoke(this, nodeToAdd);
                            return;
                        }
                        else
                            currentNode = currentNode.LeftChild;
                    }
                }
            }
        }

        //public bool TryRemove(T data)
        //{
        //    var currentNode = _root;
        //    while(true)
        //    {
        //        var compareResult = data.CompareTo(currentNode);
        //        if (compareResult==0)
        //            cu
        //    }
        //}

        public bool Find(T data)
        {
            var currentNode = _root;
            while (currentNode != null)
            {
                var compareResult = data.CompareTo(currentNode);
                if (compareResult == 0)
                    return true;
                else
                    currentNode = compareResult > 0 ? currentNode.RightChild : currentNode.LeftChild;
            }
            return false;
        }

        public T FindMin()
        {
            var currentNode = _root;
            while (true)
                if (currentNode.LeftChild == null)
                    return currentNode;
                else currentNode = currentNode.LeftChild;
        }
        public T FindMax()
        {
            var currentNode = _root;
            while (true)
                if (currentNode.RightChild == null)
                    return currentNode;
                else currentNode = currentNode.RightChild;
        }

        public IEnumerator<T> GetEnumerator() => _root.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _root.GetEnumerator();
    }
}
