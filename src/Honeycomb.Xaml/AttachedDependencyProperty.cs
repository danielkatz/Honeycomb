using System;
using System.Collections.Generic;
using System.Text;
using Honeycomb.Xaml.Abstractions;

namespace Honeycomb.Xaml
{
    public class AttachedDependencyProperty : DependencyPropertyBase
    {
        public AttachedDependencyProperty(string name, Type propertyType, Type ownerType, IPropertyMetadata defaultMetadata)
            : base(name, propertyType, ownerType, defaultMetadata)
        {
        }
    }
}
