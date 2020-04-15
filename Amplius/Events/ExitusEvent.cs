using System;
using System.Reflection;

namespace Amplius.Events
{
    /// <summary>
    /// An internal wrapper for the provided listener and function.
    /// </summary>
    internal sealed class ExitusEvent
    {
        private readonly MethodInfo function;
        private readonly Type listener;

        internal ExitusEvent(MethodInfo function, Type listener)
        {
            this.function = function;
            this.listener = listener;
        }

        /// <summary>
        /// Invokes a listener with an event (<paramref name="e"/>) of type <typeparamref name="T"/> from an <see cref="EventBus"/> (<paramref name="bus"/>) with the provided <paramref name="parameters"/>.
        /// </summary>
        /// <typeparam name="T">The event type</typeparam>
        /// <param name="e">The provided event</param>
        /// <param name="bus">The provided EventBus</param>
        /// <param name="parameters">The provided parameters</param>
        internal object Invoke<T>(T e, EventBus bus, params object[] parameters) where T : class
        {
            var functionParameters = function.GetParameters();
            ParameterInfo parameter = functionParameters.Length > 0 ? functionParameters[0] : null;

            var args = new object[functionParameters.Length];

            if (parameter?.ParameterType == e.GetType())
                args[0] = e;

            if (functionParameters.Length >= 2)
                for (int i = 1; i < functionParameters.Length; i++)
                {
                    args[i] = parameters?[i - 1] ?? default;
                }

            return function.Invoke(bus.Instances[listener], args);
        }
    }
}
