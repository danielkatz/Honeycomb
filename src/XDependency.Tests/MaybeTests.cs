using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency;
using Xunit;

namespace XDependency.Tests
{
    public class MaybeTests
    {
        [Fact]
        public void CanConstructFromValueType()
        {
            var maybe = Maybe<int>.FromValue(1);

            Assert.Equal(1, maybe.Value);
        }

        [Fact]
        public void CanConstructFromReferenceType()
        {
            var maybe = Maybe<string>.FromValue("test");

            Assert.Equal("test", maybe.Value);
        }

        [Fact]
        public void ThrowsWhenEmpty()
        {
            var maybe = Maybe<object>.None;

            Assert.False(maybe.HasValue);
            Assert.Throws<InvalidOperationException>(() => maybe.Value);
        }

        [Fact]
        public void Equal1()
        {
            var maybe1 = Maybe<object>.None;
            var maybe2 = Maybe<object>.None;

            Assert.Equal(maybe1, maybe2);
            Assert.Equal(maybe1.GetHashCode(), maybe2.GetHashCode());
        }

        [Fact]
        public void Equal2()
        {
            var maybe1 = Maybe<string>.FromValue("fox");
            var maybe2 = Maybe<string>.FromValue("fox");

            Assert.Equal(maybe1, maybe2);
            Assert.Equal(maybe1.GetHashCode(), maybe2.GetHashCode());
        }

        [Fact]
        public void Equal3()
        {
            var maybe1 = Maybe<string>.None;
            var maybe2 = Maybe<string>.FromValue("fox");

            Assert.NotEqual(maybe1, maybe2);
            Assert.NotEqual(maybe1.GetHashCode(), maybe2.GetHashCode());
        }

        [Fact]
        public void Equal4()
        {
            var maybe1 = Maybe<object>.None;
            var maybe2 = Maybe<string>.None;

            Assert.False(object.Equals(maybe1, maybe2));
        }

        [Fact]
        public void Equal5()
        {
            var maybe1 = Maybe<object>.FromValue("fox");
            var maybe2 = Maybe<string>.FromValue("fox");

            Assert.True(object.Equals(maybe1, maybe2));
        }

        [Fact]
        public void Equal6()
        {
            var maybe = Maybe<object>.FromValue("fox");

            Assert.True(object.Equals(maybe, "fox"));
        }

        [Fact]
        public void Equal7()
        {
            var maybe = Maybe<object>.None;

            Assert.False(object.Equals(maybe, "fox"));
        }

        [Fact]
        public void Equal8()
        {
            var maybe = Maybe<object>.None;

            Assert.False(object.Equals(maybe, null));
        }

        [Fact]
        public void Equal9()
        {
            var maybe1 = Maybe<string>.None;
            var maybe2 = Maybe<string>.FromValue("fox");

            Assert.False(object.Equals(maybe1, new object()));
            Assert.False(object.Equals(maybe2, new object()));
        }

        [Fact]
        public void Implicit()
        {
            Maybe<string> maybe = "fox";

            Assert.True(maybe.HasValue);
            Assert.Equal("fox", maybe.Value);
        }
    }
}
