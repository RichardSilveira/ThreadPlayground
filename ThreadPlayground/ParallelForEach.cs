using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace ThreadPlayground
{
    internal static class ParallelForEach
    {
        public static void Start()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Sync();
            ParallelForEachVersion();

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
            Thread.Sleep(i * 10);
        }

        static void ParallelForEachVersion() => Parallel.ForEach(Enumerable.Range(0, 10), (index, loopState) =>
        {

            if (index == 5)
            {
                Console.WriteLine("Condition to stop the loop was reached");
                loopState.Stop();
            }
            Decorate(index, DoWait);
        });

        static void Decorate(int index, Action<int> action)
        {
            Console.WriteLine($"next index:{index}");
            action(index);
        }
    }
}
