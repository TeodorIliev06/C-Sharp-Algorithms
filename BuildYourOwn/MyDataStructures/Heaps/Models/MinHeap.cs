namespace MyDataStructures.Heaps.Models
{
    using System;
    using System.Collections.Generic;

    using MyDataStructures.Heaps.Contracts;

    public class MinHeap<T> : IMinHeap<T>
        where T : IComparable<T>
    {
        protected List<T> elements;

        public MinHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);

            this.HeapifyUp(this.Size - 1);
        }

        public T ExtractMin()
        {
            this.CheckIfEmpty();

            var minNode = this.elements[0];
            (this.elements[0], this.elements[this.Size - 1]) =
                (this.elements[this.Size - 1], this.elements[0]);

            this.elements.RemoveAt(this.Size - 1);

            this.HeapifyDown(0);

            return minNode;
        }

        public T Peek()
        {
            this.CheckIfEmpty();

            return this.elements[0];
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = (index - 1) / 2;

            while (index > 0 && IsLesser(index, parentIndex))
            {
                (this.elements[index], this.elements[parentIndex]) =
                    (this.elements[parentIndex], this.elements[index]);

                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        protected bool IsLesser(int index, int parentIndex)
        {
            return this.elements[index].CompareTo(this.elements[parentIndex]) < 0;
        }

        private void HeapifyDown(int index)
        {
            var smallerChildIndex = this.GetSmallerChildIndex(index);

            while (smallerChildIndex > 0 && this.IsLesser(smallerChildIndex, index))
            {
                (this.elements[index], this.elements[smallerChildIndex]) = 
                    (this.elements[smallerChildIndex], this.elements[index]);

                index = smallerChildIndex;
                smallerChildIndex = this.GetSmallerChildIndex(index);
            }
        }

        private int GetSmallerChildIndex(int index)
        {
            var firstChildIndex = index * 2 + 1;
            var secondChildIndex = index * 2 + 2;

            if (secondChildIndex < this.Size)
            {
                if (this.IsLesser(firstChildIndex, secondChildIndex))
                {
                    return firstChildIndex;
                }

                return secondChildIndex;
            }

            if (firstChildIndex < this.Size)
            {
                return firstChildIndex;
            }

            return -1;
        }

        private void CheckIfEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException(nameof(this.Size));
            }
        }
    }
}
