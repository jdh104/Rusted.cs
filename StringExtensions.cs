
using System;

namespace Rusted
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string. A parameter specifies the type of search to use for the specified string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="query"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool Contains(this string @this, string query, StringComparison stringComparison)
            => @this.IndexOf(query, stringComparison) >= 0;

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string. Parameters specify the starting search position in the current string and the type of search to use for the specified string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="query"></param>
        /// <param name="startIndex"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool Contains(this string @this, string query, int startIndex, StringComparison stringComparison)
            => @this.IndexOf(query, startIndex, stringComparison) >= 0;

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string. Parameters specify the starting search position in the current string, the number of characters in the current string to search, and the type of search to use for the specified string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="query"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool Contains(this string @this, string query, int startIndex, int count, StringComparison stringComparison)
            => @this.IndexOf(query, startIndex, count, stringComparison) >= 0;

        /// <summary>
        /// Split a string into a maximum number of substrings based on the given character.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator">the Character to split on.</param>
        /// <param name="maxCount">the maximum number of strings in the returned array.</param>
        /// <returns>An array of strings no larger than the size of the given maxCount</returns>
        public static string[] Split(this string @this, char separator, int maxCount)
            => @this.Split(new char[] { separator }, maxCount);

        /// <summary>
        /// Split a string into a maximum number of substrings based on the given character.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator">the Character to split on.</param>
        /// <param name="maxCount">the maximum number of strings in the returned array.</param>
        /// <param name="options"></param>
        /// <returns>An array of strings no larger than the size of the given maxCount</returns>
        public static string[] Split(this string @this, char separator, int maxCount, StringSplitOptions options)
            => @this.Split(new char[] { separator }, maxCount, options);

        /// <summary>
        /// Split a string into a maximum number of substrings based on the given string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator">the Character to split on.</param>
        /// <param name="maxCount">the maximum number of strings in the returned array.</param>
        /// <param name="options"></param>
        /// <returns>An array of strings no larger than the size of the given maxCount</returns>
        public static string[] Split(this string @this, string separator, int maxCount, StringSplitOptions options)
            => @this.Split(new string[] { separator }, maxCount, options);
    }
}
