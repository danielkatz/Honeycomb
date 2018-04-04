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
            var dp = RegisterCommon(name, propertyType, ownerType, null);
            OverrideMetadata(dp, ownerType, typeMetadata);
            return dp;
        }

        public IDependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            var dp = RegisterCommon(name, propertyType, ownerType, null);
            var key = new DependencyPropertyKey(dp);
            OverrideMetadata(dp, ownerType, typeMetadata);
            return key;
        }

        public IDependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            throw new NotImplementedException();
        }

        public IDependencyPropertyKey RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            throw new NotImplementedException();
        }

        DependencyProperty RegisterCommon(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            EnsureDependencyObject(ownerType);

            if (defaultMetadata == null)
            {
                defaultMetadata = CreateDefaultMetadata(propertyType);
            }

            var dp = new DependencyProperty(name, propertyType, ownerType, defaultMetadata);
            return dp;
        }

        IPropertyMetadata CreateDefaultMetadata(Type propertyType)
        {
            var defaultValue = propertyType.GetDefaultValue();
            return new PropertyMetadata(defaultValue);
        }

        public IPropertyMetadata GetPropertyMetadata(IDependencyProperty dp, Type forType)
        {
            EnsureDependencyObject(forType);

            return this.metadataStore[dp][forType];
        }

        void OverrideMetadata(IDependencyProperty dp, Type forType, IPropertyMetadata typeMetadata)
        {
            if (!metadataStore.TryGetValue(dp, out var propertyMetadata))
            {
                propertyMetadata = new Dictionary<Type, IPropertyMetadata>();
                metadataStore[dp] = propertyMetadata;
            }
            propertyMetadata[forType] = typeMetadata;
        }

        static void EnsureDependencyObject(Type type)
        {
            if (!typeof(IDependencyObject).IsAssignableFrom(type))
                throw new InvalidOperationException($"{type} doesn't implement {nameof(IDependencyObject)}");
        }
    }
}
