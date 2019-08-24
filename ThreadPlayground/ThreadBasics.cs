using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadPlayground
{
    class ThreadBasics
    {
        internal static void Start(string[] args)
        {
            var threadStart = new ThreadStart(StarThreadEvent);
            var threadStartParametrized = new ParameterizedThreadStart(StarThreadEventParams);

            Thread thread = new Thread(threadStartParametrized);

            thread.Start("some param");

            Console.WriteLine("Caller operations...");

            thread.Join();

            Console.WriteLine("Caller operations after join...");
        }

        static void StarThreadEvent()
        {

        }

        static void StarThreadEventParams(object param)
        {
            Console.WriteLine($"new thread started with param: {param}");

            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine("new thread finished");
        }
    }
}
