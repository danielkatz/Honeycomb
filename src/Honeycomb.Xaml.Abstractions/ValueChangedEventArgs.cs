using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Abstractions
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

        public IDependencyComponent Component { get; }
        public IDependencyProperty Property { get; }
        public IMaybe<object> OldValue { get; }
        public IMaybe<object> NewValue { get; }
    }
}
