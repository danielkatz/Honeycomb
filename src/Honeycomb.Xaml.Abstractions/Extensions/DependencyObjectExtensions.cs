using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Abstractions.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static object GetValue(this IDependencyObject obj, IDependencyProperty dp)
        {
            return obj.Component.GetValue(dp);
        }

        public static void SetValue(this IDependencyObject obj, IDependencyProperty dp, object value)
        {
            obj.Component.SetValue(dp, value);
        }

        public static void SetValue(this IDependencyObject obj, IDependencyPropertyKey key, object value)
        {
            obj.Component.SetValue(key, value);
        }

        public static void ClearValue(this IDependencyObject obj, IDependencyProperty dp)
        {
            obj.Component.ClearValue(dp);
        }

        public static void ClearValue(this IDependencyObject obj, IDependencyPropertyKey key)
        {
            obj.Component.ClearValue(key);
        }

        public static object ReadLocalValue(this IDependencyObject obj, IDependencyProperty dp)
        {
            return obj.Component.ReadLocalValue(dp);
        }

        public static object GetAnimationBaseValue(this IDependencyObject obj, IDependencyProperty dp)
        {
            return obj.Component.GetAnimationBaseValue(dp);
        }
    }
}
