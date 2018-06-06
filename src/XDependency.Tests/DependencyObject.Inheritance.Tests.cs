using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;
using XDependency.Abstractions.Extensions;
using XDependency.Tests.Fakes;
using XDependency.Tests.Fixtures;
using Xunit;

namespace XDependency.Tests
{
    public partial class DependencyObjectTests
    {
        [Fact]
        public void CanInheritValueFromParent()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.RegisterAttached("AttachedValue", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default", inherits: true));
                var parent = new DependencyObjectFake();
                var child = new DependencyObjectFake();
                var childInheritanceSource = child.Component.GetValueSource<InheritanceValueSource>();

                parent.SetValue(prop, "inherited");

                Assert.Equal("default", child.GetValue(prop));

                childInheritanceSource.ParentComponent = parent.Component;

                Assert.Equal("inherited", child.GetValue(prop));
            }
        }
    }
}
