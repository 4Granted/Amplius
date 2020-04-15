using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Amplius.Events
{
    /// <summary>
    /// Provides a wrapper for subscribed methods and listeners.
    /// </summary>
    public class EventBus
    {
        public static readonly EventBus Default = new EventBus();

        internal Dictionary<Type, object> Instances => instances;

        private readonly List<Type> listeners;
        private readonly Dictionary<Type, object> instances;
        private readonly Dictionary<Type, List<ExitusEvent>> events;

        public EventBus()
        {
            listeners = new List<Type>();
            instances = new Dictionary<Type, object>();
            events = new Dictionary<Type, List<ExitusEvent>>();
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
                var e = new ExitusEvent(method, listener.GetType());

                var type = method.GetCustomAttribute<SubscribeAttribute>().Type;

                if (!events.ContainsKey(type))
                    events[type] = new List<ExitusEvent>();

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

            /*events[te]?.Also(list =>
            {
                list.ForEach(it => it.Invoke(e, this, parameters));

                foreach (var super in te.GetBases())
                {
                    if (!events.ContainsKey(super))
                        events[super]?.ForEach(it => it.Invoke(e, this));
                }
            });*/
        }
    }
}
