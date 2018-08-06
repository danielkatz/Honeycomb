using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Abstractions
{
    public interface IDependencyComponentFactory
    {
        IDependencyComponent Create(IDependencyObject obj);
    }
}
