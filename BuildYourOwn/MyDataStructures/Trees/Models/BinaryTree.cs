namespace MyDataStructures.Trees.Models
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    using MyDataStructures.Trees.Contracts;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;

            if (leftChild != null)
            {
                this.LeftChild.Parent = this;
            }

            if (rightChild != null)
            {
                this.RightChild.Parent = this;
            }
        }

        public T Value { get; }

        public IAbstractBinaryTree<T> LeftChild { get; }

        public IAbstractBinaryTree<T> RightChild { get; }

        public IAbstractBinaryTree<T> Parent { get; set; }

        public string AsIndentedPreOrder(int indent)
        {
            var sb = new StringBuilder();

            this.PreOrderDfs(this, sb, indent);

            return sb.ToString().Trim();
        }

        public void ForEachInOrder(Action<T> action)
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.ForEachInOrder(action);
            }

            action(this.Value);

            if (this.RightChild != null)
            {
                this.RightChild.ForEachInOrder(action);
            }
        }

        public T FindLowestCommonAncestor(T first, T second)
        {
            /*
             TODO:
                1. Get all parents of first and second node
                2. Intersect them and return the first element
             */

            var firstNodeAncestors = this.GetAncestors(first);
            var secondNodeAncestors = this.GetAncestors(second);

            return firstNodeAncestors.Intersect(secondNodeAncestors).First().Value;
        }

        private Queue<BinaryTree<T>> GetAncestors(T element)
        {
            var node = this.FindNodeBfs(element, this);

            if (node == null)
            {
                throw new InvalidOperationException();
            }

            var ancestors = new Queue<BinaryTree<T>>();
            while (node != null)
            {
                ancestors.Enqueue(node);
                node = (BinaryTree<T>)node.Parent;
            }

            return ancestors;
        }

        private BinaryTree<T> FindNodeBfs(T element, BinaryTree<T> node)
        {
            var queue = new Queue<BinaryTree<T>>();
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();

                if (element.Equals(subtree.Value))
                {
                    return subtree;
                }

                if (subtree.LeftChild != null)
                {
                    queue.Enqueue((BinaryTree<T>)subtree.LeftChild);
                }

                if (subtree.RightChild != null)
                {
                    queue.Enqueue((BinaryTree<T>)subtree.RightChild);
                }
            }

            return null;
        }

        // L -> S -> R
        public IEnumerable<IAbstractBinaryTree<T>> InOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();
            
            if (this.LeftChild != null)
            {
                result.AddRange(this.LeftChild.InOrder());
            }

            result.Add(this);

            if (this.RightChild != null)
            {
                result.AddRange(this.RightChild.InOrder());
            }

            return result;
        }

        // L -> R -> S
        public IEnumerable<IAbstractBinaryTree<T>> PostOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            if (this.LeftChild != null)
            {
                result.AddRange(this.LeftChild.PostOrder());
            }

            if (this.RightChild != null)
            {
                result.AddRange(this.RightChild.PostOrder());
            }

            result.Add(this);

            return result;
        }

        // S -> L -> R
        public IEnumerable<IAbstractBinaryTree<T>> PreOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            result.Add(this);

            if (this.LeftChild != null)
            {
                result.AddRange(this.LeftChild.PreOrder());
            }

            if (this.RightChild != null)
            {
                result.AddRange(this.RightChild.PreOrder());
            }

            return result;
        }

        private void PreOrderDfs(IAbstractBinaryTree<T> node, StringBuilder sb, int indent)
        {
            sb.Append(new string(' ', indent))
                .AppendLine(node.Value.ToString());

            if (node.LeftChild != null)
            {
                this.PreOrderDfs(node.LeftChild, sb, indent + 2);
            }

            if (node.LeftChild != null)
            {
                this.PreOrderDfs(node.RightChild, sb, indent + 2);
            }
        }
    }
}
