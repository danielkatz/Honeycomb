using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class MemberDependencyProperty : DependencyPropertyBase
    {
        public MemberDependencyProperty(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
            : base(name, propertyType, ownerType, new PropertyMetadata(typeMetadata.DefaultValue))
        {
            ownerType.EnsureDependencyObject();

            metadataMap[ownerType] = typeMetadata;
        }

        public override IPropertyMetadata GetMetadata(Type forType)
        {
            forType.EnsureDependencyObject();

            for (var type = forType; type.IsDependencyObject(); type = type.BaseType)
            {
                if (metadataMap.TryGetValue(type, out var metadata))
                {
                    return metadata;
                }
            }

            return DefaultMetadata;
        }
    }
}
