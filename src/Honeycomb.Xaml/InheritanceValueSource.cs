using System;
using System.Collections.Generic;
using System.Text;
using Honeycomb.Xaml.Abstractions;
using Honeycomb.Xaml.Utility;

namespace Honeycomb.Xaml
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
            var allSetProperties = new HashSet<IDependencyProperty>();
            if (oldParent != null)
            {
                oldParent.PropertyChanged -= OnParentPropertyChanged;

                foreach (var dp in oldParent.SetProperties)
                {
                    if (Component.GetMetadata(dp).IsInherited)
                    {
                        allSetProperties.Add(dp);
                    }
                }
            }

            if (newParent != null)
            {
                newParent.PropertyChanged += OnParentPropertyChanged;

                foreach (var dp in newParent.SetProperties)
                {
                    if (Component.GetMetadata(dp).IsInherited)
                    {
                        allSetProperties.Add(dp);
                    }
                }
            }

            foreach (var dp in allSetProperties)
            {
                var oldValue = oldParent != null
                    ? Maybe.FromValue(oldParent.GetValue(dp))
                    : Maybe.None<object>();

                var newValue = newParent != null
                    ? Maybe.FromValue(newParent.GetValue(dp))
                    : Maybe.None<object>();

                if (Helpers.AreDifferent(oldValue, newValue))
                {
                    ValueChanged?.Invoke(this, new ValueChangedEventArgs(Component, dp, oldValue, newValue));
                }
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
