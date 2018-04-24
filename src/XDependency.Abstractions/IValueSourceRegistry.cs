﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions
{
    public interface IValueSourceRegistry
    {
        void Add<TSource>(Func<IDependencyComponent, int, TSource> factory) where TSource : class, IValueSource;

        IReadOnlyList<IValueSource> GetValueSources(IDependencyComponent component);
    }
}