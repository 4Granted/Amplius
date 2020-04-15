using System.Collections;
using System.Collections.Generic;

namespace Amplius.Registry
{
    public abstract class Registry<K, V> : IEnumerable<KeyValuePair<K, V>>
    {
        public IEnumerable<K> Keys => entries.Keys as ICollection<K>;
        public IEnumerable<V> Values => entries.Values as ICollection<V>;
        public IEnumerable<KeyValuePair<K, V>> Pairs => entries as ICollection<KeyValuePair<K, V>>;

        protected readonly Dictionary<K, V> entries = new Dictionary<K, V>();

        public virtual bool Add(K key, V value)
        {
            if (entries.ContainsKey(key))
                return false;
            entries.Add(key, value);
            return true;
        }
        public virtual IEnumerable<bool> Add(params (K, V)[] pairs)
        {
            foreach (var pair in pairs)
                yield return Add(pair.Item1, pair.Item2);
        }
        public virtual V Get(K key) => entries[key];
        public virtual V this[K key]
        {
            get => entries[key];
            set => entries[key] = value;
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator() => Pairs.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
