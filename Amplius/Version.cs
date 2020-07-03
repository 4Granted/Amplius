using Amplius.Data.UBJson;
using System;
using System.Diagnostics.CodeAnalysis;

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
    /// A wrapper around a major, minor, patch and extra (snapshot, etc) data.
    /// </summary>
    public sealed class Version : IComparable<Version>, ICloneable, IUBSerializable
    {
        public int Major => major;
        public int Minor => minor;
        public int Patch => patch;
        public string Extra => extra;

        private int major;
        private int minor;
        private int patch;
        private string extra;

        private string label => extra != "" ? $"-{extra}" : "";
        private string ver => $"{major}.{minor}.{patch}{label}";

        public Version(int major, int minor, int patch, string extra)
        {
            this.major = major;
            this.minor = minor;
            this.patch = patch;
            this.extra = extra;
        }

        public override bool Equals(object other) => 
            other is Version
            && major == ((Version)other).major
            && minor == ((Version)other).minor
            && patch == ((Version)other).patch
            && extra == ((Version)other).extra;
        public override int GetHashCode() => major * (minor + (patch * extra.GetHashCode())).GetHashCode();
        public override string ToString() => ver;

        public int CompareTo([AllowNull] Version other)
        {
            if (major > other.major) return 1;
            else if (major < other.major) return -1;
            else if (major == other.major && minor > other.minor) return 1;
            else if (major == other.major && minor < other.minor) return -1;
            else if (major == other.major && minor == other.minor && patch > other.patch) return 1;
            else if (major == other.major && minor == other.minor && patch < other.patch) return -1;
            else return 0;
        }
        public object Clone() => new Version(major, minor, patch, extra);

        /// <summary>
        /// Creates a <see cref="Version"/> object from the <paramref name="versionString"/>.
        /// </summary>
        /// <param name="versionString"></param>
        /// <returns></returns>
        public static Version FromString(string versionString)
        {
            string[] data = versionString.Split('.', '-');

            Version version;

            try
            {
                version = new Version(
                    Convert.ToInt32(data[0]),
                    Convert.ToInt32(data[1]),
                    Convert.ToInt32(data[2]),
                    data.Length > 3 ? data[3] : "");
            }
            catch (Exception) { throw new InvalidVersionString(versionString); }

            return version;
        }

        public UBObject Serialize(UBObject ub)
        {
            ub.SetString("version", label);

            return ub;
        }
        public void Deserialize(UBObject ub)
        {
            Version newVer = ub.GetString("version");

            major = newVer.major;
            minor = newVer.minor;
            patch = newVer.patch;
            extra = newVer.extra;
        }

        public static implicit operator Version(string versionString) => FromString(versionString);
        public static explicit operator string(Version version) => version.ToString();
    }

    /// <summary>
    /// A simplistic exception for invalid version strings.
    /// </summary>
    public sealed class InvalidVersionString : Exception
    {
        public InvalidVersionString(string attempted) : base($"Invalid string: Cannot convert {attempted} to a version.") { }
    }
}
