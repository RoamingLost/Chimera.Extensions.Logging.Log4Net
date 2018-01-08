using System;
using System.Collections.Generic;
using System.Linq;

namespace Chimera.Extensions.Logging.Log4Net
{
    internal static class SafeDisposableFromMultipleIEnumerableExtensions
    {
        public static IDisposable AsSafeDisposableDisposingAll(this IEnumerable<IDisposable> disposablesEnumerable) =>
            new SafeDisposableFromMultiple(disposablesEnumerable);

        private class SafeDisposableFromMultiple : IDisposable
        {
            private readonly Stack<IDisposable> _disposables = new Stack<IDisposable>();

            internal SafeDisposableFromMultiple(IEnumerable<IDisposable> disposablesEnumerable)
            {
                try
                {
                    foreach (var disposable in disposablesEnumerable.Where(_ => _ != null))
                        _disposables.Push(disposable);
                }
                catch (Exception)
                {
                    Dispose();
                    throw;
                }
            }

            public void Dispose()
            {
                var exceptions = new List<Exception>();
                foreach (var disposable in _disposables)
                {
                    try
                    {
                        disposable.Dispose();
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                }
                if (exceptions.Count > 0)
                    throw new AggregateException(exceptions.ToArray());
            }
        }
    }
}