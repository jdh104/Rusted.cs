
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rusted
{
    public static class Linq
    {
        
        public static TSource FirstOr<TSource>(this IEnumerable<TSource> @this, TSource def) => @this.Any() ? @this.First() : def;
        
        public static TSource FirstOrElse<TSource>(this IEnumerable<TSource> @this, Func<TSource> fallback) => @this.Any() ? @this.First() : fallback();
        
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
    }
}
