
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rusted
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string. Parameters specify 
        /// the starting search position in the current string, the number of characters in the current string to 
        /// search, and the type of search to use for the specified string.
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
        /// Returns a value indicating whether a specified substring occurs within this 
        /// string. A Parameter specifies the type of search to use for the specified string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="query"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool Contains(this string @this, string query, StringComparison strategy)
            => @this.Contains(query, 0, @this.Length, strategy);

        /// <summary>
        /// Returns a value indicating whether at least one of many specified substrings occurs within this string. 
        /// Parameters specify the starting search position in the current string, the number of characters in 
        /// the current string to search, and the type of search to use for the specified string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="query"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string @this, int startIndex = 0, int count = int.MaxValue, StringComparison strategy = StringComparison.InvariantCulture, params string[] queries)
            => queries.Any(query => @this.IndexOf(query, startIndex, count.AtMost(@this.Length - startIndex), strategy) >= 0);

        /// <summary>
        /// Returns a value indicating whether at least one of many specified substrings occurs within this string. 
        /// Parameters specify the type of search to use for the specified string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="query"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string @this, StringComparison strategy = StringComparison.InvariantCulture, params string[] queries)
            => @this.ContainsAny(0, @this.Length, strategy, queries);

        /// <summary>
        /// Check if this string is equal to any of a set of other strings. Parameters specify the StringComparison
        /// type to use.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="comparison"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static bool EqualsAny(this string @this, IEnumerable<string> others, StringComparison comparison = StringComparison.InvariantCulture)
            => others.Any(other => @this.Equals(other, comparison));

        /// <summary>
        /// Check if this string is equal to any of a set of other strings. Parameters specify the StringComparison
        /// type to use.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="comparison"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static bool EqualsAny(this string @this, StringComparison comparison = StringComparison.InvariantCulture, params string[] others)
            => others.Any(other => @this.Equals(other, comparison));

        /// <summary>
        /// Returns a substring of this string using rules similar to Python's string slice mechanism, where
        /// negative numbers can be used as indexing from the end of the string instead of the beginning.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <example><code>"Hello World".Slice(0, -4)   // returns Result.Ok("Hello W")</code></example>
        /// <example><code>"Hello World".Slice(-5)      // returns Result.Ok("World")</code></example>
        /// <example><code>"Hello World".Slice(1, 5)    // returns Result.Ok("ello")</code></example>
        /// <example><code>"Hello World".Slice(-10, -6) // also returns Result.Ok("ello")</code></example>
        /// <returns></returns>
        public static Result<string> Slice(this string @this, int startIndex = 0, Option<int> endIndex = default)
        {
            if (Math.Abs(startIndex) > @this.Length || Math.Abs(endIndex.GetOrInsert(@this.Length)) > @this.Length)
            {
                return new IndexOutOfRangeException();
            }
            else
            {
                int substringStartIndex, substringCount;

                if (startIndex < 0)
                {
                    substringStartIndex = @this.Length + startIndex;
                }
                else
                {
                    substringStartIndex = startIndex;
                }

                if (endIndex.Unwrap() < 0)
                {
                    substringCount = @this.Length + endIndex.Unwrap() - substringStartIndex;
                }
                else
                {
                    substringCount = endIndex.Unwrap() - substringStartIndex;
                }

                if (substringCount < 1)
                {
                    return "";
                }
                else
                {
                    return @this.Substring(substringStartIndex, substringCount);
                }
            }
        }

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
        /// Split a string into a maximum number of substrings based on the given string(s).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separators">the strings to split on.</param>
        /// <returns>An array of strings no larger than the size of the given maxCount</returns>
        public static string[] Split(this string @this, params string[] separators)
            => @this.Split(separators, int.MaxValue, StringSplitOptions.None);

        /// <summary>
        /// Split a string into a maximum number of substrings based on the given string(s).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separators">the strings to split on.</param>
        /// <param name="maxCount">the maximum number of strings in the returned array.</param>
        /// <returns>An array of strings no larger than the size of the given maxCount</returns>
        public static string[] Split(this string @this, int maxCount, params string[] separators)
            => @this.Split(separators, maxCount, StringSplitOptions.None);

        /// <summary>
        /// Split a string into a maximum number of substrings based on the given string(s).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separators">the strings to split on.</param>
        /// <param name="options"></param>
        /// <returns>An array of strings no larger than the size of the given maxCount</returns>
        public static string[] Split(this string @this, StringSplitOptions options, params string[] separators)
            => @this.Split(separators, int.MaxValue, options);

        /// <summary>
        /// Split a string into a maximum number of substrings based on the given string(s).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separators">the strings to split on.</param>
        /// <param name="maxCount">the maximum number of strings in the returned array.</param>
        /// <param name="options"></param>
        /// <returns>An array of strings no larger than the size of the given maxCount</returns>
        public static string[] Split(this string @this, int maxCount, StringSplitOptions options, params string[] separators)
            => @this.Split(separators, maxCount, options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="indexes"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Yielded when any of the index arguments are out of range of the length of the string</exception>
        public static IEnumerable<string> SplitAtIndexes(this string @this, params int[] indexes)
        {
            int previousIndex = 0;
            foreach (int nextIndex in indexes)
            {
                yield return @this.Substring(previousIndex, nextIndex - previousIndex);
                previousIndex = nextIndex;
            }

            yield return @this.Substring(previousIndex);
        }

        /// <summary>
        /// Check if this string starts with any of a number of arguments.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool StartsWithAny(this string @this, IEnumerable<string> query)
            => query.Any(str => @this.StartsWith(str));

        /// <summary>
        /// Check if this string starts with any of a number of arguments. A parameter can be 
        /// used to specify what type of StringComparison to use.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="comparison"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool StartsWithAny(this string @this, IEnumerable<string> query, StringComparison comparison)
            => query.Any(str => @this.StartsWith(str, comparison));

        /// <summary>
        /// Check if this string starts with any of a number of arguments.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool StartsWithAny(this string @this, params string[] query)
            => query.Any(str => @this.StartsWith(str));

        /// <summary>
        /// Check if this string starts with any of a number of arguments. A parameter can be 
        /// used to specify what type of StringComparison to use.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="comparison"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool StartsWithAny(this string @this, StringComparison comparison, params string[] query)
            => query.Any(str => @this.StartsWith(str, comparison));
    }
}
