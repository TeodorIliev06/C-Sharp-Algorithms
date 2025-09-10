namespace MyDataStructures.Stacks
{
    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            // Check if odd number of parentheses
            if (String.IsNullOrEmpty(parentheses) ||
                parentheses.Length % 2 == 1)
            {
                return false;
            }

            var openingBrackets = new System.Collections.Generic.Stack<char>(parentheses.Length / 2);
            foreach (var bracket in parentheses)
            {
                char expectedBracket = default;
                switch (bracket)
                {
                    case ')':
                        expectedBracket = '(';
                        break;
                    case ']':
                        expectedBracket = '[';
                        break;
                    case '}':
                        expectedBracket = '{';
                        break;
                    default:
                        openingBrackets.Push(bracket);
                        break;
                }

                if (expectedBracket == default)
                {
                    continue;
                }

                if (openingBrackets.Pop() != expectedBracket)
                {
                    return false;
                }
            }

            return openingBrackets.Count == 0;
        }
    }
}
