namespace MyDataStructures.Trees.Models
{
    using System.Collections.Generic;

    using MyDataStructures.Trees.Contracts;

    public class IntegerTree(int key, params Tree<int>[] children)
        : Tree<int>(key, children), IIntegerTree
    {
        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            var result = new List<List<int>>();
            var currentPath = new LinkedList<int>();
            currentPath.AddFirst(this.Value);

            var currentSum = this.Value;

            this.GetPathsWithGivenSumDfs(this, currentPath, result, ref currentSum, sum);

            return result;
        }

        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
        {
            var result = new List<Tree<int>>();
            var queue = new Queue<Tree<int>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var currentSubtree = queue.Dequeue();
                var currentSubtreeSum = this.GetSubtreeSum(currentSubtree);

                if (currentSubtreeSum == sum)
                {
                    result.Add(currentSubtree);
                }

                foreach (var child in currentSubtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        private void GetPathsWithGivenSumDfs(Tree<int> node, LinkedList<int> currentPath,
            List<List<int>> result, ref int currentSum, int targetSum)
        {
            foreach (var child in node.Children)
            {
                currentSum += child.Value;
                currentPath.AddLast(child.Value);

                this.GetPathsWithGivenSumDfs(child, currentPath, result, ref currentSum, targetSum);
            }

            // LinkedList keeps a reference
            // We do not want to modify already added paths in result set
            if (currentSum == targetSum)
            {
                result.Add(new List<int>(currentPath));
            }

            currentSum -= node.Value;
            currentPath.RemoveLast();
        }

        private int GetSubtreeSum(Tree<int> subtree)
        {
            var sum = 0;
            var queue = new Queue<Tree<int>>();
            queue.Enqueue(subtree);

            while (queue.Count > 0)
            {
                var currentSubtree = queue.Dequeue();
                sum += currentSubtree.Value;

                foreach (var child in currentSubtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return sum;
        }
    }
}
