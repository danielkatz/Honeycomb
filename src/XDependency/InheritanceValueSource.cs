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
                if (metadata.IsInherited)
                {
                    return true;
                }
            }
            return false;
        }

        public void OnInheritanceParentChanged(IDependencyComponent oldParent, IDependencyComponent newParent)
        {
            if (oldParent != null)
            {
                oldParent.PropertyChanged -= OnParentPropertyChanged;
            }
            if (newParent != null)
            {
                newParent.PropertyChanged += OnParentPropertyChanged;
            }
        }

        private void OnParentPropertyChanged(IDependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(sender.Component, args.Property, Maybe.FromValue(args.OldValue), Maybe.FromValue(args.NewValue)));
        }

        public int Order { get; }
        public IDependencyComponent Component { get; }
        public IDependencyComponent ParentComponent
        {
            get => parent;
            set
            {
                if (!object.ReferenceEquals(parent, value))
                {
                    var oldParent = parent;
                    parent = value;

                    OnInheritanceParentChanged(oldParent, value);
                }
            }
        }
    }
}
