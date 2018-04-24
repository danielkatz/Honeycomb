using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public delegate void ValueChangedCallback(IValueSource source, ValueChangedEventArgs e);
}