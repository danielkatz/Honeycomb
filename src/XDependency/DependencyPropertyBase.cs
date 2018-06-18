using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;
using XDependency.Utility;

namespace XDependency
{
    public abstract class DependencyPropertyBase : IDependencyProperty
    {
        protected readonly Dictionary<Type, IPropertyMetadata> metadataMap = new Dictionary<Type, IPropertyMetadata>();
        IDependencyPropertyKey readOnlyKey = null;

        public DependencyPropertyBase(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            this.Name = name;
            this.PropertyType = propertyType;
            this.OwnerType = ownerType;
            this.DefaultMetadata = defaultMetadata;
        }

        public virtual IPropertyMetadata GetMetadata(Type forType)
        {
            for (var type = forType; type.IsDependencyObject(); type = type.BaseType)
            {
                if (metadataMap.TryGetValue(type, out var metadata))
                {
                    return metadata;
                }
            }

            return DefaultMetadata;
        }

        public void OverrideMetadata(Type forType, IPropertyMetadata typeMetadata)
        {
            this.EnsureNotReadOnly();

            OverrideMetadataCommon(forType, typeMetadata);
        }

        public void OverrideMetadata(Type forType, IPropertyMetadata typeMetadata, IDependencyPropertyKey key)
        {
            this.VerifyReadOnlyKey(key);

            OverrideMetadataCommon(forType, typeMetadata);
        }

        protected virtual void OverrideMetadataCommon(Type forType, IPropertyMetadata typeMetadata)
        {
            var baseMetadata = GetMetadata(forType);
            typeMetadata.Merge(baseMetadata);

            metadataMap[forType] = typeMetadata;
        }

        public IDependencyPropertyKey MakeReadOnly()
        {
            if (readOnlyKey != null)
                throw new InvalidOperationException("The property is already read-only.");

            readOnlyKey = new DependencyPropertyKey(this);
            return readOnlyKey;
        }

        public string Name { get; }
        public Type PropertyType { get; }
        public Type OwnerType { get; }
        public bool IsReadOnly => readOnlyKey != null;
        public IPropertyMetadata DefaultMetadata { get; }
    }
}