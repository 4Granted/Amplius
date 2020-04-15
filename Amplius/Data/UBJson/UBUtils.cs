using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
    /// Static utilities for <c>UBJ</c>.
    /// </summary>
    public static class UBUtils
    {
        public static UBType GetUBTypeFromType<T>(T type)
        {
            if (type == null)
                return UBType.NULL;
            else if (type is bool)
                return UBType.BOOL;
            else if (type is char)
                return UBType.CHAR;
            else if (type is sbyte)
                return UBType.INT8;
            else if (type is byte)
                return UBType.UINT8;
            else if (type is short)
                return UBType.INT16;
            else if (type is int)
                return UBType.INT32;
            else if (type is long)
                return UBType.INT64;
            else if (type is float)
                return UBType.FLOAT32;
            else if (type is double)
                return UBType.FLOAT64;
            else if (type is string)
                return UBType.STRING;
            else if (type is IEnumerable)
                return UBType.ARRAY;
            else if (type is UBObject)
                return UBType.OBJECT;
            else
                return UBType.UNKNOWN;
        }
    }
}
