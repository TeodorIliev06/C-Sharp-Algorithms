namespace UnitTest.DataStructuresTests
{
    using System;

    using Xunit;

    using DataStructures.Common;

    public class PrimeListTest
    {
        [Fact]
        public void DoTest()
        {
            var instance = PrimesList.Instance;
            Assert.Equal(10000, instance.Count);
        }

        [Fact]
        public void PrimesListIsReadOnly()
        {
            var instance = PrimesList.Instance;
            NotSupportedException ex = Assert.Throws<NotSupportedException>(() => instance.GetAll[0] = -1);
            Assert.Equal("Collection is read-only.", ex.Message);
        }
    }
}
