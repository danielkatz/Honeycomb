using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyProperty : IDependencyProperty
    {
        public DependencyProperty(string name, Type propertyType, Type ownerType, bool isReadOnly)
        {
            this.Name = name;
            this.IsReadOnly = isReadOnly;
            this.PropertyType = propertyType;
            this.OwnerType = ownerType;
        }

        public IPropertyMetadata GetMetadata(Type forType)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }
        public Type PropertyType { get; }
        public Type OwnerType { get; }
        public bool IsReadOnly { get; }
    }
}
