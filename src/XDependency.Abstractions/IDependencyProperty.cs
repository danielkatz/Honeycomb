using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public interface IDependencyProperty
    {
        IPropertyMetadata GetMetadata(Type forType);

        string Name { get; }
        bool IsReadOnly { get; }
        Type PropertyType { get; }
        Type OwnerType { get; }
    }
}
