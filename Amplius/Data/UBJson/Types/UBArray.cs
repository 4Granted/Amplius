using System;
using System.Diagnostics.CodeAnalysis;

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
    public partial class UBArray : UBValue<UBValue[]>, IComparable<UBArray>
    {
        public int Length => value.Length;

        public UBArray() : base(null) { }
        public UBArray(UBValue[] value) : base(value) { }

        /// <summary>
        /// Returns whether or not the array is strongly typed; i.e. has a static type.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsStronglyTyped() => false;
        /// <summary>
        /// Returns the array type.
        /// </summary>
        public virtual UBArrayType GetArrayType => UBArrayType.GENERIC;

        /// <summary>
        /// Returns a value at the <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Index pointer</param>
        public virtual UBValue Get(int index) => value[index];

        public override UBType GetUBType => UBType.ARRAY;
        public override UBValue[] GetValue() => value;
        public override string ToString() => value.ToString();
        public override int GetHashCode()
        {
            int val = 0;

            for (int i = 0; i < Length; i++)
                val ^= Get(i).GetHashCode();

            return val;
        }
        public int CompareTo([AllowNull] UBArray other)
        {
            int length = Length;
            int otherLength = other.Length;

            int minSize = System.Math.Min(length, otherLength);

            for (int i = 0; i < minSize; i++)
            {
                int val = 0; // Compare

                if (val != 0)
                    return val;
            }

            if (length == otherLength)
                return 0;
            else if (length < otherLength)
                return -1;
            else
                return 1;
        }
    }
}
