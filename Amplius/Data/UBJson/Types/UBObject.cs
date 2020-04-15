using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <info>
/// All code below is derived—at least somewhat—from https://github.com/dinocore1/ubjson.
/// 
/// Their projects license can be found here: https://github.com/dinocore1/ubjson/blob/master/LICENSE.txt;
/// </info>
/// 
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

namespace Amplius.Data.UBJson
{
    public class UBObject : UBValue<SortedDictionary<string, UBValue>>, IEnumerable<KeyValuePair<string, UBValue>>
    {
        public int Length => value.Count;

        public UBObject() : base(new SortedDictionary<string, UBValue>()) { }
        public UBObject(SortedDictionary<string, UBValue> value) : base(value) { }

        public UBValue Get(string key) => value[key];
        public string GetKeyAt(int index) => value.ElementAt(index).Key;
        public UBValue GetValueAt(int index) => value.ElementAt(index).Value;
        public T Get<T>(string key) where T : UBValue => value[key] as T;
        public void Add(string key, UBValue value) => this.value.Add(key, value);
        public void Set(string key, UBValue value) => this.value[key] = value;
        public void AddOrSet(string key, UBValue value)
        {
            if (this.value.ContainsKey(key))
                Set(key, value);
            else
                Add(key, value);
        }
        public void Remove(string key) => value.Remove(key);

        public bool GetBool(string key)
        {
            var val = Get(key);

            return val!.IsBool() ? val.AsBool() : false;
        }
        public char GetChar(string key)
        {
            var val = Get(key);

            return val!.IsChar() ? val.AsChar() : (char)0;
        }
        public string GetString(string key)
        {
            var val = Get(key);

            return val!.IsString() ? val.AsString() : "";
        }
        public byte GetByte(string key)
        {
            var val = Get(key);

            return (val != null && val.IsNumber()) ? val.AsByte() : (byte)0;
        }
        public short GetShort(string key)
        {
            var val = Get(key);

            return (val != null && val.IsNumber()) ? val.AsShort() : (short)0;
        }
        public int GetInt(string key)
        {
            var val = Get(key);

            return (val != null && val.IsNumber()) ? val.AsInt() : 0;
        }
        public long GetLong(string key)
        {
            var val = Get(key);

            return (val != null && val.IsNumber()) ? val.AsLong() : 0;
        }
        public float GetFloat(string key)
        {
            var val = Get(key);

            return (val != null && val.IsFloat()) ? val.AsFloat32() : 0f;
        }
        public double GetDouble(string key)
        {
            var val = Get(key);

            return (val != null && val.IsFloat()) ? val.AsFloat64() : 0d;
        }
#nullable enable
        public UBObject? GetObject(string key)
        {
            var val = Get(key);

            return (val != null && val.IsObject()) ? val.AsObject() : null;
        }
#nullable restore
        public UBArray GetArray(string key)
        {
            var val = Get(key);

            return (val != null && val.IsArray()) ? val.AsArray() : UBValue.CreateArray();
        }
        public byte[] GetByteArray(string key)
        {
            var val = GetArray(key);

            return (val != null && val.GetArrayType == UBArrayType.INT8) ? (val as UBInt8Array).GetValue() : new byte[0];
        }
        public bool[] GetBoolArray(string key)
        {
            var val = GetByteArray(key);
            var bools = new bool[val.Length];

            for (int i = 0; i < val.Length; i++)
                bools[i] = val[i] == (byte)1;

            return val != null ? bools : new bool[0];
        }
        public short[] GetShortArray(string key)
        {
            var val = GetArray(key);

            return (val != null && val.GetArrayType == UBArrayType.INT16) ? (val as UBInt16Array).GetValue() : new short[0];
        }
        public int[] GetIntArray(string key)
        {
            var val = GetArray(key);

            return (val != null && val.GetArrayType == UBArrayType.INT32) ? (val as UBInt32Array).GetValue() : new int[0];
        }
        public long[] GetLongArray(string key)
        {
            var val = GetArray(key);

            return (val != null && val.GetArrayType == UBArrayType.INT64) ? (val as UBInt64Array).GetValue() : new long[0];
        }
        public float[] GetFloatArray(string key)
        {
            var val = GetArray(key);

            return (val != null && val.GetArrayType == UBArrayType.FLOAT32) ? (val as UBFloat32Array).GetValue() : new float[0];
        }
        public double[] GetDoubleArray(string key)
        {
            var val = GetArray(key);

            return (val != null && val.GetArrayType == UBArrayType.FLOAT64) ? (val as UBFloat64Array).GetValue() : new double[0];
        }
        public string[] GetStringArray(string key)
        {
            var val = GetArray(key);

            return (val != null && val.GetArrayType == UBArrayType.STRING) ? (val as UBStringArray).GetValue() : new string[0];
        }

        public void SetNull(string key) => AddOrSet(key, NULL);
        public void SetBool(string key, bool value) => AddOrSet(key, CreateBool(value));
        public void SetChar(string key, char value) => AddOrSet(key, CreateChar(value));
        public void SetByte(string key, sbyte value) => AddOrSet(key, CreateInt8(value));
        public void SetShort(string key, short value) => AddOrSet(key, CreateInt16(value));
        public void SetInt(string key, int value) => AddOrSet(key, CreateInt32(value));
        public void SetLong(string key, long value) => AddOrSet(key, CreateInt64(value));
        public void SetFloat(string key, float value) => AddOrSet(key, CreateFloat(value));
        public void SetDouble(string key, double value) => AddOrSet(key, CreateDouble(value));
        public void SetString(string key, string value) => AddOrSet(key, CreateString(value));
        public void SetObject(string key, UBObject value) => AddOrSet(key, value);
        public void SetArray(string key, params UBValue[] values) => AddOrSet(key, CreateArray(values));
        public void SetByteArray(string key, params byte[] values) => AddOrSet(key, CreateArray(values));
        public void SetBoolArray(string key, params bool[] values) => AddOrSet(key, CreateArray(values));
        public void SetShortArray(string key, params short[] values) => AddOrSet(key, CreateArray(values));
        public void SetIntArray(string key, params int[] values) => AddOrSet(key, CreateArray(values));
        public void SetLongArray(string key, params long[] values) => AddOrSet(key, CreateArray(values));
        public void SetFloatArray(string key, params float[] values) => AddOrSet(key, CreateArray(values));
        public void SetDoubleArray(string key, params double[] values) => AddOrSet(key, CreateArray(values));
        public void SetStringArray(string key, params string[] values) => AddOrSet(key, CreateArray(values));

        public bool ContainsKey(string key) => value.ContainsKey(key);
        public bool ContainsValue(UBValue value) => this.value.ContainsValue(value);

        public ICollection<string> Keys() => value.Keys as ICollection<string>;
        public ICollection<UBValue> Values() => value.Values as ICollection<UBValue>;

        public bool IsEmpty() => value.Count <= 0;
        public void Clear() => value.Clear();

        public override UBType GetUBType => UBType.OBJECT;
        public override SortedDictionary<string, UBValue> GetValue() => value;
        public override string ToString() => value.ToString();
        public override int GetHashCode()
        {
            int val = 0;

            foreach (var key in Keys())
                val ^= key.GetHashCode() + value[key].GetHashCode();

            return val;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is UBObject))
                return false;

            UBObject uobj = (UBObject)obj;

            return value.Equals(uobj.value);
        }

        public List<KeyValuePair<string, UBValue>> Pairs()
        {
            var list = new List<KeyValuePair<string, UBValue>>();

            foreach (var key in Keys())
                list.Add(new KeyValuePair<string, UBValue>(key, value[key]));

            return list;
        }

        public IEnumerator<KeyValuePair<string, UBValue>> GetEnumerator() => Pairs().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
