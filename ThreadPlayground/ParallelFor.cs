using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPlayground
{
    internal class ParallelFor
    {
        public static void Start()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Sync();
            ParallelVersion();

            stopwatch.Stop();
            Console.WriteLine($"Finished in {stopwatch.ElapsedMilliseconds}");
        }

        static void Sync()
        {
            for (int i = 0; i < 10; i++)
            {
                DoWait(i);
            }
        }

        private static void DoWait(int i)
        {
            Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(i * 10);
        }

        static void ParallelVersion() => Parallel.For(0, 10, index => Decorate(index, DoWait));

        static void Decorate(int index, Action<int> action)
        {
            Console.WriteLine($"next index:{index}");
            action(index);
        }
    }
}
