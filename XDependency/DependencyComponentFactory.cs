using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyComponentFactory : IDependencyComponentFactory
    {
        public IDependencyComponent Create(IDependencyObject obj)
        {
            return new DependencyComponent(obj);
        }
    }
}
