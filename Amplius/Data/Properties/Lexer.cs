using Amplius.Utils;
using System;
using System.Collections.Generic;

namespace Amplius.Data.Properties
{
    internal sealed class PLexer
    {
        private readonly string input;
        private int position = 0;
        private int start;
        private TokenType type;
        private object value;

        private char current => Peek(0);
        private char peek => Peek(1);

        internal PLexer(string input) => this.input = input;

        public List<Token> LexAll(bool print = false)
        {
            var tokens = new List<Token>();
            Token token = null;

            while (token == null || (token.Type != TokenType.EOF && token.Type != TokenType.BAD))
            {
                token = Lex();
                tokens.Add(token);
                if (print && token?.Type != TokenType.EOF)
                    Console.WriteLine($"Text: {token.Text}, Start: {token.Start}, Type: {token.Type}, Value: {token.Value}");
            }

            return tokens;
        }

        public Token Lex()
        {
            start = position;
            type = TokenType.BAD;
            value = null;

            switch (current)
            {
                case '\0':
                    type = TokenType.EOF;
                    break;
                case ',':
                    type = TokenType.COMMA;
                    position++;
                    break;
                case '=':
                    type = TokenType.EQUALS;
                    position++;
                    break;
                case '#':
                    ReadComment();
                    return Lex();
                case '"':
                    ReadString();
                    break;
                case '-':
                case '0': case '1': case '2': case '3': case '4':
                case '5': case '6': case '7': case '8': case '9':
                    ReadNumber();
                    break;
                case '[':
                    ReadArray();
                    break;
                case ' ':
                case '\t':
                case '\r':
                    ReadWhiteSpace();
                    return Lex();
                case '\n':
                    type = TokenType.ENDLINE;
                    position++;
                    break;
                case '_':
                    ReadIdOrKeyword();
                    break;
                default:
                    if (char.IsLetter(current)) ReadIdOrKeyword();
                    else if (char.IsWhiteSpace(current)) ReadWhiteSpace();
                    else
                    {
                        var span = new TextSpan(start, 1);
                        var loc = new TextLocation(input, span);
                        Console.WriteLine("Bad token or character");
                        position++;
                    }
                    break;
            }

            var t = "";
            if (string.IsNullOrEmpty(t)) t = input.Slice(start, position);
            if (t == '\n'.ToString()) t = "@ENDLINE";

            return new Token(t, start, type, value);
        }

        private void ReadComment()
        {
            position++;

            var done = false;

            while (!done)
            {
                switch (current)
                {
                    // case '\0':
                    //     var span = new TextSpan(this.start, 1);
                    //     var loc = new TextLocation(this.text, span);
                    //     console.log(`Unterminated comment`);
                    //     done = true;
                    //     break;
                    case '\r':
                    case '\n':
                        position++;
                        done = true;
                        break;
                    default:
                        position++;
                        break;
                }
            }
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
                        var loc = new TextLocation(input, span);
                        Console.WriteLine("Unterminated string");
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

            type = TokenType.STRING;
            value = sb;
        }

        private void ReadNumber()
        {
            var start = position;

            if (current == '-') position++;

            while (char.IsDigit(current) || current == '_') position++;

            var t = input.Slice (start, position);

            int? val = Convert.ToInt32(t);

            if (!val.HasValue)
            {
                var span = new TextSpan(start, position);
                var loc = new TextLocation(input, span);
                Console.WriteLine("Invalid number");
            }

            value = val;
            type = TokenType.NUMBER;
        }

        private void ReadArray()
        {
            position++;

            var values = new List<Token>();
            var done = false;

            while (!done)
            {
                switch (current)
                {
                    case ']':
                        position++;
                        done = true;
                        break;
                    case ',':
                        position++;
                        break;
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        position++;
                        break;
                    default:
                        values.Add(Lex());
                        break;
                }
            }

            type = TokenType.ARRAY;
            value = values;
        }

        private void ReadIdOrKeyword()
        {
            while (char.IsLetterOrDigit(current)) position++;

            var t = input.Slice(start, position);
            type = GetKeywordKind(t);
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

        private static TokenType GetKeywordKind(string input)
        {
            var lowercase = input.ToLower();

            switch (lowercase)
            {
                case "true":
                    return TokenType.TRUE;
                case "false":
                    return TokenType.FALSE;
                default:
                    return TokenType.IDENTIFIER;
            }
        }
    }
}
