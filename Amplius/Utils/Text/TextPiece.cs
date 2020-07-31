namespace Amplius.Utils.Text
{
    /// <summary>
    /// A piece of text marked by a <see cref="TextSpan"/>
    /// </summary>
    public sealed class TextPiece
    {
        /// <summary>
        /// Contained source <see cref="string"/>
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// Span of the source <see cref="string"/>
        /// </summary>
        public TextSpan Span { get; }

        public TextPiece(string text, TextSpan span)
        {
            Text = text;
            Span = span;
        }
    }
}
