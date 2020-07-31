using System;

namespace Amplius.Logging
{
    /// <summary>
    /// Contains information on a logged message
    /// </summary>
    public struct LogMessage
    {
        public LogLevel Level { get; }
        public string Message { get; }

        private readonly ILogger logger;

        public LogMessage(ILogger logger, LogLevel level, string message)
        {
            this.logger = logger;
            Level = level;
            Message = message;
        }

        /// <summary>
        /// Throws a <see cref="LogException"/> of itself
        /// </summary>
        public void Throw() => throw new LogException(this);

        public override string ToString() => $"{DateTime.Now} {Level.ToString().ToUpper()} {logger!.Key!.DeserializationKey}: {Message}";
    }
}
