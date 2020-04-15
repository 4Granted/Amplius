using System;

namespace Amplius
{
    /// <summary>
    /// Useful system extensions and utilities.
    /// </summary>
    public static class SystemUtils
    {
        public static long CurrentTimeMillis() => DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
}
