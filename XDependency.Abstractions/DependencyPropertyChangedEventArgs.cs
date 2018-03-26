using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public class DependencyPropertyChangedEventArgs
    {
        IDependencyProperty Property { get; }
        object OldValue { get; }
        object NewValue { get; }
    }
}
