using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Abstractions
{
    public interface IValueStore
    {
        void SetValue(IDependencyProperty dp, object value);
        void ClearValue(IDependencyProperty dp);
    }
}
