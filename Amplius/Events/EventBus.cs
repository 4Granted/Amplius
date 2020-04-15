using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

namespace Amplius.Events
{
    /// <summary>
    /// Provides a wrapper for subscribed methods and listeners.
    /// </summary>
    public class EventBus
    {
        public static readonly EventBus DEFAULT = new EventBus();

        internal Dictionary<Type, object> Instances => instances;

        private readonly List<Type> listeners;
        private readonly Dictionary<Type, object> instances;
        private readonly Dictionary<Type, List<EventWrapper>> events;

        public EventBus()
        {
            listeners = new List<Type>();
            instances = new Dictionary<Type, object>();
            events = new Dictionary<Type, List<EventWrapper>>();
        }

        /// <summary>
        /// Subscribes a listeners to the <see cref="EventBus"/>.
        /// </summary>
        /// <typeparam name="T">Listener type</typeparam>
        /// <param name="listener">Provided listener</param>
        public void Subscribe<T>(T listener) where T : class
        {
            var methods = listener.GetType().GetMethods().Where(m => m.GetCustomAttribute<SubscribeAttribute>() != null);

            foreach (var method in methods)
            {
                var e = new EventWrapper(method, listener.GetType());

                var type = method.GetCustomAttribute<SubscribeAttribute>().Type;

                if (!events.ContainsKey(type))
                    events[type] = new List<EventWrapper>();

                events[type]?.Add(e);
            }

            listeners.Add(listener.GetType());
            instances.Add(listener.GetType(), listener);
        }

        /// <summary>
        /// Unsubscribes a listener from the <see cref="EventBus"/>.
        /// </summary>
        /// <param name="listener">Provided listener type</param>
        public void Unsubscribe(Type listener) => listeners.Remove(listener);

        /// <summary>
        /// Invokes any subscribers of the type <typeparamref name="T"/> with the event <paramref name="e"/> and given <paramref name="parameters"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <param name="parameters"></param>
        public void Invoke<T>(T e, params object[] parameters) where T : class
        {
            Type te = e.GetType();

            if (!events.ContainsKey(te))
                return;

            if (events[te] == null)
                throw new Exception($"No subscribed events were found for {te.Name}.");

            foreach (var item in events[te])
                item.Invoke(e, this, parameters);
        }
    }
}
