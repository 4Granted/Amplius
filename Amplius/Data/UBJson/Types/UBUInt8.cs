namespace Amplius.Data.UBJson
{
    public sealed class UBUInt8 : UBValue<short>
    {
        public UBUInt8(long value) : base((short)(0xFF & value)) { }

        public override UBType GetUBType => UBType.UINT8;

        public override short GetValue() => value;

        public override string ToString() => value.ToString();
    }
}
