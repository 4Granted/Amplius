using System;
using System.IO;

namespace Amplius
{
    /// <summary>
    /// Useful <see cref="Type"/> extensions.
    /// </summary>
    public static class TypeExtensions
    {
        /* Misc type-to-type conversion extensions */
        public static FileStream ToFile(this Uri uri) => uri.AbsolutePath.ToFile();
    }
}
