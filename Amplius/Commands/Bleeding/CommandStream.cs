using Amplius.Commands.Bleeding.Nodes;
using System;
using System.Collections.Immutable;

namespace Amplius.Commands.Bleeding
{
#nullable enable
    /// <summary>
    /// A <see cref="CommandStream"/> contains the parsed label and arguments for a matched <see cref="ICommand"/> implementation
    /// </summary>
    public sealed class CommandStream
    {
        public string Label => label.Value;

        private readonly LiteralNode label;
        private readonly ImmutableArray<CommandNode?> arguments;
        private int position = 0;

        internal CommandStream(LiteralNode label, ImmutableArray<CommandNode?> arguments)
        {
            this.label = label;
            this.arguments = arguments;
        }

        /// <summary>
        /// Consumes a <see cref="Nodes.CommandNode"/> of <paramref name="type"/> and converts to specified <paramref name="value"/>
        /// 
        /// <para>If <paramref name="simulate"/> is set, <c>Consume</c> will not eat the argument</para>
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="type">Type to expect</param>
        /// <param name="value">Out value to set</param>
        /// <param name="simulate">Whether to simulate consumption</param>
        /// <returns>Returns whether the consumption was a success</returns>
        public bool Consume<T>(NodeType type, out T? value, bool simulate = false) where T : class
        {
            if (current != null && current.NodeType == type)
            {
                var node = simulate ? current : Next();

                if (node is StringNode sn && typeof(T).IsAssignableFrom(typeof(string)))
                {
                    value = sn.Value as T;
                    return true;
                }
                else if (node is NumberNode nn && typeof(T).IsAssignableFrom(typeof(double)))
                {
                    value = nn.Value as T;
                    return true;
                }
                else if (node is ArrayNode an && typeof(T).IsAssignableFrom(typeof(ImmutableArray<CommandNode>)))
                {
                    value = an.Value as T;
                    return true;
                }
                else if (node is LiteralNode ln && typeof(T).IsAssignableFrom(typeof(string)))
                {
                    value = ln.Value as T;
                    return true;
                }
                else if (node is BoolNode bn && typeof(T).IsAssignableFrom(typeof(bool)))
                {
                    value = bn.Value as T;
                    return true;
                }
            }

            value = default;
            return false;
        }
        /// <summary>
        /// Consumes a <see cref="Nodes.CommandNode"/> of <paramref name="type"/> and converts to specified <paramref name="value"/>
        /// 
        /// <para>If <paramref name="simulate"/> is set, <c>Consume</c> will not eat the argument</para>
        /// </summary>
        /// <typeparam name="T">Type to convert to</typeparam>
        /// <param name="type">Type to expect</param>
        /// <param name="value">Out value to set</param>
        /// <param name="simulate">Whether to simulate consumption</param>
        /// <returns>Returns whether the consumption was a success</returns>
        public bool Consume<T>(NodeType type, out T? value, bool simulate = false) where T : struct
        {
            if (current != null && current.NodeType == type)
            {
                var node = simulate ? current : Next();
                
                if (node is NumberNode nn && typeof(T).IsAssignableFrom(typeof(double)))
                {
                    if (nn.Value is T val)
                    {
                        value = val;
                        return true;
                    }
                }
                else if (node is NumberNode nni && typeof(T).IsAssignableFrom(typeof(int)))
                {
                    if ((int)nni.Value is T val)
                    {
                        value = val;
                        return true;
                    }
                }
                else if (node is ArrayNode an && typeof(T).IsAssignableFrom(typeof(ImmutableArray<CommandNode>)))
                {
                    if (an.Value is T val)
                    {
                        value = val;
                        return true;
                    }
                }
                else if (node is BoolNode bn && typeof(T).IsAssignableFrom(typeof(bool)))
                {
                    if (bn.Value is T val)
                    {
                        value = val;
                        return true;
                    }
                }
            }

            value = default;
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public bool ConsumeLiteral(string expected, bool caseSensitive = false)
        {
            if (Consume(NodeType.LITERAL, out string? value, true))
            {
                position++;
                return !string.IsNullOrEmpty(value) && (caseSensitive ? value : value.ToLower()) == (caseSensitive ? expected : expected.ToLower());
            }

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool ConsumeEnum<T>(out T value, T defaultValue) where T : Enum
        {
            if (Consume(NodeType.LITERAL, out string? literalValue, true))
            {
                position++;
                var possibleValues = Enum.GetValues(typeof(T));

                if (possibleValues != null && literalValue != null)
                {
                    foreach (var possibleValue in possibleValues)
                    {
                        if (possibleValue!.ToString()!.ToLower() == literalValue.ToLower())
                        {
                            value = (T)possibleValue;
                            return true;
                        }
                    }
                }
            }

            value = defaultValue;
            return false;
        }

        private CommandNode? current => Peek(0);
        private CommandNode? Peek(int offset)
        {
            var index = position + offset;

            if (index >= arguments.Length) return null;//return arguments[arguments.Length - 1];

            return arguments[index];
        }
        private CommandNode? Next()
        {
            var current = this.current;
            position++;
            return current;
        }
    }
}
