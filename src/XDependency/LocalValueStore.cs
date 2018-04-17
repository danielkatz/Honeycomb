using System;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class LocalValueStore : IValueSource, IValueStore
    {
        readonly Dictionary<IDependencyProperty, object> store = new Dictionary<IDependencyProperty, object>();

        public bool HasValue(IDependencyProperty dp)
        {
            return store.ContainsKey(dp);
        }

        public bool TryGetValue(IDependencyProperty dp, out object value)
        {
            return store.TryGetValue(dp, out value);
        }

        public void SetValue(IDependencyProperty dp, object value)
        {
            store[dp] = value;
        }

        public void ClearValue(IDependencyProperty dp)
        {
            if (store.ContainsKey(dp))
            {
                store.Remove(dp);
            }
        }

        public int Order => throw new NotImplementedException();
    }
}
