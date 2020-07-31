using Amplius.Utils;
using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Amplius.Logging
{
#nullable enable
    /// <summary>
    /// A basic implementation of <see cref="ILogger"/> for generic use
    /// </summary>
    public sealed class Logger : ILogger
    {
        public ImmutableArray<LogMessage> Messages => messages.ToImmutable();
        public NamespaceKey? Key { get; }

        private readonly ImmutableArray<LogMessage>.Builder messages;
        private readonly TextWriter? writer;

        public Logger([NotNull] NamespaceKey key, TextWriter? writer = null)
        {
            messages = ImmutableArray.CreateBuilder<LogMessage>();
            Key = key;
            this.writer = writer;
        }

        public void Log(string message, LogLevel level = LogLevel.Info)
        {
            if (string.IsNullOrEmpty(message))
                return;

            messages.Add(new LogMessage(this, level, message));
        }
        /// <summary>
        /// Dumps all logs in an external <see cref="TextWriter"/>; if one isn't set, it will default to <see cref="Console.Out"/>
        /// </summary>
        public void Dump()
        {
            foreach (var message in messages)
                (writer ?? Console.Out).WriteLine(message.ToString());
        }

        public override int GetHashCode() => Key.GetHashCode();
    }
}
