using System;

namespace Amplius
{
    /// <summary>
    /// Useful <c>bool</c> extensions.
    /// </summary>
    public static class BoolExtensions
    {
        public static bool WhenFalse(this bool self, Action<bool> callback) => self.When(false, callback);
        public static bool WhenTrue(this bool self, Action<bool> callback) => self.When(true, callback);
        public static bool WhenFalse(this bool self, Func<bool, bool> callback) => callback.Invoke(self);
        public static bool WhenTrue(this bool self, Func<bool, bool> callback) => callback.Invoke(self);
        public static bool When(this bool self, bool expected, Action<bool> callback) => self.Also(_ => { if (self == expected) callback.Invoke(self); });

        public static int AsNumber(this bool self) => self ? 1 : 0;
    }
}
