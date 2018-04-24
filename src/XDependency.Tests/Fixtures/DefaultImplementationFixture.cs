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
            Dependency.Init(new DependencyComponentFactory(), new DependencyPropertyRegistry(), new ValueSourceRegistry());
            Dependency.ValueSources.Add((c, i) => new LocalValueStore());
        }

        public void Dispose()
        {
            Dependency.Init(null, null, null);
        }
    }
}
