using System;

namespace Rusted
{
    public static class Nullable
    {
        public static Option<T> ToOption<T>(this T? @this)
            where T: struct
        {
            return Option.Wrap(@this);
        }

        public static U? And<T, U>(this T? @this, U? optb)
            where T: struct
            where U: struct
        {
            return @this.HasValue ? optb : null;
        } 

        public static U? AndThen<T, U>(this T? @this, Func<U?> f)
            where T: struct
            where U: struct
        {
            return @this.HasValue ? f() : null;
        }

        public static T Expect<T>(this T? @this, string msg)
            where T: struct
        {
            return @this.HasValue ? @this.Value : throw new Exception(msg);
        }

        public static T? Filter<T>(this T? @this, Func<T, bool> predicate)
            where T: struct
        {
            return @this.HasValue && predicate(@this.Value) ? (T?)@this.Value : null;
        }

        // public static T GetOrInsert<T>(this T? @this, T v) where T: struct { }
        // public static T GetOrInsertWith<T>(this T? @this, Func<T> f) where T: struct { }

        public static U? Map<T, U>(this T? @this, Func<T, U> f)
            where T: struct
            where U: struct
        {
            return @this.HasValue ? (U?)f(@this.Value) : null;
        }

        public static U MapOr<T, U>(this T? @this, U def, Func<T, U> f)
            where T: struct
            where U: struct
        {
            return @this.HasValue ? f(@this.Value) : def;
        }

        public static U MapOrElse<T, U>(this T? @this, Func<U> def, Func<T, U> f)
            where T: struct
            where U: struct
        {
            return @this.HasValue ? f(@this.Value) : def();
        }

        public static Result<T> OkOr<T>(this T? @this, Exception err) 
            where T: struct
        {
            return @this.HasValue ? new Result<T>(@this.Value) : new Result<T>(err);
        }

        public static Result<T> OkOrElse<T>(this T? @this, Func<Exception> err) 
            where T: struct
        {
            return @this.HasValue ? new Result<T>(@this.Value) : new Result<T>(err());
        }

        public static T Unwrap<T>(this T? @this) 
            where T: struct
        {
            return @this.HasValue ? @this.Value : throw new Exception("Value is null");
        }

        public static T UnwrapOr<T>(this T? @this, T def)
            where T: struct
        {
            return @this.HasValue ? @this.Value : def;
        }
        
        public static T UnwrapOrElse<T>(this T? @this, Func<T> f)
            where T: struct
        {
            return @this.HasValue ? @this.Value : f();
        }
        
        public static T? Xor<T>(this T? @this, T? optb)
            where T: struct
        {
            if (!@this.HasValue)
            {
                if (!optb.HasValue)
                {
                    return null;
                }
                else
                {
                    return optb;
                }
            }
            else
            {
                if (!optb.HasValue)
                {
                    return @this;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
