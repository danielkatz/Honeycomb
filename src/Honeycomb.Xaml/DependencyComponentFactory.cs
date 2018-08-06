using System;
using System.Collections.Generic;
using System.Text;
using Honeycomb.Xaml.Abstractions;

namespace Honeycomb.Xaml
{
    public class DependencyComponentFactory : IDependencyComponentFactory
    {
        public IDependencyComponent Create(IDependencyObject obj)
        {
            return new DependencyComponent(obj);
        }
    }
}
