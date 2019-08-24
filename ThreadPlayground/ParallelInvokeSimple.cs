using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPlayground
{
    internal class ParallelInvokeSimple
    {
        public static void Start()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int sum1 = 0, sum2 = 0, sum3 = 0;

            Parallel.Invoke(
                () => sum1 = Sum1(),
                () => sum2 = Sum2(),
                () => sum3 = Sum3());

            int total = sum1 + sum2 + sum3;

            stopwatch.Stop();
            Console.WriteLine($"Total:{total} in {stopwatch.ElapsedMilliseconds}");
        }

        private static int Sum1()
        {
            Thread.Sleep(200);
            return 10;
        }

        private static int Sum2()
        {
            Thread.Sleep(100);
            return 20;
        }

        private static int Sum3()
        {
            Thread.Sleep(300);
            return 30;
        }
    }
}
