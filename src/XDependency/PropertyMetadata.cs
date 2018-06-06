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
        readonly DependencyPropertyChangedCallback propertyChangedCallback;

        public PropertyMetadata(object defaultValue, DependencyPropertyChangedCallback propertyChangedCallback = null, bool inherits = false)
        {
            this.defaultValue = defaultValue;
            this.propertyChangedCallback = propertyChangedCallback;
            this.Inherits = inherits;
        }

        public PropertyMetadata(CreateDefaultValueCallback createDefaultValueCallback, DependencyPropertyChangedCallback propertyChangedCallback = null, bool inherits = false)
        {
            this.createDefaultValueCallback = createDefaultValueCallback;
            this.propertyChangedCallback = propertyChangedCallback;
            this.Inherits = inherits;
        }

        public void Merge(IPropertyMetadata baseMetadata)
        {
            throw new NotImplementedException();
        }

        public object DefaultValue => defaultValue;

        public bool Inherits { get; private set; }

        public CreateDefaultValueCallback CreateDefaultValueCallback => createDefaultValueCallback;

        public DependencyPropertyChangedCallback PropertyChangedCallback => propertyChangedCallback;
    }
}
