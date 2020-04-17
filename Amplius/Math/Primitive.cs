#nullable enable

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

namespace Amplius.Math
{
    public struct Primitive
    {
        private readonly object data;

        private Primitive(object data) => this.data = data;

        public override bool Equals(object? obj)
        {
            if (obj is Primitive)
            {
                var prim = ((Primitive)obj);

                return data.GetType() == prim.data.GetType() && data == prim.data;
            }

            return false;
        }
        public override int GetHashCode() => data.GetHashCode();
        public override string? ToString() => data.ToString();

        public static explicit operator byte?(Primitive primitive) => (byte)primitive.data;
        public static explicit operator sbyte?(Primitive primitive) => (sbyte)primitive.data;
        public static explicit operator short?(Primitive primitive) => (short)primitive.data;
        public static implicit operator int?(Primitive primitive) => (int)primitive.data;
        public static explicit operator long?(Primitive primitive) => (long)primitive.data;
        public static explicit operator ushort?(Primitive primitive) => (ushort)primitive.data;
        public static explicit operator uint?(Primitive primitive) => (uint)primitive.data;
        public static explicit operator ulong?(Primitive primitive) => (ulong)primitive.data;
        public static explicit operator float?(Primitive primitive) => (float)primitive.data;
        public static explicit operator double?(Primitive primitive) => (double)primitive.data;
        public static explicit operator decimal?(Primitive primitive) => (decimal)primitive.data;
        public static explicit operator char?(Primitive primitive) => (char)primitive.data;
        public static explicit operator bool?(Primitive primitive) => (bool)primitive.data;

        public static explicit operator byte(Primitive primitive) => (byte)primitive.data;
        public static explicit operator sbyte(Primitive primitive) => (sbyte)primitive.data;
        public static explicit operator short(Primitive primitive) => (short)primitive.data;
        public static implicit operator int(Primitive primitive) => (int)primitive.data;
        public static explicit operator long(Primitive primitive) => (long)primitive.data;
        public static explicit operator ushort(Primitive primitive) => (ushort)primitive.data;
        public static explicit operator uint(Primitive primitive) => (uint)primitive.data;
        public static explicit operator ulong(Primitive primitive) => (ulong)primitive.data;
        public static explicit operator float(Primitive primitive) => (float)primitive.data;
        public static explicit operator double(Primitive primitive) => (double)primitive.data;
        public static explicit operator decimal(Primitive primitive) => (decimal)primitive.data;
        public static explicit operator string(Primitive primitive) => (string)primitive.data;
        public static explicit operator char(Primitive primitive) => (char)primitive.data;
        public static explicit operator bool(Primitive primitive) => (bool)primitive.data;

        public static implicit operator Primitive(byte obj) => new Primitive(obj);
        public static implicit operator Primitive(sbyte obj) => new Primitive(obj);
        public static implicit operator Primitive(short obj) => new Primitive(obj);
        public static implicit operator Primitive(int obj) => new Primitive(obj);
        public static implicit operator Primitive(long obj) => new Primitive(obj);
        public static implicit operator Primitive(ushort obj) => new Primitive(obj);
        public static implicit operator Primitive(uint obj) => new Primitive(obj);
        public static implicit operator Primitive(ulong obj) => new Primitive(obj);
        public static implicit operator Primitive(float obj) => new Primitive(obj);
        public static implicit operator Primitive(double obj) => new Primitive(obj);
        public static implicit operator Primitive(decimal obj) => new Primitive(obj);
        public static implicit operator Primitive(string obj) => new Primitive(obj);
        public static implicit operator Primitive(char obj) => new Primitive(obj);
        public static implicit operator Primitive(bool obj) => new Primitive(obj);
    }
}
