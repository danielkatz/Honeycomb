using System;
using System.Collections.Generic;
using System.Text;
using Honeycomb.Xaml.Abstractions;

namespace Honeycomb.Xaml
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static bool IsDependencyObject(this Type type)
        {
            return (!type.IsInterface)
                && (typeof(IDependencyObject).IsAssignableFrom(type));
        }

        public static void EnsureDependencyObject(this Type type)
        {
            if (!type.IsDependencyObject())
                throw new InvalidOperationException($"{type} isn't a dependency object");
        }
    }
}
