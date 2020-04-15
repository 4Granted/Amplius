namespace Amplius.Data.UBJson
{
    public sealed class UBInt32Array : UBArray
    {
        private int[] array;

        public UBInt32Array(int[] array) : base(null) => this.array = array;

        public override bool IsStronglyTyped() => true;
        public override UBArrayType GetArrayType => UBArrayType.INT32;

        public new UBValue Get(int index) => CreateIntAuto(array[index]);

        public override UBType GetUBType => UBType.ARRAY;
        public new int[] GetValue() => array;
        public override string ToString() => value.ToString();
        public override int GetHashCode() => value.GetHashCode();
    }
}
