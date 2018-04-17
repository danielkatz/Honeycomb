using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public static class Dependency
    {
        static IDependencyComponentFactory componentFactory;
        static IDependencyPropertyRegistry propertyRegistry;
        static IValueSourceRegistry valueSourceRegistry;

        public static void Init(
            IDependencyComponentFactory componentFactory,
            IDependencyPropertyRegistry propertyRegistry,
            IValueSourceRegistry valueSourceRegistry)
        {
            Dependency.componentFactory = componentFactory;
            Dependency.propertyRegistry = propertyRegistry;
            Dependency.valueSourceRegistry = valueSourceRegistry;
        }

        public static IDependencyComponentFactory Component => componentFactory;
        public static IDependencyPropertyRegistry Property => propertyRegistry;
        public static IValueSourceRegistry ValueSources => valueSourceRegistry;
    }
}