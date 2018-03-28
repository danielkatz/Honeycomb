using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyPropertyRegistryFactory : IDependencyPropertyRegistryFactory
    {
        public IDependencyPropertyRegistry Create()
        {
            return new DependencyPropertyRegistry();
        }
    }
}
