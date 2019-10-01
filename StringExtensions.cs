
using System;
using System.Linq;

namespace Rusted
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string. Parameters specify the starting search position in the current string, the number of characters in the current string to search, and the type of search to use for the specified string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="query"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool Contains(this string @this, string query, int startIndex = 0, int count = int.MaxValue, StringComparison strategy = StringComparison.InvariantCulture)
            => @this.IndexOf(query, startIndex, count.AtMost(@this.Length - startIndex), strategy) >= 0;

        /// <summary>
        /// Split a string into a maximum number of substrings based on the given character.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator">the Character to split on.</param>
        /// <param name="maxCount">the maximum number of strings in the returned array.</param>
        /// <param name="options"></param>
        /// <returns>An array of strings no larger than the size of the given maxCount</returns>
        public static string[] Split(this string @this, char separator, int maxCount = int.MaxValue, StringSplitOptions options = StringSplitOptions.None)
            => @this.Split(new char[] { separator }, maxCount, options);

        /// <summary>
        /// Split a string into a maximum number of substrings based on the given string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator">the Character to split on.</param>
        /// <param name="maxCount">the maximum number of strings in the returned array.</param>
        /// <param name="options"></param>
        /// <returns>An array of strings no larger than the size of the given maxCount</returns>
        public static string[] Split(this string @this, string separator, int maxCount = int.MaxValue, StringSplitOptions options = StringSplitOptions.None)
            => @this.Split(new string[] { separator }, maxCount, options);

        /// <summary>
        /// Check if this string starts with any of a number of arguments.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool StartsWithAny(this string @this, params string[] query)
            => query.Any(str => @this.StartsWith(str));

        /// <summary>
        /// Check if this string starts with any of a number of arguments. A parameter can be used to specify what type of StringComparison to use.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="comparison"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool StartsWithAny(this string @this, StringComparison comparison, params string[] query)
            => query.Any(str => @this.StartsWith(str, comparison));
    }
}
