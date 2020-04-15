namespace Amplius.Data.UBJson
{
    public sealed class UBNull : UBValue<object>
    {
        public UBNull() : base(null) { }

        public override UBType GetUBType => UBType.NULL;

        public override object GetValue() => null;

        public override string ToString() => "null";
    }
}
