using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static System.Console;

namespace ThreadPlayground
{
    class ExclusiveLock
    {
        internal static void Start()
        {
            for (int i = 0; i < 31; i++)
            {
                int index = i;

                new Thread(() => Divide(index == 0))
                {
                    Name = $"Thread_{index}"
                }.Start();
            }
        }

        private static void Divide(bool wait)
        {
            try
            {
                lock (_stateGuard)
                {
                    if (_val2 != 0)
                    {
                        WriteLine($"val2 != 0 for thread {Thread.CurrentThread.Name}");

                        if (wait)
                        {
                            Thread.Sleep(500);
                        }
                        int result = _val1 / _val2;
                    }
                    else
                        WriteLine($"val == 0 and operation was not executed for thread {Thread.CurrentThread.Name}");
                }
            }
            catch (DivideByZeroException ex)
            {
                WriteLine($"{ex.Message} on thread {Thread.CurrentThread.Name}");
            }
            _val2 = 0;
        }

        private static readonly object _stateGuard = new object();

        private static int _val1 = 2;
        private static int _val2 = 4;
    }
}
