
using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

namespace Rusted
{
    public class LazyArray<T> : IEnumerable<T>
    {
        public static implicit operator LazyArray<T>(Lazy<T[]> t) => LazyArray.From(() => t.Value);
        public static implicit operator Lazy<T[]>(LazyArray<T> t) => new Lazy<T[]>(() => t.GetOrEvaluateSource());

        public static LazyArray<T> Empty()
            => new LazyArray<T>(Enumerable.Empty<T>());

        Option<T[]> ComputedSource;
        Option<IEnumerable<T>> LazySource;

        public int Count()
            => GetOrEvaluateSource().Length;

        /// <summary>
        /// Returns a string that represents the current LazyArray.
        /// </summary>
        /// <returns>A string that represents the current LazyArray.</returns>
        public override string ToString()
            => $"LazyArray: [ {ComputedSource.Map(array => $"Evaluated: {array.ToString()}").UnwrapOrElse(() => $"Not Evaluated: {LazySource.UnwrapOrEmpty().ToString()}")} ]";

        public T this[int index]
        {
            get => GetOrEvaluateSource()[index];
            set => GetOrEvaluateSource()[index] = value;
        }

        private T[] GetOrEvaluateSource()
            => ComputedSource.UnwrapOrElse(() =>
            {
                T[] result = LazySource.Unwrap().ToArray();
                LazySource = Option.None<IEnumerable<T>>();
                ComputedSource = Option.Some(result);
                return result;
            });

        public void CopyTo(T[] array, int index)
            => GetOrEvaluateSource().CopyTo(array, index);

        public void CopyTo(T[] array, long index)
            => GetOrEvaluateSource().CopyTo(array, index);


        public IEnumerator<T> GetEnumerator()
        {
            if (ComputedSource.IsSome())
            {
                // this is done to avoid explicit conversion (i.e. potential runtime exception).
                IEnumerable<T> enumerable = ComputedSource.Unwrap();
                return enumerable.GetEnumerator();
            }
            else
            {
                IEnumerator<T> yieldWhileEvaluating()
                {
                    List<T> items = new List<T>();
                    foreach (T item in LazySource.Unwrap())
                    {
                        yield return item;
                        items.Add(item);
                    }

                    LazySource = Option.None<IEnumerable<T>>();
                    ComputedSource = items.ToArray();
                }

                return yieldWhileEvaluating();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public LazyArray(IEnumerable<T> source)
        {
            if (source is T[] array)
            {
                T[] tmp = new T[array.Length];
                array.CopyTo(tmp, 0);
                ComputedSource = tmp;
            }
            else
            {
                LazySource = Option.Some(source);
            }
        }
    }

    public static class LazyArray
    {
        public static LazyArray<T> Empty<T>()
            => new LazyArray<T>(new T[0]);

        public static LazyArray<T> From<T>(IEnumerable<T> enumerable)
            => new LazyArray<T>(enumerable);

        public static LazyArray<T> From<T>(Func<IEnumerable<T>> generator)
            => new LazyArray<T>(generator());

        public static LazyArray<T> ToLazyArray<T>(this IEnumerable<T> @this)
        {
            return new LazyArray<T>(@this);
        }
    }
}
