using System.Collections;
using System.Collections.Generic;

/// <license>
/// MIT License
/// 
/// Copyright(c) 2020 RuthlessBoi
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
/// </license>

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
