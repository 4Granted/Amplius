using System;

namespace Amplius.Logging
{
    /// <summary>
    /// A basic exception representation of a <see cref="LogMessage"/>
    /// </summary>
    public sealed class LogException : Exception
    {
        public LogMessage ExceptionMessage { get; }

        public LogException(LogMessage message) : base(message.ToString()) => ExceptionMessage = message;
    }
}
