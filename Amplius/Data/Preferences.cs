using Amplius.Data.UBJson;
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

namespace Amplius.Data
{
    /// <summary>
    /// A static wrapper around a <see cref="UBObject"/> instance; used like the <c>Storage</c> object from JavaScript, or <c>PlayerPrefs</c> in Unity.
    /// </summary>
    public sealed class Preferences
    {
        private static Preferences Default;
        private readonly UBObject obj;
        private const string path = "$preferences";

        public static void Initialize() => Default = new Preferences();

        private Preferences()
        {
            var reader = new UBReader(File.Open(path, FileMode.OpenOrCreate));
            var loaded = reader.Read()?.AsObject();

            obj = loaded != null ? loaded : UBValue.CreateObject();

            reader.Dispose();

            AppDomain.CurrentDomain.ProcessExit += new EventHandler((object sender, EventArgs e) =>
            {
                UBWriter writer = new UBWriter(File.OpenWrite(path));
                writer.WriteObject(obj);
                writer.Dispose();
            });
        }

        public static UBValue Get(string key) => Default.obj.Get(key);
        public static void Set(string key, UBValue value) => Default.obj.Set(key, value);
        public static void Remove(string key) => Default.obj.Remove(key);

        public static bool GetBool(string key) => Default.obj.GetBool(key);
        public static char GetChar(string key) => Default.obj.GetChar(key);
        public static string GetString(string key) => Default.obj.GetString(key);
        public static byte GetByte(string key) => Default.obj.GetByte(key);
        public static short GetShort(string key) => Default.obj.GetShort(key);
        public static int GetInt(string key) => Default.obj.GetInt(key);
        public static long GetLong(string key) => Default.obj.GetLong(key);
        public static float GetFloat(string key) => Default.obj.GetFloat(key);
        public static double GetDouble(string key) => Default.obj.GetDouble(key);
#nullable enable
        public static UBObject? GetObject(string key) => Default.obj.GetObject(key);
#nullable restore
        public static UBArray GetArray(string key) => Default.obj.GetArray(key);
        public static byte[] GetByteArray(string key) => Default.obj.GetByteArray(key);
        public static bool[] GetBoolArray(string key) => Default.obj.GetBoolArray(key);
        public static short[] GetShortArray(string key) => Default.obj.GetShortArray(key);
        public static int[] GetIntArray(string key) => Default.obj.GetIntArray(key);
        public static long[] GetLongArray(string key) => Default.obj.GetLongArray(key);
        public static float[] GetFloatArray(string key) => Default.obj.GetFloatArray(key);
        public static double[] GetDoubleArray(string key) => Default.obj.GetDoubleArray(key);
        public static string[] GetStringArray(string key) => Default.obj.GetStringArray(key);

        public static void SetNull(string key) => Default.obj.SetNull(key);
        public static void SetBool(string key, bool value) => Default.obj.SetBool(key, value);
        public static void SetChar(string key, char value) => Default.obj.SetChar(key, value);
        public static void SetByte(string key, sbyte value) => Default.obj.SetByte(key, value);
        public static void SetShort(string key, short value) => Default.obj.SetShort(key, value);
        public static void SetInt(string key, int value) => Default.obj.SetInt(key, value);
        public static void SetLong(string key, long value) => Default.obj.SetLong(key, value);
        public static void SetFloat(string key, float value) => Default.obj.SetFloat(key, value);
        public static void SetDouble(string key, double value) => Default.obj.SetDouble(key, value);
        public static void SetString(string key, string value) => Default.obj.SetString(key, value);
        public static void SetObject(string key, UBObject value) => Default.obj.SetObject(key, value);
        public static void SetArray(string key, params UBValue[] values) => Default.obj.SetArray(key, values);
        public static void SetByteArray(string key, params byte[] values) => Default.obj.SetByteArray(key, values);
        public static void SetBoolArray(string key, params bool[] values) => Default.obj.SetBoolArray(key, values);
        public static void SetShortArray(string key, params short[] values) => Default.obj.SetShortArray(key, values);
        public static void SetIntArray(string key, params int[] values) => Default.obj.SetIntArray(key, values);
        public static void SetLongArray(string key, params long[] values) => Default.obj.SetLongArray(key, values);
        public static void SetFloatArray(string key, params float[] values) => Default.obj.SetFloatArray(key, values);
        public static void SetDoubleArray(string key, params double[] values) => Default.obj.SetDoubleArray(key, values);
        public static void SetStringArray(string key, params string[] values) => Default.obj.SetStringArray(key, values);
    }
}
