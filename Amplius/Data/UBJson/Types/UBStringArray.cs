namespace Amplius.Data.UBJson
{
    public sealed class UBStringArray : UBArray
    {
        private string[] array;

        public UBStringArray(string[] array) : base(null) => this.array = array;

        public override bool IsStronglyTyped() => true;
        public override UBArrayType GetArrayType => UBArrayType.STRING;

        public new UBValue Get(int index) => CreateStringOrNull(array[index]);

        public override UBType GetUBType => UBType.ARRAY;
        public new string[] GetValue() => array;
        public override string ToString() => value.ToString();
        public override int GetHashCode() => value.GetHashCode();
    }
}
