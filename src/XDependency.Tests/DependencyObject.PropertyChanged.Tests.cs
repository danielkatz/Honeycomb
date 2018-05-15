using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XDependency.Abstractions;
using XDependency.Abstractions.Extensions;
using XDependency.Tests.Fakes;
using XDependency.Tests.Fixtures;
using Xunit;

namespace XDependency.Tests
{
    public partial class DependencyObjectTests
    {
        private class TestCase
        {
            public int Case { get; set; }
            public string HIGH { get; set; }
            public string LOW { get; set; }
            public string DEFAULT { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }
            public string Raise { get; set; }
        }

        [Fact]
        public void PropertyChangedEventTestCases()
        {
            // https://docs.google.com/spreadsheets/d/1ubr8rybfcFehGWplfxLCxmEHj0n3dDhRVjOBtvGK-GI

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "XDependency.Tests.Data.PropertyChangedEventTestCases.csv";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                var csv = new CsvReader(reader);
                var cases = csv.GetRecords<TestCase>();

                foreach (var test in cases)
                {
                    var expected = Convert.ToBoolean(test.Raise) ? 1 : 0;

                    using (new DefaultImplementationFixture())
                    {
                        var raised = 0;

                        Action<IDependencyObject, DependencyPropertyChangedEventArgs> handler = null;

                        var owner = new DependencyObjectFake();
                        var high = owner.Component.GetValueStore<ValueStoreFake>();
                        var low = owner.Component.GetValueStore<LocalValueStore>();

                        var prop = Dependency.Property.Register("Value", typeof(string), typeof(DependencyObjectFake),
                            new PropertyMetadata(test.DEFAULT, (d, e) => handler?.Invoke(d, e)));

                        IValueStore changingStore;
                        IValueStore constantStore;
                        string changeOperation;
                        string constValue;

                        if (test.HIGH.Contains("->"))
                        {
                            changingStore = high;
                            changeOperation = test.HIGH;
                            constantStore = low;
                            constValue = test.LOW;
                        }
                        else
                        {
                            changingStore = low;
                            changeOperation = test.LOW;
                            constantStore = high;
                            constValue = test.HIGH;
                        }

                        var beforeAndAfter = changeOperation.Split("->").Select(x => x.Trim()).ToArray();

                        if (!string.IsNullOrEmpty(constValue))
                        {
                            constantStore.SetValue(prop, constValue);
                        }

                        if (!string.IsNullOrEmpty(beforeAndAfter[0]))
                        {
                            changingStore.SetValue(prop, beforeAndAfter[0]);
                        }

                        Assert.Equal(test.OldValue, owner.GetValue(prop));

                        handler = (d, e) =>
                        {
                            raised++;

                            Assert.Equal(test.OldValue, e.OldValue);
                            Assert.Equal(test.NewValue, e.NewValue);
                        };

                        if (string.IsNullOrEmpty(beforeAndAfter[1]))
                        {
                            changingStore.ClearValue(prop);
                        }
                        else
                        {
                            changingStore.SetValue(prop, beforeAndAfter[1]);
                        }

                        Assert.Equal(test.NewValue, owner.GetValue(prop));
                        Assert.Equal(expected, raised);
                    }
                }
            }
        }
    }
}
