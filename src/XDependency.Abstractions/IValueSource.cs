﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public interface IValueSource
    {
        bool HasValue(IDependencyProperty dp);
        bool TryGetValue(IDependencyProperty dp, out object value);

        // TODO: Property change notification mechanism
    }
}
