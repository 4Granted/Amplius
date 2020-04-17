using System;
using System.Linq;

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
    public static class Mathf
    {
        public static float Max(float a, float b) => System.Math.Max(a, b);
        public static float Max(float a, float b, float c) => System.Math.Max(a, System.Math.Max(b, c));
        public static float Max(float a, float b, int c, float d) => System.Math.Max(a, System.Math.Max(b, System.Math.Max(c, d)));
        // Has high overhead, but great readability.
        public static float Max(params float[] values) => Enumerable.Max(values);

        public static float Min(float a, float b) => System.Math.Min(a, b);
        public static float Min(float a, float b, float c) => System.Math.Min(a, System.Math.Min(b, c));
        public static float Min(float a, float b, int c, float d) => System.Math.Min(a, System.Math.Min(b, System.Math.Min(c, d)));
        // Has high overhead, but great readability.
        public static float Min(params float[] values) => Enumerable.Min(values);
    }
}
