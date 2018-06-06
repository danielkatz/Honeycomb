using System;

namespace XDependency.Abstractions
{
    public interface IDependencyComponent
    {
        event DependencyPropertyChangedCallback PropertyChanged;

        object GetValue(IDependencyProperty dp);

        void SetValue(IDependencyProperty dp, object value);

        void SetValue(IDependencyPropertyKey key, object value);

        void ClearValue(IDependencyProperty dp);

        void ClearValue(IDependencyPropertyKey key);

        object ReadLocalValue(IDependencyProperty dp);

        object GetAnimationBaseValue(IDependencyProperty dp);

        IPropertyMetadata GetMetadata(IDependencyProperty dp);

        T GetValueSource<T>() where T : IValueSource;
    }
}
