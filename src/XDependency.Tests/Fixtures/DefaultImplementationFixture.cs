using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;

namespace XDependency.Tests.Fixtures
{
    class DefaultImplementationFixture : IDisposable
    {
        public DefaultImplementationFixture()
        {
            Dependency.Init(new DependencyComponentFactory(), new DependencyPropertyRegistryFactory());
        }

        public void Dispose()
        {
            Dependency.Init(null, null);
        }
    }
}
