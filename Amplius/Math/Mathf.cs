using System;
using System.Linq;

namespace Amplius
{
    public static class Mathf
    {
        public static float Max(float a, float b) => Math.Max(a, b);
        public static float Max(float a, float b, float c) => Math.Max(a, Math.Max(b, c));
        public static float Max(float a, float b, int c, float d) => Math.Max(a, Math.Max(b, Math.Max(c, d)));
        // Has high overhead, but great readability.
        public static float Max(params float[] values) => Enumerable.Max(values);

        public static float Min(float a, float b) => Math.Min(a, b);
        public static float Min(float a, float b, float c) => Math.Min(a, Math.Min(b, c));
        public static float Min(float a, float b, int c, float d) => Math.Min(a, Math.Min(b, Math.Min(c, d)));
        // Has high overhead, but great readability.
        public static float Min(params float[] values) => Enumerable.Min(values);
    }
}
