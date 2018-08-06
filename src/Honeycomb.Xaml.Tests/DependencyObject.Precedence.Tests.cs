using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeycomb.Xaml.Abstractions;
using Honeycomb.Xaml.Abstractions.Extensions;
using Honeycomb.Xaml.Tests.Fakes;
using Honeycomb.Xaml.Tests.Fixtures;
using Xunit;

namespace Honeycomb.Xaml.Tests
{
    public partial class DependencyObjectTests
    {
        [Fact]
        public void SetPropertyValue_SetTopFirst()
        {
            using (new DefaultImplementationFixture())
            {
                var member = Dependency.Property.Register("Value", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var attached = Dependency.Property.RegisterAttached("AttachedValue", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var owner = new DependencyObjectFake();
                var top = owner.Component.GetValueSource<ValueStoreFake>();

                top.SetValue(member, "top");
                owner.SetValue(member, "local");

                top.SetValue(attached, "top");
                owner.SetValue(attached, "local");

                Assert.Equal("top", owner.GetValue(member));
                Assert.Equal("top", owner.GetValue(attached));
            }
        }

        [Fact]
        public void SetPropertyValue_SetTopSecond()
        {
            using (new DefaultImplementationFixture())
            {
                var member = Dependency.Property.Register("Value", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var attached = Dependency.Property.RegisterAttached("AttachedValue", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var owner = new DependencyObjectFake();
                var top = owner.Component.GetValueSource<ValueStoreFake>();

                owner.SetValue(member, "local");
                top.SetValue(member, "top");

                owner.SetValue(attached, "local");
                top.SetValue(attached, "top");

                Assert.Equal("top", owner.GetValue(member));
                Assert.Equal("top", owner.GetValue(attached));
            }
        }

        [Fact]
        public void SetPropertyValue_ChangeTop()
        {
            using (new DefaultImplementationFixture())
            {
                var member = Dependency.Property.Register("Value", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var attached = Dependency.Property.RegisterAttached("AttachedValue", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var owner = new DependencyObjectFake();
                var top = owner.Component.GetValueSource<ValueStoreFake>();

                top.SetValue(member, "top");
                owner.SetValue(member, "local");
                top.SetValue(member, "new top");

                top.SetValue(attached, "top");
                owner.SetValue(attached, "local");
                top.SetValue(attached, "new top");

                Assert.Equal("new top", owner.GetValue(member));
                Assert.Equal("new top", owner.GetValue(attached));
            }
        }

        [Fact]
        public void SetPropertyValue_ClearTop()
        {
            using (new DefaultImplementationFixture())
            {
                var member = Dependency.Property.Register("Value", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var attached = Dependency.Property.RegisterAttached("AttachedValue", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var owner = new DependencyObjectFake();
                var top = owner.Component.GetValueSource<ValueStoreFake>();

                top.SetValue(member, "top");
                owner.SetValue(member, "local");
                top.ClearValue(member);

                top.SetValue(attached, "top");
                owner.SetValue(attached, "local");
                top.ClearValue(attached);

                Assert.Equal("local", owner.GetValue(member));
                Assert.Equal("local", owner.GetValue(attached));
            }
        }

        [Fact]
        public void SetPropertyValue_NullIsAValue()
        {
            using (new DefaultImplementationFixture())
            {
                var member = Dependency.Property.Register("Value", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var attached = Dependency.Property.RegisterAttached("AttachedValue", typeof(string), typeof(DependencyObjectFake), new PropertyMetadata("default"));
                var owner = new DependencyObjectFake();
                var top = owner.Component.GetValueSource<ValueStoreFake>();

                top.SetValue(member, null);
                owner.SetValue(member, "local");

                top.SetValue(attached, null);
                owner.SetValue(attached, "local");

                Assert.Null(owner.GetValue(member));
                Assert.Null(owner.GetValue(attached));
            }
        }
    }
}
