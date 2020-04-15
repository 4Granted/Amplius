namespace Amplius.Data.UBJson
{
    public sealed class UBInt32 : UBValue<int>
    {
        public UBInt32(int value) : base(value) { }

        public override UBType GetUBType => UBType.INT32;

        public override int GetValue() => value;

        public override string ToString() => value.ToString();
    }
}
