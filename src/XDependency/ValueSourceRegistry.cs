using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public class ValueSourceRegistry : IValueSourceRegistry
    {
        readonly Dictionary<Type, Func<IDependencyObject, IValueSource>> factories
            = new Dictionary<Type, Func<IDependencyObject, IValueSource>>();

        readonly List<Type> order = new List<Type>();

        public void AddFirst<TSource>(Func<IDependencyObject, TSource> factory) where TSource : class, IValueSource
        {
            factories.Add(typeof(TSource), factory);

            order.Insert(0, typeof(TSource));
        }

        public void AddAfter<TSource, TTarget>(Func<IDependencyObject, TSource> factory) where TSource : class, IValueSource
        {
            factories.Add(typeof(TSource), factory);

            var target = GetTargetIndex<TTarget>();
            order.Insert(target + 1, typeof(TSource));
        }

        public void AddBefore<TSource, TTarget>(Func<IDependencyObject, TSource> factory) where TSource : class, IValueSource
        {
            factories.Add(typeof(TSource), factory);

            var target = GetTargetIndex<TTarget>();
            order.Insert(target, typeof(TSource));
        }

        public void AddLast<TSource>(Func<IDependencyObject, TSource> factory) where TSource : class, IValueSource
        {
            factories.Add(typeof(TSource), factory);

            order.Add(typeof(TSource));
        }

        public void Remove<TSource>()
        {
            factories.Remove(typeof(TSource));
            order.Remove(typeof(TSource));
        }

        public void Clear()
        {
            factories.Clear();
            order.Clear();
        }

        int GetTargetIndex<TTarget>()
        {
            var index = order.IndexOf(typeof(TTarget));

            if (index == -1)
                throw new ArgumentException(nameof(TTarget));

            return index;
        }

        public int Count => order.Count;

        public IEnumerator<Type> GetEnumerator() => order.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => order.GetEnumerator();
    }
}
