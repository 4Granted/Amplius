namespace Amplius.Data.UBJson
{
    public sealed class UBFloat32Array : UBArray
    {
        private float[] array;

        public UBFloat32Array(float[] array) : base(null) => this.array = array;

        public override bool IsStronglyTyped() => true;
        public override UBArrayType GetArrayType => UBArrayType.FLOAT32;

        public override UBValue Get(int index) => CreateFloat(array[index]);

        public override UBType GetUBType => UBType.ARRAY;
        public new float[] GetValue() => array;
        public override string ToString() => value.ToString();
        public override int GetHashCode() => value.GetHashCode();
    }
}
