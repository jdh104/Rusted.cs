
using System;
using System.Threading;

namespace Rusted
{
    public static class Lazy
    {
        /// <summary>
        /// Initialize a new Lazy&lt;T&gt; (without having to use clunky "<code>new Lazy&lt;T&gt;()</code>" syntax)
        /// </summary>
        /// <typeparam name="T">The (inferred) type stored within the Lazy object</typeparam>
        /// <param name="valueFactory">The function that evaluates this Lazy object's value</param>
        /// <returns>A newly-instantiated Lazy object</returns>
        public static Lazy<T> From<T>(Func<T> valueFactory)
            => new Lazy<T>(valueFactory);

        /// <summary>
        /// Initialize a new Lazy&lt;T&gt; (without having to use clunky "<code>new Lazy&lt;T&gt;()</code>" syntax)
        /// </summary>
        /// <typeparam name="T">The (inferred) type stored within the Lazy object</typeparam>
        /// <param name="valueFactory">The function that evaluates this Lazy object's value</param>
        /// <param name="isThreadSafe"></param>
        /// <returns>A newly-instantiated Lazy object</returns>
        public static Lazy<T> From<T>(Func<T> valueFactory, bool isThreadSafe)
            => new Lazy<T>(valueFactory, isThreadSafe);

        /// <summary>
        /// Initialize a new Lazy&lt;T&gt; (without having to use clunky "<code>new Lazy&lt;T&gt;()</code>" syntax)
        /// </summary>
        /// <typeparam name="T">The (inferred) type stored within the Lazy object</typeparam>
        /// <param name="valueFactory">The function that evaluates this Lazy object's value</param>
        /// <param name="mode"></param>
        /// <returns>A newly-instantiated Lazy object</returns>
        public static Lazy<T> From<T>(Func<T> valueFactory, LazyThreadSafetyMode mode)
            => new Lazy<T>(valueFactory, mode);
    }
}
