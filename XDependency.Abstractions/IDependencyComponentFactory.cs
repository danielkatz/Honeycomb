using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public interface IDependencyComponentFactory
    {
        IDependencyComponent Create(IDependencyObject obj);
    }
}
