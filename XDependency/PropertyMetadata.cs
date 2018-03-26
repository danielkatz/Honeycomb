using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class PropertyMetadata : IPropertyMetadata
    {
        public PropertyMetadata(object defaultValue)
        {
            throw new NotImplementedException();
        }

        public PropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback)
        {
            throw new NotImplementedException();
        }

        public PropertyMetadata(CreateDefaultValueCallback createDefaultValueCallback)
        {
            throw new NotImplementedException();
        }

        public PropertyMetadata(CreateDefaultValueCallback createDefaultValueCallback, PropertyChangedCallback propertyChangedCallback)
        {
            throw new NotImplementedException();
        }

        public object DefaultValue => throw new NotImplementedException();

        public CreateDefaultValueCallback CreateDefaultValueCallback => throw new NotImplementedException();
    }
}
