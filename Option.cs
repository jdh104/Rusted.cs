using System;

namespace Rusted
{
    public static class Option
    {
        public static Option<object> None() => new Option<object>();
        
        public static Option<U> None<U>() => new Option<U>();
        
        public static Option<U> Some<U>(U value) => value == null ? throw new ArgumentNullException("Argument is null") : new Option<U>(value);
        
        public static Option<U> Wrap<U>(U value) => value == null ? new Option<U>() : new Option<U>(value);

        public static Result<Option<T>, E> Transpose<T, E>(this Option<Result<T, E>> @this)
            where E: Exception
        {
            if (!@this.some)
            {
                return new Result<Option<T>, E>(new Option<T>());
            }
            else if (!@this.wrapped.ok)
            {
                return new Result<Option<T>, E>(@this.wrapped.error);
            }
            else
            {
                return new Result<Option<T>, E>(@this.wrapped.Ok());
            }
        }

        public static bool Equals(this Option<string> @this, Option<string> other, StringComparison stringComparison)
            => (@this.IsNone() && other.IsNone()) || (@this.IsSome() && @this.wrapped.Equals(other.wrapped, stringComparison));

        public static bool Equals(this Option<string> @this, string other, StringComparison stringComparison)
            => (@this.IsNone() && other == null) || (@this.IsSome() && @this.wrapped.Equals(other, stringComparison));
    }
    
    public class Option<T> : IEquatable<Option<T>>, IEquatable<T>
    {
        internal bool some;
        internal T wrapped;
        
        internal Option()
        {
            this.some = false;
        }
        
        internal Option(T val)
        {
            this.some = true;
            this.wrapped = val;
        }

        public bool Equals(Option<T> other) => this.wrapped.Equals(other.wrapped);

        public bool Equals(T other) => this.wrapped.Equals(other);
        
        public Option<U> And<U>(Option<U> optb) => some ? optb : new Option<U>();
        
        public Option<U> AndThen<U>(Func<Option<U>> f) => some ? f() : new Option<U>();
        
        public T Expect(string msg) => some ? wrapped : throw new Exception(msg);
        
        public Option<T> Filter(Func<T, bool> predicate) => some && predicate(wrapped) ? new Option<T>(wrapped) : new Option<T>();
        
        public T GetOrInsert(T v)
        {
            if (v == null)
            {
                throw new ArgumentNullException("Argument is null");
            }
            else if (!some)
            {
                some = true;
                return wrapped = v;
            }
            else
            {
                return wrapped;
            }
        }
        
        public T GetOrInsertWith(Func<T> f)
        {
            if (!some)
            {
                T val = f();
                if (val == null)
                {
                    throw new ArgumentException("Callback returned null");
                }
                else
                {
                    some = true;
                    return wrapped = val;
                }
            }
            else
            {
                return wrapped;
            }
        }
        
        public bool IsSome() => some;
        
        public bool IsNone() => !some;
        
        public Option<U> Map<U>(Func<T, U> f)
        {
            if (!this.some)
            {
                return new Option<U>();
            }
            else
            {
                U val = f(wrapped);
                if (val == null)
                {
                    return new Option<U>();
                }
                else
                {
                    return new Option<U>(val);
                }
            }
        }
        
        public U MapOr<U>(U def, Func<T, U> f) => some ? f(wrapped) : def;
        
        public U MapOrElse<U>(Func<U> def, Func<T, U> f) => some ? f(wrapped) : def();
        
        public Result<T, E> OkOr<E>(E err) where E: Exception => some ? new Result<T, E>(wrapped) : new Result<T, E>(err);
        
        public Result<T, E> OkOrElse<E>(Func<E> err) where E: Exception => some ? new Result<T, E>(wrapped) : new Result<T, E>(err());
        
        public Option<T> Replace(T val)
        {
            if (val == null)
            {
                if (!this.some)
                {
                    return new Option<T>();
                }
                else
                {
                    this.some = false;
                    T old = this.wrapped;
                    this.wrapped = default(T);
                    return new Option<T>(old);
                }
            }
            else if (!some)
            {
                this.some = true;
                this.wrapped = val;
                return new Option<T>();
            }
            else
            {
                T old = this.wrapped;
                this.wrapped = val;
                return new Option<T>(old);
            }
        }
        
        public Option<T> Take()
        {
            if (!this.some)
            {
                return new Option<T>();
            }
            else
            {
                this.some = false;
                T old = this.wrapped;
                this.wrapped = default(T);
                return new Option<T>(old);
            }
        }
        
        public T Unwrap() => some ? wrapped : throw new Exception("Value is null");
        
        public T UnwrapOr(T def) => some ? wrapped : def;
        
        public T UnwrapOrElse(Func<T> f) => some ? wrapped : f();
        
        public Option<T> Xor(Option<T> optb)
        {
            if (!this.some)
            {
                if (!optb.some)
                {
                    return new Option<T>();
                }
                else
                {
                    return optb;
                }
            }
            else
            {
                if (!optb.some)
                {
                    return this;
                }
                else
                {
                    return new Option<T>();
                }
            }
        }
    }
}
