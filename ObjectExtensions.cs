
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rusted
{
    public static class ObjectExtensions
    {
        public static bool EqualsAny<T, U>(this T @this, IEnumerable<U> others)
            where T : IEquatable<U>
        {
            return others.Any(other => @this.Equals(other));
        }

        public static bool EqualsAny<T, U>(this T @this, params U[] others)
            where T : IEquatable<U>
        {
            return others.Any(other => @this.Equals(other));
        }

        public static bool EqualsAll<T, U>(this T @this, IEnumerable<U> others)
            where T : IEquatable<U>
        {
            return others.All(other => @this.Equals(other));
        }

        public static bool EqualsAll<T, U>(this T @this, params U[] others)
            where T : IEquatable<U>
        {
            return others.All(other => @this.Equals(other));
        }

        public static bool EqualsEquals<T, U>(this T @this, U other)
            where U : IEquatable<T>
        {
            return @this.Equals(other);
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

        public static Option<T> Not<T, U>(this T @this, U other)
            where T : IEquatable<U>
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

        public static Option<T> NotAny<T, U>(this T @this, params U[] others)
            where T : IEquatable<U>
        {
            if (@this.EqualsAny(others))
            {
                return Option.None<T>();
            }
            else
            {
                return Option.Some(@this);
            }
        }

        /// <summary>
        /// If this is greater than other, return this, else return other.
        /// This is useful when setting some kind of minimum limit.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="other"></param>
        /// <returns></returns>
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

        /// <summary>
        /// If this is less than other, return this, else return other.
        /// This is useful when setting some kind of maximum limit.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="other"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert a single object to an IEnumerable containing only that object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<T> Yield<T>(this T @this)
        {
            yield return @this;
        }
    }
}
