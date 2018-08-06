using System;
using System.Collections.Generic;
using System.Text;
using Honeycomb.Xaml.Abstractions;
using Honeycomb.Xaml.Utility;

namespace Honeycomb.Xaml
{
    public abstract class ValueStoreBase : IValueSource, IValueStore
    {
        protected readonly Dictionary<IDependencyProperty, object> store = new Dictionary<IDependencyProperty, object>();

        public event ValueChangedCallback ValueChanged;

        public ValueStoreBase(IDependencyComponent component, int order)
        {
            this.Component = component;
            this.Order = order;
        }

        public virtual bool HasValue(IDependencyProperty dp)
        {
            return store.ContainsKey(dp);
        }

        public virtual IMaybe<object> GetValue(IDependencyProperty dp)
        {
            return store.MaybeGetValue(dp);
        }

        public virtual void SetValue(IDependencyProperty dp, object value)
        {
            var oldValue = GetValue(dp);
            var newValue = Maybe.FromValue(value);

            if (Helpers.AreDifferent(oldValue, newValue))
            {
                store[dp] = value;

                ValueChanged?.Invoke(this, new ValueChangedEventArgs(Component, dp, oldValue, newValue));
            }
        }

        public virtual void ClearValue(IDependencyProperty dp)
        {
            if (store.ContainsKey(dp))
            {
                var oldValue = Maybe.FromValue(store[dp]);
                var newValue = Maybe.None<object>();

                store.Remove(dp);

                ValueChanged?.Invoke(this, new ValueChangedEventArgs(Component, dp, oldValue, newValue));
            }
        }

        public IDependencyComponent Component { get; }
        public int Order { get; }
    }
}
