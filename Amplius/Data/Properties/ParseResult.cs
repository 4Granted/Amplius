namespace Amplius.Data.Properties
{
    internal struct ParseResult
    {
        public readonly string Key;
        public readonly object Value;

        internal ParseResult(string Key, object Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }
}
