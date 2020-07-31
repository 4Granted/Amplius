using System.Collections.Generic;
using System.Linq;

namespace Amplius.Commands.Bleeding.Nodes
{
    internal sealed class LiteralNode : ValueNode<string>
    {
        public override NodeType NodeType => NodeType.LITERAL;

        public LiteralNode(string value) : base(value) { }

        public override IEnumerable<CommandNode> GetChildren() => Enumerable.Empty<CommandNode>();
    }
}
