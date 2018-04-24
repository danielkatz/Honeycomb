using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public class ValueChangedEventArgs
    {
        public ValueChangedEventArgs(IDependencyComponent component, IDependencyProperty property, IMaybe<object> oldValue, IMaybe<object> newValue)
        {
            Component = component;
            Property = property;
            OldValue = oldValue;
            NewValue = newValue;
        }

        IDependencyComponent Component { get; }
        IDependencyProperty Property { get; }
        IMaybe<object> OldValue { get; }
        IMaybe<object> NewValue { get; }
    }
}
