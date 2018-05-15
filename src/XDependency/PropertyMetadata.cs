using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class PropertyMetadata : IPropertyMetadata
    {
        readonly object defaultValue;
        readonly CreateDefaultValueCallback createDefaultValueCallback;
        readonly PropertyChangedCallback propertyChangedCallback;

        public PropertyMetadata(object defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public PropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback)
        {
            this.defaultValue = defaultValue;
            this.propertyChangedCallback = propertyChangedCallback;
        }

        public PropertyMetadata(CreateDefaultValueCallback createDefaultValueCallback)
        {
            this.createDefaultValueCallback = createDefaultValueCallback;
        }

        public PropertyMetadata(CreateDefaultValueCallback createDefaultValueCallback, PropertyChangedCallback propertyChangedCallback)
        {
            this.createDefaultValueCallback = createDefaultValueCallback;
            this.propertyChangedCallback = propertyChangedCallback;
        }

        public void Merge(IPropertyMetadata baseMetadata)
        {
            throw new NotImplementedException();
        }

        public object DefaultValue => defaultValue;

        public CreateDefaultValueCallback CreateDefaultValueCallback => createDefaultValueCallback;

        public PropertyChangedCallback PropertyChangedCallback => propertyChangedCallback;
    }
}
