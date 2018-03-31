using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions.Extensions
{
    public static class DependencyPropertyExtensions
    {
        public static IPropertyMetadata GetMetadata(this IDependencyProperty dp, Type forType)
        {
            return Dependency.Property.GetPropertyMetadata(dp, forType);
        }
    }
}
