namespace MyDataStructures.Stacks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private class Node
        {
            public T Value { get; set; }

            public Node Next { get; set; }

            public Node(T value)
            {
                Value = value;
                Next = null;
            }

            public Node(T value, Node next) : this(value)
            {
                Next = next;
            }
        }

        private Node top;

        public int Count { get; private set; }

        public void Push(T item)
        {
            var newTop = new Node(item, top);

            top = newTop;

            Count++;
        }

        public T Pop()
        {
            CheckIfEmpty();

            var oldTop = top;
            top = oldTop.Next;

            Count--;

            return oldTop.Value;
        }

        public T Peek()
        {
            CheckIfEmpty();

            return top.Value;
        }

        public bool Contains(T item)
        {
            var node = top;

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
            var node = top;

            while (node != null)
            {
                yield return node.Value;

                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void CheckIfEmpty()
        {
            if (top == null)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
        }
    }
}