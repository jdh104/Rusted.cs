using System;

namespace Rusted
{
    public static class Result
    {
        
        public static Result<T, E> Err<T, E>(E err) where E: Exception => new Result<T, E>(err);
        public static Result<T, E> Ok<T, E>(T val) where E: Exception => new Result<T, E>(val);

        public static bool Equals<E>(this Result<string, E> @this, Result<string, E> other, StringComparison stringComparison)
            where E: Exception
        {
            return (@this.IsErr() && other.IsErr()) || (@this.IsOk() && @this.wrapped.Equals(other.wrapped, stringComparison));
        }

        public static bool Equals<E>(this Result<string, E> @this, string other, StringComparison stringComparison)
            where E : Exception
        {
            return @this.IsOk() && @this.wrapped.Equals(other, stringComparison);
        }
    }

    public class Result<T> : IEquatable<Result<T>>, IEquatable<Result<T, Exception>>, IEquatable<T>
    {
        internal T wrapped;
        internal string error;
        internal bool ok;

        public Result(T val)
        {
            this.ok = true;
            this.wrapped = val;
        }

        public Result(string err)
        {
            this.ok = false;
            this.error = err;
        }

        public bool Equals(Result<T> other)
            => this.ok && this.wrapped.Equals(other.wrapped);

        public bool Equals<X>(Result<T, X> other)
            where X : Exception
        {
            return this.ok && this.wrapped.Equals(other.wrapped);
        }

        public bool Equals(Result<T, Exception> other)
            => this.ok && this.wrapped.Equals(other.wrapped);

        public bool Equals(T other)
            => this.ok && this.wrapped.Equals(other);

        /// <summary>
        /// Returns a string that represents the current Result.
        /// </summary>
        /// <returns>A string that represents the current Result.</returns>
        public override string ToString() => ok ? $"Ok({wrapped.ToString()})" : $"Err({error})";

        public Result<U> And<U>(Result<U> res) => ok ? res : new Result<U>(error);

        public Result<U> AndThen<U>(Func<Result<U>> op) => ok ? op() : new Result<U>(error);

        public Option<string> Err() => ok ? new Option<string>() : new Option<string>(error);

        public bool IsOk() => ok;

        public bool IsErr() => !ok;

        public Result<U> Map<U>(Func<T, U> op) => ok ? new Result<U>(op(wrapped)) : new Result<U>(error);

        public Result<U> MapOrElse<U>(Func<string, U> fallback, Func<T, U> map) => ok ? new Result<U>(map(wrapped)) : new Result<U>(fallback(error));

        public Result<T, E2> MapErr<E2>(Func<string, E2> op) where E2 : Exception => ok ? new Result<T, E2>(wrapped) : new Result<T, E2>(op(error));

        public Option<T> Ok() => ok ? new Option<T>(wrapped) : new Option<T>();

        public T UnwrapOr(T optb) => ok ? wrapped : optb;

        public T UnwrapOrElse(Func<T> op) => ok ? wrapped : op();
    }

    public class Result<T, E> : IEquatable<Result<T, E>>, IEquatable<Result<T>>, IEquatable<T> where E: Exception
    {
        internal T wrapped;
        internal E error;
        internal bool ok;
        
        public Result(T val)
        {
            this.ok = true;
            this.wrapped = val;
        }
        
        public Result(E err)
        {
            this.ok = false;
            this.error = err;
        }

        public bool Equals(Result<T, E> other)
            => this.ok && this.wrapped.Equals(other.wrapped);

        public bool Equals<X>(Result<T, X> other)
            where X : Exception
        {
            return this.ok && this.wrapped.Equals(other.wrapped);
        }

        public bool Equals(Result<T> other)
            => this.ok && this.wrapped.Equals(other.wrapped);

        public bool Equals(T other)
            => this.ok && this.wrapped.Equals(other);

        /// <summary>
        /// Returns a string that represents the current Result.
        /// </summary>
        /// <returns>A string that represents the current Result.</returns>
        public override string ToString() => ok ? $"Ok({wrapped.ToString()})" : $"Err({error.Message})";

        public Result<U, E> And<U>(Result<U, E> res) => ok ? res : new Result<U, E>(error);
        
        public Result<U, E> AndThen<U>(Func<Result<U, E>> op) => ok ? op() : new Result<U, E>(error);
        
        public Option<E> Err() => ok ? new Option<E>() : new Option<E>(error);
        
        public bool IsOk() => ok;
        
        public bool IsErr() => !ok;
        
        public Result<U, E> Map<U>(Func<T, U> op) => ok ? new Result<U, E>(op(wrapped)) : new Result<U, E>(error);
        
        public Result<U, E> MapOrElse<U>(Func<E, U> fallback, Func<T, U> map) => ok ? new Result<U, E>(map(wrapped)) : new Result<U, E>(fallback(error));
        
        public Result<T, E2> MapErr<E2>(Func<E, E2> op) where E2: Exception => ok ? new Result<T, E2>(wrapped) : new Result<T, E2>(op(error));
        
        public Option<T> Ok() => ok ? new Option<T>(wrapped) : new Option<T>();
        
        public T UnwrapOr(T optb) => ok ? wrapped : optb;
        
        public T UnwrapOrElse(Func<T> op) => ok ? wrapped : op();
    }
}
