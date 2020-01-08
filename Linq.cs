
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rusted
{
    public static class Linq
    {
        public static bool ContainsAny<T>(this IEnumerable<T> @this, IEnumerable<IEquatable<T>> others)
            => others.Any(other => other.EqualsAny(@this));

        public static bool ContainsAny<T, U>(this IEnumerable<T> @this, IEnumerable<U> others)
            where T: IEquatable<U>
        {
            return @this.Any(item => item.EqualsAny(others));
        }

        public static bool ContainsAny<T>(this IEnumerable<T> @this, params IEquatable<T>[] others)
            => others.Any(other => other.EqualsAny(@this));

        public static IEnumerable<string> Distinct(this IEnumerable<string> @this, StringComparison comparisonStrategy)
        {
            if (@this == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                List<string> lookup;

                if (@this is string[] array)
                {
                    lookup = new List<string>(array.Length);
                }
                else if (@this is IList<string> list)
                {
                    lookup = new List<string>(list.Count);
                }
                else
                {
                    lookup = new List<string>();
                }

                foreach (string element in @this)
                {
                    if (!lookup.Contains(element, comparisonStrategy))
                    {
                        yield return element;
                        lookup.Add(element);
                    }
                }

                yield break;
            }
        }
        
        public static TSource FirstOr<TSource>(this IEnumerable<TSource> @this, TSource def) => @this.Any() ? @this.First() : def;
        
        public static TSource FirstOrElse<TSource>(this IEnumerable<TSource> @this, Func<TSource> fallback) => @this.Any() ? @this.First() : fallback();

        public static Option<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> @this)
            => @this.Any() ? Option.Some(@this.First()) : Option.None<TSource>();
        
        public static TSource FirstOr<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate, TSource def)
        {
            foreach (TSource element in @this)
            {
                if (predicate(element))
                {
                    return element;
                }
            }
            return def;
        }

        public static TSource FirstOrElse<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate, Func<TSource> fallback)
        {
            foreach (TSource element in @this)
            {
                if (predicate(element))
                {
                    return element;
                }
            }
            return fallback();
        }

        public static Option<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate)
        {
            foreach (TSource element in @this)
            {
                if (predicate(element))
                {
                    return Option.Some(element);
                }
            }
            return Option.None<TSource>();
        }

        public static bool IsSingle<TSource>(this IEnumerable<TSource> @this)
            => @this.Take(2).Count() == 1;

        public static bool IsSingle<TSource>(this ICollection<TSource> @this)
            => @this.Count == 1;

        public static bool IsSingle<TSource>(this TSource[] @this)
            => @this.Length == 1;

        public static TSource LastOr<TSource>(this IEnumerable<TSource> @this, TSource def) => @this.Any() ? @this.Last() : def;
        
        public static TSource LastOrElse<TSource>(this IEnumerable<TSource> @this, Func<TSource> fallback) => @this.Any() ? @this.Last() : fallback();

        public static Option<TSource> LastOrNone<TSource>(this IEnumerable<TSource> @this)
            => @this.Any() ? Option.Some(@this.Last()) : Option.None<TSource>();
        
        public static TSource LastOr<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate, TSource def)
        {
            switch (@this)
            {
                case TSource[] array:
                    for (int i = array.Length - 1; i >= 0; i--)
                    {
                        if (predicate(array[i])) return array[i];
                    }
                    return def;

                case IList<TSource> list:
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (predicate(list[i])) return list[i];
                    }
                    return def;

                default:
                    TSource result = def;
                    foreach (TSource element in @this)
                    {
                        if (predicate(element)) result = element;
                    }
                    return result;
            }
        }

        public static TSource LastOrElse<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate, Func<TSource> fallback)
        {
            switch (@this)
            {
                case TSource[] array:
                    for (int i = array.Length - 1; i >= 0; i--)
                    {
                        if (predicate(array[i])) return array[i];
                    }
                    return fallback();

                case IList<TSource> list:
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (predicate(list[i])) return list[i];
                    }
                    return fallback();

                default:
                    TSource result = default;
                    bool found = false;
                    foreach (TSource element in @this)
                    {
                        if (predicate(element))
                        {
                            result = element;
                            found = true;
                        }
                    }
                    return found ? result : fallback();
            }
        }

        public static Option<TSource> LastOrNone<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate)
        {
            switch (@this)
            {
                case TSource[] array:
                    for (int i = array.Length - 1; i >= 0; i--)
                    {
                        if (predicate(array[i])) return array[i];
                    }
                    return Option.None<TSource>();

                case IList<TSource> list:
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (predicate(list[i])) return list[i];
                    }
                    return Option.None<TSource>();

                default:
                    TSource result = default;
                    bool found = false;
                    foreach (TSource element in @this)
                    {
                        if (predicate(element))
                        {
                            result = element;
                            found = true;
                        }
                    }
                    return found ? Option.Some(result) : Option.None<TSource>();
            }
        }

        public static TSource SingleOr<TSource>(this IEnumerable<TSource> @this, TSource def)
        {
            TSource[] sample = @this.Take(2).ToArray();
            if (sample.Length != 1)
            {
                return def;
            }
            else
            {
                return sample[0];
            }
        }

        public static TSource SingleOrElse<TSource>(this IEnumerable<TSource> @this, Func<TSource> fallback)
        {
            TSource[] sample = @this.Take(2).ToArray();
            if (sample.Length != 1)
            {
                return fallback();
            }
            else
            {
                return sample[0];
            }
        }

        public static Option<TSource> SingleOrNone<TSource>(this IEnumerable<TSource> @this)
        {
            TSource[] sample = @this.Take(2).ToArray();
            if (sample.Length != 1)
            {
                return Option.None<TSource>();
            }
            else
            {
                return sample[0];
            }
        }

        public static Option<TSource> MinOrNone<TSource>(this IEnumerable<TSource> @this)
            => @this.Any() ? @this.Min() : Option.None<TSource>();

        public static Option<TSource> MaxOrNone<TSource>(this IEnumerable<TSource> @this)
            => @this.Any() ? @this.Max() : Option.None<TSource>();

        public static bool AnyAndAll<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate) => @this.Any() && @this.All(predicate);

        public static IEnumerable<Option<T>> GetSomes<T>(this IEnumerable<Option<T>> @this)
            => @this.Where(option => option.IsSome());

        public static Result<IEnumerable<TSource>> Slice<TSource>(this TSource[] @this, int startIndex = 0, Option<int> endIndex = default)
        {
            if (Math.Abs(startIndex) > @this.Length || Math.Abs(endIndex.GetOrInsert(@this.Length)) > @this.Length)
            {
                return new IndexOutOfRangeException();
            }
            else
            {
                int sliceStartIndex, sliceLength;

                if (startIndex < 0)
                {
                    sliceStartIndex = @this.Length + startIndex;
                }
                else
                {
                    sliceStartIndex = startIndex;
                }

                if (endIndex.Unwrap() < 0)
                {
                    sliceLength = @this.Length + endIndex.Unwrap() - sliceStartIndex;
                }
                else
                {
                    sliceLength = endIndex.Unwrap() - sliceStartIndex;
                }

                if (sliceLength < 1)
                {
                    return Result.Ok(Enumerable.Empty<TSource>());
                }
                else
                {
                    return Result.Ok(@this.Skip(sliceStartIndex).Take(sliceLength));
                }
            }
        }

        public static IEnumerable<System.Text.RegularExpressions.Match> ToEnumerable(this System.Text.RegularExpressions.MatchCollection @this)
            => @this.Cast<System.Text.RegularExpressions.Match>();

        public static IEnumerable<T> UnwrapAll<T>(this IEnumerable<Option<T>> @this)
            => @this.Select(option => option.Unwrap());

        public static bool Contains(this IEnumerable<string> @this, string query, StringComparison stringComparison)
            => @this.Any(str => str.Equals(query, stringComparison));
    }
}
