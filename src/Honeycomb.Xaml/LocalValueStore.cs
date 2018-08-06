using System;
using System.Collections.Generic;
using System.Text;
using Honeycomb.Xaml.Abstractions;

namespace Honeycomb.Xaml
{
    public class LocalValueStore : ValueStoreBase
    {
        public LocalValueStore(IDependencyComponent component, int order) : base(component, order)
        {
        }
    }
}
