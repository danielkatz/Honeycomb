using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Honeycomb.Xaml.Abstractions;
using Honeycomb.Xaml.Utility;
using Xunit;

namespace Honeycomb.Xaml.Tests
{
    public class OrderedDictionaryTests
    {
        [Fact]
        public void CanAdd()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key1", "value1");
            dict.Add("key2", "value2");

            // as collection
            Assert.Collection(dict,
                first => Assert.Equal("key1", first.Key),
                second => Assert.Equal("key2", second.Key));

            // as dictionarry
            Assert.True(dict.ContainsKey("key1"));
            Assert.True(dict.ContainsKey("key2"));
        }

        [Fact]
        public void CanInsert()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key2", "value2");
            dict.Insert(0, "key1", "value1");
            dict.Insert(dict.Count, "key3", "value3");

            // as collection
            Assert.Collection(dict,
                first => Assert.Equal("key1", first.Key),
                second => Assert.Equal("key2", second.Key),
                third => Assert.Equal("key3", third.Key));

            // as dictionarry
            Assert.True(dict.ContainsKey("key1"));
            Assert.True(dict.ContainsKey("key2"));
            Assert.True(dict.ContainsKey("key3"));
        }

        [Fact]
        public void CanRemove()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key1", "value1");
            dict.Add("key2", "value2");
            dict.Remove("key1");

            // as collection
            Assert.Collection(dict,
                first => Assert.Equal("key2", first.Key));

            // as dictionary
            Assert.False(dict.ContainsKey("key1"));
        }

        [Fact]
        public void CanClear()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key1", "value1");
            dict.Add("key2", "value2");
            dict.Clear();

            // as collection
            Assert.Empty(dict);

            // as dictionary
            Assert.False(dict.ContainsKey("key1"));
            Assert.False(dict.ContainsKey("key2"));
        }

        [Fact]
        public void CanNotAddSameValueSourceTwice()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key1", "value1");

            Assert.Throws<ArgumentException>(() => dict.Add("key1", "value1"));
            Assert.Throws<ArgumentException>(() => dict.Insert(0, "key1", "value1"));

            // as collection
            Assert.Collection(dict,
                first => Assert.Equal("key1", first.Key));

            // as dictionary
            Assert.True(dict.ContainsKey("key1"));
        }

        [Fact]
        public void CanNotInsertToOutOfBoundsIndex()
        {
            var dict = new OrderedDictionary<string, string>();

            Assert.Throws<ArgumentOutOfRangeException>(() => dict.Insert(-1, "key1", "value1"));
            Assert.Throws<ArgumentOutOfRangeException>(() => dict.Insert(1, "key1", "value1"));

            // as collection
            Assert.Empty(dict);

            // as dictionary
            Assert.False(dict.ContainsKey("key1"));
        }

        [Fact]
        public void CanGetKeyValuePairByIndex()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key2", "value2");
            dict.Add("key1", "value1");

            Assert.Equal("key2", dict.ElementAt(0).Key);
            Assert.Equal("key1", dict.ElementAt(1).Key);
        }

        [Fact]
        public void CanGetIndexByKey()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key2", "value2");
            dict.Add("key1", "value1");

            Assert.Equal(0, dict.IndexOf("key2"));
            Assert.Equal(1, dict.IndexOf("key1"));
            Assert.Equal(-1, dict.IndexOf("other"));
        }

        [Fact]
        public void CanGetKeys()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key1", "value1");
            dict.Add("key2", "value2");

            Assert.Collection(dict.Keys,
                first => Assert.Equal("key1", first),
                second => Assert.Equal("key2", second));
        }

        [Fact]
        public void CanGetValues()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key1", "value1");
            dict.Add("key2", "value2");

            Assert.Collection(dict.Values,
                first => Assert.Equal("value1", first),
                second => Assert.Equal("value2", second));
        }

        [Fact]
        public void CanGetValueByKey()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key1", "value1");
            dict.Add("key2", "value2");

            Assert.True(dict.TryGetValue("key1", out var val));
            Assert.Equal("value1", val);

            Assert.False(dict.TryGetValue("other", out _));
        }

        [Fact]
        public void CanGetValueByKeyIndexer()
        {
            var dict = new OrderedDictionary<string, string>();

            dict.Add("key1", "value1");
            dict.Add("key2", "value2");

            Assert.Equal("value1", dict["key1"]);

            Assert.Throws<KeyNotFoundException>(() => dict["other"]);
        }

        [Fact]
        public void CanMakeReadOnly()
        {
            var dict = new OrderedDictionary<string, string>();
            var ro = dict.AsReadOnly();

            dict.Add("key1", "value1");

            Assert.Throws<InvalidOperationException>(() => ro.Add("key2", "value2"));
            Assert.Throws<InvalidOperationException>(() => ro.Insert(0, "key2", "value2"));
            Assert.Throws<InvalidOperationException>(() => ro.Remove("key1"));
            Assert.Throws<InvalidOperationException>(() => ro.Clear());

            // as collections
            Assert.False(dict.IsReadOnly);
            Assert.Collection(dict,
                first => Assert.Equal("key1", first.Key));

            Assert.True(ro.IsReadOnly);
            Assert.Collection(ro,
                first => Assert.Equal("key1", first.Key));

            // as dictionaries
            Assert.True(dict.ContainsKey("key1"));
            Assert.True(ro.ContainsKey("key1"));
        }
    }
}
