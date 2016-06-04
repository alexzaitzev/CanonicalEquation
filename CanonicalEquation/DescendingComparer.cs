using System.Collections.Generic;

namespace CanonicalEquation
{
    internal sealed class DescendingComparer<T> : IComparer<T>
    {
        private readonly IComparer<T> original;

        public DescendingComparer(IComparer<T> original)
        {
            this.original = original;
        }

        public int Compare(T left, T right)
        {
            return original.Compare(right, left);
        }
    }
}
