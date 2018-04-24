using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public interface IValueSource
    {
        event ValueChangedCallback ValueChanged;

        bool HasValue(IDependencyProperty dp);
        IMaybe<object> GetValue(IDependencyProperty dp);
        int Order { get; }
    }
}
