namespace MyDataStructures.Lists.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using MyDataStructures.Lists.Contracts;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);

                // We are using the reversed items at that index
                var reversedIndex = this.Count - 1 - index;
                return this.items[reversedIndex];
            }
            set
            {
                this.ValidateIndex(index);
                this.items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (this.Count == this.items.Length)
            {
                this.Grow();
            }

            this.items[this.Count++] = item;
        }

        public bool Contains(T item)
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                if (this.items[i].Equals(item))
                {
                    return true;
                }
            }

            return false;

            //return this.IndexOf(item) != -1;
        }

        public int IndexOf(T item)
        {
            // We need to traverse normally for correct indexing
            // But we decrement the array elements
            for (int i = 0; i < this.Count; i++)
            {
                if (this.items[this.Count - 1 - i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);

            // Again, reversed index
            // meaning we stop at Count - index

            if (this.Count == this.items.Length)
            {
                this.Grow();
            }

            var reversedIndex = this.Count - index;
            for (int i = this.Count; i > reversedIndex; i--)
            {
                this.items[i] = this.items[i - 1];
            }

            this.items[reversedIndex] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            var index = this.IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            this.RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            // We start from the back, hence:
            // Count - 1 - index
            var reversedIndex = this.Count - 1 - index;
            for (int i = reversedIndex; i < this.Count; i++)
            {
                items[i] = items[i + 1];
            }

            this.items[this.Count - 1] = default(T);

            this.Count--;

            if (this.Count <= this.items.Length / 4)
            {
                this.Shrink();
            }

        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
        }

        private void Grow()
        {
            var newArray = new T[this.Count * 2];

            Array.Copy(this.items, newArray, this.Count);

            this.items = newArray;
        }

        private void Shrink()
        {
            var newArray = new T[this.Count / 2];

            Array.Copy(this.items, newArray, this.Count);

            this.items = newArray;
        }
    }
}