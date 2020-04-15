using System;
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
    /// An internal wrapper for the provided listener and function.
    /// </summary>
    internal sealed class EventWrapper
    {
        private readonly MethodInfo method;
        private readonly Type listener;

        internal EventWrapper(MethodInfo method, Type listener)
        {
            this.method = method;
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
            var functionParameters = method.GetParameters();
            ParameterInfo parameter = functionParameters.Length > 0 ? functionParameters[0] : null;

            var args = new object[functionParameters.Length];

            if (parameter?.ParameterType == e.GetType())
                args[0] = e;

            if (functionParameters.Length >= 2)
                for (int i = 1; i < functionParameters.Length; i++)
                {
                    args[i] = parameters?[i - 1] ?? default;
                }

            return method.Invoke(bus.Instances[listener], args);
        }
    }
}
