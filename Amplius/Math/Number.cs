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
    /// <summary>
    /// A struct which represents a unified storage of all numeric data types
    /// </summary>
    public struct Number
    {
        private readonly object data;

        private Number(object data) => this.data = data;

        public override bool Equals(object? obj)
        {
            if (obj is Number)
            {
                var numb = ((Number)obj);

                return data.GetType() == numb.data.GetType() && data == numb.data;
            }

            return false;
        }
        public override int GetHashCode() => data.GetHashCode();
        public override string? ToString() => data.ToString();

        public static Number operator +(Number a)
        {
            switch (a.data)
            {
                case byte _:
                    return +(byte)a.data;
                case sbyte _:
                    return +(sbyte)a.data;
                case short _:
                    return +(short)a.data;
                case int _:
                    return +(int)a.data;
                case long _:
                    return +(long)a.data;
                case float _:
                    return +(float)a.data;
                case double _:
                    return +(double)a.data;
                default:
                    return +(decimal)a.data;
            }
        }
        public static Number operator -(Number a)
        {
            switch (a.data)
            {
                case byte _:
                    return -(byte)a.data;
                case sbyte _:
                    return -(sbyte)a.data;
                case short _:
                    return -(short)a.data;
                case int _:
                    return -(int)a.data;
                case long _:
                    return -(long)a.data;
                case float _:
                    return -(float)a.data;
                case double _:
                    return -(double)a.data;
                default:
                    return -(decimal)a.data;
            }
        }
        public static Number operator ~(Number a)
        {
            switch (a.data)
            {
                case byte _:
                    return ~(byte)a.data;
                case sbyte _:
                    return ~(sbyte)a.data;
                case short _:
                    return ~(short)a.data;
                case int _:
                    return ~(int)a.data;
                case long _:
                case float _:
                case double _:
                    return ~(long)a.data;
                default:
                    return ~(long)a.data;
            }
        }
        public static Number operator ++(Number a)
        {
            switch (a.data)
            {
                case byte _:
                    var b = (byte)a.data;
                    return ++b;
                case sbyte _:
                    var sb = (byte)a.data;
                    return ++sb;
                case short _:
                    var s = (short)a.data;
                    return ++s;
                case int _:
                    var i = (int)a.data;
                    return ++i;
                case long _:
                    var l = (long)a.data;
                    return ++l;
                case float _:
                    var f = (float)a.data;
                    return ++f;
                case double _:
                    var d = (double)a.data;
                    return ++d;
                default:
                    var dc = (decimal)a.data;
                    return ++dc;
            }
        }
        public static Number operator --(Number a)
        {
            switch (a.data)
            {
                case byte _:
                    var b = (byte)a.data;
                    return --b;
                case sbyte _:
                    var sb = (byte)a.data;
                    return --sb;
                case short _:
                    var s = (short)a.data;
                    return --s;
                case int _:
                    var i = (int)a.data;
                    return --i;
                case long _:
                    var l = (long)a.data;
                    return --l;
                case float _:
                    var f = (float)a.data;
                    return --f;
                case double _:
                    var d = (double)a.data;
                    return --d;
                default:
                    var dc = (decimal)a.data;
                    return --dc;
            }
        }

        public static Number operator +(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data + (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data + (sbyte)b.data;
                case short _:
                    return (short)a.data + (short)b.data;
                case int _:
                    return (int)a.data + (int)b.data;
                case long _:
                    return (long)a.data + (long)b.data;
                case float _:
                    return (float)a.data + (float)b.data;
                case double _:
                    return (double)a.data + (double)b.data;
                default:
                    return (decimal)a.data + (decimal)b.data;
            }
        }
        public static Number operator -(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data - (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data - (sbyte)b.data;
                case short _:
                    return (short)a.data - (short)b.data;
                case int _:
                    return (int)a.data - (int)b.data;
                case long _:
                    return (long)a.data - (long)b.data;
                case float _:
                    return (float)a.data - (float)b.data;
                case double _:
                    return (double)a.data - (double)b.data;
                default:
                    return (decimal)a.data - (decimal)b.data;
            }
        }
        public static Number operator /(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data / (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data / (sbyte)b.data;
                case short _:
                    return (short)a.data / (short)b.data;
                case int _:
                    return (int)a.data / (int)b.data;
                case long _:
                    return (long)a.data / (long)b.data;
                case float _:
                    return (float)a.data / (float)b.data;
                case double _:
                    return (double)a.data / (double)b.data;
                default:
                    return (decimal)a.data / (decimal)b.data;
            }
        }
        public static Number operator *(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data * (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data * (sbyte)b.data;
                case short _:
                    return (short)a.data * (short)b.data;
                case int _:
                    return (int)a.data * (int)b.data;
                case long _:
                    return (long)a.data * (long)b.data;
                case float _:
                    return (float)a.data * (float)b.data;
                case double _:
                    return (double)a.data * (double)b.data;
                default:
                    return (decimal)a.data * (decimal)b.data;
            }
        }
        public static Number operator %(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data % (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data % (sbyte)b.data;
                case short _:
                    return (short)a.data % (short)b.data;
                case int _:
                    return (int)a.data % (int)b.data;
                case long _:
                    return (long)a.data % (long)b.data;
                case float _:
                    return (float)a.data % (float)b.data;
                case double _:
                    return (double)a.data % (double)b.data;
                default:
                    return (decimal)a.data % (decimal)b.data;
            }
        }
        public static Number operator |(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data | (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data | (sbyte)b.data;
                case short _:
                    return (short)a.data | (short)b.data;
                case int _:
                    return (int)a.data | (int)b.data;
                case long _:
                case float _:
                case double _:
                    return (long)a.data | (long)b.data;
                default:
                    return (long)a.data | (long)b.data;
            }
        }
        public static Number operator &(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data & (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data & (sbyte)b.data;
                case short _:
                    return (short)a.data & (short)b.data;
                case int _:
                    return (int)a.data & (int)b.data;
                case long _:
                case double _:
                    return (long)a.data & (long)b.data;
                default:
                    return (long)a.data & (long)b.data;
            }
        }
        public static Number operator ^(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data ^ (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data ^ (sbyte)b.data;
                case short _:
                    return (short)a.data ^ (short)b.data;
                case int _:
                    return (int)a.data ^ (int)b.data;
                case long _:
                case float _:
                case double _:
                    return (long)a.data ^ (long)b.data;
                default:
                    return (long)a.data ^ (long)b.data;
            }
        }
        public static Number operator <<(Number a, int b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data << b;
                case sbyte _:
                    return (sbyte)a.data << b;
                case short _:
                    return (short)a.data << b;
                case int _:
                    return (int)a.data << b;
                case long _:
                case float _:
                case double _:
                    return (long)a.data << b;
                default:
                    return (long)a.data << b;
            }
        }
        public static Number operator >>(Number a, int b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data >> b;
                case sbyte _:
                    return (sbyte)a.data >> b;
                case short _:
                    return (short)a.data >> b;
                case int _:
                    return (int)a.data >> b;
                case long _:
                case float _:
                case double _:
                    return (long)a.data >> b;
                default:
                    return (long)a.data >> b;
            }
        }

        public static bool operator >(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data > (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data > (sbyte)b.data;
                case short _:
                    return (short)a.data > (short)b.data;
                case int _:
                    return (int)a.data > (int)b.data;
                case long _:
                    return (long)a.data > (long)b.data;
                case float _:
                    return (float)a.data > (float)b.data;
                case double _:
                    return (double)a.data > (double)b.data;
                default:
                    return (decimal)a.data > (decimal)b.data;
            }
        }
        public static bool operator <(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data < (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data < (sbyte)b.data;
                case short _:
                    return (short)a.data < (short)b.data;
                case int _:
                    return (int)a.data < (int)b.data;
                case long _:
                    return (long)a.data < (long)b.data;
                case float _:
                    return (float)a.data < (float)b.data;
                case double _:
                    return (double)a.data < (double)b.data;
                default:
                    return (decimal)a.data < (decimal)b.data;
            }
        }

        public static bool operator >=(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data >= (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data >= (sbyte)b.data;
                case short _:
                    return (short)a.data >= (short)b.data;
                case int _:
                    return (int)a.data >= (int)b.data;
                case long _:
                    return (long)a.data >= (long)b.data;
                case float _:
                    return (float)a.data >= (float)b.data;
                case double _:
                    return (double)a.data >= (double)b.data;
                default:
                    return (decimal)a.data >= (decimal)b.data;
            }
        }
        public static bool operator <=(Number a, Number b)
        {
            switch (a.data)
            {
                case byte _:
                    return (byte)a.data <= (byte)b.data;
                case sbyte _:
                    return (sbyte)a.data <= (sbyte)b.data;
                case short _:
                    return (short)a.data <= (short)b.data;
                case int _:
                    return (int)a.data <= (int)b.data;
                case long _:
                    return (long)a.data <= (long)b.data;
                case float _:
                    return (float)a.data <= (float)b.data;
                case double _:
                    return (double)a.data <= (double)b.data;
                default:
                    return (decimal)a.data <= (decimal)b.data;
            }
        }

        public static bool operator ==(Number a, Number b) => a.data.GetType() == b.data.GetType() && a.data == b.data;
        public static bool operator !=(Number a, Number b) => a.data.GetType() != b.data.GetType() || a.data != b.data;

        public static explicit operator byte?(Number primitive) => (byte)primitive.data;
        public static explicit operator sbyte?(Number primitive) => (sbyte)primitive.data;
        public static explicit operator short?(Number primitive) => (short)primitive.data;
        public static implicit operator int?(Number primitive) => (int)primitive.data;
        public static explicit operator long?(Number primitive) => (long)primitive.data;
        public static explicit operator float?(Number primitive) => (float)primitive.data;
        public static explicit operator double?(Number primitive) => (double)primitive.data;
        public static explicit operator decimal?(Number primitive) => (decimal)primitive.data;

        public static explicit operator byte(Number primitive) => (byte)primitive.data;
        public static explicit operator sbyte(Number primitive) => (sbyte)primitive.data;
        public static explicit operator short(Number primitive) => (short)primitive.data;
        public static implicit operator int(Number primitive) => (int)primitive.data;
        public static explicit operator long(Number primitive) => (long)primitive.data;
        public static explicit operator float(Number primitive) => (float)primitive.data;
        public static explicit operator double(Number primitive) => (double)primitive.data;
        public static explicit operator decimal(Number primitive) => (decimal)primitive.data;

        public static implicit operator Number(byte obj) => new Number(obj);
        public static implicit operator Number(sbyte obj) => new Number(obj);
        public static implicit operator Number(short obj) => new Number(obj);
        public static implicit operator Number(int obj) => new Number(obj);
        public static implicit operator Number(long obj) => new Number(obj);
        public static implicit operator Number(float obj) => new Number(obj);
        public static implicit operator Number(double obj) => new Number(obj);
        public static implicit operator Number(decimal obj) => new Number(obj);
    }
#nullable restore
}
