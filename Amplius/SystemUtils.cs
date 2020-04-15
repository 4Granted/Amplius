using System;

namespace Amplius
{
    public static class SystemUtils
    {
        public static long CurrentTimeMillis() => DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
}
