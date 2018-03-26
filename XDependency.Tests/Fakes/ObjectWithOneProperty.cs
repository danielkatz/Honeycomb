using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;

namespace XDependency.Tests.Fakes
{
    public class ObjectWithOneProperty : IDependencyObject
    {
        readonly IDependencyComponent component;

        public static readonly IDependencyProperty StateProperty = Dependency.Property.Register(
            "State", typeof(Boolean), typeof(ObjectWithOneProperty), new PropertyMetadata(false));

        public ObjectWithOneProperty()
        {
            component = Dependency.Component.Create(this);
        }

        public IDependencyComponent Component => component;
    }
}
