using System.Collections.Generic;
using System.Collections.Immutable;

namespace Amplius.Commands.Bleeding.Nodes
{
#nullable enable
    internal sealed class ArrayNode : ValueNode<ImmutableArray<CommandNode?>>
    {
        public override NodeType NodeType => NodeType.ARRAY;

        public ArrayNode(ImmutableArray<CommandNode?> value) : base(value) { }

        public override IEnumerable<CommandNode> GetChildren()
        {
            foreach (var child in Value)
                if (child != null)
                    yield return child;
        }
    }
}
