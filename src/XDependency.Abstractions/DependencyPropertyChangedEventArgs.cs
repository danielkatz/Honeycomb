using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public class DependencyPropertyChangedEventArgs
    {
        public DependencyPropertyChangedEventArgs(IDependencyProperty dp, object oldValue, object newValue)
        {
            Property = dp;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public IDependencyProperty Property { get; }
        public object OldValue { get; }
        public object NewValue { get; }
    }
}
