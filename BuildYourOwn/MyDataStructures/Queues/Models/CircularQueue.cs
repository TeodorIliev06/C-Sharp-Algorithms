namespace MyDataStructures.Queues.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using MyDataStructures.Queues.Contracts;

    public class CircularQueue<T> : IAbstractCircularQueue<T>
    {
        private const int DEFAULT_CAPACITY = 4;

        private T[] items;
        private int endIndex;
        private int startIndex;

        public CircularQueue(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            this.items = new T[capacity];
        }

        public CircularQueue() : this(DEFAULT_CAPACITY)
        {
        }

        public int Count { get; private set; }

        public T Dequeue()
        {
            this.CheckIfEmpty();

            var firstElement = this.items[this.startIndex];
            this.items[this.startIndex] = default(T);
            this.startIndex = (this.startIndex + 1) % this.items.Length;

            this.Count--;

            if (this.Count <= this.items.Length / 4)
            {
                this.Shrink();
            }

            return firstElement;
        }

        public void Enqueue(T item)
        {
            if (this.Count >= this.items.Length)
            {
                this.Grow();
            }

            // Use modular division to move index back to start
            // if we add elements beyond capacity
            this.items[this.endIndex] = item;
            this.endIndex = (this.endIndex + 1) % this.items.Length;

            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.items[(startIndex + i) % this.items.Length];
            }
        }

        public T Peek()
        {
            this.CheckIfEmpty();

            return this.items[startIndex];
        }

        public T[] ToArray()
        {
            return this.CopyElements(this.Count);
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void Grow()
        {
            var newArray = this.CopyElements(this.items.Length * 2);

            this.items = newArray;

            // new array -> re-order indices of copied array
            this.startIndex = 0;
            this.endIndex = Count;
        }

        private void Shrink()
        {
            var newArray = this.CopyElements(this.items.Length / 2);

            this.items = newArray;

            this.startIndex = 0;
            this.endIndex = Count;
        }

        private T[] CopyElements(int size)
        {
            var newArr = new T[size];

            for (int i = 0; i < this.Count; i++)
            {
                newArr[i] = this.items[(this.startIndex + i) % this.items.Length];
            }

            return newArr;
        }

        private void CheckIfEmpty()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Circular Queue is empty");
            }
        }
    }

}
