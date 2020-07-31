using Amplius.Commands;
using Amplius.Commands.Legacy;
using Amplius.Utils.Tree;
using Amplius.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

/// <license>
/// MIT License
/// 
/// Copyright(c) 2020 RuthlessBoi
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.
/// </license>

namespace Amplius.Tests
{
#nullable enable
    internal sealed class TestCommandsLegacy
    {
        private static readonly TestCommandBase[] Commands = new TestCommandBase[]
        {
            new HelpCommand(),
            new TreeCommand(),
            new DumpCommand(),
            new LogCommand(),
        };

        private static readonly ICommandDispatcher dispatcher = new DefaultCommandDispatcher(Commands);
        private static readonly ICommandExecutor executor = new DefaultCommandExecutor();

        internal static bool Dispatch(string line, ICommandSource? source) => dispatcher.Dispatch(executor, source, line);
        internal static void PrintCommand(TestCommandBase command)
        {
            Console.WriteLine($"{command.Name}:");
            Console.WriteLine($" - Desc: {command.Description}");
            Console.WriteLine($" - Usage: {command.Usage}");
        }

        internal abstract class TestCommandBase : CommandRepresentation
        {
            protected static ImmutableArray<TestCommandBase> Commands => commands.ToImmutableArray();
            private static readonly List<TestCommandBase> commands = new List<TestCommandBase>();

            public string Description => description;
            public string Usage => usage;

            private readonly string description;
            private readonly string usage;

            public TestCommandBase(string name, string description, string? usage = null) : base(name)
            {
                this.description = description;
                this.usage = usage ?? name;

                commands.Add(this);
            }
        }

        internal sealed class HelpCommand : TestCommandBase
        {
            public HelpCommand() : base("help", "Displays all commands", "help") { }

            public override bool Execute(Command? command)
            {
                Console.WriteLine("> Displaying all commands:");

                foreach (var cmd in Commands)
                {
                    PrintCommand(cmd);
                }

                return true;
            }
        }
        internal sealed class TreeCommand : TestCommandBase
        {
            public TreeCommand() : base("tree", "Displays a tree list of the specified directory", "tree <path>") { }

            public override bool Execute(Command? command)
            {
                var args = command?.Args;

                if (args.Any() && args!.Length == 1)
                {
                    var arg = args[0];

                    if (Directory.Exists(arg))
                    {
                        var graph = Tree.FromDirectory(new DirectoryInfo(arg));
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

            public override bool Execute(Command? command)
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

            public override bool Execute(Command? command)
            {
                var args = command?.Args;

                if (args.Any() && args!.Length >= 2)
                {
                    var levelString = args[0];
                    var message = args[1];

                    if (!string.IsNullOrEmpty(message))
                    {
                        var level = GetLevel(levelString) ?? LogLevel.Info;
                        var builder = new StringBuilder();

                        builder.Append(message);
                        foreach (var part in args.Skip(2))
                            builder.Append($" {part}");

                        var built = builder.ToString();

                        Program.LOGGER.Log(built, level);
                        Console.WriteLine($"Wrote message '{built}' to the logs");
                        return true;
                    }
                }

                Console.WriteLine("'log' requires at least a single argument.");
                return false;
            }

            private LogLevel? GetLevel(string? level)
            {
                if (string.IsNullOrEmpty(level))
                    return null;

                var lower = level.ToLower();

                switch (lower)
                {
                    case "debug":
                        return LogLevel.Debug;
                    case "info":
                        return LogLevel.Info;
                    case "warn":
                        return LogLevel.Warn;
                    case "error":
                        return LogLevel.Error;
                    case "fatal":
                        return LogLevel.Fatal;
                    default:
                        return null;
                }
            }
        }
    }
}
