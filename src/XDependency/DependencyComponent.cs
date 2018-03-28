using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class DependencyComponent : IDependencyComponent
    {
        IDependencyObject obj;

        public DependencyComponent(IDependencyObject obj)
        {
            this.obj = obj;
        }

        public object GetValue(IDependencyProperty dp)
        {
            throw new NotImplementedException();
        }

        public void SetValue(IDependencyProperty dp, object value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(IDependencyPropertyKey key, object value)
        {
            throw new NotImplementedException();
        }

        public void ClearValue(IDependencyProperty dp)
        {
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
    }
}
