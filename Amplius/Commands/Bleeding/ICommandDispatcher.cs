using System.Collections.Generic;

namespace Amplius.Commands.Bleeding
{
#nullable enable
    /// <summary>
    /// Base interface for dispatching the Bleeding branch of <c>Amplius.Commands</c> <see cref="ICommand"/>'s
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Internal list of registered commands
        /// </summary>
        public IEnumerable<ICommand> Commands { get; }

        /// <summary>
        /// Dispatches a command from <paramref name="source"/> to a registered <see cref="ICommand"/> implementation
        /// </summary>
        /// <param name="source">Source of the command sender</param>
        /// <param name="line">Input string to dispatch</param>
        /// <returns>Returns a bool dependant upon the success of the dispatch</returns>
        public bool Dispatch(ICommandSource? source, string line);
        /// <summary>
        /// Searches the internal list of <see cref="ICommand"/>'s for a matching label
        /// </summary>
        /// <param name="label">Label to match</param>
        /// <returns>Returns the matched command</returns>
        public ICommand? Search(string label);
    }
}
