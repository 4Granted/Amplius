using Amplius.Utils;
using System.Collections.Immutable;
using System.IO;

namespace Amplius.Logging
{
    /// <summary>
    /// <see cref="ILogger"/> represents a class that stores <see cref="LogMessage"/>'s under a specific namespace
    /// </summary>
    public interface ILogger : INamespaced
    {
        /// <summary>
        /// A <see cref="ImmutableArray{T}"/> of type <see cref="LogMessage"/> that contains all logged messages
        /// </summary>
        public ImmutableArray<LogMessage> Messages { get; }

        /// <summary>
        /// Logs a message to a internal collection
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <param name="level">Message level</param>
        public void Log(string message, LogLevel level = LogLevel.Info);
        /// <summary>
        /// Dumps all logs to an external <see cref="TextWriter"/>
        /// </summary>
        public void Dump();
    }
}
