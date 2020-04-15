using Amplius.Data.UBJson;
using System;

namespace Amplius.Data
{
    /// <summary>
    /// A static wrapper for the <c>Jar</c> wrapper; can be used like the JS <c>Storage</c> object.
    /// </summary>
    public static class Cookies
    {
        internal static void OnProcessExit(object sender, EventArgs e) => Default.Dispose();

        internal static readonly Jar Default = new Jar("@cookies", new EventHandler(OnProcessExit));

        public static UBValue Get(string key) => Default.Get(key);
        public static void Set(string key, UBValue value) => Default.Set(key, value);
        public static void Remove(string key) => Default.Remove(key);

        public static bool GetBool(string key) => Default.GetBool(key);
        public static char GetChar(string key) => Default.GetChar(key);
        public static string GetString(string key) => Default.GetString(key);
        public static byte GetByte(string key) => Default.GetByte(key);
        public static short GetShort(string key) => Default.GetShort(key);
        public static int GetInt(string key) => Default.GetInt(key);
        public static long GetLong(string key) => Default.GetLong(key);
        public static float GetFloat(string key) => Default.GetFloat(key);
        public static double GetDouble(string key) => Default.GetDouble(key);
#nullable enable
        public static UBObject? GetObject(string key) => Default.GetObject(key);
#nullable restore
        public static UBArray GetArray(string key) => Default.GetArray(key);
        public static byte[] GetByteArray(string key) => Default.GetByteArray(key);
        public static bool[] GetBoolArray(string key) => Default.GetBoolArray(key);
        public static short[] GetShortArray(string key) => Default.GetShortArray(key);
        public static int[] GetIntArray(string key) => Default.GetIntArray(key);
        public static long[] GetLongArray(string key) => Default.GetLongArray(key);
        public static float[] GetFloatArray(string key) => Default.GetFloatArray(key);
        public static double[] GetDoubleArray(string key) => Default.GetDoubleArray(key);
        public static string[] GetStringArray(string key) => Default.GetStringArray(key);

        public static void SetNull(string key) => Default.SetNull(key);
        public static void SetBool(string key, bool value) => Default.SetBool(key, value);
        public static void SetChar(string key, char value) => Default.SetChar(key, value);
        public static void SetByte(string key, sbyte value) => Default.SetByte(key, value);
        public static void SetShort(string key, short value) => Default.SetShort(key, value);
        public static void SetInt(string key, int value) => Default.SetInt(key, value);
        public static void SetLong(string key, long value) => Default.SetLong(key, value);
        public static void SetFloat(string key, float value) => Default.SetFloat(key, value);
        public static void SetDouble(string key, double value) => Default.SetDouble(key, value);
        public static void SetString(string key, string value) => Default.SetString(key, value);
        public static void SetObject(string key, UBObject value) => Default.SetObject(key, value);
        public static void SetArray(string key, params UBValue[] values) => Default.SetArray(key, values);
        public static void SetByteArray(string key, params byte[] values) => Default.SetByteArray(key, values);
        public static void SetBoolArray(string key, params bool[] values) => Default.SetBoolArray(key, values);
        public static void SetShortArray(string key, params short[] values) => Default.SetShortArray(key, values);
        public static void SetIntArray(string key, params int[] values) => Default.SetIntArray(key, values);
        public static void SetLongArray(string key, params long[] values) => Default.SetLongArray(key, values);
        public static void SetFloatArray(string key, params float[] values) => Default.SetFloatArray(key, values);
        public static void SetDoubleArray(string key, params double[] values) => Default.SetDoubleArray(key, values);
        public static void SetStringArray(string key, params string[] values) => Default.SetStringArray(key, values);
    }
}
