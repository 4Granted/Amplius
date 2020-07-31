using System.Collections.Generic;
using System.Linq;

namespace Amplius.Commands.Bleeding.Nodes
{
    internal sealed class NumberNode : ValueNode<double>
    {
        public override NodeType NodeType => NodeType.NUMBER;

        public NumberNode(double value) : base(value) { }

        public override IEnumerable<CommandNode> GetChildren() => Enumerable.Empty<CommandNode>();
    }
}
