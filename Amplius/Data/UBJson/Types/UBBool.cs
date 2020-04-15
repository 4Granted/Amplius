namespace Amplius.Data.UBJson
{
    public sealed class UBBool : UBValue<bool>
    {
        public UBBool(bool value) : base(value) { }

        public override UBType GetUBType => UBType.BOOL;

        public override bool GetValue() => value;

        public override string ToString() => value.ToString();
    }
}
