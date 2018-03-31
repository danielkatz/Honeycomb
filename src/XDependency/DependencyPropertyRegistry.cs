using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyPropertyRegistry : IDependencyPropertyRegistry
    {
        //readonly Dictionary<Type, List<IDependencyProperty>> properties = new Dictionary<Type, List<IDependencyProperty>>();
        //readonly Dictionary<Type, List<IPropertyMetadata>> metadata = new Dictionary<Type, List<IPropertyMetadata>>();

        public IDependencyProperty Register(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            var dp = new DependencyProperty(name, false);
            return dp;
        }

        public IDependencyPropertyKey RegisterReadOnly(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
        {
            var dp = new DependencyProperty(name, true);
            var dpk = new DependencyPropertyKey(dp);
            return dpk;
        }

        public IDependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            var dp = new DependencyProperty(name, false);
            return dp;
        }

        public IDependencyPropertyKey RegisterAttachedReadOnly(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
        {
            var dp = new DependencyProperty(name, true);
            var dpk = new DependencyPropertyKey(dp);
            return dpk;
        }
    }
}
