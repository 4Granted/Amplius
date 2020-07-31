
using Amplius.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
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
namespace Amplius.Utils
{
#nullable enable
    /// <summary>
    /// Represents a key owned by a parent <see cref="Namespace"/> defined under a unique global id
    /// </summary>s
    public sealed class NamespaceKey : ILocalizable
    {
        /// <summary>
        /// Merges the <see cref="Namespace"/> id and the <see cref="NamespaceKey"/> key into a string of the format <c>'id:key'</c>
        /// </summary>
        public string FullKey => $"{Parent.Name}:{Key}";
        public string DeserializationKey => $"{Parent.Name}.{Key}";
        /// <summary>
        /// Parent <see cref="Namespace"/>
        /// </summary>
        public Namespace Parent { get; }
        /// <summary>
        /// <see cref="Namespace"/> local key
        /// </summary>
        public string Key { get; }
        /// <summary>
        /// A truly secure key for namespaces; merges <see cref="DeserializationKey"/> and a generated <see cref="Guid"/>
        /// </summary>
        public string SecureKey { get; }

        public NamespaceKey(Namespace parent, string key)
        {
            Parent = parent;

            if (key.Contains('.') || key.Contains(':'))
                throw new NamespaceKeyCreationException($"A NamespaceKey key cannot contain '.' or ':'.", key);

            Key = key;
            SecureKey = $"{DeserializationKey}.{Guid.NewGuid()}";
        }

        /// <summary>
        /// Localizes the internal <see cref="FullKey"/> to its' <see cref="Languages"/> code variant
        /// </summary>
        /// <param name="code">Language to localize to</param>
        /// <returns>Returns a localized string</returns>
        public string? Localize(Languages? code = null) => Language.Localize(this, code);

        public bool Equals(object? obj, bool secure = false) => obj is NamespaceKey nk && nk.FullKey == FullKey && (secure ? nk.SecureKey == SecureKey : true);
        public override bool Equals(object? obj) => obj is NamespaceKey nk && nk.FullKey == FullKey;
        public override int GetHashCode() => FullKey.GetHashCode();
        public override string ToString() => FullKey;

        /// <summary>
        /// Creates a <see cref="NamespaceKey"/> from a string
        /// 
        /// <para>If the string contains a char of type <paramref name="splitter"/>, it automatically creates a <see cref="Namespace"/></para>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="splitter"></param>
        /// <returns></returns>
        public static NamespaceKey? From(string? key, char splitter = ':')
        {
            if (string.IsNullOrEmpty(key))
                return null;

            if (key.Contains(splitter))
            {
                var split = key.Split(splitter);
                var builder = new StringBuilder();

                builder.Append(split[0]);

                for (int i = 1; i < split.Length - 1; i++)
                {
                    builder.Append('.');
                    builder.Append(split[i]);
                }

                /*if (split.Length > 2)
                    throw new NamespaceKeyCreationException($"Key contained more than one splittable character of type '{splitter}'", key);*/

                return new NamespaceKey(new Namespace(builder.ToString()), split[split.Length - 1]);
            }

            return Namespace.Default.From(key);
        }
        public static implicit operator NamespaceKey?([NotNull] string key) => From(key);
    }

    public sealed class NamespaceKeyCreationException : Exception
    {
        public string Evidence => evidence;

        private readonly string evidence;

        public NamespaceKeyCreationException(string message, string evidence) : base(message) => this.evidence = evidence;
    }
}
