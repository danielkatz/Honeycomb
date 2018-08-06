using System;
using Honeycomb.Xaml.Abstractions;

namespace Honeycomb.Xaml
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