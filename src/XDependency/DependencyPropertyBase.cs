using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public abstract class DependencyPropertyBase : IDependencyProperty
    {
        protected readonly Dictionary<Type, IPropertyMetadata> metadataMap = new Dictionary<Type, IPropertyMetadata>();
        IDependencyPropertyKey readOnlyKey = null;

        public DependencyPropertyBase(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            ownerType.EnsureDependencyObject();

            this.Name = name;
            this.PropertyType = propertyType;
            this.OwnerType = ownerType;
            this.DefaultMetadata = defaultMetadata;
        }

        public IPropertyMetadata GetMetadata(Type forType)
        {
            forType.EnsureDependencyObject();

            for (var type = forType; type.IsDependencyObject(); type = type.BaseType)
            {
                if (metadataMap.TryGetValue(type, out var metadata))
                {
                    return metadata;
                }
            }

            return DefaultMetadata;
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
        public bool IsReadOnly => readOnlyKey != null;
        public IPropertyMetadata DefaultMetadata { get; }
    }
}