namespace Amplius.Commands.Bleeding
{
    /// <summary>
    /// All <see cref="Nodes.CommandNode"/> types produced by the <see cref="CommandParser"/>
    /// </summary>
    public enum NodeType
    {
        STRING,
        NUMBER,
        ARRAY,
        LITERAL,
        BOOL,
        UNKNOWN
    }
}
