using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;
using XDependency.Abstractions.Extensions;

namespace XDependency
{
    public class DependencyComponent : IDependencyComponent
    {
        readonly Type ownerType;
        readonly IDependencyObject owner;

        readonly Dictionary<IDependencyProperty, object> localStore = new Dictionary<IDependencyProperty, object>();

        public DependencyComponent(IDependencyObject owner)
        {
            this.owner = owner;
            this.ownerType = owner.GetType();
        }

        public object GetValue(IDependencyProperty dp)
        {
            if (localStore.TryGetValue(dp, out var value))
            {
                return value;
            }
            var metadata = dp.GetMetadata(ownerType);
            return metadata.DefaultValue;
        }

        public void SetValue(IDependencyProperty dp, object value)
        {
            EnsureNotReadOnly(dp);

            localStore[dp] = value;
        }

        public void SetValue(IDependencyPropertyKey key, object value)
        {
            localStore[key.DependencyProperty] = value;
        }

        public void ClearValue(IDependencyProperty dp)
        {
            EnsureNotReadOnly(dp);

            throw new NotImplementedException();
        }

        public void ClearValue(IDependencyPropertyKey key)
        {
            throw new NotImplementedException();
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

        static void EnsureNotReadOnly(IDependencyProperty dp)
        {
            if (dp.IsReadOnly)
                throw new InvalidOperationException($"{dp.Name} is a read only property and should be changed using an {nameof(IDependencyPropertyKey)}");
        }
    }
}
