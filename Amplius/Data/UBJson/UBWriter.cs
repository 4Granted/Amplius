using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Amplius.Data.UBJson
{
    /// <summary>
    /// Writes objects and values as <c>UBValue</c>'s to a provided stream.
    /// </summary>
    public sealed class UBWriter : IDisposable
    {
        private readonly Stream stream;

        public UBWriter(Stream stream) => this.stream = stream;

        /// <summary>
        /// Disposes the current <c>UBWriter</c>.
        /// </summary>
        public void Dispose() => stream?.Dispose();

        /// <summary>
        /// Writes the given <paramref name="value"/> to the stream.
        /// </summary>
        /// <param name="value"></param>
        public void Write(UBValue value)
        {
            switch (value.GetUBType)
            {
                case UBType.NULL:
                    WriteNull();
                    break;
                case UBType.CHAR:
                    WriteChar(value.AsChar());
                    break;
                case UBType.BOOL:
                    WriteBool(value.AsBool());
                    break;
                case UBType.INT8:
                    WriteInt8((byte)value.AsInt());
                    break;
                case UBType.UINT8:
                    WriteUInt8((short)value.AsInt());
                    break;
                case UBType.INT16:
                    WriteInt16((short)value.AsInt());
                    break;
                case UBType.INT32:
                    WriteInt32(value.AsInt());
                    break;
                case UBType.INT64:
                    WriteInt64(value.AsLong());
                    break;
                case UBType.FLOAT32:
                    WriteFloat32(value.AsFloat32());
                    break;
                case UBType.FLOAT64:
                    WriteFloat64(value.AsFloat64());
                    break;
                case UBType.STRING:
                    WriteString((UBString)value);
                    break;
                case UBType.ARRAY:
                    WriteArray(value.AsArray());
                    break;
                case UBType.OBJECT:
                    WriteObject(value.AsObject());
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Writes the given <paramref name="values"/> to the stream.
        /// </summary>
        /// <param name="values"></param>
        public void Write(params UBValue[] values)
        {
            foreach (var value in values)
                Write(value);
        }
        /// <summary>
        /// Writes a <c><see cref="IUBSerializable"/></c> object to the stream.
        /// </summary>
        /// <param name="serializable">Object to serialize</param>
        public void Write(IUBSerializable serializable, UBObject provider = null) => Write(serializable.Write(provider == null ? UBValue.CreateObject() : provider));
        /// <summary>
        /// Writes a <c><see cref="UBSerializable"/></c> object to the stream.
        /// </summary>
        /// <param name="serializable"></param>
        public void Write(UBSerializable serializable) => Write(serializable.Write());
        /// <summary>
        /// Writes the <paramref name="obj"/> as a <c>UBObject</c> to the stream.
        /// </summary>
        /// <param name="obj">Obj to write to the stream</param>
        public void Write(object obj)
        {
            var fields = obj?.GetType().GetFields().Where(f => f.GetCustomAttribute<UBFieldAttribute>() != null);

            if (!fields.Any())
                throw new Exception("No fields contain the UBField attribute; cannot write fields.");

            UBObject ubobj = UBValue.CreateObject();

            foreach (var field in fields)
            {
                var fieldKey = field.GetCustomAttribute<UBFieldAttribute>().Key;

                ubobj.Set(fieldKey != "" ? fieldKey : field.Name, UBValue.CreateValue(field.GetValue(obj)));
            }

            Write(ubobj);
        }
        /// <summary>
        /// Writes the given <paramref name="value"/> to the stream asynchronously.
        /// </summary>
        /// <param name="value"></param>
        public async Task WriteAsync(UBValue value)
        {
            switch (value.GetUBType)
            {
                case UBType.NULL:
                    await WriteNullAsync();
                    break;
                case UBType.CHAR:
                    await WriteCharAsync(value.AsChar());
                    break;
                case UBType.BOOL:
                    await WriteBoolAsync(value.AsBool());
                    break;
                case UBType.INT8:
                    await WriteInt8Async((byte)value.AsInt());
                    break;
                case UBType.UINT8:
                    await WriteUInt8Async((short)value.AsInt());
                    break;
                case UBType.INT16:
                    await WriteInt16Async((short)value.AsInt());
                    break;
                case UBType.INT32:
                    await WriteInt32Async(value.AsInt());
                    break;
                case UBType.INT64:
                    await WriteInt64Async(value.AsLong());
                    break;
                case UBType.FLOAT32:
                    await WriteFloat32Async(value.AsFloat32());
                    break;
                case UBType.FLOAT64:
                    await WriteFloat64Async(value.AsFloat64());
                    break;
                case UBType.STRING:
                    await WriteStringAsync((UBString)value);
                    break;
                case UBType.ARRAY:
                    await WriteArrayAsync(value.AsArray());
                    break;
                case UBType.OBJECT:
                    await WriteObjectAsync(value.AsObject());
                    break;
                default:
                    break;
            }
        }

        #region Write
        internal void WriteNull() => stream.WriteByte(UBValue.NULL_MARKER);
        internal void WriteBool(bool value) => stream.WriteByte(value ? UBValue.TRUE_MARKER : UBValue.FALSE_MARKER);
        internal void WriteChar(char value)
        {
            stream.WriteByte(UBValue.CHAR_MARKER);
            stream.WriteByte((byte)value);
        }
        internal void WriteInt8(byte value)
        {
            stream.WriteByte(UBValue.INT8_MARKER);
            WriteRawInt8(value);
        }
        internal void WriteUInt8(short value)
        {
            stream.WriteByte(UBValue.UINT8_MARKER);
            WriteRawUInt8(value);
        }
        internal void WriteInt16(short value)
        {
            stream.WriteByte(UBValue.INT16_MARKER);
            WriteRawInt16(value);
        }
        internal void WriteInt32(int value)
        {
            stream.WriteByte(UBValue.INT32_MARKER);
            WriteRawInt32(value);
        }
        internal void WriteInt64(long value)
        {
            stream.WriteByte(UBValue.INT64_MARKER);
            WriteRawInt64(value);
        }
        internal void WriteInt(long value)
        {
            if (UBValue.InRange(value, 0, 255))
                WriteUInt8((byte)value);
            else if (UBValue.InRange(value, -128, 127))
                WriteInt8((byte)value);
            else if (UBValue.InRange(value, -32768, 32767))
                WriteInt16((short)value);
            else if (UBValue.InRange(value, -2147483648, 2147483647))
                WriteInt32((int)value);
            else
                WriteInt64(value);
        }
        internal void WriteFloat32(float value)
        {
            stream.WriteByte(UBValue.FLOAT32_MARKER);
            WriteRawFloat32(value);
        }
        internal void WriteFloat64(double value)
        {
            stream.WriteByte(UBValue.FLOAT64_MARKER);
            WriteRawFloat64(value);
        }
        internal void WriteString(UBString str)
        {
            stream.WriteByte(UBValue.STRING_MARKER);
            byte[] data = str.GetValue();
            WriteData(data);
        }
        internal void WriteObject(UBObject obj)
        {
            stream.WriteByte(UBValue.OBJ_START_MARKER);

            foreach (var pair in obj.Pairs())
            {
                WriteData(UBString.UTF8.GetBytes(pair.Key));
                Write(pair.Value);
            }

            stream.WriteByte(UBValue.OBJ_END_MARKER);
        }

        internal async Task WriteNullAsync()
        {
            byte[] buffer = new byte[] { UBValue.NULL_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        internal async Task WriteBoolAsync(bool value)
        {
            byte[] buffer = new byte[] { value ? UBValue.TRUE_MARKER : UBValue.FALSE_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        internal async Task WriteCharAsync(char value)
        {
            byte[] buffer = new byte[] { UBValue.CHAR_MARKER, (byte)value };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        internal async Task WriteInt8Async(byte value)
        {
            byte[] buffer = new byte[] { UBValue.INT8_MARKER, value };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        internal async Task WriteUInt8Async(short value)
        {
            byte[] buffer = new byte[] { UBValue.UINT8_MARKER, (byte)(0xFF & value) };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        internal async Task WriteInt16Async(short value)
        {
            byte[] buffer = new byte[] { UBValue.INT16_MARKER, (byte)(value >> 8), (byte)value };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        internal async Task WriteInt32Async(int value)
        {
            byte[] buffer = new byte[] { UBValue.INT32_MARKER, (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value };
            await stream.WriteAsync(buffer, 0, buffer.Length); ;
        }
        internal async Task WriteInt64Async(long value)
        {
            byte[] buffer = new byte[] { UBValue.INT64_MARKER, (byte)(0xff & ((value >> 56))), (byte)(0xff & ((value >> 48))), (byte)(0xff & ((value >> 40))), (byte)(0xff & ((value >> 32))), (byte)(0xff & ((value >> 24))), (byte)(0xff & ((value >> 16))), (byte)(0xff & ((value >> 8))), (byte)(0xff & value) };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        internal async Task WriteIntAsync(long value)
        {
            if (UBValue.InRange(value, 0, 255))
                await WriteUInt8Async((byte)value);
            else if (UBValue.InRange(value, -128, 127))
                await WriteInt8Async((byte)value);
            else if (UBValue.InRange(value, -32768, 32767))
                await WriteInt16Async((short)value);
            else if (UBValue.InRange(value, -2147483648, 2147483647))
                await WriteInt32Async((int)value);
            else
                await WriteInt64Async(value);
        }
        internal async Task WriteFloat32Async(float value)
        {
            byte[] buffer = new byte[] { UBValue.FLOAT32_MARKER, (byte)(value.BitsToInt() >> 24), (byte)(value.BitsToInt() >> 16), (byte)(value.BitsToInt() >> 8), (byte)value };
            await stream.WriteAsync(buffer, 0, buffer.Length); ;
        }
        internal async Task WriteFloat64Async(double value)
        {
            byte[] buffer = new byte[] { UBValue.INT64_MARKER, (byte)(0xff & ((value.BitsToLong() >> 56))), (byte)(0xff & ((value.BitsToLong() >> 48))), (byte)(0xff & ((value.BitsToLong() >> 40))), (byte)(0xff & ((value.BitsToLong() >> 32))), (byte)(0xff & ((value.BitsToLong() >> 24))), (byte)(0xff & ((value.BitsToLong() >> 16))), (byte)(0xff & ((value.BitsToLong() >> 8))), (byte)(0xff & value.BitsToLong()) };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        internal async Task WriteStringAsync(UBString str)
        {
            byte[] buffer = new byte[] { UBValue.STRING_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);
            byte[] data = str.GetValue();
            await WriteDataAsync(data);
        }
        internal async Task WriteObjectAsync(UBObject obj)
        {
            stream.WriteByte(UBValue.OBJ_START_MARKER);

            foreach (var pair in obj.Pairs())
            {
                await WriteDataAsync(UBString.UTF8.GetBytes(pair.Key));
                await WriteAsync(pair.Value);
            }

            stream.WriteByte(UBValue.OBJ_END_MARKER);
        }
        #endregion

        #region Write Array
        internal void WriteInt8Array(byte[] value)
        {
            stream.WriteByte(UBValue.ARRAY_START_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_TYPE_MARKER);
            stream.WriteByte(UBValue.INT8_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_SIZE_MARKER);
            WriteInt(value.Length);

            for (int i = 0; i < value.Length; i++)
                WriteRawInt8(value[i]);
        }
        internal void WriteInt16Array(short[] value)
        {
            stream.WriteByte(UBValue.ARRAY_START_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_TYPE_MARKER);
            stream.WriteByte(UBValue.INT16_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_SIZE_MARKER);
            WriteInt(value.Length);

            for (int i = 0; i < value.Length; i++)
                WriteRawInt16(value[i]);
        }
        internal void WriteInt32Array(int[] value)
        {
            stream.WriteByte(UBValue.ARRAY_START_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_TYPE_MARKER);
            stream.WriteByte(UBValue.INT32_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_SIZE_MARKER);
            WriteInt(value.Length);

            for (int i = 0; i < value.Length; i++)
                WriteRawInt32(value[i]);
        }
        internal void WriteInt64Array(long[] value)
        {
            stream.WriteByte(UBValue.ARRAY_START_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_TYPE_MARKER);
            stream.WriteByte(UBValue.INT64_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_SIZE_MARKER);
            WriteInt(value.Length);

            for (int i = 0; i < value.Length; i++)
                WriteRawInt64(value[i]);
        }
        internal void WriteFloat32Array(float[] value)
        {
            stream.WriteByte(UBValue.ARRAY_START_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_TYPE_MARKER);
            stream.WriteByte(UBValue.FLOAT32_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_SIZE_MARKER);
            WriteInt(value.Length);

            for (int i = 0; i < value.Length; i++)
                WriteRawFloat32(value[i]);
        }
        internal void WriteFloat64Array(double[] value)
        {
            stream.WriteByte(UBValue.ARRAY_START_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_TYPE_MARKER);
            stream.WriteByte(UBValue.FLOAT64_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_SIZE_MARKER);
            WriteInt(value.Length);

            for (int i = 0; i < value.Length; i++)
                WriteRawFloat64(value[i]);
        }
        internal void WriteStringArray(String[] value)
        {
            stream.WriteByte(UBValue.ARRAY_START_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_TYPE_MARKER);
            stream.WriteByte(UBValue.STRING_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_SIZE_MARKER);
            WriteInt(value.Length);

            for (int i = 0; i < value.Length; i++)
            {
                string str = value[i];

                if (str == null)
                    throw new IOException("cannot serialize null string in strongly-typed array");

                WriteData(UBString.UTF8.GetBytes(str));
            }
        }
        internal void WriteGenericeArray(UBArray value)
        {
            stream.WriteByte(UBValue.ARRAY_START_MARKER);
            stream.WriteByte(UBValue.OPTIMIZED_SIZE_MARKER);

            int size = value.Length;
            WriteInt(size);

            for (int i = 0; i < size; i++)
                Write(value.Get(i));
        }
        internal void WriteArray(UBArray value)
        {
            switch (value.GetArrayType)
            {
                case UBArrayType.INT8:
                    WriteInt8Array(((UBInt8Array)value).GetValue());
                    break;
                case UBArrayType.INT16:
                    WriteInt16Array(((UBInt16Array)value).GetValue());
                    break;
                case UBArrayType.INT32:
                    WriteInt32Array(((UBInt32Array)value).GetValue());
                    break;
                case UBArrayType.INT64:
                    WriteInt64Array(((UBInt64Array)value).GetValue());
                    break;
                case UBArrayType.FLOAT32:
                    WriteFloat32Array(((UBFloat32Array)value).GetValue());
                    break;
                case UBArrayType.FLOAT64:
                    WriteFloat64Array(((UBFloat64Array)value).GetValue());
                    break;
                case UBArrayType.STRING:
                    WriteStringArray(((UBStringArray)value).GetValue());
                    break;
                default:
                    WriteGenericeArray(value);
                    break;

            }
        }

        internal async Task WriteInt8ArrayAsync(byte[] value)
        {
            byte[] buffer = new byte[] { UBValue.ARRAY_START_MARKER, UBValue.OPTIMIZED_TYPE_MARKER, UBValue.INT8_MARKER, UBValue.OPTIMIZED_SIZE_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);
            await WriteIntAsync(value.Length);

            for (int i = 0; i < value.Length; i++)
                await WriteRawInt8Async(value[i]);
        }
        internal async Task WriteInt16ArrayAsync(short[] value)
        {
            byte[] buffer = new byte[] { UBValue.ARRAY_START_MARKER, UBValue.OPTIMIZED_TYPE_MARKER, UBValue.INT16_MARKER, UBValue.OPTIMIZED_SIZE_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);
            await WriteIntAsync(value.Length);

            for (int i = 0; i < value.Length; i++)
                await WriteRawInt16Async(value[i]);
        }
        internal async Task WriteInt32ArrayAsync(int[] value)
        {
            byte[] buffer = new byte[] { UBValue.ARRAY_START_MARKER, UBValue.OPTIMIZED_TYPE_MARKER, UBValue.INT32_MARKER, UBValue.OPTIMIZED_SIZE_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);
            await WriteIntAsync(value.Length);

            for (int i = 0; i < value.Length; i++)
                await WriteRawInt32Async(value[i]);
        }
        internal async Task WriteInt64ArrayAsync(long[] value)
        {
            byte[] buffer = new byte[] { UBValue.ARRAY_START_MARKER, UBValue.OPTIMIZED_TYPE_MARKER, UBValue.INT64_MARKER, UBValue.OPTIMIZED_SIZE_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);
            await WriteIntAsync(value.Length);

            for (int i = 0; i < value.Length; i++)
                await WriteRawInt64Async(value[i]);
        }
        internal async Task WriteFloat32ArrayAsync(float[] value)
        {
            byte[] buffer = new byte[] { UBValue.ARRAY_START_MARKER, UBValue.OPTIMIZED_TYPE_MARKER, UBValue.FLOAT32_MARKER, UBValue.OPTIMIZED_SIZE_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);
            await WriteIntAsync(value.Length);

            for (int i = 0; i < value.Length; i++)
                await WriteRawFloat32Async(value[i]);
        }
        internal async Task WriteFloat64ArrayAsync(double[] value)
        {
            byte[] buffer = new byte[] { UBValue.ARRAY_START_MARKER, UBValue.OPTIMIZED_TYPE_MARKER, UBValue.FLOAT64_MARKER, UBValue.OPTIMIZED_SIZE_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);
            await WriteIntAsync(value.Length);

            for (int i = 0; i < value.Length; i++)
                await WriteRawFloat64Async(value[i]);
        }
        internal async Task WriteStringArrayAsync(String[] value)
        {
            byte[] buffer = new byte[] { UBValue.ARRAY_START_MARKER, UBValue.OPTIMIZED_TYPE_MARKER, UBValue.STRING_MARKER, UBValue.OPTIMIZED_SIZE_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);
            await WriteIntAsync(value.Length);

            for (int i = 0; i < value.Length; i++)
            {
                string str = value[i];

                if (str == null)
                    throw new IOException("cannot serialize null string in strongly-typed array");

                await WriteDataAsync(UBString.UTF8.GetBytes(str));
            }
        }
        internal async Task WriteGenericeArrayAsync(UBArray value)
        {
            byte[] buffer = new byte[] { UBValue.ARRAY_START_MARKER, UBValue.OPTIMIZED_SIZE_MARKER };
            await stream.WriteAsync(buffer, 0, buffer.Length);

            int size = value.Length;
            await WriteIntAsync(size);

            for (int i = 0; i < size; i++)
                await WriteAsync(value.Get(i));
        }
        internal async Task WriteArrayAsync(UBArray value)
        {
            switch (value.GetArrayType)
            {
                case UBArrayType.INT8:
                    await WriteInt8ArrayAsync(((UBInt8Array)value).GetValue());
                    break;
                case UBArrayType.INT16:
                    await WriteInt16ArrayAsync(((UBInt16Array)value).GetValue());
                    break;
                case UBArrayType.INT32:
                    await WriteInt32ArrayAsync(((UBInt32Array)value).GetValue());
                    break;
                case UBArrayType.INT64:
                    await WriteInt64ArrayAsync(((UBInt64Array)value).GetValue());
                    break;
                case UBArrayType.FLOAT32:
                    await WriteFloat32ArrayAsync(((UBFloat32Array)value).GetValue());
                    break;
                case UBArrayType.FLOAT64:
                    await WriteFloat64ArrayAsync(((UBFloat64Array)value).GetValue());
                    break;
                case UBArrayType.STRING:
                    await WriteStringArrayAsync(((UBStringArray)value).GetValue());
                    break;
                default:
                    await WriteGenericeArrayAsync(value);
                    break;

            }
        }
        #endregion

        #region Write Data
        private void WriteData(byte[] data)
        {
            WriteInt(data.Length);
            stream.Write(data);
        }
        private void WriteData(long len, FileStream fs)
        {
            WriteInt(len);

            long bytesLeft = len;
            byte[]

            buf = new byte[4096];

            while (bytesLeft > 0)
            {
                int bytesRead = fs.Read(buf, 0, (int)Math.Min(buf.Length, bytesLeft));

                if (bytesRead < 0)
                    throw new IOException("input stream too short");

                stream.Write(buf, 0, bytesRead);
                bytesLeft -= bytesRead;
            }
        }

        private async Task WriteDataAsync(byte[] data)
        {
            await WriteIntAsync(data.Length);
            await stream.WriteAsync(data);
        }
        private async Task WriteDataAsync(long len, FileStream fs)
        {
            await WriteIntAsync(len);

            long bytesLeft = len;
            byte[]

            buf = new byte[4096];

            while (bytesLeft > 0)
            {
                int bytesRead = await fs.ReadAsync(buf, 0, (int)Math.Min(buf.Length, bytesLeft));

                if (bytesRead < 0)
                    throw new IOException("input stream too short");

                await stream.WriteAsync(buf, 0, bytesRead);
                bytesLeft -= bytesRead;
            }
        }
        #endregion

        #region Write Raw
        private void WriteRawInt8(byte value) => stream.WriteByte(value);
        private void WriteRawUInt8(short value) => stream.WriteByte((byte)(0xFF & value));
        private void WriteRawInt16(short value)
        {
            stream.WriteByte((byte)(value >> 8));
            stream.WriteByte((byte)value);
        }
        private void WriteRawInt32(int value)
        {
            stream.WriteByte((byte)(value >> 24));
            stream.WriteByte((byte)(value >> 16));
            stream.WriteByte((byte)(value >> 8));
            stream.WriteByte((byte)value);
        }
        private void WriteRawInt64(long value)
        {
            stream.WriteByte((byte)(0xff & ((value >> 56))));
            stream.WriteByte((byte)(0xff & ((value >> 48))));
            stream.WriteByte((byte)(0xff & ((value >> 40))));
            stream.WriteByte((byte)(0xff & ((value >> 32))));
            stream.WriteByte((byte)(0xff & ((value >> 24))));
            stream.WriteByte((byte)(0xff & ((value >> 16))));
            stream.WriteByte((byte)(0xff & ((value >> 8))));
            stream.WriteByte((byte)(0xff & value));
        }
        private void WriteRawFloat32(float value) => WriteRawInt32(value.BitsToInt());
        private void WriteRawFloat64(double value) => WriteRawInt64(value.BitsToLong());
        private void WriteRawString(byte[] data) => WriteData(data);

        private async Task WriteRawInt8Async(byte value)
        {
            byte[] buffer = new byte[] { UBValue.INT8_MARKER, value };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        private async Task WriteRawUInt8Async(short value)
        {
            byte[] buffer = new byte[] { UBValue.UINT8_MARKER, (byte)(0xFF & value) };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        private async Task WriteRawInt16Async(short value)
        {
            byte[] buffer = new byte[] { UBValue.INT16_MARKER, (byte)(value >> 8), (byte)value };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        private async Task WriteRawInt32Async(int value)
        {
            byte[] buffer = new byte[] { UBValue.INT32_MARKER, (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        private async Task WriteRawInt64Async(long value)
        {
            byte[] buffer = new byte[] { UBValue.INT64_MARKER, (byte)(0xff & ((value >> 56))), (byte)(0xff & ((value >> 48))), (byte)(0xff & ((value >> 40))), (byte)(0xff & ((value >> 32))), (byte)(0xff & ((value >> 24))), (byte)(0xff & ((value >> 16))), (byte)(0xff & ((value >> 8))), (byte)(0xff & value) };
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        private async Task WriteRawFloat32Async(float value) => await WriteRawInt32Async(value.BitsToInt());
        private async Task WriteRawFloat64Async(double value) => await WriteRawInt64Async(value.BitsToLong());
        #endregion
    }
}
