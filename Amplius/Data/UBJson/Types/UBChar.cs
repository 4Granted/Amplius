namespace Amplius.Data.UBJson
{
    public sealed class UBChar : UBValue<char>
    {
        public UBChar(char value) : base(value) { }

        public override UBType GetUBType => UBType.CHAR;

        public override char GetValue() => value;

        public override string ToString() => value.ToString();
    }
}
