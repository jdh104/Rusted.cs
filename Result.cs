using System;

namespace Rusted
{
    public static class Result
    {
        public static Result<T, Exception> Err<T>(string err) => new Result<T, Exception>(new Exception(err));

        public static Result<T, E> Err<T, E>(E err)
            where E: Exception, new()
        {
            return new Result<T, E>(err);
        }

        public static Result<T, E> Ok<T, E>(T val) 
            where E: Exception, new()
        {
            return new Result<T, E>(val);
        }

        public static Result<T, E> Wrap<T, E>(T val)
            where E: Exception, new()
        {
            if (val == null)
            {
                return new Result<T, E>(val);
            }
            else
            {
                return new Result<T, E>(new E());
            }
        }

        public static bool Equals<E>(this Result<string, E> @this, Result<string, E> other, StringComparison stringComparison)
            where E: Exception, new()
        {
            return (@this.IsErr() && other.IsErr()) || (@this.IsOk() && @this.wrapped.Equals(other.wrapped, stringComparison));
        }

        public static bool Equals<E>(this Result<string, E> @this, string other, StringComparison stringComparison)
            where E : Exception, new()
        {
            return @this.IsOk() && @this.wrapped.Equals(other, stringComparison);
        }
    }

    public class Result<T, E> : IEquatable<Result<T, E>>, IEquatable<T> where E: Exception, new()
    {
        internal T wrapped;
        internal E error;
        internal bool ok;
        
        internal Result(T val)
        {
            this.ok = true;
            this.wrapped = val;
        }
        
        internal Result(E err)
        {
            this.ok = false;
            this.error = err ?? new E();
        }

        public bool Equals(Result<T, E> other)
            => this.ok && this.wrapped.Equals(other.wrapped);

        public bool Equals<X>(Result<T, X> other)
            where X : Exception, new()
        {
            return this.ok && this.wrapped.Equals(other.wrapped);
        }

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
        
        public Result<T, E2> MapErr<E2>(Func<E, E2> op) 
            where E2: Exception, new()
        {
            return ok ? new Result<T, E2>(wrapped) : new Result<T, E2>(op(error));
        }
        
        public Option<T> Ok() => ok ? new Option<T>(wrapped) : new Option<T>();
        
        public T UnwrapOr(T optb) => ok ? wrapped : optb;
        
        public T UnwrapOrElse(Func<T> op) => ok ? wrapped : op();
    }
}
