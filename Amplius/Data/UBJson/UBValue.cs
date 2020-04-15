using System;
using System.Diagnostics.CodeAnalysis;

namespace Amplius.Data.UBJson
{
    /// <summary>
    /// A base <c>UBJ</c> wrapper for objects.
    /// </summary>
    public abstract partial class UBValue : IComparable<UBValue>
    {
        /// <summary>
        /// Contains all <c>markers</c> and their respective <c>chars</c>.
        /// </summary>
        public static readonly (char, byte)[] Markers = new (char, byte)[]
        {
            ('Z', NULL_MARKER),
            ('T', TRUE_MARKER),
            ('F', FALSE_MARKER),
            ('C', CHAR_MARKER),
            ('S', STRING_MARKER),
            ('i', INT8_MARKER),
            ('u', UINT8_MARKER),
            ('I', INT16_MARKER),
            ('l', INT32_MARKER),
            ('l', INT64_MARKER),
            ('d', FLOAT32_MARKER),
            ('D', FLOAT64_MARKER),
            ('[', ARRAY_START_MARKER),
            (']', ARRAY_END_MARKER),
            ('{', OBJ_START_MARKER),
            ('}', OBJ_END_MARKER),
            ('$', OPTIMIZED_TYPE_MARKER),
            ('#', OPTIMIZED_SIZE_MARKER),
        };

        internal const byte NULL_MARKER = (byte)'Z';
        internal const byte TRUE_MARKER = (byte)'T';
        internal const byte FALSE_MARKER = (byte)'F';
        internal const byte CHAR_MARKER = (byte)'C';
        internal const byte STRING_MARKER = (byte)'S';
        internal const byte INT8_MARKER = (byte)'i';
        internal const byte UINT8_MARKER = (byte)'U';
        internal const byte INT16_MARKER = (byte)'I';
        internal const byte INT32_MARKER = (byte)'l';
        internal const byte INT64_MARKER = (byte)'L';
        internal const byte FLOAT32_MARKER = (byte)'d';
        internal const byte FLOAT64_MARKER = (byte)'D';
        internal const byte ARRAY_START_MARKER = (byte)'[';
        internal const byte ARRAY_END_MARKER = (byte)']';
        internal const byte OBJ_START_MARKER = (byte)'{';
        internal const byte OBJ_END_MARKER = (byte)'}';
        internal const byte OPTIMIZED_TYPE_MARKER = (byte)'$';
        internal const byte OPTIMIZED_SIZE_MARKER = (byte)'#';

        /* Flag markers */

        /// <summary>
        /// Raw value stored by the wrapper.
        /// </summary>
        public object ObjValue => objValue;

        protected object objValue;

        /// <summary>
        /// Returns the <c>UBType</c> of the wrapper.
        /// </summary>
        /// <returns></returns>
        public abstract UBType GetUBType { get; }
        /// <summary>
        /// Overrides the base <c>ToString</c> method.
        /// </summary>
        /// <returns></returns>
        public new abstract string ToString();

        /// <summary>
        /// Determines whether or not the wrapper is of type <c>UBNull</c>.
        /// </summary>
        public bool IsNull() => GetUBType == UBType.NULL;
        /// <summary>
        /// Determines whether or not the wrapper is of type <c>UBBool</c>.
        /// </summary>
        public bool IsBool() => GetUBType == UBType.BOOL;
        /// <summary>
        /// Determines whether or not the wrapper is of type <c>UBChar</c>.
        /// </summary>
        public bool IsChar() => GetUBType == UBType.CHAR;
        /// <summary>
        /// Determines whether or not the wrapper is of type <c>UBString</c>.
        /// </summary>
        public bool IsString() => GetUBType == UBType.STRING;
        /// <summary>
        /// Determines whether or not the wrapper is of type <c>UBArray</c>.
        /// </summary>
        public bool IsArray() => GetUBType == UBType.ARRAY;
        /// <summary>
        /// Determines whether or not the wrapper is of type <c>UBObject</c>.
        /// </summary>
        public bool IsObject() => GetUBType == UBType.OBJECT;
        /// <summary>
        /// Determines whether or not the wrapper is of type <c>byte</c>, <c>uint</c>, <c>short</c>, <c>int</c>, <c>long</c>, <c>float</c> or <c>double</c>.
        /// </summary>
        public bool IsNumber()
        {
            switch (GetUBType)
            {
                case UBType.INT8:
                case UBType.UINT8:
                case UBType.INT16:
                case UBType.INT32:
                case UBType.INT64:
                case UBType.FLOAT32:
                case UBType.FLOAT64:
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// Determines whether or not the wrapper is of type <c>byte</c>, <c>uint</c>, <c>short</c>, <c>int</c> or <c>long</c>.
        /// </summary>
        public bool IsInt()
        {
            switch (GetUBType)
            {
                case UBType.INT8:
                case UBType.UINT8:
                case UBType.INT16:
                case UBType.INT32:
                case UBType.INT64:
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// Determines whether or not the wrapper is of type <c>float</c> or <c>double</c>.
        /// </summary>
        public bool IsFloat()
        {
            switch (GetUBType)
            {
                case UBType.FLOAT32:
                case UBType.FLOAT64:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns the wrapper as a <c>bool</c>.
        /// </summary>
        public bool AsBool() => ((UBBool)this).GetValue();
        /// <summary>
        /// Returns the wrapper as a <c>char</c>.
        /// </summary>
        public char AsChar() => ((UBChar)this).GetValue();
        /// <summary>
        /// Returns the wrapper as a <c>string</c>.
        /// </summary>
        public string AsString() => ((UBString)this)?.ToString();
        /// <summary>
        /// Returns the wrapper as a <c>byte</c>.
        /// </summary>
        public byte AsByte() => (byte)AsInt();
        /// <summary>
        /// Returns the wrapper as a <c>short</c>.
        /// </summary>
        public short AsShort() => (short)AsInt();
        /// <summary>
        /// Returns the wrapper as a <c>int</c>.
        /// </summary>
        public int AsInt()
        {
            switch (GetUBType)
            {
                case UBType.INT8:
                    return ((UBInt8)this).GetValue();
                case UBType.UINT8:
                    return ((UBUInt8)this).GetValue();
                case UBType.INT16:
                    return ((UBInt16)this).GetValue();
                case UBType.INT32:
                    return ((UBInt32)this).GetValue();
                case UBType.INT64:
                    return (int)((UBInt64)this).GetValue();
                case UBType.FLOAT32:
                    return (int)((UBFloat32)this).GetValue();
                case UBType.FLOAT64:
                    return (int)((UBFloat64)this).GetValue();
                case UBType.STRING:
                    return Convert.ToInt32(AsString());
                default:
                    throw new Exception("Not a number type");
            }
        }
        /// <summary>
        /// Returns the wrapper as a <c>long</c>.
        /// </summary>
        public long AsLong()
        {
            switch (GetUBType)
            {
                case UBType.BOOL:
                    return AsBool() ? 1 : 0;
                case UBType.CHAR:
                    return AsChar();
                case UBType.INT8:
                    return ((UBInt8)this).GetValue();
                case UBType.UINT8:
                    return ((UBUInt8)this).GetValue();
                case UBType.INT16:
                    return ((UBInt16)this).GetValue();
                case UBType.INT32:
                    return ((UBInt32)this).GetValue();
                case UBType.INT64:
                    return (long)((UBInt64)this).GetValue();
                case UBType.FLOAT32:
                    return (long)((UBFloat32)this).GetValue();
                case UBType.FLOAT64:
                    return (long)((UBFloat64)this).GetValue();
                case UBType.STRING:
                    return Convert.ToInt64(AsString());
                default:
                    throw new Exception("Not a number type");
            }
        }
        /// <summary>
        /// Returns the wrapper as a <c>float</c>.
        /// </summary>
        public float AsFloat32()
        {
            switch (GetUBType)
            {
                case UBType.FLOAT32:
                    return ((UBFloat32)this).GetValue();
                case UBType.FLOAT64:
                    return (float)((UBFloat64)this).GetValue();
                case UBType.INT8:
                    return ((UBInt8)this).GetValue();
                case UBType.UINT8:
                    return ((UBUInt8)this).GetValue();
                case UBType.INT16:
                    return ((UBInt16)this).GetValue();
                case UBType.INT32:
                    return ((UBInt32)this).GetValue();
                case UBType.INT64:
                    return ((UBInt64)this).GetValue();
                case UBType.STRING:
                    return Convert.ToInt32(AsString());
                default:
                    throw new Exception("Not a float type");
            }
        }
        /// <summary>
        /// Returns the wrapper as a <c>float</c>.
        /// </summary>
        public float AsFloat64()
        {
            switch (GetUBType)
            {
                case UBType.FLOAT32:
                    return ((UBFloat32)this).GetValue();
                case UBType.FLOAT64:
                    return (float)((UBFloat64)this).GetValue();
                case UBType.INT8:
                    return ((UBInt8)this).GetValue();
                case UBType.UINT8:
                    return ((UBUInt8)this).GetValue();
                case UBType.INT16:
                    return ((UBInt16)this).GetValue();
                case UBType.INT32:
                    return ((UBInt32)this).GetValue();
                case UBType.INT64:
                    return ((UBInt64)this).GetValue();
                case UBType.STRING:
                    return Convert.ToInt32(AsString());
                default:
                    throw new Exception("Not a float type");
            }
        }
        /// <summary>
        /// Returns the wrapper as a <c>UBArray</c>.
        /// </summary>
        public UBArray AsArray() => ((UBArray)this);
        /// <summary>
        /// Returns the wrapper as a <c>bool[]</c>.
        /// </summary>
        public bool[] AsBoolArray()
        {
            bool[] val;
            UBArray array = AsArray();

            switch (array.GetArrayType)
            {
                case UBArrayType.INT8:
                    {
                        byte[] data = ((UBInt8Array)array).GetValue();
                        val = new bool[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i] > 0;

                        break;
                    }
                case UBArrayType.INT16:
                    {
                        short[] data = ((UBInt16Array)array).GetValue();
                        val = new bool[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i] > 0;

                        break;
                    }
                case UBArrayType.INT32:
                    {
                        int[] data = ((UBInt32Array)array).GetValue();
                        val = new bool[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i] > 0;

                        break;
                    }
                case UBArrayType.INT64:
                    {
                        long[] data = ((UBInt64Array)array).GetValue();
                        val = new bool[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i] > 0;

                        break;
                    }
                case UBArrayType.FLOAT32:
                    {
                        float[] data = ((UBFloat32Array)array).GetValue();
                        val = new bool[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i] > 0;

                        break;
                    }
                case UBArrayType.FLOAT64:
                    {
                        double[] data = ((UBFloat64Array)array).GetValue();
                        val = new bool[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i] > 0;

                        break;
                    }
                default:
                    throw new Exception("not an int32[] type");
            }

            return val;
        }
        /// <summary>
        /// Returns the wrapper as a <c>short[]</c>.
        /// </summary>
        public short[] AsShortArray()
        {
            short[] val;
            UBArray array = AsArray();

            switch (array.GetArrayType)
            {
                case UBArrayType.INT8:
                    {
                        byte[] data = ((UBInt8Array)array).GetValue();
                        val = new short[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT16:
                    {
                        val = ((UBInt16Array)array).GetValue();
                        break;
                    }
                case UBArrayType.INT32:
                    {
                        int[] data = ((UBInt32Array)array).GetValue();
                        val = new short[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (short)data[i];

                        break;
                    }
                case UBArrayType.INT64:
                    {
                        long[] data = ((UBInt64Array)array).GetValue();
                        val = new short[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (short)data[i];

                        break;
                    }
                case UBArrayType.FLOAT32:
                    {
                        float[] data = ((UBFloat32Array)array).GetValue();
                        val = new short[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (short)data[i];

                        break;
                    }
                case UBArrayType.FLOAT64:
                    {
                        double[] data = ((UBFloat64Array)array).GetValue();
                        val = new short[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (short)data[i];

                        break;
                    }
                default:
                    throw new Exception("not an int32[] type");
            }

            return val;
        }
        /// <summary>
        /// Returns the wrapper as a <c>int[]</c>.
        /// </summary>
        public int[] AsIntArray()
        {
            int[] val;
            UBArray array = AsArray();

            switch (array.GetArrayType)
            {
                case UBArrayType.INT8:
                    {
                        byte[] data = ((UBInt8Array)array).GetValue();
                        val = new int[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT16:
                    {
                        short[] data = ((UBInt16Array)array).GetValue();
                        val = new int[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT32:
                    {
                        val = ((UBInt32Array)array).GetValue();
                        break;
                    }
                case UBArrayType.INT64:
                    {
                        long[] data = ((UBInt64Array)array).GetValue();
                        val = new int[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (int)data[i];

                        break;
                    }
                case UBArrayType.FLOAT32:
                    {
                        float[] data = ((UBFloat32Array)array).GetValue();
                        val = new int[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (int)data[i];

                        break;
                    }
                case UBArrayType.FLOAT64:
                    {
                        double[] data = ((UBFloat64Array)array).GetValue();
                        val = new int[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (int)data[i];

                        break;
                    }
                default:
                    throw new Exception("not an int32[] type");
            }

            return val;
        }
        /// <summary>
        /// Returns the wrapper as a <c>long[]</c>.
        /// </summary>
        public long[] AsLongArray()
        {
            long[] val;
            UBArray array = AsArray();

            switch (array.GetArrayType)
            {
                case UBArrayType.INT8:
                    {
                        byte[] data = ((UBInt8Array)array).GetValue();
                        val = new long[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT16:
                    {
                        short[] data = ((UBInt16Array)array).GetValue();
                        val = new long[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT32:
                    {
                        int[] data = ((UBInt32Array)array).GetValue();
                        val = new long[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT64:
                    {
                        val = ((UBInt64Array)array).GetValue();
                        break;
                    }
                case UBArrayType.FLOAT32:
                    {
                        float[] data = ((UBFloat32Array)array).GetValue();
                        val = new long[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (long)data[i];

                        break;
                    }
                case UBArrayType.FLOAT64:
                    {
                        double[] data = ((UBFloat64Array)array).GetValue();
                        val = new long[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (long)data[i];

                        break;
                    }
                default:
                    throw new Exception("not an int32[] type");
            }

            return val;
        }
        /// <summary>
        /// Returns the wrapper as a <c>float[]</c>.
        /// </summary>
        public float[] AsFloatArray()
        {
            float[] val;
            UBArray array = AsArray();

            switch (array.GetArrayType)
            {
                case UBArrayType.INT8:
                    {
                        byte[] data = ((UBInt8Array)array).GetValue();
                        val = new float[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT16:
                    {
                        short[] data = ((UBInt16Array)array).GetValue();
                        val = new float[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT32:
                    {
                        int[] data = ((UBInt32Array)array).GetValue();
                        val = new float[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT64:
                    {
                        long[] data = ((UBInt64Array)array).GetValue();
                        val = new float[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.FLOAT32:
                    {
                        val = ((UBFloat32Array)array).GetValue();
                        break;
                    }
                case UBArrayType.FLOAT64:
                    {
                        double[] data = ((UBFloat64Array)array).GetValue();
                        val = new float[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (float)data[i];

                        break;
                    }
                default:
                    throw new Exception("not an int32[] type");
            }

            return val;
        }
        /// <summary>
        /// Returns the wrapper as a <c>double[]</c>.
        /// </summary>
        public double[] AsDoubleArray()
        {
            double[] val;
            UBArray array = AsArray();

            switch (array.GetArrayType)
            {
                case UBArrayType.INT8:
                    {
                        byte[] data = ((UBInt8Array)array).GetValue();
                        val = new double[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT16:
                    {
                        short[] data = ((UBInt16Array)array).GetValue();
                        val = new double[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT32:
                    {
                        int[] data = ((UBInt32Array)array).GetValue();
                        val = new double[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.INT64:
                    {
                        long[] data = ((UBInt64Array)array).GetValue();
                        val = new double[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = data[i];

                        break;
                    }
                case UBArrayType.FLOAT32:
                    {
                        float[] data = ((UBFloat32Array)array).GetValue();
                        val = new double[data.Length];

                        for (int i = 0; i < data.Length; i++)
                            val[i] = (long)data[i];

                        break;
                    }
                case UBArrayType.FLOAT64:
                    {
                        val = ((UBFloat64Array)array).GetValue();
                        break;
                    }
                default:
                    throw new Exception("not an int32[] type");
            }

            return val;
        }
        /// <summary>
        /// Returns the wrapper as a <c>string[]</c>.
        /// </summary>
        public string[] AsStringArray()
        {
            UBArray array = AsArray();

            if (UBArrayType.STRING != array.GetArrayType)
                throw new Exception("Not a strongly-typed string array");

            return ((UBStringArray)array).GetValue();
        }
        /// <summary>
        /// Returns the wrapper as a <c>UBObject</c>.
        /// </summary>
        public UBObject AsObject() => ((UBObject)this);

        public virtual int CompareTo([AllowNull] UBValue other)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A generic version of <see cref="UBValue"/>.
    /// 
    /// <para>Used for easier creation of <c>UBJ</c> types.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class UBValue<T> : UBValue
    {
        protected T value;

        protected UBValue(T value)
        {
            this.value = value;
            this.objValue = value;
        }

        /// <summary>
        /// Returns the wrappers' internal value.
        /// </summary>
        public abstract T GetValue();
    }
}
