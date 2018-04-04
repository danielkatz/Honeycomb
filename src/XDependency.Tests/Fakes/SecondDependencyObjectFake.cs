using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;

namespace XDependency.Tests.Fakes
{
    public class SecondDependencyObjectFake : IDependencyObject
    {
        public SecondDependencyObjectFake()
        {
            Component = Dependency.Component.Create(this);
        }

        public IDependencyComponent Component { get; }
    }
}
