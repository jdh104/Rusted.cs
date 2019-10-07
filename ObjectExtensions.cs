
using System;
using System.Linq;

namespace Rusted
{
    public static class ObjectExtensions
    {
        public static bool EqualsAny<T, U>(this T @this, params U[] others)
            where T: IEquatable<U>
        {
            return others.Any(other => @this.Equals(other));
        }

        public static T Or<T>(this T @this, T alt)
        {
            if (@this.Equals(default(T)))
            {
                return alt;
            }
            else
            {
                return @this;
            }
        }

        public static T OrElse<T>(this T @this, Func<T> fallback)
        {
            if (@this.Equals(default(T)))
            {
                return fallback();
            }
            else
            {
                return @this;
            }
        }

        public static Option<T> Not<T>(this T @this, T other)
        {
            if (@this.Equals(other))
            {
                return Option.None<T>();
            }
            else
            {
                return Option.Some(@this);
            }
        }

        public static T AtLeast<T>(this T @this, T other)
            where T: IComparable<T>
        {
            switch (@this.CompareTo(other))
            {
                case -1:
                    return other;

                default:
                    return @this;
            }
        }

        public static T AtMost<T>(this T @this, T other)
            where T: IComparable<T>
        {
            switch (@this.CompareTo(other))
            {
                case 1:
                    return other;

                default:
                    return @this;
            }
        }
    }
}
