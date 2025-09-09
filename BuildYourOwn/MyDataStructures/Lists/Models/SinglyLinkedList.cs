namespace MyDataStructures.Lists.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using MyDataStructures.Lists.Contracts;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
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

        public void AddFirst(T item)
        {
            // Option 1: without 2nd parameter in constructor
            //var newNode = new Node(item);

            //var oldHead = this.head;
            //head = newNode;

            //head.Next = oldHead;

            // Option 2: more concise
            var newNode = new Node(item, this.head);

            head = newNode;
            this.Count++;
        }

        public void AddLast(T item)
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

        public IEnumerator<T> GetEnumerator()
        {
            var node = this.head;

            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        public T GetFirst()
        {
            this.CheckIfEmpty();

            return head.Value;
        }

        public T GetLast()
        {
            this.CheckIfEmpty();

            var node = this.head;

            while (node.Next != null)
            {
                node = node.Next;
            }

            return node.Value;
        }

        public T RemoveFirst()
        {
            this.CheckIfEmpty();

            var oldHead = this.head;
            this.head = oldHead.Next;

            // Leave memory leaks handling to GC
            // oldHead = default(T);

            this.Count--;

            return oldHead.Value;
        }

        public T RemoveLast()
        {
            this.CheckIfEmpty();

            var node = this.head;
            Node last = null;
            
            // If we have 1 element
            if (node.Next == null)
            {
                last = this.head;
                this.head = null;
            }
            else
            {
                while (node != null)
                {
                    // If we have 1+ elements
                    if (node.Next.Next == null)
                    {
                        last = node.Next;
                        node.Next = null;
                    }

                    node = node.Next;
                }
            }

            this.Count--;

            return last.Value;
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