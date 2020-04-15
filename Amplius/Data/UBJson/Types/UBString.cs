using System.Text;

namespace Amplius.Data.UBJson
{
    public sealed class UBString : UBValue<byte[]>
    {
        public static readonly Encoding UTF8 = Encoding.UTF8;

        public int Length => value.Length;

        public UBString(byte[] value) : base(value) { }

        public override UBType GetUBType => UBType.STRING;

        public override byte[] GetValue() => value;

        public override string ToString() => UTF8.GetString(value);
    }
}
