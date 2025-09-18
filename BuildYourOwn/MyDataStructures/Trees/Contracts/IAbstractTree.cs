namespace MyDataStructures.Trees.Contracts
{
    using System.Collections.Generic;

    using MyDataStructures.Trees.Models;

    public interface IAbstractTree<T>
    {
        // Lab
        IEnumerable<T> OrderBfs();

        IEnumerable<T> OrderDfs();

        void AddChild(T parentKey, Tree<T> child);

        void AddChild(Tree<T> child);

        void RemoveNode(T nodeKey);

        void Swap(T firstKey, T secondKey);

        // Exercise
        IReadOnlyCollection<Tree<T>> Children { get; }

        string AsString();

        IEnumerable<T> GetLeafKeys();

        IEnumerable<T> GetInternalKeys();

        T GetDeepestKey();
    }
}
