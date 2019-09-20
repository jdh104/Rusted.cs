using System;

namespace Rusted
{
    public static class Result
    {
        public static Result<T, E> Err<T, E>(E err) where E: Exception => new Result<T, E>(err);
        public static Result<T, E> Ok<T, E>(T val) where E: Exception => new Result<T, E>(val);
    }
    public class Result<T, E> where E: Exception
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
