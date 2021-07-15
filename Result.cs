
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rusted
{
    public static class Result
    {
        public static Result<T> Err<T>(string err)
            => new Result<T>(new Exception(err));

        public static Result<T> Err<T>(Exception err)
            => new Result<T>(err);

        public static Result<T> Ok<T>(T val)
            => new Result<T>(val);

        public static Result<T> Wrap<T>(T val)
        {
            if (val == null)
            {
                return new Result<T>(new Exception());
            }
            else
            {
                return new Result<T>(val);
            }
        }

        public static bool Equals(this Result<string> @this, Result<string> other, StringComparison stringComparison)
            => (@this.IsErr() && other.IsErr()) || (@this.IsOk() && @this.wrapped.Equals(other.wrapped, stringComparison));

        public static bool Equals(this Result<string> @this, string other, StringComparison stringComparison)
            => @this.IsOk() && @this.wrapped.Equals(other, stringComparison);

        //public static IEnumerable<S> UnwrapOrEmpty<T, S>(this Result<T> @this)
        //    where T : IEnumerable<S>
        //{
        //    return @this.Map(enumerable => enumerable.AsEnumerable()).UnwrapOr(Enumerable.Empty<S>());
        //}

        public static IEnumerable<T> UnwrapOrEmpty<T>(this Result<IEnumerable<T>> @this)
            => @this.UnwrapOr(Enumerable.Empty<T>());
    }

    public class Result<T> : IEquatable<Result<T>>, IEquatable<T>
    {
        public static implicit operator Result<T>(T t) => Result.Wrap(t);
        public static implicit operator Result<T>(Exception e) => Result.Err<T>(e);

        internal T wrapped;
        internal Exception error;
        internal bool ok;
        
        internal Result(T val)
        {
            this.ok = true;
            this.wrapped = val;
        }
        
        internal Result(Exception err)
        {
            this.ok = false;
            this.error = err ?? new Exception();
        }

        public bool Equals(Result<T> other)
            => this.ok && this.wrapped.Equals(other.wrapped);

        public bool Equals(T other)
            => this.ok && this.wrapped.Equals(other);

        /// <summary>
        /// Returns a string that represents the current Result.
        /// </summary>
        /// <returns>A string that represents the current Result.</returns>
        public override string ToString() 
            => ok ? $"Ok({wrapped.ToString()})" : $"Err({error.Message})";

        public Result<U> And<U>(Result<U> res) 
            => ok ? res : new Result<U>(error);
        
        public Result<U> AndThen<U>(Func<Result<U>> op) 
            => ok ? op() : new Result<U>(error);
        
        public Option<Exception> Err() 
            => ok ? new Option<Exception>() : new Option<Exception>(error);
        
        public bool IsOk() => ok;
        
        public bool IsErr() => !ok;
        
        public Result<U> Map<U>(Func<T, U> op) 
            => ok ? new Result<U>(op(wrapped)) : new Result<U>(error);
        
        public Result<U> MapOrElse<U>(Func<Exception, U> fallback, Func<T, U> map) 
            => ok ? new Result<U>(map(wrapped)) : new Result<U>(fallback(error));
        
        public Result<T> MapErr(Func<Exception, Exception> op) 
            => ok ? new Result<T>(wrapped) : new Result<T>(op(error));
        
        public Option<T> Ok() 
            => ok ? new Option<T>(wrapped) : new Option<T>();

        /// <summary>
        /// <para>Returns the value from the Result, assuming it is not an Err.</para>
        /// </summary>
        /// <exception cref="Exception">Thrown if the Result is an Err</exception>
        /// <seealso cref="UnwrapOr(T)"/>
        /// <seealso cref="UnwrapOrElse(Func{T})"/>
        public T Unwrap()
            => ok ? wrapped : throw new Exception("Tried to call Unwrap() on an Err result.");

        public T UnwrapOr(T optb) 
            => ok ? wrapped : optb;

        public T UnwrapOrElse(Func<T> op)
            => ok ? wrapped : op();

        public T UnwrapOrElse(Func<Exception, T> op)
            => ok ? wrapped : op(error);
    }
}
