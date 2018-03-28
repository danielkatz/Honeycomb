using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyPropertyRegistry : IDependencyPropertyRegistry
    {
        public IDependencyProperty Register(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            throw new NotImplementedException();
        }

        public IDependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            throw new NotImplementedException();
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
