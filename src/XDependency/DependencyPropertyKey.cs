using System;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyPropertyKey : IDependencyPropertyKey
    {
        internal DependencyPropertyKey(DependencyProperty dp)
        {
            dp.SetReadOnlyKey(this);
            DependencyProperty = dp;
        }

        public IDependencyProperty DependencyProperty { get; }
    }
}