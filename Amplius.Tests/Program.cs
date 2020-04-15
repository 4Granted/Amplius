using Amplius.Data;
using Amplius.Events;
using Amplius.Extensibility;
using Amplius.Registry;
using System;

namespace Amplius.Tests
{
    /*
     * Will convert this into a proper unit test suite.
     * 
     * For now, I'll be using this as a more REPL/interactive based testing suite.
     */
    internal sealed class Program
    {
        internal static void Main(string[] args) => new Program(args).Also(_ => Console.ReadLine());

        private Program(string[] args)
        {
            
        }
    }
}
