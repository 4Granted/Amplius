namespace Amplius.Utils.Text
{
    /// <summary>
    /// Points to the <c>Start</c> and <c>End</c> in a source <see cref="string"/>; provides the <c>Length</c> as-well
    /// </summary>
    public sealed class TextSpan
    {
        /// <summary>
        /// Start of the <see cref="TextSpan"/>
        /// </summary>
        public int Start { get; }
        /// <summary>
        /// Length of the <see cref="TextSpan"/>
        /// </summary>
        public int Length { get; }
        /// <summary>
        /// End of the <see cref="TextSpan"/>
        /// </summary>
        public int End => Start + Length;

        public TextSpan(int start, int length)
        {
            Start = start;
            Length = length;
        }

        /// <summary>
        /// Checks if <paramref name="location"/> overlaps with the current <see cref="TextSpan"/>
        /// </summary>
        /// <param name="span">Span to validate</param>
        /// <returns>Returns whether the validation was correct</returns>
        public bool Overlaps(TextSpan span) => Start < span.End && End > span.Start;
        public override string ToString() => $"{Start}..{End}";
        public override bool Equals(object obj)
        {
            if (obj is TextSpan span) return span.Start == Start && span.Length == Length;
            return false;
        }
        public override int GetHashCode() => Start.GetHashCode() * Length.GetHashCode();

        /// <summary>
        /// Creates a <see cref="TextSpan"/> within the bounds of <paramref name="start"/> and <paramref name="end"/>
        /// </summary>
        /// <param name="start">Start bounds</param>
        /// <param name="end">End bounds</param>
        /// <returns>Returns a <see cref="TextSpan"/></returns>
        public static TextSpan FromBounds(int start, int end) => new TextSpan(start, end - start);
    }
}
