using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Abstractions
{
    public interface IDependencyProperty
    {
        IPropertyMetadata GetMetadata(Type forType);

        string Name { get; }
        bool IsReadOnly { get; }
        Type PropertyType { get; }
        Type OwnerType { get; }

        void OverrideMetadata(Type forType, IPropertyMetadata typeMetadata);
        void OverrideMetadata(Type forType, IPropertyMetadata typeMetadata, IDependencyPropertyKey key);
    }
}
