using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XDependency.Utility
{
    public class OrderedDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        readonly Dictionary<TKey, TValue> dict;
        readonly List<KeyValuePair<TKey, TValue>> list;
        readonly bool isReadOnly = false;

        public OrderedDictionary()
        {
            dict = new Dictionary<TKey, TValue>();
            list = new List<KeyValuePair<TKey, TValue>>();
        }

        public OrderedDictionary(int capacity)
        {
            dict = new Dictionary<TKey, TValue>(capacity);
            list = new List<KeyValuePair<TKey, TValue>>(capacity);
        }

        private OrderedDictionary(OrderedDictionary<TKey, TValue> source)
        {
            dict = source.dict;
            list = source.list;
            isReadOnly = true;
        }

        public void Add(TKey key, TValue value)
        {
            if (isReadOnly)
                throw new InvalidOperationException("This instance is read-only.");

            dict.Add(key, value);
            list.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Insert(int index, TKey key, TValue value)
        {
            if (isReadOnly)
                throw new InvalidOperationException("This instance is read-only.");

            if (index > Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            dict.Add(key, value);
            list.Insert(index, new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool Remove(TKey key)
        {
            if (isReadOnly)
                throw new InvalidOperationException("This instance is read-only.");

            var listIndex = IndexOf(key);
            var removed = dict.Remove(key);
            if (removed)
            {
                list.RemoveAt(listIndex);
            }
            return removed;
        }

        public void Clear()
        {
            if (isReadOnly)
                throw new InvalidOperationException("This instance is read-only.");

            dict.Clear();
            list.Clear();
        }

        public KeyValuePair<TKey, TValue> ElementAt(int index)
        {
            return list[index];
        }

        public int IndexOf(TKey key)
        {
            if (!dict.ContainsKey(key))
                return -1;

            return list.FindIndex(p => dict.Comparer.Equals(p.Key, key));
        }

        public OrderedDictionary<TKey, TValue> AsReadOnly()
        {
            return new OrderedDictionary<TKey, TValue>(this);
        }

        public bool TryGetValue(TKey key, out TValue value) => dict.TryGetValue(key, out value);

        public bool ContainsKey(TKey key) => dict.ContainsKey(key);

        public TValue this[TKey key] => dict[key];

        public IEnumerable<TKey> Keys => list.Select(x => x.Key);

        public IEnumerable<TValue> Values => list.Select(x => x.Value);

        public int Count => dict.Count;

        public bool IsReadOnly => isReadOnly;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
    }
}
