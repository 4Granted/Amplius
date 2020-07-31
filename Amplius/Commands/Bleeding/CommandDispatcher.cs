using Amplius.Commands.Bleeding.Nodes;
using Amplius.Data.UBJson;
using Amplius.Registry;
using Amplius.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Amplius.Commands.Bleeding
{
#nullable enable
    /// <summary>
    /// The default implementation of <see cref="ICommandDispatcher"/>
    /// </summary>
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        public IEnumerable<ICommand> Commands => commands;

        private static int index = 0;

        //private readonly ICommand[] commands;
        private readonly IEnumerable<ICommand> commands;
        private readonly CommandLexer lexer;
        private readonly CommandParser parser;

        public CommandDispatcher(params ICommand[] commands) : this((IEnumerable<ICommand>)commands) { }
        public CommandDispatcher(IEnumerable<ICommand> commands)
        {
            lexer = new CommandLexer();
            parser = new CommandParser();
            this.commands = commands;
        }

        public bool Dispatch(ICommandSource? source, string line)
        {
            var tokens = lexer.LexAll(line);
            var streams = parser.ParseCommands(tokens);
            var success = false;

            if (streams.Any())
            {
                foreach (var stream in streams)
                {
                    if (stream != null)
                    {
                        var command = Search(stream.Label);

                        success = command?.Execute(source, stream) ?? false;

                        if (!success)
                            Error(command?.Label);
                    }
                }
            }

            return success;
        }
        public ICommand? Search(string label)
        {
            foreach (var command in commands)
            {
                if (command.Label == label)
                    return command;
                else
                {
                    if (command?.Aliases != null)
                    {
                        foreach (var alias in command.Aliases)
                        {
                            if (alias == label)
                                return command;
                        }
                    }
                }
            }

            return null;
        }

        private void Error(string? label) => Console.WriteLine($"Failed to dispatch '{label ?? "unknown"}'");

        private static string KeyFactory(ICommand value) => value.Label ?? $"command{index++}";
    }
}
