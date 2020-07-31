using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Immutable;

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

namespace Amplius.Extensibility
{
    /// <summary>
    /// Loads DLL's from a specified path and scans for <see cref="IExtension{I, C}"/>'s in the assembly.
    /// </summary>
    /// <typeparam name="T">Base extension type</typeparam>
    /// <typeparam name="I">Extension info type</typeparam>
    /// <typeparam name="C">Plugin context type</typeparam>
    public class ExtensionLoader<T, I, C> where T : class, IExtension<I, C> where I : IExtensionInfo
    {
        public readonly Type ExtensionType = typeof(T);
        private readonly string Prefix = $"ExtensionLoader<{typeof(T).Name}>";

        public ImmutableArray<T> Extensions => extensions.ToImmutable();

        private ImmutableArray<T>.Builder extensions;

        public ExtensionLoader() => extensions = ImmutableArray.CreateBuilder<T>();

        public ImmutableArray<T> LoadFrom(string path)
        {
            if (!Directory.Exists(path))
                throw new Exception($"{Prefix} error: Non existent directory '{path}'.");

            var extensionNames = Directory.GetFiles(path, "*.dll");

            if (extensionNames?.Length <= 0)
                 throw new Exception($"{Prefix} error: No DLL's found the '{path}' directory.");

            foreach (var name in extensionNames)
                Assembly.LoadFile(Path.GetFullPath(name));

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(type => type.GetTypes())
                .Where(extension => ExtensionType.IsAssignableFrom(extension))
                .ToList();

            // Removes the base type from the enumerable
            types.RemoveAt(0);

            List<T> tempExtensions = new List<T>();

            foreach (Type extension in types)
                tempExtensions.Add(Activator.CreateInstance(extension) as T);

            extensions.AddRange(tempExtensions);

            return extensions.ToImmutable();
        }
    }
}
