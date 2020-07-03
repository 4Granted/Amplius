namespace Amplius.Data.Properties
{
    internal sealed class Token
    {
        public readonly string Text;
        public readonly int Start;
        public readonly TokenType Type;
        public readonly object Value;

        internal Token(string Text, int Start, TokenType Type, object Value)
        {
            this.Text = Text;
            this.Start = Start;
            this.Type = Type;
            this.Value = Value;
        }
    }
}
