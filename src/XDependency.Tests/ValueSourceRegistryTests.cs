using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;
using Xunit;

namespace XDependency.Tests
{
    public class ValueSourceRegistryTests
    {
        [Fact]
        public void CanAddFirst()
        {
            var registry = new ValueSourceRegistry();

            registry.AddFirst(x => new SecondValueSource());
            registry.AddFirst(x => new FirstValueSource());

            Assert.Equal(typeof(FirstValueSource), registry.First());
            Assert.Collection(registry, first => { }, second => { });
        }

        [Fact]
        public void CanAddLast()
        {
            var registry = new ValueSourceRegistry();

            registry.AddLast(x => new FirstValueSource());
            registry.AddLast(x => new SecondValueSource());

            Assert.Equal(typeof(SecondValueSource), registry.Last());
            Assert.Collection(registry, first => { }, second => { });
        }

        [Fact]
        public void CanAddBefore()
        {
            var registry = new ValueSourceRegistry();

            registry.AddFirst(x => new SecondValueSource());
            registry.AddBefore<FirstValueSource, SecondValueSource>(x => new FirstValueSource());

            Assert.Equal(typeof(FirstValueSource), registry.First());
            Assert.Collection(registry, first => { }, second => { });
        }

        [Fact]
        public void CanAddAfter()
        {
            var registry = new ValueSourceRegistry();

            registry.AddLast(x => new FirstValueSource());
            registry.AddLast(x => new ThirdValueSource());
            registry.AddAfter<SecondValueSource, FirstValueSource>(x => new SecondValueSource());

            Assert.Equal(typeof(SecondValueSource), registry.ElementAt(1));
            Assert.Collection(registry, first => { }, second => { }, third => { });
        }

        [Fact]
        public void CanNotAddSameValueSourceTwice()
        {
            var registry = new ValueSourceRegistry();

            registry.AddLast(x => new FirstValueSource());

            Assert.Throws<ArgumentException>(() => registry.AddFirst(x => new FirstValueSource()));
            Assert.Throws<ArgumentException>(() => registry.AddBefore<FirstValueSource, FirstValueSource>(x => new FirstValueSource()));
            Assert.Throws<ArgumentException>(() => registry.AddAfter<FirstValueSource, FirstValueSource>(x => new FirstValueSource()));
            Assert.Throws<ArgumentException>(() => registry.AddLast(x => new FirstValueSource()));

            Assert.Collection(registry, i => { });
        }

        class FirstValueSource : IValueSource
        {
            public int Order => throw new NotImplementedException();
            public bool HasValue(IDependencyProperty dp) => throw new NotImplementedException();
            public bool TryGetValue(IDependencyProperty dp, out object value) => throw new NotImplementedException();
        }

        class SecondValueSource : IValueSource
        {
            public int Order => throw new NotImplementedException();
            public bool HasValue(IDependencyProperty dp) => throw new NotImplementedException();
            public bool TryGetValue(IDependencyProperty dp, out object value) => throw new NotImplementedException();
        }

        class ThirdValueSource : IValueSource
        {
            public int Order => throw new NotImplementedException();
            public bool HasValue(IDependencyProperty dp) => throw new NotImplementedException();
            public bool TryGetValue(IDependencyProperty dp, out object value) => throw new NotImplementedException();
        }
    }
}
