using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;

namespace XDependency.Tests.Fakes
{
    public class ValueStoreFake : IValueStore, IValueSource
    {
        public int Order => throw new NotImplementedException();

        public void ClearValue(IDependencyProperty dp)
        {
            throw new NotImplementedException();
        }

        public bool HasValue(IDependencyProperty dp)
        {
            throw new NotImplementedException();
        }

        public void SetValue(IDependencyProperty dp, object value)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(IDependencyProperty dp, out object value)
        {
            throw new NotImplementedException();
        }
    }
}
