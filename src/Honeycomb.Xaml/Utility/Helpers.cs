using System;
using System.Collections.Generic;
using System.Text;

namespace Honeycomb.Xaml.Utility
{
    public static class Helpers
    {
        public static bool AreDifferent(object a, object b)
        {
            if ((b is ValueType) || (b is string))
            {
                return !object.Equals(a, b);
            }

            return !object.ReferenceEquals(a, b);
        }
    }
}
