using System.Collections.Generic;

namespace Amplius.Commands.Bleeding.Nodes
{
    internal abstract class CommandNode
    {
        public abstract NodeType NodeType { get; }

        public abstract IEnumerable<CommandNode> GetChildren();
    }
}
