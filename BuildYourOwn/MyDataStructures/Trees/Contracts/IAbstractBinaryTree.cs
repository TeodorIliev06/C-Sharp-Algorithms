namespace MyDataStructures.Trees.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IAbstractBinaryTree<T>
    {
        T Value { get; }

        IAbstractBinaryTree<T> LeftChild { get; }

        IAbstractBinaryTree<T> RightChild { get; }

        IAbstractBinaryTree<T> Parent { get; set; }

        string AsIndentedPreOrder(int indent);

        IEnumerable<IAbstractBinaryTree<T>> PreOrder();

        IEnumerable<IAbstractBinaryTree<T>> InOrder();

        IEnumerable<IAbstractBinaryTree<T>> PostOrder();

        void ForEachInOrder(Action<T> action);

        T FindLowestCommonAncestor(T first, T second);
    }
}
