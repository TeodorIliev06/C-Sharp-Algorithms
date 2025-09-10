namespace MyUnitTests.Stacks
{
    using NUnit.Framework;

    using MyDataStructures.Stacks;

    [TestFixture]
    public class BalancedParenthesesTests
    {
        private ISolvable _instance;

        [SetUp]
        public void SetUp() => this._instance = GetInstance();

        [Theory]
        [TestCase("{[()]}", true)]
        [TestCase("{[(]]}", false)]
        [TestCase("{{{[()]}}}", true)]
        [TestCase("{{[[(())]]}}", true)]
        [TestCase("()()()()()()()()()()", true)]
        [TestCase("()[]{}{}}", false)]
        [TestCase("{()[]{}{}}", true)]
        [TestCase("{(())[]{}{}}", true)]
        [TestCase("{(())[[]]{}{}}", true)]
        [TestCase("{(())[[]]{{}}{{([])}}}", true)]
        [TestCase("()[{[{{[{}{}}{}}{}}{}}{{}())()))()))(]}}]}]", false)]
        [TestCase("((((((())))))))))))))))", false)]
        [TestCase("()(((({{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{{[[[[[[[[[[[[[[[[[[[[[[[[]]]]]]]]]]]]]]]]]]]]]]]]}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}}))))", true)]
        public void ImplementationShouldWorkCorrectly(string input, bool expectedOutcome)
        {
            var solve = this._instance.AreBalanced(input);
            Assert.AreEqual(expectedOutcome, solve);
        }

        private static ISolvable GetInstance() => new BalancedParenthesesSolve();
    }
}