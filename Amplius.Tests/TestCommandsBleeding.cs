using Amplius.Collections;
using Amplius.Commands;
using Amplius.Commands.Bleeding;
using Amplius.Utils.Tree;
using Amplius.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Amplius.Tests
{
#nullable enable
    internal sealed class TestCommandsBleeding
    {
        private static readonly PagedList<TestCommandBase> Commands = new PagedList<TestCommandBase>
        {
            new HelpCommand(),
            new TreeCommand(),
            new DumpCommand(),
            new LogCommand(),
            new ClearCommand(),
            new AliasCommand(),
        };
        private static int counter = 1;

        private static readonly CommandDispatcher dispatcher = new CommandDispatcher(Commands);
        private static readonly ConsoleColor Default = ConsoleColor.Gray;
        public static bool ContainsCommandLabel(string label)
        {
            foreach (var command in Commands)
            {
                if (command.Label == label)
                    return true;
            }

            return false;
        }
        public static void WriteLine(string message = "", ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = Default;
        }
        public static void Write(string message = "", ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = Default;
        }

        internal static bool Dispatch(ICommandSource? source, string line) => dispatcher.Dispatch(source, line);
        internal static void PrintCommand(TestCommandBase command)
        {
            Write($" {counter++}. ", ConsoleColor.White);
            WriteLine($"{command.Label}:");
            Write($"  -", ConsoleColor.Yellow);
            WriteLine($" {command.Description}");
            Write($"  -", ConsoleColor.Yellow);
            WriteLine($" {command.Usage}");
        }
        internal static void PrintCommands(int? page)
        {
            var commandsPerPage = 10;
            var commandsLength = Commands.Count;
            var nonNullPage = page ?? 0;
            var totalPages = commandsLength / commandsPerPage;
            var actualPage = nonNullPage > totalPages ? totalPages : nonNullPage;

            WriteLine($"> Displaying all commands:");

            WriteLine();
            Write("Page: ");
            WriteLine($"{actualPage}/{totalPages}", ConsoleColor.White);

            foreach (var command in Commands.GetPage(actualPage))
                PrintCommand(command);
        }

        internal abstract class TestCommandBase : Command
        {
            protected static ImmutableArray<TestCommandBase> Commands => commands.ToImmutableArray();
            private static readonly List<TestCommandBase> commands = new List<TestCommandBase>();

            public string Description => description;
            public string Usage => usage;

            private readonly string description;
            private readonly string usage;

            public TestCommandBase(string name, string description, string? usage = null, params string[] aliases) : base(name, aliases)
            {
                this.description = description;
                this.usage = usage ?? name;

                commands.Add(this);
            }
        }

        internal sealed class HelpCommand : TestCommandBase
        {
            public HelpCommand() : base("help", "Displays all commands", "help") { }

            public override bool Execute(ICommandSource? source, CommandStream stream)
            {
                counter = 1;

                if (stream.Consume(NodeType.NUMBER, out double? value))
                {
                    PrintCommands((int)value!);
                    return true;
                }

                PrintCommands(0);
                return true;
            }
        }
        internal sealed class ClearCommand : TestCommandBase
        {
            public ClearCommand() : base("clear", "Clears the console", "clear", "clr") { }

            public override bool Execute(ICommandSource? source, CommandStream stream)
            {
                Console.Clear();
                return true;
            }
        }
        internal sealed class TreeCommand : TestCommandBase
        {
            public TreeCommand() : base("tree", "Displays a tree list of the specified directory", "tree <path>") { }

            public override bool Execute(ICommandSource? source, CommandStream stream)
            {
                if (stream.Consume(NodeType.STRING, out string? value))
                {
                    if (Directory.Exists(value))
                    {
                        var graph = Tree.FromDirectory(new DirectoryInfo(value));
                        graph.PrettyPrint(Console.Out, DirectoryTree.NameFactory);
                        return true;
                    }
                }

                Console.WriteLine("'tree' requires a single argument referring to a valid directory.");
                return false;
            }
        }
        internal sealed class DumpCommand : TestCommandBase
        {
            public DumpCommand() : base("dump", "Dumps the logs to the console", "dump") { }

            public override bool Execute(ICommandSource? source, CommandStream stream)
            {
                var messages = Program.LOGGER.Messages;

                Console.WriteLine("Dumping logs...");
                Console.WriteLine();

                if (messages.Any())
                    Program.LOGGER.Dump();
                else
                    Console.WriteLine("No logs found");

                return true;
            }
        }
        internal sealed class LogCommand : TestCommandBase
        {
            public LogCommand() : base("log", "Displays a tree list of the specified directory", "log <debug|info|warn|error|fatal> <message>...") { }

            public override bool Execute(ICommandSource? source, CommandStream stream)
            {
                if (stream.ConsumeEnum(out LogLevel level, LogLevel.Info) && stream.Consume(NodeType.STRING, out string? message))
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        Program.LOGGER.Log(message, level);
                        Console.WriteLine($"Wrote message '{message}' to the logs");
                        return true;
                    }
                }

                Console.WriteLine("'log' requires at least a single argument.");
                return false;
            }
        }
        internal sealed class AliasCommand : TestCommandBase
        {
            public AliasCommand() : base("alias", "Set's the alias for provided string", "alias <name> <command string>") { }

            public override bool Execute(ICommandSource? source, CommandStream stream)
            {
                if (stream.Consume(NodeType.LITERAL, out string? name))
                {
                    if (stream.Consume(NodeType.STRING, out string? value))
                    {
                        if (ContainsCommandLabel(name!))
                        {
                            Console.WriteLine($"A command of the name '{name}' already exists.");
                            return false;
                        }

                        TestCommandsBleeding.Commands.Add(new AnonCommand(name!, value!));
                        Console.WriteLine($"Set alias for {value} to {name}.");
                        return true;
                    }
                }

                Console.WriteLine("'alias' requires a two arguments; one referring to a valid name, and one referring to a valid command(s).");
                return false;
            }
        }
        internal sealed class AnonCommand : TestCommandBase, ICommandSource
        {
            private readonly string command;

            public AnonCommand(string name, string command) : base(name, $"An alias for running '{command}'", name) => this.command = command;

            public override bool Execute(ICommandSource? source, CommandStream stream) => Dispatch(this, command);
            public string? GetName() => "Anonymous Alias source";
        }
    }
}
