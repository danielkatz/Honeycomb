﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeycomb.Xaml.Abstractions;

namespace Honeycomb.Xaml.Tests.Fakes
{
    public class ValueStoreFake : ValueStoreBase
    {
        public ValueStoreFake(IDependencyComponent component, int order) : base(component, order)
        {
        }
    }
}
