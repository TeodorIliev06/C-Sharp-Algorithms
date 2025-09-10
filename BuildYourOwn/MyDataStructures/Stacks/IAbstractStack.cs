namespace MyDataStructures.Stacks
{
    using System.Collections.Generic;

    public interface IAbstractStack<T> : IEnumerable<T>
    {
        int Count { get; }

        void Push(T item);

        T Pop();

        T Peek();

        bool Contains(T item);
    }
}