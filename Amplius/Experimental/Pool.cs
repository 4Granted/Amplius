using System;
using System.Collections.Generic;

namespace Amplius.Experimental
{
    [Experimental]
    public sealed class Pool<T> where T : class
    {
        public int PooledCount => pooled.Count;
        public bool IsEmpty => PooledCount == 0;

        private readonly List<T> pooled;
        private readonly List<T> used;
        private readonly Func<T> factory;

        public Pool(Func<T> factory = null)
        {
            pooled = new List<T>();
            used = new List<T>();
            this.factory = factory ?? DefaultFactory;
        }

        public T Reinstate(bool simulate = false)
        {
            if (!IsEmpty) return Depool();

            var pooledObj = factory();
            pooled.Add(pooledObj);

            return simulate ? Depool(pooledObj) : null;
        }

        public void Retire(T obj)
        {
            if (!used.Contains(obj)) throw new Exception("Cannot retire an object to a pool if not currently used.");

            pooled.Add(obj);
            used.Remove(obj);
        }

        private T Depool(T obj = null)
        {
            var index = PooledCount - 1;
            var pooledObj = obj ?? pooled[index];

            pooled.RemoveAt(index);
            used.Add(obj);

            return pooledObj;
        }

        private T DefaultFactory() => default(T);
    }
}
