namespace Amplius.Utils
{
    public sealed class TextLocation
    {
        public string Text => text;
        public TextSpan Span => span;

        private readonly string text;
        private readonly TextSpan span;

        public TextLocation(string text, TextSpan span)
        {
            this.text = text;
            this.span = span;
        }
    }
}
