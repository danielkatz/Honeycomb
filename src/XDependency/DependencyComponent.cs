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
        }

        private bool TryGetEffectiveValue(IDependencyProperty dp, out object value)
        {
            for (int i = 0; i < valueSources.Count; i++)
            {
                var source = valueSources[i];

                if (source.TryGetValue(dp, out value))
                {
                    return true;
                }
            }
            value = null;
            return false;
        }

        public object GetValue(IDependencyProperty dp)
        {
            if (TryGetEffectiveValue(dp, out var value))
            {
                return value;
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
