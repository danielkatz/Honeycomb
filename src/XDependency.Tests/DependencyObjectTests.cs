﻿using System;
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
    public class DependencyObjectTests
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
        public void GetPropertyDefaultValue()
        {
            using (new DefaultImplementationFixture())
            {
                var prop = Dependency.Property.Register("IsEnabled", typeof(bool), typeof(DependencyObjectFake), new PropertyMetadata(true));
                var owner = new DependencyObjectFake();

                Assert.True((bool)owner.GetValue(prop));
            }
        }

        [Fact]
        public void SetPropertyValue()
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
        public void SetReadonlyPropertyValueViaDP()
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
        public void SetReadonlyPropertyValueViaDPKey()
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
        public void ClearPropertyValue()
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
    }
}
