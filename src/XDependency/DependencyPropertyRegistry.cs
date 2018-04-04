using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyPropertyRegistry : IDependencyPropertyRegistry
    {
        //readonly Dictionary<Type, List<IDependencyProperty>> properties = new Dictionary<Type, List<IDependencyProperty>>();
        readonly Dictionary<IDependencyProperty, Dictionary<Type, IPropertyMetadata>> metadataStore = new Dictionary<IDependencyProperty, Dictionary<Type, IPropertyMetadata>>();

        public IDependencyProperty Register(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            var dp = new DependencyProperty(name, propertyType, ownerType, false);
            SetPropertyMetadata(dp, ownerType, typeMetadata);
            return dp;
        }

        public IDependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            var dp = new DependencyProperty(name, propertyType, ownerType, true);
            var dpk = new DependencyPropertyKey(dp);
            SetPropertyMetadata(dp, ownerType, typeMetadata);
            return dpk;
        }

        public IDependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            var dp = new DependencyProperty(name, propertyType, ownerType, false);
            SetPropertyMetadata(dp, ownerType, defaultMetadata);
            return dp;
        }

        public IDependencyPropertyKey RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            var dp = new DependencyProperty(name, propertyType, ownerType, true);
            var dpk = new DependencyPropertyKey(dp);
            SetPropertyMetadata(dp, ownerType, defaultMetadata);
            return dpk;
        }

        IPropertyMetadata SetPropertyMetadata(IDependencyProperty dp, Type ownerType, IPropertyMetadata typeMetadata)
        {
            if (!metadataStore.TryGetValue(dp, out var propertyMetadata))
            {
                propertyMetadata = new Dictionary<Type, IPropertyMetadata>();
                metadataStore[dp] = propertyMetadata;
            }
            propertyMetadata[ownerType] = typeMetadata;
            return typeMetadata;
        }

        public IPropertyMetadata GetPropertyMetadata(IDependencyProperty dp, Type forType)
        {
            return this.metadataStore[dp][forType];
        }
    }
}
