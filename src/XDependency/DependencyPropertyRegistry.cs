using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyPropertyRegistry : IDependencyPropertyRegistry
    {
        //readonly Dictionary<Type, List<IDependencyProperty>> properties = new Dictionary<Type, List<IDependencyProperty>>();

        public IDependencyProperty Register(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            var dp = new DependencyProperty(name, propertyType, ownerType, typeMetadata);
            return dp;
        }

        public IDependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            var dp = new DependencyProperty(name, propertyType, ownerType, typeMetadata);
            var key = new DependencyPropertyKey(dp);
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
    }
}
