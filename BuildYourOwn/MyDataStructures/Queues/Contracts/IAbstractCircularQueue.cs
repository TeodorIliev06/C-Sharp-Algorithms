namespace MyDataStructures.Queues.Contracts
{
    public interface IAbstractCircularQueue<T> : IEnumerable<T>
    {
        int Count { get; }

        void Enqueue(T item);

        T Dequeue();

        T Peek();

        T[] ToArray();
    }
}
