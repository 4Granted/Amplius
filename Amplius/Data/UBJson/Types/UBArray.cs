using System;
using System.Diagnostics.CodeAnalysis;

namespace Amplius.Data.UBJson
{
    public partial class UBArray : UBValue<UBValue[]>, IComparable<UBArray>
    {
        public int Length => value.Length;

        public UBArray() : base(null) { }
        public UBArray(UBValue[] value) : base(value) { }

        public virtual bool IsStronglyTyped() => false;
        public virtual UBArrayType GetArrayType => UBArrayType.GENERIC;

        public virtual UBValue Get(int index) => value[index];

        public override UBType GetUBType => UBType.ARRAY;
        public override sealed UBValue[] GetValue() => value;
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

            int minSize = Math.Min(length, otherLength);

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
