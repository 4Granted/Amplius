using System;
using System.IO;

namespace Amplius
{
    /// <summary>
    /// Useful <see cref="string"/> extensions.
    /// </summary>
    public static class StringExtensions
    {
        public static string ReplaceAll(this string self, string replacement, params string[] strings)
        {
            var str = self;

            foreach (var s in strings)
                str.Replace(s, replacement);

            return str;
        }
        public static string RemoveAll(this string self, params string[] strings) => self.ReplaceAll("", strings);

        public static Version ToVersion(this string self) => self;
        public static Uri ToURI(this string self) => new Uri(self);
        public static FileStream ToFile(this string self, FileMode mode = FileMode.OpenOrCreate, FileAccess access = FileAccess.ReadWrite, FileShare share = FileShare.None) => File.Open(self, mode, access, share);
    }
}
