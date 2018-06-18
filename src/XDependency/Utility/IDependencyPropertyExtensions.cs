using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency.Utility
{
    public static class IDependencyPropertyExtensions
    {
        public static void EnsureNotReadOnly(this IDependencyProperty dp)
        {
            if (dp.IsReadOnly)
                throw new InvalidOperationException($"{dp.Name} is a read only property and should be changed using a key");
        }

        public static void VerifyReadOnlyKey(this IDependencyProperty dp, IDependencyPropertyKey key)
        {
            if (ReferenceEquals(dp, key.DependencyProperty))
                throw new InvalidOperationException($"The provided key doesn't match property {dp.Name}");
        }
    }
}
