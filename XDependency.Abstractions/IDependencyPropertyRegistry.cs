﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public interface IDependencyPropertyRegistry
    {
        IDependencyProperty Register(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata);
        IDependencyProperty RegisterAttached(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata);
    }
}
