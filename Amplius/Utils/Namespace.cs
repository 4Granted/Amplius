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
    /// <summary>
    /// A namespace represents <see cref="NamespaceKey"/>'s by a global key
    /// </summary>
    public sealed class Namespace
    {
        public static readonly Namespace Amplius = new Namespace("amplius");
        public static readonly Namespace Amplius_Tests = new Namespace("amplius.tests");
        public static readonly Namespace Default = new Namespace("global");

        public string Name { get; }

        public Namespace(string name) => Name = name;

        /// <summary>
        /// Creates a <see cref="NamespaceKey"/> of <paramref name="key"/> under the parent <see cref="Namespace"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public NamespaceKey From(string key) => new NamespaceKey(this, key);
    }
}
