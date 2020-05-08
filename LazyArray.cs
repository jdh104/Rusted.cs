
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

        T[] FullyComputedSource;
        List<T> ComputedPart;
        IEnumerator<T> NonComputedPart;

        public int Count()
            => GetOrEvaluateSource().Length;

        /// <summary>
        /// Returns a string that represents the current LazyArray.
        /// </summary>
        /// <returns>A string that represents the current LazyArray.</returns>
        public override string ToString()
            => $"LazyArray: [ {(IsValueComputed ? $"Evaluated: {FullyComputedSource}" : "Not yet evaluated")} ]";

        public T this[int index]
        {
            get => this.Skip(index).FirstOrElse(() => throw new IndexOutOfRangeException());
            set => GetOrEvaluateSource()[index] = value;
        }

        public bool IsValueComputed => FullyComputedSource != null;

        private T[] GetOrEvaluateSource()
        {
            if (FullyComputedSource == null)
            {
                FullyComputedSource = ComputedPart.Concat(NonComputedPart.YieldToEnd()).ToArray();
            }

            return FullyComputedSource;
        }

        public void CopyTo(T[] array, int index)
            => GetOrEvaluateSource().CopyTo(array, index);

        public void CopyTo(T[] array, long index)
            => GetOrEvaluateSource().CopyTo(array, index);

        private IEnumerator<T> YieldWhileEvaluating()
        {
            if (FullyComputedSource == null)
            {
                foreach (T previouslyComputedItem in ComputedPart)
                {
                    yield return previouslyComputedItem;
                }

                foreach (T newlyComputedItem in NonComputedPart.YieldToEnd())
                {
                    // it is imperative that we add before yielding
                    ComputedPart.Add(newlyComputedItem);
                    yield return newlyComputedItem;
                }

                FullyComputedSource = ComputedPart.ToArray();
            }
            else
            {
                ComputedPart = null;
                NonComputedPart = null;

                foreach (T computedItem in FullyComputedSource)
                {
                    yield return computedItem;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (FullyComputedSource == null)
            {
                return YieldWhileEvaluating();
            }
            else
            {
                // this is done to avoid explicit conversion (i.e. potential runtime exception).
                IEnumerable<T> enumerable = FullyComputedSource;
                return enumerable.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public LazyArray(IEnumerable<T> source)
        {
            if (source is T[] array)
            {
                // do not keep reference of array, make copy
                FullyComputedSource = new T[array.Length];
                array.CopyTo(FullyComputedSource, 0);
                Lazy<int> a = new Lazy<int>(() => 1);
            }
            else
            {
                ComputedPart = new List<T>();
                NonComputedPart = source.GetEnumerator();
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
