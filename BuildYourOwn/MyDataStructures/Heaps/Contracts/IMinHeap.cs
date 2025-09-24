namespace MyDataStructures.Heaps.Contracts
{
    using System;

    public interface IMinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        T ExtractMin();
    }
}
