namespace MyDataStructures.Queues.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using MyDataStructures.Queues.Contracts;

    public class Queue<T> : IAbstractQueue<T>
    {
        private class Node
        {
            public T Value { get; set; }

            public Node Next { get; set; }

            public Node(T value)
            {
                this.Value = value;
                this.Next = null;
            }

            public Node(T value, Node next) : this(value)
            {
                this.Next = next;
            }
        }

        private Node head;

        public int Count { get; private set; }

        public void Enqueue(T item)
        {
            var newNode = new Node(item);

            if (this.head == null)
            {
                this.head = newNode;
            }
            else
            {
                var node = this.head;

                while (node.Next != null)
                {
                    node = node.Next;
                }

                node.Next = newNode;
            }

            this.Count++;
        }

        public T Dequeue()
        {
            this.CheckIfEmpty();

            var oldHead = this.head;
            this.head = oldHead.Next;

            //oldHead = default;
            this.Count--;

            return oldHead.Value;
        }

        public T Peek()
        {
            this.CheckIfEmpty();

            return this.head.Value;
        }

        public bool Contains(T item)
        {
            var node = this.head;

            while (node != null)
            {
                if (node.Value.Equals(item))
                {
                    return true;
                }

                node = node.Next;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this.head;

            while (node != null)
            {
                yield return node.Value;

                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void CheckIfEmpty()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException("Queue is empty.");
            }
        }
    }
}