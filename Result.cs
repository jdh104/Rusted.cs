using System;

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
    }

    public class Result<T> : IEquatable<Result<T>>, IEquatable<T>
    {
        public static implicit operator Result<T>(T t) => Result.Wrap<T>(t);

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
        
        public T UnwrapOr(T optb) 
            => ok ? wrapped : optb;
        
        public T UnwrapOrElse(Func<T> op) 
            => ok ? wrapped : op();
    }
}
