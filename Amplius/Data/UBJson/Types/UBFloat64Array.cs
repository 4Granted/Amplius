
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
    public sealed class UBFloat64Array : UBArray
    {
        private double[] array;

        public UBFloat64Array(double[] array) : base(null)
        {
            this.array = array;
            value = array.As(i => CreateValue(i)).ToArray();
        }

        public override bool IsStronglyTyped() => true;
        public override UBArrayType GetArrayType => UBArrayType.FLOAT64;

        public new UBValue Get(int index) => CreateDouble(array[index]);

        public override UBType GetUBType => UBType.ARRAY;
        public new double[] GetValue() => array;
        public override string ToString() => value.ToString();
        public override int GetHashCode() => value.GetHashCode();
    }
}
