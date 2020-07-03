using System;

namespace Amplius.Experimental
{
    /// <summary>
    /// An attribute for marking classes, methods, fields, events, interfaces, properties and structs for primarily experimental use.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class ExperimentalAttribute : DebugAttribute { }
}
