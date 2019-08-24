﻿using System;
using System.Threading.Tasks;
using static System.Console;

namespace ThreadPlayground
{
    class RandomTests
    {
        internal static void Start(string[] args)
        {
            WriteLine("Start!");

            var opResult = InvokeOperationAsync().Result;

            WriteLine("intensive work on Main Thread after blocked thread");
            //intensive work
            for (int i = 0; i < 10_000_000; i++) { }

            WriteLine("END");
        }

        static async Task<int> InvokeOperationAsync()
        {
            var opTask = ChildOperationDelay0();

            WriteLine("START intensive work on InvokeOperationAsync");

            //intensive work
            for (int i = 0; i < 80_000_000; i++) { }

            WriteLine("END intensive work on InvokeOperationAsync (waiting for async child operation is done)");

            var result = await opTask;

            WriteLine("result is get back from child task operation");
            return result;
        }

        static Task<int> ChildOperationSync()
        {
            WriteLine("intensive work on ChildOperationSync");
            //intensive work
            for (int i = 0; i < 100_000_000; i++) { }

            return Task.FromResult<int>(100);
        }

        static Task<int> ChildOpTaskRun()
        {
            return Task.Run<int>(() =>
            {
                WriteLine("intensive work on Anonymous func on ChildOpTaskRun");
                //intensive work
                for (int i = 0; i < 100_000_000; i++) { }

                return 100;
            });
        }

        static async Task<int> ChildOperationDelay0()
        {
            await Task.Delay(0);

            WriteLine("intensive work on ChildOperationDelay0");
            //intensive work
            for (int i = 0; i < 100_000_000; i++) { }

            return 100;
        }

        static Task Teste()
        {
            //intensive work
            for (int i = 0; i < 100_000_000; i++) { }

            return Task.CompletedTask;
        }
    }
}
