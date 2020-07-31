using Amplius.Commands.Bleeding.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace Amplius.Commands.Bleeding
{
#nullable enable
    /// <summary>
    /// Base data class for storage of token data
    /// </summary>
    internal sealed class Token : CommandNode
    {
        public override NodeType NodeType => NodeType.UNKNOWN;

        public readonly string? Text;
        public readonly int Start;
        public readonly SyntaxType Type;
        public readonly object? Value;

        internal Token(string? Text, int Start, SyntaxType Type, object? Value)
        {
            this.Text = Text;
            this.Start = Start;
            this.Type = Type;
            this.Value = Value;
        }

        public override IEnumerable<CommandNode> GetChildren() => Enumerable.Empty<CommandNode>();

        public override string ToString() => $"Text: {Text}, Start: {Start}, Type: {Type}, Value: {Value}";
    }
}
