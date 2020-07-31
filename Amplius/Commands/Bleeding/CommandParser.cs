using Amplius.Commands.Bleeding.Nodes;
using System;
using System.Collections.Immutable;

namespace Amplius.Commands.Bleeding
{
#nullable enable
    internal sealed class CommandParser
    {
        private static readonly BoolNode TRUE = new BoolNode(true);
        private static readonly BoolNode FALSE = new BoolNode(false);

        private ImmutableArray<Token> tokens;
        private int position = 0;

        private Token current => Peek(0);

        internal ImmutableArray<CommandStream?> ParseCommands(ImmutableArray<Token> tokens)
        {
            position = 0;
            this.tokens = tokens;

            var commands = ImmutableArray.CreateBuilder<CommandStream?>();

            var parseNextCommand = true;
            while (parseNextCommand && current.Type != SyntaxType.EOF)
            {
                var node = ParseCommand();
                commands.Add(node);

                if (current.Type == SyntaxType.PIPE)
                    MatchToken(SyntaxType.PIPE);
                else
                    parseNextCommand = false;
            }

            return commands.ToImmutable();
        }
        private CommandStream? ParseCommand()
        {
            if (current.Type == SyntaxType.NODE_TOKEN)
            {
                var label = (LiteralNode)ParseNode()!;
                var arguments = ParseRest();

                return new CommandStream(label, arguments);
            }

            return null;
        }
        private ImmutableArray<CommandNode?> ParseRest()
        {
            var nodes = ImmutableArray.CreateBuilder<CommandNode?>();

            var parseNext = true;

            while (parseNext)
            {
                CommandNode? node = null;

                if (current.Type != SyntaxType.PIPE)
                    node = ParseNode();

                if (node != null)
                    nodes.Add(node);
                else
                    parseNext = false;
            }

            return nodes.ToImmutable();
        }
        private CommandNode? ParseNode()
        {
            var token = NextToken();

            switch (token.Type)
            {
                case SyntaxType.STRING_TOKEN:
                    return new StringNode((string)token.Value!);
                case SyntaxType.NUMBER_TOKEN:
                    return new NumberNode((double)token.Value!);
                case SyntaxType.OPEN_BRACE:
                    return ParseArray();
                case SyntaxType.NODE_TOKEN:
                    return new LiteralNode(token.Text!);
                case SyntaxType.TRUE:
                    return TRUE;
                case SyntaxType.FALSE:
                    return FALSE;
                default:
                    return null;
            }
        }
        private ArrayNode ParseArray()
        {
            var array = ImmutableArray.CreateBuilder<CommandNode?>();

            var parseNextParameter = true;
            while (parseNextParameter && current.Type != SyntaxType.CLOSED_BRACE && current.Type != SyntaxType.EOF)
            {
                var node = ParseNode();
                array.Add(node);

                if (current.Type == SyntaxType.COMMA)
                    MatchToken(SyntaxType.COMMA);
                else
                    parseNextParameter = false;
            }

            MatchToken(SyntaxType.CLOSED_BRACE);

            return new ArrayNode(array.ToImmutable());
        }

        private Token Peek(int offset)
        {
            var index = position + offset;

            if (index >= tokens.Length) return tokens[tokens.Length - 1];

            return tokens[index];
        }
        private Token NextToken()
        {
            var current = this.current;
            position++;
            return current;
        }
        private Token MatchToken(SyntaxType type)
        {
            if (current.Type == type) return NextToken();

            return new Token(null, current.Start, type, null);
        }
    }
}
