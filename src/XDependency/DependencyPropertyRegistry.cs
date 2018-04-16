using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyPropertyRegistry : IDependencyPropertyRegistry
    {
        readonly Dictionary<Type, Dictionary<string, IDependencyProperty>> properties = new Dictionary<Type, Dictionary<string, IDependencyProperty>>();

        public IDependencyProperty Register(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            var dp = new MemberDependencyProperty(name, propertyType, ownerType, typeMetadata);
            AddToRegistry(ownerType, dp);
            return dp;
        }

        public IDependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            var dp = new MemberDependencyProperty(name, propertyType, ownerType, typeMetadata);
            AddToRegistry(ownerType, dp);
            var key = dp.MakeReadOnly();
            return key;
        }

        public IDependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            var dp = new AttachedDependencyProperty(name, propertyType, ownerType, defaultMetadata);
            AddToRegistry(ownerType, dp);
            return dp;
        }

        public IDependencyPropertyKey RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            var dp = new AttachedDependencyProperty(name, propertyType, ownerType, defaultMetadata);
            AddToRegistry(ownerType, dp);
            var key = dp.MakeReadOnly();
            return key;
        }

        private void AddToRegistry(Type ownerType, IDependencyProperty dp)
        {
            var existing = FindByName(ownerType, dp.Name);
            if (existing != null)
                throw new InvalidOperationException($"There is already a property named {dp.Name} registered on type {existing.OwnerType}.");

            Dictionary<string, IDependencyProperty> typeRegistry = null;
            if (!properties.TryGetValue(ownerType, out typeRegistry))
            {
                typeRegistry = new Dictionary<string, IDependencyProperty>();
                properties[ownerType] = typeRegistry;
            }

            typeRegistry.Add(dp.Name, dp);
        }

        private IDependencyProperty FindByName(Type forType, string name)
        {
            for (var type = forType; type != null; type = type.BaseType)
            {
                if (properties.TryGetValue(type, out var typeRepository))
                {
                    if (typeRepository.TryGetValue(name, out var dp))
                    {
                        return dp;
                    }
                }
            }
            return null;
        }
    }
}
