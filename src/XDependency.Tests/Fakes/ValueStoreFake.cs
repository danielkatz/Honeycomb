using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;

namespace XDependency.Tests.Fakes
{
    public class ValueStoreFake : ValueStoreBase
    {
        public ValueStoreFake(IDependencyComponent component, int order) : base(component, order)
        {
        }
    }
}
