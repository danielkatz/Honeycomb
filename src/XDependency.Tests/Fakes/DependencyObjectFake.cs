using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;

namespace XDependency.Tests.Fakes
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
