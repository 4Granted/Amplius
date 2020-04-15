using System;
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

namespace Amplius
{
    /// <summary>
    /// A static class housing extensions for functional programming ease.
    /// 
    /// <para>Most extension functions here are based off of <c>Kotlin</c>.</para>
    /// </summary>
    public static class FunctionalExtensions
    {
        /// <summary>
        /// Calls the specified <see cref="Func{T, TResult}"/> with <c>self</c> value as its argument and returns its result.
        /// </summary>
        /// <typeparam name="T">The type being operated on</typeparam>
        /// <typeparam name="R">The type to return</typeparam>
        /// <param name="self">The type to operate on</param>
        /// <param name="callback">The callback <c>Func</c> to invoke</param>
        /// <returns>Returns the result of the <c>Func</c></returns>
        public static R Let<T, R>(this T self, Func<T, R> callback) => callback.Invoke(self);

        /// <summary>
        /// A enumeralized version of the <c>Let&lt;<typeparamref name="T"/>, <typeparamref name="R"/>&gt;</c> function.
        /// <para> Calls the specified <see cref="Func{T, TResult}"/> with <c>self</c> value as its argument and returns its result.</para>
        /// </summary>
        /// <typeparam name="T">The IEnumerable&lt;<typeparamref name="T"/>&gt; being operated on</typeparam>
        /// <typeparam name="R">The enumeralized results to return</typeparam>
        /// <param name="self">The IEnumerable&lt;<typeparamref name="T"/>&gt; to operate on</param>
        /// <param name="callback">The callback <c>Func</c> to invoke on each item</param>
        /// <returns>Returns the results of the <c>Func</c> on a IEnumerable&lt;<typeparamref name="R"/>&gt;</returns>
        public static IEnumerable<R> Let<T, R>(this IEnumerable<T> self, Func<T, R> callback)
        {
            foreach (var item in self)
                yield return callback.Invoke(item);
        }

        /// <summary>
        /// Calls the specified <see cref="Action{T}"/> with <c>self</c> value as its receiver and returns <c>self</c> value.
        /// Identitical to <c>Also&lt;<typeparamref name="T"/>&gt;</c>; used for code readability.
        /// </summary>
        /// <typeparam name="T">The type being operated on</typeparam>
        /// <typeparam name="R">he type to return</typeparam>
        /// <param name="_">The type to operate on; not used.</param>
        /// <param name="callback">The callback <c>Func</c> to invoke</param>
        /// <returns>Returns the result of the <c>Func</c></returns>
        public static T Apply<T>(this T self, Action<T> callback)
        {
            callback.Invoke(self);
            return self;
        }

        /// <summary>
        /// Calls the specified <see cref="Action{T}"/> with <c>self</c> value as its argument and returns <c>self</c> value.
        /// </summary>
        /// <typeparam name="T">The type being operated on</typeparam>
        /// <typeparam name="R">he type to return</typeparam>
        /// <param name="_">The type to operate on; not used.</param>
        /// <param name="callback">The callback <c>Func</c> to invoke</param>
        /// <returns>Returns the result of the <c>Func</c></returns>
        public static T Also<T>(this T self, Action<T> callback)
        {
            callback.Invoke(self);
            return self;
        }

        /// <summary>
        /// Calls the specified <see cref="Func{TResult}"/> with <c>self</c> value as its receiver and returns its result.
        /// </summary>
        /// <typeparam name="T">The type being operated on</typeparam>
        /// <typeparam name="R">he type to return</typeparam>
        /// <param name="_">The type to operate on; not used.</param>
        /// <param name="callback">The callback <c>Func</c> to invoke</param>
        /// <returns>Returns the result of the <c>Func</c></returns>
        public static R Run<T, R>(this T _, Func<R> callback) => callback.Invoke();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="expected"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static T Do<T>(this T self, T expected, Action<T> callback) => self.Also(_ => { if (self.Equals(expected)) callback.Invoke(self); });

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="condition"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static T DoIf<T>(this T self, Func<T, bool> condition, Action<T> callback) => self.Also(_ => { if (condition.Invoke(self)) callback.Invoke(self); });
    }
}
