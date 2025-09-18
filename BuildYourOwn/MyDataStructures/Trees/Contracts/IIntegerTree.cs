namespace MyDataStructures.Trees.Contracts
{
    using System.Collections.Generic;

    using MyDataStructures.Trees.Models;

    public interface IIntegerTree: IAbstractTree<int>
    {
        IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum);

        IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum);
    }
}
