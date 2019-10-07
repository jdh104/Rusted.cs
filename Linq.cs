
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rusted
{
    public static class Linq
    {

        public static IEnumerable<string> Distinct(this IEnumerable<string> @this, StringComparison comparisonStrategy)
        {
            if (@this == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                List<string> lookup;

                if (@this is IList<string> list)
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

        public static TSource LastOr<TSource>(this IEnumerable<TSource> @this, TSource def) => @this.Any() ? @this.Last() : def;
        
        public static TSource LastOrElse<TSource>(this IEnumerable<TSource> @this, Func<TSource> fallback) => @this.Any() ? @this.Last() : fallback();
        
        public static TSource LastOr<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate, TSource def)
        {
            switch (@this)
            {
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
                case IList<TSource> list:
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        if (predicate(list[i])) return list[i];
                    }
                    return fallback();
                    
                default:
                    TSource result = default(TSource);
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

        public static bool AnyAndAll<TSource>(this IEnumerable<TSource> @this, Func<TSource, bool> predicate) => @this.Any() && @this.All(predicate);

        public static IEnumerable<Option<T>> Somes<T>(this IEnumerable<Option<T>> @this)
            => @this.Where(option => option.IsSome());

        public static IEnumerable<T> UnwrapAll<T>(this IEnumerable<Option<T>> @this)
            => @this.Select(option => option.Unwrap());

        public static bool Contains(this IEnumerable<string> @this, string query, StringComparison stringComparison)
            => @this.Any(str => str.Equals(query, stringComparison));
    }
}
