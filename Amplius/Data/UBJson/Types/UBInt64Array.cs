namespace Amplius.Data.UBJson
{
    public sealed class UBInt64Array : UBArray
    {
        private long[] array;

        public UBInt64Array(long[] array) : base(null) => this.array = array;

        public override bool IsStronglyTyped() => true;
        public override UBArrayType GetArrayType => UBArrayType.INT64;

        public new UBValue Get(int index) => CreateIntAuto(array[index]);

        public override UBType GetUBType => UBType.ARRAY;
        public new long[] GetValue() => array;
        public override string ToString() => value.ToString();
        public override int GetHashCode() => value.GetHashCode();
    }
}
