namespace MyDataStructures.Lists.Models
{
    using MyDataStructures.Lists.Contracts;
    using System.Collections;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] items;

        public List() : this(DEFAULT_CAPACITY)
        {
        }

        public List(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this.items[index];
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
            if (this.Count == items.Length)
            {
                this.Grow();
            }

            this.items[Count++] = item;
        }


        public bool Contains(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.items[i]!.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.items[i];
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.items[i]!.Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);

            for (int i = this.Count; i > index; i--)
            {
                items[i] = items[i - 1];
            }

            this.items[index] = item;
            this.Count++;

            if (this.Count == items.Length)
            {
                this.Grow();
            }
        }

        public bool Remove(T item)
        {
            var index = this.IndexOf(item);

            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            for (int i = index; i < this.Count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            this.items[this.Count - 1] = default(T);
            this.Count--;

            // Shrink for performance optimization
            if (this.Count <= items.Length / 4)
            {
                this.Shrink();
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
            Array.Copy(items, newArray, Count);

            items = newArray;
        }

        private void Shrink()
        {
            var newArray = new T[this.Count / 2];
            Array.Copy(items, newArray, Count);

            items = newArray;
        }
    }
}