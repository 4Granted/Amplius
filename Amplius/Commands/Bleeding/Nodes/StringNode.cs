using System.Collections.Generic;
using System.Linq;

namespace Amplius.Commands.Bleeding.Nodes
{
    internal sealed class StringNode : ValueNode<string>
    {
        public override NodeType NodeType => NodeType.STRING;

        public StringNode(string value) : base(value) { }

        public override IEnumerable<CommandNode> GetChildren() => Enumerable.Empty<CommandNode>();
    }
}
