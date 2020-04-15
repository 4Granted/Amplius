namespace Amplius.Data.UBJson
{
    public sealed class UBFloat64Array : UBArray
    {
        private double[] array;

        public UBFloat64Array(double[] array) : base(null) => this.array = array;

        public override bool IsStronglyTyped() => true;
        public override UBArrayType GetArrayType => UBArrayType.FLOAT64;

        public new UBValue Get(int index) => CreateDouble(array[index]);

        public override UBType GetUBType => UBType.ARRAY;
        public new double[] GetValue() => array;
        public override string ToString() => value.ToString();
        public override int GetHashCode() => value.GetHashCode();
    }
}
