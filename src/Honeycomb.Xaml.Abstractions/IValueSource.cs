using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Abstractions
{
    public interface IValueSource
    {
        event ValueChangedCallback ValueChanged;

        bool HasValue(IDependencyProperty dp);
        IMaybe<object> GetValue(IDependencyProperty dp);
        int Order { get; }
    }
}
