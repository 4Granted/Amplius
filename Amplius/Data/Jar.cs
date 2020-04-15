using Amplius.Data.UBJson;
using System;
using System.IO;

namespace Amplius.Data
{
    /// <summary>
    /// A wrapper around the <c>UBObject</c> class. Can be used like a UBJ file or for quick storage.
    /// <para>Writes the object to the stream once disposed.</para>
    /// </summary>
    public class Jar : IDisposable
    {
        private string path;

        private UBObject obj;

        public Jar(string path, EventHandler disposeOnExitHandler = null)
        {
            this.path = path;

            UBReader reader = new UBReader(File.Open(path, FileMode.OpenOrCreate));

            UBObject loaded = reader.Read()?.AsObject();

            obj = loaded != null ? loaded : UBValue.CreateObject();

            reader.Dispose();

            if (disposeOnExitHandler != null)
                AppDomain.CurrentDomain.ProcessExit += disposeOnExitHandler;
        }

        public UBValue Get(string key) => obj.Get(key);
        public void Set(string key, UBValue value) => obj.Set(key, value);
        public void Remove(string key) => obj.Remove(key);

        public bool GetBool(string key) => obj.GetBool(key);
        public char GetChar(string key) => obj.GetChar(key);
        public string GetString(string key) => obj.GetString(key);
        public byte GetByte(string key) => obj.GetByte(key);
        public short GetShort(string key) => obj.GetShort(key);
        public int GetInt(string key) => obj.GetInt(key);
        public long GetLong(string key) => obj.GetLong(key);
        public float GetFloat(string key) => obj.GetFloat(key);
        public double GetDouble(string key) => obj.GetDouble(key);
#nullable enable
        public UBObject? GetObject(string key) => obj.GetObject(key);
#nullable restore
        public UBArray GetArray(string key) => obj.GetArray(key);
        public byte[] GetByteArray(string key) => obj.GetByteArray(key);
        public bool[] GetBoolArray(string key) => obj.GetBoolArray(key);
        public short[] GetShortArray(string key) => obj.GetShortArray(key);
        public int[] GetIntArray(string key) => obj.GetIntArray(key);
        public long[] GetLongArray(string key) => obj.GetLongArray(key);
        public float[] GetFloatArray(string key) => obj.GetFloatArray(key);
        public double[] GetDoubleArray(string key) => obj.GetDoubleArray(key);
        public string[] GetStringArray(string key) => obj.GetStringArray(key);

        public void SetNull(string key) => obj.SetNull(key);
        public void SetBool(string key, bool value) => obj.SetBool(key, value);
        public void SetChar(string key, char value) => obj.SetChar(key, value);
        public void SetByte(string key, sbyte value) => obj.SetByte(key, value);
        public void SetShort(string key, short value) => obj.SetShort(key, value);
        public void SetInt(string key, int value) => obj.SetInt(key, value);
        public void SetLong(string key, long value) => obj.SetLong(key, value);
        public void SetFloat(string key, float value) => obj.SetFloat(key, value);
        public void SetDouble(string key, double value) => obj.SetDouble(key, value);
        public void SetString(string key, string value) => obj.SetString(key, value);
        public void SetObject(string key, UBObject value) => obj.SetObject(key, value);
        public void SetArray(string key, params UBValue[] values) => obj.SetArray(key, values);
        public void SetByteArray(string key, params byte[] values) => obj.SetByteArray(key, values);
        public void SetBoolArray(string key, params bool[] values) => obj.SetBoolArray(key, values);
        public void SetShortArray(string key, params short[] values) => obj.SetShortArray(key, values);
        public void SetIntArray(string key, params int[] values) => obj.SetIntArray(key, values);
        public void SetLongArray(string key, params long[] values) => obj.SetLongArray(key, values);
        public void SetFloatArray(string key, params float[] values) => obj.SetFloatArray(key, values);
        public void SetDoubleArray(string key, params double[] values) => obj.SetDoubleArray(key, values);
        public void SetStringArray(string key, params string[] values) => obj.SetStringArray(key, values);

        public void Dispose()
        {
            UBWriter writer = new UBWriter(File.OpenWrite(path));
            writer.WriteObject(obj);
            writer.Dispose();
        }
    }
}
