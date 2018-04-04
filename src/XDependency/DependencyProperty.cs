using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyProperty : DependencyPropertyBase
    {
        public DependencyProperty(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
            : base(name, propertyType, ownerType, new PropertyMetadata(typeMetadata.DefaultValue))
        {
            metadataMap[ownerType] = typeMetadata;
        }
    }
}
