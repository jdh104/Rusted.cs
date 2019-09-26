using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

namespace Rusted
{
    public class LazyArray<T> : IEnumerable<T>
    {
        public static LazyArray<T> Empty()
            => new LazyArray<T>(Enumerable.Empty<T>());

        readonly Lazy<T[]> Source;

        /// <summary>
        /// Returns a string that represents the current LazyArray.
        /// </summary>
        /// <returns>A string that represents the current LazyArray.</returns>
        public override string ToString() => Source.Value.ToString();

        public T this[int index]
        {
            get => Source.Value[index];
            set => Source.Value[index] = value;
        }

        public int Length => Source.Value.Length;

        public void CopyTo(T[] array, int index)
            => Source.Value.CopyTo(array, index);

        public void CopyTo(T[] array, long index)
            => Source.Value.CopyTo(array, index);

        public IEnumerator<T> GetEnumerator()
        {
            // This is done to avoid explicit casting
            IEnumerable<T> enumerable = Source.Value;
            return enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
            => Source.Value.GetEnumerator();

        public LazyArray(IEnumerable<T> source)
        {
            Source = new Lazy<T[]>(() => source == null ? new T[0] : source.ToArray());
        }

        public LazyArray(Lazy<T[]> source)
        {
            Source = source;
        }

        internal LazyArray(Func<T[]> generator)
        {
            Source = new Lazy<T[]>(generator);
        }
    }

    public static class LazyArray
    {
        public static LazyArray<T> Empty<T>()
            => new LazyArray<T>(Enumerable.Empty<T>());

        public static LazyArray<T> From<T>(Func<IEnumerable<T>> generator)
            => new LazyArray<T>(() => generator().ToArray());

        public static LazyArray<T> From<T>(Func<T[]> generator)
            => new LazyArray<T>(generator);

        public static LazyArray<T> ToLazyArray<T>(this IEnumerable<T> @this)
        {
            switch (@this)
            {
                case LazyArray<T> lazy:
                    return lazy;

                default:
                    return new LazyArray<T>(@this);
            }
        }
    }
}
