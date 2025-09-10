namespace MyDataStructures.Queues.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using MyDataStructures.Queues.Contracts;

    public class Stack<T> : IAbstractStack<T>
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

        private Node top;

        public int Count { get; private set; }

        public void Push(T item)
        {
            var newTop = new Node(item, this.top);

            this.top = newTop;

            this.Count++;
        }

        public T Pop()
        {
            this.CheckIfEmpty();

            var oldTop = this.top;
            this.top = oldTop.Next;

            this.Count--;

            return oldTop.Value;
        }

        public T Peek()
        {
            this.CheckIfEmpty();

            return this.top.Value;
        }

        public bool Contains(T item)
        {
            var node = this.top;

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
            var node = this.top;

            while (node != null)
            {
                yield return node.Value;

                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void CheckIfEmpty()
        {
            if (this.top == null)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
        }
    }
}