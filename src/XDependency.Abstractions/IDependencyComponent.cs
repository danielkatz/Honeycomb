using System;

namespace XDependency.Abstractions
{
    public interface IDependencyComponent
    {
        object GetValue(IDependencyProperty dp);

        void SetValue(IDependencyProperty dp, object value);

        void SetValue(IDependencyPropertyKey key, object value);

        void ClearValue(IDependencyProperty dp);

        void ClearValue(IDependencyPropertyKey key);

        object ReadLocalValue(IDependencyProperty dp);

        object GetAnimationBaseValue(IDependencyProperty dp);

        long RegisterPropertyChangedCallback(IDependencyProperty dp, DependencyPropertyChangedCallback callback);

        void UnregisterPropertyChangedCallback(IDependencyProperty dp, long token);
    }
}
