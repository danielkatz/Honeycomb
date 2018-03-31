using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyProperty : IDependencyProperty
    {
        public DependencyProperty(string name, bool isReadOnly)
        {
            this.Name = name;
            this.IsReadOnly = isReadOnly;
        }

        public IPropertyMetadata GetMetadata(Type forType)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }
        public bool IsReadOnly { get; }
    }
}
