namespace Amplius.Commands.Bleeding
{
#nullable enable
    /// <summary>
    /// A less strict interface for command implementations
    /// </summary>
    public interface ICommand
    {
        public string Label { get; }
        public string[]? Aliases { get; }

        public bool Execute(ICommandSource? source, CommandStream stream);
    }
}
