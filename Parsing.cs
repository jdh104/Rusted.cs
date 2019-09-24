
using BigInteger = System.Numerics.BigInteger;

namespace Rusted
{
    public static class Parsing
    {
        public static Option<bool> ParseBool(this string @this)
            => bool.TryParse(@this, out bool tmp) ? Option.Some(tmp) : Option.None<bool>();

        public static Option<byte> ParseByte(this string @this)
            => byte.TryParse(@this, out byte tmp) ? Option.Some(tmp) : Option.None<byte>();

        public static Option<sbyte> ParseSByte(this string @this)
            => sbyte.TryParse(@this, out sbyte tmp) ? Option.Some(tmp) : Option.None<sbyte>();

        public static Option<decimal> ParseDecimal(this string @this)
            => decimal.TryParse(@this, out decimal tmp) ? Option.Some(tmp) : Option.None<decimal>();

        public static Option<double> ParseDouble(this string @this)
            => double.TryParse(@this, out double tmp) ? Option.Some(tmp) : Option.None<double>();

        public static Option<float> ParseFloat(this string @this)
            => float.TryParse(@this, out float tmp) ? Option.Some(tmp) : Option.None<float>();

        public static Option<int> ParseInt(this string @this)
            => int.TryParse(@this, out int tmp) ? Option.Some(tmp) : Option.None<int>();

        public static Option<uint> ParseUInt(this string @this)
            => uint.TryParse(@this, out uint tmp) ? Option.Some(tmp) : Option.None<uint>();

        public static Option<long> ParseLong(this string @this)
            => long.TryParse(@this, out long tmp) ? Option.Some(tmp) : Option.None<long>();

        public static Option<ulong> ParseULong(this string @this)
            => ulong.TryParse(@this, out ulong tmp) ? Option.Some(tmp) : Option.None<ulong>();

        public static Option<short> ParseShort(this string @this)
            => short.TryParse(@this, out short tmp) ? Option.Some(tmp) : Option.None<short>();

        public static Option<ushort> ParsUShort(this string @this)
            => ushort.TryParse(@this, out ushort tmp) ? Option.Some(tmp) : Option.None<ushort>();

        public static Option<BigInteger> ParseBigInteger(this string @this)
            => BigInteger.TryParse(@this, out BigInteger tmp) ? Option.Some(tmp) : Option.None<BigInteger>();
    }
}
