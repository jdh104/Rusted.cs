
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Rusted
{
    public static class MathX
    {
        public static byte ClosestToZero(byte left, byte right)
            => left < right ? left : right;

        public static sbyte ClosestToZero(sbyte left, sbyte right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;

        public static short ClosestToZero(short left, short right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;

        public static ushort ClosestToZero(ushort left, ushort right)
            => left < right ? left : right;

        public static int ClosestToZero(int left, int right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;

        public static uint ClosestToZero(uint left, uint right)
            => left < right ? left : right;

        public static long ClosestToZero(long left, long right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;

        public static ulong ClosestToZero(ulong left, ulong right)
            => left < right ? left : right;

        public static float ClosestToZero(float left, float right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;

        public static double ClosestToZero(double left, double right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;

        public static decimal ClosestToZero(decimal left, decimal right)
            => Math.Abs(left) < Math.Abs(right) ? left : right;

        public static BigInteger ClosestToZero(BigInteger left, BigInteger right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;

        public static byte FarthestFromZero(byte left, byte right)
            => left < right ? right : left;

        public static sbyte FarthestFromZero(sbyte left, sbyte right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;

        public static short FarthestFromZero(short left, short right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;

        public static ushort FarthestFromZero(ushort left, ushort right)
            => left < right ? right : left;

        public static int FarthestFromZero(int left, int right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;

        public static uint FarthestFromZero(uint left, uint right)
            => left < right ? right : left;

        public static long FarthestFromZero(long left, long right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;

        public static ulong FarthestFromZero(ulong left, ulong right)
            => left < right ? right : left;

        public static float FarthestFromZero(float left, float right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;

        public static double FarthestFromZero(double left, double right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;

        public static decimal FarthestFromZero(decimal left, decimal right)
            => Math.Abs(left) < Math.Abs(right) ? right : left;

        public static BigInteger FarthestFromZero(BigInteger left, BigInteger right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;
    }
}
