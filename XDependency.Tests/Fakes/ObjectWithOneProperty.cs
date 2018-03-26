using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;
using XDependency.Abstractions.Extensions;

namespace XDependency.Tests.Fakes
{
    public class ObjectWithOneProperty : IDependencyObject
    {
        readonly IDependencyComponent component;

        public static readonly IDependencyProperty StateProperty = Dependency.Property.Register(
            nameof(State), typeof(Boolean), typeof(ObjectWithOneProperty), new PropertyMetadata(false));

        public ObjectWithOneProperty()
        {
            component = Dependency.Component.Create(this);
        }

        public IDependencyComponent Component => component;

        public bool State
        {
            get => (bool)this.GetValue(StateProperty);
            set => this.SetValue(StateProperty, value);
        }
    }
}
