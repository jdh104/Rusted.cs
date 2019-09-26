using System;

namespace Rusted
{
    public static class Option
    {
        /// <summary>
        /// Initialize a None.
        /// </summary>
        public static Option<object> None() => new Option<object>();
        
        /// <summary>
        /// Initialize a generic None.
        /// </summary>
        public static Option<U> None<U>() => new Option<U>();
        
        /// <summary>
        /// Initialize a Some using the given argument.
        /// </summary>
        public static Option<U> Some<U>(U value) => value == null ? throw new ArgumentNullException("Argument is null") : new Option<U>(value);
        
        /// <summary>
        /// Initialize a None if the given argument is null, otherwise initialize a Some.
        /// </summary>
        public static Option<U> Wrap<U>(U value) => value == null ? new Option<U>() : new Option<U>(value);

        /// <summary>
        /// Initialize a None if the given Nullable's HasValue property is false, otherwise initialize a Some.
        /// </summary>
        public static Option<U> Wrap<U>(U? value)
            where U: struct
        {
            if (!value.HasValue)
            {
                return new Option<U>();
            }
            else
            {
                return new Option<U>(value.Value);
            }
        }

        /// <summary>
        /// <para>Transposes an Option of a Result into a Result of an Option.</para>
        /// <para>None will be mapped to Ok(None). Some(Ok(_)) and Some(Err(_)) will be mapped to Ok(Some(_)) and Err(_), respectively.</para>
        /// </summary>
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

        /// <summary>
        /// Determines whether this option and a specified option have the same value. A parameter specifies the culture, case, and sort rules used in the comparison.
        /// </summary>
        public static bool Equals(this Option<string> @this, Option<string> other, StringComparison stringComparison)
            => (@this.IsNone() && other.IsNone()) || (@this.IsSome() && @this.wrapped.Equals(other.wrapped, stringComparison));

        /// <summary>
        /// Determines whether this option's value is equal to the specified value. A parameter specifies the culture, case, and sort rules used in the comparison.
        /// </summary>
        public static bool Equals(this Option<string> @this, string other, StringComparison stringComparison)
            => (@this.IsNone() && other == null) || (@this.IsSome() && @this.wrapped.Equals(other, stringComparison));
    }
    
    public struct Option<T> : IEquatable<Option<T>>, IEquatable<T>
    {
        internal bool some;
        internal T wrapped;
        
        /// <summary>
        /// Initialize a Some using the given argument.
        /// </summary>
        internal Option(T val)
        {
            this.some = true;
            this.wrapped = val;
        }

        /// <summary>
        /// Determines whether this option and a specified option have the same value.
        /// </summary>
        public bool Equals(Option<T> other) => this.wrapped.Equals(other.wrapped);

        /// <summary>
        /// Determines whether this option's value is equal to the specified value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(T other) => this.wrapped.Equals(other);

        /// <summary>
        /// Returns a string that represents the current Option.
        /// </summary>
        /// <returns>A string that represents the current Option.</returns>
        public override string ToString() => some ? $"Some({wrapped.ToString()})" : "None";

        /// <summary>
        /// Returns None if the option is None, otherwise returns optb
        /// </summary>
        public Option<U> And<U>(Option<U> optb) => some ? optb : new Option<U>();

        /// <summary>
        /// <para>Returns None if the option is None, otherwise calls f with the wrapped value and returns the result.</para>
        /// <para>Some languages call this operation flatmap.</para>
        /// </summary>
        public Option<U> AndThen<U>(Func<Option<U>> f) => some ? f() : new Option<U>();

        /// <summary>
        /// <para>Unwraps an option, yielding the content of a Some.</para>
        /// <para>Throws an exception if the option is a None.</para>
        /// </summary>
        /// <exception cref="Exception">Thrown if the value is a None with a custom error message provided by msg.</exception>
        public T Expect(string msg) => some ? wrapped : throw new Exception(msg);

        /// <summary>
        /// <para>Unwraps an option, yielding the content of a Some.</para>
        /// <para>Throws an exception if the option is a None.</para>
        /// </summary>
        /// <exception cref="Exception">Thrown if the value is a None with a custom error message provided by msg.</exception>
        public T ExpectWith(Func<string> f) => some ? wrapped : throw new Exception(f());

        /// <summary>
        /// <para>
        /// Returns None if the option is None, otherwise calls predicate with the wrapped value and returns:
        /// </para>
        /// <list type="bullet">
        /// <item>
        /// <description>Some(t) if predicate returns true (where t is the wrapped value).</description>
        /// </item>
        /// <item>
        /// <description>None if predicate returns false.</description>
        /// </item>
        /// </list>
        /// </summary>
        public Option<T> Filter(Func<T, bool> predicate) => some && predicate(wrapped) ? new Option<T>(wrapped) : new Option<T>();

        /// <summary>
        /// Inserts v into the option if it is None, then returns the contained value.
        /// </summary>
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

        /// <summary>
        /// Inserts a value computed from f into the option if it is None, then returns the contained value.
        /// </summary>
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
        
        /// <summary>
        /// Returns true if the option is a Some value.
        /// </summary>
        public bool IsSome() => some;
        
        /// <summary>
        /// Returns true if the option is a None value
        /// </summary>
        /// <returns></returns>
        public bool IsNone() => !some;

        /// <summary>
        /// <para>If the option is a None, return a None. Otherwise:</para>
        /// <para>Maps an Option&lt;T&gt; to Option&lt;U&gt; by applying a function to a contained value.</para>
        /// </summary>
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

        /// <summary>
        /// Applies a function to the contained value (if any), or returns the provided default (if not).
        /// </summary>
        public U MapOr<U>(U def, Func<T, U> f) => some ? f(wrapped) : def;

        /// <summary>
        /// Applies a function to the contained value (if any), or computes a default (if not).
        /// </summary>
        public U MapOrElse<U>(Func<U> def, Func<T, U> f) => some ? f(wrapped) : def();

        /// <summary>
        /// <para>Transforms the Option&lt;T&gt; into a Result&lt;T, E&gt;, mapping Some(v) to Ok(v) and None to Err(err).</para>
        /// <para>Arguments passed to OkOr are eagerly evaluated; if you are passing the result of a function call, it is recommended to use <see cref="OkOrElse{E}(Func{E})"/>, which is lazily evaluated.</para>
        /// </summary>
        public Result<T, E> OkOr<E>(E err) where E: Exception => some ? new Result<T, E>(wrapped) : new Result<T, E>(err);

        /// <summary>
        /// Transforms the Option&lt;T&gt; into a Result&lt;T, E&gt;, mapping Some(v) to Ok(v) and None to Err(err()).
        /// </summary>
        public Result<T, E> OkOrElse<E>(Func<E> err) where E: Exception => some ? new Result<T, E>(wrapped) : new Result<T, E>(err());

        /// <summary>
        /// Replaces the actual value in the option by the value given in parameter, returning the old value if present, leaving a Some in its place.
        /// </summary>
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
                    this.wrapped = default;
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

        /// <summary>
        /// Takes the value out of the option, leaving a None in its place.
        /// </summary>
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
                this.wrapped = default;
                return new Option<T>(old);
            }
        }
        
        /// <summary>
        /// <para>Returns the value from the option, assuming it is not a None.</para>
        /// </summary>
        /// <exception cref="Exception">Thrown if the option is a None</exception>
        /// <seealso cref="UnwrapOr(T)"/>
        /// <seealso cref="UnwrapOrElse(Func{T})"/>
        public T Unwrap() => some ? wrapped : throw new Exception("Tried to call Unwrap() on a None option.");

        /// <summary>
        /// <para>Returns the contained value or a default.</para>
        /// <para>Arguments passed to UnwrapOr are eagerly evaluated; if you are passing the result of a function call, it is recommended to use <see cref="UnwrapOrElse(Func{T})"/>, which is lazily evaluated.</para>
        /// </summary>
        public T UnwrapOr(T def) => some ? wrapped : def;

        /// <summary>
        /// Returns the contained value or computes a default.
        /// </summary>
        public T UnwrapOrElse(Func<T> f) => some ? wrapped : f();

        /// <summary>
        /// Returns Some if exactly one of this, optb is Some, otherwise returns None.
        /// </summary>
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
