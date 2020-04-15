namespace Amplius.Data.UBJson
{
    public sealed class UBInt8Array : UBArray
    {
        private byte[] array;

        public UBInt8Array(byte[] array) : base(null) => this.array = array;

        public override bool IsStronglyTyped() => true;
        public override UBArrayType GetArrayType => UBArrayType.INT8;

        public new UBValue Get(int index) => CreateIntAuto(array[index]);

        public override UBType GetUBType => UBType.ARRAY;
        public new byte[] GetValue() => array;
        public override string ToString() => value.ToString();
        public override int GetHashCode() => value.GetHashCode();
    }
}
