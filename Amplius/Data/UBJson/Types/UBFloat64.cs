namespace Amplius.Data.UBJson
{
    public sealed class UBFloat64 : UBValue<double>
    {
        public float AsFloat => (float)value;

        public UBFloat64(double value) : base(value) { }

        public override UBType GetUBType => UBType.FLOAT64;

        public override double GetValue() => value;

        public override string ToString() => value.ToString();
    }
}
