using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Honeycomb.Xaml.Abstractions;
using Honeycomb.Xaml.Tests.Fakes;

namespace Honeycomb.Xaml.Tests
{
    public class LocalValueStoreTests
    {
        [Fact]
        public void CanInstansiate()
        {
            var component = Mock.Of<IDependencyComponent>();
            var store = new LocalValueStore(component, 1);

            Assert.Same(component, store.Component);
            Assert.Equal(1, store.Order);
        }

        [Fact]
        public void CanSetValue()
        {
            var component = Mock.Of<IDependencyComponent>();
            var prop = Mock.Of<IDependencyProperty>();
            var store = new LocalValueStore(component, 0);

            var raised = 0;
            store.ValueChanged += (s, e) =>
            {
                Assert.Same(component, e.Component);
                Assert.Equal(Maybe.None<object>(), e.OldValue);
                Assert.Equal(Maybe.FromValue<object>("value"), e.NewValue);

                raised++;
            };

            store.SetValue(prop, "value");

            Assert.Equal(1, raised);
        }

        [Fact]
        public void CanChangeValue()
        {
            var component = Mock.Of<IDependencyComponent>();
            var prop = Mock.Of<IDependencyProperty>();
            var store = new LocalValueStore(component, 0);

            store.SetValue(prop, "old value");

            var raised = 0;
            store.ValueChanged += (s, e) =>
            {
                Assert.Same(component, e.Component);
                Assert.Equal(Maybe.FromValue<object>("old value"), e.OldValue);
                Assert.Equal(Maybe.FromValue<object>("new value"), e.NewValue);

                raised++;
            };

            store.SetValue(prop, "new value");

            Assert.Equal(1, raised);
        }

        [Fact]
        public void WontRaiseValueChangedOnEqualReferenceTypeValue()
        {
            var component = Mock.Of<IDependencyComponent>();
            var prop1 = Mock.Of<IDependencyProperty>();
            var prop2 = Mock.Of<IDependencyProperty>();
            var store = new LocalValueStore(component, 0);
            var val = new object();

            store.SetValue(prop1, "value");
            store.SetValue(prop2, val);

            var raised = 0;
            store.ValueChanged += (s, e) =>
            {
                raised++;
            };

            store.SetValue(prop1, "value");
            store.SetValue(prop2, val);

            Assert.Equal(0, raised);
        }

        [Fact]
        public void WontRaiseValueChangedOnEqualValueTypeValue()
        {
            var component = Mock.Of<IDependencyComponent>();
            var prop = Mock.Of<IDependencyProperty>();
            var store = new LocalValueStore(component, 0);

            store.SetValue(prop, 1);

            var raised = 0;
            store.ValueChanged += (s, e) =>
            {
                raised++;
            };

            store.SetValue(prop, 1);

            Assert.Equal(0, raised);
        }

        [Fact]
        public void CanClearValue()
        {
            var component = Mock.Of<IDependencyComponent>();
            var prop = Mock.Of<IDependencyProperty>();
            var store = new LocalValueStore(component, 0);

            store.SetValue(prop, "value");

            var raised = 0;
            store.ValueChanged += (s, e) =>
            {
                Assert.Same(component, e.Component);
                Assert.Equal(Maybe.FromValue<object>("value"), e.OldValue);
                Assert.Equal(Maybe.None<object>(), e.NewValue);

                raised++;
            };

            store.ClearValue(prop);

            Assert.Equal(1, raised);
            Assert.False(store.HasValue(prop));
        }

        [Fact]
        public void HasValueWorks()
        {
            var component = Mock.Of<IDependencyComponent>();
            var prop = Mock.Of<IDependencyProperty>();
            var store = new LocalValueStore(component, 0);

            Assert.False(store.HasValue(prop));

            store.SetValue(prop, "value");

            Assert.True(store.HasValue(prop));

            store.SetValue(prop, "other value");

            Assert.True(store.HasValue(prop));

            store.ClearValue(prop);

            Assert.False(store.HasValue(prop));

            store.SetValue(prop, "reassign value");

            Assert.True(store.HasValue(prop));
        }
    }
}
