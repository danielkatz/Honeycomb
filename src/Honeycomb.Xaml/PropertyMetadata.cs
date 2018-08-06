using System;
using System.Collections.Generic;
using System.Text;
using Honeycomb.Xaml.Abstractions;

namespace Honeycomb.Xaml
{
    public class PropertyMetadata : IPropertyMetadata
    {
        readonly object defaultValue;
        readonly CreateDefaultValueCallback createDefaultValueCallback;

        public PropertyMetadata(object defaultValue, DependencyPropertyChangedCallback propertyChangedCallback = null, bool isInherited = false)
        {
            this.defaultValue = defaultValue;
            this.PropertyChangedCallback = propertyChangedCallback;
            this.IsInherited = isInherited;
        }

        public PropertyMetadata(CreateDefaultValueCallback createDefaultValueCallback, DependencyPropertyChangedCallback propertyChangedCallback = null, bool isInherited = false)
        {
            this.createDefaultValueCallback = createDefaultValueCallback;
            this.PropertyChangedCallback = propertyChangedCallback;
            this.IsInherited = isInherited;
        }

        public void Merge(IPropertyMetadata baseMetadata)
        {
            if (ReferenceEquals(this, baseMetadata))
                return;

            IsInherited = baseMetadata.IsInherited;

            if (baseMetadata.PropertyChangedCallback != null)
            {
                Delegate[] invocationList = baseMetadata.PropertyChangedCallback.GetInvocationList();
                if (invocationList.Length > 0)
                {
                    var propertyChangedCallback = (DependencyPropertyChangedCallback)invocationList[0];

                    for (int index = 1; index < invocationList.Length; ++index)
                    {
                        propertyChangedCallback = (DependencyPropertyChangedCallback)Delegate.Combine((Delegate)propertyChangedCallback, invocationList[index]);
                    }

                    PropertyChangedCallback = propertyChangedCallback + this.PropertyChangedCallback;
                }
            }

            MergeProtected(baseMetadata);
        }

        protected virtual void MergeProtected(IPropertyMetadata baseMetadata)
        {
        }

        public object DefaultValue
        {
            get
            {
                return (createDefaultValueCallback != null)
                    ? createDefaultValueCallback()
                    : defaultValue;
            }
        }

        public bool IsInherited { get; private set; }

        public CreateDefaultValueCallback CreateDefaultValueCallback => createDefaultValueCallback;

        public DependencyPropertyChangedCallback PropertyChangedCallback { get; private set; }
    }
}
