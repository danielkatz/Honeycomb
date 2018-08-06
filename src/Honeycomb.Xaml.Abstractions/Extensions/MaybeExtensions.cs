using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Abstractions.Extensions
{
    public static class MaybeExtensions
    {
        public static U Case<T, U>(this IMaybe<T> maybe, Func<T, U> some, Func<U> none)
        {
            return maybe.HasValue
                ? some(maybe.Value)
                : none();
        }
    }
}
