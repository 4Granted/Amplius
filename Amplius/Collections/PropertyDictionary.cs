using Amplius.Utils.Properties;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Amplius.Collections
{
    public class PropertyDictionary<K> : IPropertyList, IPagedList<KeyValuePair<K, IProperty>>, IDictionary<K, IProperty> where K : notnull
    {
        public IProperty this[K key] { get => properties[key]; set => properties[key] = value; }
        public ICollection<K> Keys => properties.Keys;
        public ICollection<IProperty> Values => properties.Values;
        public int Count => properties.Count;
        public bool IsReadOnly => false;

        protected readonly Dictionary<K, IProperty> properties;

        public PropertyDictionary() : this(new Dictionary<K, IProperty>()) { }
        public PropertyDictionary(Dictionary<K, IProperty> properties) => this.properties = properties;

        public virtual void ResetDefaults()
        {
            foreach (var property in properties.Values)
                property.Value = property.DefaultValue;
        }
        public IEnumerable<KeyValuePair<K, IProperty>> GetPage(int page, int pageSize = 10) => properties.Skip(page * pageSize).Take(pageSize);
        IEnumerable IPagedList.GetPage(int page, int pageSize) => GetPage(page, pageSize);
        public int GetTotalPages(int pageSize = 10) => properties.Count / pageSize;
        public void Add(K key, IProperty value) => properties.Add(key, value);
        public void Add(KeyValuePair<K, IProperty> item) => properties.Add(item.Key, item.Value);
        public void Clear() => properties.Clear();
        public bool Contains(KeyValuePair<K, IProperty> item) => properties.Contains(item);
        public bool ContainsKey(K key) => properties.ContainsKey(key);
        public void CopyTo(KeyValuePair<K, IProperty>[] array, int arrayIndex) { }
        public bool Remove(K key) => properties.Remove(key);
        public bool Remove(KeyValuePair<K, IProperty> item) => properties.Remove(item.Key);
        public bool TryGetValue(K key, [MaybeNullWhen(false)] out IProperty value) => properties.TryGetValue(key, out value);

        public IEnumerator<IProperty> GetEnumerator() => Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IEnumerator<KeyValuePair<K, IProperty>> IEnumerable<KeyValuePair<K, IProperty>>.GetEnumerator() => properties.GetEnumerator();
        IEnumerator<IProperty> IEnumerable<IProperty>.GetEnumerator() => GetEnumerator();
    }

    public class PropertyDictionary<K, V> : IPropertyList<V>, IPagedList<KeyValuePair<K, IProperty<V>>>, IDictionary<K, IProperty<V>> where K : notnull
    {
        public IProperty<V> this[K key] { get => properties[key]; set => properties[key] = value; }
        public ICollection<K> Keys => properties.Keys;
        public ICollection<IProperty<V>> Values => properties.Values;
        public int Count => properties.Count;
        public bool IsReadOnly => false;

        protected readonly Dictionary<K, IProperty<V>> properties;

        public PropertyDictionary() : this(new Dictionary<K, IProperty<V>>()) { }
        public PropertyDictionary(Dictionary<K, IProperty<V>> properties) => this.properties = properties;

        public virtual void ResetDefaults()
        {
            foreach (var property in properties.Values)
                property.Value = property.DefaultValue;
        }
        public IEnumerable<KeyValuePair<K, IProperty<V>>> GetPage(int page, int pageSize = 10) => properties.Skip(page * pageSize).Take(pageSize);
        IEnumerable IPagedList.GetPage(int page, int pageSize) => GetPage(page, pageSize);
        public int GetTotalPages(int pageSize = 10) => properties.Count / pageSize;
        public void Add(K key, IProperty<V> value) => properties.Add(key, value);
        public void Add(KeyValuePair<K, IProperty<V>> item) => properties.Add(item.Key, item.Value);
        public void Clear() => properties.Clear();
        public bool Contains(KeyValuePair<K, IProperty<V>> item) => properties.Contains(item);
        public bool ContainsKey(K key) => properties.ContainsKey(key);
        public void CopyTo(KeyValuePair<K, IProperty<V>>[] array, int arrayIndex) { }
        public bool Remove(K key) => properties.Remove(key);
        public bool Remove(KeyValuePair<K, IProperty<V>> item) => properties.Remove(item.Key);
        public bool TryGetValue(K key, [MaybeNullWhen(false)] out IProperty<V> value) => properties.TryGetValue(key, out value);

        public IEnumerator<IProperty<V>> GetEnumerator() => Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IEnumerator<KeyValuePair<K, IProperty<V>>> IEnumerable<KeyValuePair<K, IProperty<V>>>.GetEnumerator() => properties.GetEnumerator();
        IEnumerator<IProperty> IEnumerable<IProperty>.GetEnumerator() => GetEnumerator();
    }
}
