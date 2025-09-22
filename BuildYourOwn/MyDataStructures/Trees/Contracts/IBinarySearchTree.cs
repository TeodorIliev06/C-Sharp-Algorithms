namespace MyDataStructures.Trees.Contracts
{
    using System;

    public interface IBinarySearchTree<T> where T : IComparable<T>
    {
        // Lab
        void Insert(T element);

        bool Contains(T element);

        void EachInOrder(Action<T> action);

        IBinarySearchTree<T> Search(T element);

        // Exercise
        void Delete(T element);

        void DeleteMin();

        void DeleteMax();

        int Count();

        int Rank(T element);

        T Select(int rank);

        T Ceiling(T element);

        T Floor(T element);

        IEnumerable<T> Range(T startRange, T endRange);
    }
}