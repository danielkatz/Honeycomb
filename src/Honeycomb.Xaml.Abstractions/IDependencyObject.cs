using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Abstractions
{
    public interface IDependencyObject
    {
        IDependencyComponent Component { get; }
    }
}
