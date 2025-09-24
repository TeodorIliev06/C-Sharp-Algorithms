namespace MyUnitTests.Trees
{
    using System;
    using System.Text;
    using System.Linq;

    using NUnit.Framework;

    using MyDataStructures.Trees.Models;
    using MyDataStructures.Trees.Contracts;

    public class BinaryTreeTests
    {
        private IAbstractBinaryTree<int> tree;
        private IAbstractBinaryTree<int> lcaTree;

        [SetUp]
        public void InitializeBinaryTree()
        {
            this.tree = new BinaryTree<int>(
                            17,
                            new BinaryTree<int>(
                                9,
                                new BinaryTree<int>(3, null, null),
                                new BinaryTree<int>(11, null, null)
                            ),
                            new BinaryTree<int>(
                                25,
                                new BinaryTree<int>(20, null, null),
                                new BinaryTree<int>(31, null, null)
                            )
            );

            this.lcaTree = new BinaryTree<int>(
                12,
                new BinaryTree<int>(5,
                    new BinaryTree<int>(1, null, null),
                    new BinaryTree<int>(8, null, null)
                ),
                new BinaryTree<int>(19,
                    new BinaryTree<int>(16, null, null),
                    new BinaryTree<int>(23,
                        new BinaryTree<int>(21, null, null),
                        new BinaryTree<int>(30, null, null)
                    )
                )
            );
        }

        [Test]
        public void TestAsIndentedPreOrder()
        {
            string indentedPreOrder = this.tree.AsIndentedPreOrder(0);
            Assert.AreEqual(
                $"17{Environment.NewLine}" +
                $"  9{Environment.NewLine}" +
                $"    3{Environment.NewLine}" +
                $"    11{Environment.NewLine}" +
                $"  25{Environment.NewLine}" +
                $"    20{Environment.NewLine}" +
                 "    31", indentedPreOrder.TrimEnd());
        }

        [Test]
        public void TestPreOrder()
        {
            var trees = this.tree.PreOrder().ToList();
            int[] expected = { 17, 9, 3, 11, 25, 20, 31 };
            Assert.AreEqual(expected.Length, trees.Count);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], trees[i].Value);
            }
        }


        [Test]
        public void TestInOrder()
        {
            var trees = this.tree.InOrder().ToList();
            int[] expected = { 3, 9, 11, 17, 20, 25, 31 };
            Assert.AreEqual(expected.Length, trees.Count);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], trees[i].Value);
            }
        }


        [Test]
        public void TestPostOrder()
        {
            var trees = this.tree.PostOrder().ToList();
            int[] expected = { 3, 11, 9, 20, 31, 25, 17 };
            Assert.AreEqual(expected.Length, trees.Count);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], trees[i].Value);
            }
        }

        [Test]
        public void TestForEachInOrder()
        {
            string expected = "3, 9, 11, 17, 20, 25, 31";
            StringBuilder builder = new StringBuilder();

            this.tree.ForEachInOrder(key => builder.Append(key).Append(", "));
            string actual = builder.ToString();

            Assert.AreEqual(expected, actual.Substring(0, actual.LastIndexOf(", ")));
        }

        [Test]
        [TestCase(19, 16, 21)]
        [TestCase(12, 1, 21)]
        [TestCase(23, 21, 30)]
        [TestCase(23, 23, 30)]
        [TestCase(12, 23, 8)]
        [TestCase(19, 19, 23)]
        public void FindLowestCommonAncestor_WithExistingNodes_ReturnsCorrectAncestor(int expected, int firstNode, int secondNode)
        {
            Assert.AreEqual(expected, this.lcaTree.FindLowestCommonAncestor(firstNode, secondNode));
        }

        [Test]
        [TestCase(5, 54)]
        [TestCase(54, 5)]
        [TestCase(51, 54)]
        public void FindLowestCommonAncestor_WithInvalidNode_ThrowsException(int firstNode, int secondNode)
        {
            Assert.Throws<InvalidOperationException>(() => this.lcaTree.FindLowestCommonAncestor(firstNode, secondNode));
        }

        [Test]
        public void TestFirstTopView()
        {
            var binaryTree =
                new BinaryTree<int>(1,
                        new BinaryTree<int>(2,
                                new BinaryTree<int>(4, null, null),
                                new BinaryTree<int>(5, null, null)),
                        new BinaryTree<int>(3,
                                new BinaryTree<int>(6, null, null),
                                new BinaryTree<int>(7, null, null)));

            var topViewActual = binaryTree.TopView()
                .OrderBy(n => n)
                .ToArray();

            int[] topViewExpected = { 1, 2, 3, 4, 7 };

            Assert.AreEqual(topViewExpected.Length, topViewActual.Length);
            for (int i = 0; i < topViewExpected.Length; i++)
            {
                Assert.AreEqual(topViewExpected[i], topViewActual[i]);
            }
        }

        [Test]
        public void TestSecondTopView()
        {
            var binaryTree =
              new BinaryTree<int>(1,
                      new BinaryTree<int>(2,
                              new BinaryTree<int>(4, null, null),
                              new BinaryTree<int>(5, null, null)),
                      new BinaryTree<int>(3, null, null));

            var topViewActual = binaryTree.TopView()
                .OrderBy(n => n)
                .ToArray();

            int[] topViewExpected = { 1, 2, 3, 4 };

            Assert.AreEqual(topViewExpected.Length, topViewActual.Length);
            for (int i = 0; i < topViewExpected.Length; i++)
            {
                Assert.AreEqual(topViewExpected[i], topViewActual[i]);
            }
        }

        [Test]
        public void TestThirdTopView()
        {
            var binaryTree =
              new BinaryTree<int>(1,
                      new BinaryTree<int>(2, null, null),
                      new BinaryTree<int>(3,
                                new BinaryTree<int>(6, null, null),
                                new BinaryTree<int>(7, null, null)));

            var topViewActual = binaryTree.TopView()
                .OrderBy(n => n)
                .ToArray();

            int[] topViewExpected = { 1, 2, 3, 7 };

            Assert.AreEqual(topViewExpected.Length, topViewActual.Length);
            for (int i = 0; i < topViewExpected.Length; i++)
            {
                Assert.AreEqual(topViewExpected[i], topViewActual[i]);
            }
        }
    }
}

