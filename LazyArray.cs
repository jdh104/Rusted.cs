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
            Source = new Lazy<T[]>(() => source.ToArray());
        }

        public LazyArray(Lazy<T[]> source)
        {
            Source = source;
        }
    }

    public static class LazyArrayExtensions
    {
        public static LazyArray<T> ToLazyArray<T>(this IEnumerable<T> @this)
            => new LazyArray<T>(@this);
    }
}
