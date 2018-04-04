using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;
using XDependency.Tests.Fakes;
using XDependency.Tests.Fixtures;
using Xunit;

namespace XDependency.Tests
{
    public class DependencyPropertyRegistryTests
    {
        [Fact]
        public void RegisterReadWriteProperty()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(false));

                Assert.Equal("IsEnabled", prop.Name);
                Assert.Equal(typeof(bool), prop.PropertyType);
                Assert.Equal(typeof(DependencyObjectFake), prop.OwnerType);
                Assert.False(prop.IsReadOnly);
            }
        }

        [Fact]
        public void RegisterReadOnlyProperty()
        {
            using (new DefaultImplementationFixture())
            {
                var propKey = Dependency.Property.RegisterReadOnly("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(false));

                Assert.NotNull(propKey.DependencyProperty);

                var prop = propKey.DependencyProperty;
                Assert.Equal("IsEnabled", prop.Name);
                Assert.Equal(typeof(bool), prop.PropertyType);
                Assert.Equal(typeof(DependencyObjectFake), prop.OwnerType);
                Assert.True(prop.IsReadOnly);
            }
        }
    }
}
