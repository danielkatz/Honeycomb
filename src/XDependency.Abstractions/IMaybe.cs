using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public interface IMaybe<T>
    {
        bool HasValue { get; }
        T Value { get; }
    }
}