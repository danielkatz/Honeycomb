using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class LocalValueStore : ValueStoreBase
    {
        public LocalValueStore(IDependencyComponent component, int order) : base(component, order)
        {
        }
    }
}
