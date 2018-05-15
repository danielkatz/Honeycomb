using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XDependency.Abstractions;
using XDependency.Abstractions.Extensions;

namespace XDependency
{
    public class DependencyComponent : IDependencyComponent
    {
        readonly Type ownerType;
        readonly IDependencyObject owner;

        readonly IReadOnlyList<IValueSource> valueSources;
        readonly LocalValueStore localStore;

        public DependencyComponent(IDependencyObject owner)
        {
            this.owner = owner;
            this.ownerType = owner.GetType();

            this.valueSources = Dependency.ValueSources.GetValueSources(this);
            this.localStore = GetValueStore<LocalValueStore>();

            for (int i = 0; i < this.valueSources.Count; i++)
            {
                this.valueSources[i].ValueChanged += OnValueChanged;
            }
        }

        private IMaybe<object> GetEffectiveValue(IDependencyProperty dp)
        {
            for (int i = 0; i < valueSources.Count; i++)
            {
                var maybe = valueSources[i].GetValue(dp);
                if (maybe.HasValue)
                {
                    return maybe;
                }
            }
            return Maybe.None<object>();
        }

        private IValueSource GetHighestPrecedenceSetValueSource(IDependencyProperty dp, int startIndex = 0)
        {
            for (int i = startIndex; i < valueSources.Count; i++)
            {
                var store = valueSources[i];
                if (store.HasValue(dp))
                {
                    return store;
                }
            }

            return null;
        }

        public void OnValueChanged(IValueSource source, ValueChangedEventArgs e)
        {
            var highestSetValueSource = GetHighestPrecedenceSetValueSource(e.Property, 0);

            if (highestSetValueSource != null && source.Order > highestSetValueSource.Order)
                return;

            var metadata = GetMetadata(e.Property);

            if (e.OldValue.HasValue && e.NewValue.HasValue)
            {
                RaisePropertyChanged(e.Property, metadata, e.OldValue.Value, e.NewValue.Value);
            }
            else
            {
                var below = GetHighestPrecedenceSetValueSource(e.Property, source.Order + 1);
                var belowValue = below?.GetValue(e.Property) ?? Maybe.None<object>();
                var previousValue = belowValue.Case(s => s, () => metadata.DefaultValue);

                if (!e.OldValue.HasValue) // none -> value
                {
                    if (!object.Equals(e.NewValue.Value, previousValue))
                    {
                        RaisePropertyChanged(e.Property, metadata, previousValue, e.NewValue.Value);
                    }
                }
                else if (!e.NewValue.HasValue) // value -> none
                {
                    if (!object.Equals(e.OldValue.Value, previousValue))
                    {
                        RaisePropertyChanged(e.Property, metadata, e.OldValue.Value, previousValue);
                    }
                }
            }
        }

        void RaisePropertyChanged(IDependencyProperty dp, IPropertyMetadata metadata, object oldValue, object newValue)
        {
            if (metadata.PropertyChangedCallback != null)
            {
                metadata.PropertyChangedCallback(this.owner, new DependencyPropertyChangedEventArgs(dp, oldValue, newValue));
            }
        }

        public object GetValue(IDependencyProperty dp)
        {
            var maybe = GetEffectiveValue(dp);
            if (maybe.HasValue)
            {
                return maybe.Value;
            }

            var metadata = dp.GetMetadata(ownerType);
            return metadata.DefaultValue;
        }

        public void SetValue(IDependencyProperty dp, object value)
        {
            EnsureNotReadOnly(dp);

            localStore.SetValue(dp, value);
        }

        public void SetValue(IDependencyPropertyKey key, object value)
        {
            localStore.SetValue(key.DependencyProperty, value);
        }

        public void ClearValue(IDependencyProperty dp)
        {
            EnsureNotReadOnly(dp);

            localStore.ClearValue(dp);
        }

        public void ClearValue(IDependencyPropertyKey key)
        {
            localStore.ClearValue(key.DependencyProperty);
        }

        public object ReadLocalValue(IDependencyProperty dp)
        {
            throw new NotImplementedException();
        }

        public object GetAnimationBaseValue(IDependencyProperty dp)
        {
            throw new NotImplementedException();
        }

        public long RegisterPropertyChangedCallback(IDependencyProperty dp, DependencyPropertyChangedCallback callback)
        {
            throw new NotImplementedException();
        }

        public void UnregisterPropertyChangedCallback(IDependencyProperty dp, long token)
        {
            throw new NotImplementedException();
        }

        public IPropertyMetadata GetMetadata(IDependencyProperty dp)
        {
            return dp.GetMetadata(this.ownerType);
        }

        public T GetValueStore<T>() where T : IValueStore
        {
            return valueSources.OfType<T>().First();
        }

        static void EnsureNotReadOnly(IDependencyProperty dp)
        {
            if (dp.IsReadOnly)
                throw new InvalidOperationException($"{dp.Name} is a read only property and should be changed using an {nameof(IDependencyPropertyKey)}");
        }
    }
}
