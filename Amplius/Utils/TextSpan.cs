namespace Amplius.Utils
{
    public sealed class TextSpan
    {
        public int Start => start;
        public int Length => length;
        public int End => start + length;

        private readonly int start;
        private readonly int length;

        public TextSpan(int start, int length)
        {
            this.start = start;
            this.length = length;
        }

        public bool Overlaps(TextSpan span) => start < span.End && End > span.Start;
        public override string ToString() => $"{start}..{End}";
        public override bool Equals(object obj)
        {
            if (obj is TextSpan span) return span.Start == Start && span.length == Length;
            return false;
        }
        public override int GetHashCode() => Start.GetHashCode() * Length.GetHashCode();

        public static TextSpan FromBounds(int start, int end) => new TextSpan(start, end - start);
    }
}
