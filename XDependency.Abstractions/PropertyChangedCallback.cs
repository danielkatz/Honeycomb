using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public delegate void PropertyChangedCallback(IDependencyObject d, DependencyPropertyChangedEventArgs e);
}