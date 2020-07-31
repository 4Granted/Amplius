using System.Diagnostics.CodeAnalysis;

namespace Amplius.Commands.Bleeding
{
#nullable enable
    /// <summary>
    /// A stricter implementation of <see cref="ICommand"/> for faster setup
    /// </summary>
    public abstract class Command : ICommand
    {
        public string Label => label;
        public string[]? Aliases => aliases;

        private readonly string label;
        private readonly string[]? aliases;

        public Command([NotNull] string label, params string[] aliases)
        {
            this.label = label;
            this.aliases = aliases;
        }

        public abstract bool Execute(ICommandSource? source, CommandStream stream);
    }
}
