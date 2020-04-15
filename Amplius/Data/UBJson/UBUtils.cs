using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
