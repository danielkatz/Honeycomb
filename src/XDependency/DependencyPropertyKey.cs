using System;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyPropertyKey : IDependencyPropertyKey
    {
        internal DependencyPropertyKey(DependencyPropertyBase dp)
        {
            DependencyProperty = dp;
        }

        public IDependencyProperty DependencyProperty { get; }
    }
}