namespace Amplius.Commands.Bleeding.Nodes
{
    internal abstract class ValueNode<T> : CommandNode
    {
        public T Value { get; }

        public ValueNode(T value) => Value = value;
    }
}
