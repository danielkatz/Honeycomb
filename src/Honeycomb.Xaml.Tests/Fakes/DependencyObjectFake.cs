using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeycomb.Xaml.Abstractions;

namespace Honeycomb.Xaml.Tests.Fakes
{
    public class DependencyObjectFake : IDependencyObject
    {
        public DependencyObjectFake()
        {
            Component = Dependency.Component.Create(this);
        }

        public IDependencyComponent Component { get; }
    }
}
