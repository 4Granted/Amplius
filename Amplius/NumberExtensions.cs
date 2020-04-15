using System;

namespace Amplius
{
    /// <summary>
    /// Useful number extensions.
    /// </summary>
    public static class NumberExtensions
    {
        public static int BitsToShort(this short self) => BitConverter.ToInt16(BitConverter.GetBytes(self), 0);
        public static int BitsToShort(this int self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToShort(this long self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToShort(this float self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToShort(this double self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);

        public static int BitsToInt(this short self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToInt(this int self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToInt(this long self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToInt(this float self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);
        public static int BitsToInt(this double self) => BitConverter.ToInt32(BitConverter.GetBytes(self), 0);

        public static long BitsToLong(this short self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);
        public static long BitsToLong(this int self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);
        public static long BitsToLong(this long self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);
        public static long BitsToLong(this float self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);
        public static long BitsToLong(this double self) => BitConverter.ToInt64(BitConverter.GetBytes(self), 0);

        public static float BitsToFloat(this short self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);
        public static float BitsToFloat(this int self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);
        public static float BitsToFloat(this long self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);
        public static float BitsToFloat(this float self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);
        public static float BitsToFloat(this double self) => BitConverter.ToSingle(BitConverter.GetBytes(self), 0);

        public static double BitsToDouble(this short self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);
        public static double BitsToDouble(this int self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);
        public static double BitsToDouble(this long self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);
        public static double BitsToDouble(this float self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);
        public static double BitsToDouble(this double self) => BitConverter.ToDouble(BitConverter.GetBytes(self), 0);
    }
}
