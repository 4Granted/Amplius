namespace Amplius.Data.UBJson
{
    public sealed class UBInt16 : UBValue<short>
    {
        public UBInt16(short value) : base(value) { }

        public override UBType GetUBType => UBType.INT16;

        public override short GetValue() => value;

        public override string ToString() => value.ToString();
    }
}
