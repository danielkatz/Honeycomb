using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class MemberDependencyProperty : DependencyPropertyBase
    {
        public MemberDependencyProperty(string name, Type propertyType, Type ownerType, IPropertyMetadata typeMetadata)
            : base(name, propertyType, ownerType, CreateDefaultPropertyMetadata(typeMetadata))
        {
            OverrideMetadata(ownerType, typeMetadata);
        }

        public override IPropertyMetadata GetMetadata(Type forType)
        {
            forType.EnsureDependencyObject();

            return base.GetMetadata(forType);
        }

        protected override void OverrideMetadataCommon(Type forType, IPropertyMetadata typeMetadata)
        {
            forType.EnsureDependencyObject();

            base.OverrideMetadataCommon(forType, typeMetadata);
        }

        private static IPropertyMetadata CreateDefaultPropertyMetadata(IPropertyMetadata typeMetadata)
        {
            if (typeMetadata.CreateDefaultValueCallback != null)
            {
                return new PropertyMetadata(typeMetadata.CreateDefaultValueCallback);
            }
            return new PropertyMetadata(typeMetadata.DefaultValue);
        }
    }
}
