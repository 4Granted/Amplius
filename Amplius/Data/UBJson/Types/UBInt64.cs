namespace Amplius.Data.UBJson
{
    public sealed class UBInt64 : UBValue<long>
    {
        public UBInt64(long value) : base(value) { }

        public override UBType GetUBType => UBType.INT64;

        public override long GetValue() => value;

        public override string ToString() => value.ToString();
    }
}
