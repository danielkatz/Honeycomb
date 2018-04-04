using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyProperty : IDependencyProperty
    {
        IDependencyPropertyKey readOnlyKey = null;

        public DependencyProperty(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            this.Name = name;
            this.PropertyType = propertyType;
            this.OwnerType = ownerType;
            this.DefaultMetadata = defaultMetadata;
        }

        internal void SetReadOnlyKey(IDependencyPropertyKey readOnlyKey)
        {
            if (this.readOnlyKey != null)
                throw new InvalidOperationException("The key is already set.");

            this.readOnlyKey = readOnlyKey;
        }

        public string Name { get; }
        public Type PropertyType { get; }
        public Type OwnerType { get; }
        public IPropertyMetadata DefaultMetadata { get; }
        public bool IsReadOnly => readOnlyKey != null;
    }
}