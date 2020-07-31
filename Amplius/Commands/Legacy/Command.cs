using System;

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

namespace Amplius.Commands.Legacy
{
#nullable enable
    public sealed class Command
    {
        public string Label => label;
        public string[] Args => args;
        public CommandRepresentation? Representation => representation;
        public ICommandDispatcher Dispatcher => dispatcher;
        public ICommandExecutor Executor => executor;
        public ICommandSource? Source => source;

        private readonly string raw;
        private readonly string label;
        private readonly string[] args;
        private readonly CommandRepresentation? representation;
        private readonly ICommandDispatcher dispatcher;
        private readonly ICommandExecutor executor;
        private readonly ICommandSource? source;

        internal Command(ICommandDispatcher dispatcher, ICommandExecutor executor, ICommandSource? source, string raw, IInputDissector? dissector = null)
        {
            this.source = source;
            this.dispatcher = dispatcher;
            this.executor = executor;
            this.raw = raw;

            var inputDissector = dissector ?? new DefaultInputDissector();

            var dissection = inputDissector.Dissect(raw);
            label = dissection.label;
            args = dissection.args ?? new string[] { };
            representation = dispatcher.FindRepresentation(label);

            if (representation == null)
                Console.WriteLine($"Couldn't find a representation of the command '{label}'");
        }
        internal Command(ICommandDispatcher dispatcher, ICommandExecutor executor, ICommandSource? source, string label, string[] args, CommandRepresentation representation)
        {
            this.source = source;
            this.dispatcher = dispatcher;
            this.executor = executor;
            raw = "";

            this.label = label;
            this.args = args ?? new string[] { };
            this.representation = representation;
        }

        public bool Execute() => representation?.Execute(this) ?? false;
        public bool Redirect(CommandRepresentation representation, string[] args) => Executor.Execute(new Command(Dispatcher, Executor, Source, representation.Name, args, representation));
    }
}
