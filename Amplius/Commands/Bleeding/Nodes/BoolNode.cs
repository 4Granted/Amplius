using System.Collections.Generic;
using System.Linq;

namespace Amplius.Commands.Bleeding.Nodes
{
    internal sealed class BoolNode : ValueNode<bool>
    {
        public override NodeType NodeType => NodeType.BOOL;

        public BoolNode(bool value) : base(value) { }

        public override IEnumerable<CommandNode> GetChildren() => Enumerable.Empty<CommandNode>();
    }
}
