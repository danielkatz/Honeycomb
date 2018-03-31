using XDependency.Abstractions;

namespace XDependency
{
    internal class DependencyPropertyKey : IDependencyPropertyKey
    {
        public DependencyPropertyKey(IDependencyProperty dp)
        {
            this.DependencyProperty = dp;
        }

        public IDependencyProperty DependencyProperty { get; }
    }
}