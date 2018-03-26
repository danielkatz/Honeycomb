using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public delegate void DependencyPropertyChangedCallback(IDependencyObject sender, IDependencyProperty dp);
}