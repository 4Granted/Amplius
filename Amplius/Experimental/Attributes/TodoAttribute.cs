namespace Amplius.Experimental
{
    /// <summary>
    /// Marks a declarative block as in-need of a implementation, a change or a fix.
    /// </summary>
    public sealed class TodoAttribute : DebugAttribute
    {
        public string Message => message;

        private readonly string message;

        public TodoAttribute(string message, bool warn = true) : base(warn) => this.message = message;
    }
}
