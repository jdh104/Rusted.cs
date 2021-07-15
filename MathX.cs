
using System;
using System.Numerics;

namespace Rusted
{
    public static class MathX
    {
        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static byte ClosestToZero(byte left, byte right)
            => left < right ? left : right;

        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static sbyte ClosestToZero(sbyte left, sbyte right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;

        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static short ClosestToZero(short left, short right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;

        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ushort ClosestToZero(ushort left, ushort right)
            => left < right ? left : right;
        
        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int ClosestToZero(int left, int right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;
        
        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static uint ClosestToZero(uint left, uint right)
            => left < right ? left : right;
        
        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static long ClosestToZero(long left, long right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;
        
        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ulong ClosestToZero(ulong left, ulong right)
            => left < right ? left : right;
        
        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static float ClosestToZero(float left, float right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;
        
        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static double ClosestToZero(double left, double right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;
        
        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static decimal ClosestToZero(decimal left, decimal right)
            => Math.Abs(left) < Math.Abs(right) ? left : right;
        
        /// <summary>
        /// Returns the given argument with the smallest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static BigInteger ClosestToZero(BigInteger left, BigInteger right)
            => left - right < 2 * left ^ left - right > 0 ? left : right;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static byte FarthestFromZero(byte left, byte right)
            => left < right ? right : left;

        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static sbyte FarthestFromZero(sbyte left, sbyte right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static short FarthestFromZero(short left, short right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ushort FarthestFromZero(ushort left, ushort right)
            => left < right ? right : left;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int FarthestFromZero(int left, int right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static uint FarthestFromZero(uint left, uint right)
            => left < right ? right : left;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static long FarthestFromZero(long left, long right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ulong FarthestFromZero(ulong left, ulong right)
            => left < right ? right : left;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static float FarthestFromZero(float left, float right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static double FarthestFromZero(double left, double right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static decimal FarthestFromZero(decimal left, decimal right)
            => Math.Abs(left) < Math.Abs(right) ? right : left;
        
        /// <summary>
        /// Returns the given argument with the largest magnitude
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static BigInteger FarthestFromZero(BigInteger left, BigInteger right)
            => left - right < 2 * left ^ left - right > 0 ? right : left;
    }
}
