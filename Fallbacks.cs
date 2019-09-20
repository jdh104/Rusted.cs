using System;

namespace Rusted
{
    public static class Fallbacks
    {
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
    }
}
