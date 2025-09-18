namespace MyDataStructures.Trees.Models
{
    using System;
    using System.Collections.Generic;

    using MyDataStructures.Trees.Contracts;

    public class Tree<T> : IAbstractTree<T>
    {
        private T value;
        private List<Tree<T>> children;

        public Tree(T value)
        {
            this.value = value;
            this.children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this.children.Add(child);
            }
        }

        public Tree<T> Parent { get; private set; }

        // Lab
        public void AddChild(T parentKey, Tree<T> child)
        {
            var parentTree = this.FindNodeWithBfs(parentKey);
            this.CheckIfEmpty(parentTree);

            parentTree.children.Add(child);
            child.Parent = parentTree;
        }

        public IEnumerable<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();
                result.Add(subtree.value);

                foreach (var child in subtree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public IEnumerable<T> OrderDfs()
        {
            var result = new List<T>();
            this.Dfs(this, result);

            return result;
        }

        public void RemoveNode(T nodeKey)
        {
            var node = this.FindNodeWithBfs(nodeKey);
            this.CheckIfEmpty(node);

            var parentNode = node.Parent;
            this.CheckIfRoot(parentNode);

            // Remove directly the subtree and its descendents
            parentNode.children.Remove(node);
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindNodeWithBfs(firstKey);
            this.CheckIfEmpty(firstNode);

            var secondNode = this.FindNodeWithBfs(secondKey);
            this.CheckIfEmpty(secondNode);

            var firstParentNode = firstNode.Parent;
            this.CheckIfRoot(firstParentNode);

            var secondParentNode = secondNode.Parent;
            this.CheckIfRoot(secondParentNode);

            // 3 cases:
            // 1. Swap two internal nodes
            // 2. Swap two leaves
            // 3. Swap internal with a leaf (leaf stays)

            var firstNodeIndex = firstParentNode.children.IndexOf(firstNode);
            var secondNodeIndex = secondParentNode.children.IndexOf(secondNode);

            firstParentNode.children[firstNodeIndex] = secondNode;
            secondNode.Parent = firstParentNode;

            secondParentNode.children[secondNodeIndex] = firstNode;
            firstNode.Parent = secondParentNode;
        }

        private void Dfs(Tree<T> node, List<T> result)
        {
            foreach (var child in node.children)
            {
                this.Dfs(child, result);
            }

            result.Add(node.value);
        }

        private Tree<T> FindNodeWithBfs(T key)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subtree = queue.Dequeue();

                if (subtree.value.Equals(key))
                {
                    return subtree;
                }

                foreach (var child in subtree.children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private void CheckIfEmpty(Tree<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
        }

        private void CheckIfRoot(Tree<T> parentNode)
        {
            if (parentNode == null)
            {
                throw new ArgumentException();
            }
        }

        // Exercise
        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }
    }
}
