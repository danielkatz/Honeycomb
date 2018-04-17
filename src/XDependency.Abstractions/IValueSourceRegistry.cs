using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public interface IValueSourceRegistry : IReadOnlyCollection<Type>
    {
        void AddFirst<TSource>(Func<IDependencyObject, TSource> factory) where TSource : class, IValueSource;
        void AddBefore<TSource, TTarget>(Func<IDependencyObject, TSource> factory) where TSource : class, IValueSource;
        void AddAfter<TSource, TTarget>(Func<IDependencyObject, TSource> factory) where TSource : class, IValueSource;
        void AddLast<TSource>(Func<IDependencyObject, TSource> factory) where TSource : class, IValueSource;

        void Remove<TSource>();
        void Clear();
    }
}