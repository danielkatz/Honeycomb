﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XDependency.Abstractions.Extensions
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

        public static void ClearValue(this IDependencyObject obj, IDependencyProperty dp)
        {
            obj.ClearValue(dp);
        }

        public static object ReadLocalValue(this IDependencyObject obj, IDependencyProperty dp)
        {
            return obj.Component.ReadLocalValue(dp);
        }

        public static object GetAnimationBaseValue(this IDependencyObject obj, IDependencyProperty dp)
        {
            return obj.Component.GetAnimationBaseValue(dp);
        }

        public static long RegisterPropertyChangedCallback(this IDependencyObject obj, IDependencyProperty dp, DependencyPropertyChangedCallback callback)
        {
            return obj.Component.RegisterPropertyChangedCallback(dp, callback);
        }

        public static void UnregisterPropertyChangedCallback(this IDependencyObject obj, IDependencyProperty dp, long token)
        {
            obj.UnregisterPropertyChangedCallback(dp, token);
        }
    }
}
