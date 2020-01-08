
using System;
using System.Globalization;
using System.Numerics;

namespace Rusted
{
    public static class Parsing
    {
        public static Option<bool> ParseBool(this string @this)
            => bool.TryParse(@this, out bool tmp) ? Option.Some(tmp) : Option.None<bool>();

        public static Option<byte> ParseByte(this string @this, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => byte.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out byte tmp) ? Option.Some(tmp) : Option.None<byte>();

        public static Option<sbyte> ParseSByte(this string @this, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => sbyte.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out sbyte tmp) ? Option.Some(tmp) : Option.None<sbyte>();

        public static Option<decimal> ParseDecimal(this string @this, NumberStyles style = NumberStyles.Float, IFormatProvider provider = null)
            => decimal.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out decimal tmp) ? Option.Some(tmp) : Option.None<decimal>();

        public static Option<double> ParseDouble(this string @this, NumberStyles style = NumberStyles.Float, IFormatProvider provider = null)
            => double.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out double tmp) ? Option.Some(tmp) : Option.None<double>();

        public static Option<float> ParseFloat(this string @this, NumberStyles style = NumberStyles.Float, IFormatProvider provider = null)
            => float.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out float tmp) ? Option.Some(tmp) : Option.None<float>();

        public static Option<int> ParseInt(this string @this, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => int.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out int tmp) ? Option.Some(tmp) : Option.None<int>();

        public static Option<uint> ParseUInt(this string @this, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => uint.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out uint tmp) ? Option.Some(tmp) : Option.None<uint>();

        public static Option<long> ParseLong(this string @this, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => long.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out long tmp) ? Option.Some(tmp) : Option.None<long>();

        public static Option<ulong> ParseULong(this string @this, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => ulong.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out ulong tmp) ? Option.Some(tmp) : Option.None<ulong>();

        public static Option<short> ParseShort(this string @this, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => short.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out short tmp) ? Option.Some(tmp) : Option.None<short>();

        public static Option<ushort> ParseUShort(this string @this, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => ushort.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out ushort tmp) ? Option.Some(tmp) : Option.None<ushort>();

        public static Option<BigInteger> ParseBigInteger(this string @this, NumberStyles style = NumberStyles.Integer, IFormatProvider provider = null)
            => BigInteger.TryParse(@this, style, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), out BigInteger tmp) ? Option.Some(tmp) : Option.None<BigInteger>();

        public static Option<DateTime> ParseDateTime(this string @this, string format = null, IFormatProvider provider = null, DateTimeStyles styles = DateTimeStyles.None)
        {
            if (format == null)
            {
                return DateTime.TryParse(@this, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), styles, out DateTime tmp) ? Option.Some(tmp) : Option.None<DateTime>();
            }
            else
            {
                return DateTime.TryParseExact(@this, format, Option.Wrap(provider).UnwrapOr(CultureInfo.InvariantCulture), styles, out DateTime tmp) ? Option.Some(tmp) : Option.None<DateTime>();
            }
        }
    }
}
