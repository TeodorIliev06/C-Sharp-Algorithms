namespace MyUnitTests.Trees
{
    using System;
    using MyDataStructures.Trees.Models;

    [TestFixture]
    public class TreeTests
    {
        private Tree<int> tree;

        [SetUp]
        public void InitializeTree()
        {
            this.tree = new Tree<int>(7,
                            new Tree<int>(19,
                                    new Tree<int>(1),
                                    new Tree<int>(12),
                                    new Tree<int>(31)),
                            new Tree<int>(21),
                            new Tree<int>(14,
                                    new Tree<int>(23),
                                    new Tree<int>(6)));
        }

        [Test]
        public void Constructor_WithValueOnly_ShouldWorkCorrectly()
        {
            var tree = new Tree<string>("Random Value");

            string[] expected = { "Random Value" };

            CollectionAssert.AreEqual(expected, tree.OrderBfs());
        }

        [Test]
        public void Constructor_WithSubtree_ShouldWorkCorrectly()
        {
            var tree = new Tree<string>("A", new Tree<string>("B"), new Tree<string>("C"));

            string[] expected = { "B", "C", "A" };

            CollectionAssert.AreEqual(expected, tree.OrderDfs());
        }

        [Test]
        public void OrderBfs_WithMultipleElements_ShouldReturnElementsInCorrectOrder()
        {
            int[] expected = { 7, 19, 21, 14, 1, 12, 31, 23, 6 };

            CollectionAssert.AreEqual(expected, this.tree.OrderBfs());
        }

        [Test]
        public void OrderBfs_WithSingleElement_ShouldReturnSingle()
        {
            int[] expected = { 1 };

            this.tree = new Tree<int>(1);

            CollectionAssert.AreEqual(expected, this.tree.OrderBfs());
        }

        [Test]
        public void OrderDfs_WithMultipleElements_ShouldReturnElementsInCorrectOrder()
        {
            int[] expected = { 1, 12, 31, 19, 21, 23, 6, 14, 7 };

            CollectionAssert.AreEqual(expected, this.tree.OrderDfs());
        }

        [Test]
        public void OrderDfs_WithSingleElement_ShouldReturnSingle()
        {
            int[] expected = { 1 };

            this.tree = new Tree<int>(1);

            CollectionAssert.AreEqual(expected, this.tree.OrderDfs());
        }

        [Test]
        public void AddChild_SmallSubtree_SubtreeAddedCorrectly()
        {
            this.tree.AddChild(1, new Tree<int>(-1, new Tree<int>(-2), new Tree<int>(-3)));
            int[] expected = { -2, -3, -1, 1, 12, 31, 19, 21, 23, 6, 14, 7 };

            CollectionAssert.AreEqual(expected, this.tree.OrderDfs());
        }

        [Test]
        public void AddChild_SingleNode_AddedCorrectly()
        {
            this.tree.AddChild(1, new Tree<int>(13));
            int[] expected = { 13, 1, 12, 31, 19, 21, 23, 6, 14, 7 };

            CollectionAssert.AreEqual(expected, this.tree.OrderDfs());
        }

        [Test]
        public void AddChild_OnInvalidElement_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(()
                => tree.AddChild(77, new Tree<int>(-1)));
        }

        [Test]
        public void RemoveNode_InternalNode_RemovesEntireSubtree()
        {
            int[] expected = { 7, 21, 14, 23, 6 };
            tree.RemoveNode(19);
            CollectionAssert.AreEqual(expected, tree.OrderBfs());
        }

        [Test]
        public void RemoveNode_LeafNode_RemovesTheLeaf()
        {
            int[] expected = { 7, 19, 14, 1, 12, 31, 23, 6 };

            tree.RemoveNode(21);

            var resultBfs = tree.OrderBfs();

            CollectionAssert.AreEqual(expected, resultBfs);
        }

        [Test]
        public void RemoveNode_RootNode_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => tree.RemoveNode(7));
        }

        [Test]
        public void RemoveNode_InvalidNode_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => tree.RemoveNode(77));
        }

        [Test]
        public void Swap_TwoLeafs_WorksCorrectly()
        {
            int[] expected = { 7, 19, 31, 14, 1, 12, 21, 23, 6 };
            tree.Swap(21, 31);

            CollectionAssert.AreEqual(expected, tree.OrderBfs());
        }

        [Test]
        public void Swap_TwoInternalNodes_WorksCorrectly()
        {
            int[] expected = { 23, 6, 14, 21, 1, 12, 31, 19, 7 };
            tree.Swap(19, 14);

            CollectionAssert.AreEqual(expected, tree.OrderDfs());
        }

        [Test]
        public void Swap_LeafAndInternalNode_WorksCorrectly()
        {
            int[] expected = { 21, 1, 12, 31, 19, 23, 6, 14, 7 };
            tree.Swap(19, 21);

            CollectionAssert.AreEqual(expected, tree.OrderDfs());
        }

        [Test]
        public void Swap_InternalNodeAndDescendantLeaf_WorksCorrectly()
        {
            int[] expected = { 31, 21, 23, 6, 14, 7 };
            tree.Swap(19, 31);

            CollectionAssert.AreEqual(expected, tree.OrderDfs());
        }

        [Test]
        public void Swap_LeftParamRootWithAnyNode_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => tree.Swap(7, 19));
        }

        [Test]
        public void Swap_RightParamRootWithAnyNode_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => tree.Swap(21, 7));
        }

        [Test]
        public void Swap_WithLeftParamInvalidNode_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => tree.Swap(34, 19));
        }

        [Test]
        public void Swap_WithRightParamInvalidNode_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => tree.Swap(21, 191));
        }

        [Test]
        public void AsString_WithBasicTree_ShouldWorkCorrectly()
        {
            string[] input = new string[]
               {
                "7 19", "7 21", "7 14", "19 1", "19 12", "19 31", "14 23", "14 6"
               };

            var tree = new IntegerTreeFactory().CreateTreeFromStrings(input);

            string expectedOutput =
                $"7{Environment.NewLine}" +
                $"  19{Environment.NewLine}" +
                $"    1{Environment.NewLine}" +
                $"    12{Environment.NewLine}" +
                $"    31{Environment.NewLine}" +
                $"  21{Environment.NewLine}" +
                $"  14{Environment.NewLine}" +
                $"    23{Environment.NewLine}" +
                $"    6";

            Assert.AreEqual(expectedOutput, tree.AsString());
        }

        [Test]
        [TestCaseSource("TreeTestData")]
        public void GetLeafKeys_ShouldWorkCorrectly(TreeTestData data)
        {
            int[] expected = data.expectedLeafNodes;
            IEnumerable<int> leafKeys = data.tree.GetLeafKeys();


            CollectionAssert.AreEquivalent(expected, leafKeys);
        }

        [Test]
        [TestCaseSource("TreeTestData")]
        public void GetInternalKeys_ShouldWorkCorrectly(TreeTestData data)
        {
            int[] expected = data.expectedInternalNodes;
            IEnumerable<int> middleKeys = data.tree.GetInternalKeys();

            CollectionAssert.AreEquivalent(expected, middleKeys);
        }

        [Test]
        [TestCaseSource("TreeTestData")]
        public void GetDeepestKey_ShouldWorkCorrectly(TreeTestData data)
        {
            int deepestKey = data.tree.GetDeepestKey();

            Assert.AreEqual(data.expectedDeepestNode, deepestKey);
        }


        [Test]
        [TestCaseSource("TreeTestData")]
        public void GetLongestPathToRoot_ShouldWorkCorrectly(TreeTestData data)
        {
            int[] expectedPath = data.expectedLongestPath;
            IEnumerable<int> longestLeftmostPath = data.tree.GetLongestPathToRoot();

            CollectionAssert.AreEqual(expectedPath, longestLeftmostPath);
        }

        public static IEnumerable<TreeTestData> TreeTestData()
        {
            yield return new TreeTestData(
                "First tree",
                new string[] { "9 17", "9 4", "9 14", "4 36", "14 53", "14 59", "53 67", "53 73" },
                new int[] { 17, 36, 67, 73, 59 },
                new int[] { 4, 14, 53 },
                67,
                new int[] { 9, 14, 53, 67 }
            );
            yield return new TreeTestData(
                "Second tree",
                new string[] { "2 5", "2 11", "2 18", "11 38", "38 87", "18 72" },
                new int[] { 5, 87, 72 },
                new int[] { 11, 38, 18 },
                87,
                new int[] { 2, 11, 38, 87 }
            );
            yield return new TreeTestData(
                "Third tree",
                new string[] { "35 23", "35 93", "23 19", "23 41", "93 42", "93 43", "93 44", "93 45" },
                new int[] { 19, 41, 42, 43, 44, 45 },
                new int[] { 23, 93 },
                19,
                new int[] { 35, 23, 19 }
            );
            yield return new TreeTestData(
                "Fourth tree",
                new string[] { "3 9", "3 2", "3 35", "9 17", "9 4", "9 14", "4 36", "14 53", "14 59", "53 67", "53 73", "2 5", "2 11", "2 18", "11 38", "38 87", "18 72", "35 93", "35 23", "23 19", "23 41", "93 42", "93 43", "93 44", "93 45" },
                new int[] { 17, 36, 67, 73, 59, 5, 87, 72, 19, 41, 42, 43, 44, 45 },
                new int[] { 9, 2, 35, 4, 14, 53, 11, 38, 18, 23, 93 },
                67,
                new int[] { 3, 9, 14, 53, 67 }
            );
        }
    }

    public class TreeTestData
    {
        private string testName;
        public Tree<int> tree;
        public int[] expectedLeafNodes;
        public int[] expectedInternalNodes;
        public int expectedDeepestNode;
        public int[] expectedLongestPath;

        public TreeTestData(string testName, string[] pairs, int[] leafNodes, int[] internalNodes, int deepestNode, int[] longestPath)
        {
            this.tree = new IntegerTreeFactory().CreateTreeFromStrings(pairs);
            this.expectedLeafNodes = leafNodes;
            this.expectedInternalNodes = internalNodes;
            this.expectedDeepestNode = deepestNode;
            this.expectedLongestPath = longestPath;

            this.testName = testName;
        }

        public override string ToString()
        {
            return testName;
        }
    }
}