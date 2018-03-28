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

        public IDependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            throw new NotImplementedException();
        }
    }
}
