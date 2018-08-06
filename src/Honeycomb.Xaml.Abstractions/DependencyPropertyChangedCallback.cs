using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Abstractions
{
    public delegate void DependencyPropertyChangedCallback(IDependencyObject sender, DependencyPropertyChangedEventArgs args);
}