using System;

namespace Amplius
{
    public static class NumberExtensions
    {
        public static float BitsToFloat(this int self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);

        public static double BitsToDouble(this long self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);

        public static long BitsToLong(this double self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);

        public static int BitsToInt(this float self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
    }
}
