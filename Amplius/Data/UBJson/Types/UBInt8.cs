namespace Amplius.Data.UBJson
{
    public sealed class UBInt8 : UBValue<sbyte>
    {
        public UBInt8(sbyte value) : base(value) { }

        public override UBType GetUBType => UBType.INT8;

        public override sbyte GetValue() => value;

        public override string ToString() => value.ToString();
    }
}
