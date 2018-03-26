using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public interface IDependencyObject
    {
        IDependencyComponent Component { get; }
    }
}
