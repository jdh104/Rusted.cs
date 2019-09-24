
using System;

namespace Rusted
{
    public static class StringExtensions
    {
        public static string[] Split(this string @this, char separator, int maxCount)
            => @this.Split(new char[] { separator }, maxCount);

        public static string[] Split(this string @this, char separator, int maxCount, StringSplitOptions options)
            => @this.Split(new char[] { separator }, maxCount, options);

        public static string[] Split(this string @this, string separator, int maxCount, StringSplitOptions options)
            => @this.Split(new string[] { separator }, maxCount, options);
    }
}
