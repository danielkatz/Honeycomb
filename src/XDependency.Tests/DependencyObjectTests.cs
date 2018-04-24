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
        public void InstansiateEmptyObject()
        {
            using (new DefaultImplementationFixture())
            {
                var obj = new DependencyObjectFake();
            }
        }

        [Fact]
        public void GetMemberPropertyDefaultValue()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var owner = new DependencyObjectFake();

                Assert.True((bool)owner.GetValue(prop));
            }
        }

        [Fact]
        public void GetAttachedPropertyDefaultValue()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var fake = new SecondDependencyObjectFake();

                Assert.True((bool)fake.GetValue(prop));
            }
        }

        [Fact]
        public void SetMemberPropertyValue()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var owner = new DependencyObjectFake();

                owner.SetValue(prop, false);

                Assert.False((bool)owner.GetValue(prop));
            }
        }

        [Fact]
        public void SetAttachedPropertyValue()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var fake = new SecondDependencyObjectFake();

                fake.SetValue(prop, false);

                Assert.False((bool)fake.GetValue(prop));
            }
        }

        [Fact]
        public void SetMemberReadOnlyPropertyValueViaDP()
        {
            using (new DefaultImplementationFixture())
            {
                var propKey = Dependency.Property.RegisterReadOnly("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var prop = propKey.DependencyProperty;
                var owner = new DependencyObjectFake();

                Assert.Throws<InvalidOperationException>(() => owner.SetValue(prop, false));
            }
        }

        [Fact]
        public void SetAttachedReadOnlyPropertyValueViaDP()
        {
            using (new DefaultImplementationFixture())
            {
                var propKey = Dependency.Property.RegisterAttachedReadOnly("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var prop = propKey.DependencyProperty;
                var fake = new SecondDependencyObjectFake();

                Assert.Throws<InvalidOperationException>(() => fake.SetValue(prop, false));
            }
        }

        [Fact]
        public void SetMemberReadOnlyPropertyValueViaDPKey()
        {
            using (new DefaultImplementationFixture())
            {
                var propKey = Dependency.Property.RegisterReadOnly("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var owner = new DependencyObjectFake();

                owner.SetValue(propKey, false);

                Assert.False((bool)owner.GetValue(propKey.DependencyProperty));
            }
        }

        [Fact]
        public void SetAttachedReadOnlyPropertyValueViaDPKey()
        {
            using (new DefaultImplementationFixture())
            {
                var propKey = Dependency.Property.RegisterAttachedReadOnly("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var fake = new SecondDependencyObjectFake();

                fake.SetValue(propKey, false);

                Assert.False((bool)fake.GetValue(propKey.DependencyProperty));
            }
        }

        [Fact]
        public void ClearMemberPropertyValue()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var owner = new DependencyObjectFake();

                owner.SetValue(prop, false);
                owner.ClearValue(prop);

                Assert.True((bool)owner.GetValue(prop));
            }
        }

        [Fact]
        public void ClearAttachedPropertyValue()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var fake = new SecondDependencyObjectFake();

                fake.SetValue(prop, false);
                fake.ClearValue(prop);

                Assert.True((bool)fake.GetValue(prop));
            }
        }
    }
}
