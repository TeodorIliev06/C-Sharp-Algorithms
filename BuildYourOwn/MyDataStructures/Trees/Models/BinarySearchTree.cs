namespace MyDataStructures.Trees.Models
{
    using System;

    using MyDataStructures.Trees.Contracts;

    public class BinarySearchTree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; set; }

            public Node Left { get; set; }
            public Node Right { get; set; }

            public int Count { get; set; }
        }

        // Creating a new tree means we have to copy the nodes positioning
        public BinarySearchTree()
        {

        }

        private BinarySearchTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        private Node root;

        // Lab
        public bool Contains(T element)
        {
            return this.FindNode(element) != null;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(action, this.root);
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        public IBinarySearchTree<T> Search(T element)
        {
            var node = this.FindNode(element);

            if (node == null)
            {
                return null;
            }

            return new BinarySearchTree<T>(node);
        }

        // Exercise
        public void Delete(T element)
        {
            CheckIfNull(this.root);

            this.root = this.Delete(this.root, element);
        }

        public void DeleteMin()
        {
            CheckIfNull(this.root);

            this.root = this.DeleteMin(this.root);
        }

        public void DeleteMax()
        {
            CheckIfNull(this.root);

            this.root = this.DeleteMax(this.root);
        }

        public int Count()
        {
            return this.Count(this.root);
        }

        public int Rank(T element)
        {
            return this.Rank(this.root, element);
        }

        public T Select(int rank)
        {
            CheckIfNull(this.root);

            Node node = this.Select(this.root, rank);
            CheckIfNull(node);

            return node.Value;
        }

        public T Ceiling(T element)
        {
            return this.Select(this.Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return this.Select(this.Rank(element) - 1);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            var queue = new Queue<T>();

            this.Range(this.root, startRange, endRange, queue);

            return queue;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);

            this.PreOrderCopy(node.Left);

            this.PreOrderCopy(node.Right);
        }

        private Node Insert(T element, Node node)
        {
            // Left is always smaller
            // Right is always larger
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }

            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return node;
        }

        private void EachInOrder(Action<T> action, Node node)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(action, node.Left);

            action(node.Value);

            this.EachInOrder(action, node.Right);
        }

        private Node FindNode(T element)
        {
            var node = this.root;

            while (node != null)
            {
                if (element.CompareTo(node.Value) < 0)
                {
                    node = node.Left;
                }
                else if (element.CompareTo(node.Value) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    break;
                }
            }
            return node;
        }

        private Node Delete(Node node, T element)
        {
            if (node == null)
            {
                return null;
            }

            var comparisonValue = element.CompareTo(node.Value);

            if (comparisonValue < 0)
            {
                node.Left = this.Delete(node.Left, element);
            }
            else if (comparisonValue > 0)
            {
                node.Right = this.Delete(node.Right, element);
            }

            /*
                Found the element:
                1. No children → remove the node (return null).
                2. One child → promote that child.
                3. Two children:
                    Keep tree shape, replace the node’s value with 3.1 or 3.2:
                        3.1 the smallest value in the right subtree (in-order successor)
                        3.2 the largest value in the left subtree (in-order predecessor)
                    Then, recursively delete that successor/predecessor node.
            */
            else
            {
                if (node.Left == null)
                {
                    return node.Right;
                }

                if (node.Right == null)
                {
                    return node.Left;
                }

                var descendant = this.FindMin(node.Right);

                node.Value = descendant.Value;

                node.Right = this.Delete(node.Right, descendant.Value);
            }

            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return node;
        }

        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return this.FindMin(node.Left);
        }

        private Node DeleteMin(Node node)
        {
            // Leftmost element does not have a left child
            // Promote right child (if null -> set null)
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = this.DeleteMin(node.Left);
            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return node;
        }

        private Node DeleteMax(Node node)
        {
            // Reversed logic of deleting the min element
            if (node.Right == null)
            {
                return node.Left;
            }

            node.Right = this.DeleteMax(node.Right);
            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return node;
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private int Rank(Node node, T element)
        {
            if (node == null)
            {
                return 0;
            }

            if (element.CompareTo(node.Value) < 0)
            {
                return this.Rank(node.Left, element);
            }

            // Traversing right means we add current node and the sum of the left subtree.
            if (element.CompareTo(node.Value) > 0)
            {
                return 1 + this.Count(node.Left) + this.Rank(node.Right, element);
            }

            return this.Count(node.Left);
        }

        private Node Select(Node node, int rank)
        {
            if (node == null)
            {
                return null;
            }

            int leftCount = this.Count(node.Left);

            if (leftCount == rank)
            {
                return node;
            }

            if (leftCount > rank)
            {
                return this.Select(node.Left, rank);
            }

            return this.Select(node.Right, rank - (leftCount + 1));
        }

        private void Range(Node node, T startRange, T endRange, Queue<T> queue)
        {
            if (node == null)
            {
                return;
            }

            if (startRange.CompareTo(node.Value) < 0)
            {
                this.Range(node.Left, startRange, endRange, queue);
            }

            if (startRange.CompareTo(node.Value) <= 0 &&
                endRange.CompareTo(node.Value) >= 0)
            {
                queue.Enqueue(node.Value);
            }

            if (endRange.CompareTo(node.Value) > 0)
            {
                this.Range(node.Right, startRange, endRange, queue);
            }
        }

        private static void CheckIfNull(Node node)
        {
            if (node == null)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
