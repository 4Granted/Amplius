using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

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
    /// <summary>
    /// Reads objects and values as <c>UBValue</c>'s from a provided stream.
    /// </summary>
    public sealed class UBReader : IDisposable
    {
        private readonly Stream stream;

        public UBReader(Stream stream) => this.stream = stream;

        /// <summary>
        /// Disposes the current <c>UBReader</c>.
        /// </summary>
        public void Dispose() => stream.Dispose();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fs"></param>
        public void ReadData(FileStream fs)
        {
            long length = ReadInt(ReadControl());
            long bytesLeft = length;

            byte[] buffer = new byte[4096];

            while (bytesLeft > 0)
            {
                int bytesRead = stream.Read(buffer, 0, (int)Math.Min(buffer.Length, bytesLeft));

                if (bytesRead < 0)
                    throw new Exception("Stream was too short to read from.");

                fs.Write(buffer, 0, bytesRead);
                bytesLeft -= bytesRead;
            }
        }
        /// <summary>
        /// Reads the next value from the stream.
        /// </summary>
        /// <returns></returns>
        public UBValue Read() => ReadValue(ReadControl());
        /// <summary>
        /// Reads all available values from the stream.
        /// </summary>
        /// <returns></returns>
        public List<UBValue> ReadAll()
        {
            List<UBValue> list = new List<UBValue>();
            bool run = true;

            while (run)
            {
                int c = ReadControlByte();
                // If the read byte is -1 (Out of bounds for the type of byte), then we must have reached the EoF.
                run = !(c == -1);

                list.Add(ReadValue((byte)c));
            }

            // Remove the file terminator
            if (list.Count > 0)
                list.RemoveAt(list.Count - 1);

            return list;
        }
        /// <summary>
        /// Reads the next values from the stream as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Expected type</typeparam>
        /// <returns></returns>
        public T ReadAs<T>() where T : class => Read().ObjValue as T;
        /// <summary>
        /// Reads a object of type <typeparamref name="T"/> from the stream.
        /// </summary>
        public T ReadObjectAs<T>()
        {
            T obj = Activator.CreateInstance<T>();

            var fields = obj?.GetType().GetFields().Where(f => f.GetCustomAttribute<UBFieldAttribute>() != null);

            if (!fields.Any())
                throw new Exception("No fields contain the UBField attribute; cannot read fields.");

            UBObject ubobj = Read().AsObject();

            foreach (var field in fields)
            {
                var val = ubobj.Get(field.Name);

                field.SetValue(obj, val is UBString ? Encoding.UTF8.GetString((byte[])val.ObjValue) : val.ObjValue);
            }

            return obj;
        }
#nullable enable
        public UBObject? ReadDefaultObject()
        {
            var val = Read()?.AsObject();

            return val;
        }
#nullable restore

        #region Read Controls
        private byte ReadControl()
        {
            int value = stream.ReadByte();

            if (value == -1)
                return 0;

            return (byte)value;
        }
        private int ReadControlByte() => stream.ReadByte();
        #endregion

        #region Read
        private byte ReadInt8() => ReadControl();
        private short ReadUInt8() => (short)(0xFF & ReadControl());
        private short ReadInt16() => (short)((ReadControl() & 0xFF) << 8 | (ReadControl() & 0xFF));
        private int ReadInt32() => ((ReadControl() & 0xFF) << 24 | (ReadControl() & 0xFF) << 16 | (ReadControl() & 0xFF) << 8 | (ReadControl() & 0xFF));
        private long ReadInt64() => ((long)ReadControl() & 0xFF) << 56 | ((long)ReadControl() & 0xFF) << 48
                                    | ((long)ReadControl() & 0xFF) << 40 | ((long)ReadControl() & 0xFF) << 32
                                    | ((long)ReadControl() & 0xFF) << 24 | ((long)ReadControl() & 0xFF) << 16
                                    | ((long)ReadControl() & 0xFF) << 8 | ((long)ReadControl() & 0xFF);
        private long ReadInt(byte control)
        {
            var value = control switch
            {
                UBValue.INT8_MARKER => ReadInt8(),
                UBValue.UINT8_MARKER => ReadUInt8(),
                UBValue.INT16_MARKER => ReadInt16(),
                UBValue.INT32_MARKER => ReadInt32(),
                UBValue.INT64_MARKER => ReadInt64(),
                _ => throw new Exception("The control is not of the types: (INT8, UINT8, INT16, INT32, INT64)."),
            };

            return value;
        }
        private float ReadFloat32()
        {
            int iValue = (ReadControl() & 0xFF) << 24 | (ReadControl() & 0xFF) << 16 | (ReadControl() & 0xFF) << 8 | (ReadControl() & 0xFF);

            return iValue.BitsToFloat();
        }
        private double ReadFloat64()
        {
            long iValue = ((long)ReadControl() & 0xFF) << 56 | ((long)ReadControl() & 0xFF) << 48
                | ((long)ReadControl() & 0xFF) << 40 | ((long)ReadControl() & 0xFF) << 32
                | ((long)ReadControl() & 0xFF) << 24 | ((long)ReadControl() & 0xFF) << 16
                | ((long)ReadControl() & 0xFF) << 8 | ((long)ReadControl() & 0xFF);

            return iValue.BitsToDouble();
        }

        private char ReadChar() => (char)ReadControl();
        private byte[] ReadString(byte control)
        {
            int size = (int)ReadInt(control);
            byte[] value = new byte[size];

            int bytesLeft = size;
            int offset = 0;

            while (bytesLeft > 0)
            {
                int bytesRead = stream.Read(value, offset, size);

                if (bytesRead < 0)
                    throw new Exception("EoF reached");
                else
                {
                    bytesLeft -= bytesRead;
                    offset += bytesRead;
                }
            }

            return value;
        }
        #endregion

        #region Read Optimized Arrays
        private byte[] ReadOptimizedArrayInt8(int size)
        {
            byte[] data = new byte[size];

            for (int i = 0; i < size; i++)
                data[i] = ReadInt8();

            return data;
        }
        private short[] ReadOptimizedArrayInt16(int size)
        {
            short[] data = new short[size];

            for (int i = 0; i < size; i++)
                data[i] = ReadInt16();

            return data;
        }
        private int[] ReadOptimizedArrayInt32(int size)
        {
            int[] data = new int[size];

            for (int i = 0; i < size; i++)
                data[i] = ReadInt32();

            return data;
        }
        private long[] ReadOptimizedArrayInt64(int size)
        {
            long[] data = new long[size];

            for (int i = 0; i < size; i++)
                data[i] = ReadInt64();

            return data;
        }
        private float[] ReadOptimizedArrayFloat32(int size)
        {
            float[] data = new float[size];

            for (int i = 0; i < size; i++)
                data[i] = ReadFloat32();

            return data;
        }
        private double[] ReadOptimizedArrayFloat64(int size)
        {
            double[] data = new double[size];

            for (int i = 0; i < size; i++)
                data[i] = ReadFloat64();

            return data;
        }
        private string[] ReadOptimizedArrayString(int size)
        {
            string[] data = new string[size];

            for (int i = 0; i < size; i++)
                data[i] = UBString.UTF8.GetString(ReadString(ReadControl()));

            return data;
        }
        private UBValue[] ReadOptimizedArray(int size, byte type)
        {
            UBValue[] val = new UBValue[size];

            for (int i = 0; i < size; i++)
                val[i] = ReadValue(type);

            return val;
        }
        private UBValue[] ReadOptimizedArray(int size)
        {
            UBValue[] retval = new UBValue[size];

            for (int i = 0; i < size; i++)
            {
                byte type = ReadControl();
                retval[i] = ReadValue(type);
            }

            return retval;
        }
        private UBArray ReadArray()
        {
            byte control, type;
            int size;

            control = ReadControl();

            if (control == UBValue.OPTIMIZED_TYPE_MARKER)
            {
                type = ReadControl();

                if (ReadControl() != UBValue.OPTIMIZED_SIZE_MARKER)
                    throw new IOException("Optimized size missing");

                size = (int)ReadInt(ReadControl());

                return type switch
                {
                    UBValue.INT8_MARKER => UBValue.CreateArray(ReadOptimizedArrayInt8(size)) as UBArray,
                    UBValue.INT16_MARKER => UBValue.CreateArray(ReadOptimizedArrayInt16(size)) as UBArray,
                    UBValue.INT32_MARKER => UBValue.CreateArray(ReadOptimizedArrayInt32(size)) as UBArray,
                    UBValue.INT64_MARKER => UBValue.CreateArray(ReadOptimizedArrayInt64(size)) as UBArray,
                    UBValue.FLOAT32_MARKER => UBValue.CreateArray(ReadOptimizedArrayFloat32(size)) as UBArray,
                    UBValue.FLOAT64_MARKER => UBValue.CreateArray(ReadOptimizedArrayFloat64(size)) as UBArray,
                    UBValue.STRING_MARKER => UBValue.CreateArray(ReadOptimizedArrayString(size)) as UBArray,
                    _ => UBValue.CreateArray(ReadOptimizedArray(size, type)),
                };
            }
            else if (control == UBValue.OPTIMIZED_SIZE_MARKER)
            {
                size = (int)ReadInt(ReadControl());
                return UBValue.CreateArray(ReadOptimizedArray(size));
            }
            else
            {
                List<UBValue> data = new List<UBValue>();

                while (control != UBValue.ARRAY_END_MARKER)
                {
                    data.Add(ReadValue(control));
                    control = ReadControl();
                }

                return UBValue.CreateArray(data.ToArray());
            }
        }
        #endregion

        #region Read Other
        private UBValue ReadObject()
        {
            byte control, type;
            int size;

            SortedDictionary<string, UBValue> obj = new SortedDictionary<string, UBValue>();

            control = ReadControl();
            if (control == UBValue.OPTIMIZED_TYPE_MARKER)
            {
                type = ReadControl();

                if (ReadControl() != UBValue.OPTIMIZED_SIZE_MARKER)
                    throw new IOException("optimized size missing");

                size = (int)ReadInt(ReadControl());

                for (int i = 0; i < size; i++)
                {
                    string key = UBString.UTF8.GetString(ReadString(ReadControl()));
                    UBValue value = ReadValue(type);

                    obj.Add(key, value);
                }
            }
            else if (control == UBValue.OPTIMIZED_SIZE_MARKER)
            {
                size = (int)ReadInt(ReadControl());

                for (int i = 0; i < size; i++)
                {
                    string key = UBString.UTF8.GetString(ReadString(ReadControl()));
                    UBValue value = ReadValue(ReadControl());

                    obj.Add(key, value);
                }
            }
            else
            {
                while (control != UBValue.OBJ_END_MARKER)
                {
                    string key = UBString.UTF8.GetString(ReadString(control));
                    control = ReadControl();
                    UBValue value = ReadValue(control);

                    obj.Add(key, value);

                    control = ReadControl();
                }
            }

            return UBValue.CreateObject(obj);
        }
        private UBValue ReadValue(byte control)
        {
            UBValue val = null;
            switch (control)
            {
                case UBValue.NULL_MARKER:
                    val = UBValue.CreateNull();
                    break;
                case UBValue.TRUE_MARKER:
                    val = UBValue.CreateBool(true);
                    break;
                case UBValue.FALSE_MARKER:
                    val = UBValue.CreateBool(false);
                    break;
                case UBValue.CHAR_MARKER:
                    val = UBValue.CreateChar(ReadChar());
                    break;
                case UBValue.INT8_MARKER:
                case UBValue.UINT8_MARKER:
                case UBValue.INT16_MARKER:
                case UBValue.INT32_MARKER:
                case UBValue.INT64_MARKER:
                    val = UBValue.CreateIntAuto(ReadInt(control));
                    break;
                case UBValue.FLOAT32_MARKER:
                    val = UBValue.CreateFloat(ReadFloat32());
                    break;
                case UBValue.FLOAT64_MARKER:
                    val = UBValue.CreateDouble(ReadFloat64());
                    break;
                case UBValue.STRING_MARKER:
                    val = UBValue.CreateString(ReadString(ReadControl()));
                    break;
                case UBValue.ARRAY_START_MARKER:
                    val = ReadArray();
                    break;
                case UBValue.OBJ_START_MARKER:
                    val = ReadObject();
                    break;
            }

            return val;
        }
        #endregion
    }
}
