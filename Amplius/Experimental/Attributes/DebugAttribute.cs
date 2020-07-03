using System;

namespace Amplius.Experimental
{
    [Todo("Add functionality to the <ref>warn</ref> parameter.")]
    public abstract class DebugAttribute : Attribute
    {
        public bool Warn => warn;

        private readonly bool warn;

        public DebugAttribute(bool warn = true) => this.warn = warn;
    }
}
