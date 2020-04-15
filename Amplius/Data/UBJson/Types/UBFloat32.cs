namespace Amplius.Data.UBJson
{
    public sealed class UBFloat32 : UBValue<float>
    {
        public double AsDouble => value;

        public UBFloat32(float value) : base(value) { }

        public override UBType GetUBType => UBType.FLOAT32;

        public override float GetValue() => value;

        public override string ToString() => value.ToString();
    }
}
