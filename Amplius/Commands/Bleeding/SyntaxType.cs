namespace Amplius.Commands.Bleeding
{
    /// <summary>
    /// All syntax/token types produced by the <see cref="CommandLexer"/>
    /// </summary>
    internal enum SyntaxType
    {
        EOF,
        BAD,

        COMMA,
        OPEN_BRACE,
        CLOSED_BRACE,
        PIPE,

        STRING_TOKEN,
        NUMBER_TOKEN,
        NODE_TOKEN,

        TRUE,
        FALSE,
    }
}
