using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Tests.Fakes;
using XDependency.Tests.Fixtures;
using Xunit;

namespace XDependency.Tests
{
    public class BasicTests : IClassFixture<DefaultImplementationFixture>
    {
        [Fact]
        public void InstansiateEmptyObject()
        {
            var obj = new EmptyObject();
        }

        [Fact]
        public void InstansiateObjectWithOneProperty()
        {
            var obj = new ObjectWithOneProperty();
        }

        [Fact]
        public void GetPropertyDefaultValue()
        {
            var obj = new ObjectWithOneProperty();
            Assert.False(obj.State);
        }

        [Fact]
        public void SetPropertyValue()
        {
            var obj = new ObjectWithOneProperty();
            obj.State = true;
            Assert.True(obj.State);
        }
    }
}
