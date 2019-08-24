using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadPlayground
{
    internal static class TimedMonitor
    {
        public static void Start()
        {

        }

        //Usando o Monitor (tem o poder do time out) como uma extension function do guard object do struct lock)
        //Se deixar para o client final, vai ter que lembrar de implementar o timeout exception e fazer o Monitor.Release
        //o lock, na IL gera um monitor
        internal static LockHelp Lock(this object guard, int timeout)
        {
            var ts = TimeSpan.FromMilliseconds(timeout);
            bool lockTaken = false;

            try
            {
                Monitor.TryEnter(guard, ts, ref lockTaken);
                if (lockTaken)
                {
                    return new LockHelp(guard);
                }
                else
                {
                    throw new TimeoutException("lock timed out");
                }
            }
            catch
            {
                if (lockTaken)
                {
                    Monitor.Exit(guard);
                }
                throw;
            }
        }

        internal struct LockHelp : IDisposable
        {
            private readonly object _guard;

            public LockHelp(object guard)
            {
                _guard = guard;
            }

            public void Dispose()
            {
                Monitor.Exit(_guard);
            }
        }
    }
}
