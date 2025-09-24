namespace MyDataStructures.Queues.Models
{
    using System;
    using System.Collections.Generic;

    using MyDataStructures.Heaps.Models;

    public class PriorityQueue<T> : MinHeap<T> 
        where T : IComparable<T>
    {
        private Dictionary<T, int> indicesByKeys = new Dictionary<T, int>();
        public PriorityQueue()
        {
            this.elements = new List<T>();
        }

        public void Enqueue(T element)
        {
            this.elements.Add(element);
            this.indicesByKeys.Add(element, this.Size - 1);

            this.HeapifyUp(this.Size - 1);
        }

        public T Dequeue()
        {
            var elementToRemove = this.ExtractMin();
            this.indicesByKeys.Remove(elementToRemove);

            return elementToRemove;
        }

        public void DecreaseKey(T key)
        {
            var index = this.indicesByKeys[key];

            this.HeapifyUp(index);
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = (index - 1) / 2;

            while (index > 0 && IsLesser(index, parentIndex))
            {
                // Swap indices as well
                this.indicesByKeys[this.elements[index]] = index;
                this.indicesByKeys[this.elements[parentIndex]] = parentIndex;

                (this.elements[index], this.elements[parentIndex]) =
                    (this.elements[parentIndex], this.elements[index]);

                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }
    }
}
