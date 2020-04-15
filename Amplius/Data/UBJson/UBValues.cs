using System;
using System.Collections;
using System.Collections.Generic;

namespace Amplius.Data.UBJson
{
    public partial class UBValue
    {
        /// <summary>
        /// Returns a global <c>UBNull</c> constant.
        /// </summary>
        public static readonly UBNull NULL = new UBNull();
        /// <summary>
        /// Returns a global <c>UBBool</c> (true) constant.
        /// </summary>
        public static readonly UBBool TRUE = new UBBool(true);
        /// <summary>
        /// Returns a global <c>UBBool</c> (false) constant.
        /// </summary>
        public static readonly UBBool FALSE = new UBBool(false);

        /// <summary>
        /// <c>REDUNDANT</c>: Only use for a Func or delegate value; otherwise, use <c>UBValueFactory.NULL</c>.
        /// </summary>
        /// <returns></returns>
        public static UBNull CreateNull() => NULL;
        /// <summary>
        /// Creates a <c>UBBool</c> based off of the <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBBool CreateBool(bool value) => value ? TRUE : FALSE;
        /// <summary>
        /// Creates a <c>UBChar</c> based off of the <paramref name="value"/> <c>char</c>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBChar CreateChar(char value) => new UBChar(value);

        /// <summary>
        /// Creates a <c>UBUInt8</c> based off of the <paramref name="value"/> <c>byte</c>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBUInt8 CreateUInt8(byte value) => new UBUInt8(value);
        /// <summary>
        /// Creates a <c>UBInt8</c> based off of the <paramref name="value"/> <c>byte</c>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBInt8 CreateInt8(sbyte value) => new UBInt8(value);
        /// <summary>
        /// Creates a <c>UBInt16</c> based off of the <paramref name="value"/> <c>short</c>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBInt16 CreateInt16(short value) => new UBInt16(value);
        /// <summary>
        /// Creates a <c>UBInt32</c> based off of the <paramref name="value"/> <c>int</c>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBInt32 CreateInt32(int value) => new UBInt32(value);
        /// <summary>
        /// Creates a <c>UBInt64</c> based off of the <paramref name="value"/> <c>long</c>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBInt64 CreateInt64(long value) => new UBInt64(value);

        /// <summary>
        /// Creates a <c>UBValue</c> based off of the <paramref name="value"/>'s size and capacity.
        /// </summary>
        /// <param name="value"></param>
        public static UBValue CreateIntAuto(long value)
        {
            if (InRange(value, 0, 255))
                return new UBUInt8((byte)value);
            else if (InRange(value, -128, 127))
                return new UBInt8((sbyte)value);
            else if (InRange(value, -32768, 32767))
                return new UBInt16((short)value);
            else if (InRange(value, -2147483648, 2147483647))
                return new UBInt32((int)value);
            else
                return new UBInt64(value);
        }

        /// <summary>
        /// Creates a <c>UBFloat32</c> based off of the <paramref name="value"/> <c>float</c>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBFloat32 CreateFloat(float value) => new UBFloat32(value);
        /// <summary>
        /// Creates a <c>UBFloat64</c> based off of the <paramref name="value"/> <c>double</c>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBFloat64 CreateDouble(double value) => new UBFloat64(value);

        /// <summary>
        /// Creates a <c>UBString</c> based off of the <paramref name="value"/> <c>byte</c> array.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBString CreateString(byte[] value) => new UBString(value);
        /// <summary>
        /// Creates a <c>UBString</c> based off of the <paramref name="value"/> <c>string</c> data.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBString CreateString(string value) => new UBString(UBString.UTF8.GetBytes(value));
        /// <summary>
        /// Creates a <c>UBString</c> based off of the <paramref name="value"/> <c>string</c> data; if null, returns a <c>UBNull</c>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBValue CreateStringOrNull(string value) => value == null ? CreateNull() : (UBValue)CreateString(value);

        /// <summary>
        /// Creates an array—or null—of type <c>byte</c>.
        /// </summary>
        public static UBValue CreateArrayOrNull(byte[] values) => values == null ? CreateNull() : (UBValue)CreateArray(values);
        /// <summary>
        /// Creates an array—or null—of type <c>short</c>.
        /// </summary>
        public static UBValue CreateArrayOrNull(short[] values) => values == null ? CreateNull() : (UBValue)CreateArray(values);
        /// <summary>
        /// Creates an array—or null—of type <c>int</c>.
        /// </summary>
        public static UBValue CreateArrayOrNull(int[] values) => values == null ? CreateNull() : (UBValue)CreateArray(values);
        /// <summary>
        /// Creates an array—or null—of type <c>long</c>.
        /// </summary>
        public static UBValue CreateArrayOrNull(long[] values) => values == null ? CreateNull() : (UBValue)CreateArray(values);
        /// <summary>
        /// Creates an array—or null—of type <c>float</c>.
        /// </summary>
        public static UBValue CreateArrayOrNull(float[] values) => values == null ? CreateNull() : (UBValue)CreateArray(values);
        /// <summary>
        /// Creates an array—or null—of type <c>double</c>.
        /// </summary>
        public static UBValue CreateArrayOrNull(double[] values) => values == null ? CreateNull() : (UBValue)CreateArray(values);
        /// <summary>
        /// Creates an array—or null—of type <c>string</c>.
        /// </summary>
        public static UBValue CreateArrayOrNull(string[] values) => values == null ? CreateNull() : (UBValue)CreateArray(values);
        /// <summary>
        /// Creates an array—or null—of type <c>bool</c>.
        /// </summary>
        public static UBValue CreateArrayOrNull(bool[] values) => values == null ? CreateNull() : (UBValue)CreateArray(values);


        /// <summary>
        /// Creates an array of type <c>byte</c>.
        /// </summary>
        public static UBInt8Array CreateArray(byte[] bytes) => new UBInt8Array(bytes);
        /// <summary>
        /// Creates an array of type <c>short</c>.
        /// </summary>
        public static UBInt16Array CreateArray(short[] values) => new UBInt16Array(values);
        /// <summary>
        /// Creates an array of type <c>int</c>.
        /// </summary>
        public static UBInt32Array CreateArray(int[] values) => new UBInt32Array(values);
        /// <summary>
        /// Creates an array of type <c>long</c>.
        /// </summary>
        public static UBInt64Array CreateArray(long[] values) => new UBInt64Array(values);
        /// <summary>
        /// Creates an array of type <c>float</c>.
        /// </summary>
        public static UBFloat32Array CreateArray(float[] values) => new UBFloat32Array(values);
        /// <summary>
        /// Creates an array of type <c>double</c>.
        /// </summary>
        public static UBFloat64Array CreateArray(double[] values) => new UBFloat64Array(values);
        /// <summary>
        /// Creates an array of type <c>string</c>.
        /// </summary>
        public static UBStringArray CreateArray(string[] values) => new UBStringArray(values);
        /// <summary>
        /// Creates an array of type <c>bool</c>.
        /// </summary>
        public static UBInt8Array CreateArray(bool[] values)
        {
            byte[] data = new byte[values.Length];

            for (int i = 0; i < data.Length; i++)
                data[i] = (byte)(values[i] ? 1 : 0);

            return CreateArray(data);
        }
        /// <summary>
        /// Creates a generic (<c>UBValue</c>) array.
        /// </summary>
        public static UBArray CreateArray(params UBValue[] values) => new UBArray(values);
        /// <summary>
        /// Creates a generic (<c>UBValue</c>) array from an <c>IEnumerable</c>.
        /// </summary>
        public static UBValue CreateArray(IEnumerable enumerable)
        {
            if (enumerable is byte[]) return CreateArray(enumerable as byte[]);
            else if (enumerable is short[]) return CreateArray(enumerable as short[]);
            else if(enumerable is int[]) return CreateArray(enumerable as int[]);
            else if(enumerable is long[]) return CreateArray(enumerable as long[]);
            else if(enumerable is float[]) return CreateArray(enumerable as float[]);
            else if(enumerable is double[]) return CreateArray(enumerable as double[]);
            else if(enumerable is string[]) return CreateArray(enumerable as string[]);
            else if(enumerable is UBValue[]) return CreateArray(enumerable as UBValue[]);
            else if(enumerable is bool[]) return CreateArray(enumerable as bool[]);
            else throw new Exception($"Unknow array type: {enumerable.GetType().Name}; nothing further will be done.");
        }

        /// <summary>
        /// Creates an empty <c>UBObject</c>.
        /// </summary>
        public static UBObject CreateObject() => new UBObject();
        /// <summary>
        /// Creates an empty <c>UBObject</c> with the provided map (SortedDictionary).
        /// </summary>
        public static UBObject CreateObject(SortedDictionary<string, UBValue> map) => new UBObject(map);

        /// <summary>
        /// Creates a <c>UBValue</c> based off of the <paramref name="value"/>'s type, and returns it as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type tp wrap as</typeparam>
        /// <param name="value">Value to wrap</param>
        /// <returns></returns>
        public static T CreateValue<T>(object value) where T : UBValue => CreateValue(value) as T;
        /// <summary>
        /// Creates a <c>UBValue</c> based off of the <paramref name="value"/>'s type.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBValue CreateValue(object value)
        {
            if (value == null)
                return CreateNull();
            else if (value is UBValue)
                return (UBValue)value;
            else if (value is bool)
                return CreateBool(((bool)value));
            else if (value is byte)
                return CreateUInt8(((byte)value));
            else if (value is sbyte)
                return CreateInt8(((sbyte)value));
            else if (value is short)
                return CreateInt16(((short)value));
            else if (value is int)
                return CreateInt32(((int)value));
            else if (value is long)
                return CreateInt64(((long)value));
            else if (value is int)
                return CreateIntAuto(((long)value));
            else if (value is float)
                return CreateFloat(((float)value));
            else if (value is double)
                return CreateDouble(((double)value));
            else if (value is string)
                return CreateString(((string)value));
            else if (value is SortedDictionary<string, UBValue>)
                return CreateObject(((SortedDictionary<string, UBValue>)value));
            else if (value is IEnumerable)
                return CreateArray(((IEnumerable)value));
            else
                throw new Exception($"Unknow object type: {value.GetType().Name}; nothing further will be done.");
        }
        /// <summary>
        /// Creates a <c>UBValue</c> based off of the <paramref name="value"/>'s type, if the value is null, create a <c>UBNull</c>.
        /// </summary>
        /// <param name="value">Value to wrap</param>
        public static UBValue CreateValueOrNull(object value) => value == null ? CreateNull() : CreateValue(value);

        internal static bool InRange(long value, long min, long max) => value >= min && value <= max;
    }
}
