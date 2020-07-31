using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
namespace Amplius.Collections
{
    public class PagedList<T> : IPagedList<T>, IList<T>, ICollection<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, IEnumerable, ICollection, IList
    {
        public int Count => items.Count;
        public bool IsReadOnly => false;
        public bool IsSynchronized => false;
        public object SyncRoot => false;
        public bool IsFixedSize => false;

        object IList.this[int index] 
        { 
            get => items[index]; 
            set
            {
                if (value is T t)
                    items[index] = t;
            }
        }
        public T this[int index] { get => items[index]; set => items[index] = value; }

        private readonly List<T> items;

        public PagedList() : this(new List<T>()) { }
        public PagedList(List<T> items) => this.items = items;

        public IEnumerable<T> GetPage(int page, int pageSize = 10) => items.Skip(page * pageSize).Take(pageSize);
        IEnumerable IPagedList.GetPage(int page, int pageSize) => GetPage(page, pageSize);
        public int GetTotalPages(int pageSize = 10) => items.Count / pageSize;
        public void Add(T item) => items.Add(item);
        public void Insert(int index, T item) => items.Insert(index, item);
        public bool Remove(T item) => items.Remove(item);
        public void RemoveAt(int index) => items.RemoveAt(index);
        public void Clear() => items.Clear();
        public int IndexOf(T item) => items.IndexOf(item);
        public bool Contains(T item) => items.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);
        public void CopyTo(Array array, int index)
        {
            if (index < -1 || index > items.Count)
                return;

            var count = index;
            var max = array.Length;

            foreach (var obj in array)
            {
                if (obj != null && obj is T t)
                {
                    if (count >= max)
                        return;

                    items[count] = t;
                    count++;
                }
            }
        }
        public int Add(object value)
        {
            if (value is T t)
            {
                items.Add(t);
                return 1;
            }

            return 0;
        }
        public bool Contains(object value)
        {
            if (value != null && value is T t)
                return items.Contains(t);

            return false;
        }
        public int IndexOf(object value)
        {
            if (value != null && value is T t)
                return items.IndexOf(t);

            return -1;
        }
        public void Insert(int index, object value)
        {
            if (index > -1 && value is T t)
                items.Insert(index, t);
        }
        public void Remove(object value)
        {
            if (value != null && value is T t)
                items.Remove(t);
        }

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
