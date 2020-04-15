using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Amplius
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetBases<T>(this T self)
        {
            return Assembly.GetAssembly(typeof(T)).GetTypes().Where(type => type.IsSubclassOf(typeof(T)));
        }

        /* Misc type-to-type conversion extensions */
        public static FileStream ToFile(this Uri uri) => uri.AbsolutePath.ToFile();
    }
}
