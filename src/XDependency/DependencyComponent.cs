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

        public void OnValueChanged(IValueSource source, ValueChangedEventArgs e)
        {
            //throw new NotImplementedException();
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
