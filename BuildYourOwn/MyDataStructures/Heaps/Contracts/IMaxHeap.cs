namespace MyDataStructures.Heaps.Contracts
{
    public interface IMaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        T ExtractMax();
    }
}
