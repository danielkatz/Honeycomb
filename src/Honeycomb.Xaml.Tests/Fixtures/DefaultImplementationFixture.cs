using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeycomb.Xaml.Abstractions;
using Honeycomb.Xaml.Tests.Fakes;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace Honeycomb.Xaml.Tests.Fixtures
{
    class DefaultImplementationFixture : IDisposable
    {
        public DefaultImplementationFixture()
        {
            Dependency.Init(new DependencyComponentFactory(), new DependencyPropertyRegistry(), new ValueSourceRegistry());
            Dependency.ValueSources.Add((c, i) => new ValueStoreFake(c, i));
            Dependency.ValueSources.Add((c, i) => new LocalValueStore(c, i));
            Dependency.ValueSources.Add((c, i) => new InheritanceValueSource(c, i));
        }

        public void Dispose()
        {
            Dependency.Init(null, null, null);
        }
    }
}
