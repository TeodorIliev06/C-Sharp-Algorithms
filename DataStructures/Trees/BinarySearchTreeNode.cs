namespace DataStructures.Trees
{
    using System;

    /// <summary>
    /// The binary search tree node.
    /// </summary>
    public class BSTNode<T> : IComparable<BSTNode<T>> where T : IComparable<T>
    {
        private T _value;
        private BSTNode<T> _parent;
        private BSTNode<T> _left;
        private BSTNode<T> _right;

        public BSTNode() : this(default(T), 0, null, null, null) { }
        public BSTNode(T value) : this(value, 0, null, null, null) { }
        public BSTNode(T value, int subTreeSize, BSTNode<T> parent, BSTNode<T> left, BSTNode<T> right)
        {
            Value = value;
            Parent = parent;
            LeftChild = left;
            RightChild = right;
        }

        public virtual T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public virtual BSTNode<T> Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public virtual BSTNode<T> LeftChild
        {
            get { return _left; }
            set { _left = value; }
        }

        public virtual BSTNode<T> RightChild
        {
            get { return _right; }
            set { _right = value; }
        }

        /// <summary>
        /// Checks whether this node has any children.
        /// </summary>
        public virtual bool HasChildren
        {
            get { return (ChildrenCount > 0); }
        }

        /// <summary>
        /// Checks whether this node has left child.
        /// </summary>
        public virtual bool HasLeftChild
        {
            get { return (LeftChild != null); }
        }

        /// <summary>
        ///     Check if this node has only one child and whether it is the right child.
        /// </summary>
        public virtual bool HasOnlyRightChild => !HasLeftChild && HasRightChild;

        /// <summary>
        /// Checks whether this node has right child.
        /// </summary>
        public virtual bool HasRightChild
        {
            get { return (RightChild != null); }
        }

        /// <summary>
        ///     Check if this node has only one child and whether it is the left child.
        /// </summary>
        public virtual bool HasOnlyLeftChild => !HasRightChild && HasLeftChild;

        /// <summary>
        /// Checks whether this node is the left child of it's parent.
        /// </summary>
        public virtual bool IsLeftChild
        {
            get { return (Parent != null && Parent.LeftChild == this); }
        }

        /// <summary>
        /// Checks whether this node is the left child of it's parent.
        /// </summary>
        public virtual bool IsRightChild
        {
            get { return (Parent != null && Parent.RightChild == this); }
        }

        /// <summary>
        /// Checks whether this node is a leaf node.
        /// </summary>
        public virtual bool IsLeafNode
        {
            get { return (ChildrenCount == 0); }
        }

        /// <summary>
        /// Returns number of direct descendents: 0, 1, 2 (none, left or right, or both).
        /// </summary>
        /// <returns>Number (0,1,2)</returns>
        public virtual int ChildrenCount
        {
            get
            {
                int count = 0;

                if (HasLeftChild)
                    count++;
                if (HasRightChild)
                    count++;

                return count;
            }
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        public virtual int CompareTo(BSTNode<T> other)
        {
            if (other == null)
                return -1;

            return Value.CompareTo(other.Value);
        }
    }//end-of-bstnode
}
