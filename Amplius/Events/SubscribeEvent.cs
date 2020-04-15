using System;

namespace Amplius.Events
{
    /// <summary>
    /// Subscribes to the specificied type in the current listener.
    /// <para>Provides an <see cref="EventBus"/> with the binded event type.</para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class SubscribeAttribute : Attribute 
    {
        public Type Type => type;

        private Type type;

        public SubscribeAttribute(Type type) => this.type = type;
    }
}
