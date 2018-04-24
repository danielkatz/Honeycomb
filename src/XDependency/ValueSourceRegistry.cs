using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class ValueSourceRegistry : IValueSourceRegistry
    {
        OrderedDictionary<Type, Func<IDependencyComponent, int, IValueSource>> storeFactories
            = new OrderedDictionary<Type, Func<IDependencyComponent, int, IValueSource>>();

        public void Add<TSource>(Func<IDependencyComponent, int, TSource> factory) where TSource : class, IValueSource
        {
            storeFactories.Add(typeof(TSource), factory);
        }

        public IReadOnlyList<IValueSource> GetValueSources(IDependencyComponent component)
        {
            EnsureReadOnly();

            return storeFactories.Select((p, i) => p.Value(component, i)).ToImmutableArray();
        }

        private void EnsureReadOnly()
        {
            if (!storeFactories.IsReadOnly)
            {
                storeFactories = storeFactories.AsReadOnly();
            }
        }
    }
}
