
using System;
using System.Threading.Tasks;

namespace Rusted
{
    public static class Async
    {
        /// <summary>
        /// Wait for the task to finish and return it's result.
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="this"></param>
        /// <returns>The result of this Task</returns>
        /// <exception cref="AggregateException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        public static T Await<T>(this Task<T> @this)
        {
            @this.Wait();
            return @this.Result;
        }
    }
}
