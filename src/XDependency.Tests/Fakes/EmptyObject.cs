using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;

namespace XDependency.Tests.Fakes
{
    public class EmptyObject : IDependencyObject
    {
        readonly IDependencyComponent component;

        public EmptyObject()
        {
            component = Dependency.Component.Create(this);
        }

        public IDependencyComponent Component => component;
    }
}
