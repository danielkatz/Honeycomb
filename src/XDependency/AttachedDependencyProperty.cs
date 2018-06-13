using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class AttachedDependencyProperty : DependencyPropertyBase
    {
        public AttachedDependencyProperty(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
            : base(name, propertyType, ownerType, defaultMetadata)
        {
        }
    }
}
