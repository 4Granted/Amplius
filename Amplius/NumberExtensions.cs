using System;

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

namespace Amplius
{
    /// <summary>
    /// Useful number extensions.
    /// </summary>
    public static class NumberExtensions
    {
        public static int BitsToShort(this short self) => BitConverter.ToInt16(BitConverter.GetBytes(self), 0);
        public static int BitsToShort(this int self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToShort(this long self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToShort(this float self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToShort(this double self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);

        public static int BitsToInt(this short self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToInt(this int self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToInt(this long self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToInt(this float self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToInt(this double self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);

        public static long BitsToLong(this short self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);
        public static long BitsToLong(this int self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);
        public static long BitsToLong(this long self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);
        public static long BitsToLong(this float self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);
        public static long BitsToLong(this double self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);

        public static float BitsToFloat(this short self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);
        public static float BitsToFloat(this int self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);
        public static float BitsToFloat(this long self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);
        public static float BitsToFloat(this float self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);
        public static float BitsToFloat(this double self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);

        public static double BitsToDouble(this short self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);
        public static double BitsToDouble(this int self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);
        public static double BitsToDouble(this long self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);
        public static double BitsToDouble(this float self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);
        public static double BitsToDouble(this double self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);
    }
}
