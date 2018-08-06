using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Abstractions
{
    public interface IMaybe<T>
    {
        bool HasValue { get; }
        T Value { get; }
    }
}