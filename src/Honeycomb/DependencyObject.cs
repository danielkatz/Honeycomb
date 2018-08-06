using Honeycomb.Xaml;
using Honeycomb.Xaml.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb
{
    public abstract class DependencyObject : IDependencyObject
    {
        public DependencyObject()
        {
            Component = Dependency.Component.Create(this);
        }

        public IDependencyComponent Component { get; }
    }
}
