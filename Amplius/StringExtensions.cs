using System;
using System.IO;

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
    /// Useful <see cref="string"/> extensions.
    /// </summary>
    public static class StringExtensions
    {
        public static string Slice(this string self, int start, int end)
        {
            if (start > self.Length)
                throw new ArgumentOutOfRangeException("start", "The 'start' parameter of string.Slice cannot be larger than the base string");

            if (end < 0) end = self.Length + end;
            int len = end - start;
            return self.Substring(start, len);
        }
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
