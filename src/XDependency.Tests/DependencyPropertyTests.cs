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
    public class DependencyPropertyTests
    {
        [Fact]
        public void RegisterMemberReadWriteProperty()
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
        public void RegisterAttachedReadWriteProperty()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(false));

                Assert.Equal("IsEnabled", prop.Name);
                Assert.Equal(typeof(bool), prop.PropertyType);
                Assert.Equal(typeof(DependencyObjectFake), prop.OwnerType);
                Assert.False(prop.IsReadOnly);
            }
        }

        [Fact]
        public void RegisterMemberReadOnlyProperty()
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

        [Fact]
        public void RegisterAttachedReadOnlyProperty()
        {
            using (new DefaultImplementationFixture())
            {
                var propKey = Dependency.Property.RegisterAttachedReadOnly("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(false));

                Assert.NotNull(propKey.DependencyProperty);

                var prop = propKey.DependencyProperty;
                Assert.Equal("IsEnabled", prop.Name);
                Assert.Equal(typeof(bool), prop.PropertyType);
                Assert.Equal(typeof(DependencyObjectFake), prop.OwnerType);
                Assert.True(prop.IsReadOnly);
            }
        }

        [Fact]
        public void RequireIDependencyObjectContract()
        {
            // related POCO
            using (new DefaultImplementationFixture())
                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.Register("IsEnabled", typeof(bool), typeof(Object), new PropertyMetadata(false)));

            using (new DefaultImplementationFixture())
                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.RegisterReadOnly("IsEnabled", typeof(bool), typeof(Object), new PropertyMetadata(false)));

            // unrelated POCO
            using (new DefaultImplementationFixture())
                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.Register("IsEnabled", typeof(bool), typeof(UnrelatedPOCOFake), new PropertyMetadata(false)));

            using (new DefaultImplementationFixture())
                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.RegisterReadOnly("IsEnabled", typeof(bool), typeof(UnrelatedPOCOFake), new PropertyMetadata(false)));
        }

        [Fact]
        public void AllowAttachedPropertyRegistrationOnPOCOs()
        {
            using (new DefaultImplementationFixture())
                Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(Object), new PropertyMetadata(false));

            using (new DefaultImplementationFixture())
                Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(UnrelatedPOCOFake), new PropertyMetadata(false));

            using (new DefaultImplementationFixture())
                Dependency.Property.RegisterAttachedReadOnly("IsEnabled", typeof(bool), typeof(Object), new PropertyMetadata(false));

            using (new DefaultImplementationFixture())
                Dependency.Property.RegisterAttachedReadOnly("IsEnabled", typeof(bool), typeof(UnrelatedPOCOFake), new PropertyMetadata(false));
        }

        [Fact]
        public void GetMemberPropertyMetadata()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                var actual = prop.GetMetadata(typeof(DependencyObjectFake));
                Assert.Same(metadata, actual);
            }
        }

        [Fact]
        public void GetAttachedPropertyMetadata()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                var actual = prop.GetMetadata(typeof(DependencyObjectFake));
                Assert.Same(metadata, actual);
            }
        }

        [Fact]
        public void GetMemberPropertyMetadataForDescendantType()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                var actual = prop.GetMetadata(typeof(DescendantDependencyObjectFake));
                Assert.Same(metadata, actual);
            }
        }

        [Fact]
        public void GetAttachedPropertyMetadataForDescendantType()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                var actual = prop.GetMetadata(typeof(DescendantDependencyObjectFake));
                Assert.Same(metadata, actual);
            }
        }

        [Fact]
        public void GetMemberPropertyMetadataForUnrelatedDependencyType()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                var actual = prop.GetMetadata(typeof(SecondDependencyObjectFake));
                Assert.NotSame(metadata, actual);
            }
        }

        [Fact]
        public void GetAttachedPropertyMetadataForUnrelatedDependencyType()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                var actual = prop.GetMetadata(typeof(SecondDependencyObjectFake));
                Assert.Same(metadata, actual);
            }
        }

        [Fact]
        public void GetMemberPropertyMetadataThrowsForPOCOTypes()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                Assert.Throws<InvalidOperationException>(() => prop.GetMetadata(typeof(Object)));
                Assert.Throws<InvalidOperationException>(() => prop.GetMetadata(typeof(UnrelatedPOCOFake)));
            }
        }

        [Fact]
        public void GetAttachedPropertyMetadataThrowsForPOCOTypes()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                Assert.Throws<InvalidOperationException>(() => prop.GetMetadata(typeof(Object)));
                Assert.Throws<InvalidOperationException>(() => prop.GetMetadata(typeof(UnrelatedPOCOFake)));
            }
        }

        private class UnrelatedPOCOFake { }

        private class DescendantDependencyObjectFake : DependencyObjectFake
        {
        }
    }
}
