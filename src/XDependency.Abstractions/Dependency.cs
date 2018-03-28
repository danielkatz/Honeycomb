using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public static class Dependency
    {
        static IDependencyComponentFactory componentFactory;
        static IDependencyPropertyRegistry registry;

        public static void Init(IDependencyComponentFactory componentFactory, IDependencyPropertyRegistryFactory registryFactory)
        {
            Dependency.componentFactory = componentFactory;
            Dependency.registry = registryFactory?.Create();
        }

        public static IDependencyComponentFactory Component => componentFactory;
        public static IDependencyPropertyRegistry Property => registry;
    }
}