using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XDependency.Abstractions;
using XDependency.Abstractions.Extensions;
using XDependency.Helpers;

namespace XDependency
{
    public class DependencyComponent : IDependencyComponent
    {
        readonly Type ownerType;
        readonly IDependencyObject owner;

        readonly IReadOnlyList<IValueSource> valueSources;
        readonly LocalValueStore localStore;
        readonly Dictionary<IDependencyProperty, IValueSource> effectiveSources;

        public event DependencyPropertyChangedCallback PropertyChanged; // TODO: should be a weak event

        public DependencyComponent(IDependencyObject owner)
        {
            this.owner = owner;
            this.ownerType = owner.GetType();

            this.valueSources = Dependency.ValueSources.GetValueSources(this);
            this.effectiveSources = new Dictionary<IDependencyProperty, IValueSource>();
            this.localStore = GetValueSource<LocalValueStore>();

            for (int i = 0; i < this.valueSources.Count; i++)
            {
                this.valueSources[i].ValueChanged += OnValueChanged;
            }
        }

        private IMaybe<object> ResolveEffectiveStoresValue(IDependencyProperty dp)
        {
            if (effectiveSources.TryGetValue(dp, out var source))
            {
                return source.GetValue(dp);
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

            if (e.OldValue.HasValue && e.NewValue.HasValue) // value_a -> value_b
            {
                effectiveSources[e.Property] = source;

                RaisePropertyChanged(e.Property, metadata, e.OldValue.Value, e.NewValue.Value);
            }
            else
            {
                var below = GetHighestPrecedenceSetValueSource(e.Property, source.Order + 1);
                var belowValue = below?.GetValue(e.Property) ?? Maybe.None<object>();
                var previousValue = belowValue.Case(s => s, () => metadata.DefaultValue);

                if (!e.OldValue.HasValue) // none -> value
                {
                    effectiveSources[e.Property] = source;

                    if (!object.Equals(e.NewValue.Value, previousValue))
                    {
                        RaisePropertyChanged(e.Property, metadata, previousValue, e.NewValue.Value);
                    }
                }
                else if (!e.NewValue.HasValue) // value -> none
                {
                    if (below == null)
                    {
                        effectiveSources.Remove(e.Property);
                    }
                    else
                    {
                        effectiveSources[e.Property] = below;
                    }

                    if (!object.Equals(e.OldValue.Value, previousValue))
                    {
                        RaisePropertyChanged(e.Property, metadata, e.OldValue.Value, previousValue);
                    }
                }
            }
        }

        private void RaisePropertyChanged(IDependencyProperty dp, IPropertyMetadata metadata, object oldValue, object newValue)
        {
            var args = new DependencyPropertyChangedEventArgs(dp, oldValue, newValue);

            metadata.PropertyChangedCallback?.Invoke(this.owner, args);
            PropertyChanged?.Invoke(this.owner, args);
        }

        public object GetValue(IDependencyProperty dp)
        {
            var maybe = ResolveEffectiveStoresValue(dp);
            if (maybe.HasValue)
            {
                return maybe.Value;
            }

            var metadata = dp.GetMetadata(ownerType);
            return metadata.DefaultValue;
        }

        public void SetValue(IDependencyProperty dp, object value)
        {
            dp.EnsureNotReadOnly();

            localStore.SetValue(dp, value);
        }

        public void SetValue(IDependencyPropertyKey key, object value)
        {
            localStore.SetValue(key.DependencyProperty, value);
        }

        public void ClearValue(IDependencyProperty dp)
        {
            dp.EnsureNotReadOnly();

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

        public IPropertyMetadata GetMetadata(IDependencyProperty dp)
        {
            return dp.GetMetadata(this.ownerType);
        }

        public T GetValueSource<T>() where T : IValueSource
        {
            return valueSources.OfType<T>().First();
        }

        public IReadOnlyCollection<IDependencyProperty> SetProperties => effectiveSources.Keys;
    }
}
