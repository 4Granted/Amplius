using System;

namespace Amplius.Data.UBJson
{
    /// <summary>
    /// Marks a field on an object as a writable <c>UBObject</c> value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class UBFieldAttribute : Attribute
    {
        public string Key => key;

        private readonly string key;

        public UBFieldAttribute(string key = "") => this.key = key;
    }
}
