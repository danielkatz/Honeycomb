using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public interface IValueStore
    {
        void SetValue(IDependencyProperty dp, object value);
        void ClearValue(IDependencyProperty dp);
    }
}
