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
                    () => Dependency.Property.Register("IsEnabled", typeof(bool), typeof(UnrelatedPOCO), new PropertyMetadata(false)));

            using (new DefaultImplementationFixture())
                Assert.Throws<InvalidOperationException>(
                    () => Dependency.Property.RegisterReadOnly("IsEnabled", typeof(bool), typeof(UnrelatedPOCO), new PropertyMetadata(false)));
        }

        [Fact]
        public void GetPropertyMetadata()
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
        public void GetPropertyMetadataForDcecendandType()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                var actual = prop.GetMetadata(typeof(DcecendandDependencyObjectFake));
                Assert.Same(metadata, actual);
            }
        }

        [Fact]
        public void GetPropertyMetadataForUnrelatedDependencyType()
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
        public void GetPropertyMetadataThrowsForPOCOTypes()
        {
            using (new DefaultImplementationFixture())
            {
                var metadata = new PropertyMetadata(false);
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), metadata);

                Assert.Throws<InvalidOperationException>(() => prop.GetMetadata(typeof(Object)));
                Assert.Throws<InvalidOperationException>(() => prop.GetMetadata(typeof(UnrelatedPOCO)));
            }
        }

        private class UnrelatedPOCO { }

        private class DcecendandDependencyObjectFake : DependencyObjectFake
        {
        }

        private class SecondDependencyObjectFake : IDependencyObject
        {
            public SecondDependencyObjectFake()
            {
                Component = Dependency.Component.Create(this);
            }

            public IDependencyComponent Component { get; }
        }
    }
}
