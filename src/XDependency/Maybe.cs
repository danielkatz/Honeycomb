using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using XDependency.Abstractions;

namespace XDependency
{
    public struct Maybe<T> : IMaybe<T>
    {
        readonly bool hasValue;
        readonly T value;

        Maybe(T value)
        {
            this.hasValue = true;
            this.value = value;
        }

        public override int GetHashCode()
        {
            return (value != null)
                ? value.GetHashCode()
                : hasValue.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Maybe<T> maybe)
            {
                if (!maybe.hasValue && !this.hasValue)
                {
                    return true;
                }
                else if (maybe.hasValue != this.hasValue)
                {
                    return false;
                }
                else
                {
                    return object.Equals(maybe.value, this.value);
                }
            }
            else if (obj is T value)
            {
                if (!this.hasValue)
                {
                    return false;
                }
                else
                {
                    return object.Equals(value, this.value);
                }
            }
            return false;
        }

        public override string ToString()
        {
            return hasValue
                ? $"{nameof(Value)} = {(Value == null ? (object)"null" : Value)}"
                : $"{nameof(HasValue)} = {HasValue}";
        }

        public bool HasValue => hasValue;

        public T Value
        {
            get
            {
                if (!hasValue)
                    throw new InvalidOperationException($"{nameof(hasValue)} is false.");

                return value;
            }
        }

        public static Maybe<T> FromValue(T value)
        {
            return new Maybe<T>(value);
        }

        public static Maybe<T> None => new Maybe<T>();

        public static implicit operator Maybe<T>(T value)
        {
            return Maybe<T>.FromValue(value);
        }
    }

    public static class Maybe
    {
        public static Maybe<T> FromValue<T>(T value)
        {
            return Maybe<T>.FromValue(value);
        }

        public static Maybe<T> None<T>()
        {
            return Maybe<T>.None;
        }

        public static Maybe<TValue> MaybeGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dict, TKey key)
        {
            if (dict.TryGetValue(key, out var value))
            {
                return Maybe<TValue>.FromValue(value);
            }
            return Maybe<TValue>.None;
        }
    }
}