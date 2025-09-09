namespace MyUnitTests.Lists
{
    using MyDataStructures.Lists.Models;
    using MyDataStructures.Lists.Contracts;

    [TestFixture]
    public class ListTests
    {
        private IAbstractList<int> list;

        [SetUp]
        public void InitializeList()
        {
            list = new List<int>();
        }

        private static IEnumerable<int[]> RandomCollections()
        {
            var collections = new System.Collections.Generic.List<int[]>
            {
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 },
                new[] { 3, 8, 1, 6, 5, 7, 2, 9, 4 },
                new[] { 3, 8, 1 },
                new[] { 3 },
                new[] { 3, 8, 1, 3, 8, 1, 6, 5, 7, 2, 9, 4, 3, 8, 1, 6, 5, 7, 2, 9, 4 }
            };

            return collections;
        }

        [Test]
        public void Add_SingleNumber_ShouldAddCorrectElement()
        {
            var expected = new[] { 1 };

            foreach (var num in expected)
            {
                list.Add(num);
            }

            CollectionAssert.AreEqual(expected, list);
        }

        [Test]
        public void Add_SingleNumber_ShouldIncrementCount()
        {
            list.Add(5);

            Assert.AreEqual(1, list.Count);
        }

        [Test]
        [TestCaseSource(nameof(RandomCollections))]
        public void Add_MultipleNumbers_ShouldAddCorrectElements(int[] expected)
        {
            foreach (var num in expected)
            {
                list.Add(num);
            }

            CollectionAssert.AreEqual(expected, list);
        }

        [Test]
        [TestCaseSource(nameof(RandomCollections))]
        public void Add_MultipleNumbers_ShouldIncreaseCount(int[] expected)
        {
            Assert.AreEqual(0, list.Count);

            foreach (var num in expected)
            {
                list.Add(num);
            }

            Assert.AreEqual(expected.Length, list.Count);
        }

        [Test]
        [TestCase(5, ExpectedResult = 5)]
        public int Indexer_GetOnlyElement_ShouldReturnCorrectElement(int element)
        {
            list.Add(element);

            return list[0];
        }

        [Test]
        [TestCaseSource(nameof(RandomCollections))]
        public void Indexer_GetFirstElement_ShouldReturnCorrectElement(int[] expected)
        {
            foreach (var num in expected)
            {
                list.Add(num);
            }

            Assert.AreEqual(expected[0], list[0]);
        }

        [Test]
        [TestCaseSource(nameof(RandomCollections))]
        public void Indexer_GetLastElement_ShouldReturnCorrectElement(int[] expected)
        {
            foreach (var num in expected)
            {
                list.Add(num);
            }

            Assert.AreEqual(expected[expected.Length - 1], list[list.Count - 1]);
        }

        [Test]
        public void Indexer_GetInvalidElement_ShouldThrowException([Values(-1, 1, 8, -5, int.MinValue, int.MaxValue)] int index)
        {
            list.Add(10);

            Assert.Throws<IndexOutOfRangeException>(() => { var test = list[index]; });
        }

        [Test]
        public void Indexer_SetSingleElement_ShouldWorkCorrectly()
        {
            list.Add(1);
            list[0] = 5;

            CollectionAssert.AreEqual(new[] { 5 }, list);
        }

        [Test]
        public void Indexer_SetInvalidElement_ShouldThrowException([Values(-1, 1, 8, -5, int.MinValue, int.MaxValue)] int index)
        {
            list.Add(10);

            Assert.Throws<IndexOutOfRangeException>(() => { list[index] = 5; ; });
        }


        [Test]
        public void IndexOf_OnCollectionWithSingleElement_ShouldReturnCorrectIndex()
        {
            var value = 10;
            list.Add(value);

            Assert.AreEqual(0, list.IndexOf(value));
        }

        [Test]
        public void IndexOf_WithExistingElement_ShouldReturnCorrectIndex()
        {
            var numbers = new[] { 3, 5, 7, 1, -5, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            Assert.AreEqual(1, list.IndexOf(5));
        }

        [Test]
        public void IndexOf_WithNonExistingElement_ShouldReturnMinusOne()
        {
            var numbers = new[] { 3, 5, 7, 1, -5, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            Assert.AreEqual(-1, list.IndexOf(56));
        }

        [Test]
        public void IndexOf_OnEmptyCollection_ShouldReturnMinusOne()
        {
            Assert.AreEqual(-1, list.IndexOf(56));
        }

        [Test]
        public void IndexOf_WithMultipleCopiesOfElement_ShouldReturnLeftmostIndex()
        {
            var numbers = new[] { 3, 5, 7, 1, 7, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            Assert.AreEqual(2, list.IndexOf(7));
        }

        [Test]
        public void IndexOf_FirstElement_ShouldReturnIndexZero()
        {
            var numbers = new[] { 3, 5, 7, 1, 7, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            Assert.AreEqual(0, list.IndexOf(3));
        }

        [Test]
        public void IndexOf_LastElement_ShouldReturnLastIndex()
        {
            var numbers = new[] { 3, 5, 7, 1, 7, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            Assert.AreEqual(numbers.Length - 1, list.IndexOf(-100));
        }

        [Test]
        public void Contains_OnCollectionWithSingleElement_ShouldReturnTrue()
        {
            var value = 10;
            list.Add(value);

            Assert.AreEqual(true, list.Contains(value));
        }

        [Test]
        public void Contains_WithExistingElement_ShouldReturnTrue()
        {
            var numbers = new[] { 3, 5, 7, 1, -5, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            Assert.AreEqual(true, list.Contains(5));
        }

        [Test]
        public void Contains_OnEmptyCollection_ShouldReturnFalse()
        {
            Assert.AreEqual(false, list.Contains(56));
        }

        [Test]
        public void Contains_OnEmptyCollectionSearchingForZero_ShouldReturnFalse()
        {
            Assert.AreEqual(false, list.Contains(0));
        }

        [Test]
        public void Contains_WithNonExistingElement_ShouldReturnFalse()
        {
            var numbers = new[] { 3, 5, 7, 1, -5, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            Assert.AreEqual(false, list.Contains(536));
        }

        [Test]
        public void RemoveAt_WithSingleElement_ShouldDecreaseCount()
        {
            list.Add(15);
            list.RemoveAt(0);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        [TestCaseSource(nameof(RandomCollections))]
        public void RemoveAt_WithMultipleElements_ShouldDecreaseCount(int[] expected)
        {
            foreach (var num in expected)
            {
                list.Add(num);
            }
            list.RemoveAt(0);

            Assert.AreEqual(expected.Length - 1, list.Count);
        }

        [Test]
        [TestCaseSource(nameof(RandomCollections))]
        public void RemoveAt_WithMultipleElements_ShouldRemoveCorrectElement(int[] expected)
        {
            int indexToRemove = expected.Length / 2;
            foreach (var num in expected)
            {
                list.Add(num);
            }
            list.RemoveAt(indexToRemove);

            var expectedAsList = expected.ToList();
            expectedAsList.RemoveAt(indexToRemove);

            CollectionAssert.AreEqual(expectedAsList, list);
        }

        [Test]
        public void RemoveAt_InvalidIndex_ShouldThrowException([Values(-1, 1, 8, -5, int.MinValue, int.MaxValue)] int index)
        {
            list.Add(10);

            Assert.Throws<IndexOutOfRangeException>(() => { list.RemoveAt(index); });
        }

        [Test]
        public void Remove_WithSingleElement_ShouldDecreaseCount()
        {
            list.Add(15);
            list.Remove(15);

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Remove_WithSingleElement_ShouldReturnTrue()
        {
            list.Add(15);

            Assert.AreEqual(true, list.Remove(15));
        }

        [Test]
        [TestCaseSource(nameof(RandomCollections))]
        public void Remove_WithMultipleElements_ShouldRemoveCorrectElement(int[] expected)
        {
            int itemToRemove = expected[expected.Length / 2];
            foreach (var num in expected)
            {
                list.Add(num);
            }
            list.Remove(itemToRemove);

            var expectedAsList = expected.ToList();
            expectedAsList.Remove(itemToRemove);

            CollectionAssert.AreEqual(expectedAsList, list);
        }

        [Test]
        public void Remove_InvalidElement_ShouldReturnFalse([Values(-1, 1)] int element)
        {
            list.Add(10);

            Assert.AreEqual(false, list.Remove(element));
        }


        [Test]
        public void Insert_SingleElement_ShouldIncreaseCount()
        {
            var numbers = new[] { 3, 5, 7, 1, -5, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            list.Insert(2, 1001);

            Assert.AreEqual(numbers.Length + 1, list.Count);
        }

        [Test]
        public void Insert_SingleElement_ShouldPutElementInCorrectPosition()
        {
            var numbers = new[] { 3, 5, 7, 1, -5, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            list.Insert(2, 1001);

            Assert.AreEqual(2, list.IndexOf(1001));
        }

        [Test]
        public void Insert_TwoElements_ShouldPutThemInCorrectPositions()
        {
            var numbers = new[] { 3, 5, 7, 1, -5, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            list.Insert(2, 1001);

            Assert.AreEqual(2, list.IndexOf(1001));

            list.Insert(4, 1002);
            Assert.AreEqual(4, list.IndexOf(1002));
            Assert.AreEqual(1, list[5]);
        }

        [Test]
        public void Insert_LastIndex_ShouldWorkCorrectly()
        {
            var numbers = new[] { 3, 5, 7, 1, -5, -100 };
            foreach (var num in numbers)
            {
                list.Add(num);
            }

            list.Insert(list.Count - 1, 100);

            Assert.AreEqual(-100, list[list.Count - 1]);
            Assert.AreEqual(100, list[5]);
        }

        [Test]
        public void Insert_InvalidIndex_ShouldThrowException([Values(-1, 1, 3, -5, int.MinValue, int.MaxValue)] int index)
        {
            list.Add(10);

            Assert.Throws<IndexOutOfRangeException>(() => { list.Insert(index, 15); });
        }
    }
}