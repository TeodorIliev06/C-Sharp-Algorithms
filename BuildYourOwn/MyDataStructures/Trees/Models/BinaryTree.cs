namespace MyDataStructures.Trees.Models
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    using MyDataStructures.Trees.Contracts;

    public class BinaryTree<T>(T element,
        IAbstractBinaryTree<T> left,
        IAbstractBinaryTree<T> right) : IAbstractBinaryTree<T>
    {
        public T Value { get; private set; } = element;

        public IAbstractBinaryTree<T> LeftChild { get; private set; } = left;

        public IAbstractBinaryTree<T> RightChild { get; private set; } = right;

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
