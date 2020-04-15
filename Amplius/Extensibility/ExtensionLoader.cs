using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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

        public List<T> Extensions => extensions;

        private List<T> extensions;

        public ExtensionLoader() => extensions = new List<T>();

        public IEnumerable<T> LoadFrom(string path)
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

            return extensions;
        }
    }
}
