namespace MyDataStructures.Heaps.Models
{
    using System;

    using MyDataStructures.Heaps.Contracts;

    public class MaxHeap<T> : IMaxHeap<T> 
        where T : IComparable<T>
    {
        private List<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);

            this.HeapifyUp(this.elements.Count - 1);
        }

        public T ExtractMax()
        {
            this.CheckIfEmpty();

            var maxNode = this.elements[0];

            (this.elements[0], this.elements[this.Size - 1]) =
                (this.elements[this.Size - 1], this.elements[0]);

            this.elements.RemoveAt(this.Size - 1);

            this.HeapifyDown(0);

            return maxNode;
        }

        public T Peek()
        {
            this.CheckIfEmpty();

            return this.elements[0];
        }

        private void CheckIfEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException(nameof(this.Size));
            }
        }

        private void HeapifyUp(int index)
        {
            // Find parent, check if swap is needed, repeat
            int parentIndex = (index - 1) / 2;

            while (index > 0 && this.IsGreater(index, parentIndex))
            {
                (this.elements[index], this.elements[parentIndex]) =
                    (this.elements[parentIndex], this.elements[index]);

                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private void HeapifyDown(int index)
        {
            var biggerChildIndex = this.GetBiggerChildIndex(index);

            // Sink the node while the child is greater
            while (biggerChildIndex > 0 && this.IsGreater(biggerChildIndex, index))
            {
                (this.elements[index], this.elements[biggerChildIndex]) =
                    (this.elements[biggerChildIndex], this.elements[index]);

                index = biggerChildIndex;
                biggerChildIndex = this.GetBiggerChildIndex(index);
            }
        }

        private int GetBiggerChildIndex(int index)
        {
            var firstChildIndex = index * 2 + 1;
            var secondChildIndex = index * 2 + 2;

            // Both could be bigger than current parent
            if (secondChildIndex < this.Size)
            {
                if (this.IsGreater(firstChildIndex, secondChildIndex))
                {
                    return firstChildIndex;
                }

                return secondChildIndex;
            }
            else if (firstChildIndex < this.Size)
            {
                return firstChildIndex;
            }
            else
            {
                return -1;
            }
        }

        private bool IsGreater(int firstIndex, int secondIndex)
        {
            return this.elements[firstIndex].CompareTo(this.elements[secondIndex]) > 0;
        }
    }
}
