using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public static class Dependency
    {
        static IDependencyComponentFactory componentFactory;
        static IDependencyPropertyRegistry propertyRegistry;
        static IValueSourceRegistry valueSourceFactory;

        public static void Init(
            IDependencyComponentFactory componentFactory,
            IDependencyPropertyRegistry propertyRegistry,
            IValueSourceRegistry valueSourceFactory)
        {
            Dependency.componentFactory = componentFactory;
            Dependency.propertyRegistry = propertyRegistry;
            Dependency.valueSourceFactory = valueSourceFactory;
        }

        public static IDependencyComponentFactory Component => componentFactory;
        public static IDependencyPropertyRegistry Property => propertyRegistry;
        public static IValueSourceRegistry ValueSources => valueSourceFactory;
    }
}