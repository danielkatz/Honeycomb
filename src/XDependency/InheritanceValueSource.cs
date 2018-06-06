using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class InheritanceValueSource : IValueSource
    {
        private IDependencyComponent parent;

        public event ValueChangedCallback ValueChanged;

        public InheritanceValueSource(IDependencyComponent component, int order)
        {
            this.Component = component;
            this.Order = order;
        }

        public IMaybe<object> GetValue(IDependencyProperty dp)
        {
            if (HasValue(dp))
            {
                return Maybe.FromValue(parent.GetValue(dp));
            }
            return Maybe.None<object>();
        }

        public bool HasValue(IDependencyProperty dp)
        {
            if (parent != null)
            {
                var metadata = Component.GetMetadata(dp);
                if (metadata.Inherits)
                {
                    return true;
                }
            }
            return false;
        }

        public void OnInheritanceParentChanged(IDependencyComponent oldParent, IDependencyComponent newParent)
        {
            
        }

        public int Order { get; }
        public IDependencyComponent Component { get; }
        public IDependencyComponent ParentComponent
        {
            get => parent;
            set
            {
                if (!object.Equals(parent, value))
                {
                    var oldParent = parent;
                    parent = value;

                    OnInheritanceParentChanged(oldParent, value);
                }
            }
        }
    }
}
