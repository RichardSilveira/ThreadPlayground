using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static System.Console;

namespace ThreadPlayground
{
    class NonExclusiveLockSemaphore
    {
        private static SemaphoreSlim _semaphore = new SemaphoreSlim(3);
        internal static void Start()
        {
            for (int i = 0; i <= 6; i++)
            {
                new Thread(EnterInClub).Start(i);
            }
        }

        private static void EnterInClub(object id)
        {
            WriteLine($"{id} wants to enter");

            _semaphore.Wait();

            WriteLine($"{id} walked in");

            Thread.Sleep(2000 * (int)id);

            WriteLine($"{id} is leaving already");
            _semaphore.Release();
        }
    }
}
