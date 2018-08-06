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
        public void PreventTwoPropertiesWithTheSameName()
        {
            using (new DefaultImplementationFixture())
            {
                var first = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(false));

                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(false)));

                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(false)));

                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.RegisterReadOnly("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(false)));

                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.RegisterAttachedReadOnly("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(false)));
            }
        }

        [Fact]
        public void PreventTwoPropertiesWithTheSameNameInDescendant()
        {
            using (new DefaultImplementationFixture())
            {
                var first = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(false));

                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DescendantDependencyObjectFake), new PropertyMetadata(false)));

                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DescendantDependencyObjectFake), new PropertyMetadata(false)));

                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DescendantDependencyObjectFake), new PropertyMetadata(false)));

                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.RegisterReadOnly("IsEnabled", typeof(bool), typeof(DescendantDependencyObjectFake), new PropertyMetadata(false)));

                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.RegisterAttachedReadOnly("IsEnabled", typeof(bool), typeof(DescendantDependencyObjectFake), new PropertyMetadata(false)));
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
        public void GetAttachedPropertyMetadataDoesNotThrowForPOCOTypes()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.RegisterAttached("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                Assert.Same(metadata, prop.GetMetadata(typeof(UnrelatedPOCOFake)));
                Assert.Same(metadata, prop.GetMetadata(typeof(Object)));
            }
        }

        private class UnrelatedPOCOFake { }

        private class DescendantDependencyObjectFake : DependencyObjectFake
        {
        }
    }
}
