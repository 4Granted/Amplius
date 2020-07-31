using Amplius.Utils;
using Amplius.Utils.Text;
using System;
using System.Collections.Immutable;

namespace Amplius.Commands.Bleeding
{
#nullable enable
    internal sealed class CommandLexer
    {
        private string input = "";
        private int position = 0;
        private int start;
        private SyntaxType type;
        private object? value;
        private string text = "";

        private char current => Peek(0);
        private char peek => Peek(1);

        internal ImmutableArray<Token> LexAll(string input)
        {
            this.position = 0;
            this.input = input;
            var tokens = ImmutableArray.CreateBuilder<Token>();
            Token? token = null;

            while ((token == null || (token.Type != SyntaxType.EOF && token.Type != SyntaxType.BAD)))
            {
                token = Lex();
                tokens.Add(token);
            }

            return tokens.ToImmutable();
        }
        private Token Lex()
        {
            start = position;
            type = SyntaxType.BAD;
            value = null;
            text = "";

            switch (current)
            {
                case '\0':
                    type = SyntaxType.EOF;
                    break;
                case ',':
                    type = SyntaxType.COMMA;
                    position++;
                    break;
                case '[':
                    type = SyntaxType.OPEN_BRACE;
                    position++;
                    break;
                case ']':
                    type = SyntaxType.CLOSED_BRACE;
                    position++;
                    break;
                case '|':
                    type = SyntaxType.PIPE;
                    position++;
                    break;
                case '"':
                    ReadString();
                    break;
                case '-':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    ReadNumber();
                    break;
                case ' ':
                case '\t':
                case '\r':
                case '\n':
                    ReadWhiteSpace();
                    return Lex();
                case '_':
                    ReadNodeOrKeyword();
                    break;
                default:
                    if (char.IsLetter(current)) ReadNodeOrKeyword();
                    else if (char.IsWhiteSpace(current)) ReadWhiteSpace();
                    else
                    {
                        var span = new TextSpan(start, 1);
                        var piece = new TextPiece(input, span);

                        Error("Bad token or character", piece);
                        position++;
                    }
                    break;
            }

            if (string.IsNullOrEmpty(text)) text = input.Slice(start, position);

            return new Token(text, start, type, value);
        }

        private void ReadString()
        {
            position++;

            var sb = "";
            var done = false;

            while (!done)
            {
                switch (current)
                {
                    case '\0':
                    case '\r':
                    case '\n':
                        var span = new TextSpan(start, 1);
                        var piece = new TextPiece(input, span);

                        Error("Unterminated string", piece);
                        done = true;
                        break;
                    case '"':
                        if (peek == '"')
                        {
                            sb += current;
                            position += 2;
                        }
                        else
                        {
                            position++;
                            done = true;
                        }
                        break;
                    default:
                        sb += current;
                        position++;
                        break;
                }
            }

            type = SyntaxType.STRING_TOKEN;
            value = sb;
        }
        private void ReadNumber()
        {
            var start = position;
            var hasDecimal = false;

            if (current == '-') position++;

            while (char.IsDigit(current) || current == '_' || current == '.')
            {
                if (current == '.' && hasDecimal)
                {
                    hasDecimal = true;
                    position++;
                } else
                    position++;
            }

            var t = input.Slice(start, position);

            //double? val = Convert.ToDouble(t);
            if (double.TryParse(t, out double val))
            {
                value = val;
                type = SyntaxType.NUMBER_TOKEN;
            } else
            {
                var span = new TextSpan(start, position);
                var piece = new TextPiece(input, span);

                Error("Invalid number", piece);
            }

            /*if (!val.HasValue)
            {
                var span = new TextSpan(start, position);
                var loc = new TextLocation(input, span);

                Error("Invalid number", loc);
            }

            value = val;
            type = SyntaxType.NUMBER_TOKEN;*/
        }
        private void ReadNodeOrKeyword()
        {
            while (char.IsLetterOrDigit(current)) position++;

            text = input.Slice(start, position);
            type = GetKeywordKind(text);
        }
        private void ReadWhiteSpace()
        {
            while (char.IsWhiteSpace(current)) position++;
        }

        private char Peek(int offset)
        {
            var index = position + offset;

            if (!string.IsNullOrEmpty(input) && index >= input.Length) return '\0';
            if (string.IsNullOrEmpty(input)) return '\0';

            return input[index];
        }
        private static SyntaxType GetKeywordKind(string input)
        {
            var lowercase = input.ToLower();

            switch (lowercase)
            {
                case "true":
                    return SyntaxType.TRUE;
                case "false":
                    return SyntaxType.FALSE;
                default:
                    return SyntaxType.NODE_TOKEN;
            }
        }
        private static void Error(string message, TextPiece piece) => Console.WriteLine($"Commands: Lexer Error - {{Message: {message}, Location: {piece.Span}}}");
    }
}
