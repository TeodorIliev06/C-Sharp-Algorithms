namespace MyDataStructures.Lists.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using MyDataStructures.Lists.Contracts;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public T Value { get; set; }

            public Node Next { get; set; }
            public Node Prev { get; set; }

            public Node(T value)
            {
                this.Value = value;
            }
        }

        private Node head;
        private Node tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node(item);

            if (this.Count == 0)
            {
                this.head = this.tail = newNode;
            }
            else
            {
                head.Prev = newNode;
                newNode.Next = head;
                head = newNode;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node(item);

            if (this.Count == 0)
            {
                this.head = this.tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.CheckIfEmpty();

            return this.head.Value;
        }

        public T GetLast()
        {
            this.CheckIfEmpty();

            return this.tail.Value;
        }

        public T RemoveFirst()
        {
            this.CheckIfEmpty();

            var oldHead = this.head;

            if (this.Count == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.head = oldHead.Next;

                this.head.Prev = null;
            }

            this.Count--;

            return oldHead.Value;
        }

        public T RemoveLast()
        {
            this.CheckIfEmpty();

            var oldTail = this.tail;

            if (this.Count == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.tail = oldTail.Prev;
                this.tail.Next = null;
            }

            this.Count--;

            return oldTail.Value;
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
                throw new InvalidOperationException("Stack is empty.");
            }
        }
    }
}