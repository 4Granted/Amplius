using System.Collections.Generic;

namespace Amplius.Data.Properties
{
    internal sealed class PParser
    {
        private static int globalIndex = 0;
        private readonly List<Token> tokens;
        private int position = 0;

        private Token current => Peek(0);

        internal PParser(List<Token> tokens) => this.tokens = tokens;

        public List<ParseResult> ParseAll()
        {
            var values = new List<ParseResult>();

            while (position < tokens.Count)
            {
                var result = Parse();
                if (!(result.Value is TokenType.BAD)) values.Add(result);
                if (Peek(0).Type == TokenType.EOF) return values;
            }

            return values;
        }

        public ParseResult Parse()
        {
            if (Peek(0).Type == TokenType.IDENTIFIER && Peek(1).Type == TokenType.EQUALS && Peek(2) != null)
            {
                var identifierToken = NextToken();
                NextToken();
                var value = ParseValue();
                MatchToken(TokenType.ENDLINE);

                return new ParseResult(identifierToken.Text, value);
            }

            var kind = NextToken().Type;
            return new ParseResult($"Error${globalIndex++} -${ kind}", TokenType.BAD);
        }

        private object ParseValue(Token given = null)
        {
            var token = given != null ? given : this.NextToken();

            switch (token.Type)
            {
                case TokenType.TRUE:
                    return true;
                case TokenType.FALSE:
                    return false;
                case TokenType.ARRAY:
                    {
                        var values = new List<object>();
                        var val = (List<Token>)token.Value;

                        for (var i = 0; i < val.Count; i++)
                        {
                            var target = val[i];
                            values.Add(this.ParseValue(target));
                        }

                        return values;
                    }
                default:
                    return token.Value;
            }
        }

        private Token Peek(int offset) 
        {
            var index = position + offset;

            if (index >= tokens.Count) return tokens[tokens.Count - 1];

            return tokens[index];
        }

        private Token NextToken() 
        {
            var current = this.current;
            position++;
            return current;
        }

        private Token MatchToken(TokenType type) 
        {
            if (current.Type == type) return NextToken();

            return new Token(null, current.Start, type, null);
        }
    }
}
