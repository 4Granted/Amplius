using Amplius.Utils.Properties;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Amplius.Collections
{
    public class PropertyList : IPropertyList, IPagedList<IProperty>, IList<IProperty>
    {
        public IProperty this[int index] { get => properties[index]; set => properties[index] = value; }
        public int Count => properties.Count;
        public bool IsReadOnly => false;

        protected readonly List<IProperty> properties;

        public PropertyList() : this(new List<IProperty>()) { }
        public PropertyList(List<IProperty> properties) => this.properties = properties;

        public virtual void ResetDefaults()
        {
            foreach (var property in properties)
                property.Value = property.DefaultValue;
        }
        public virtual IEnumerable<IProperty> GetPage(int page, int pageSize = 10) => properties.Skip(page * pageSize).Take(pageSize);
        IEnumerable IPagedList.GetPage(int page, int pageSize) => GetPage(page, pageSize);
        public int GetTotalPages(int pageSize = 10) => properties.Count / pageSize;
        public virtual void Add(IProperty item) => properties.Add(item);
        public virtual void Insert(int index, IProperty item) => properties.Insert(index, item);
        public virtual bool Remove(IProperty item) => properties.Remove(item);
        public virtual void RemoveAt(int index) => properties.RemoveAt(index);
        public virtual void Clear() => properties.Clear();
        public virtual int IndexOf(IProperty item) => properties.IndexOf(item);
        public virtual bool Contains(IProperty item) => properties.Contains(item);
        public virtual void CopyTo(IProperty[] array, int arrayIndex) => properties.CopyTo(array, arrayIndex);

        public IEnumerator<IProperty> GetEnumerator() => properties.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IEnumerator<IProperty> IEnumerable<IProperty>.GetEnumerator() => GetEnumerator();
    }

    public class PropertyList<T> : IPropertyList<T>, IPagedList<IProperty<T>>, IList<IProperty<T>>
    {
        public IProperty<T> this[int index] { get => properties[index]; set => properties[index] = value; }
        public int Count => properties.Count;
        public bool IsReadOnly => false;

        protected readonly List<IProperty<T>> properties;
        protected readonly int pageSize;

        public PropertyList() : this(new List<IProperty<T>>()) { }
        public PropertyList(List<IProperty<T>> properties) => this.properties = properties;

        public virtual void ResetDefaults()
        {
            foreach (var property in properties)
                property.Value = property.DefaultValue;
        }
        public virtual IEnumerable<IProperty<T>> GetPage(int page, int pageSize = 10) => properties.Skip(page * pageSize).Take(pageSize);
        IEnumerable IPagedList.GetPage(int page, int pageSize) => GetPage(page, pageSize);
        public int GetTotalPages(int pageSize = 10) => properties.Count / pageSize;
        public virtual void Add(IProperty<T> item) => properties.Add(item);
        public virtual void Insert(int index, IProperty<T> item) => properties.Insert(index, item);
        public virtual bool Remove(IProperty<T> item) => properties.Remove(item);
        public virtual void RemoveAt(int index) => properties.RemoveAt(index);
        public virtual void Clear() => properties.Clear();
        public virtual int IndexOf(IProperty<T> item) => properties.IndexOf(item);
        public virtual bool Contains(IProperty<T> item) => properties.Contains(item);
        public virtual void CopyTo(IProperty<T>[] array, int arrayIndex) => properties.CopyTo(array, arrayIndex);

        public IEnumerator<IProperty<T>> GetEnumerator() => properties.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IEnumerator<IProperty> IEnumerable<IProperty>.GetEnumerator() => GetEnumerator();
    }
}
