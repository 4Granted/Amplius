namespace Amplius.Data.UBJson
{
    public sealed class UBInt16Array : UBArray
    {
        private short[] array;

        public UBInt16Array(short[] array) : base(null) => this.array = array;

        public override bool IsStronglyTyped() => true;
        public override UBArrayType GetArrayType => UBArrayType.INT16;

        public new UBValue Get(int index) => CreateIntAuto(array[index]);

        public override UBType GetUBType => UBType.ARRAY;
        public new short[] GetValue() => array;
        public override string ToString() => value.ToString();
        public override int GetHashCode() => value.GetHashCode();
    }
}
